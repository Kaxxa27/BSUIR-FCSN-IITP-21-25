using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Versioning;
using sMkTaskManager.Classes;
using sMkTaskManager.Controls;
namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
internal class tabPorts : UserControl, ITaskManagerTab {
    private readonly Stopwatch _stopWatch = new();
    private HashSet<string> ColsPorts = new();
    private readonly HashSet<string> HashPorts = new();
    private readonly TaskManagerConnectionCollection Ports = new();

    private sMkListView lv;
    private Button btnForceRefresh;
    private ContextMenuStrip cms;
    private CheckBox btnIncludeIPv6;
    private ToolStripMenuItem cmsKillProcess;
    private ToolStripMenuItem cmsGoToProcess;
    private ToolStripMenuItem cmsOnline;
    private Label lblTotal;

    public event EventHandler? ForceRefreshClicked;
    public event EventHandler? RefreshStarts;
    public event EventHandler? RefreshComplete;

    private IContainer? components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }

    public tabPorts() {
        InitializeComponent();
        InitializeExtras();
        Extensions.CascadingDoubleBuffer(this);
    }
    private void InitializeComponent() {
        components = new Container();
        btnForceRefresh = new Button();
        lv = new sMkListView();
        cms = new ContextMenuStrip(components);
        cmsKillProcess = new ToolStripMenuItem();
        cmsGoToProcess = new ToolStripMenuItem();
        cmsOnline = new ToolStripMenuItem();
        lblTotal = new Label();
        btnIncludeIPv6 = new CheckBox();
        cms.SuspendLayout();
        SuspendLayout();
        // 
        // btnForceRefresh
        // 
        btnForceRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnForceRefresh.Location = new Point(505, 571);
        btnForceRefresh.Margin = new Padding(3, 3, 0, 3);
        btnForceRefresh.Name = "btnForceRefresh";
        btnForceRefresh.Size = new Size(89, 23);
        btnForceRefresh.TabIndex = 3;
        btnForceRefresh.Text = "Force Refresh";
        // 
        // lv
        // 
        lv.AlternateRowColors = false;
        lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lv.ContextMenuStrip = cms;
        lv.FullRowSelect = true;
        lv.GridLines = true;
        lv.LabelWrap = false;
        lv.Location = new Point(6, 6);
        lv.Margin = new Padding(6, 6, 6, 0);
        lv.Name = "lv";
        lv.ShowGroups = false;
        lv.ShowItemToolTips = true;
        lv.Size = new Size(588, 559);
        lv.Sortable = true;
        lv.SortColumn = 0;
        lv.Sorting = SortOrder.Ascending;
        lv.TabIndex = 0;
        lv.UseCompatibleStateImageBehavior = false;
        lv.View = View.Details;
        // 
        // cms
        // 
        cms.Items.AddRange(new ToolStripItem[] { cmsKillProcess, cmsGoToProcess, cmsOnline });
        cms.Name = "cms";
        cms.Size = new Size(157, 70);
        // 
        // cmsKillProcess
        // 
        cmsKillProcess.Name = "cmsKillProcess";
        cmsKillProcess.Size = new Size(156, 22);
        cmsKillProcess.Text = "&Kill Process";
        // 
        // cmsGoToProcess
        // 
        cmsGoToProcess.Name = "cmsGoToProcess";
        cmsGoToProcess.Size = new Size(156, 22);
        cmsGoToProcess.Text = "&Go To Process";
        // 
        // cmsOnline
        // 
        cmsOnline.Name = "cmsOnline";
        cmsOnline.Size = new Size(156, 22);
        cmsOnline.Text = "Search &Online...";
        // 
        // lblTotal
        // 
        lblTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lblTotal.Location = new Point(98, 571);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(213, 23);
        lblTotal.TabIndex = 2;
        lblTotal.Text = "Total Listening Ports:";
        lblTotal.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // btnIncludeIPv6
        // 
        btnIncludeIPv6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnIncludeIPv6.Appearance = Appearance.Button;
        btnIncludeIPv6.Location = new Point(6, 571);
        btnIncludeIPv6.Margin = new Padding(3, 3, 0, 3);
        btnIncludeIPv6.Name = "btnIncludeIPv6";
        btnIncludeIPv6.Size = new Size(89, 23);
        btnIncludeIPv6.TabIndex = 1;
        btnIncludeIPv6.Text = "Include IPv6";
        btnIncludeIPv6.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // tabPorts
        // 
        Controls.Add(lblTotal);
        Controls.Add(btnIncludeIPv6);
        Controls.Add(lv);
        Controls.Add(btnForceRefresh);
        Name = "tabPorts";
        Size = new Size(600, 600);
        cms.ResumeLayout(false);
        ResumeLayout(false);
    }
    private void InitializeExtras() {
        lv.ContentType = typeof(TaskManagerConnection);
        lv.DataSource = Ports.DataExporter;
        lv.SpaceFirstValue = Settings.IconsInProcess;
        // Add event handlers
        KeyPress += OnKeyPress;
        VisibleChanged += OnVisibleChanged;
        cms.Opening += OnContextOpening;
        cms.ItemClicked += OnContextItemClicked;
        lv.ColumnReordered += OnListViewColumnReordered;
        lv.KeyDown += OnListViewKeyDown;
        lv.KeyPress += OnListViewKeyPress;
        btnIncludeIPv6.Click += OnButtonClicked;
        btnForceRefresh.Click += OnButtonClicked;
    }

    private void OnKeyPress(object? sender, KeyPressEventArgs e) {
        lv.Focus();
        OnListViewKeyPress(lv, e);
    }
    private void OnVisibleChanged(object? sender, EventArgs e) {
        if (Visible && lv.Items.Count == 0 && Shared.InitComplete) {
            SuspendLayout(); Refresher(true); ResumeLayout();
        }
    }
    private void OnContextOpening(object? sender, CancelEventArgs e) {
        e.Cancel = lv.SelectedItems.Count == 0;
        cmsGoToProcess.Enabled = lv.SelectedItems.Count == 1;
        cmsOnline.Enabled = lv.SelectedItems.Count == 1;
    }
    private void OnContextItemClicked(object? sender, ToolStripItemClickedEventArgs e) {
        if (e.ClickedItem == null) return;
        if (!e.ClickedItem.Enabled) return;
        switch (e.ClickedItem.Name) {
            case nameof(cmsOnline): { BeginInvoke(Feature_SearchOnline); break; }
            case nameof(cmsKillProcess): { BeginInvoke(Feature_KillProcess); break; }
            case nameof(cmsGoToProcess): { BeginInvoke(Feature_GoToProcess); break; }
            default: break;
        }
    }
    private void OnButtonClicked(object? sender, EventArgs e) {
        if (sender == null) return;
        if (sender == btnIncludeIPv6) { BeginInvoke(Refresher, true); return; }
        if (sender == btnForceRefresh) { BeginInvoke(Feature_ForceRefresh); return; }
    }
    private void OnListViewKeyDown(object? sender, KeyEventArgs e) {
        if (e.Control && e.KeyCode == Keys.A) {
            e.Handled = true;
            foreach (sMkListViewItem itm in lv.Items) { itm.Selected = true; }
        }
    }
    private void OnListViewKeyPress(object? sender, KeyPressEventArgs e) {
        lv.KeyJumper("Process", ref e);
    }
    private void OnListViewColumnReordered(object? sender, ColumnReorderedEventArgs e) {
        if (e.Header!.Text == "PID") { e.Cancel = true; }
        if (e.NewDisplayIndex == 0) { e.Cancel = true; }
    }

    public void Feature_ForceRefresh() {
        lv.SuspendLayout();
        Ports.Clear();
        lv.Items.Clear();
        Refresher(true);
        lv.ResumeLayout();
        ForceRefreshClicked?.Invoke(this, EventArgs.Empty);
    }
    public void Feature_GoToProcess() {
        if (lv.SelectedItems.Count != 1) return;
        string? PID = Ports.GetConnection(lv.SelectedItems[0].Name)?.PID.ToString();
        if (!string.IsNullOrEmpty(PID)) {
            Shared.FindOwnerForm();
            Shared.MainForm?.GoToProcess(PID);
        }
    }
    public void Feature_KillProcess() {
        if (lv.SelectedItems.Count < 1) return;
        if (lv.SelectedItems.Count > 1) {
            if (!(MessageBox.Show("Are you sure you want to kill all selected processes?", ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)) {
                return;
            }
        }
        foreach (ListViewItem itm in lv.SelectedItems) {
            var conn = Ports.GetConnection(itm.Name);
            if (conn == null) continue;
            if (conn.PID < Shared.bpi) continue;
            try {
                TaskManagerProcess p = new(conn.PID);
                p.Kill();
            } catch {
                Shared.SetStatusText("Failed to terminate Process " + conn.PID + " ...");
            }
            Shared.SetStatusText("Process " + conn.PID + " && Connections Terminated ...");
        }
        Refresher();
    }
    public void Feature_SearchOnline() {
        if (lv.SelectedItems.Count < 1) return;
        try {
            string QueryAddress = "http://www.google.com/search?q=Port%20" + Path.GetFileName(Ports.GetConnection(lv.SelectedItems[0].Name)!.LocalPort);
            Process.Start(QueryAddress);
        } catch { }
    }

    public sMkListView ListView => lv;
    public string Title { get; set; } = "Ports";
    public string Description { get; set; } = "Display Listening Ports";
    public string TimmingKey => "Ports";
    public long TimmingValue => _stopWatch.ElapsedMilliseconds;
    public bool CanSelectColumns => false;
    public TaskManagerColumnTypes ColumnType => TaskManagerColumnTypes.Ports;
    public void ForceRefresh() => btnForceRefresh.PerformClick();
    public ListView.ColumnHeaderCollection? GetColumns() => lv.Columns;
    public void SetColumns(in ListView.ListViewItemCollection colItems) {
        lv.SetColumns(colItems);
        ColsPorts = lv.Columns.Cast<ColumnHeader>().Select(x => x.Name).ToHashSet()!;
    }

    private void RefresherDoWork(bool firstTime = false) {
        RefreshStarts?.Invoke(this, EventArgs.Empty);
        if (lv.Items.Count == 0) firstTime = true;
        // Store last round items and initialize new ones.
        HashSet<string> LastRun = new();
        LastRun.UnionWith(HashPorts);
        HashPorts.Clear();
        // Iterate through all the items
        foreach (TaskManagerConnection c in TaskManagerConnection.GetAllListeners(btnIncludeIPv6.Checked)) {
            TaskManagerConnection? thisPort;
            HashPorts.Add(c.Ident);
            if (Ports.Contains(c)) {
                thisPort = Ports.GetConnection(c.Ident)!;
                if (thisPort.BackColor == Settings.Highlights.NewColor) thisPort.BackColor = Color.Empty;
                try {
                    thisPort.Update(c);
                } catch (Exception ex) { Shared.DebugTrap(ex, 071); }
            } else {
                thisPort = new();
                c.AlreadyCreated = firstTime;
                try {
                    thisPort.Load(c);
                } catch (Exception ex) { Shared.DebugTrap(ex, 072); }
                if (Settings.Highlights.NewItems && !firstTime) thisPort.BackColor = Settings.Highlights.NewColor;
                Ports.Add(thisPort);
            }
            // Since PID can change for a connection we need to recheck for its icon
            if (thisPort.PID == 0) {
                thisPort.ImageIndex = 0;
            } else {
                if (thisPort.PID > Shared.bpi && Settings.IconsInProcess) {
                    thisPort.ImageIndex = -1;
                    thisPort.ImageKey = Shared.ProcessIconImageKey(thisPort.PID, thisPort.ProcessName, default);
                } else {
                    thisPort.ImageIndex = 1;
                }
            }
        }
        // Clean out old Items
        LastRun.ExceptWith(HashPorts);
        for (int i = 0; i < LastRun.Count; i++) {
            TaskManagerConnection? thisPort = Ports.GetConnection(LastRun.ElementAtOrDefault(i)!);
            if (thisPort == null) continue;
            if (thisPort.BackColor == Settings.Highlights.RemovedColor || Settings.Highlights.RemovedItems == false || firstTime) {
                lv.RemoveItemByKey(thisPort.Ident);
                Ports.Remove(thisPort);
            } else {
                thisPort.BackColor = Settings.Highlights.RemovedColor;
                HashPorts.Add(thisPort.Ident);
            }
        }
        // Update Total Connections Label
        lblTotal.Text = "Total Listening Ports: " + HashPorts.Count;
        RefreshComplete?.Invoke(this, EventArgs.Empty);
    }
    public void Refresher(bool firstTime = false) {
        _stopWatch.Restart();
        if (Visible || firstTime) {
            if (InvokeRequired) {
                Invoke(() => RefresherDoWork(firstTime));
            } else {
                RefresherDoWork(firstTime);
            }
        }
        _stopWatch.Stop();
    }
    public void LoadSettings() {
        Settings.LoadColsInformation(TaskManagerColumnTypes.Ports, lv, ref ColsPorts);
    }
    public bool SaveSettings() {
        // return Settings.SaveColsInformation("colsPorts", lv);
        return true;
    }
    public void ApplySettings() {
        lv.AlternateRowColors = Settings.AlternateRowColors;
        lv.SmallImageList = Settings.IconsInProcess ? Shared.ilProcesses : null;
        lv.SpaceFirstValue = Settings.IconsInProcess;
    }

}
