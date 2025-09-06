using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Timers;
using sMkTaskManager.Classes;
using sMkTaskManager.Forms;
namespace sMkTaskManager;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
public partial class frmMain : Form {

    private readonly List<Task> _MonitorTasks = new();
    private readonly System.Timers.Timer _StatusTimer = new() { Enabled = false, Interval = 10000 };
    private readonly System.Timers.Timer _MonitorTriggerTimer = new() { Enabled = false };
    private readonly System.Timers.Timer _TrayUpdateTimer = new() { Enabled = false, Interval = 1000 };
    private readonly Stopwatch _StopWatch1 = new(), _StopWatch2 = new();
    private readonly BackgroundWorker _InitWorker = new();
    private Size? _prevSize, _prevMinSize, _fullScreenSize;
    private Point? _prevLocation, _fullScreenLocation;

    public frmMain() {
        InitializeComponent();
        InitializeMainMenu();
        // Initialize all tabControls. Maybe this should be delayed.
        Tabs.Tab.Add("Sys", new tabSystem());
        Tabs.Tab.Add("Apps", new tabApplications());
        Tabs.Tab.Add("Procs", new tabProcesses());
        Tabs.Tab.Add("Servs", new tabServices());
        //Tabs.Tab.Add("Perfs", new tabPerformance());
        //Tabs.Tab.Add("GPUs", new tabGpu());
        //Tabs.Tab.Add("Net", new tabNetworking());
       // Tabs.Tab.Add("Conns", new tabConnections());
        //Tabs.Tab.Add("Ports", new tabPorts());
        //Tabs.Tab.Add("Devices", new tabDevices());
        Tabs.Tab.Add("Users", new tabUsers());
        // Flicker Free Controls by DoubleBuffer
        Extensions.CascadingDoubleBuffer(this);
        Shared.ilProcesses.Images.Add(Resources.Resources.Process_Empty);
        Shared.ilProcesses.Images.Add(Resources.Resources.Process_Info);
        // Extensions.UseImmersiveDarkMode(Handle, true);
        // Extensions.UseImmersiveRoundCorner(Handle, 3);
    }
    protected override void WndProc(ref Message m) {
        if (m.Msg == 0x112) { // WM_SYSCOMMAND
            if (m.WParam == 61536) { Shared.RealExit = false; }
            if (m.WParam == 1001) { mnuHelp_About.PerformClick(); }
        } else if (m.Msg == Shared.PrivateMsgID) { // Private Communication Channel? :)
            Debug.WriteLine($"Got PrivateMsg with wParm: {m.WParam}");
            // We use this to activate the window from external apps or a diff instance.
            if (m.WParam == 1) { Feature_ActivateMainWindow(); }
        }
        base.WndProc(ref m);
    }

    private void OnLoad(object sender, EventArgs e) {
        Extensions.StartMeasure(_StopWatch1);
        // Load Settings and then Fork
        Settings.LoadAll();
        OnLoadApplySettings();
        OnLoadCreateTabs();
        OnLoadAddHandlers();
        Settings_Apply();
        FullScreen = Settings.inFullScreen;
        // At this point, we can follow with Parallel Worker;
        _InitWorker.RunWorkerAsync();
        Shared.LoadComplete = true;
        Extensions.StopMeasure(_StopWatch1);
    }
    private void OnLoadApplySettings() {
        // Load Window Size & Position
        Settings.LoadMainWindow();
        if (Settings.RememberPositions) {
            Width = Math.Max(MinimumSize.Width, Settings.MainWindow.Size.Width);
            Height = Math.Max(MinimumSize.Height, Settings.MainWindow.Size.Height);
            if (Settings.MainWindow.Location.X != 0 || Settings.MainWindow.Location.Y != 0) {
                Location = Settings.MainWindow.Location;
            }
            if (Settings.MainWindow.Maximized) {
                WindowState = FormWindowState.Maximized;
            }
        } else { Width = 760; Height = 750; }
        // Properly Handle Start Minimized
        if (Settings.StartMinimized) {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = !Settings.ToTrayWhenMinimized;
            if (Settings.ToTrayWhenMinimized) Hide();
        }
        // Acurately set Update Timer
        switch (Settings.UpdateSpeed) {
            case <= 500: mnuView_SpeedHigh.PerformClick(); break;
            case > 2000: mnuView_SpeedLow.PerformClick(); break;
            default: mnuView_SpeedNormal.PerformClick(); break;
        }
    }
    private void OnLoadCreateTabs() {
        // Load Settings for all tabs.
        foreach (ITaskManagerTab? t in Tabs.Tab.Values) { t?.LoadSettings(); }
        tc.SuspendLayout();
        tc.TabPages.Clear();
        foreach (ITaskManagerTab? t in Tabs.Tab.Values) {
            TabPage tp = new() {
                Name = "tp" + t.Title,
                Text = t.Title,
                ToolTipText = t.Description
            };
            ((Control)t).Dock = DockStyle.Fill;
            ((Control)t).Location = new Point(0, 0);
            ((Control)t).TabIndex = 0;
            if (t.Icon != null) {
                tc.ImageList = ilMain;
                ilMain.Images.Add("tp" + t.Name, t.Icon);
                tp.ImageKey = "tp" + t.Name;
            }
            tp.Controls.Add((Control?)t);
            tc.TabPages.Add(tp);
        }

        // Load Last Tab
        if (Settings.RememberActiveTab) { if (Settings.ActiveTab < tc.TabCount) tc.SelectTab(Settings.ActiveTab); }
        tc.ResumeLayout();
    }
    private void OnLoadParallelInit(object? sender, DoWorkEventArgs e) {
        Stopwatch sw = new();
        Extensions.StartMeasure(sw);
        // We should use this to initialize something that is not really critical?
        if (Shared.PrivateMsgID == 0) Shared.PrivateMsgID = API.RegisterWindowMessage(Application.ExecutablePath.Replace("\\", "_"));
        Shared.ETW.sessionName += "-" + Shared.PrivateMsgID;
        Shared.ETW.Start();
        // if (Shared.AddPrivilege("SeDebugPrivilege")) Debug.WriteLine("SeDebugPrivilege Set");
        Shared.InitComplete = true;
        MonitorStart(true);
        Extensions.StopMeasure(sw);
    }
    private void OnLoadAddHandlers() {
        if (Tabs.Contains("Perfs")) {
            ((Control)Tabs.Tab["Perfs"]).MouseDoubleClick += evtabPerf_MouseDoubleClick;
            Tabs.Tab["Perfs"].RefreshComplete += evtabPerf_RefreshComplete;
        }
        _InitWorker.DoWork += OnLoadParallelInit;
        _MonitorTriggerTimer.Elapsed += MonitorTriggerExecutor;
        _TrayUpdateTimer.Elapsed += TrayUpdaterExecutor;
        _StatusTimer.Elapsed += OnStatusTimerEventHandler;
        ssBtnState.DropDownOpening += evStatusBarStateOpening;
        ssBtnState.DropDownClosed += evStatusBarStateClosed;
        ssBtnState.ButtonDoubleClick += evStatusBarStateDoubleClick;
    }

    private void OnStatusTimerEventHandler(object? sender, EventArgs e) {
        SetStatusText();
        _StatusTimer.Stop();
    }
    private void OnSizeChangedEventHandler(object sender, EventArgs e) {
        if (!Shared.LoadComplete) return;
        ToolStripMenuItem mnuItm = (ToolStripMenuItem)mnuOptions.DropDownItems["mnuOptions_HideMinimize"];

        if (WindowState == FormWindowState.Minimized && mnuItm.Checked) {
            WindowState = FormWindowState.Minimized;
            Hide();
        }
        if (WindowState != FormWindowState.Minimized) {
            Settings.MainWindow.Maximized = (WindowState == FormWindowState.Maximized);
        }
        if ((WindowState == FormWindowState.Normal | WindowState == FormWindowState.Maximized) && !Visible) {
            Visible = true;
        }
    }
    private void OnKeyDownEventHandler(object sender, KeyEventArgs e) {
        if (e.Handled) return;
        if (e.KeyCode == Keys.Escape && !(e.Alt || e.Control || e.Shift)) {
            e.SuppressKeyPress = true;
            e.Handled = true;
            Shared.RealExit = false;
            Close();
        } else if (e.KeyCode == Keys.F3 && !(e.Alt || e.Control || e.Shift)) {
            e.Handled = true;
            Feature_Preferences(1);
        } else if (e.KeyCode == Keys.F4 && !(e.Alt || e.Control || e.Shift)) {
            e.Handled = true;
            Feature_Preferences(2);
        }
    }
    private void OnClosingEventHandler(object sender, FormClosingEventArgs e) {
        if (e.CloseReason == CloseReason.UserClosing && !Shared.RealExit) {
            if (mnuOptions_MinimizeClose.Checked & !Shared.RealExit) {
                WindowState = FormWindowState.Minimized;
                e.Cancel = true;
            }
            if (Settings.ToTrayWhenClosed & !Shared.RealExit) {
                Visible = false;
                e.Cancel = true;
            }
        }
        if (!e.Cancel) {
            Extensions.StartMeasure(_StopWatch2);
            // If monitor is running we must stop it right now...
            if (MonitorRunning) MonitorToggle();
            if (_TrayUpdateTimer.Enabled) { _TrayUpdateTimer.Stop(); }
            if (Shared.ETW.Running) Shared.ETW.Stop();
            // Save Window Position & Tab...
            if (Settings.RememberPositions) {
                if (WindowState == FormWindowState.Normal) {
                    Settings.MainWindow.Size = Size;
                    Settings.MainWindow.Location = Location;
                    Settings.MainWindow.Maximized = WindowState == FormWindowState.Maximized;
                }
                Settings.SaveMainWindow();
            }
            Settings.ActiveTab = tc.SelectedIndex;
            // Save Settings and Columns Information...
            Settings.SaveAll();
            foreach (ITaskManagerTab? t in Tabs.Tab.Values) { t?.SaveSettings(); }
            // Hide Tray Icons
            if (niTray.Visible) niTray.Visible = false;
            Extensions.StopMeasure(_StopWatch2, "Close Time");
        }
    }
    private void OnClosedEventHandler(object sender, FormClosedEventArgs e) {
        niTray.Visible = false;
    }

    private void evStatusBarStateOpening(object? sender, EventArgs e) {
        mnuView_SpeedHigh.Checked = MonitorSpeed == mnuView_SpeedHigh.ImageIndex;
        mnuView_SpeedNormal.Checked = MonitorSpeed == mnuView_SpeedNormal.ImageIndex;
        mnuView_SpeedLow.Checked = MonitorSpeed == mnuView_SpeedLow.ImageIndex;
        mnuView_Pause.Checked = !MonitorRunning;
        while (mnuView.DropDownItems.Count > 0) ssBtnState.DropDownItems.Add(mnuView.DropDownItems[0]);
    }
    private void evStatusBarStateClosed(object? sender, EventArgs e) {
        while (ssBtnState.DropDownItems.Count > 0) mnuView.DropDownItems.Add(ssBtnState.DropDownItems[0]);
    }
    private void evStatusBarStateDoubleClick(object? sender, EventArgs e) {
        MonitorRunning = !MonitorRunning;
    }
    private void evTrayMouseClick(object sender, MouseEventArgs e) {
        if (Settings.DblClickToRestore) return;
        if (e.Button == MouseButtons.Left && e.Clicks == 1) Feature_ActivateMainWindow();
    }
    private void evTrayMouseDoubleClick(object sender, MouseEventArgs e) {
        if (e.Button == MouseButtons.Left && e.Clicks == 2) Feature_ActivateMainWindow();
    }
    private void evtabPerf_MouseDoubleClick(object? sender, MouseEventArgs e) {
        if (e.Button == MouseButtons.Left && e.Clicks > 1) { FullScreen = !FullScreen; }
    }
    private void evtabPerf_RefreshComplete(object? sender, EventArgs e) {
        if (sender == null) return;
        ssProcesses.Text = "Processes: " + ((tabPerformance)sender).System.ProcessCount.Value;
        ssServices.Text = "Services: " + ((tabPerformance)sender).System.ServicesCount.Value;
        Shared.CpuLoad = (int)((tabPerformance)sender).System.CpuUsage.Value;
        ssCpuLoad.Text = "CPU Load: " + Shared.CpuLoad + "%";
    }

    internal bool FullScreen {
        get { return Settings.inFullScreen; }
        set {
            if (value && FullScreen) return;
            if (!value && !FullScreen) return;
            if (value) { tc.SelectTab("tpPerformance"); }
            Settings.inFullScreen = value;
            SuspendLayout();
            mnu.Visible = !value;
            ss.Visible = !value;
            tc.Visible = !value;
            Control ctrl = (Control)Tabs.Tab["Perfs"];
            if (value) {
                Controls.Add(ctrl);
                if (WindowState == FormWindowState.Normal) {
                    _prevLocation = Location;
                    _prevMinSize = MinimumSize;
                    _prevSize = Size;
                }
                MinimumSize = new(100, 300);
                TopMost = true;
                if (WindowState == FormWindowState.Normal) {
                    _fullScreenSize ??= Size;
                    _fullScreenLocation ??= Location;
                    Size = (Size)_fullScreenSize;
                    Location = (Point)_fullScreenLocation;
                }
            } else {
                tc.TabPages["tpPerformance"]?.Controls.Add(ctrl);
                if (WindowState == FormWindowState.Normal) {
                    _fullScreenSize = Size;
                    _fullScreenLocation = Location;
                }
                TopMost = Settings.AlwaysOnTop;
                if (WindowState == FormWindowState.Normal) {
                    if (_prevSize != null) Size = (Size)_prevSize;
                    if (_prevMinSize != null) MinimumSize = (Size)_prevMinSize;
                    if (_prevLocation != null) Location = (Point)_prevLocation;
                }
            }
            ResumeLayout();
            OnSizeChangedEventHandler(this, new EventArgs());
        }
    }
    internal void Settings_Apply() {
        // General Settings 
        ((ToolStripMenuItem)mnuOptions.DropDownItems["mnuOptions_OnTop"]).Checked = Settings.AlwaysOnTop;
        ((ToolStripMenuItem)mnuOptions.DropDownItems["mnuOptions_HideMinimize"]).Checked = Settings.ToTrayWhenMinimized;
        ((ToolStripMenuItem)mnuOptions.DropDownItems["mnuOptions_MinimizeClose"]).Checked = Settings.MinimizeWhenClosing;
        TopMost = Settings.AlwaysOnTop;
        ssBusyTime.Visible = Settings.TimmingInStatus;
        ssServices.Visible = Settings.ServicesInStatus;
        // Cascade to any loaded tab
        foreach (ITaskManagerTab? t in Tabs.Tab.Values) { t?.ApplySettings(); }
        // Tray is always visible, all we do is change the icon and start a timer to update it if needed.
        _TrayUpdateTimer.Enabled = Settings.ShowCPUOnTray;
        niTray.Icon = Settings.ShowCPUOnTray ? default : Icon;
    }

    internal bool MonitorBusy;
    internal bool MonitorRunning {
        get { return _MonitorTriggerTimer.Enabled; }
        set { MonitorToggle(); }
    }
    internal double MonitorSpeed {
        get { return _MonitorTriggerTimer.Interval; }
        set {
            _MonitorTriggerTimer.Interval = value;
            if (MonitorRunning) MonitorUpdateBtnStatus();
        }
    }
    internal void MonitorStart(bool firstTime = false) {
        if (MonitorRunning) return;
        if (firstTime) _MonitorTriggerTimer.Interval = 50;
        // MonitorRefreshAsync(firstTime);
        _MonitorTriggerTimer.Start();
        SetStatusText();
        MonitorUpdateBtnStatus();
    }
    internal void MonitorStop() {
        if (!MonitorRunning) return;
        _MonitorTriggerTimer.Stop();
        SetStatusText();
        MonitorUpdateBtnStatus();
    }
    internal void MonitorToggle() {
        if (_MonitorTriggerTimer.Enabled) { MonitorStop(); } else { MonitorStart(); }
    }
    internal void MonitorRefresh(bool firstTime = false) {
        if (MonitorBusy) return;
        _StopWatch1.Restart();
        MonitorBusy = true;
        foreach (ITaskManagerTab? t in Tabs.Tab.Values) { t?.Refresher(firstTime); }
        MonitorBusy = false;
        _StopWatch1.Stop();
        SetStatusText(_StopWatch1.ElapsedMilliseconds + "ms.", ssBusyTime);
        TimmingDisplay();
    }
    internal async void MonitorRefreshAsync(bool firstTime = false) {
        if (MonitorSpeed <= 200) { MonitorSpeed = Settings.UpdateSpeed; }
        if (MonitorBusy) return;
        _StopWatch1.Restart();
        MonitorBusy = true;
        _MonitorTasks.Clear();
        // _MonitorTasks.Add(Task.Run(() => DelayTask()));
        foreach (ITaskManagerTab? t in Tabs.Tab.Values) {
            _MonitorTasks.Add(Task.Run(() => t?.Refresher(firstTime)));
        }
        _MonitorTasks.Add(Task.Run(() => Shared.ShrinkMainProcess()));
        await Task.WhenAll(_MonitorTasks);
        MonitorBusy = false;
        _StopWatch1.Stop();
        SetStatusText(_StopWatch1.ElapsedMilliseconds + "ms.", ssBusyTime);
        TimmingDisplay();
    }
    private void MonitorTriggerExecutor(object? sender, ElapsedEventArgs e) {
        if (MonitorRunning) {
            MonitorRefreshAsync();
        }
    }
    private void MonitorUpdateBtnStatus() {
        if (ss.InvokeRequired) { ss.Invoke(MonitorUpdateBtnStatus); return; }
        if (MonitorRunning) {
            if (_MonitorTriggerTimer.Interval is <= 500) ssBtnState.Text = "High";
            if (_MonitorTriggerTimer.Interval is > 500 and <= 2000) ssBtnState.Text = "Normal";
            if (_MonitorTriggerTimer.Interval is > 2000) ssBtnState.Text = "Low";
            ssBtnState.Image = Resources.Resources.State_Play;
        } else {
            ssBtnState.Text = "Paused";
            ssBtnState.Image = Resources.Resources.State_Stop;
        }
    }
    private void TrayUpdaterExecutor(object? sender, ElapsedEventArgs e) {
        if (!Settings.ShowCPUOnTray) { _TrayUpdateTimer.Stop(); niTray.Icon = Icon; return; }
        // Draw a graph with the value of Shared.CpuLoad
        var bitmap = new Bitmap(16, 16);
        Graphics g = Graphics.FromImage(bitmap);
        Rectangle baseRectangle = new(0, 0, 16, 16);
        // Draw Black background
        using (SolidBrush thisBrush = new(Color.Black)) {
            g.FillRectangle(thisBrush, baseRectangle);
        }
        // Draw Used lines with green
        using (Pen thisPen = new(Color.Lime, 1)) {
            int lines = (int)Math.Round(16d * Shared.CpuLoad / 100d, 0);
            for (int i = 1; i <= lines; i++) {
                g.DrawLine(thisPen, 1, (16 - i), 14, (16 - i));
            }
        }
        // Draw a middle black line as spacer.
        using (Pen thisPen = new(Color.Black, 2)) {
            g.DrawLine(thisPen, 8, 0, 8, 16);
        }
        // Set the resulting bitmap as the icon.
        niTray.Icon = Icon.FromHandle(bitmap.GetHicon());
    }

    private bool TimmingVisible {
        get { return timmingStrip.Visible; }
        set {
            if (timmingStrip.Visible == value) return;
            if (timmingStrip.Items.Count == 0 && value) TimmingInitStrip();
            timmingStrip.Visible = value;
            tc.Height += value ? -20 : +20;
            if (timmingStrip.Dock == DockStyle.Top) tc.Top += value ? +20 : -20; ;
        }
    }
    private void TimmingInitStrip() {
        timmingStrip.Items.Clear();
        int i = 0;
        foreach (ITaskManagerTab? t in Tabs.Tab.Values) {
            if (string.IsNullOrEmpty(t.TimmingKey)) continue;
            if (i >= Shared.TimmingsColors.Count) { i = 0; }
            ToolStripStatusLabel lbl = new() {
                BackColor = Shared.TimmingsColors[i],
                BorderSides = ToolStripStatusLabelBorderSides.All,
                BorderStyle = Border3DStyle.Flat,
                AutoToolTip = false,
                Spring = true,
                Visible = true,
                Name = t.TimmingKey,
                AccessibleName = t.TimmingKey + ": "
            };
            lbl.Text = lbl.AccessibleName + string.Format("{0:N0}", t.TimmingValue) + "ms.";
            timmingStrip.Items.Add(lbl);
            i++;
        }
    }
    private void TimmingDisplay() {
        if (!TimmingVisible) return;
        if (timmingStrip.InvokeRequired) {
            // Debug.WriteLine("Timming Requires Invoke");
            // timmingStrip.BeginInvoke(TimmingDisplay); return;
        }
        foreach (ITaskManagerTab? t in Tabs.Tab.Values) {
            if (!timmingStrip.Items.ContainsKey(t.TimmingKey)) continue;
            var itm = timmingStrip.Items[t.TimmingKey];
            itm.Text = itm.AccessibleName + string.Format("{0:N0}", t.TimmingValue) + "ms.";
        }
    }

    public void SetStatusText(string value = "", ToolStripLabel? obj = null) {
        obj ??= ssText;
        if (ss.InvokeRequired) { ss.Invoke(SetStatusText, new object[] { value, obj }); return; }
        if (value == "") { value = MonitorRunning ? "Running ..." : "Paused ..."; }
        if (value != obj.Text) {
            obj.Text = value;
            if (obj == ssText) { _StatusTimer.Stop(); _StatusTimer.Start(); }
        }
    }
    public string GetStatusText(ToolStripLabel? obj) {
        obj ??= ssText;
        return obj.Text;
    }
    public void GoToProcess(string PID) {
        tabProcesses? tabProcs = (tabProcesses?)Tabs.GetTab("Procs");
        if (tabProcs == null) { return; }
        tabProcs.Refresher(true);
        if (tabProcs.ListView.Items.ContainsKey(PID)) {
            tabProcs.ListView.SelectedItems.Clear();
            tabProcs.ListView.Items[PID].Selected = true;
            tabProcs.ListView.Items[PID].Focused = true;
            tabProcs.ListView.Items[PID].EnsureVisible();
            tc.SelectTab((TabPage)tabProcs.Parent!);
        } else {
            SetStatusText($"Sorry, PID {PID} not in Process List");
        }
    }
    public void GoToService(string serviceName) {
        tabServices? tabServs = (tabServices?)Tabs.GetTab("Servs");
        if (tabServs == null) { return; }
        tabServs.Refresher(true);
        if (tabServs.ListView.Items.ContainsKey(serviceName)) {
            tabServs.ListView.SelectedItems.Clear();
            tabServs.ListView.Items[serviceName].Selected = true;
            tabServs.ListView.Items[serviceName].Focused = true;
            tabServs.ListView.Items[serviceName].EnsureVisible();
            tc.SelectTab((TabPage)tabServs.Parent!);
        } else {
            SetStatusText($"Sorry, Service {serviceName} not in Service List");
        }
    }

}
