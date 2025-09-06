using sMkTaskManager.Controls;

namespace sMkTaskManager;

partial class frmMain {
    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }
    private void InitializeComponent() {
        components = new System.ComponentModel.Container();
        mnu = new MenuStrip();
        mnuFile = new ToolStripMenuItem();
        mnuOptions = new ToolStripMenuItem();
        mnuView = new ToolStripMenuItem();
        mnuHelp = new ToolStripMenuItem();
        ss = new StatusStrip();
        ssText = new ToolStripStatusLabel();
        ssProcesses = new ToolStripStatusLabel();
        ssServices = new ToolStripStatusLabel();
        ssCpuLoad = new ToolStripStatusLabel();
        ssBtnState = new ToolStripSplitButton();
        ssBusyTime = new ToolStripStatusLabel();
        tc = new sMkTabControl();
        tpGeneric = new TabPage();
        timmingStrip = new StatusStrip();
        niTray = new NotifyIcon(components);
        cmsTray = new ContextMenuStrip(components);
        ilMain = new ImageList(components);
        mnu.SuspendLayout();
        ss.SuspendLayout();
        tc.SuspendLayout();
        SuspendLayout();
        // 
        // mnu
        // 
        mnu.Items.AddRange(new ToolStripItem[] { mnuFile, mnuOptions, mnuView, mnuHelp });
        mnu.Location = new Point(0, 0);
        mnu.Name = "mnu";
        mnu.Size = new Size(584, 24);
        mnu.TabIndex = 0;
        mnu.Text = "menuStrip1";
        // 
        // mnuFile
        // 
        mnuFile.Name = "mnuFile";
        mnuFile.Size = new Size(37, 20);
        mnuFile.Text = "&File";
        // 
        // mnuOptions
        // 
        mnuOptions.Name = "mnuOptions";
        mnuOptions.Size = new Size(61, 20);
        mnuOptions.Text = "&Options";
        // 
        // mnuView
        // 
        mnuView.Name = "mnuView";
        mnuView.Size = new Size(44, 20);
        mnuView.Text = "&View";
        // 
        // mnuHelp
        // 
        mnuHelp.Alignment = ToolStripItemAlignment.Right;
        mnuHelp.Name = "mnuHelp";
        mnuHelp.Size = new Size(44, 20);
        mnuHelp.Text = "&Help";
        // 
        // ss
        // 
        ss.Items.AddRange(new ToolStripItem[] { ssText, ssProcesses, ssServices, ssCpuLoad, ssBtnState, ssBusyTime });
        ss.Location = new Point(0, 537);
        ss.Name = "ss";
        ss.Size = new Size(584, 24);
        ss.TabIndex = 1;
        ss.Text = "statusStrip1";
        // 
        // ssText
        // 
        ssText.BorderSides = ToolStripStatusLabelBorderSides.Right;
        ssText.BorderStyle = Border3DStyle.Etched;
        ssText.Name = "ssText";
        ssText.Size = new Size(154, 19);
        ssText.Spring = true;
        ssText.Text = "Windows Task Manager By Kakhnouski Yauheni";
        ssText.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // ssProcesses
        // 
        ssProcesses.AutoSize = false;
        ssProcesses.BorderSides = ToolStripStatusLabelBorderSides.Right;
        ssProcesses.BorderStyle = Border3DStyle.Etched;
        ssProcesses.Name = "ssProcesses";
        ssProcesses.Size = new Size(120, 20);
        ssProcesses.Text = "Processes: 0";
        ssProcesses.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // ssServices
        // 
        ssServices.AutoSize = false;
        ssServices.BorderSides = ToolStripStatusLabelBorderSides.Right;
        ssServices.BorderStyle = Border3DStyle.Etched;
        ssServices.Name = "ssServices";
        ssServices.Size = new Size(100, 20);
        ssServices.Text = "Services: 0";
        ssServices.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // ssCpuLoad
        // 
        ssCpuLoad.AutoSize = false;
        ssCpuLoad.BorderSides = ToolStripStatusLabelBorderSides.Right;
        ssCpuLoad.BorderStyle = Border3DStyle.Etched;
        ssCpuLoad.Name = "ssCpuLoad";
        ssCpuLoad.Size = new Size(120, 20);
        ssCpuLoad.Text = "CPU Load: 0%";
        ssCpuLoad.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // ssBtnState
        // 
        ssBtnState.AutoSize = false;
        ssBtnState.DoubleClickEnabled = true;
        ssBtnState.ImageTransparentColor = Color.Magenta;
        ssBtnState.Name = "ssBtnState";
        ssBtnState.Size = new Size(80, 22);
        ssBtnState.Text = "High";
        ssBtnState.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // ssBusyTime
        // 
        ssBusyTime.AutoSize = false;
        ssBusyTime.BorderSides = ToolStripStatusLabelBorderSides.Left;
        ssBusyTime.BorderStyle = Border3DStyle.Etched;
        ssBusyTime.Margin = new Padding(5, 3, 0, 2);
        ssBusyTime.Name = "ssBusyTime";
        ssBusyTime.Size = new Size(50, 19);
        ssBusyTime.Text = "20ms";
        // 
        // tc
        // 
        tc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tc.Controls.Add(tpGeneric);
        tc.ItemSize = new Size(70, 20);
        tc.Location = new Point(3, 27);
        tc.Margin = new Padding(0, 3, 0, 3);
        tc.Name = "tc";
        tc.Padding = new Point(5, 3);
        tc.SelectedIndex = 0;
        tc.Size = new Size(577, 508);
        tc.TabIndex = 1;
        // 
        // tpGeneric
        // 
        tpGeneric.BackColor = SystemColors.Control;
        tpGeneric.Location = new Point(4, 24);
        tpGeneric.Name = "tpGeneric";
        tpGeneric.Size = new Size(569, 480);
        tpGeneric.TabIndex = 0;
        tpGeneric.Text = "Generic";
        // 
        // timmingStrip
        // 
        timmingStrip.AllowMerge = false;
        timmingStrip.Dock = DockStyle.Top;
        timmingStrip.GripMargin = new Padding(0);
        timmingStrip.Location = new Point(0, 515);
        timmingStrip.Name = "timmingStrip";
        timmingStrip.Padding = new Padding(10, 0, 10, 0);
        timmingStrip.ShowItemToolTips = true;
        timmingStrip.Size = new Size(584, 22);
        timmingStrip.SizingGrip = false;
        timmingStrip.TabIndex = 2;
        timmingStrip.Text = "statusStrip1";
        timmingStrip.Visible = false;
        // 
        // niTray
        // 
        niTray.ContextMenuStrip = cmsTray;
        niTray.Text = "Windows Task Manager By Kakhnouski Yauheni";
        niTray.Visible = true;
        niTray.MouseClick += evTrayMouseClick;
        niTray.MouseDoubleClick += evTrayMouseDoubleClick;
        // 
        // cmsTray
        // 
        cmsTray.Name = "cmsTray";
        cmsTray.Size = new Size(61, 4);
        // 
        // ilMain
        // 
        ilMain.ColorDepth = ColorDepth.Depth32Bit;
        ilMain.ImageSize = new Size(16, 16);
        ilMain.TransparentColor = Color.Transparent;
        // 
        // frmMain
        // 
        ClientSize = new Size(584, 561);
        Controls.Add(timmingStrip);
        Controls.Add(tc);
        Controls.Add(ss);
        Controls.Add(mnu);
        Icon = Resources.Resources.frmMain;
        KeyPreview = true;
        MainMenuStrip = mnu;
        Margin = new Padding(6);
        MinimumSize = new Size(480, 400);
        Name = "frmMain";
        Text = "Windows Task Manager By Kakhnouski Yauheni";
        FormClosing += OnClosingEventHandler;
        FormClosed += OnClosedEventHandler;
        Load += OnLoad;
        SizeChanged += OnSizeChangedEventHandler;
        KeyDown += OnKeyDownEventHandler;
        mnu.ResumeLayout(false);
        mnu.PerformLayout();
        ss.ResumeLayout(false);
        ss.PerformLayout();
        tc.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    private MenuStrip mnu;
    private ToolStripMenuItem mnuFile;
    private ToolStripMenuItem mnuOptions;
    private ToolStripMenuItem mnuView;
    private ToolStripMenuItem mnuHelp;
    private StatusStrip ss;
    private ToolStripStatusLabel ssText;
    private ToolStripStatusLabel ssProcesses;
    private ToolStripStatusLabel ssServices;
    private ToolStripStatusLabel ssCpuLoad;
    private ToolStripSplitButton ssBtnState;
    private ToolStripStatusLabel ssBusyTime;
    private sMkTabControl tc;
    private TabPage tpGeneric;
    private StatusStrip timmingStrip;
    private NotifyIcon niTray;
    private ContextMenuStrip cmsTray;
    private ImageList ilMain;
}
