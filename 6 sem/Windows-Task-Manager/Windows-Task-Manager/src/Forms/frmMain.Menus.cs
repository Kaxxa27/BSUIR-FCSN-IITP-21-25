using sMkTaskManager.Classes;
namespace sMkTaskManager;

partial class frmMain {
    private ToolStripMenuItem mnuMonitor, mnuComputer;
    private ToolStripMenuItem mnuMonitor_ScreenSaver, mnuMonitor_PowerOff;
    private ToolStripMenuItem mnuFile_NewTask, mnuFile_NewTaskAsUser, mnuFile_SystemProperties, mnuFile_Exit;
    private ToolStripMenuItem mnuComputer_Lock, mnuComputer_Logoff, mnuComputer_Sleep, mnuComputer_Hibernate;
    private ToolStripMenuItem mnuComputer_Restart, mnuComputer_Shutdown, mnuComputer_ForceShutdown;
    private ToolStripMenuItem mnuOptions_Preferences, mnuOptions_OnTop, mnuOptions_Highlight;
    private ToolStripMenuItem mnuOptions_HideMinimize, mnuOptions_MinimizeClose, mnuOptions_Default;
    private ToolStripMenuItem mnuOptions_SelectColumns, mnuOptions_SelectSummary;
    private ToolStripMenuItem mnuView_Refresh, mnuView_SpeedHigh, mnuView_SpeedNormal, mnuView_SpeedLow, mnuView_Pause;
    private ToolStripMenuItem mnuHelp_About, mnuHelp_Timmings;
    private ToolStripMenuItem mnuTray_Open, mnuTray_NewTask, mnuTray_NewTaskAsUser, mnuTray_Monitor, mnuTray_Computer, mnuTray_Exit;

    private void InitializeMainMenu() {
        mnu.SuspendLayout();
        InitializeFileMenu();
        mnuFile.DropDownOpening += mnu_DropDownOpening;
        mnuFile.DropDownItemClicked += mnu_DropDownItemClicked;
        mnuMonitor.DropDownItemClicked += mnu_DropDownItemClicked;
        mnuComputer.DropDownItemClicked += mnuComputer_DropDownItemClicked;
        InitializeOptionsMenu();
        mnuOptions.DropDownOpening += mnu_DropDownOpening;
        mnuOptions.DropDownItemClicked += mnu_DropDownItemClicked;
        InitializeViewMenu();
        mnuView.DropDownOpening += mnu_DropDownOpening;
        InitializeHelpMenu();
        mnuHelp.DropDownOpening += mnu_DropDownOpening;
        mnuHelp.DropDownItemClicked += mnu_DropDownItemClicked;
        mnu.ResumeLayout(true);
        // Aditional specific Event Handlers that I want for specific items.
        mnuView_Refresh.Click += mnuView_ItemClicked;
        mnuView_SpeedHigh.Click += mnuView_ItemClicked;
        mnuView_SpeedNormal.Click += mnuView_ItemClicked;
        mnuView_SpeedLow.Click += mnuView_ItemClicked;
        mnuView_Pause.Click += mnuView_ItemClicked;
        // Tray Menu
        InitializeTrayMenu();
        cmsTray.ItemClicked += mnu_DropDownItemClicked;
        mnuTray_Monitor.DropDownItemClicked += mnu_DropDownItemClicked;
        mnuTray_Computer.DropDownItemClicked += mnuComputer_DropDownItemClicked;
    }
    private void InitializeFileMenu() {
        // mnuMonitor
        mnuMonitor = new("&Monitor") { Name = "mnuMonitor" };
        mnuMonitor_ScreenSaver = new("&Screensaver...") { Name = "mnuMonitor_ScreenSaver" };
        mnuMonitor_PowerOff = new("Power &off...") { Name = "mnuMonitor_PowerOff" };
        mnuMonitor.DropDownItems.AddRange(new[] { mnuMonitor_ScreenSaver, mnuMonitor_PowerOff });
        // mnuComputer
        mnuComputer = new("&Computer") { Name = "mnuComputer" };
        mnuComputer_Lock = new("Lock") { Name = "mnuComputer_Lock" };
        mnuComputer_Logoff = new("Log off") { Name = "mnuComputer_Logoff" };
        mnuComputer_Sleep = new("Sleep") { Name = "mnuComputer_Sleep" };
        mnuComputer_Hibernate = new("Hibernate") { Name = "mnuComputer_Hibernate" };
        mnuComputer_Restart = new("Restart") { Name = "mnuComputer_Restart" };
        mnuComputer_Shutdown = new("Shutdown") { Name = "mnuComputer_Shutdown" };
        mnuComputer_ForceShutdown = new("Force Shutdown") { Name = "mnuComputer_ForceShutdown" };
        mnuComputer.DropDownItems.AddRange(new[] { mnuComputer_Lock, mnuComputer_Logoff });
        mnuComputer.DropDownItems.AddSeparator();
        mnuComputer.DropDownItems.AddRange(new[] { mnuComputer_Sleep, mnuComputer_Hibernate });
        mnuComputer.DropDownItems.AddSeparator();
        mnuComputer.DropDownItems.AddRange(new[] { mnuComputer_Restart, mnuComputer_Shutdown, mnuComputer_ForceShutdown });
        // mnuFile
        mnuFile_NewTask = new("&New Task (Run...)") { Name = "mnuFile_NewTask" };
        mnuFile_NewTaskAsUser = new("New Task (Run As...)") { Name = "mnuFile_NewTaskAsUser" };
        mnuFile_SystemProperties = new("System P&roperties") { Name = "mnuFile_SystemProperties", ShowShortcutKeys = false, ShortcutKeys = Keys.F12 };
        mnuFile_Exit = new("E&xit") { Name = "mnuFile_Exit" };
        mnuFile.DropDownItems.AddRange(new[] { mnuFile_NewTask, mnuFile_NewTaskAsUser });
        mnuFile.DropDownItems.AddSeparator();
        mnuFile.DropDownItems.AddRange(new[] { mnuMonitor, mnuComputer });
        mnuFile.DropDownItems.Add(mnuFile_SystemProperties);
        mnuFile.DropDownItems.AddSeparator();
        mnuFile.DropDownItems.Add(mnuFile_Exit);
    }
    private void InitializeOptionsMenu() {
        mnuOptions_Preferences = new("&Preferences...") { Name = "mnuOptions_Preferences", ShortcutKeys = Keys.F2 };
        mnuOptions_OnTop = new("&Always On Top") { Name = "mnuOptions_OnTop" };
        mnuOptions_Highlight = new("H&ighlight Changes") { Name = "mnuOptions_Highlight" };
        mnuOptions_HideMinimize = new("&Hide When Minimized") { Name = "mnuOptions_HideMinimize" };
        mnuOptions_MinimizeClose = new("&Minimize When Closing") { Name = "mnuOptions_MinimizeClose" };
        mnuOptions_Default = new("&Default Task Manager") { Name = "mnuOptions_Default" };
        mnuOptions_SelectColumns = new("&Select Columns...") { Name = "mnuOptions_SelectColumns" };
        mnuOptions_SelectSummary = new("Select S&ummary View...") { Name = "mnuOptions_SelectSummary" };
        mnuOptions.DropDownItems.Add(mnuOptions_Preferences);
        mnuOptions.DropDownItems.AddSeparator("mnuOptions_Separator1");
        mnuOptions.DropDownItems.AddRange(new[] { mnuOptions_OnTop, mnuOptions_Highlight, mnuOptions_HideMinimize, mnuOptions_MinimizeClose });
        mnuOptions.DropDownItems.AddSeparator("mnuOptions_Separator2");
        mnuOptions.DropDownItems.Add(mnuOptions_Default);
        mnuOptions.DropDownItems.AddSeparator("mnuOptions_Separator3");
        mnuOptions.DropDownItems.AddRange(new[] { mnuOptions_SelectColumns, mnuOptions_SelectSummary });
    }
    private void InitializeViewMenu() {
        mnuView_Refresh = new("&Refresh Now") { Name = "mnuView_Refresh", ShortcutKeys = Keys.F5 };
        mnuView_SpeedHigh = new("Update Speed: &High") { Name = "mnuView_SpeedHigh", ImageIndex = 500 };
        mnuView_SpeedNormal = new("Update Speed: &Normal") { Name = "mnuView_SpeedNormal", ImageIndex = 1000 };
        mnuView_SpeedLow = new("Update Speed: &Low") { Name = "mnuView_SpeedLow", ImageIndex = 3000 };
        mnuView_Pause = new("&Pause Data") { Name = "mnuView_Pause", ShortcutKeys = Keys.F6 };
        mnuView.DropDownItems.Add(mnuView_Refresh);
        mnuView.DropDownItems.AddSeparator();
        mnuView.DropDownItems.AddRange(new[] { mnuView_SpeedHigh, mnuView_SpeedNormal, mnuView_SpeedLow });
        mnuView.DropDownItems.AddSeparator();
        mnuView.DropDownItems.Add(mnuView_Pause);
    }
    private void InitializeHelpMenu() {
        mnuHelp_About = new("&About") { Name = "mnuHelp_About", ShortcutKeys = Keys.F1 };
        mnuHelp_Timmings = new("&Timmings") { Name = "mnuHelp_Timmings", ShortcutKeys = Keys.F9 };
        mnuHelp.DropDownItems.Add(mnuHelp_About);
        mnuHelp.DropDownItems.Add(mnuHelp_Timmings);
    }
    private void InitializeTrayMenu() {
        mnuTray_Open = new("&Open") { Name = "mnuTray_Open" };
        mnuTray_NewTask = new("&New Task (Run...)") { Name = "mnuTray_NewTask" };
        mnuTray_NewTaskAsUser = new("New Task (Run As...)") { Name = "mnuTray_NewTaskAsUser" };
        mnuTray_Monitor = new("&Monitor") { Name = "mnuTray_Monitor" };
        mnuTray_Computer = new("&Computer") { Name = "mnuTray_Computer" };
        mnuTray_Exit = new("E&xit") { Name = "mnuTray_Exit" };
        cmsTray.Items.AddRange(new[] { mnuTray_Open });
        cmsTray.Items.AddSeparator();
        cmsTray.Items.AddRange(new[] { mnuTray_NewTask, mnuTray_NewTaskAsUser });
        cmsTray.Items.AddSeparator();
        cmsTray.Items.AddRange(new[] { mnuTray_Monitor, mnuTray_Computer });
        cmsTray.Items.AddSeparator();
        cmsTray.Items.Add(mnuTray_Exit);
        mnuTray_Open.SwitchToBold();
        mnuTray_Monitor.DropDown = mnuMonitor.DropDown;
        mnuTray_Computer.DropDown = mnuComputer.DropDown;
        niTray.Icon = Icon;
    }

    private void mnu_DropDownOpening(object? sender, EventArgs e) {
        var mnu = (ToolStripMenuItem)sender!;
        if (mnu == mnuHelp) {
            mnuHelp_Timmings.Checked = TimmingVisible;
        } else if (mnu == mnuOptions) {
            mnuOptions_SelectColumns.Enabled = Tabs.ActiveTab != null && Tabs.ActiveTab.CanSelectColumns;
            mnuOptions_SelectColumns.Visible = mnuOptions_SelectColumns.Enabled;
            mnuOptions_SelectSummary.Enabled = false;
            mnuOptions_SelectSummary.Visible = mnuOptions_SelectSummary.Enabled;
            mnuOptions.DropDownItems["mnuOptions_Separator3"].Visible = mnuOptions_SelectColumns.Enabled | mnuOptions_SelectSummary.Enabled;
            mnuOptions_OnTop.Checked = Settings.AlwaysOnTop;
            mnuOptions_Highlight.Checked = Settings.Highlights.ChangingItems;
            mnuOptions_HideMinimize.Checked = Settings.ToTrayWhenMinimized;
            mnuOptions_Default.Checked = Feature_CheckDefaultTaskManager();
            mnuOptions_MinimizeClose.Checked = Settings.MinimizeWhenClosing;
        } else if (mnu == mnuView) {
            while (ssBtnState.DropDownItems.Count > 0) mnuView.DropDownItems.Add(ssBtnState.DropDownItems[0]);
            mnuView_SpeedHigh.Checked = MonitorSpeed == mnuView_SpeedHigh.ImageIndex;
            mnuView_SpeedNormal.Checked = MonitorSpeed == mnuView_SpeedNormal.ImageIndex;
            mnuView_SpeedLow.Checked = MonitorSpeed == mnuView_SpeedLow.ImageIndex;
            mnuView_Pause.Checked = !MonitorRunning;
        }
    }
    private void mnu_DropDownItemClicked(object? sender, ToolStripItemClickedEventArgs e) {
        if (e.ClickedItem == null) return;
        switch (e.ClickedItem.Name) {
            case nameof(mnuTray_Open): BeginInvoke(Feature_ActivateMainWindow); return;
            case nameof(mnuFile_NewTask) or nameof(mnuTray_NewTask): BeginInvoke(Feature_NewTask); return;
            case nameof(mnuFile_NewTaskAsUser) or nameof(mnuTray_NewTaskAsUser): BeginInvoke(Feature_NewTaskAsUser); return;
            case nameof(mnuFile_SystemProperties): BeginInvoke(Feature_SystemProperties); return;
            case nameof(mnuFile_Exit) or nameof(mnuTray_Exit): BeginInvoke(Feature_RealExit); return;
            case nameof(mnuMonitor_ScreenSaver): BeginInvoke(Feature_MonitorScreenSaver); return;
            case nameof(mnuMonitor_PowerOff): BeginInvoke(Feature_MonitorPowerOff); return;
            case nameof(mnuOptions_Preferences): Feature_Preferences(); break;
            case nameof(mnuOptions_OnTop): Feature_ToggleAlwaysOnTop(); break;
            case nameof(mnuOptions_Highlight): Feature_ToggleHighlightChanges(); break;
            case nameof(mnuOptions_HideMinimize): Feature_ToggleHideOnMinimize(); break;
            case nameof(mnuOptions_MinimizeClose): Feature_ToggleMinimizeOnClose(); break;
            case nameof(mnuOptions_Default): Feature_DefaultTaskManager(!mnuOptions_Default.Checked); break;
            case nameof(mnuOptions_SelectColumns): Feature_SelectColumns(); break;
            case nameof(mnuHelp_About): Feature_AboutDialog(); break;
            case nameof(mnuHelp_Timmings): TimmingVisible = !TimmingVisible; break;
            default: break;
        }
    }
    private void mnuComputer_DropDownItemClicked(object? sender, ToolStripItemClickedEventArgs e) {
        if (e.ClickedItem == null) return;
        BeginInvoke(Feature_Computer, e.ClickedItem.Text);
    }
    private void mnuView_ItemClicked(object? sender, EventArgs e) {
        if (sender == null) return;
        switch (((ToolStripMenuItem)sender).Name) {
            case nameof(mnuView_Refresh): MonitorRefresh(); return;
            case nameof(mnuView_Pause): MonitorRunning = !MonitorRunning; return;
        }
        mnuView_SpeedHigh.Checked = false; mnuView_SpeedNormal.Checked = false; mnuView_SpeedLow.Checked = false;
        ((ToolStripMenuItem)sender).Checked = true;
        int newSpeed = ((ToolStripMenuItem)sender).ImageIndex;
        MonitorSpeed = newSpeed;
        Settings.UpdateSpeed = newSpeed;
        SetStatusText($"Update speed set to: {((ToolStripMenuItem)sender).Name.Replace("mnuView_Speed", "")} ({newSpeed}ms) ...");
    }

}