namespace sMkTaskManager.Forms;

partial class frmProcess_Details {

    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }
    private void InitializeComponent() {
        components = new System.ComponentModel.Container();
        tc = new TabControl();
        tpGeneral = new TabPage();
        g_TableLayout1 = new TableLayoutPanel();
        g_gpCPU = new GroupBox();
        g_lblCPURunning = new Label();
        g_lblCPUCreation = new Label();
        g_lblCPUTimeTotal = new Label();
        g_lblCPUTimeKernel = new Label();
        g_lblCPUTimeUser = new Label();
        g_lblCPUPriority = new Label();
        lblDivider2 = new Label();
        lblDivider1 = new Label();
        g_Label13 = new Label();
        g_Label12 = new Label();
        g_Label11 = new Label();
        g_Label10 = new Label();
        g_Label09 = new Label();
        g_Label08 = new Label();
        g_gpVmMemory = new GroupBox();
        g_lblPageFaultsDelta = new Label();
        g_lblPageFaults = new Label();
        g_lblPagedMemory = new Label();
        g_lblVirtualMemory = new Label();
        g_Label27 = new Label();
        g_Label26 = new Label();
        g_Label25 = new Label();
        g_Label24 = new Label();
        g_gpPhMemory = new GroupBox();
        g_lblMemWSpeak = new Label();
        g_Label17 = new Label();
        g_lblMemWSshare = new Label();
        g_lblMemWSpriv = new Label();
        g_lblMemWS = new Label();
        g_Label16 = new Label();
        g_Label15 = new Label();
        g_Label14 = new Label();
        g_gpResources = new GroupBox();
        g_lblResUser = new Label();
        g_lblResGDI = new Label();
        g_lblResHandlesPeak = new Label();
        g_lblResHandles = new Label();
        g_lblResThreadsPeak = new Label();
        g_lblResThreads = new Label();
        lblDivider4 = new Label();
        lblDivider3 = new Label();
        g_Label23 = new Label();
        g_Label22 = new Label();
        g_Label21 = new Label();
        g_Label20 = new Label();
        g_Label18 = new Label();
        g_Label19 = new Label();
        g_gpDetails = new GroupBox();
        g_pbIcon = new PictureBox();
        g_txtDescription = new TextBox();
        g_txtCompany = new TextBox();
        g_txtVersion = new TextBox();
        g_txtType = new TextBox();
        g_txtPath = new TextBox();
        g_txtName = new TextBox();
        g_Label07 = new Label();
        g_Label06 = new Label();
        g_Label05 = new Label();
        g_Label04 = new Label();
        g_Label03 = new Label();
        g_txtPID = new TextBox();
        g_Label02 = new Label();
        g_Label01 = new Label();
        tpPerformance = new TabPage();
        p_TableLayout1 = new TableLayoutPanel();
        p_ChartGDI = new Controls.sMkPerfChart();
        p_panel2 = new Panel();
        p_lblWorkingSet = new Label();
        p_Label02 = new Label();
        p_panel5 = new Panel();
        p_lblPageFaults = new Label();
        p_Label05 = new Label();
        p_panel3 = new Panel();
        p_lblPrivateBytes = new Label();
        p_Label03 = new Label();
        p_panel7 = new Panel();
        p_lblUser = new Label();
        p_Label07 = new Label();
        p_panel1 = new Panel();
        p_lblCpuUsage = new Label();
        p_Label01 = new Label();
        p_panel6 = new Panel();
        p_lblGDI = new Label();
        p_Label06 = new Label();
        p_panel4 = new Panel();
        p_lblVirtualMemory = new Label();
        p_Label04 = new Label();
        p_ChartCPU = new Controls.sMkPerfChart();
        p_ChartWS = new Controls.sMkPerfChart();
        p_ChartPB = new Controls.sMkPerfChart();
        p_ChartVM = new Controls.sMkPerfChart();
        p_ChartPF = new Controls.sMkPerfChart();
        p_ChartUser = new Controls.sMkPerfChart();
        tpIO = new TabPage();
        i_TableLayout1 = new TableLayoutPanel();
        i_ChartOthers = new Controls.sMkPerfChart();
        i_ChartWrites = new Controls.sMkPerfChart();
        i_ChartReads = new Controls.sMkPerfChart();
        i_Label06 = new Label();
        i_Label05 = new Label();
        i_Label04 = new Label();
        i_Label03 = new Label();
        i_Label02 = new Label();
        i_Label01 = new Label();
        i_PerfReads = new Controls.sMkPerfMeter();
        i_PerfWrites = new Controls.sMkPerfMeter();
        i_PerfOthers = new Controls.sMkPerfMeter();
        i_TableLayout2 = new TableLayoutPanel();
        i_GroupBox3 = new GroupBox();
        i_lblOtherBytesDelta = new Label();
        i_lblOtherBytes = new Label();
        i_lblOtherCountDelta = new Label();
        i_lblOtherCount = new Label();
        i_Label34 = new Label();
        i_Label33 = new Label();
        i_Label32 = new Label();
        i_Label31 = new Label();
        i_GroupBox2 = new GroupBox();
        i_lblWriteBytesDelta = new Label();
        i_lblWriteBytes = new Label();
        i_lblWriteCountDelta = new Label();
        i_lblWriteCount = new Label();
        i_Label24 = new Label();
        i_Label23 = new Label();
        i_Label22 = new Label();
        i_Label21 = new Label();
        i_GroupBox1 = new GroupBox();
        i_lblReadBytesDelta = new Label();
        i_lblReadBytes = new Label();
        i_lblReadCountDelta = new Label();
        i_lblReadCount = new Label();
        i_Label14 = new Label();
        i_Label13 = new Label();
        i_Label12 = new Label();
        i_Label11 = new Label();
        tpDiskNet = new TabPage();
        d_TableLayout1 = new TableLayoutPanel();
        d_ChartNet = new Controls.sMkPerfChart();
        d_ChartDisk = new Controls.sMkPerfChart();
        d_Label04 = new Label();
        d_Label03 = new Label();
        d_PerfDisk = new Controls.sMkPerfMeter();
        d_PerfNet = new Controls.sMkPerfMeter();
        d_Label02 = new Label();
        d_Label01 = new Label();
        d_TableLayout2 = new TableLayoutPanel();
        d_GroupBox2 = new GroupBox();
        d_lblNetRcvdRate = new Label();
        d_lblNetRcvdDelta = new Label();
        d_lblNetRcvd = new Label();
        d_Label26 = new Label();
        d_Label25 = new Label();
        d_Label24 = new Label();
        d_lblNetSendRate = new Label();
        d_lblNetSendDelta = new Label();
        d_lblNetSend = new Label();
        d_lblDivider2 = new Label();
        d_Label23 = new Label();
        d_Label22 = new Label();
        d_Label21 = new Label();
        d_GroupBox1 = new GroupBox();
        d_lblDiskWriteRate = new Label();
        d_lblDiskWriteDelta = new Label();
        d_lblDiskWrite = new Label();
        d_Label15 = new Label();
        d_Label14 = new Label();
        d_Label13 = new Label();
        d_lblDiskReadRate = new Label();
        d_lblDiskReadDelta = new Label();
        d_lblDiskRead = new Label();
        d_lblDivider1 = new Label();
        d_Label12 = new Label();
        d_Label11 = new Label();
        d_Label10 = new Label();
        tpModules = new TabPage();
        lvModules = new ListView();
        colModules_Name = new ColumnHeader();
        colModules_Version = new ColumnHeader();
        colModules_Type = new ColumnHeader();
        colModules_Address = new ColumnHeader();
        colModules_Memory = new ColumnHeader();
        colModules_Description = new ColumnHeader();
        colModules_Path = new ColumnHeader();
        colModules_Company = new ColumnHeader();
        colModules_Language = new ColumnHeader();
        tpThreads = new TabPage();
        lvThreads = new ListView();
        colThreads_ID = new ColumnHeader();
        colThreads_Priority = new ColumnHeader();
        colThreads_State = new ColumnHeader();
        colThreads_Reason = new ColumnHeader();
        colThreads_StartTime = new ColumnHeader();
        colThreads_RunTime = new ColumnHeader();
        tpLocked = new TabPage();
        lvLockedFiles = new ListView();
        colLockedFiles_Filename = new ColumnHeader();
        colLockedFiles_Path = new ColumnHeader();
        btnOK = new Button();
        btnCancel = new Button();
        lblSpeed = new Label();
        tbSpeed = new TrackBar();
        lblSpeedValue = new Label();
        tc.SuspendLayout();
        tpGeneral.SuspendLayout();
        g_TableLayout1.SuspendLayout();
        g_gpCPU.SuspendLayout();
        g_gpVmMemory.SuspendLayout();
        g_gpPhMemory.SuspendLayout();
        g_gpResources.SuspendLayout();
        g_gpDetails.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)g_pbIcon).BeginInit();
        tpPerformance.SuspendLayout();
        p_TableLayout1.SuspendLayout();
        p_panel2.SuspendLayout();
        p_panel5.SuspendLayout();
        p_panel3.SuspendLayout();
        p_panel7.SuspendLayout();
        p_panel1.SuspendLayout();
        p_panel6.SuspendLayout();
        p_panel4.SuspendLayout();
        tpIO.SuspendLayout();
        i_TableLayout1.SuspendLayout();
        i_TableLayout2.SuspendLayout();
        i_GroupBox3.SuspendLayout();
        i_GroupBox2.SuspendLayout();
        i_GroupBox1.SuspendLayout();
        tpDiskNet.SuspendLayout();
        d_TableLayout1.SuspendLayout();
        d_TableLayout2.SuspendLayout();
        d_GroupBox2.SuspendLayout();
        d_GroupBox1.SuspendLayout();
        tpModules.SuspendLayout();
        tpThreads.SuspendLayout();
        tpLocked.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)tbSpeed).BeginInit();
        SuspendLayout();
        // 
        // tc
        // 
        tc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tc.Controls.Add(tpGeneral);
        tc.Controls.Add(tpPerformance);
        tc.Controls.Add(tpIO);
        tc.Controls.Add(tpDiskNet);
        tc.Controls.Add(tpModules);
        tc.Controls.Add(tpThreads);
        tc.Controls.Add(tpLocked);
        tc.Location = new Point(7, 7);
        tc.Margin = new Padding(0);
        tc.Multiline = true;
        tc.Name = "tc";
        tc.Padding = new Point(5, 3);
        tc.SelectedIndex = 0;
        tc.Size = new Size(471, 464);
        tc.SizeMode = TabSizeMode.FillToRight;
        tc.TabIndex = 0;
        // 
        // tpGeneral
        // 
        tpGeneral.Controls.Add(g_TableLayout1);
        tpGeneral.Controls.Add(g_gpDetails);
        tpGeneral.Location = new Point(4, 24);
        tpGeneral.Name = "tpGeneral";
        tpGeneral.Size = new Size(463, 436);
        tpGeneral.TabIndex = 0;
        tpGeneral.Text = "General";
        tpGeneral.UseVisualStyleBackColor = true;
        // 
        // g_TableLayout1
        // 
        g_TableLayout1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_TableLayout1.ColumnCount = 2;
        g_TableLayout1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        g_TableLayout1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        g_TableLayout1.Controls.Add(g_gpCPU, 0, 0);
        g_TableLayout1.Controls.Add(g_gpVmMemory, 1, 1);
        g_TableLayout1.Controls.Add(g_gpPhMemory, 0, 1);
        g_TableLayout1.Controls.Add(g_gpResources, 1, 0);
        g_TableLayout1.Location = new Point(3, 192);
        g_TableLayout1.Name = "g_TableLayout1";
        g_TableLayout1.RowCount = 2;
        g_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Absolute, 142F));
        g_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
        g_TableLayout1.Size = new Size(455, 238);
        g_TableLayout1.TabIndex = 1;
        // 
        // g_gpCPU
        // 
        g_gpCPU.Controls.Add(g_lblCPURunning);
        g_gpCPU.Controls.Add(g_lblCPUCreation);
        g_gpCPU.Controls.Add(g_lblCPUTimeTotal);
        g_gpCPU.Controls.Add(g_lblCPUTimeKernel);
        g_gpCPU.Controls.Add(g_lblCPUTimeUser);
        g_gpCPU.Controls.Add(g_lblCPUPriority);
        g_gpCPU.Controls.Add(lblDivider2);
        g_gpCPU.Controls.Add(lblDivider1);
        g_gpCPU.Controls.Add(g_Label13);
        g_gpCPU.Controls.Add(g_Label12);
        g_gpCPU.Controls.Add(g_Label11);
        g_gpCPU.Controls.Add(g_Label10);
        g_gpCPU.Controls.Add(g_Label09);
        g_gpCPU.Controls.Add(g_Label08);
        g_gpCPU.Dock = DockStyle.Fill;
        g_gpCPU.Location = new Point(0, 1);
        g_gpCPU.Margin = new Padding(0, 1, 3, 3);
        g_gpCPU.Name = "g_gpCPU";
        g_gpCPU.Size = new Size(224, 138);
        g_gpCPU.TabIndex = 0;
        g_gpCPU.TabStop = false;
        g_gpCPU.Text = "CPU Details";
        // 
        // g_lblCPURunning
        // 
        g_lblCPURunning.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblCPURunning.Location = new Point(92, 115);
        g_lblCPURunning.Margin = new Padding(3, 0, 3, 1);
        g_lblCPURunning.Name = "g_lblCPURunning";
        g_lblCPURunning.Size = new Size(129, 16);
        g_lblCPURunning.TabIndex = 12;
        g_lblCPURunning.TextAlign = ContentAlignment.MiddleRight;
        // 
        // g_lblCPUCreation
        // 
        g_lblCPUCreation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblCPUCreation.Location = new Point(92, 98);
        g_lblCPUCreation.Margin = new Padding(3, 0, 3, 1);
        g_lblCPUCreation.Name = "g_lblCPUCreation";
        g_lblCPUCreation.Size = new Size(129, 16);
        g_lblCPUCreation.TabIndex = 11;
        g_lblCPUCreation.TextAlign = ContentAlignment.MiddleRight;
        // 
        // g_lblCPUTimeTotal
        // 
        g_lblCPUTimeTotal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblCPUTimeTotal.Location = new Point(80, 75);
        g_lblCPUTimeTotal.Margin = new Padding(3, 0, 3, 1);
        g_lblCPUTimeTotal.Name = "g_lblCPUTimeTotal";
        g_lblCPUTimeTotal.Size = new Size(141, 16);
        g_lblCPUTimeTotal.TabIndex = 10;
        g_lblCPUTimeTotal.TextAlign = ContentAlignment.MiddleRight;
        // 
        // g_lblCPUTimeKernel
        // 
        g_lblCPUTimeKernel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblCPUTimeKernel.Location = new Point(80, 58);
        g_lblCPUTimeKernel.Margin = new Padding(3, 0, 3, 1);
        g_lblCPUTimeKernel.Name = "g_lblCPUTimeKernel";
        g_lblCPUTimeKernel.Size = new Size(141, 16);
        g_lblCPUTimeKernel.TabIndex = 9;
        g_lblCPUTimeKernel.TextAlign = ContentAlignment.MiddleRight;
        // 
        // g_lblCPUTimeUser
        // 
        g_lblCPUTimeUser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblCPUTimeUser.Location = new Point(80, 41);
        g_lblCPUTimeUser.Margin = new Padding(3, 0, 3, 1);
        g_lblCPUTimeUser.Name = "g_lblCPUTimeUser";
        g_lblCPUTimeUser.Size = new Size(141, 16);
        g_lblCPUTimeUser.TabIndex = 8;
        g_lblCPUTimeUser.TextAlign = ContentAlignment.MiddleRight;
        // 
        // g_lblCPUPriority
        // 
        g_lblCPUPriority.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblCPUPriority.Location = new Point(80, 18);
        g_lblCPUPriority.Margin = new Padding(3, 0, 3, 1);
        g_lblCPUPriority.Name = "g_lblCPUPriority";
        g_lblCPUPriority.Size = new Size(141, 16);
        g_lblCPUPriority.TabIndex = 7;
        g_lblCPUPriority.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblDivider2
        // 
        lblDivider2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblDivider2.BackColor = SystemColors.ControlDark;
        lblDivider2.Location = new Point(6, 94);
        lblDivider2.Margin = new Padding(3, 0, 3, 1);
        lblDivider2.Name = "lblDivider2";
        lblDivider2.Size = new Size(212, 1);
        lblDivider2.TabIndex = 4;
        lblDivider2.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblDivider1
        // 
        lblDivider1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblDivider1.BackColor = SystemColors.ControlDark;
        lblDivider1.Location = new Point(6, 37);
        lblDivider1.Margin = new Padding(3, 0, 3, 1);
        lblDivider1.Name = "lblDivider1";
        lblDivider1.Size = new Size(212, 1);
        lblDivider1.TabIndex = 1;
        lblDivider1.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label13
        // 
        g_Label13.Location = new Point(6, 115);
        g_Label13.Margin = new Padding(3, 0, 3, 1);
        g_Label13.Name = "g_Label13";
        g_Label13.Size = new Size(90, 16);
        g_Label13.TabIndex = 6;
        g_Label13.Text = "Running Time:";
        g_Label13.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label12
        // 
        g_Label12.Location = new Point(6, 98);
        g_Label12.Margin = new Padding(3, 0, 3, 1);
        g_Label12.Name = "g_Label12";
        g_Label12.Size = new Size(90, 16);
        g_Label12.TabIndex = 5;
        g_Label12.Text = "Creation Time:";
        g_Label12.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label11
        // 
        g_Label11.Location = new Point(6, 75);
        g_Label11.Margin = new Padding(3, 0, 3, 1);
        g_Label11.Name = "g_Label11";
        g_Label11.Size = new Size(75, 16);
        g_Label11.TabIndex = 3;
        g_Label11.Text = "Total Time:";
        g_Label11.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label10
        // 
        g_Label10.Location = new Point(6, 58);
        g_Label10.Margin = new Padding(3, 0, 3, 1);
        g_Label10.Name = "g_Label10";
        g_Label10.Size = new Size(75, 16);
        g_Label10.TabIndex = 2;
        g_Label10.Text = "Kernel Time:";
        g_Label10.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label09
        // 
        g_Label09.Location = new Point(6, 41);
        g_Label09.Margin = new Padding(3, 0, 3, 1);
        g_Label09.Name = "g_Label09";
        g_Label09.Size = new Size(75, 16);
        g_Label09.TabIndex = 8;
        g_Label09.Text = "User Time:";
        g_Label09.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label08
        // 
        g_Label08.Location = new Point(6, 18);
        g_Label08.Margin = new Padding(3, 0, 3, 1);
        g_Label08.Name = "g_Label08";
        g_Label08.Size = new Size(75, 16);
        g_Label08.TabIndex = 0;
        g_Label08.Text = "Priority:";
        g_Label08.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_gpVmMemory
        // 
        g_gpVmMemory.Controls.Add(g_lblPageFaultsDelta);
        g_gpVmMemory.Controls.Add(g_lblPageFaults);
        g_gpVmMemory.Controls.Add(g_lblPagedMemory);
        g_gpVmMemory.Controls.Add(g_lblVirtualMemory);
        g_gpVmMemory.Controls.Add(g_Label27);
        g_gpVmMemory.Controls.Add(g_Label26);
        g_gpVmMemory.Controls.Add(g_Label25);
        g_gpVmMemory.Controls.Add(g_Label24);
        g_gpVmMemory.Dock = DockStyle.Fill;
        g_gpVmMemory.Location = new Point(230, 145);
        g_gpVmMemory.Margin = new Padding(3, 3, 0, 1);
        g_gpVmMemory.Name = "g_gpVmMemory";
        g_gpVmMemory.Size = new Size(225, 92);
        g_gpVmMemory.TabIndex = 1;
        g_gpVmMemory.TabStop = false;
        g_gpVmMemory.Text = "Virtual Memory";
        // 
        // g_lblPageFaultsDelta
        // 
        g_lblPageFaultsDelta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblPageFaultsDelta.Location = new Point(109, 69);
        g_lblPageFaultsDelta.Margin = new Padding(3, 0, 3, 1);
        g_lblPageFaultsDelta.Name = "g_lblPageFaultsDelta";
        g_lblPageFaultsDelta.Size = new Size(113, 16);
        g_lblPageFaultsDelta.TabIndex = 7;
        g_lblPageFaultsDelta.TextAlign = ContentAlignment.MiddleRight;
        // 
        // g_lblPageFaults
        // 
        g_lblPageFaults.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblPageFaults.Location = new Point(100, 52);
        g_lblPageFaults.Margin = new Padding(3, 0, 3, 1);
        g_lblPageFaults.Name = "g_lblPageFaults";
        g_lblPageFaults.Size = new Size(122, 16);
        g_lblPageFaults.TabIndex = 6;
        g_lblPageFaults.TextAlign = ContentAlignment.MiddleRight;
        // 
        // g_lblPagedMemory
        // 
        g_lblPagedMemory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblPagedMemory.Location = new Point(100, 35);
        g_lblPagedMemory.Margin = new Padding(3, 0, 3, 1);
        g_lblPagedMemory.Name = "g_lblPagedMemory";
        g_lblPagedMemory.Size = new Size(122, 16);
        g_lblPagedMemory.TabIndex = 5;
        g_lblPagedMemory.TextAlign = ContentAlignment.MiddleRight;
        // 
        // g_lblVirtualMemory
        // 
        g_lblVirtualMemory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblVirtualMemory.Location = new Point(100, 18);
        g_lblVirtualMemory.Margin = new Padding(3, 0, 3, 1);
        g_lblVirtualMemory.Name = "g_lblVirtualMemory";
        g_lblVirtualMemory.Size = new Size(122, 16);
        g_lblVirtualMemory.TabIndex = 4;
        g_lblVirtualMemory.TextAlign = ContentAlignment.MiddleRight;
        // 
        // g_Label27
        // 
        g_Label27.Location = new Point(6, 69);
        g_Label27.Margin = new Padding(3, 0, 3, 1);
        g_Label27.Name = "g_Label27";
        g_Label27.Size = new Size(100, 16);
        g_Label27.TabIndex = 3;
        g_Label27.Text = "Page Faults Delta:";
        g_Label27.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label26
        // 
        g_Label26.Location = new Point(6, 52);
        g_Label26.Margin = new Padding(3, 0, 3, 1);
        g_Label26.Name = "g_Label26";
        g_Label26.Size = new Size(95, 16);
        g_Label26.TabIndex = 2;
        g_Label26.Text = "Page Faults:";
        g_Label26.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label25
        // 
        g_Label25.Location = new Point(6, 35);
        g_Label25.Margin = new Padding(3, 0, 3, 1);
        g_Label25.Name = "g_Label25";
        g_Label25.Size = new Size(95, 16);
        g_Label25.TabIndex = 1;
        g_Label25.Text = "Paged Memory:";
        g_Label25.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label24
        // 
        g_Label24.Location = new Point(6, 18);
        g_Label24.Margin = new Padding(3, 0, 3, 1);
        g_Label24.Name = "g_Label24";
        g_Label24.Size = new Size(95, 16);
        g_Label24.TabIndex = 0;
        g_Label24.Text = "Virtual Memory:";
        g_Label24.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_gpPhMemory
        // 
        g_gpPhMemory.Controls.Add(g_lblMemWSpeak);
        g_gpPhMemory.Controls.Add(g_Label17);
        g_gpPhMemory.Controls.Add(g_lblMemWSshare);
        g_gpPhMemory.Controls.Add(g_lblMemWSpriv);
        g_gpPhMemory.Controls.Add(g_lblMemWS);
        g_gpPhMemory.Controls.Add(g_Label16);
        g_gpPhMemory.Controls.Add(g_Label15);
        g_gpPhMemory.Controls.Add(g_Label14);
        g_gpPhMemory.Dock = DockStyle.Fill;
        g_gpPhMemory.Location = new Point(0, 145);
        g_gpPhMemory.Margin = new Padding(0, 3, 3, 1);
        g_gpPhMemory.Name = "g_gpPhMemory";
        g_gpPhMemory.Size = new Size(224, 92);
        g_gpPhMemory.TabIndex = 0;
        g_gpPhMemory.TabStop = false;
        g_gpPhMemory.Text = "Physical Memory";
        // 
        // g_lblMemWSpeak
        // 
        g_lblMemWSpeak.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblMemWSpeak.Location = new Point(109, 69);
        g_lblMemWSpeak.Margin = new Padding(3, 0, 3, 1);
        g_lblMemWSpeak.Name = "g_lblMemWSpeak";
        g_lblMemWSpeak.Size = new Size(112, 16);
        g_lblMemWSpeak.TabIndex = 7;
        g_lblMemWSpeak.TextAlign = ContentAlignment.MiddleRight;
        // 
        // g_Label17
        // 
        g_Label17.Location = new Point(6, 69);
        g_Label17.Margin = new Padding(3, 0, 3, 1);
        g_Label17.Name = "g_Label17";
        g_Label17.Size = new Size(110, 16);
        g_Label17.TabIndex = 3;
        g_Label17.Text = "Peak Working Set:";
        g_Label17.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_lblMemWSshare
        // 
        g_lblMemWSshare.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblMemWSshare.Location = new Point(96, 52);
        g_lblMemWSshare.Margin = new Padding(3, 0, 3, 1);
        g_lblMemWSshare.Name = "g_lblMemWSshare";
        g_lblMemWSshare.Size = new Size(125, 16);
        g_lblMemWSshare.TabIndex = 6;
        g_lblMemWSshare.TextAlign = ContentAlignment.MiddleRight;
        // 
        // g_lblMemWSpriv
        // 
        g_lblMemWSpriv.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblMemWSpriv.Location = new Point(96, 35);
        g_lblMemWSpriv.Margin = new Padding(3, 0, 3, 1);
        g_lblMemWSpriv.Name = "g_lblMemWSpriv";
        g_lblMemWSpriv.Size = new Size(125, 16);
        g_lblMemWSpriv.TabIndex = 5;
        g_lblMemWSpriv.TextAlign = ContentAlignment.MiddleRight;
        // 
        // g_lblMemWS
        // 
        g_lblMemWS.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblMemWS.Location = new Point(96, 18);
        g_lblMemWS.Margin = new Padding(3, 0, 3, 1);
        g_lblMemWS.Name = "g_lblMemWS";
        g_lblMemWS.Size = new Size(125, 16);
        g_lblMemWS.TabIndex = 4;
        g_lblMemWS.TextAlign = ContentAlignment.MiddleRight;
        // 
        // g_Label16
        // 
        g_Label16.Location = new Point(14, 52);
        g_Label16.Margin = new Padding(3, 0, 3, 1);
        g_Label16.Name = "g_Label16";
        g_Label16.Size = new Size(90, 16);
        g_Label16.TabIndex = 2;
        g_Label16.Text = "WS Shareable:";
        g_Label16.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label15
        // 
        g_Label15.Location = new Point(14, 35);
        g_Label15.Margin = new Padding(3, 0, 3, 1);
        g_Label15.Name = "g_Label15";
        g_Label15.Size = new Size(90, 16);
        g_Label15.TabIndex = 1;
        g_Label15.Text = "WS Private:";
        g_Label15.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label14
        // 
        g_Label14.Location = new Point(6, 18);
        g_Label14.Margin = new Padding(3, 0, 3, 1);
        g_Label14.Name = "g_Label14";
        g_Label14.Size = new Size(90, 16);
        g_Label14.TabIndex = 0;
        g_Label14.Text = "Working Set:";
        g_Label14.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_gpResources
        // 
        g_gpResources.Controls.Add(g_lblResUser);
        g_gpResources.Controls.Add(g_lblResGDI);
        g_gpResources.Controls.Add(g_lblResHandlesPeak);
        g_gpResources.Controls.Add(g_lblResHandles);
        g_gpResources.Controls.Add(g_lblResThreadsPeak);
        g_gpResources.Controls.Add(g_lblResThreads);
        g_gpResources.Controls.Add(lblDivider4);
        g_gpResources.Controls.Add(lblDivider3);
        g_gpResources.Controls.Add(g_Label23);
        g_gpResources.Controls.Add(g_Label22);
        g_gpResources.Controls.Add(g_Label21);
        g_gpResources.Controls.Add(g_Label20);
        g_gpResources.Controls.Add(g_Label18);
        g_gpResources.Controls.Add(g_Label19);
        g_gpResources.Dock = DockStyle.Fill;
        g_gpResources.Location = new Point(230, 1);
        g_gpResources.Margin = new Padding(3, 1, 0, 3);
        g_gpResources.Name = "g_gpResources";
        g_gpResources.Size = new Size(225, 138);
        g_gpResources.TabIndex = 3;
        g_gpResources.TabStop = false;
        g_gpResources.Text = "Resources";
        // 
        // g_lblResUser
        // 
        g_lblResUser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblResUser.Location = new Point(92, 115);
        g_lblResUser.Margin = new Padding(3, 0, 3, 1);
        g_lblResUser.Name = "g_lblResUser";
        g_lblResUser.Size = new Size(130, 16);
        g_lblResUser.TabIndex = 12;
        g_lblResUser.TextAlign = ContentAlignment.MiddleRight;
        // 
        // g_lblResGDI
        // 
        g_lblResGDI.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblResGDI.Location = new Point(92, 98);
        g_lblResGDI.Margin = new Padding(3, 0, 3, 1);
        g_lblResGDI.Name = "g_lblResGDI";
        g_lblResGDI.Size = new Size(130, 16);
        g_lblResGDI.TabIndex = 11;
        g_lblResGDI.TextAlign = ContentAlignment.MiddleRight;
        // 
        // g_lblResHandlesPeak
        // 
        g_lblResHandlesPeak.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblResHandlesPeak.Location = new Point(92, 75);
        g_lblResHandlesPeak.Margin = new Padding(3, 0, 3, 1);
        g_lblResHandlesPeak.Name = "g_lblResHandlesPeak";
        g_lblResHandlesPeak.Size = new Size(130, 16);
        g_lblResHandlesPeak.TabIndex = 10;
        g_lblResHandlesPeak.TextAlign = ContentAlignment.MiddleRight;
        // 
        // g_lblResHandles
        // 
        g_lblResHandles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblResHandles.Location = new Point(92, 58);
        g_lblResHandles.Margin = new Padding(3, 0, 3, 1);
        g_lblResHandles.Name = "g_lblResHandles";
        g_lblResHandles.Size = new Size(130, 16);
        g_lblResHandles.TabIndex = 9;
        g_lblResHandles.TextAlign = ContentAlignment.MiddleRight;
        // 
        // g_lblResThreadsPeak
        // 
        g_lblResThreadsPeak.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblResThreadsPeak.Location = new Point(92, 35);
        g_lblResThreadsPeak.Margin = new Padding(3, 0, 3, 1);
        g_lblResThreadsPeak.Name = "g_lblResThreadsPeak";
        g_lblResThreadsPeak.Size = new Size(130, 16);
        g_lblResThreadsPeak.TabIndex = 8;
        g_lblResThreadsPeak.TextAlign = ContentAlignment.MiddleRight;
        // 
        // g_lblResThreads
        // 
        g_lblResThreads.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_lblResThreads.Location = new Point(92, 18);
        g_lblResThreads.Margin = new Padding(3, 0, 3, 1);
        g_lblResThreads.Name = "g_lblResThreads";
        g_lblResThreads.Size = new Size(130, 16);
        g_lblResThreads.TabIndex = 7;
        g_lblResThreads.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblDivider4
        // 
        lblDivider4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblDivider4.BackColor = SystemColors.ControlDark;
        lblDivider4.Location = new Point(6, 94);
        lblDivider4.Margin = new Padding(3, 0, 3, 1);
        lblDivider4.Name = "lblDivider4";
        lblDivider4.Size = new Size(213, 1);
        lblDivider4.TabIndex = 28;
        lblDivider4.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblDivider3
        // 
        lblDivider3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblDivider3.BackColor = SystemColors.ControlDark;
        lblDivider3.Location = new Point(6, 54);
        lblDivider3.Margin = new Padding(3, 0, 3, 1);
        lblDivider3.Name = "lblDivider3";
        lblDivider3.Size = new Size(213, 1);
        lblDivider3.TabIndex = 2;
        lblDivider3.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label23
        // 
        g_Label23.Location = new Point(6, 115);
        g_Label23.Margin = new Padding(3, 0, 3, 1);
        g_Label23.Name = "g_Label23";
        g_Label23.Size = new Size(85, 16);
        g_Label23.TabIndex = 6;
        g_Label23.Text = "User Objects:";
        g_Label23.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label22
        // 
        g_Label22.Location = new Point(6, 98);
        g_Label22.Margin = new Padding(3, 0, 3, 1);
        g_Label22.Name = "g_Label22";
        g_Label22.Size = new Size(85, 16);
        g_Label22.TabIndex = 5;
        g_Label22.Text = "GDI Objects:";
        g_Label22.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label21
        // 
        g_Label21.Location = new Point(6, 75);
        g_Label21.Margin = new Padding(3, 0, 3, 1);
        g_Label21.Name = "g_Label21";
        g_Label21.Size = new Size(85, 16);
        g_Label21.TabIndex = 4;
        g_Label21.Text = "Peak Handles:";
        g_Label21.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label20
        // 
        g_Label20.Location = new Point(6, 58);
        g_Label20.Margin = new Padding(3, 0, 3, 1);
        g_Label20.Name = "g_Label20";
        g_Label20.Size = new Size(85, 16);
        g_Label20.TabIndex = 3;
        g_Label20.Text = "Handles:";
        g_Label20.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label18
        // 
        g_Label18.Location = new Point(6, 18);
        g_Label18.Margin = new Padding(3, 0, 3, 1);
        g_Label18.Name = "g_Label18";
        g_Label18.Size = new Size(85, 16);
        g_Label18.TabIndex = 0;
        g_Label18.Text = "Threads:";
        g_Label18.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label19
        // 
        g_Label19.Location = new Point(6, 35);
        g_Label19.Margin = new Padding(3, 0, 3, 1);
        g_Label19.Name = "g_Label19";
        g_Label19.Size = new Size(85, 16);
        g_Label19.TabIndex = 1;
        g_Label19.Text = "Peak Threads:";
        g_Label19.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_gpDetails
        // 
        g_gpDetails.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_gpDetails.Controls.Add(g_pbIcon);
        g_gpDetails.Controls.Add(g_txtDescription);
        g_gpDetails.Controls.Add(g_txtCompany);
        g_gpDetails.Controls.Add(g_txtVersion);
        g_gpDetails.Controls.Add(g_txtType);
        g_gpDetails.Controls.Add(g_txtPath);
        g_gpDetails.Controls.Add(g_txtName);
        g_gpDetails.Controls.Add(g_Label07);
        g_gpDetails.Controls.Add(g_Label06);
        g_gpDetails.Controls.Add(g_Label05);
        g_gpDetails.Controls.Add(g_Label04);
        g_gpDetails.Controls.Add(g_Label03);
        g_gpDetails.Controls.Add(g_txtPID);
        g_gpDetails.Controls.Add(g_Label02);
        g_gpDetails.Controls.Add(g_Label01);
        g_gpDetails.Location = new Point(3, 6);
        g_gpDetails.Name = "g_gpDetails";
        g_gpDetails.Size = new Size(455, 180);
        g_gpDetails.TabIndex = 0;
        g_gpDetails.TabStop = false;
        g_gpDetails.Text = "Process Details";
        // 
        // g_pbIcon
        // 
        g_pbIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        g_pbIcon.Image = Resources.Resources.pbProcessDetails;
        g_pbIcon.Location = new Point(401, 10);
        g_pbIcon.Name = "g_pbIcon";
        g_pbIcon.Size = new Size(40, 40);
        g_pbIcon.SizeMode = PictureBoxSizeMode.CenterImage;
        g_pbIcon.TabIndex = 14;
        g_pbIcon.TabStop = false;
        // 
        // g_txtDescription
        // 
        g_txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_txtDescription.BackColor = SystemColors.Window;
        g_txtDescription.BorderStyle = BorderStyle.None;
        g_txtDescription.Location = new Point(77, 129);
        g_txtDescription.Multiline = true;
        g_txtDescription.Name = "g_txtDescription";
        g_txtDescription.ReadOnly = true;
        g_txtDescription.Size = new Size(364, 42);
        g_txtDescription.TabIndex = 13;
        // 
        // g_txtCompany
        // 
        g_txtCompany.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_txtCompany.BackColor = SystemColors.Window;
        g_txtCompany.BorderStyle = BorderStyle.None;
        g_txtCompany.Location = new Point(80, 111);
        g_txtCompany.Name = "g_txtCompany";
        g_txtCompany.ReadOnly = true;
        g_txtCompany.Size = new Size(361, 16);
        g_txtCompany.TabIndex = 11;
        // 
        // g_txtVersion
        // 
        g_txtVersion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_txtVersion.BackColor = SystemColors.Window;
        g_txtVersion.BorderStyle = BorderStyle.None;
        g_txtVersion.Location = new Point(80, 93);
        g_txtVersion.Name = "g_txtVersion";
        g_txtVersion.ReadOnly = true;
        g_txtVersion.Size = new Size(361, 16);
        g_txtVersion.TabIndex = 9;
        // 
        // g_txtType
        // 
        g_txtType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_txtType.BackColor = SystemColors.Window;
        g_txtType.BorderStyle = BorderStyle.None;
        g_txtType.Location = new Point(80, 75);
        g_txtType.Name = "g_txtType";
        g_txtType.ReadOnly = true;
        g_txtType.Size = new Size(361, 16);
        g_txtType.TabIndex = 7;
        // 
        // g_txtPath
        // 
        g_txtPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_txtPath.BackColor = SystemColors.Window;
        g_txtPath.BorderStyle = BorderStyle.None;
        g_txtPath.Location = new Point(80, 57);
        g_txtPath.Name = "g_txtPath";
        g_txtPath.ReadOnly = true;
        g_txtPath.Size = new Size(361, 16);
        g_txtPath.TabIndex = 5;
        // 
        // g_txtName
        // 
        g_txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_txtName.BackColor = SystemColors.Window;
        g_txtName.BorderStyle = BorderStyle.None;
        g_txtName.Location = new Point(80, 39);
        g_txtName.Name = "g_txtName";
        g_txtName.ReadOnly = true;
        g_txtName.Size = new Size(303, 16);
        g_txtName.TabIndex = 3;
        // 
        // g_Label07
        // 
        g_Label07.Location = new Point(6, 128);
        g_Label07.Margin = new Padding(3, 0, 3, 2);
        g_Label07.Name = "g_Label07";
        g_Label07.Size = new Size(75, 16);
        g_Label07.TabIndex = 12;
        g_Label07.Text = "Description:";
        g_Label07.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label06
        // 
        g_Label06.Location = new Point(6, 110);
        g_Label06.Margin = new Padding(3, 0, 3, 2);
        g_Label06.Name = "g_Label06";
        g_Label06.Size = new Size(75, 16);
        g_Label06.TabIndex = 10;
        g_Label06.Text = "Company:";
        g_Label06.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label05
        // 
        g_Label05.Location = new Point(6, 92);
        g_Label05.Margin = new Padding(3, 0, 3, 2);
        g_Label05.Name = "g_Label05";
        g_Label05.Size = new Size(75, 16);
        g_Label05.TabIndex = 8;
        g_Label05.Text = "Version:";
        g_Label05.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label04
        // 
        g_Label04.Location = new Point(6, 74);
        g_Label04.Margin = new Padding(3, 0, 3, 2);
        g_Label04.Name = "g_Label04";
        g_Label04.Size = new Size(75, 16);
        g_Label04.TabIndex = 6;
        g_Label04.Text = "File Type:";
        g_Label04.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label03
        // 
        g_Label03.Location = new Point(6, 56);
        g_Label03.Margin = new Padding(3, 0, 3, 2);
        g_Label03.Name = "g_Label03";
        g_Label03.Size = new Size(75, 16);
        g_Label03.TabIndex = 4;
        g_Label03.Text = "Full Path:";
        g_Label03.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_txtPID
        // 
        g_txtPID.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        g_txtPID.BackColor = SystemColors.Window;
        g_txtPID.BorderStyle = BorderStyle.None;
        g_txtPID.Location = new Point(80, 21);
        g_txtPID.Name = "g_txtPID";
        g_txtPID.ReadOnly = true;
        g_txtPID.Size = new Size(303, 16);
        g_txtPID.TabIndex = 1;
        // 
        // g_Label02
        // 
        g_Label02.Location = new Point(6, 38);
        g_Label02.Margin = new Padding(3, 0, 3, 2);
        g_Label02.Name = "g_Label02";
        g_Label02.Size = new Size(75, 16);
        g_Label02.TabIndex = 2;
        g_Label02.Text = "Name:";
        g_Label02.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // g_Label01
        // 
        g_Label01.Location = new Point(6, 20);
        g_Label01.Margin = new Padding(3, 0, 3, 2);
        g_Label01.Name = "g_Label01";
        g_Label01.Size = new Size(75, 16);
        g_Label01.TabIndex = 0;
        g_Label01.Text = "PID:";
        g_Label01.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // tpPerformance
        // 
        tpPerformance.Controls.Add(p_TableLayout1);
        tpPerformance.Location = new Point(4, 24);
        tpPerformance.Name = "tpPerformance";
        tpPerformance.Size = new Size(463, 436);
        tpPerformance.TabIndex = 1;
        tpPerformance.Text = "Performance";
        tpPerformance.UseVisualStyleBackColor = true;
        // 
        // p_TableLayout1
        // 
        p_TableLayout1.ColumnCount = 2;
        p_TableLayout1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        p_TableLayout1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        p_TableLayout1.Controls.Add(p_ChartGDI, 0, 7);
        p_TableLayout1.Controls.Add(p_panel2, 0, 2);
        p_TableLayout1.Controls.Add(p_panel5, 1, 4);
        p_TableLayout1.Controls.Add(p_panel3, 1, 2);
        p_TableLayout1.Controls.Add(p_panel7, 1, 6);
        p_TableLayout1.Controls.Add(p_panel1, 0, 0);
        p_TableLayout1.Controls.Add(p_panel6, 0, 6);
        p_TableLayout1.Controls.Add(p_panel4, 0, 4);
        p_TableLayout1.Controls.Add(p_ChartCPU, 0, 1);
        p_TableLayout1.Controls.Add(p_ChartWS, 0, 3);
        p_TableLayout1.Controls.Add(p_ChartPB, 1, 3);
        p_TableLayout1.Controls.Add(p_ChartVM, 0, 5);
        p_TableLayout1.Controls.Add(p_ChartPF, 1, 5);
        p_TableLayout1.Controls.Add(p_ChartUser, 1, 7);
        p_TableLayout1.Dock = DockStyle.Fill;
        p_TableLayout1.Location = new Point(0, 0);
        p_TableLayout1.Name = "p_TableLayout1";
        p_TableLayout1.Padding = new Padding(1, 0, 2, 0);
        p_TableLayout1.RowCount = 8;
        p_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        p_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        p_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
        p_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        p_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
        p_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        p_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
        p_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        p_TableLayout1.Size = new Size(463, 436);
        p_TableLayout1.TabIndex = 0;
        // 
        // p_ChartGDI
        // 
        p_ChartGDI.AntiAliasing = true;
        p_ChartGDI.BackColor = Color.Black;
        p_ChartGDI.BackColorShade = Color.FromArgb(0, 0, 0);
        p_ChartGDI.BackSolid = true;
        p_ChartGDI.BorderStyle = Border3DStyle.Sunken;
        p_ChartGDI.DetailsOnHover = false;
        p_ChartGDI.DisplayAverage = false;
        p_ChartGDI.DisplayIndexes = false;
        p_ChartGDI.DisplayLegends = true;
        p_ChartGDI.Dock = DockStyle.Fill;
        p_ChartGDI.GridSpacing = 10;
        p_ChartGDI.LegendSpacing = 17;
        p_ChartGDI.LightColors = false;
        p_ChartGDI.Location = new Point(3, 346);
        p_ChartGDI.Margin = new Padding(2);
        p_ChartGDI.MaxValue = 0D;
        p_ChartGDI.Name = "p_ChartGDI";
        p_ChartGDI.ScaleMode = sMkTaskManager.Controls.sMkPerfChart.ScaleModes.Relative;
        p_ChartGDI.ShadeBackground = false;
        p_ChartGDI.Size = new Size(226, 88);
        p_ChartGDI.TabIndex = 12;
        p_ChartGDI.ValueSpacing = 2;
        p_ChartGDI.ValuesSuffix = "K";
        // 
        // p_panel2
        // 
        p_panel2.Controls.Add(p_lblWorkingSet);
        p_panel2.Controls.Add(p_Label02);
        p_panel2.Dock = DockStyle.Fill;
        p_panel2.Location = new Point(2, 113);
        p_panel2.Margin = new Padding(1);
        p_panel2.Name = "p_panel2";
        p_panel2.Size = new Size(228, 14);
        p_panel2.TabIndex = 3;
        // 
        // p_lblWorkingSet
        // 
        p_lblWorkingSet.Dock = DockStyle.Fill;
        p_lblWorkingSet.Location = new Point(90, 0);
        p_lblWorkingSet.Name = "p_lblWorkingSet";
        p_lblWorkingSet.Padding = new Padding(0, 0, 3, 0);
        p_lblWorkingSet.Size = new Size(138, 14);
        p_lblWorkingSet.TabIndex = 1;
        p_lblWorkingSet.Text = "0";
        p_lblWorkingSet.TextAlign = ContentAlignment.BottomRight;
        // 
        // p_Label02
        // 
        p_Label02.Dock = DockStyle.Left;
        p_Label02.Location = new Point(0, 0);
        p_Label02.Name = "p_Label02";
        p_Label02.Padding = new Padding(3, 0, 0, 0);
        p_Label02.Size = new Size(90, 14);
        p_Label02.TabIndex = 0;
        p_Label02.Text = "Working Set";
        p_Label02.TextAlign = ContentAlignment.BottomLeft;
        // 
        // p_panel5
        // 
        p_panel5.Controls.Add(p_lblPageFaults);
        p_panel5.Controls.Add(p_Label05);
        p_panel5.Dock = DockStyle.Fill;
        p_panel5.Location = new Point(232, 221);
        p_panel5.Margin = new Padding(1);
        p_panel5.Name = "p_panel5";
        p_panel5.Size = new Size(228, 14);
        p_panel5.TabIndex = 9;
        // 
        // p_lblPageFaults
        // 
        p_lblPageFaults.Dock = DockStyle.Fill;
        p_lblPageFaults.Location = new Point(90, 0);
        p_lblPageFaults.Name = "p_lblPageFaults";
        p_lblPageFaults.Padding = new Padding(0, 0, 3, 0);
        p_lblPageFaults.Size = new Size(138, 14);
        p_lblPageFaults.TabIndex = 1;
        p_lblPageFaults.Text = "0";
        p_lblPageFaults.TextAlign = ContentAlignment.BottomRight;
        // 
        // p_Label05
        // 
        p_Label05.Dock = DockStyle.Left;
        p_Label05.Location = new Point(0, 0);
        p_Label05.Name = "p_Label05";
        p_Label05.Padding = new Padding(3, 0, 0, 0);
        p_Label05.Size = new Size(90, 14);
        p_Label05.TabIndex = 0;
        p_Label05.Text = "Page Faults";
        p_Label05.TextAlign = ContentAlignment.BottomLeft;
        // 
        // p_panel3
        // 
        p_panel3.Controls.Add(p_lblPrivateBytes);
        p_panel3.Controls.Add(p_Label03);
        p_panel3.Dock = DockStyle.Fill;
        p_panel3.Location = new Point(232, 113);
        p_panel3.Margin = new Padding(1);
        p_panel3.Name = "p_panel3";
        p_panel3.Size = new Size(228, 14);
        p_panel3.TabIndex = 5;
        // 
        // p_lblPrivateBytes
        // 
        p_lblPrivateBytes.Dock = DockStyle.Fill;
        p_lblPrivateBytes.Location = new Point(90, 0);
        p_lblPrivateBytes.Name = "p_lblPrivateBytes";
        p_lblPrivateBytes.Padding = new Padding(0, 0, 3, 0);
        p_lblPrivateBytes.Size = new Size(138, 14);
        p_lblPrivateBytes.TabIndex = 1;
        p_lblPrivateBytes.Text = "0";
        p_lblPrivateBytes.TextAlign = ContentAlignment.BottomRight;
        // 
        // p_Label03
        // 
        p_Label03.Dock = DockStyle.Left;
        p_Label03.Location = new Point(0, 0);
        p_Label03.Name = "p_Label03";
        p_Label03.Padding = new Padding(3, 0, 0, 0);
        p_Label03.Size = new Size(90, 14);
        p_Label03.TabIndex = 0;
        p_Label03.Text = "Private Bytes";
        p_Label03.TextAlign = ContentAlignment.BottomLeft;
        // 
        // p_panel7
        // 
        p_panel7.Controls.Add(p_lblUser);
        p_panel7.Controls.Add(p_Label07);
        p_panel7.Dock = DockStyle.Fill;
        p_panel7.Location = new Point(232, 329);
        p_panel7.Margin = new Padding(1);
        p_panel7.Name = "p_panel7";
        p_panel7.Size = new Size(228, 14);
        p_panel7.TabIndex = 13;
        // 
        // p_lblUser
        // 
        p_lblUser.Dock = DockStyle.Fill;
        p_lblUser.Location = new Point(100, 0);
        p_lblUser.Name = "p_lblUser";
        p_lblUser.Padding = new Padding(0, 0, 3, 0);
        p_lblUser.Size = new Size(128, 14);
        p_lblUser.TabIndex = 1;
        p_lblUser.Text = "0";
        p_lblUser.TextAlign = ContentAlignment.BottomRight;
        // 
        // p_Label07
        // 
        p_Label07.Dock = DockStyle.Left;
        p_Label07.Location = new Point(0, 0);
        p_Label07.Name = "p_Label07";
        p_Label07.Padding = new Padding(3, 0, 0, 0);
        p_Label07.Size = new Size(100, 14);
        p_Label07.TabIndex = 0;
        p_Label07.Text = "User Resources";
        p_Label07.TextAlign = ContentAlignment.BottomLeft;
        // 
        // p_panel1
        // 
        p_TableLayout1.SetColumnSpan(p_panel1, 2);
        p_panel1.Controls.Add(p_lblCpuUsage);
        p_panel1.Controls.Add(p_Label01);
        p_panel1.Dock = DockStyle.Fill;
        p_panel1.Location = new Point(2, 1);
        p_panel1.Margin = new Padding(1);
        p_panel1.Name = "p_panel1";
        p_panel1.Size = new Size(458, 18);
        p_panel1.TabIndex = 1;
        // 
        // p_lblCpuUsage
        // 
        p_lblCpuUsage.Dock = DockStyle.Fill;
        p_lblCpuUsage.Location = new Point(90, 0);
        p_lblCpuUsage.Name = "p_lblCpuUsage";
        p_lblCpuUsage.Padding = new Padding(0, 0, 3, 0);
        p_lblCpuUsage.Size = new Size(368, 18);
        p_lblCpuUsage.TabIndex = 1;
        p_lblCpuUsage.Text = "0";
        p_lblCpuUsage.TextAlign = ContentAlignment.BottomRight;
        // 
        // p_Label01
        // 
        p_Label01.Dock = DockStyle.Left;
        p_Label01.Location = new Point(0, 0);
        p_Label01.Name = "p_Label01";
        p_Label01.Padding = new Padding(3, 0, 0, 0);
        p_Label01.Size = new Size(90, 18);
        p_Label01.TabIndex = 0;
        p_Label01.Text = "CPU Usage";
        p_Label01.TextAlign = ContentAlignment.BottomLeft;
        // 
        // p_panel6
        // 
        p_panel6.Controls.Add(p_lblGDI);
        p_panel6.Controls.Add(p_Label06);
        p_panel6.Dock = DockStyle.Fill;
        p_panel6.Location = new Point(2, 329);
        p_panel6.Margin = new Padding(1);
        p_panel6.Name = "p_panel6";
        p_panel6.Size = new Size(228, 14);
        p_panel6.TabIndex = 11;
        // 
        // p_lblGDI
        // 
        p_lblGDI.Dock = DockStyle.Fill;
        p_lblGDI.Location = new Point(100, 0);
        p_lblGDI.Name = "p_lblGDI";
        p_lblGDI.Padding = new Padding(0, 0, 3, 0);
        p_lblGDI.Size = new Size(128, 14);
        p_lblGDI.TabIndex = 1;
        p_lblGDI.Text = "0";
        p_lblGDI.TextAlign = ContentAlignment.BottomRight;
        // 
        // p_Label06
        // 
        p_Label06.Dock = DockStyle.Left;
        p_Label06.Location = new Point(0, 0);
        p_Label06.Name = "p_Label06";
        p_Label06.Padding = new Padding(3, 0, 0, 0);
        p_Label06.Size = new Size(100, 14);
        p_Label06.TabIndex = 0;
        p_Label06.Text = "GDI Resources";
        p_Label06.TextAlign = ContentAlignment.BottomLeft;
        // 
        // p_panel4
        // 
        p_panel4.Controls.Add(p_lblVirtualMemory);
        p_panel4.Controls.Add(p_Label04);
        p_panel4.Dock = DockStyle.Fill;
        p_panel4.Location = new Point(2, 221);
        p_panel4.Margin = new Padding(1);
        p_panel4.Name = "p_panel4";
        p_panel4.Size = new Size(228, 14);
        p_panel4.TabIndex = 7;
        // 
        // p_lblVirtualMemory
        // 
        p_lblVirtualMemory.Dock = DockStyle.Fill;
        p_lblVirtualMemory.Location = new Point(100, 0);
        p_lblVirtualMemory.Name = "p_lblVirtualMemory";
        p_lblVirtualMemory.Padding = new Padding(0, 0, 3, 0);
        p_lblVirtualMemory.Size = new Size(128, 14);
        p_lblVirtualMemory.TabIndex = 1;
        p_lblVirtualMemory.Text = "0";
        p_lblVirtualMemory.TextAlign = ContentAlignment.BottomRight;
        // 
        // p_Label04
        // 
        p_Label04.Dock = DockStyle.Left;
        p_Label04.Location = new Point(0, 0);
        p_Label04.Name = "p_Label04";
        p_Label04.Padding = new Padding(3, 0, 0, 0);
        p_Label04.Size = new Size(100, 14);
        p_Label04.TabIndex = 0;
        p_Label04.Text = "Virtual Memory";
        p_Label04.TextAlign = ContentAlignment.BottomLeft;
        // 
        // p_ChartCPU
        // 
        p_ChartCPU.AntiAliasing = true;
        p_ChartCPU.BackColor = Color.Black;
        p_ChartCPU.BackColorShade = Color.FromArgb(0, 0, 0);
        p_ChartCPU.BackSolid = true;
        p_ChartCPU.BorderStyle = Border3DStyle.Sunken;
        p_TableLayout1.SetColumnSpan(p_ChartCPU, 2);
        p_ChartCPU.DetailsOnHover = false;
        p_ChartCPU.DisplayAverage = false;
        p_ChartCPU.DisplayIndexes = false;
        p_ChartCPU.DisplayLegends = true;
        p_ChartCPU.Dock = DockStyle.Fill;
        p_ChartCPU.GridSpacing = 10;
        p_ChartCPU.LegendSpacing = 29;
        p_ChartCPU.LightColors = false;
        p_ChartCPU.Location = new Point(3, 22);
        p_ChartCPU.Margin = new Padding(2);
        p_ChartCPU.MaxValue = 0D;
        p_ChartCPU.Name = "p_ChartCPU";
        p_ChartCPU.ScaleMode = sMkTaskManager.Controls.sMkPerfChart.ScaleModes.Absolute;
        p_ChartCPU.ShadeBackground = false;
        p_ChartCPU.Size = new Size(456, 88);
        p_ChartCPU.TabIndex = 2;
        p_ChartCPU.ValueSpacing = 2;
        p_ChartCPU.ValuesSuffix = "";
        // 
        // p_ChartWS
        // 
        p_ChartWS.AntiAliasing = true;
        p_ChartWS.BackColor = Color.Black;
        p_ChartWS.BackColorShade = Color.FromArgb(0, 0, 0);
        p_ChartWS.BackSolid = true;
        p_ChartWS.BorderStyle = Border3DStyle.Sunken;
        p_ChartWS.DetailsOnHover = false;
        p_ChartWS.DisplayAverage = false;
        p_ChartWS.DisplayIndexes = false;
        p_ChartWS.DisplayLegends = true;
        p_ChartWS.Dock = DockStyle.Fill;
        p_ChartWS.GridSpacing = 10;
        p_ChartWS.LegendSpacing = 17;
        p_ChartWS.LightColors = false;
        p_ChartWS.Location = new Point(3, 130);
        p_ChartWS.Margin = new Padding(2);
        p_ChartWS.MaxValue = 0D;
        p_ChartWS.Name = "p_ChartWS";
        p_ChartWS.ScaleMode = sMkTaskManager.Controls.sMkPerfChart.ScaleModes.Relative;
        p_ChartWS.ShadeBackground = false;
        p_ChartWS.Size = new Size(226, 88);
        p_ChartWS.TabIndex = 4;
        p_ChartWS.ValueSpacing = 2;
        p_ChartWS.ValuesSuffix = "K";
        // 
        // p_ChartPB
        // 
        p_ChartPB.AntiAliasing = true;
        p_ChartPB.BackColor = Color.Black;
        p_ChartPB.BackColorShade = Color.FromArgb(0, 0, 0);
        p_ChartPB.BackSolid = true;
        p_ChartPB.BorderStyle = Border3DStyle.Sunken;
        p_ChartPB.DetailsOnHover = false;
        p_ChartPB.DisplayAverage = false;
        p_ChartPB.DisplayIndexes = false;
        p_ChartPB.DisplayLegends = true;
        p_ChartPB.Dock = DockStyle.Fill;
        p_ChartPB.GridSpacing = 10;
        p_ChartPB.LegendSpacing = 17;
        p_ChartPB.LightColors = false;
        p_ChartPB.Location = new Point(233, 130);
        p_ChartPB.Margin = new Padding(2);
        p_ChartPB.MaxValue = 0D;
        p_ChartPB.Name = "p_ChartPB";
        p_ChartPB.ScaleMode = sMkTaskManager.Controls.sMkPerfChart.ScaleModes.Relative;
        p_ChartPB.ShadeBackground = false;
        p_ChartPB.Size = new Size(226, 88);
        p_ChartPB.TabIndex = 6;
        p_ChartPB.ValueSpacing = 2;
        p_ChartPB.ValuesSuffix = "K";
        // 
        // p_ChartVM
        // 
        p_ChartVM.AntiAliasing = true;
        p_ChartVM.BackColor = Color.Black;
        p_ChartVM.BackColorShade = Color.FromArgb(0, 0, 0);
        p_ChartVM.BackSolid = true;
        p_ChartVM.BorderStyle = Border3DStyle.Sunken;
        p_ChartVM.DetailsOnHover = false;
        p_ChartVM.DisplayAverage = false;
        p_ChartVM.DisplayIndexes = false;
        p_ChartVM.DisplayLegends = true;
        p_ChartVM.Dock = DockStyle.Fill;
        p_ChartVM.GridSpacing = 10;
        p_ChartVM.LegendSpacing = 17;
        p_ChartVM.LightColors = false;
        p_ChartVM.Location = new Point(3, 238);
        p_ChartVM.Margin = new Padding(2);
        p_ChartVM.MaxValue = 0D;
        p_ChartVM.Name = "p_ChartVM";
        p_ChartVM.ScaleMode = sMkTaskManager.Controls.sMkPerfChart.ScaleModes.Relative;
        p_ChartVM.ShadeBackground = false;
        p_ChartVM.Size = new Size(226, 88);
        p_ChartVM.TabIndex = 8;
        p_ChartVM.ValueSpacing = 2;
        p_ChartVM.ValuesSuffix = "K";
        // 
        // p_ChartPF
        // 
        p_ChartPF.AntiAliasing = true;
        p_ChartPF.BackColor = Color.Black;
        p_ChartPF.BackColorShade = Color.FromArgb(0, 0, 0);
        p_ChartPF.BackSolid = true;
        p_ChartPF.BorderStyle = Border3DStyle.Sunken;
        p_ChartPF.DetailsOnHover = false;
        p_ChartPF.DisplayAverage = false;
        p_ChartPF.DisplayIndexes = false;
        p_ChartPF.DisplayLegends = true;
        p_ChartPF.Dock = DockStyle.Fill;
        p_ChartPF.GridSpacing = 10;
        p_ChartPF.LegendSpacing = 17;
        p_ChartPF.LightColors = false;
        p_ChartPF.Location = new Point(233, 238);
        p_ChartPF.Margin = new Padding(2);
        p_ChartPF.MaxValue = 0D;
        p_ChartPF.Name = "p_ChartPF";
        p_ChartPF.ScaleMode = sMkTaskManager.Controls.sMkPerfChart.ScaleModes.Relative;
        p_ChartPF.ShadeBackground = false;
        p_ChartPF.Size = new Size(226, 88);
        p_ChartPF.TabIndex = 10;
        p_ChartPF.ValueSpacing = 2;
        p_ChartPF.ValuesSuffix = "K";
        // 
        // p_ChartUser
        // 
        p_ChartUser.AntiAliasing = true;
        p_ChartUser.BackColor = Color.Black;
        p_ChartUser.BackColorShade = Color.FromArgb(0, 0, 0);
        p_ChartUser.BackSolid = true;
        p_ChartUser.BorderStyle = Border3DStyle.Sunken;
        p_ChartUser.DetailsOnHover = false;
        p_ChartUser.DisplayAverage = false;
        p_ChartUser.DisplayIndexes = false;
        p_ChartUser.DisplayLegends = true;
        p_ChartUser.Dock = DockStyle.Fill;
        p_ChartUser.GridSpacing = 10;
        p_ChartUser.LegendSpacing = 17;
        p_ChartUser.LightColors = false;
        p_ChartUser.Location = new Point(233, 346);
        p_ChartUser.Margin = new Padding(2);
        p_ChartUser.MaxValue = 0D;
        p_ChartUser.Name = "p_ChartUser";
        p_ChartUser.ScaleMode = sMkTaskManager.Controls.sMkPerfChart.ScaleModes.Relative;
        p_ChartUser.ShadeBackground = false;
        p_ChartUser.Size = new Size(226, 88);
        p_ChartUser.TabIndex = 14;
        p_ChartUser.ValueSpacing = 2;
        p_ChartUser.ValuesSuffix = "K";
        // 
        // tpIO
        // 
        tpIO.Controls.Add(i_TableLayout1);
        tpIO.Location = new Point(4, 24);
        tpIO.Name = "tpIO";
        tpIO.Size = new Size(463, 436);
        tpIO.TabIndex = 2;
        tpIO.Text = "I/O Usage";
        tpIO.UseVisualStyleBackColor = true;
        // 
        // i_TableLayout1
        // 
        i_TableLayout1.ColumnCount = 2;
        i_TableLayout1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 78F));
        i_TableLayout1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        i_TableLayout1.Controls.Add(i_ChartOthers, 1, 5);
        i_TableLayout1.Controls.Add(i_ChartWrites, 1, 3);
        i_TableLayout1.Controls.Add(i_ChartReads, 1, 1);
        i_TableLayout1.Controls.Add(i_Label06, 1, 4);
        i_TableLayout1.Controls.Add(i_Label05, 0, 4);
        i_TableLayout1.Controls.Add(i_Label04, 1, 2);
        i_TableLayout1.Controls.Add(i_Label03, 0, 2);
        i_TableLayout1.Controls.Add(i_Label02, 1, 0);
        i_TableLayout1.Controls.Add(i_Label01, 0, 0);
        i_TableLayout1.Controls.Add(i_PerfReads, 0, 1);
        i_TableLayout1.Controls.Add(i_PerfWrites, 0, 3);
        i_TableLayout1.Controls.Add(i_PerfOthers, 0, 5);
        i_TableLayout1.Controls.Add(i_TableLayout2, 0, 6);
        i_TableLayout1.Dock = DockStyle.Fill;
        i_TableLayout1.Location = new Point(0, 0);
        i_TableLayout1.Margin = new Padding(0);
        i_TableLayout1.Name = "i_TableLayout1";
        i_TableLayout1.Padding = new Padding(0, 0, 2, 0);
        i_TableLayout1.RowCount = 7;
        i_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        i_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        i_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
        i_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        i_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
        i_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
        i_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Absolute, 96F));
        i_TableLayout1.Size = new Size(463, 436);
        i_TableLayout1.TabIndex = 0;
        // 
        // i_ChartOthers
        // 
        i_ChartOthers.AntiAliasing = true;
        i_ChartOthers.BackColor = Color.Black;
        i_ChartOthers.BackColorShade = Color.FromArgb(0, 0, 0);
        i_ChartOthers.BackSolid = true;
        i_ChartOthers.BorderStyle = Border3DStyle.Sunken;
        i_ChartOthers.DetailsOnHover = false;
        i_ChartOthers.DisplayAverage = false;
        i_ChartOthers.DisplayIndexes = false;
        i_ChartOthers.DisplayLegends = true;
        i_ChartOthers.Dock = DockStyle.Fill;
        i_ChartOthers.GridSpacing = 10;
        i_ChartOthers.LegendSpacing = 17;
        i_ChartOthers.LightColors = false;
        i_ChartOthers.Location = new Point(80, 246);
        i_ChartOthers.Margin = new Padding(2);
        i_ChartOthers.MaxValue = 0D;
        i_ChartOthers.Name = "i_ChartOthers";
        i_ChartOthers.ScaleMode = sMkTaskManager.Controls.sMkPerfChart.ScaleModes.Relative;
        i_ChartOthers.ShadeBackground = false;
        i_ChartOthers.Size = new Size(379, 92);
        i_ChartOthers.TabIndex = 11;
        i_ChartOthers.ValueSpacing = 2;
        i_ChartOthers.ValuesSuffix = "K";
        // 
        // i_ChartWrites
        // 
        i_ChartWrites.AntiAliasing = true;
        i_ChartWrites.BackColor = Color.Black;
        i_ChartWrites.BackColorShade = Color.FromArgb(0, 0, 0);
        i_ChartWrites.BackSolid = true;
        i_ChartWrites.BorderStyle = Border3DStyle.Sunken;
        i_ChartWrites.DetailsOnHover = false;
        i_ChartWrites.DisplayAverage = false;
        i_ChartWrites.DisplayIndexes = false;
        i_ChartWrites.DisplayLegends = true;
        i_ChartWrites.Dock = DockStyle.Fill;
        i_ChartWrites.GridSpacing = 10;
        i_ChartWrites.LegendSpacing = 17;
        i_ChartWrites.LightColors = false;
        i_ChartWrites.Location = new Point(80, 134);
        i_ChartWrites.Margin = new Padding(2);
        i_ChartWrites.MaxValue = 0D;
        i_ChartWrites.Name = "i_ChartWrites";
        i_ChartWrites.ScaleMode = sMkTaskManager.Controls.sMkPerfChart.ScaleModes.Relative;
        i_ChartWrites.ShadeBackground = false;
        i_ChartWrites.Size = new Size(379, 92);
        i_ChartWrites.TabIndex = 7;
        i_ChartWrites.ValueSpacing = 2;
        i_ChartWrites.ValuesSuffix = "K";
        // 
        // i_ChartReads
        // 
        i_ChartReads.AntiAliasing = true;
        i_ChartReads.BackColor = Color.Black;
        i_ChartReads.BackColorShade = Color.FromArgb(0, 0, 0);
        i_ChartReads.BackSolid = true;
        i_ChartReads.BorderStyle = Border3DStyle.Sunken;
        i_ChartReads.DetailsOnHover = false;
        i_ChartReads.DisplayAverage = false;
        i_ChartReads.DisplayIndexes = false;
        i_ChartReads.DisplayLegends = true;
        i_ChartReads.Dock = DockStyle.Fill;
        i_ChartReads.GridSpacing = 10;
        i_ChartReads.LegendSpacing = 17;
        i_ChartReads.LightColors = false;
        i_ChartReads.Location = new Point(80, 22);
        i_ChartReads.Margin = new Padding(2);
        i_ChartReads.MaxValue = 0D;
        i_ChartReads.Name = "i_ChartReads";
        i_ChartReads.ScaleMode = sMkTaskManager.Controls.sMkPerfChart.ScaleModes.Relative;
        i_ChartReads.ShadeBackground = false;
        i_ChartReads.Size = new Size(379, 92);
        i_ChartReads.TabIndex = 3;
        i_ChartReads.ValueSpacing = 2;
        i_ChartReads.ValuesSuffix = "K";
        // 
        // i_Label06
        // 
        i_Label06.Dock = DockStyle.Fill;
        i_Label06.Location = new Point(81, 228);
        i_Label06.Name = "i_Label06";
        i_Label06.Size = new Size(377, 16);
        i_Label06.TabIndex = 9;
        i_Label06.Text = "Others Bytes Rate";
        i_Label06.TextAlign = ContentAlignment.BottomLeft;
        // 
        // i_Label05
        // 
        i_Label05.Dock = DockStyle.Fill;
        i_Label05.Location = new Point(0, 228);
        i_Label05.Margin = new Padding(0);
        i_Label05.Name = "i_Label05";
        i_Label05.Size = new Size(78, 16);
        i_Label05.TabIndex = 8;
        i_Label05.Text = "Others Delta";
        i_Label05.TextAlign = ContentAlignment.BottomCenter;
        // 
        // i_Label04
        // 
        i_Label04.Dock = DockStyle.Fill;
        i_Label04.Location = new Point(81, 116);
        i_Label04.Name = "i_Label04";
        i_Label04.Size = new Size(377, 16);
        i_Label04.TabIndex = 5;
        i_Label04.Text = "Writes Bytes Rate";
        i_Label04.TextAlign = ContentAlignment.BottomLeft;
        // 
        // i_Label03
        // 
        i_Label03.Dock = DockStyle.Fill;
        i_Label03.Location = new Point(0, 116);
        i_Label03.Margin = new Padding(0);
        i_Label03.Name = "i_Label03";
        i_Label03.Size = new Size(78, 16);
        i_Label03.TabIndex = 4;
        i_Label03.Text = "Writes Delta";
        i_Label03.TextAlign = ContentAlignment.BottomCenter;
        // 
        // i_Label02
        // 
        i_Label02.Dock = DockStyle.Fill;
        i_Label02.Location = new Point(81, 0);
        i_Label02.Name = "i_Label02";
        i_Label02.Size = new Size(377, 20);
        i_Label02.TabIndex = 1;
        i_Label02.Text = "Reads Bytes Rate";
        i_Label02.TextAlign = ContentAlignment.BottomLeft;
        // 
        // i_Label01
        // 
        i_Label01.Dock = DockStyle.Fill;
        i_Label01.Location = new Point(0, 0);
        i_Label01.Margin = new Padding(0);
        i_Label01.Name = "i_Label01";
        i_Label01.Size = new Size(78, 20);
        i_Label01.TabIndex = 0;
        i_Label01.Text = "Reads Delta";
        i_Label01.TextAlign = ContentAlignment.BottomCenter;
        // 
        // i_PerfReads
        // 
        i_PerfReads.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
        i_PerfReads.BackColor = Color.Black;
        i_PerfReads.BarBackColor = Color.DarkGreen;
        i_PerfReads.BarForeColor = Color.Lime;
        i_PerfReads.BorderStyle = Border3DStyle.Sunken;
        i_PerfReads.HistoryValues = 10;
        i_PerfReads.LightColors = false;
        i_PerfReads.Location = new Point(3, 22);
        i_PerfReads.Margin = new Padding(0, 2, 2, 2);
        i_PerfReads.Name = "i_PerfReads";
        i_PerfReads.ScaleMode = sMkTaskManager.Controls.sMkPerfMeter.ScaleModes.Relative;
        i_PerfReads.Size = new Size(70, 92);
        i_PerfReads.TabIndex = 2;
        // 
        // i_PerfWrites
        // 
        i_PerfWrites.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
        i_PerfWrites.BackColor = Color.Black;
        i_PerfWrites.BarBackColor = Color.DarkRed;
        i_PerfWrites.BarForeColor = Color.LightCoral;
        i_PerfWrites.BorderStyle = Border3DStyle.Sunken;
        i_PerfWrites.HistoryValues = 10;
        i_PerfWrites.LightColors = false;
        i_PerfWrites.Location = new Point(3, 134);
        i_PerfWrites.Margin = new Padding(0, 2, 2, 2);
        i_PerfWrites.Name = "i_PerfWrites";
        i_PerfWrites.ScaleMode = sMkTaskManager.Controls.sMkPerfMeter.ScaleModes.Relative;
        i_PerfWrites.Size = new Size(70, 92);
        i_PerfWrites.TabIndex = 6;
        // 
        // i_PerfOthers
        // 
        i_PerfOthers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
        i_PerfOthers.BackColor = Color.Black;
        i_PerfOthers.BarBackColor = Color.MidnightBlue;
        i_PerfOthers.BarForeColor = Color.DodgerBlue;
        i_PerfOthers.BorderStyle = Border3DStyle.Sunken;
        i_PerfOthers.HistoryValues = 10;
        i_PerfOthers.LightColors = false;
        i_PerfOthers.Location = new Point(3, 246);
        i_PerfOthers.Margin = new Padding(0, 2, 2, 2);
        i_PerfOthers.Name = "i_PerfOthers";
        i_PerfOthers.ScaleMode = sMkTaskManager.Controls.sMkPerfMeter.ScaleModes.Relative;
        i_PerfOthers.Size = new Size(70, 92);
        i_PerfOthers.TabIndex = 10;
        // 
        // i_TableLayout2
        // 
        i_TableLayout2.ColumnCount = 3;
        i_TableLayout1.SetColumnSpan(i_TableLayout2, 2);
        i_TableLayout2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        i_TableLayout2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        i_TableLayout2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        i_TableLayout2.Controls.Add(i_GroupBox3, 0, 0);
        i_TableLayout2.Controls.Add(i_GroupBox2, 0, 0);
        i_TableLayout2.Controls.Add(i_GroupBox1, 0, 0);
        i_TableLayout2.Dock = DockStyle.Fill;
        i_TableLayout2.Location = new Point(0, 340);
        i_TableLayout2.Margin = new Padding(0);
        i_TableLayout2.Name = "i_TableLayout2";
        i_TableLayout2.RowCount = 1;
        i_TableLayout2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        i_TableLayout2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        i_TableLayout2.Size = new Size(461, 96);
        i_TableLayout2.TabIndex = 12;
        // 
        // i_GroupBox3
        // 
        i_GroupBox3.Controls.Add(i_lblOtherBytesDelta);
        i_GroupBox3.Controls.Add(i_lblOtherBytes);
        i_GroupBox3.Controls.Add(i_lblOtherCountDelta);
        i_GroupBox3.Controls.Add(i_lblOtherCount);
        i_GroupBox3.Controls.Add(i_Label34);
        i_GroupBox3.Controls.Add(i_Label33);
        i_GroupBox3.Controls.Add(i_Label32);
        i_GroupBox3.Controls.Add(i_Label31);
        i_GroupBox3.Dock = DockStyle.Fill;
        i_GroupBox3.Location = new Point(309, 3);
        i_GroupBox3.Name = "i_GroupBox3";
        i_GroupBox3.Size = new Size(149, 90);
        i_GroupBox3.TabIndex = 2;
        i_GroupBox3.TabStop = false;
        i_GroupBox3.Text = "Other Operations";
        // 
        // i_lblOtherBytesDelta
        // 
        i_lblOtherBytesDelta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        i_lblOtherBytesDelta.Location = new Point(73, 69);
        i_lblOtherBytesDelta.Margin = new Padding(3, 0, 3, 1);
        i_lblOtherBytesDelta.Name = "i_lblOtherBytesDelta";
        i_lblOtherBytesDelta.Size = new Size(72, 16);
        i_lblOtherBytesDelta.TabIndex = 7;
        i_lblOtherBytesDelta.Text = "0";
        i_lblOtherBytesDelta.TextAlign = ContentAlignment.MiddleRight;
        // 
        // i_lblOtherBytes
        // 
        i_lblOtherBytes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        i_lblOtherBytes.Location = new Point(48, 54);
        i_lblOtherBytes.Margin = new Padding(3, 0, 3, 1);
        i_lblOtherBytes.Name = "i_lblOtherBytes";
        i_lblOtherBytes.Size = new Size(97, 16);
        i_lblOtherBytes.TabIndex = 5;
        i_lblOtherBytes.Text = "0";
        i_lblOtherBytes.TextAlign = ContentAlignment.MiddleRight;
        // 
        // i_lblOtherCountDelta
        // 
        i_lblOtherCountDelta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        i_lblOtherCountDelta.Location = new Point(77, 34);
        i_lblOtherCountDelta.Margin = new Padding(3, 0, 3, 1);
        i_lblOtherCountDelta.Name = "i_lblOtherCountDelta";
        i_lblOtherCountDelta.Size = new Size(68, 16);
        i_lblOtherCountDelta.TabIndex = 3;
        i_lblOtherCountDelta.Text = "0";
        i_lblOtherCountDelta.TextAlign = ContentAlignment.MiddleRight;
        // 
        // i_lblOtherCount
        // 
        i_lblOtherCount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        i_lblOtherCount.Location = new Point(48, 19);
        i_lblOtherCount.Margin = new Padding(3, 0, 3, 1);
        i_lblOtherCount.Name = "i_lblOtherCount";
        i_lblOtherCount.Size = new Size(97, 16);
        i_lblOtherCount.TabIndex = 1;
        i_lblOtherCount.Text = "0";
        i_lblOtherCount.TextAlign = ContentAlignment.MiddleRight;
        // 
        // i_Label34
        // 
        i_Label34.Location = new Point(2, 69);
        i_Label34.Margin = new Padding(3, 0, 3, 1);
        i_Label34.Name = "i_Label34";
        i_Label34.Size = new Size(70, 16);
        i_Label34.TabIndex = 6;
        i_Label34.Text = "Bytes Delta:";
        i_Label34.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // i_Label33
        // 
        i_Label33.Location = new Point(2, 54);
        i_Label33.Margin = new Padding(3, 0, 3, 1);
        i_Label33.Name = "i_Label33";
        i_Label33.Size = new Size(50, 16);
        i_Label33.TabIndex = 4;
        i_Label33.Text = "Bytes:";
        i_Label33.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // i_Label32
        // 
        i_Label32.Location = new Point(2, 34);
        i_Label32.Margin = new Padding(3, 0, 3, 1);
        i_Label32.Name = "i_Label32";
        i_Label32.Size = new Size(75, 16);
        i_Label32.TabIndex = 2;
        i_Label32.Text = "Count Delta:";
        i_Label32.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // i_Label31
        // 
        i_Label31.Location = new Point(2, 19);
        i_Label31.Margin = new Padding(3, 0, 3, 1);
        i_Label31.Name = "i_Label31";
        i_Label31.Size = new Size(50, 16);
        i_Label31.TabIndex = 0;
        i_Label31.Text = "Count:";
        i_Label31.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // i_GroupBox2
        // 
        i_GroupBox2.Controls.Add(i_lblWriteBytesDelta);
        i_GroupBox2.Controls.Add(i_lblWriteBytes);
        i_GroupBox2.Controls.Add(i_lblWriteCountDelta);
        i_GroupBox2.Controls.Add(i_lblWriteCount);
        i_GroupBox2.Controls.Add(i_Label24);
        i_GroupBox2.Controls.Add(i_Label23);
        i_GroupBox2.Controls.Add(i_Label22);
        i_GroupBox2.Controls.Add(i_Label21);
        i_GroupBox2.Dock = DockStyle.Fill;
        i_GroupBox2.Location = new Point(156, 3);
        i_GroupBox2.Name = "i_GroupBox2";
        i_GroupBox2.Size = new Size(147, 90);
        i_GroupBox2.TabIndex = 1;
        i_GroupBox2.TabStop = false;
        i_GroupBox2.Text = "Write Operations";
        // 
        // i_lblWriteBytesDelta
        // 
        i_lblWriteBytesDelta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        i_lblWriteBytesDelta.Location = new Point(73, 69);
        i_lblWriteBytesDelta.Margin = new Padding(3, 0, 3, 1);
        i_lblWriteBytesDelta.Name = "i_lblWriteBytesDelta";
        i_lblWriteBytesDelta.Size = new Size(71, 16);
        i_lblWriteBytesDelta.TabIndex = 7;
        i_lblWriteBytesDelta.Text = "0";
        i_lblWriteBytesDelta.TextAlign = ContentAlignment.MiddleRight;
        // 
        // i_lblWriteBytes
        // 
        i_lblWriteBytes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        i_lblWriteBytes.Location = new Point(48, 54);
        i_lblWriteBytes.Margin = new Padding(3, 0, 3, 1);
        i_lblWriteBytes.Name = "i_lblWriteBytes";
        i_lblWriteBytes.Size = new Size(96, 16);
        i_lblWriteBytes.TabIndex = 5;
        i_lblWriteBytes.Text = "0";
        i_lblWriteBytes.TextAlign = ContentAlignment.MiddleRight;
        // 
        // i_lblWriteCountDelta
        // 
        i_lblWriteCountDelta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        i_lblWriteCountDelta.Location = new Point(77, 34);
        i_lblWriteCountDelta.Margin = new Padding(3, 0, 3, 1);
        i_lblWriteCountDelta.Name = "i_lblWriteCountDelta";
        i_lblWriteCountDelta.Size = new Size(67, 16);
        i_lblWriteCountDelta.TabIndex = 3;
        i_lblWriteCountDelta.Text = "0";
        i_lblWriteCountDelta.TextAlign = ContentAlignment.MiddleRight;
        // 
        // i_lblWriteCount
        // 
        i_lblWriteCount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        i_lblWriteCount.Location = new Point(48, 19);
        i_lblWriteCount.Margin = new Padding(3, 0, 3, 1);
        i_lblWriteCount.Name = "i_lblWriteCount";
        i_lblWriteCount.Size = new Size(96, 16);
        i_lblWriteCount.TabIndex = 1;
        i_lblWriteCount.Text = "0";
        i_lblWriteCount.TextAlign = ContentAlignment.MiddleRight;
        // 
        // i_Label24
        // 
        i_Label24.Location = new Point(2, 69);
        i_Label24.Margin = new Padding(3, 0, 3, 1);
        i_Label24.Name = "i_Label24";
        i_Label24.Size = new Size(70, 16);
        i_Label24.TabIndex = 6;
        i_Label24.Text = "Bytes Delta:";
        i_Label24.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // i_Label23
        // 
        i_Label23.Location = new Point(2, 54);
        i_Label23.Margin = new Padding(3, 0, 3, 1);
        i_Label23.Name = "i_Label23";
        i_Label23.Size = new Size(50, 16);
        i_Label23.TabIndex = 4;
        i_Label23.Text = "Bytes:";
        i_Label23.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // i_Label22
        // 
        i_Label22.Location = new Point(2, 34);
        i_Label22.Margin = new Padding(3, 0, 3, 1);
        i_Label22.Name = "i_Label22";
        i_Label22.Size = new Size(75, 16);
        i_Label22.TabIndex = 2;
        i_Label22.Text = "Count Delta:";
        i_Label22.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // i_Label21
        // 
        i_Label21.Location = new Point(2, 19);
        i_Label21.Margin = new Padding(3, 0, 3, 1);
        i_Label21.Name = "i_Label21";
        i_Label21.Size = new Size(50, 16);
        i_Label21.TabIndex = 0;
        i_Label21.Text = "Count:";
        i_Label21.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // i_GroupBox1
        // 
        i_GroupBox1.Controls.Add(i_lblReadBytesDelta);
        i_GroupBox1.Controls.Add(i_lblReadBytes);
        i_GroupBox1.Controls.Add(i_lblReadCountDelta);
        i_GroupBox1.Controls.Add(i_lblReadCount);
        i_GroupBox1.Controls.Add(i_Label14);
        i_GroupBox1.Controls.Add(i_Label13);
        i_GroupBox1.Controls.Add(i_Label12);
        i_GroupBox1.Controls.Add(i_Label11);
        i_GroupBox1.Dock = DockStyle.Fill;
        i_GroupBox1.Location = new Point(3, 3);
        i_GroupBox1.Name = "i_GroupBox1";
        i_GroupBox1.Size = new Size(147, 90);
        i_GroupBox1.TabIndex = 0;
        i_GroupBox1.TabStop = false;
        i_GroupBox1.Text = "Read Operations";
        // 
        // i_lblReadBytesDelta
        // 
        i_lblReadBytesDelta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        i_lblReadBytesDelta.Location = new Point(73, 69);
        i_lblReadBytesDelta.Margin = new Padding(3, 0, 3, 1);
        i_lblReadBytesDelta.Name = "i_lblReadBytesDelta";
        i_lblReadBytesDelta.Size = new Size(71, 16);
        i_lblReadBytesDelta.TabIndex = 7;
        i_lblReadBytesDelta.Text = "0";
        i_lblReadBytesDelta.TextAlign = ContentAlignment.MiddleRight;
        // 
        // i_lblReadBytes
        // 
        i_lblReadBytes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        i_lblReadBytes.Location = new Point(48, 54);
        i_lblReadBytes.Margin = new Padding(3, 0, 3, 1);
        i_lblReadBytes.Name = "i_lblReadBytes";
        i_lblReadBytes.Size = new Size(96, 16);
        i_lblReadBytes.TabIndex = 5;
        i_lblReadBytes.Text = "0";
        i_lblReadBytes.TextAlign = ContentAlignment.MiddleRight;
        // 
        // i_lblReadCountDelta
        // 
        i_lblReadCountDelta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        i_lblReadCountDelta.Location = new Point(77, 34);
        i_lblReadCountDelta.Margin = new Padding(3, 0, 3, 1);
        i_lblReadCountDelta.Name = "i_lblReadCountDelta";
        i_lblReadCountDelta.Size = new Size(67, 16);
        i_lblReadCountDelta.TabIndex = 3;
        i_lblReadCountDelta.Text = "0";
        i_lblReadCountDelta.TextAlign = ContentAlignment.MiddleRight;
        // 
        // i_lblReadCount
        // 
        i_lblReadCount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        i_lblReadCount.Location = new Point(48, 19);
        i_lblReadCount.Margin = new Padding(3, 0, 3, 1);
        i_lblReadCount.Name = "i_lblReadCount";
        i_lblReadCount.Size = new Size(96, 16);
        i_lblReadCount.TabIndex = 1;
        i_lblReadCount.Text = "0";
        i_lblReadCount.TextAlign = ContentAlignment.MiddleRight;
        // 
        // i_Label14
        // 
        i_Label14.Location = new Point(2, 69);
        i_Label14.Margin = new Padding(3, 0, 3, 1);
        i_Label14.Name = "i_Label14";
        i_Label14.Size = new Size(70, 16);
        i_Label14.TabIndex = 6;
        i_Label14.Text = "Bytes Delta:";
        i_Label14.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // i_Label13
        // 
        i_Label13.Location = new Point(2, 54);
        i_Label13.Margin = new Padding(3, 0, 3, 1);
        i_Label13.Name = "i_Label13";
        i_Label13.Size = new Size(50, 16);
        i_Label13.TabIndex = 4;
        i_Label13.Text = "Bytes:";
        i_Label13.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // i_Label12
        // 
        i_Label12.Location = new Point(2, 34);
        i_Label12.Margin = new Padding(3, 0, 3, 1);
        i_Label12.Name = "i_Label12";
        i_Label12.Size = new Size(75, 16);
        i_Label12.TabIndex = 2;
        i_Label12.Text = "Count Delta:";
        i_Label12.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // i_Label11
        // 
        i_Label11.Location = new Point(2, 19);
        i_Label11.Margin = new Padding(3, 0, 3, 1);
        i_Label11.Name = "i_Label11";
        i_Label11.Size = new Size(50, 16);
        i_Label11.TabIndex = 0;
        i_Label11.Text = "Count:";
        i_Label11.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // tpDiskNet
        // 
        tpDiskNet.Controls.Add(d_TableLayout1);
        tpDiskNet.Location = new Point(4, 24);
        tpDiskNet.Name = "tpDiskNet";
        tpDiskNet.Size = new Size(463, 436);
        tpDiskNet.TabIndex = 3;
        tpDiskNet.Text = "Disk & Network";
        tpDiskNet.UseVisualStyleBackColor = true;
        // 
        // d_TableLayout1
        // 
        d_TableLayout1.ColumnCount = 2;
        d_TableLayout1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 78F));
        d_TableLayout1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        d_TableLayout1.Controls.Add(d_ChartNet, 1, 3);
        d_TableLayout1.Controls.Add(d_ChartDisk, 1, 1);
        d_TableLayout1.Controls.Add(d_Label04, 1, 2);
        d_TableLayout1.Controls.Add(d_Label03, 0, 2);
        d_TableLayout1.Controls.Add(d_PerfDisk, 0, 1);
        d_TableLayout1.Controls.Add(d_PerfNet, 0, 3);
        d_TableLayout1.Controls.Add(d_Label02, 1, 0);
        d_TableLayout1.Controls.Add(d_Label01, 0, 0);
        d_TableLayout1.Controls.Add(d_TableLayout2, 0, 4);
        d_TableLayout1.Dock = DockStyle.Fill;
        d_TableLayout1.Location = new Point(0, 0);
        d_TableLayout1.Margin = new Padding(0);
        d_TableLayout1.Name = "d_TableLayout1";
        d_TableLayout1.Padding = new Padding(0, 0, 2, 0);
        d_TableLayout1.RowCount = 5;
        d_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        d_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        d_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
        d_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        d_TableLayout1.RowStyles.Add(new RowStyle(SizeType.Absolute, 130F));
        d_TableLayout1.Size = new Size(463, 436);
        d_TableLayout1.TabIndex = 0;
        // 
        // d_ChartNet
        // 
        d_ChartNet.AntiAliasing = true;
        d_ChartNet.BackColor = Color.Black;
        d_ChartNet.BackColorShade = Color.FromArgb(0, 0, 0);
        d_ChartNet.BackSolid = true;
        d_ChartNet.BorderStyle = Border3DStyle.Sunken;
        d_ChartNet.DetailsOnHover = false;
        d_ChartNet.DisplayAverage = false;
        d_ChartNet.DisplayIndexes = false;
        d_ChartNet.DisplayLegends = true;
        d_ChartNet.Dock = DockStyle.Fill;
        d_ChartNet.GridSpacing = 10;
        d_ChartNet.LegendSpacing = 17;
        d_ChartNet.LightColors = false;
        d_ChartNet.Location = new Point(80, 173);
        d_ChartNet.Margin = new Padding(2);
        d_ChartNet.MaxValue = 0D;
        d_ChartNet.Name = "d_ChartNet";
        d_ChartNet.ScaleMode = sMkTaskManager.Controls.sMkPerfChart.ScaleModes.Relative;
        d_ChartNet.ShadeBackground = false;
        d_ChartNet.Size = new Size(379, 131);
        d_ChartNet.TabIndex = 7;
        d_ChartNet.ValueSpacing = 2;
        d_ChartNet.ValuesSuffix = "K";
        // 
        // d_ChartDisk
        // 
        d_ChartDisk.AntiAliasing = true;
        d_ChartDisk.BackColor = Color.Black;
        d_ChartDisk.BackColorShade = Color.FromArgb(0, 0, 0);
        d_ChartDisk.BackSolid = true;
        d_ChartDisk.BorderStyle = Border3DStyle.Sunken;
        d_ChartDisk.DetailsOnHover = false;
        d_ChartDisk.DisplayAverage = false;
        d_ChartDisk.DisplayIndexes = false;
        d_ChartDisk.DisplayLegends = true;
        d_ChartDisk.Dock = DockStyle.Fill;
        d_ChartDisk.GridSpacing = 10;
        d_ChartDisk.LegendSpacing = 17;
        d_ChartDisk.LightColors = false;
        d_ChartDisk.Location = new Point(80, 22);
        d_ChartDisk.Margin = new Padding(2);
        d_ChartDisk.MaxValue = 0D;
        d_ChartDisk.Name = "d_ChartDisk";
        d_ChartDisk.ScaleMode = sMkTaskManager.Controls.sMkPerfChart.ScaleModes.Relative;
        d_ChartDisk.ShadeBackground = false;
        d_ChartDisk.Size = new Size(379, 131);
        d_ChartDisk.TabIndex = 3;
        d_ChartDisk.ValueSpacing = 2;
        d_ChartDisk.ValuesSuffix = "K";
        // 
        // d_Label04
        // 
        d_Label04.Dock = DockStyle.Fill;
        d_Label04.Location = new Point(81, 155);
        d_Label04.Name = "d_Label04";
        d_Label04.Size = new Size(377, 16);
        d_Label04.TabIndex = 5;
        d_Label04.Text = "Network Usage History";
        d_Label04.TextAlign = ContentAlignment.BottomLeft;
        // 
        // d_Label03
        // 
        d_Label03.Dock = DockStyle.Fill;
        d_Label03.Location = new Point(0, 155);
        d_Label03.Margin = new Padding(0);
        d_Label03.Name = "d_Label03";
        d_Label03.Size = new Size(78, 16);
        d_Label03.TabIndex = 4;
        d_Label03.Text = "Net Delta";
        d_Label03.TextAlign = ContentAlignment.BottomCenter;
        // 
        // d_PerfDisk
        // 
        d_PerfDisk.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
        d_PerfDisk.BackColor = Color.Black;
        d_PerfDisk.BarBackColor = Color.Sienna;
        d_PerfDisk.BarForeColor = Color.SandyBrown;
        d_PerfDisk.BorderStyle = Border3DStyle.Sunken;
        d_PerfDisk.HistoryValues = 10;
        d_PerfDisk.LightColors = false;
        d_PerfDisk.Location = new Point(3, 22);
        d_PerfDisk.Margin = new Padding(0, 2, 2, 2);
        d_PerfDisk.Name = "d_PerfDisk";
        d_PerfDisk.ScaleMode = sMkTaskManager.Controls.sMkPerfMeter.ScaleModes.Relative;
        d_PerfDisk.Size = new Size(70, 131);
        d_PerfDisk.TabIndex = 2;
        // 
        // d_PerfNet
        // 
        d_PerfNet.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
        d_PerfNet.BackColor = Color.Black;
        d_PerfNet.BarBackColor = Color.Indigo;
        d_PerfNet.BarForeColor = Color.Violet;
        d_PerfNet.BorderStyle = Border3DStyle.Sunken;
        d_PerfNet.HistoryValues = 10;
        d_PerfNet.LightColors = false;
        d_PerfNet.Location = new Point(3, 173);
        d_PerfNet.Margin = new Padding(0, 2, 2, 2);
        d_PerfNet.Name = "d_PerfNet";
        d_PerfNet.ScaleMode = sMkTaskManager.Controls.sMkPerfMeter.ScaleModes.Relative;
        d_PerfNet.Size = new Size(70, 131);
        d_PerfNet.TabIndex = 6;
        // 
        // d_Label02
        // 
        d_Label02.Dock = DockStyle.Fill;
        d_Label02.Location = new Point(81, 0);
        d_Label02.Name = "d_Label02";
        d_Label02.Size = new Size(377, 20);
        d_Label02.TabIndex = 1;
        d_Label02.Text = "Disk Usage History";
        d_Label02.TextAlign = ContentAlignment.BottomLeft;
        // 
        // d_Label01
        // 
        d_Label01.Dock = DockStyle.Fill;
        d_Label01.Location = new Point(0, 0);
        d_Label01.Margin = new Padding(0);
        d_Label01.Name = "d_Label01";
        d_Label01.Size = new Size(78, 20);
        d_Label01.TabIndex = 0;
        d_Label01.Text = "Disk Delta";
        d_Label01.TextAlign = ContentAlignment.BottomCenter;
        // 
        // d_TableLayout2
        // 
        d_TableLayout2.ColumnCount = 2;
        d_TableLayout1.SetColumnSpan(d_TableLayout2, 2);
        d_TableLayout2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        d_TableLayout2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        d_TableLayout2.Controls.Add(d_GroupBox2, 1, 0);
        d_TableLayout2.Controls.Add(d_GroupBox1, 0, 0);
        d_TableLayout2.Dock = DockStyle.Fill;
        d_TableLayout2.Location = new Point(0, 306);
        d_TableLayout2.Margin = new Padding(0);
        d_TableLayout2.Name = "d_TableLayout2";
        d_TableLayout2.RowCount = 1;
        d_TableLayout2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        d_TableLayout2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        d_TableLayout2.Size = new Size(461, 130);
        d_TableLayout2.TabIndex = 8;
        // 
        // d_GroupBox2
        // 
        d_GroupBox2.Controls.Add(d_lblNetRcvdRate);
        d_GroupBox2.Controls.Add(d_lblNetRcvdDelta);
        d_GroupBox2.Controls.Add(d_lblNetRcvd);
        d_GroupBox2.Controls.Add(d_Label26);
        d_GroupBox2.Controls.Add(d_Label25);
        d_GroupBox2.Controls.Add(d_Label24);
        d_GroupBox2.Controls.Add(d_lblNetSendRate);
        d_GroupBox2.Controls.Add(d_lblNetSendDelta);
        d_GroupBox2.Controls.Add(d_lblNetSend);
        d_GroupBox2.Controls.Add(d_lblDivider2);
        d_GroupBox2.Controls.Add(d_Label23);
        d_GroupBox2.Controls.Add(d_Label22);
        d_GroupBox2.Controls.Add(d_Label21);
        d_GroupBox2.Dock = DockStyle.Fill;
        d_GroupBox2.Location = new Point(233, 3);
        d_GroupBox2.Name = "d_GroupBox2";
        d_GroupBox2.Size = new Size(225, 124);
        d_GroupBox2.TabIndex = 0;
        d_GroupBox2.TabStop = false;
        d_GroupBox2.Text = "Network Usage Statistics";
        // 
        // d_lblNetRcvdRate
        // 
        d_lblNetRcvdRate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        d_lblNetRcvdRate.Location = new Point(95, 103);
        d_lblNetRcvdRate.Margin = new Padding(3, 0, 3, 1);
        d_lblNetRcvdRate.Name = "d_lblNetRcvdRate";
        d_lblNetRcvdRate.Size = new Size(126, 16);
        d_lblNetRcvdRate.TabIndex = 12;
        d_lblNetRcvdRate.Text = "0Kb.";
        d_lblNetRcvdRate.TextAlign = ContentAlignment.MiddleRight;
        // 
        // d_lblNetRcvdDelta
        // 
        d_lblNetRcvdDelta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        d_lblNetRcvdDelta.Location = new Point(95, 88);
        d_lblNetRcvdDelta.Margin = new Padding(3, 0, 3, 1);
        d_lblNetRcvdDelta.Name = "d_lblNetRcvdDelta";
        d_lblNetRcvdDelta.Size = new Size(126, 16);
        d_lblNetRcvdDelta.TabIndex = 10;
        d_lblNetRcvdDelta.Text = "0Kb.";
        d_lblNetRcvdDelta.TextAlign = ContentAlignment.MiddleRight;
        // 
        // d_lblNetRcvd
        // 
        d_lblNetRcvd.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        d_lblNetRcvd.Location = new Point(95, 73);
        d_lblNetRcvd.Margin = new Padding(3, 0, 3, 1);
        d_lblNetRcvd.Name = "d_lblNetRcvd";
        d_lblNetRcvd.Size = new Size(126, 16);
        d_lblNetRcvd.TabIndex = 8;
        d_lblNetRcvd.Text = "0Kb.";
        d_lblNetRcvd.TextAlign = ContentAlignment.MiddleRight;
        // 
        // d_Label26
        // 
        d_Label26.Location = new Point(4, 103);
        d_Label26.Margin = new Padding(3, 0, 3, 1);
        d_Label26.Name = "d_Label26";
        d_Label26.Size = new Size(90, 16);
        d_Label26.TabIndex = 11;
        d_Label26.Text = "Received Rate:";
        d_Label26.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // d_Label25
        // 
        d_Label25.Location = new Point(4, 88);
        d_Label25.Margin = new Padding(3, 0, 3, 1);
        d_Label25.Name = "d_Label25";
        d_Label25.Size = new Size(90, 16);
        d_Label25.TabIndex = 9;
        d_Label25.Text = "Received Delta:";
        d_Label25.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // d_Label24
        // 
        d_Label24.Location = new Point(4, 73);
        d_Label24.Margin = new Padding(3, 0, 3, 1);
        d_Label24.Name = "d_Label24";
        d_Label24.Size = new Size(90, 16);
        d_Label24.TabIndex = 7;
        d_Label24.Text = "Total Received:";
        d_Label24.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // d_lblNetSendRate
        // 
        d_lblNetSendRate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        d_lblNetSendRate.Location = new Point(80, 49);
        d_lblNetSendRate.Margin = new Padding(3, 0, 3, 1);
        d_lblNetSendRate.Name = "d_lblNetSendRate";
        d_lblNetSendRate.Size = new Size(141, 16);
        d_lblNetSendRate.TabIndex = 5;
        d_lblNetSendRate.Text = "0Kb.";
        d_lblNetSendRate.TextAlign = ContentAlignment.MiddleRight;
        // 
        // d_lblNetSendDelta
        // 
        d_lblNetSendDelta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        d_lblNetSendDelta.Location = new Point(80, 34);
        d_lblNetSendDelta.Margin = new Padding(3, 0, 3, 1);
        d_lblNetSendDelta.Name = "d_lblNetSendDelta";
        d_lblNetSendDelta.Size = new Size(141, 16);
        d_lblNetSendDelta.TabIndex = 3;
        d_lblNetSendDelta.Text = "0Kb.";
        d_lblNetSendDelta.TextAlign = ContentAlignment.MiddleRight;
        // 
        // d_lblNetSend
        // 
        d_lblNetSend.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        d_lblNetSend.Location = new Point(80, 19);
        d_lblNetSend.Margin = new Padding(3, 0, 3, 1);
        d_lblNetSend.Name = "d_lblNetSend";
        d_lblNetSend.Size = new Size(141, 16);
        d_lblNetSend.TabIndex = 1;
        d_lblNetSend.Text = "0Kb.";
        d_lblNetSend.TextAlign = ContentAlignment.MiddleRight;
        // 
        // d_lblDivider2
        // 
        d_lblDivider2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        d_lblDivider2.BackColor = SystemColors.ControlDark;
        d_lblDivider2.Location = new Point(6, 69);
        d_lblDivider2.Margin = new Padding(3, 0, 3, 1);
        d_lblDivider2.Name = "d_lblDivider2";
        d_lblDivider2.Size = new Size(212, 1);
        d_lblDivider2.TabIndex = 6;
        d_lblDivider2.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // d_Label23
        // 
        d_Label23.Location = new Point(4, 49);
        d_Label23.Margin = new Padding(3, 0, 3, 1);
        d_Label23.Name = "d_Label23";
        d_Label23.Size = new Size(75, 16);
        d_Label23.TabIndex = 4;
        d_Label23.Text = "Sent Rate:";
        d_Label23.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // d_Label22
        // 
        d_Label22.Location = new Point(4, 34);
        d_Label22.Margin = new Padding(3, 0, 3, 1);
        d_Label22.Name = "d_Label22";
        d_Label22.Size = new Size(75, 16);
        d_Label22.TabIndex = 2;
        d_Label22.Text = "Sent Delta:";
        d_Label22.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // d_Label21
        // 
        d_Label21.Location = new Point(4, 19);
        d_Label21.Margin = new Padding(3, 0, 3, 1);
        d_Label21.Name = "d_Label21";
        d_Label21.Size = new Size(75, 16);
        d_Label21.TabIndex = 0;
        d_Label21.Text = "Total Sent:";
        d_Label21.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // d_GroupBox1
        // 
        d_GroupBox1.Controls.Add(d_lblDiskWriteRate);
        d_GroupBox1.Controls.Add(d_lblDiskWriteDelta);
        d_GroupBox1.Controls.Add(d_lblDiskWrite);
        d_GroupBox1.Controls.Add(d_Label15);
        d_GroupBox1.Controls.Add(d_Label14);
        d_GroupBox1.Controls.Add(d_Label13);
        d_GroupBox1.Controls.Add(d_lblDiskReadRate);
        d_GroupBox1.Controls.Add(d_lblDiskReadDelta);
        d_GroupBox1.Controls.Add(d_lblDiskRead);
        d_GroupBox1.Controls.Add(d_lblDivider1);
        d_GroupBox1.Controls.Add(d_Label12);
        d_GroupBox1.Controls.Add(d_Label11);
        d_GroupBox1.Controls.Add(d_Label10);
        d_GroupBox1.Dock = DockStyle.Fill;
        d_GroupBox1.Location = new Point(3, 3);
        d_GroupBox1.Name = "d_GroupBox1";
        d_GroupBox1.Size = new Size(224, 124);
        d_GroupBox1.TabIndex = 0;
        d_GroupBox1.TabStop = false;
        d_GroupBox1.Text = "Disk Usage Statistics";
        // 
        // d_lblDiskWriteRate
        // 
        d_lblDiskWriteRate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        d_lblDiskWriteRate.Location = new Point(80, 103);
        d_lblDiskWriteRate.Margin = new Padding(3, 0, 3, 1);
        d_lblDiskWriteRate.Name = "d_lblDiskWriteRate";
        d_lblDiskWriteRate.Size = new Size(141, 16);
        d_lblDiskWriteRate.TabIndex = 12;
        d_lblDiskWriteRate.Text = "0Kb.";
        d_lblDiskWriteRate.TextAlign = ContentAlignment.MiddleRight;
        // 
        // d_lblDiskWriteDelta
        // 
        d_lblDiskWriteDelta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        d_lblDiskWriteDelta.Location = new Point(80, 88);
        d_lblDiskWriteDelta.Margin = new Padding(3, 0, 3, 1);
        d_lblDiskWriteDelta.Name = "d_lblDiskWriteDelta";
        d_lblDiskWriteDelta.Size = new Size(141, 16);
        d_lblDiskWriteDelta.TabIndex = 10;
        d_lblDiskWriteDelta.Text = "0Kb.";
        d_lblDiskWriteDelta.TextAlign = ContentAlignment.MiddleRight;
        // 
        // d_lblDiskWrite
        // 
        d_lblDiskWrite.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        d_lblDiskWrite.Location = new Point(80, 73);
        d_lblDiskWrite.Margin = new Padding(3, 0, 3, 1);
        d_lblDiskWrite.Name = "d_lblDiskWrite";
        d_lblDiskWrite.Size = new Size(141, 16);
        d_lblDiskWrite.TabIndex = 8;
        d_lblDiskWrite.Text = "0Kb.";
        d_lblDiskWrite.TextAlign = ContentAlignment.MiddleRight;
        // 
        // d_Label15
        // 
        d_Label15.Location = new Point(4, 103);
        d_Label15.Margin = new Padding(3, 0, 3, 1);
        d_Label15.Name = "d_Label15";
        d_Label15.Size = new Size(75, 16);
        d_Label15.TabIndex = 11;
        d_Label15.Text = "Write Rate:";
        d_Label15.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // d_Label14
        // 
        d_Label14.Location = new Point(4, 88);
        d_Label14.Margin = new Padding(3, 0, 3, 1);
        d_Label14.Name = "d_Label14";
        d_Label14.Size = new Size(75, 16);
        d_Label14.TabIndex = 9;
        d_Label14.Text = "Write Delta:";
        d_Label14.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // d_Label13
        // 
        d_Label13.Location = new Point(4, 73);
        d_Label13.Margin = new Padding(3, 0, 3, 1);
        d_Label13.Name = "d_Label13";
        d_Label13.Size = new Size(75, 16);
        d_Label13.TabIndex = 7;
        d_Label13.Text = "Total Write:";
        d_Label13.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // d_lblDiskReadRate
        // 
        d_lblDiskReadRate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        d_lblDiskReadRate.Location = new Point(80, 49);
        d_lblDiskReadRate.Margin = new Padding(3, 0, 3, 1);
        d_lblDiskReadRate.Name = "d_lblDiskReadRate";
        d_lblDiskReadRate.Size = new Size(141, 16);
        d_lblDiskReadRate.TabIndex = 5;
        d_lblDiskReadRate.Text = "0Kb.";
        d_lblDiskReadRate.TextAlign = ContentAlignment.MiddleRight;
        // 
        // d_lblDiskReadDelta
        // 
        d_lblDiskReadDelta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        d_lblDiskReadDelta.Location = new Point(80, 34);
        d_lblDiskReadDelta.Margin = new Padding(3, 0, 3, 1);
        d_lblDiskReadDelta.Name = "d_lblDiskReadDelta";
        d_lblDiskReadDelta.Size = new Size(141, 16);
        d_lblDiskReadDelta.TabIndex = 3;
        d_lblDiskReadDelta.Text = "0Kb.";
        d_lblDiskReadDelta.TextAlign = ContentAlignment.MiddleRight;
        // 
        // d_lblDiskRead
        // 
        d_lblDiskRead.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        d_lblDiskRead.Location = new Point(80, 19);
        d_lblDiskRead.Margin = new Padding(3, 0, 3, 1);
        d_lblDiskRead.Name = "d_lblDiskRead";
        d_lblDiskRead.Size = new Size(141, 16);
        d_lblDiskRead.TabIndex = 1;
        d_lblDiskRead.Text = "0Kb.";
        d_lblDiskRead.TextAlign = ContentAlignment.MiddleRight;
        // 
        // d_lblDivider1
        // 
        d_lblDivider1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        d_lblDivider1.BackColor = SystemColors.ControlDark;
        d_lblDivider1.Location = new Point(6, 69);
        d_lblDivider1.Margin = new Padding(3, 0, 3, 1);
        d_lblDivider1.Name = "d_lblDivider1";
        d_lblDivider1.Size = new Size(212, 1);
        d_lblDivider1.TabIndex = 6;
        d_lblDivider1.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // d_Label12
        // 
        d_Label12.Location = new Point(4, 49);
        d_Label12.Margin = new Padding(3, 0, 3, 1);
        d_Label12.Name = "d_Label12";
        d_Label12.Size = new Size(75, 16);
        d_Label12.TabIndex = 4;
        d_Label12.Text = "Read Rate:";
        d_Label12.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // d_Label11
        // 
        d_Label11.Location = new Point(4, 34);
        d_Label11.Margin = new Padding(3, 0, 3, 1);
        d_Label11.Name = "d_Label11";
        d_Label11.Size = new Size(75, 16);
        d_Label11.TabIndex = 2;
        d_Label11.Text = "Read Delta:";
        d_Label11.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // d_Label10
        // 
        d_Label10.Location = new Point(4, 19);
        d_Label10.Margin = new Padding(3, 0, 3, 1);
        d_Label10.Name = "d_Label10";
        d_Label10.Size = new Size(75, 16);
        d_Label10.TabIndex = 0;
        d_Label10.Text = "Total Read:";
        d_Label10.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // tpModules
        // 
        tpModules.Controls.Add(lvModules);
        tpModules.Location = new Point(4, 24);
        tpModules.Name = "tpModules";
        tpModules.Size = new Size(463, 436);
        tpModules.TabIndex = 4;
        tpModules.Text = "Modules";
        tpModules.UseVisualStyleBackColor = true;
        // 
        // lvModules
        // 
        lvModules.BorderStyle = BorderStyle.None;
        lvModules.Columns.AddRange(new ColumnHeader[] { colModules_Name, colModules_Version, colModules_Type, colModules_Address, colModules_Memory, colModules_Description, colModules_Path, colModules_Company, colModules_Language });
        lvModules.Dock = DockStyle.Fill;
        lvModules.LabelWrap = false;
        lvModules.Location = new Point(0, 0);
        lvModules.MultiSelect = false;
        lvModules.FullRowSelect = true;
        lvModules.Name = "lvModules";
        lvModules.Size = new Size(463, 436);
        lvModules.TabIndex = 0;
        lvModules.UseCompatibleStateImageBehavior = false;
        lvModules.View = View.Details;
        // 
        // colModules_Name
        // 
        colModules_Name.Tag = "Name";
        colModules_Name.Text = "Module Name";
        colModules_Name.Width = 90;
        // 
        // colModules_Version
        // 
        colModules_Version.Tag = "Version";
        colModules_Version.Text = "Version";
        colModules_Version.Width = 70;
        // 
        // colModules_Type
        // 
        colModules_Type.Tag = "Type";
        colModules_Type.Text = "Type";
        // 
        // colModules_Address
        // 
        colModules_Address.Tag = "Address";
        colModules_Address.Text = "Base Address";
        colModules_Address.TextAlign = HorizontalAlignment.Right;
        colModules_Address.Width = 90;
        // 
        // colModules_Memory
        // 
        colModules_Memory.Tag = "Memory";
        colModules_Memory.Text = "Memory";
        colModules_Memory.TextAlign = HorizontalAlignment.Right;
        colModules_Memory.Width = 70;
        // 
        // colModules_Description
        // 
        colModules_Description.Tag = "Description";
        colModules_Description.Text = "Description";
        colModules_Description.Width = 80;
        // 
        // colModules_Path
        // 
        colModules_Path.Tag = "Path";
        colModules_Path.Text = "Path";
        // 
        // colModules_Company
        // 
        colModules_Company.Tag = "Company";
        colModules_Company.Text = "Company";
        colModules_Company.Width = 70;
        // 
        // colModules_Language
        // 
        colModules_Language.Tag = "Language";
        colModules_Language.Text = "Language";
        colModules_Language.Width = 80;
        // 
        // tpThreads
        // 
        tpThreads.Controls.Add(lvThreads);
        tpThreads.Location = new Point(4, 24);
        tpThreads.Name = "tpThreads";
        tpThreads.Size = new Size(463, 436);
        tpThreads.TabIndex = 5;
        tpThreads.Text = "Threads";
        tpThreads.UseVisualStyleBackColor = true;
        // 
        // lvThreads
        // 
        lvThreads.BorderStyle = BorderStyle.None;
        lvThreads.Columns.AddRange(new ColumnHeader[] { colThreads_ID, colThreads_Priority, colThreads_State, colThreads_Reason, colThreads_StartTime, colThreads_RunTime });
        lvThreads.Dock = DockStyle.Fill;
        lvThreads.LabelWrap = false;
        lvThreads.Location = new Point(0, 0);
        lvThreads.MultiSelect = false;
        lvThreads.FullRowSelect = true;
        lvThreads.Name = "lvThreads";
        lvThreads.Size = new Size(463, 436);
        lvThreads.TabIndex = 1;
        lvThreads.UseCompatibleStateImageBehavior = false;
        lvThreads.View = View.Details;
        // 
        // colThreads_ID
        // 
        colThreads_ID.Tag = "TID";
        colThreads_ID.Text = "TID";
        colThreads_ID.Width = 50;
        // 
        // colThreads_Priority
        // 
        colThreads_Priority.Tag = "Priority";
        colThreads_Priority.Text = "Priority";
        colThreads_Priority.Width = 70;
        // 
        // colThreads_State
        // 
        colThreads_State.Tag = "State";
        colThreads_State.Text = "State";
        colThreads_State.Width = 70;
        // 
        // colThreads_Reason
        // 
        colThreads_Reason.Tag = "WaitReason";
        colThreads_Reason.Text = "Wait Reason";
        colThreads_Reason.Width = 90;
        // 
        // colThreads_StartTime
        // 
        colThreads_StartTime.Tag = "StartTime";
        colThreads_StartTime.Text = "Start Time";
        colThreads_StartTime.Width = 80;
        // 
        // colThreads_RunTime
        // 
        colThreads_RunTime.Tag = "RunTime";
        colThreads_RunTime.Text = "Run Time";
        colThreads_RunTime.Width = 80;
        // 
        // tpLocked
        // 
        tpLocked.Controls.Add(lvLockedFiles);
        tpLocked.Location = new Point(4, 24);
        tpLocked.Name = "tpLocked";
        tpLocked.Size = new Size(463, 436);
        tpLocked.TabIndex = 6;
        tpLocked.Text = "Locked Files";
        tpLocked.UseVisualStyleBackColor = true;
        // 
        // lvLockedFiles
        // 
        lvLockedFiles.BorderStyle = BorderStyle.None;
        lvLockedFiles.Columns.AddRange(new ColumnHeader[] { colLockedFiles_Filename, colLockedFiles_Path });
        lvLockedFiles.Dock = DockStyle.Fill;
        lvLockedFiles.LabelWrap = false;
        lvLockedFiles.Location = new Point(0, 0);
        lvLockedFiles.MultiSelect = false;
        lvLockedFiles.FullRowSelect = true;
        lvLockedFiles.Name = "lvLockedFiles";
        lvLockedFiles.Size = new Size(463, 436);
        lvLockedFiles.TabIndex = 2;
        lvLockedFiles.UseCompatibleStateImageBehavior = false;
        lvLockedFiles.View = View.Details;
        // 
        // colLockedFiles_Filename
        // 
        colLockedFiles_Filename.Tag = "Filename";
        colLockedFiles_Filename.Text = "Filename";
        colLockedFiles_Filename.Width = 90;
        // 
        // colLockedFiles_Path
        // 
        colLockedFiles_Path.Tag = "Path";
        colLockedFiles_Path.Text = "Full Path";
        colLockedFiles_Path.Width = 350;
        // 
        // btnOK
        // 
        btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnOK.Location = new Point(323, 479);
        btnOK.Margin = new Padding(0);
        btnOK.Name = "btnOK";
        btnOK.Size = new Size(75, 23);
        btnOK.TabIndex = 4;
        btnOK.Text = "OK";
        btnOK.UseVisualStyleBackColor = true;
        btnOK.Click += btnOK_Click;
        // 
        // btnCancel
        // 
        btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(402, 479);
        btnCancel.Margin = new Padding(0);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(75, 23);
        btnCancel.TabIndex = 5;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;
        // 
        // lblSpeed
        // 
        lblSpeed.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblSpeed.BackColor = Color.Transparent;
        lblSpeed.Location = new Point(4, 473);
        lblSpeed.Name = "lblSpeed";
        lblSpeed.Size = new Size(124, 15);
        lblSpeed.TabIndex = 1;
        lblSpeed.Text = "Update Speed";
        lblSpeed.TextAlign = ContentAlignment.TopCenter;
        // 
        // tbSpeed
        // 
        tbSpeed.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        tbSpeed.AutoSize = false;
        tbSpeed.Location = new Point(4, 484);
        tbSpeed.Maximum = 30;
        tbSpeed.Name = "tbSpeed";
        tbSpeed.Size = new Size(124, 23);
        tbSpeed.TabIndex = 2;
        tbSpeed.TabStop = false;
        tbSpeed.TickFrequency = 3;
        tbSpeed.TickStyle = TickStyle.None;
        tbSpeed.Value = 20;
        tbSpeed.ValueChanged += tbSpeed_ValueChanged;
        // 
        // lblSpeedValue
        // 
        lblSpeedValue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblSpeedValue.Location = new Point(124, 484);
        lblSpeedValue.Name = "lblSpeedValue";
        lblSpeedValue.Size = new Size(30, 20);
        lblSpeedValue.TabIndex = 3;
        lblSpeedValue.Text = "1s";
        lblSpeedValue.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // frmProcess_Details
        // 
        AcceptButton = btnOK;
        CancelButton = btnCancel;
        ClientSize = new Size(484, 509);
        Controls.Add(lblSpeed);
        Controls.Add(tbSpeed);
        Controls.Add(lblSpeedValue);
        Controls.Add(btnCancel);
        Controls.Add(btnOK);
        Controls.Add(tc);
        DoubleBuffered = true;
        Icon = Resources.Resources.frmProcess_Details;
        MinimumSize = new Size(400, 500);
        Name = "frmProcess_Details";
        SizeGripStyle = SizeGripStyle.Show;
        Text = "sMk Task Manager - Process Details";
        FormClosing += OnFormClosing;
        Load += OnLoad;
        tc.ResumeLayout(false);
        tpGeneral.ResumeLayout(false);
        g_TableLayout1.ResumeLayout(false);
        g_gpCPU.ResumeLayout(false);
        g_gpVmMemory.ResumeLayout(false);
        g_gpPhMemory.ResumeLayout(false);
        g_gpResources.ResumeLayout(false);
        g_gpDetails.ResumeLayout(false);
        g_gpDetails.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)g_pbIcon).EndInit();
        tpPerformance.ResumeLayout(false);
        p_TableLayout1.ResumeLayout(false);
        p_panel2.ResumeLayout(false);
        p_panel5.ResumeLayout(false);
        p_panel3.ResumeLayout(false);
        p_panel7.ResumeLayout(false);
        p_panel1.ResumeLayout(false);
        p_panel6.ResumeLayout(false);
        p_panel4.ResumeLayout(false);
        tpIO.ResumeLayout(false);
        i_TableLayout1.ResumeLayout(false);
        i_TableLayout2.ResumeLayout(false);
        i_GroupBox3.ResumeLayout(false);
        i_GroupBox2.ResumeLayout(false);
        i_GroupBox1.ResumeLayout(false);
        tpDiskNet.ResumeLayout(false);
        d_TableLayout1.ResumeLayout(false);
        d_TableLayout2.ResumeLayout(false);
        d_GroupBox2.ResumeLayout(false);
        d_GroupBox1.ResumeLayout(false);
        tpModules.ResumeLayout(false);
        tpThreads.ResumeLayout(false);
        tpLocked.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)tbSpeed).EndInit();
        ResumeLayout(false);
    }

    private TabControl tc;
    private Button btnOK;
    private Button btnCancel;
    private Label lblSpeed;
    private TrackBar tbSpeed;
    private TabPage tpGeneral;
    private TabPage tpPerformance;
    private TabPage tpIO;
    private TabPage tpDiskNet;
    private TabPage tpModules;
    private TabPage tpThreads;
    private TabPage tpLocked;
    private Label lblSpeedValue;
    private GroupBox g_gpCPU;
    private GroupBox g_gpVmMemory;
    private GroupBox g_gpResources;
    private GroupBox g_gpPhMemory;
    private GroupBox g_gpDetails;
    private TableLayoutPanel g_TableLayout1;
    private TextBox g_txtPID;
    private Label g_Label02;
    private Label g_Label01;
    private Label g_Label07;
    private Label g_Label06;
    private Label g_Label05;
    private Label g_Label04;
    private Label g_Label03;
    private TextBox g_txtVersion;
    private TextBox g_txtType;
    private TextBox g_txtPath;
    private TextBox g_txtName;
    private TextBox g_txtDescription;
    private TextBox g_txtCompany;
    private PictureBox g_pbIcon;
    private Label lblDivider1;
    private Label g_Label13;
    private Label g_Label12;
    private Label g_Label11;
    private Label g_Label10;
    private Label g_Label09;
    private Label g_Label08;
    private Label lblDivider2;
    private Label g_lblCPURunning;
    private Label g_lblCPUCreation;
    private Label g_lblCPUTimeTotal;
    private Label g_lblCPUTimeKernel;
    private Label g_lblCPUTimeUser;
    private Label g_lblCPUPriority;
    private Label g_lblMemWSpeak;
    private Label g_Label17;
    private Label g_lblMemWSshare;
    private Label g_lblMemWSpriv;
    private Label g_lblMemWS;
    private Label g_Label16;
    private Label g_Label15;
    private Label g_Label14;
    private Label g_Label27;
    private Label g_Label26;
    private Label g_Label25;
    private Label g_Label24;
    private Label g_lblPageFaultsDelta;
    private Label g_lblPageFaults;
    private Label g_lblPagedMemory;
    private Label g_lblVirtualMemory;
    private Label g_lblResUser;
    private Label g_lblResGDI;
    private Label g_lblResHandlesPeak;
    private Label g_lblResHandles;
    private Label g_lblResThreadsPeak;
    private Label g_lblResThreads;
    private Label lblDivider4;
    private Label lblDivider3;
    private Label g_Label23;
    private Label g_Label22;
    private Label g_Label21;
    private Label g_Label20;
    private Label g_Label18;
    private Label g_Label19;
    private TableLayoutPanel p_TableLayout1;
    private Panel p_panel4;
    private Label p_lblVirtualMemory;
    private Label p_Label04;
    private Panel p_panel5;
    private Label p_lblPageFaults;
    private Label p_Label05;
    private Panel p_panel3;
    private Label p_lblPrivateBytes;
    private Label p_Label03;
    private Panel p_panel7;
    private Label p_lblUser;
    private Label p_Label07;
    private Panel p_panel1;
    private Label p_lblCpuUsage;
    private Label p_Label01;
    private Panel p_panel6;
    private Label p_lblGDI;
    private Label p_Label06;
    private Panel p_panel2;
    private Label p_lblWorkingSet;
    private Label p_Label02;
    private Controls.sMkPerfChart p_ChartCPU;
    private Controls.sMkPerfChart p_ChartGDI;
    private Controls.sMkPerfChart p_ChartWS;
    private Controls.sMkPerfChart p_ChartPB;
    private Controls.sMkPerfChart p_ChartVM;
    private Controls.sMkPerfChart p_ChartPF;
    private Controls.sMkPerfChart p_ChartUser;
    private TableLayoutPanel i_TableLayout1;
    private Label i_Label01;
    private Label i_Label06;
    private Label i_Label05;
    private Label i_Label04;
    private Label i_Label03;
    private Label i_Label02;
    private Controls.sMkPerfChart i_ChartOthers;
    private Controls.sMkPerfChart i_ChartWrites;
    private Controls.sMkPerfChart i_ChartReads;
    private Controls.sMkPerfMeter i_PerfReads;
    private Controls.sMkPerfMeter i_PerfWrites;
    private Controls.sMkPerfMeter i_PerfOthers;
    private TableLayoutPanel i_TableLayout2;
    private GroupBox i_GroupBox1;
    private Label i_lblReadBytesDelta;
    private Label i_lblReadBytes;
    private Label i_lblReadCountDelta;
    private Label i_lblReadCount;
    private Label i_Label14;
    private Label i_Label13;
    private Label i_Label12;
    private Label i_Label11;
    private GroupBox i_GroupBox3;
    private Label i_lblOtherBytesDelta;
    private Label i_lblOtherBytes;
    private Label i_lblOtherCountDelta;
    private Label i_lblOtherCount;
    private Label i_Label34;
    private Label i_Label33;
    private Label i_Label32;
    private Label i_Label31;
    private GroupBox i_GroupBox2;
    private Label i_lblWriteBytesDelta;
    private Label i_lblWriteBytes;
    private Label i_lblWriteCountDelta;
    private Label i_lblWriteCount;
    private Label i_Label24;
    private Label i_Label23;
    private Label i_Label22;
    private Label i_Label21;
    private TableLayoutPanel d_TableLayout1;
    private Controls.sMkPerfChart d_ChartNet;
    private Controls.sMkPerfChart d_ChartDisk;
    private Label d_Label04;
    private Label d_Label03;
    private Controls.sMkPerfMeter d_PerfDisk;
    private Controls.sMkPerfMeter d_PerfNet;
    private Label d_Label02;
    private Label d_Label01;
    private TableLayoutPanel d_TableLayout2;
    private GroupBox d_GroupBox2;
    private GroupBox d_GroupBox1;
    private Label d_lblDiskReadRate;
    private Label d_lblDiskReadDelta;
    private Label d_lblDiskRead;
    private Label d_lblDivider1;
    private Label d_Label12;
    private Label d_Label11;
    private Label d_Label10;
    private Label d_lblNetRcvdRate;
    private Label d_lblNetRcvdDelta;
    private Label d_lblNetRcvd;
    private Label d_Label26;
    private Label d_Label25;
    private Label d_Label24;
    private Label d_lblNetSendRate;
    private Label d_lblNetSendDelta;
    private Label d_lblNetSend;
    private Label d_lblDivider2;
    private Label d_Label23;
    private Label d_Label22;
    private Label d_Label21;
    private Label d_lblDiskWriteRate;
    private Label d_lblDiskWriteDelta;
    private Label d_lblDiskWrite;
    private Label d_Label15;
    private Label d_Label14;
    private Label d_Label13;
    private ListView lvModules;
    private ColumnHeader colModules_Name;
    private ColumnHeader colModules_Version;
    private ColumnHeader colModules_Type;
    private ColumnHeader colModules_Address;
    private ColumnHeader colModules_Memory;
    private ColumnHeader colModules_Description;
    private ColumnHeader colModules_Path;
    private ColumnHeader colModules_Company;
    private ColumnHeader colModules_Language;
    private ListView lvThreads;
    private ColumnHeader colThreads_ID;
    private ColumnHeader colThreads_Priority;
    private ColumnHeader colThreads_State;
    private ColumnHeader colThreads_Reason;
    private ColumnHeader colThreads_StartTime;
    private ColumnHeader colThreads_RunTime;
    private ListView lvLockedFiles;
    private ColumnHeader colLockedFiles_Filename;
    private ColumnHeader colLockedFiles_Path;
}
