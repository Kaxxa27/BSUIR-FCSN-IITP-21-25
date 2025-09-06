using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Security.Cryptography.Xml;
using sMkTaskManager.Classes;
using sMkTaskManager.Controls;
namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
internal class tabConnections : UserControl, ITaskManagerTab {
    private readonly Stopwatch _stopWatch = new();
    private HashSet<string> ColsConnections = new();
    private readonly HashSet<string> HashConnections = new();
    private readonly TaskManagerConnectionCollection Connections = new();

    private sMkListView lv;
    private ContextMenuStrip cms;
    private Button btnForceRefresh;
    private Button btnNaturalSort;
    private CheckBox btnIncludeUDP;
    private CheckBox btnIncludeIPv6;
    private Label lblTotal;
    private ToolStripMenuItem cmsClose;
    private ToolStripSeparator cmsSeparator1;
    private ToolStripMenuItem cmsKillProcess;
    private ToolStripMenuItem cmsGoToProcess;

    public event EventHandler? ForceRefreshClicked;
    public event EventHandler? RefreshStarts;
    public event EventHandler? RefreshComplete;

    private IContainer? components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }

    public tabConnections() {
        InitializeComponent();
        InitializeExtras();
        Extensions.CascadingDoubleBuffer(this);
    }
    private void InitializeComponent() {
        components = new Container();
        btnForceRefresh = new Button();
        lv = new sMkListView();
        cms = new ContextMenuStrip(components);
        btnNaturalSort = new Button();
        btnIncludeUDP = new CheckBox();
        btnIncludeIPv6 = new CheckBox();
        lblTotal = new Label();
        cmsClose = new ToolStripMenuItem();
        cmsSeparator1 = new ToolStripSeparator();
        cmsKillProcess = new ToolStripMenuItem();
        cmsGoToProcess = new ToolStripMenuItem();
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
        btnForceRefresh.TabIndex = 5;
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
        cms.Items.AddRange(new ToolStripItem[] { cmsClose, cmsSeparator1, cmsKillProcess, cmsGoToProcess });
        cms.Name = "cms";
        cms.Size = new Size(169, 76);
        // 
        // btnNaturalSort
        // 
        btnNaturalSort.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnNaturalSort.Location = new Point(413, 571);
        btnNaturalSort.Margin = new Padding(3, 3, 0, 3);
        btnNaturalSort.Name = "btnNaturalSort";
        btnNaturalSort.Size = new Size(89, 23);
        btnNaturalSort.TabIndex = 4;
        btnNaturalSort.Text = "Natural Sort";
        // 
        // btnIncludeUDP
        // 
        btnIncludeUDP.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnIncludeUDP.Appearance = Appearance.Button;
        btnIncludeUDP.Location = new Point(6, 571);
        btnIncludeUDP.Margin = new Padding(3, 3, 0, 3);
        btnIncludeUDP.Name = "btnIncludeUDP";
        btnIncludeUDP.Size = new Size(89, 23);
        btnIncludeUDP.TabIndex = 1;
        btnIncludeUDP.Text = "Include UDP";
        btnIncludeUDP.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // btnIncludeIPv6
        // 
        btnIncludeIPv6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnIncludeIPv6.Appearance = Appearance.Button;
        btnIncludeIPv6.Location = new Point(98, 571);
        btnIncludeIPv6.Margin = new Padding(3, 3, 0, 3);
        btnIncludeIPv6.Name = "btnIncludeIPv6";
        btnIncludeIPv6.Size = new Size(89, 23);
        btnIncludeIPv6.TabIndex = 2;
        btnIncludeIPv6.Text = "Include IPv6";
        btnIncludeIPv6.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblTotal
        // 
        lblTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lblTotal.Location = new Point(194, 571);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(213, 23);
        lblTotal.TabIndex = 3;
        lblTotal.Text = "Total Connections:";
        lblTotal.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // cmsClose
        // 
        cmsClose.Name = "cmsClose";
        cmsClose.Size = new Size(168, 22);
        cmsClose.Text = "&Close Connection";
        // 
        // cmsSeparator1
        // 
        cmsSeparator1.Name = "cmsSeparator1";
        cmsSeparator1.Size = new Size(165, 6);
        // 
        // cmsKillProcess
        // 
        cmsKillProcess.Name = "cmsKillProcess";
        cmsKillProcess.Size = new Size(168, 22);
        cmsKillProcess.Text = "&Kill Process";
        // 
        // cmsGoToProcess
        // 
        cmsGoToProcess.Name = "cmsGoToProcess";
        cmsGoToProcess.Size = new Size(168, 22);
        cmsGoToProcess.Text = "&Go To Process";
        // 
        // tabConnections
        // 
        Controls.Add(lblTotal);
        Controls.Add(btnIncludeIPv6);
        Controls.Add(btnIncludeUDP);
        Controls.Add(btnNaturalSort);
        Controls.Add(lv);
        Controls.Add(btnForceRefresh);
        Name = "tabConnections";
        Size = new Size(600, 600);
        cms.ResumeLayout(false);
        ResumeLayout(false);
    }
    private void InitializeExtras() {
        lv.ContentType = typeof(TaskManagerConnection);
        lv.DataSource = Connections.DataExporter;
        lv.SpaceFirstValue = Settings.IconsInProcess;
        // Add event handlers
        KeyPress += OnKeyPress;
        VisibleChanged += OnVisibleChanged;
        cms.Opening += OnContextOpening;
        cms.ItemClicked += OnContextItemClicked;
        lv.ColumnReordered += OnListViewColumnReordered;
        lv.KeyDown += OnListViewKeyDown;
        lv.KeyPress += OnListViewKeyPress;
        btnForceRefresh.Click += OnButtonClicked;
        btnIncludeIPv6.Click += OnButtonClicked;
        btnIncludeUDP.Click += OnButtonClicked;
        btnNaturalSort.Click += OnButtonClicked;
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
    }
    private void OnContextItemClicked(object? sender, ToolStripItemClickedEventArgs e) {
        if (e.ClickedItem == null) return;
        if (!e.ClickedItem.Enabled) return;
        switch (e.ClickedItem.Name) {
            case nameof(cmsClose): { BeginInvoke(Feature_CloseConnection); break; }
            case nameof(cmsKillProcess): { BeginInvoke(Feature_KillProcess); break; }
            case nameof(cmsGoToProcess): { BeginInvoke(Feature_GoToProcess); break; }
            default: break;
        }
    }
    private void OnButtonClicked(object? sender, EventArgs e) {
        if (sender == null) return;
        if (sender == btnIncludeIPv6) { BeginInvoke(Refresher, true); return; }
        if (sender == btnIncludeUDP) { BeginInvoke(Refresher, true); return; }
        if (sender == btnNaturalSort) { BeginInvoke(Feature_NaturalSort); return; }
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
        Connections.Clear();
        lv.Items.Clear();
        Refresher(true);
        lv.ResumeLayout();
        ForceRefreshClicked?.Invoke(this, EventArgs.Empty);
    }
    public void Feature_GoToProcess() {
        if (lv.SelectedItems.Count != 1) return;
        string? PID = Connections.GetConnection(lv.SelectedItems[0].Name)?.PID.ToString();
        if (!string.IsNullOrEmpty(PID)) {
            Shared.FindOwnerForm();
            Shared.MainForm?.GoToProcess(PID);
        }
    }
    public void Feature_NaturalSort() {
        try { if (lv.Columns.ContainsKey("RemotePort")) { lv.SetSort(lv.Columns["RemotePort"]!.Index, SortOrder.Ascending); } } catch { }
        try { if (lv.Columns.ContainsKey("RemoteAddr")) { lv.SetSort(lv.Columns["RemoteAddr"]!.Index, SortOrder.Ascending); } } catch { }
        try { if (lv.Columns.ContainsKey("LocalPort")) { lv.SetSort(lv.Columns["LocalPort"]!.Index, SortOrder.Ascending); } } catch { }
        try { if (lv.Columns.ContainsKey("LocalAddr")) { lv.SetSort(lv.Columns["LocalAddr"]!.Index, SortOrder.Ascending); } } catch { }
        try { if (lv.Columns.ContainsKey("Protocol")) { lv.SetSort(lv.Columns["Protocol"]!.Index, SortOrder.Ascending); } } catch { }
        try { int curCol = lv.SortColumn; lv.SetSortIcons(ref curCol, -1); } catch { }
    }
    public void Feature_CloseConnection() {
        if (lv.SelectedItems.Count < 1) return;
        Shared.BusyCursor = true;
        Shared.SetStatusText(lv.SelectedItems.Count == 1 ? "Closing Connection ..." : "Closing Connections ...");
        bool allOK = true;
        for (int i = 0; i < lv.SelectedItems.Count; i++) {
            var conn = Connections.GetConnection(lv.SelectedItems[i].Name);
            if (conn == null) continue;
            if (!conn.Close()) allOK = false;
        }
        if (allOK) {
            Shared.SetStatusText(lv.SelectedItems.Count == 1 ? "Connection Closed ..." : "Connections Closed ...");
        } else {
            Shared.SetStatusText(lv.SelectedItems.Count == 1 ? "Connection failed to close ..." : "At least one connection failed to close ...");
        }
        lv.Focus();
        Refresher();
        Shared.BusyCursor = false;
    }
    public void Feature_KillProcess() {
        if (lv.SelectedItems.Count < 1) return;
        if (lv.SelectedItems.Count > 1) {
            if (!(MessageBox.Show("Are you sure you want to kill all selected processes?", ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)) {
                return;
            }
        }
        foreach (ListViewItem itm in lv.SelectedItems) {
            var conn = Connections.GetConnection(itm.Name);
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

    public sMkListView ListView => lv;
    public string Title { get; set; } = "Connections";
    public string Description { get; set; } = "Connections";
    public string TimmingKey => "Conns";
    public long TimmingValue => _stopWatch.ElapsedMilliseconds;
    public bool CanSelectColumns => true;
    public TaskManagerColumnTypes ColumnType => TaskManagerColumnTypes.Connections;
    public void ForceRefresh() => btnForceRefresh.PerformClick();
    public ListView.ColumnHeaderCollection? GetColumns() => lv.Columns;
    public void SetColumns(in ListView.ListViewItemCollection colItems) {
        lv.SetColumns(colItems);
        ColsConnections = lv.Columns.Cast<ColumnHeader>().Select(x => x.Name).ToHashSet()!;
    }

    private void RefresherDoWork(bool firstTime = false) {
        RefreshStarts?.Invoke(this, EventArgs.Empty);
        if (lv.Items.Count == 0) firstTime = true;
        // Store last round items and initialize new ones.
        HashSet<string> LastRun = new();
        LastRun.UnionWith(HashConnections);
        HashConnections.Clear();
        // Iterate through all the items
        foreach (TaskManagerConnection c in TaskManagerConnection.GetAllConnections(btnIncludeUDP.Checked, btnIncludeIPv6.Checked)) {
            TaskManagerConnection? thisConnection;
            HashConnections.Add(c.Ident);
            if (Connections.Contains(c)) {
                thisConnection = Connections.GetConnection(c.Ident)!;
                if (thisConnection.BackColor == Settings.Highlights.NewColor) thisConnection.BackColor = Color.Empty;
                try {
                    thisConnection.Update(c);
                } catch (Exception ex) { Shared.DebugTrap(ex, 041); }
            } else {
                thisConnection = new();
                c.AlreadyCreated = firstTime;
                try {
                    thisConnection.Load(c);
                } catch (Exception ex) { Shared.DebugTrap(ex, 042); }
                if (Settings.Highlights.NewItems && !firstTime) thisConnection.BackColor = Settings.Highlights.NewColor;
                Connections.Add(thisConnection);
            }
            // Since PID can change for a connection we need to recheck for its icon
            if (thisConnection.PID == 0) {
                thisConnection.ImageIndex = 0;
            } else {
                if (thisConnection.PID > Shared.bpi && Settings.IconsInProcess) {
                    thisConnection.ImageIndex = -1;
                    thisConnection.ImageKey = Shared.ProcessIconImageKey(thisConnection.PID, thisConnection.ProcessName, default);
                } else { thisConnection.ImageIndex = 1; }
            }
        }
        // Clean out old Items
        LastRun.ExceptWith(HashConnections);
        for (int i = 0; i < LastRun.Count; i++) {
            TaskManagerConnection? thisConnection = Connections.GetConnection(LastRun.ElementAtOrDefault(i)!);
            if (thisConnection == null) continue;
            if (thisConnection.BackColor == Settings.Highlights.RemovedColor || Settings.Highlights.RemovedItems == false || firstTime) {
                lv.RemoveItemByKey(thisConnection.Ident);
                Connections.Remove(thisConnection);
            } else {
                thisConnection.BackColor = Settings.Highlights.RemovedColor;
                HashConnections.Add(thisConnection.Ident);
            }
        }
        // Update Total Connections Label
        lblTotal.Text = "Total Connections: " + HashConnections.Count;
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
        Settings.LoadColsInformation(TaskManagerColumnTypes.Connections, lv, ref ColsConnections);
    }
    public bool SaveSettings() {
        return Settings.SaveColsInformation("colsConnections", lv);
    }
    public void ApplySettings() {
        lv.AlternateRowColors = Settings.AlternateRowColors;
        lv.SmallImageList = Settings.IconsInProcess ? Shared.ilProcesses : null;
        lv.SpaceFirstValue = Settings.IconsInProcess;
    }
}
