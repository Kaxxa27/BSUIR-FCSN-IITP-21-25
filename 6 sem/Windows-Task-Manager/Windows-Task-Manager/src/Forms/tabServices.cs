using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.ServiceProcess;
using sMkTaskManager.Classes;
using sMkTaskManager.Controls;
namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
internal class tabServices : UserControl, ITaskManagerTab {
    private readonly Stopwatch _stopWatch = new();
    private HashSet<string> ColsServices = new();
    private readonly HashSet<string> HashServices = new();
    private readonly TaskManagerServicesCollection Services = new();

    private sMkListView lv;
    private Button btnForceRefresh;
    private ImageList il;
    private ContextMenuStrip cms;
    private ToolStripMenuItem cmsProperties;
    private ToolStripMenuItem cmsFileProperties;
    private ToolStripMenuItem cmsOpenLocation;
    private ToolStripSeparator cmsSeparator1;
    private ToolStripMenuItem cmsStart;
    private ToolStripMenuItem cmsStop;
    private ToolStripMenuItem cmsPause;
    private ToolStripMenuItem cmsResume;
    private ToolStripMenuItem cmsRestart;
    private ToolStripSeparator cmsSeparator2;
    private ToolStripMenuItem cmsGoToProcess;
    private ToolStripMenuItem cmsOnline;
    private Button btnStart;
    private Button btnStop;
    private Button btnRestart;
    private Button btnServices;
    private CheckBox btnDescriptions;
    private ColumnHeader lvName;
    private TextBox txtDescriptions;
    private Label lblText;

    public event EventHandler? ForceRefreshClicked;
    public event EventHandler? RefreshStarts;
    public event EventHandler? RefreshComplete;

    private IContainer? components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }

    public tabServices() {
        InitializeComponent();
        InitializeHandlers();
        InitializeExtras();
        Extensions.CascadingDoubleBuffer(this);
    }
    private void InitializeComponent() {
        components = new Container();
        btnStart = new Button();
        btnStop = new Button();
        btnDescriptions = new CheckBox();
        btnRestart = new Button();
        btnServices = new Button();
        btnForceRefresh = new Button();
        lv = new sMkListView();
        lvName = new ColumnHeader();
        il = new ImageList(components);
        cms = new ContextMenuStrip(components);
        cmsProperties = new ToolStripMenuItem();
        cmsFileProperties = new ToolStripMenuItem();
        cmsOpenLocation = new ToolStripMenuItem();
        cmsSeparator1 = new ToolStripSeparator();
        cmsStart = new ToolStripMenuItem();
        cmsStop = new ToolStripMenuItem();
        cmsPause = new ToolStripMenuItem();
        cmsResume = new ToolStripMenuItem();
        cmsRestart = new ToolStripMenuItem();
        cmsSeparator2 = new ToolStripSeparator();
        cmsGoToProcess = new ToolStripMenuItem();
        cmsOnline = new ToolStripMenuItem();
        txtDescriptions = new TextBox();
        lblText = new Label();
        cms.SuspendLayout();
        SuspendLayout();
        // 
        // btnStart
        // 
        btnStart.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnStart.Enabled = false;
        btnStart.Location = new Point(6, 571);
        btnStart.Margin = new Padding(0, 3, 3, 3);
        btnStart.Name = "btnStart";
        btnStart.Size = new Size(60, 30);
        btnStart.TabIndex = 1;
        btnStart.Text = "Start";
        // 
        // btnStop
        // 
        btnStop.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnStop.Enabled = false;
        btnStop.Location = new Point(69, 571);
        btnStop.Margin = new Padding(0, 3, 3, 3);
        btnStop.Name = "btnStop";
        btnStop.Size = new Size(60, 30);
        btnStop.TabIndex = 2;
        btnStop.Text = "Stop";
        // 
        // btnDescriptions
        // 
        btnDescriptions.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnDescriptions.Appearance = Appearance.Button;
        btnDescriptions.Location = new Point(195, 571);
        btnDescriptions.Margin = new Padding(0, 3, 3, 3);
        btnDescriptions.Name = "btnDescriptions";
        btnDescriptions.Size = new Size(80, 30);
        btnDescriptions.TabIndex = 4;
        btnDescriptions.Text = "Descriptions";
        btnDescriptions.TextAlign = ContentAlignment.MiddleCenter;
        btnDescriptions.UseVisualStyleBackColor = true;
        // 
        // btnRestart
        // 
        btnRestart.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnRestart.Enabled = false;
        btnRestart.Location = new Point(132, 571);
        btnRestart.Margin = new Padding(0, 3, 3, 3);
        btnRestart.Name = "btnRestart";
        btnRestart.Size = new Size(60, 30);
        btnRestart.TabIndex = 3;
        btnRestart.Text = "Restart";
        // 
        // btnServices
        // 
        btnServices.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnServices.Location = new Point(410, 571);
        btnServices.Margin = new Padding(3, 3, 0, 3);
        btnServices.Name = "btnServices";
        btnServices.Size = new Size(92, 30);
        btnServices.TabIndex = 5;
        btnServices.Text = "Services MMC";
        // 
        // btnForceRefresh
        // 
        btnForceRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnForceRefresh.Location = new Point(505, 571);
        btnForceRefresh.Margin = new Padding(3, 3, 0, 3);
        btnForceRefresh.Name = "btnForceRefresh";
        btnForceRefresh.Size = new Size(89, 30);
        btnForceRefresh.TabIndex = 6;
        btnForceRefresh.Text = "Force Refresh";
        // 
        // lv
        // 
        lv.AlternateRowColors = false;
        lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lv.Columns.AddRange(new ColumnHeader[] { lvName });
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
        lv.SmallImageList = il;
        lv.Sortable = true;
        lv.SortColumn = 0;
        lv.Sorting = SortOrder.Ascending;
        lv.TabIndex = 0;
        lv.UseCompatibleStateImageBehavior = false;
        lv.View = View.Details;
        // 
        // lvName
        // 
        lvName.Tag = "Name";
        lvName.Text = "Name";
        lvName.Width = 150;
        // 
        // il
        // 
        il.ColorDepth = ColorDepth.Depth32Bit;
        il.ImageSize = new Size(16, 16);
        il.TransparentColor = Color.Transparent;
        // 
        // cms
        // 
        cms.Items.AddRange(new ToolStripItem[] { cmsProperties, cmsFileProperties, cmsOpenLocation, cmsSeparator1, cmsStart, cmsStop, cmsPause, cmsResume, cmsRestart, cmsSeparator2, cmsGoToProcess, cmsOnline });
        cms.Name = "cms";
        cms.Size = new Size(157, 236);
        // 
        // cmsProperties
        // 
        cmsProperties.Name = "cmsProperties";
        cmsProperties.Size = new Size(156, 22);
        cmsProperties.Text = "P&roperties";
        // 
        // cmsFileProperties
        // 
        cmsFileProperties.Name = "cmsFileProperties";
        cmsFileProperties.Size = new Size(156, 22);
        cmsFileProperties.Text = "File Prope&rties";
        // 
        // cmsOpenLocation
        // 
        cmsOpenLocation.Name = "cmsOpenLocation";
        cmsOpenLocation.Size = new Size(156, 22);
        cmsOpenLocation.Text = "&Open Location";
        // 
        // cmsSeparator1
        // 
        cmsSeparator1.Name = "cmsSeparator1";
        cmsSeparator1.Size = new Size(153, 6);
        // 
        // cmsStart
        // 
        cmsStart.Name = "cmsStart";
        cmsStart.Size = new Size(156, 22);
        cmsStart.Text = "&Start Service";
        // 
        // cmsStop
        // 
        cmsStop.Name = "cmsStop";
        cmsStop.Size = new Size(156, 22);
        cmsStop.Text = "Sto&p Service";
        // 
        // cmsPause
        // 
        cmsPause.Name = "cmsPause";
        cmsPause.Size = new Size(156, 22);
        cmsPause.Text = "Pa&use Service";
        // 
        // cmsResume
        // 
        cmsResume.Name = "cmsResume";
        cmsResume.Size = new Size(156, 22);
        cmsResume.Text = "Resu&me Service";
        // 
        // cmsRestart
        // 
        cmsRestart.Name = "cmsRestart";
        cmsRestart.Size = new Size(156, 22);
        cmsRestart.Text = "R&estart Service";
        // 
        // cmsSeparator2
        // 
        cmsSeparator2.Name = "cmsSeparator2";
        cmsSeparator2.Size = new Size(153, 6);
        // 
        // cmsGoToProcess
        // 
        cmsGoToProcess.Name = "cmsGoToProcess";
        cmsGoToProcess.Size = new Size(156, 22);
        cmsGoToProcess.Text = "&Go To Process...";
        // 
        // cmsOnline
        // 
        cmsOnline.Name = "cmsOnline";
        cmsOnline.Size = new Size(156, 22);
        cmsOnline.Text = "Search &Online...";
        // 
        // txtDescriptions
        // 
        txtDescriptions.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        txtDescriptions.BorderStyle = BorderStyle.FixedSingle;
        txtDescriptions.HideSelection = false;
        txtDescriptions.Location = new Point(6, 505);
        txtDescriptions.Multiline = true;
        txtDescriptions.Name = "txtDescriptions";
        txtDescriptions.ReadOnly = true;
        txtDescriptions.ScrollBars = ScrollBars.Vertical;
        txtDescriptions.Size = new Size(588, 60);
        txtDescriptions.TabIndex = 7;
        txtDescriptions.TabStop = false;
        txtDescriptions.Visible = false;
        // 
        // lblText
        // 
        lblText.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lblText.AutoEllipsis = true;
        lblText.ForeColor = SystemColors.HotTrack;
        lblText.Location = new Point(277, 575);
        lblText.Name = "lblText";
        lblText.Size = new Size(131, 16);
        lblText.TabIndex = 8;
        // 
        // tabServices
        // 
        Controls.Add(lblText);
        Controls.Add(txtDescriptions);
        Controls.Add(lv);
        Controls.Add(btnForceRefresh);
        Controls.Add(btnServices);
        Controls.Add(btnRestart);
        Controls.Add(btnDescriptions);
        Controls.Add(btnStop);
        Controls.Add(btnStart);
        Name = "tabServices";
        Size = new Size(600, 600);
        cms.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }
    private void InitializeHandlers() {
        cms.Opening += OnContextOpening;
        cms.ItemClicked += OnContextItemClicked;
        lv.ColumnReordered += OnListViewColumnReordered;
        lv.SelectedIndexChanged += OnListViewSelectedIndexChanged;
        lv.KeyDown += OnListViewKeyDown;
        lv.KeyPress += OnListViewKeyPress;
        lv.MouseDoubleClick += OnListViewMouseDoubleClick;
        btnForceRefresh.Click += OnButtonClicked;
        btnRestart.Click += OnButtonClicked;
        btnServices.Click += OnButtonClicked;
        btnStart.Click += OnButtonClicked;
        btnStop.Click += OnButtonClicked;
        btnDescriptions.CheckedChanged += OnButtonClicked;
        KeyPress += OnKeyPress;
        VisibleChanged += OnVisibleChanged;
    }
    private void InitializeExtras() {
        il.Images.Clear();
        il.Images.Add(Resources.Resources.Service_Running);
        il.Images.Add(Resources.Resources.Service_Stoped);
        il.Images.Add(Resources.Resources.Service_Disabled);
        lv.ContentType = typeof(TaskManagerService);
        lv.DataSource = Services.DataExporter;
        cmsProperties.SwitchToBold();
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
            case nameof(cmsProperties): { BeginInvoke(Feature_OpenDetails); break; }
            case nameof(cmsFileProperties): { BeginInvoke(Feature_FileProperties); break; }
            case nameof(cmsOpenLocation): { BeginInvoke(Feature_OpenLocation); break; }
            case nameof(cmsGoToProcess): { BeginInvoke(Feature_GoToProcess); break; }
            case nameof(cmsOnline): { BeginInvoke(Feature_SearchOnline); break; }
            case nameof(cmsStart): { BeginInvoke(Feature_ServiceStart); break; }
            case nameof(cmsStop): { BeginInvoke(Feature_ServiceStop); break; }
            case nameof(cmsPause): { BeginInvoke(Feature_ServicePause); break; }
            case nameof(cmsResume): { BeginInvoke(Feature_ServiceResume); break; }
            case nameof(cmsRestart): { BeginInvoke(Feature_ServiceRestart); break; }
            default: break;
        }
    }
    private void OnButtonClicked(object? sender, EventArgs e) {
        if (sender == null) return;
        if (sender == btnStart) { BeginInvoke(Feature_ServiceStart); return; }
        if (sender == btnStop) { BeginInvoke(Feature_ServiceStop); return; }
        if (sender == btnRestart) { BeginInvoke(Feature_ServiceRestart); return; }
        if (sender == btnServices) { BeginInvoke(Feature_ServicesMMC); return; }
        if (sender == btnForceRefresh) { BeginInvoke(Feature_ForceRefresh); return; }
        if (sender == btnDescriptions) { ShowServiceDescriptions = btnDescriptions.Checked; return; }
    }
    private void OnListViewKeyDown(object? sender, KeyEventArgs e) {
        if (e.KeyCode == Keys.Enter && !e.Handled) {
            e.Handled = true;
            cmsProperties.PerformClick();
            e.SuppressKeyPress = true;
        }
    }
    private void OnListViewKeyPress(object? sender, KeyPressEventArgs e) {
        lv.KeyJumper("Name", ref e);
    }
    private void OnListViewColumnReordered(object? sender, ColumnReorderedEventArgs e) {
        if (e.Header!.Text == "Name") { e.Cancel = true; }
        if (e.NewDisplayIndex == 0) { e.Cancel = true; }
    }
    private void OnListViewMouseDoubleClick(object? sender, MouseEventArgs e) {
        if (lv.SelectedItems.Count > 0 && e.Button == MouseButtons.Left) { BeginInvoke(Feature_OpenDetails); }
    }
    private void OnListViewSelectedIndexChanged(object? sender, EventArgs e) {
        btnRestart.Enabled = false;
        btnStop.Enabled = false;
        btnStart.Enabled = false;
        cmsPause.Enabled = false;
        cmsResume.Enabled = false;
        if (lv.SelectedItems.Count < 1 && ShowServiceDescriptions) PopulateServiceDescription(null);

        for (int i = 0; i < lv.SelectedItems.Count; i++) {
            TaskManagerService? tmp = Services.GetService(lv.SelectedItems[i].Name);
            if (tmp == null) continue;
            if (tmp.CanStart) btnStart.Enabled = true;
            if (tmp.CanStop) btnRestart.Enabled = true;
            if (tmp.CanStop) btnStop.Enabled = true;
            if (tmp.CanPause) cmsPause.Enabled = true;
            if (tmp.CanResume) cmsResume.Enabled = true;
        }
        cmsStart.Enabled = btnStart.Enabled;
        cmsStop.Enabled = btnStop.Enabled;
        cmsRestart.Enabled = btnRestart.Enabled;
        if (lv.SelectedItems.Count < 1) return;
        if (Shared.InitComplete && Visible && ShowServiceDescriptions) {
            PopulateServiceDescription(Services.GetService(lv.SelectedItems[0].Name));
        }
    }
    private bool ShowServiceDescriptions {
        get { return txtDescriptions.Visible; }
        set {
            txtDescriptions.Visible = value;
            if (txtDescriptions.Visible) {
                lv.Height -= txtDescriptions.Height + 5;
                OnListViewSelectedIndexChanged(lv, EventArgs.Empty);
            } else {
                lv.Height += txtDescriptions.Height + 5;
            }
        }
    }
    private void PopulateServiceDescription(TaskManagerService? s) {
        txtDescriptions.Text = "";
        if (s == null) return;
        txtDescriptions.Text = s.Description;
    }

    public void Feature_ForceRefresh() {
        lv.SuspendLayout();
        Services.Clear();
        lv.Items.Clear();
        Refresher(true);
        lv.ResumeLayout();
        OnListViewSelectedIndexChanged(lv, new EventArgs());
        ForceRefreshClicked?.Invoke(this, EventArgs.Empty);
    }
    public void Feature_OpenDetails() {
        if (lv.SelectedItems.Count > 0) {
            using (frmService_Details sd = new()) {
                sd.SID = lv.SelectedItems[0].Name;
                sd.ShowInTaskbar = false;
                sd.ShowDialog(this);
            }
            lv.SelectedItems[0].Focused = false;
            lv.SelectedItems[0].Selected = false;
            OnListViewSelectedIndexChanged(lv, EventArgs.Empty);
        }
    }
    public void Feature_ServiceStart() {
        if (lv.SelectedItems.Count < 1) return;
        Shared.BusyCursor = true;
        Shared.SetStatusText((lv.SelectedItems.Count == 1) ? "Starting Service ..." : "Starting Services ...");
        bool allOK = true;
        for (int i = 0; i < lv.SelectedItems.Count; i++) {
            if (!Services.Contains(lv.SelectedItems[i].Name)) continue;
            if (!Services.GetService(lv.SelectedItems[i].Name)!.CanStart) continue;
            if (!Services.GetService(lv.SelectedItems[i].Name)!.Start()) allOK = false;
        }
        if (allOK) {
            Shared.SetStatusText((lv.SelectedItems.Count == 1) ? "Service Started ..." : "Services Started ...");
        } else {
            Shared.SetStatusText((lv.SelectedItems.Count == 1) ? "Service failed to start ..." : "At least one service failed to start ...");
        }
        lv.Focus();
        OnListViewSelectedIndexChanged(lv, EventArgs.Empty);
        Shared.BusyCursor = false;
    }
    public void Feature_ServiceStop() {
        if (lv.SelectedItems.Count < 1) return;
        Shared.BusyCursor = true;
        Shared.SetStatusText((lv.SelectedItems.Count == 1) ? "Stopping Service ..." : "Stopping Services ...");
        bool allOK = true;
        for (int i = 0; i < lv.SelectedItems.Count; i++) {
            if (!Services.Contains(lv.SelectedItems[i].Name)) continue;
            if (!Services.GetService(lv.SelectedItems[i].Name)!.CanStop) continue;
            if (!Services.GetService(lv.SelectedItems[i].Name)!.Stop()) allOK = false;
        }
        if (allOK) {
            Shared.SetStatusText((lv.SelectedItems.Count == 1) ? "Service Stopped ..." : "Services Stopped ...");
        } else {
            Shared.SetStatusText((lv.SelectedItems.Count == 1) ? "Service failed to stop ..." : "At least one service failed to stop ...");
        }
        lv.Focus();
        OnListViewSelectedIndexChanged(lv, EventArgs.Empty);
        Shared.BusyCursor = false;
    }
    public void Feature_ServicePause() {
        if (lv.SelectedItems.Count < 1) return;
        Shared.BusyCursor = true;
        Shared.SetStatusText((lv.SelectedItems.Count == 1) ? "Pausing Service ..." : "Pausing Services ...");
        bool allOK = true;
        for (int i = 0; i < lv.SelectedItems.Count; i++) {
            if (!Services.Contains(lv.SelectedItems[i].Name)) continue;
            if (!Services.GetService(lv.SelectedItems[i].Name)!.CanPause) continue;
            if (!Services.GetService(lv.SelectedItems[i].Name)!.Pause()) allOK = false;
        }
        if (allOK) {
            Shared.SetStatusText((lv.SelectedItems.Count == 1) ? "Service Paused ..." : "Services Paused ...");
        } else {
            Shared.SetStatusText((lv.SelectedItems.Count == 1) ? "Service failed to pause ..." : "At least one service failed to pause ...");
        }
        lv.Focus();
        OnListViewSelectedIndexChanged(lv, EventArgs.Empty);
        Shared.BusyCursor = false;
    }
    public void Feature_ServiceResume() {
        if (lv.SelectedItems.Count < 1) return;
        Shared.BusyCursor = true;
        Shared.SetStatusText((lv.SelectedItems.Count == 1) ? "Resuming Service ..." : "Resuming Services ...");
        bool allOK = true;
        for (int i = 0; i < lv.SelectedItems.Count; i++) {
            if (!Services.Contains(lv.SelectedItems[i].Name)) continue;
            if (!Services.GetService(lv.SelectedItems[i].Name)!.CanResume) continue;
            if (!Services.GetService(lv.SelectedItems[i].Name)!.Resume()) allOK = false;
        }
        if (allOK) {
            Shared.SetStatusText((lv.SelectedItems.Count == 1) ? "Service Resumed ..." : "Services Resumed ...");
        } else {
            Shared.SetStatusText((lv.SelectedItems.Count == 1) ? "Service failed to resume ..." : "At least one service failed to resume ...");
        }
        lv.Focus();
        OnListViewSelectedIndexChanged(lv, EventArgs.Empty);
        Shared.BusyCursor = false;
    }
    public void Feature_ServiceRestart() {
        if (lv.SelectedItems.Count < 1) return;
        Shared.BusyCursor = true;
        Shared.SetStatusText((lv.SelectedItems.Count == 1) ? "Restarting Service ..." : "Restarting Services ...");
        bool allOK = true;
        for (int i = 0; i < lv.SelectedItems.Count; i++) {
            if (!Services.Contains(lv.SelectedItems[i].Name)) continue;
            if (!Services.GetService(lv.SelectedItems[i].Name)!.CanStop) continue;
            if (!Services.GetService(lv.SelectedItems[i].Name)!.Restart()) allOK = false;
        }
        if (allOK) {
            Shared.SetStatusText((lv.SelectedItems.Count == 1) ? "Service Restarted ..." : "Services Restarted ...");
        } else {
            Shared.SetStatusText((lv.SelectedItems.Count == 1) ? "Service failed to restart ..." : "At least one service failed to restart ...");
        }
        lv.Focus();
        OnListViewSelectedIndexChanged(lv, EventArgs.Empty);
        Shared.BusyCursor = false;
    }
    public void Feature_ServicesMMC() {
        using Process x = new();
        x.StartInfo = new ProcessStartInfo(Environment.GetFolderPath(Environment.SpecialFolder.System) + @"\services.msc") {
            WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.System),
            WindowStyle = ProcessWindowStyle.Normal,
            UseShellExecute = true
        };
        x.Start();
    }
    public void Feature_FileProperties() {
        if (lv.SelectedItems.Count < 1) return;
        for (int i = 0; i < lv.SelectedItems.Count; i++) {
            try {
                string? file = Services.GetService(lv.SelectedItems[i].Name)?.ImagePath;
                if (file != null && File.Exists(file)) {
                    API.SHELLEXECUTEINFO sei = new();
                    sei.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(sei);
                    sei.lpVerb = "properties";
                    sei.lpFile = file;
                    sei.nShow = 5;
                    sei.fMask = 12;
                    API.ShellExecuteEx(ref sei);
                }
            } catch (Exception ex) { Shared.DebugTrap(ex, 501); }
        }
    }
    public void Feature_OpenLocation() {
        if (lv.SelectedItems.Count < 1) return;
        for (int i = 0; i < lv.SelectedItems.Count; i++) {
            try {
                string? Path = System.IO.Path.GetDirectoryName(Services.GetService(lv.SelectedItems[i].Name)?.ImagePath);
                if (Path != null && Directory.Exists(Path)) {
                    API.ShellExecute(Handle, "open", Path, "", "", 1);
                }
            } catch (Exception ex) { Shared.DebugTrap(ex, 502); }
        }
    }
    public void Feature_GoToProcess() {
        if (lv.SelectedItems.Count != 1) return;
        string? PID = Services.GetService(lv.SelectedItems[0].Name)?.PID;
        if (!string.IsNullOrEmpty(PID)) {
            Shared.FindOwnerForm();
            Shared.MainForm?.GoToProcess(PID);
        }
    }
    public void Feature_SearchOnline() {
        if (lv.SelectedItems.Count < 1) return;
        try {
            string QueryAddress = "http://www.google.com/search?q=" + Path.GetFileName(Services.GetService(lv.SelectedItems[0].Name)!.ImagePath);
            Process.Start(QueryAddress);
        } catch { }
    }

    public sMkListView ListView => lv;
    public string Title { get; set; } = "Services";
    public string Description { get; set; } = "List of Services";
    public string TimmingKey => "Servs";
    public long TimmingValue => _stopWatch.ElapsedMilliseconds;
    public bool CanSelectColumns => true;
    public TaskManagerColumnTypes ColumnType => TaskManagerColumnTypes.Services;
    public void ForceRefresh() => btnForceRefresh.PerformClick();
    public ListView.ColumnHeaderCollection? GetColumns() => lv.Columns;
    public void SetColumns(in ListView.ListViewItemCollection colItems) {
        lv.SetColumns(colItems);
        ColsServices = lv.Columns.Cast<ColumnHeader>().Select(x => x.Name).ToHashSet()!;
    }

    private void RefresherDoWork(bool firstTime = false) {
        RefreshStarts?.Invoke(this, EventArgs.Empty);
        if (lv.Items.Count == 0) firstTime = true;
        // Store last round items and initialize new ones
        HashSet<string> LastRun = new();
        LastRun.UnionWith(HashServices);
        HashServices.Clear();
        // Iterate through all the services
        int _servicesRunning = 0;
        foreach (ServiceController s in ServiceController.GetServices()) {
            if (Shared.skipServices.Contains(s.DisplayName) || Shared.skipServices.Contains(s.ServiceName)) continue;
            TaskManagerService thisService = new(s.ServiceName);
            HashServices.Add(s.ServiceName);
            if (Services.Contains(s.ServiceName)) {
                thisService = Services.GetService(s.ServiceName)!;
                if (thisService.BackColor == Settings.Highlights.NewColor) thisService.BackColor = Color.Empty;
                try {
                    thisService.Update();
                } catch (Exception ex) { Shared.DebugTrap(ex, 031); }
            } else {
                try {
                    thisService.Load(s);
                } catch (Exception ex) { Shared.DebugTrap(ex, 032); }

                if (Settings.Highlights.NewItems && !firstTime) thisService.BackColor = Settings.Highlights.NewColor;
                Services.Add(thisService);
            }
            if (thisService.StatusCode == ServiceControllerStatus.Running) _servicesRunning++;
        }
        // Clean out old Items
        LastRun.ExceptWith(HashServices);
        for (int i = 0; i < LastRun.Count; i++) {
            TaskManagerService? thisService = Services.GetService(LastRun.ElementAtOrDefault(i)!);
            if (thisService == null) continue;
            if (!Settings.Highlights.RemovedItems || thisService.BackColor == Settings.Highlights.RemovedColor) {
                lv.RemoveItemByKey(thisService.Ident);
                Services.Remove(thisService);
            } else {
                thisService.BackColor = Settings.Highlights.RemovedColor;
                HashServices.Add(thisService.Ident);
            }
        }
        lblText.Text = string.Format("Total: {0}, Running: {1}", lv.Items.Count, _servicesRunning);
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
        Settings.LoadColsInformation(TaskManagerColumnTypes.Services, lv, ref ColsServices);
    }
    public bool SaveSettings() {
        return Settings.SaveColsInformation("colsServices", lv);
    }
    public void ApplySettings() {
        lv.AlternateRowColors = Settings.AlternateRowColors;
    }
}
