using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.Versioning;
using sMkTaskManager.Classes;
using sMkTaskManager.Controls;
namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
internal class tabApplications : UserControl, ITaskManagerTab {
    private readonly Stopwatch _stopWatch = new();
    private readonly Dictionary<int, winInformation> HashApps = new();

    private sMkListView lv;
    private ColumnHeader colPID;
    private ColumnHeader colTask;
    private ColumnHeader colHandle;
    private ColumnHeader colStatus;
    private Button btnEndTask;
    private Button btnKillTask;
    private Button btnSwitchTo;
    private Button btnGoToProcess;
    private Button btnNewTask;
    private ContextMenuStrip cms;
    private ToolStripMenuItem cmsSwitchTo;
    private ToolStripMenuItem cmsToFront;
    private ToolStripMenuItem cmsMinimize;
    private ToolStripMenuItem cmsMaximize;
    private ToolStripMenuItem cmsCascade;
    private ToolStripMenuItem cmsTileVertical;
    private ToolStripMenuItem cmsTileHorizontal;
    private ToolStripMenuItem cmsEndTask;
    private ToolStripMenuItem cmsKillTask;
    private ToolStripMenuItem cmsGoToProcess;
    private ToolStripSeparator cmsSeparator1;
    private ToolStripSeparator cmsSeparator2;

    public event EventHandler? ForceRefreshClicked;
    public event EventHandler? RefreshStarts;
    public event EventHandler? RefreshComplete;

    private IContainer? components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }
    public tabApplications() {
        InitializeComponent();
        InitializeExtras();
        Extensions.CascadingDoubleBuffer(this);
    }
    private void InitializeComponent() {
        components = new Container();
        lv = new sMkListView();
        cms = new ContextMenuStrip(components);
        cmsSwitchTo = new ToolStripMenuItem();
        cmsToFront = new ToolStripMenuItem();
        cmsSeparator1 = new ToolStripSeparator();
        cmsMinimize = new ToolStripMenuItem();
        cmsMaximize = new ToolStripMenuItem();
        cmsCascade = new ToolStripMenuItem();
        cmsTileVertical = new ToolStripMenuItem();
        cmsTileHorizontal = new ToolStripMenuItem();
        cmsSeparator2 = new ToolStripSeparator();
        cmsEndTask = new ToolStripMenuItem();
        cmsKillTask = new ToolStripMenuItem();
        cmsGoToProcess = new ToolStripMenuItem();
        btnEndTask = new Button();
        btnKillTask = new Button();
        btnSwitchTo = new Button();
        btnGoToProcess = new Button();
        btnNewTask = new Button();
        colPID = new ColumnHeader();
        colTask = new ColumnHeader();
        colHandle = new ColumnHeader();
        colStatus = new ColumnHeader();
        cms.SuspendLayout();
        SuspendLayout();
        // 
        // lv
        // 
        lv.AlternateRowColors = false;
        lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lv.Columns.AddRange(new ColumnHeader[] { colPID, colTask, colHandle, colStatus });
        lv.ContextMenuStrip = cms;
        lv.FullRowSelect = true;
        lv.LabelWrap = false;
        lv.Location = new Point(6, 6);
        lv.Margin = new Padding(6, 6, 6, 0);
        lv.Name = "lv";
        lv.ShowGroups = false;
        lv.Size = new Size(588, 559);
        lv.SmallImageList = Shared.ilProcesses;
        lv.Sortable = true;
        lv.SortColumn = 0;
        lv.Sorting = SortOrder.Ascending;
        lv.TabIndex = 0;
        lv.UseCompatibleStateImageBehavior = false;
        lv.View = View.Details;
        // 
        // cms
        // 
        cms.Items.AddRange(new ToolStripItem[] { cmsSwitchTo, cmsToFront, cmsSeparator1, cmsMinimize, cmsMaximize, cmsCascade, cmsTileVertical, cmsTileHorizontal, cmsSeparator2, cmsEndTask, cmsKillTask, cmsGoToProcess });
        cms.Name = "cms";
        cms.Size = new Size(160, 236);
        // 
        // cmsSwitchTo
        // 
        cmsSwitchTo.Name = "cmsSwitchTo";
        cmsSwitchTo.Size = new Size(159, 22);
        cmsSwitchTo.Text = "&Switch To";
        // 
        // cmsToFront
        // 
        cmsToFront.Name = "cmsToFront";
        cmsToFront.Size = new Size(159, 22);
        cmsToFront.Text = "&Bring To Front";
        // 
        // cmsSeparator1
        // 
        cmsSeparator1.Name = "cmsSeparator1";
        cmsSeparator1.Size = new Size(156, 6);
        // 
        // cmsMinimize
        // 
        cmsMinimize.Name = "cmsMinimize";
        cmsMinimize.Size = new Size(159, 22);
        cmsMinimize.Text = "Mi&nimize";
        // 
        // cmsMaximize
        // 
        cmsMaximize.Name = "cmsMaximize";
        cmsMaximize.Size = new Size(159, 22);
        cmsMaximize.Text = "Ma&ximize";
        // 
        // cmsCascade
        // 
        cmsCascade.Name = "cmsCascade";
        cmsCascade.Size = new Size(159, 22);
        cmsCascade.Text = "&Cascade";
        // 
        // cmsTileVertical
        // 
        cmsTileVertical.Name = "cmsTileVertical";
        cmsTileVertical.Size = new Size(159, 22);
        cmsTileVertical.Text = "Tile &Vertically";
        // 
        // cmsTileHorizontal
        // 
        cmsTileHorizontal.Name = "cmsTileHorizontal";
        cmsTileHorizontal.Size = new Size(159, 22);
        cmsTileHorizontal.Text = "Tile &Horizontally";
        // 
        // cmsSeparator2
        // 
        cmsSeparator2.Name = "cmsSeparator2";
        cmsSeparator2.Size = new Size(156, 6);
        // 
        // cmsEndTask
        // 
        cmsEndTask.Name = "cmsEndTask";
        cmsEndTask.Size = new Size(159, 22);
        cmsEndTask.Text = "&End Task";
        // 
        // cmsKillTask
        // 
        cmsKillTask.Name = "cmsKillTask";
        cmsKillTask.Size = new Size(159, 22);
        cmsKillTask.Text = "&Kill Task";
        // 
        // cmsGoToProcess
        // 
        cmsGoToProcess.Name = "cmsGoToProcess";
        cmsGoToProcess.Size = new Size(159, 22);
        cmsGoToProcess.Text = "&Go To Process";
        // 
        // btnEndTask
        // 
        btnEndTask.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnEndTask.Enabled = false;
        btnEndTask.Location = new Point(167, 571);
        btnEndTask.Margin = new Padding(0, 3, 3, 3);
        btnEndTask.Name = "btnEndTask";
        btnEndTask.Size = new Size(75, 30);
        btnEndTask.TabIndex = 1;
        btnEndTask.Text = "End Task";
        // 
        // btnKillTask
        // 
        btnKillTask.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnKillTask.Enabled = false;
        btnKillTask.Location = new Point(245, 571);
        btnKillTask.Margin = new Padding(0, 3, 3, 3);
        btnKillTask.Name = "btnKillTask";
        btnKillTask.Size = new Size(75, 30);
        btnKillTask.TabIndex = 2;
        btnKillTask.Text = "Kill Task";
        // 
        // btnSwitchTo
        // 
        btnSwitchTo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnSwitchTo.Enabled = false;
        btnSwitchTo.Location = new Point(323, 571);
        btnSwitchTo.Margin = new Padding(0, 3, 3, 3);
        btnSwitchTo.Name = "btnSwitchTo";
        btnSwitchTo.Size = new Size(85, 30);
        btnSwitchTo.TabIndex = 3;
        btnSwitchTo.Text = "Switch To";
        // 
        // btnGoToProcess
        // 
        btnGoToProcess.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnGoToProcess.Enabled = false;
        btnGoToProcess.Location = new Point(411, 571);
        btnGoToProcess.Margin = new Padding(0, 3, 3, 3);
        btnGoToProcess.Name = "btnGoToProcess";
        btnGoToProcess.Size = new Size(95, 30);
        btnGoToProcess.TabIndex = 4;
        btnGoToProcess.Text = "Go To Process";
        // 
        // btnNewTask
        // 
        btnNewTask.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnNewTask.Enabled = true;
        btnNewTask.Location = new Point(509, 571);
        btnNewTask.Margin = new Padding(0, 3, 3, 3);
        btnNewTask.Name = "btnNewTask";
        btnNewTask.Size = new Size(85, 30);
        btnNewTask.TabIndex = 5;
        btnNewTask.Text = "New Task...";
        // 
        // colPID
        // 
        colPID.Tag = "PID";
        colPID.Text = "PID";
        colPID.Width = 70;
        // 
        // colTask
        // 
        colTask.Tag = "Task";
        colTask.Text = "Task";
        colTask.Width = 300;
        // 
        // colHandle
        // 
        colHandle.Tag = "Handle";
        colHandle.Text = "Handle";
        colHandle.TextAlign = HorizontalAlignment.Center;
        // 
        // colStatus
        // 
        colStatus.Tag = "Status";
        colStatus.Text = "Status";
        colStatus.TextAlign = HorizontalAlignment.Right;
        colStatus.Width = 70;
        // 
        // tabApplications
        // 
        Controls.Add(lv);
        Controls.Add(btnNewTask);
        Controls.Add(btnEndTask);
        Controls.Add(btnKillTask);
        Controls.Add(btnGoToProcess);
        Controls.Add(btnSwitchTo);
        Name = "tabApplications";
        Size = new Size(600, 600);
        cms.ResumeLayout(false);
        ResumeLayout(false);
    }
    private void InitializeExtras() {
        cmsSwitchTo.SwitchToBold();
        // Add event handlers
        cms.Opening += OnContextOpening;
        cms.ItemClicked += OnContextItemClicked;
        lv.KeyDown += OnListViewKeyDown;
        lv.KeyPress += OnListViewKeyPress;
        lv.ColumnReordered += OnListViewColumnReordered;
        lv.MouseDoubleClick += OnListViewMouseDoubleClick;
        lv.SelectedIndexChanged += OnListViewSelectedIndexChanged;
        lv.SizeChanged += OnListViewSizeChanged;
        lv.ColumnWidthChanging += OnListViewColumnWidthChanging;
        btnEndTask.Click += OnButtonClicked;
        btnGoToProcess.Click += OnButtonClicked;
        btnKillTask.Click += OnButtonClicked;
        btnNewTask.Click += OnButtonClicked;
        btnSwitchTo.Click += OnButtonClicked;
        KeyPress += OnKeyPress;
        VisibleChanged += OnVisibleChanged;
    }

    private void OnKeyPress(object? sender, KeyPressEventArgs e) {
        lv.Focus();
        OnListViewKeyPress(lv, e);
    }
    private void OnVisibleChanged(object? sender, EventArgs e) {
        if (Visible && lv.Items.Count == 0 && Shared.InitComplete) {
            SuspendLayout(); Refresher(true); ResumeLayout();
            OnListViewSizeChanged(lv, EventArgs.Empty);
        }
    }
    private void OnContextOpening(object? sender, CancelEventArgs e) {
        e.Cancel = lv.SelectedItems.Count == 0;
        cmsGoToProcess.Enabled = lv.SelectedItems.Count == 1;
        cmsCascade.Enabled = lv.SelectedItems.Count > 1;
        cmsTileHorizontal.Enabled = lv.SelectedItems.Count > 1;
        cmsTileVertical.Enabled = lv.SelectedItems.Count > 1;
    }
    private void OnContextItemClicked(object? sender, ToolStripItemClickedEventArgs e) {
        if (e.ClickedItem == null) return;
        if (!e.ClickedItem.Enabled) return;
        switch (e.ClickedItem.Name) {
            case nameof(cmsCascade): { BeginInvoke(Feature_Cascade); break; }
            case nameof(cmsMaximize): { BeginInvoke(Feature_Maximize); break; }
            case nameof(cmsMinimize): { BeginInvoke(Feature_Minimize); break; }
            case nameof(cmsTileHorizontal): { BeginInvoke(Feature_TileHorizontal); break; }
            case nameof(cmsTileVertical): { BeginInvoke(Feature_TileVertical); break; }
            case nameof(cmsEndTask): { BeginInvoke(Feature_EndTask); break; }
            case nameof(cmsKillTask): { BeginInvoke(Feature_KillTask); break; }
            case nameof(cmsSwitchTo): { BeginInvoke(Feature_SwitchTo); break; }
            case nameof(cmsToFront): { BeginInvoke(Feature_ToFront); break; }
            case nameof(cmsGoToProcess): { BeginInvoke(Feature_GoToProcess); break; }
            default: break;
        }
    }
    private void OnButtonClicked(object? sender, EventArgs e) {
        if (sender == null) return;
        if (sender == btnEndTask) { BeginInvoke(Feature_EndTask); return; }
        if (sender == btnGoToProcess) { BeginInvoke(Feature_GoToProcess); return; }
        if (sender == btnKillTask) { BeginInvoke(Feature_KillTask); return; }
        if (sender == btnNewTask) { BeginInvoke(Feature_NewTask); return; }
        if (sender == btnSwitchTo) { BeginInvoke(Feature_SwitchTo); return; }
    }
    private void OnListViewKeyDown(object? sender, KeyEventArgs e) {
        if (e.Control && e.KeyCode == Keys.A) {
            e.Handled = true;
            foreach (sMkListViewItem itm in lv.Items) { itm.Selected = true; }
        } else if (e.KeyCode == Keys.Enter && !e.Handled) {
            e.Handled = true;
            btnSwitchTo.PerformClick();
            e.SuppressKeyPress = true;
        }
    }
    private void OnListViewKeyPress(object? sender, KeyPressEventArgs e) {
        lv.KeyJumper("Task", ref e);
    }
    private void OnListViewColumnReordered(object? sender, ColumnReorderedEventArgs e) {
        if (e.Header!.Text == "PID") { e.Cancel = true; }
        if (e.NewDisplayIndex == 0) { e.Cancel = true; }
    }
    private void OnListViewMouseDoubleClick(object? sender, MouseEventArgs e) {
        if (lv.SelectedItems.Count > 0 && e.Button == MouseButtons.Left) { btnSwitchTo.PerformClick(); }
    }
    private void OnListViewSelectedIndexChanged(object? sender, EventArgs e) {
        btnEndTask.Enabled = lv.SelectedItems.Count > 0;
        btnKillTask.Enabled = lv.SelectedItems.Count > 0;
        btnGoToProcess.Enabled = lv.SelectedItems.Count == 1;
        btnSwitchTo.Enabled = lv.SelectedItems.Count == 1;
    }
    private void OnListViewSizeChanged(object? sender, EventArgs e) {
        if (lv.Columns.Count >= 4 && lv.Width > 200) {
            lv.Columns[1].Width = lv.Width - (lv.Columns[0].Width + lv.Columns[2].Width + lv.Columns[3].Width + 30);
        }
    }
    private void OnListViewColumnWidthChanging(object? sender, ColumnWidthChangingEventArgs e) {
        if (lv.Visible && lv.Columns.Count >= 4 && e.ColumnIndex != 1) {
            lv.Columns[1].Width = lv.Width - (lv.Columns[0].Width + lv.Columns[2].Width + lv.Columns[3].Width + 30);
        }
    }

    public void Feature_Minimize() {
        if (lv.SelectedItems.Count == 0) return;
        foreach (sMkListViewItem itm in lv.SelectedItems) {
            API.ShowWindow(itm.ID, API.WindowShowCommand.SW_MINIMIZE);
        }
        Shared.SetStatusText("Minimize signal sent to selected task(s) ...");
        BringToFront(); Focus();
    }
    public void Feature_Maximize() {
        if (lv.SelectedItems.Count == 0) return;
        foreach (sMkListViewItem itm in lv.SelectedItems) {
            API.ShowWindow(itm.ID, API.WindowShowCommand.SW_SHOWMAXIMIZED);
        }
        Shared.SetStatusText("Maximize signal sent to selected task(s) ...");
        BringToFront(); Focus();
    }
    public void Feature_Cascade() {
        if (lv.SelectedItems.Count == 0) return;
        List<IntPtr> toarrange = new();
        foreach (sMkListViewItem itm in lv.SelectedItems) {
            toarrange.Add(itm.ID);
            if ((API.IsWindowIconic(itm.ID) || API.IsWindowZoomed(itm.ID))) {
                API.ShowWindow(itm.ID, API.WindowShowCommand.SW_RESTORE);
            }
        }
        if (toarrange.Count < 1) return;
        API.TileWindows(IntPtr.Zero, 0, IntPtr.Zero, (uint)toarrange.Count, toarrange.ToArray());
        Shared.SetStatusText("Cascaded selected windows ...");
    }
    public void Feature_TileVertical() {
        if (lv.SelectedItems.Count == 0) return;
        List<IntPtr> toarrange = new();
        foreach (sMkListViewItem itm in lv.SelectedItems) {
            toarrange.Add(itm.ID);
            if ((API.IsWindowIconic(itm.ID) || API.IsWindowZoomed(itm.ID))) {
                API.ShowWindow(itm.ID, API.WindowShowCommand.SW_RESTORE);
            }
        }
        if (toarrange.Count < 1) return;
        API.CascadeWindows(IntPtr.Zero, 0, IntPtr.Zero, (uint)toarrange.Count, toarrange.ToArray());
        Shared.SetStatusText("Vertically tiled selected windows ...");
    }
    public void Feature_TileHorizontal() {
        if (lv.SelectedItems.Count == 0) return;
        List<IntPtr> toarrange = new();
        foreach (sMkListViewItem itm in lv.SelectedItems) {
            toarrange.Add(itm.ID);
            if ((API.IsWindowIconic(itm.ID) || API.IsWindowZoomed(itm.ID))) {
                API.ShowWindow(itm.ID, API.WindowShowCommand.SW_RESTORE);
            }
        }
        if (toarrange.Count < 1) return;
        API.CascadeWindows(IntPtr.Zero, 1, IntPtr.Zero, (uint)toarrange.Count, toarrange.ToArray());
        Shared.SetStatusText("Horizontally tiled selected windows ...");
    }
    public void Feature_ToFront() {
        if (lv.SelectedItems.Count == 0) return;
        foreach (sMkListViewItem itm in lv.SelectedItems) {
            WindowToForeground(itm.ID);
        }
        Shared.SetStatusText("Brought selected task(s) to front ...");
    }
    public void Feature_SwitchTo() {
        if (lv.SelectedItems.Count != 1) return;
        foreach (sMkListViewItem itm in lv.SelectedItems) {
            Shared.FindOwnerForm();
            if (Shared.MainForm != null) Shared.MainForm.WindowState = FormWindowState.Minimized;
            WindowToForeground(itm.ID);
        }
        Shared.SetStatusText("Switching to selected task ...");
    }
    public void Feature_NewTask() {
        API.RunFileDlg(Handle, IntPtr.Zero, null, null, null, 0);
    }
    public void Feature_EndTask() {
        if (lv.SelectedItems.Count == 0) return;
        foreach (sMkListViewItem itm in lv.SelectedItems) {
            API.PostMessage(itm.ID, 0x10, IntPtr.Zero, IntPtr.Zero); // WM_CLOSE = &H10
        }
        Shared.SetStatusText("End signal sent to selected task(s) ...");
    }
    public void Feature_KillTask() {
        if (lv.SelectedItems.Count < 1) return;
        if (!(MessageBox.Show("Are you sure you want to kill all selected tasks procceses?", ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)) {
            return;
        }
        HashSet<int> PIDsToKill = new();
        foreach (sMkListViewItem itm in lv.SelectedItems) {
            if (!Shared.IsNumeric(itm.Text)) continue;
            if (!PIDsToKill.Contains(int.Parse(itm.Text))) { PIDsToKill.Add(int.Parse(itm.Text)); }
        }
        if (PIDsToKill.Count == 0) return;
        foreach (int PID in PIDsToKill) {
            try {
                Process.GetProcessById(PID).Kill();
            } catch { }
        }
        Shared.SetStatusText(PIDsToKill.Count < 2 ? "Process Terminated ..." : "Processes Terminated ...");
        Refresher();
    }
    public void Feature_GoToProcess() {
        if (lv.SelectedItems.Count != 1) return;
        string? PID = lv.SelectedItems[0].Text.Trim();
        if (!string.IsNullOrEmpty(PID)) {
            Shared.FindOwnerForm();
            Shared.MainForm?.GoToProcess(PID);
        }
    }

    public sMkListView ListView => lv;
    public string Title { get; set; } = "Applications";
    public string Description { get; set; } = "Running Applications";
    public string TimmingKey => "Apps";
    public long TimmingValue => _stopWatch.ElapsedMilliseconds;
    public bool CanSelectColumns => false;
    public TaskManagerColumnTypes ColumnType => TaskManagerColumnTypes.Applications;
    public void ForceRefresh() => ForceRefreshClicked?.Invoke(this, EventArgs.Empty);

    private void RefresherDoWork(bool firstTime = false) {
        RefreshStarts?.Invoke(this, EventArgs.Empty);
        if (lv.Items.Count == 0) firstTime = true;
        bool _NeedReSort = false;
        HashApps.Clear();
        API.EnumWindowsProc callbackPtr = new(EnumerateWindowsCallback);
        API.EnumWindows(callbackPtr, IntPtr.Zero);
        foreach (winInformation w in HashApps.Values) {
            if (w.pHandle < Shared.bpi) continue;
            sMkListViewItem? itm = null;
            foreach (sMkListViewItem i in lv.Items) {
                if (i.ID == w.wHandle) { itm = i; break; }
            }
            if (itm == null) {
                // Create the new listview Item
                itm = new() { ID = w.wHandle, ImageIndex = 0 };
                itm.Name = itm.ID.ToString();
                itm.Text = " " + w.pHandle;
                // Try to set the icon of the process
                if (w.pHandle > Shared.bpi && Shared.ilProcesses.Images.ContainsKey(itm.Name + "-")) {
                    itm.ImageKey = itm.Name;
                } else if (w.pHandle > Shared.bpi) {
                    try {
                        IntPtr IconPtr = IntPtr.Zero;
                        // Const GCL_HICONSM As Integer = -34
                        IconPtr = API.GetClassLong(w.wHandle, -34);
                        if (IconPtr != IntPtr.Zero) {
                            Shared.ilProcesses.Images.Add(itm.Name + "-", Icon.FromHandle(IconPtr));
                            API.DestroyIcon(IconPtr);
                        }
                    } catch { }
                    itm.ImageKey = Shared.ProcessIconImageKey(w.pHandle, "");
                } else {
                    itm.ImageIndex = 0;
                }
                if (Settings.Highlights.NewItems && !firstTime) itm.BackColor = Settings.Highlights.NewColor;
                lv.Items.Add(itm);
            } else {
                if (itm.BackColor == Settings.Highlights.NewColor) itm.BackColor = Color.Empty;
            }
            foreach (ColumnHeader c in lv.Columns) {
                if (c.Tag == null) continue;
                if (c.Tag.ToString() == "PID") continue;
                string Ident = c.Tag.ToString()!;
                string _subItmValue = "";
                switch (Ident) {
                    case "Task": _subItmValue = w.wTitle; break;
                    case "Handle": _subItmValue = Convert.ToString(w.wHandle); break;
                    case "Status": _subItmValue = "Running"; break;
                }
                if (!itm.SubItems.ContainsKey(Ident)) {
                    ListViewItem.ListViewSubItem subItm = new() { Name = Ident, Text = _subItmValue };
                    if (itm.SubItems.Count >= c.Index) itm.SubItems.Insert(c.Index, subItm);
                    _NeedReSort = _NeedReSort || c.Index == lv.SortColumn;
                } else if (!(itm.SubItems[Ident]!.Text.Equals(_subItmValue))) {
                    if (!itm.ValuesChanged) { itm.ValuesChanged = true; }
                    itm.SubItems[Ident]!.Text = _subItmValue;
                    _NeedReSort = _NeedReSort || c.Index == lv.SortColumn;
                }
            }
        }
        // Clear items not on the hashTable anymore...
        foreach (sMkListViewItem itm in lv.Items) {
            if (!HashApps.ContainsKey(itm.ID)) { lv.Items.Remove(itm); }
        }
        if (_NeedReSort) { _NeedReSort = false; lv.Sort(); }
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

    private struct winInformation {
        public int wHandle;
        public int pHandle;
        public string wTitle;
        public API.WindowStyles wStyle;
        public winInformation(int wHandle, int pHandle, string wTitle, API.WindowStyles wStyle) : this() {
            this.wHandle = wHandle;
            this.pHandle = pHandle;
            this.wTitle = wTitle;
            this.wStyle = wStyle;
        }
    }
    private bool EnumerateWindowsCallback(IntPtr hwnd, int lParam) {
        bool validWindow = false;
        // Get Window Title
        System.Text.StringBuilder cs = new(256);
        _ = API.GetWindowText(hwnd, cs, cs.Capacity);
        // Qualified Window?
        if (!validWindow && API.IsWindowZoomed(hwnd)) validWindow = true;
        if (!validWindow && API.IsWindowIconic(hwnd)) validWindow = true;
        if (!validWindow && API.IsWindowVisible(hwnd)) validWindow = true;
        // Check For Title
        if (validWindow && string.IsNullOrEmpty(cs.ToString().Trim())) validWindow = false;
        // Check For Parent
        if (validWindow) validWindow = (API.GetWindow(hwnd, 4) == IntPtr.Zero);
        // Exit Point
        if (!validWindow) return true;
        // Check For Style
        API.WindowInfo wInfo = new();
        API.GetWindowInfo(hwnd, ref wInfo);
        API.WindowStyles thisStyle = (API.WindowStyles)wInfo.dwStyle;

        validWindow = thisStyle.HasFlag(API.WindowStyles.WS_OVERLAPPEDWINDOW & API.WindowStyles.WS_POPUPWINDOW) && thisStyle.HasFlag(API.WindowStyles.WS_VISIBLE);
        // validWindow = Convert.ToBoolean((API.WindowStyles)wInfo.dwStyle & (API.WindowStyles.WS_OVERLAPPEDWINDOW & API.WindowStyles.WS_POPUPWINDOW));

        if (validWindow) {
            IntPtr _PID = IntPtr.Zero;
            _ = API.GetWindowThreadProcessId(hwnd, ref _PID);
            HashApps.Add(hwnd.ToInt32(), new winInformation(hwnd.ToInt32(), _PID.ToInt32(), cs.ToString(), (API.WindowStyles)wInfo.dwStyle));
            // Debug.WriteLine($"pHandle: {_PID} wHandle: {hwnd} Style: {wInfo.dwStyle} Style: {(API.WindowStyles)wInfo.dwStyle} ");
        }
        return true;
    }
    private static void WindowToForeground(IntPtr hwnd) {
        if (API.IsWindowIconic(hwnd)) {
            API.ShowWindow(hwnd, API.WindowShowCommand.SW_RESTORE);
            API.SetForegroundWindow(hwnd);
        } else {
            API.SetForegroundWindow(hwnd);
        }
    }

}