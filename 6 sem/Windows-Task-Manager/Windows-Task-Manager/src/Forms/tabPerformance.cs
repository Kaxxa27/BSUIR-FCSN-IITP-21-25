using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Runtime.Versioning;
using sMkTaskManager.Classes;
using sMkTaskManager.Controls;
namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
internal partial class tabPerformance : UserControl, ITaskManagerTab {
    private readonly Stopwatch _stopWatch = new();
    private Point LastMousePoint = new(0, 0);
    internal TaskManagerSystem System = new();

    public event EventHandler? ForceRefreshClicked;
    public event EventHandler? RefreshStarts;
    public event EventHandler? RefreshComplete;

    #region "Form Controls"
    private TableLayoutPanel tlpMain;
    private Label lblCpuMeter;
    private Label lblCpuChart;
    private Label lblMemMeter;
    private Label lblMemChart;
    private Label lblIOMeter;
    private Label lblIOChart;
    private Label lblDiskMeter;
    private Label lblDiskChart;
    private Label lblNetMeter;
    private Label lblNetChart;
    private sMkPerfMeter meterCpu;
    private sMkPerfChart chartCpu;
    private sMkPerfMeter meterMem;
    private sMkPerfChart chartMem;
    private sMkPerfMeter meterIO;
    private sMkPerfChart chartIO;
    private sMkPerfMeter meterDisk;
    private sMkPerfChart chartDisk;
    private sMkPerfMeter meterNet;
    private sMkPerfChart chartNet;
    private TableLayoutPanel tlpDetails;
    private GroupBox gbMemory;
    private Label gbMemory_Avail;
    private Label gbMemory_Total;
    private Label gbMemory_Cached;
    private Label gbMemory_Label1;
    private Label gbMemory_Label2;
    private Label gbMemory_Label3;
    private GroupBox gbIOops;
    private Label gbIOops_Label1;
    private Label gbIOops_Label2;
    private Label gbIOops_Label3;
    private Label gbIOops_Reads;
    private Label gbIOops_Writes;
    private Label gbIOops_Others;
    private GroupBox gbIOtranf;
    private Label gbIOtranf_Label1;
    private Label gbIOtranf_Label2;
    private Label gbIOtranf_Label3;
    private Label gbIOtranf_Reads;
    private Label gbIOtranf_Writes;
    private Label gbIOtranf_Others;
    private GroupBox gbCommit;
    private Label gbCommit_Label1;
    private Label gbCommit_Label2;
    private Label gbCommit_Label3;
    private Label gbCommit_Current;
    private Label gbCommit_Peak;
    private Label gbCommit_Limit;
    private GroupBox gbPagefile;
    private Label gbPagefile_Label1;
    private Label gbPagefile_Label2;
    private Label gbPagefile_Label3;
    private Label gbPagefile_Current;
    private Label gbPagefile_Peak;
    private Label gbPagefile_Limit;
    private GroupBox gbKernel;
    private Label gbKernel_Label1;
    private Label gbKernel_Label2;
    private Label gbKernel_Label3;
    private Label gbKernel_Total;
    private Label gbKernel_Paged;
    private Label gbKernel_NonPaged;
    private GroupBox gbSystem;
    private Label gbSystem_Label1;
    private Label gbSystem_Label2;
    private Label gbSystem_Label3;
    private Label gbSystem_Label4;
    private Label gbSystem_Label5;
    private Label gbSystem_Label6;
    private Label gbSystem_Handles;
    private Label gbSystem_Threads;
    private Label gbSystem_Processes;
    private Label gbSystem_Services;
    private Label gbSystem_Devices;
    private Label gbSystem_UpTime;
    #endregion
    private IContainer? components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }

    public tabPerformance() {
        InitializeComponent();
        InitializeSettings();
        Extensions.CascadingDoubleBuffer(this);
    }
    private void InitializeComponent() {
        components = new Container();
        tlpMain = new TableLayoutPanel();
        lblCpuMeter = new Label();
        lblCpuChart = new Label();
        meterCpu = new sMkPerfMeter();
        chartCpu = new sMkPerfChart();
        lblMemMeter = new Label();
        lblMemChart = new Label();
        meterMem = new sMkPerfMeter();
        chartMem = new sMkPerfChart();
        lblIOMeter = new Label();
        lblIOChart = new Label();
        meterIO = new sMkPerfMeter();
        chartIO = new sMkPerfChart();
        lblDiskMeter = new Label();
        lblDiskChart = new Label();
        meterDisk = new sMkPerfMeter();
        chartDisk = new sMkPerfChart();
        lblNetMeter = new Label();
        lblNetChart = new Label();
        meterNet = new sMkPerfMeter();
        chartNet = new sMkPerfChart();
        tlpDetails = new TableLayoutPanel();
        gbSystem = new GroupBox();
        gbSystem_Label6 = new Label();
        gbSystem_Label5 = new Label();
        gbSystem_Label4 = new Label();
        gbSystem_Label3 = new Label();
        gbSystem_Label2 = new Label();
        gbSystem_Label1 = new Label();
        gbSystem_UpTime = new Label();
        gbSystem_Devices = new Label();
        gbSystem_Services = new Label();
        gbSystem_Processes = new Label();
        gbSystem_Threads = new Label();
        gbSystem_Handles = new Label();
        gbIOtranf = new GroupBox();
        gbIOtranf_Label1 = new Label();
        gbIOtranf_Label2 = new Label();
        gbIOtranf_Label3 = new Label();
        gbIOtranf_Others = new Label();
        gbIOtranf_Writes = new Label();
        gbIOtranf_Reads = new Label();
        gbCommit = new GroupBox();
        gbCommit_Label1 = new Label();
        gbCommit_Label2 = new Label();
        gbCommit_Label3 = new Label();
        gbCommit_Peak = new Label();
        gbCommit_Current = new Label();
        gbCommit_Limit = new Label();
        gbPagefile = new GroupBox();
        gbPagefile_Label1 = new Label();
        gbPagefile_Label2 = new Label();
        gbPagefile_Label3 = new Label();
        gbPagefile_Peak = new Label();
        gbPagefile_Current = new Label();
        gbPagefile_Limit = new Label();
        gbKernel = new GroupBox();
        gbKernel_Label1 = new Label();
        gbKernel_Label2 = new Label();
        gbKernel_Label3 = new Label();
        gbKernel_NonPaged = new Label();
        gbKernel_Paged = new Label();
        gbKernel_Total = new Label();
        gbIOops = new GroupBox();
        gbIOops_Label1 = new Label();
        gbIOops_Label2 = new Label();
        gbIOops_Label3 = new Label();
        gbIOops_Others = new Label();
        gbIOops_Writes = new Label();
        gbIOops_Reads = new Label();
        gbMemory = new GroupBox();
        gbMemory_Label1 = new Label();
        gbMemory_Label3 = new Label();
        gbMemory_Label2 = new Label();
        gbMemory_Cached = new Label();
        gbMemory_Avail = new Label();
        gbMemory_Total = new Label();
        tlpMain.SuspendLayout();
        tlpDetails.SuspendLayout();
        gbSystem.SuspendLayout();
        gbIOtranf.SuspendLayout();
        gbCommit.SuspendLayout();
        gbPagefile.SuspendLayout();
        gbKernel.SuspendLayout();
        gbIOops.SuspendLayout();
        gbMemory.SuspendLayout();
        SuspendLayout();
        // 
        // tlpMain
        // 
        tlpMain.ColumnCount = 2;
        tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
        tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tlpMain.Controls.Add(lblCpuMeter, 0, 0);
        tlpMain.Controls.Add(lblCpuChart, 1, 0);
        tlpMain.Controls.Add(meterCpu, 0, 1);
        tlpMain.Controls.Add(chartCpu, 1, 1);
        tlpMain.Controls.Add(lblMemMeter, 0, 2);
        tlpMain.Controls.Add(lblMemChart, 1, 2);
        tlpMain.Controls.Add(meterMem, 0, 3);
        tlpMain.Controls.Add(chartMem, 1, 3);
        tlpMain.Controls.Add(lblIOMeter, 0, 4);
        tlpMain.Controls.Add(lblIOChart, 1, 4);
        tlpMain.Controls.Add(meterIO, 0, 5);
        tlpMain.Controls.Add(chartIO, 1, 5);
        tlpMain.Controls.Add(lblDiskMeter, 0, 6);
        tlpMain.Controls.Add(lblDiskChart, 1, 6);
        tlpMain.Controls.Add(meterDisk, 0, 7);
        tlpMain.Controls.Add(chartDisk, 1, 7);
        tlpMain.Controls.Add(lblNetMeter, 0, 8);
        tlpMain.Controls.Add(lblNetChart, 1, 8);
        tlpMain.Controls.Add(meterNet, 0, 9);
        tlpMain.Controls.Add(chartNet, 1, 9);
        tlpMain.Controls.Add(tlpDetails, 0, 10);
        tlpMain.Dock = DockStyle.Fill;
        tlpMain.Location = new Point(0, 0);
        tlpMain.Name = "tlpMain";
        tlpMain.Padding = new Padding(0, 3, 0, 0);
        tlpMain.RowCount = 11;
        tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
        tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
        tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
        tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
        tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
        tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
        tlpMain.Size = new Size(600, 600);
        tlpMain.TabIndex = 0;
        // 
        // lblCpuMeter
        // 
        lblCpuMeter.Dock = DockStyle.Fill;
        lblCpuMeter.Location = new Point(7, 3);
        lblCpuMeter.Margin = new Padding(7, 0, 3, 0);
        lblCpuMeter.Name = "lblCpuMeter";
        lblCpuMeter.Size = new Size(70, 16);
        lblCpuMeter.TabIndex = 1;
        lblCpuMeter.Text = "CPU Usage";
        lblCpuMeter.TextAlign = ContentAlignment.BottomCenter;
        // 
        // lblCpuChart
        // 
        lblCpuChart.Dock = DockStyle.Fill;
        lblCpuChart.Location = new Point(83, 4);
        lblCpuChart.Margin = new Padding(3, 1, 3, 0);
        lblCpuChart.Name = "lblCpuChart";
        lblCpuChart.Size = new Size(514, 15);
        lblCpuChart.TabIndex = 2;
        lblCpuChart.Text = "CPU Usage History";
        lblCpuChart.TextAlign = ContentAlignment.BottomLeft;
        // 
        // meterCpu
        // 
        meterCpu.BackColor = Color.Black;
        meterCpu.BarBackColor = Color.DarkGreen;
        meterCpu.BarForeColor = Color.Lime;
        meterCpu.BorderStyle = Border3DStyle.Sunken;
        meterCpu.Dock = DockStyle.Fill;
        meterCpu.HistoryValues = 10;
        meterCpu.LightColors = false;
        meterCpu.Location = new Point(7, 21);
        meterCpu.Margin = new Padding(7, 2, 3, 2);
        meterCpu.Name = "meterCpu";
        meterCpu.ScaleMode = sMkPerfMeter.ScaleModes.Absolute;
        meterCpu.Size = new Size(70, 69);
        meterCpu.TabIndex = 3;
        meterCpu.TabStop = false;
        // 
        // chartCpu
        // 
        chartCpu.AntiAliasing = false;
        chartCpu.BackColor = Color.Black;
        chartCpu.BackColorShade = Color.FromArgb(0, 0, 0);
        chartCpu.BackSolid = false;
        chartCpu.BorderStyle = Border3DStyle.Sunken;
        chartCpu.DetailsOnHover = false;
        chartCpu.DisplayAverage = false;
        chartCpu.DisplayIndexes = false;
        chartCpu.DisplayLegends = false;
        chartCpu.Dock = DockStyle.Fill;
        chartCpu.GridSpacing = 10;
        chartCpu.LegendSpacing = 35;
        chartCpu.LightColors = false;
        chartCpu.Location = new Point(83, 21);
        chartCpu.Margin = new Padding(3, 2, 5, 2);
        chartCpu.MaxValue = 0D;
        chartCpu.Name = "chartCpu";
        chartCpu.ScaleMode = sMkPerfChart.ScaleModes.Absolute;
        chartCpu.ShadeBackground = true;
        chartCpu.Size = new Size(512, 69);
        chartCpu.TabIndex = 4;
        chartCpu.TabStop = false;
        chartCpu.ValueSpacing = 2;
        chartCpu.ValuesSuffix = "";
        // 
        // lblMemMeter
        // 
        lblMemMeter.Dock = DockStyle.Fill;
        lblMemMeter.Location = new Point(7, 92);
        lblMemMeter.Margin = new Padding(7, 0, 3, 0);
        lblMemMeter.Name = "lblMemMeter";
        lblMemMeter.Size = new Size(70, 16);
        lblMemMeter.TabIndex = 5;
        lblMemMeter.Text = "Mem Usage";
        lblMemMeter.TextAlign = ContentAlignment.BottomCenter;
        // 
        // lblMemChart
        // 
        lblMemChart.Dock = DockStyle.Fill;
        lblMemChart.Location = new Point(83, 93);
        lblMemChart.Margin = new Padding(3, 1, 3, 0);
        lblMemChart.Name = "lblMemChart";
        lblMemChart.Size = new Size(514, 15);
        lblMemChart.TabIndex = 6;
        lblMemChart.Text = "Memory Usage History";
        lblMemChart.TextAlign = ContentAlignment.BottomLeft;
        // 
        // meterMem
        // 
        meterMem.BackColor = Color.Black;
        meterMem.BarBackColor = Color.FromArgb(64, 64, 0);
        meterMem.BarForeColor = Color.Yellow;
        meterMem.BorderStyle = Border3DStyle.Sunken;
        meterMem.Dock = DockStyle.Fill;
        meterMem.HistoryValues = 10;
        meterMem.LightColors = false;
        meterMem.Location = new Point(7, 110);
        meterMem.Margin = new Padding(7, 2, 3, 2);
        meterMem.Name = "meterMem";
        meterMem.ScaleMode = sMkPerfMeter.ScaleModes.Absolute;
        meterMem.Size = new Size(70, 69);
        meterMem.TabIndex = 7;
        meterMem.TabStop = false;
        // 
        // chartMem
        // 
        chartMem.AntiAliasing = false;
        chartMem.BackColor = Color.Black;
        chartMem.BackColorShade = Color.FromArgb(0, 0, 0);
        chartMem.BackSolid = false;
        chartMem.BorderStyle = Border3DStyle.Sunken;
        chartMem.DetailsOnHover = false;
        chartMem.DisplayAverage = false;
        chartMem.DisplayIndexes = false;
        chartMem.DisplayLegends = false;
        chartMem.Dock = DockStyle.Fill;
        chartMem.GridSpacing = 10;
        chartMem.LegendSpacing = 35;
        chartMem.LightColors = false;
        chartMem.Location = new Point(83, 110);
        chartMem.Margin = new Padding(3, 2, 5, 2);
        chartMem.MaxValue = 0D;
        chartMem.Name = "chartMem";
        chartMem.ScaleMode = sMkPerfChart.ScaleModes.Absolute;
        chartMem.ShadeBackground = true;
        chartMem.Size = new Size(512, 69);
        chartMem.TabIndex = 8;
        chartMem.TabStop = false;
        chartMem.ValueSpacing = 2;
        chartMem.ValuesSuffix = "";
        // 
        // lblIOMeter
        // 
        lblIOMeter.Dock = DockStyle.Fill;
        lblIOMeter.Location = new Point(7, 182);
        lblIOMeter.Margin = new Padding(7, 1, 3, 0);
        lblIOMeter.Name = "lblIOMeter";
        lblIOMeter.Size = new Size(70, 15);
        lblIOMeter.TabIndex = 9;
        lblIOMeter.Text = "I/O Usage";
        lblIOMeter.TextAlign = ContentAlignment.BottomCenter;
        // 
        // lblIOChart
        // 
        lblIOChart.Dock = DockStyle.Fill;
        lblIOChart.Location = new Point(83, 182);
        lblIOChart.Margin = new Padding(3, 1, 3, 0);
        lblIOChart.Name = "lblIOChart";
        lblIOChart.Size = new Size(514, 15);
        lblIOChart.TabIndex = 10;
        lblIOChart.Text = "I/O Transfer History";
        lblIOChart.TextAlign = ContentAlignment.BottomLeft;
        // 
        // meterIO
        // 
        meterIO.BackColor = Color.Black;
        meterIO.BarBackColor = Color.MidnightBlue;
        meterIO.BarForeColor = Color.DodgerBlue;
        meterIO.BorderStyle = Border3DStyle.Sunken;
        meterIO.Dock = DockStyle.Fill;
        meterIO.HistoryValues = 10;
        meterIO.LightColors = false;
        meterIO.Location = new Point(7, 199);
        meterIO.Margin = new Padding(7, 2, 3, 2);
        meterIO.Name = "meterIO";
        meterIO.ScaleMode = sMkPerfMeter.ScaleModes.Relative;
        meterIO.Size = new Size(70, 69);
        meterIO.TabIndex = 11;
        meterIO.TabStop = false;
        // 
        // chartIO
        // 
        chartIO.AntiAliasing = false;
        chartIO.BackColor = Color.Black;
        chartIO.BackColorShade = Color.FromArgb(0, 0, 0);
        chartIO.BackSolid = false;
        chartIO.BorderStyle = Border3DStyle.Sunken;
        chartIO.DetailsOnHover = false;
        chartIO.DisplayAverage = false;
        chartIO.DisplayIndexes = false;
        chartIO.DisplayLegends = false;
        chartIO.Dock = DockStyle.Fill;
        chartIO.GridSpacing = 10;
        chartIO.LegendSpacing = 35;
        chartIO.LightColors = false;
        chartIO.Location = new Point(83, 199);
        chartIO.Margin = new Padding(3, 2, 5, 2);
        chartIO.MaxValue = 0D;
        chartIO.Name = "chartIO";
        chartIO.ScaleMode = sMkPerfChart.ScaleModes.Absolute;
        chartIO.ShadeBackground = true;
        chartIO.Size = new Size(512, 69);
        chartIO.TabIndex = 12;
        chartIO.TabStop = false;
        chartIO.ValueSpacing = 2;
        chartIO.ValuesSuffix = "";
        // 
        // lblDiskMeter
        // 
        lblDiskMeter.Dock = DockStyle.Fill;
        lblDiskMeter.Location = new Point(7, 271);
        lblDiskMeter.Margin = new Padding(7, 1, 3, 0);
        lblDiskMeter.Name = "lblDiskMeter";
        lblDiskMeter.Size = new Size(70, 15);
        lblDiskMeter.TabIndex = 13;
        lblDiskMeter.Text = "Disk Usage";
        lblDiskMeter.TextAlign = ContentAlignment.BottomCenter;
        // 
        // lblDiskChart
        // 
        lblDiskChart.Dock = DockStyle.Fill;
        lblDiskChart.Location = new Point(83, 271);
        lblDiskChart.Margin = new Padding(3, 1, 3, 0);
        lblDiskChart.Name = "lblDiskChart";
        lblDiskChart.Size = new Size(514, 15);
        lblDiskChart.TabIndex = 14;
        lblDiskChart.Text = "Disk Transfer History";
        lblDiskChart.TextAlign = ContentAlignment.BottomLeft;
        // 
        // meterDisk
        // 
        meterDisk.BackColor = Color.Black;
        meterDisk.BarBackColor = Color.Sienna;
        meterDisk.BarForeColor = Color.SandyBrown;
        meterDisk.BorderStyle = Border3DStyle.Sunken;
        meterDisk.Dock = DockStyle.Fill;
        meterDisk.HistoryValues = 10;
        meterDisk.LightColors = false;
        meterDisk.Location = new Point(7, 288);
        meterDisk.Margin = new Padding(7, 2, 3, 2);
        meterDisk.Name = "meterDisk";
        meterDisk.ScaleMode = sMkPerfMeter.ScaleModes.Relative;
        meterDisk.Size = new Size(70, 69);
        meterDisk.TabIndex = 15;
        meterDisk.TabStop = false;
        // 
        // chartDisk
        // 
        chartDisk.AntiAliasing = false;
        chartDisk.BackColor = Color.Black;
        chartDisk.BackColorShade = Color.FromArgb(0, 0, 0);
        chartDisk.BackSolid = false;
        chartDisk.BorderStyle = Border3DStyle.Sunken;
        chartDisk.DetailsOnHover = false;
        chartDisk.DisplayAverage = false;
        chartDisk.DisplayIndexes = false;
        chartDisk.DisplayLegends = false;
        chartDisk.Dock = DockStyle.Fill;
        chartDisk.GridSpacing = 10;
        chartDisk.LegendSpacing = 35;
        chartDisk.LightColors = false;
        chartDisk.Location = new Point(83, 288);
        chartDisk.Margin = new Padding(3, 2, 5, 2);
        chartDisk.MaxValue = 0D;
        chartDisk.Name = "chartDisk";
        chartDisk.ScaleMode = sMkPerfChart.ScaleModes.Absolute;
        chartDisk.ShadeBackground = true;
        chartDisk.Size = new Size(512, 69);
        chartDisk.TabIndex = 16;
        chartDisk.TabStop = false;
        chartDisk.ValueSpacing = 2;
        chartDisk.ValuesSuffix = "";
        // 
        // lblNetMeter
        // 
        lblNetMeter.Dock = DockStyle.Fill;
        lblNetMeter.Location = new Point(7, 360);
        lblNetMeter.Margin = new Padding(7, 1, 3, 0);
        lblNetMeter.Name = "lblNetMeter";
        lblNetMeter.Size = new Size(70, 15);
        lblNetMeter.TabIndex = 17;
        lblNetMeter.Text = "Net Usage";
        lblNetMeter.TextAlign = ContentAlignment.BottomCenter;
        // 
        // lblNetChart
        // 
        lblNetChart.Dock = DockStyle.Fill;
        lblNetChart.Location = new Point(83, 360);
        lblNetChart.Margin = new Padding(3, 1, 3, 0);
        lblNetChart.Name = "lblNetChart";
        lblNetChart.Size = new Size(514, 15);
        lblNetChart.TabIndex = 18;
        lblNetChart.Text = "Network Transfer History";
        lblNetChart.TextAlign = ContentAlignment.BottomLeft;
        // 
        // meterNet
        // 
        meterNet.BackColor = Color.Black;
        meterNet.BarBackColor = Color.Indigo;
        meterNet.BarForeColor = Color.Violet;
        meterNet.BorderStyle = Border3DStyle.Sunken;
        meterNet.Dock = DockStyle.Fill;
        meterNet.HistoryValues = 10;
        meterNet.LightColors = false;
        meterNet.Location = new Point(7, 377);
        meterNet.Margin = new Padding(7, 2, 3, 2);
        meterNet.Name = "meterNet";
        meterNet.ScaleMode = sMkPerfMeter.ScaleModes.Relative;
        meterNet.Size = new Size(70, 69);
        meterNet.TabIndex = 19;
        meterNet.TabStop = false;
        // 
        // chartNet
        // 
        chartNet.AntiAliasing = false;
        chartNet.BackColor = Color.Black;
        chartNet.BackColorShade = Color.FromArgb(0, 0, 0);
        chartNet.BackSolid = false;
        chartNet.BorderStyle = Border3DStyle.Sunken;
        chartNet.DetailsOnHover = false;
        chartNet.DisplayAverage = false;
        chartNet.DisplayIndexes = false;
        chartNet.DisplayLegends = false;
        chartNet.Dock = DockStyle.Fill;
        chartNet.GridSpacing = 10;
        chartNet.LegendSpacing = 35;
        chartNet.LightColors = false;
        chartNet.Location = new Point(83, 377);
        chartNet.Margin = new Padding(3, 2, 5, 2);
        chartNet.MaxValue = 0D;
        chartNet.Name = "chartNet";
        chartNet.ScaleMode = sMkPerfChart.ScaleModes.Absolute;
        chartNet.ShadeBackground = true;
        chartNet.Size = new Size(512, 69);
        chartNet.TabIndex = 20;
        chartNet.TabStop = false;
        chartNet.ValueSpacing = 2;
        chartNet.ValuesSuffix = "";
        // 
        // tlpDetails
        // 
        tlpDetails.ColumnCount = 4;
        tlpMain.SetColumnSpan(tlpDetails, 2);
        tlpDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tlpDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tlpDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tlpDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        tlpDetails.Controls.Add(gbSystem, 3, 0);
        tlpDetails.Controls.Add(gbIOtranf, 2, 1);
        tlpDetails.Controls.Add(gbCommit, 0, 1);
        tlpDetails.Controls.Add(gbPagefile, 1, 1);
        tlpDetails.Controls.Add(gbKernel, 1, 0);
        tlpDetails.Controls.Add(gbIOops, 2, 0);
        tlpDetails.Controls.Add(gbMemory, 0, 0);
        tlpDetails.Dock = DockStyle.Fill;
        tlpDetails.Location = new Point(0, 448);
        tlpDetails.Margin = new Padding(0);
        tlpDetails.Name = "tlpDetails";
        tlpDetails.RowCount = 2;
        tlpDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlpDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlpDetails.Size = new Size(600, 152);
        tlpDetails.TabIndex = 20;
        // 
        // gbSystem
        // 
        gbSystem.Controls.Add(gbSystem_Label6);
        gbSystem.Controls.Add(gbSystem_Label5);
        gbSystem.Controls.Add(gbSystem_Label4);
        gbSystem.Controls.Add(gbSystem_Label3);
        gbSystem.Controls.Add(gbSystem_Label2);
        gbSystem.Controls.Add(gbSystem_Label1);
        gbSystem.Controls.Add(gbSystem_UpTime);
        gbSystem.Controls.Add(gbSystem_Devices);
        gbSystem.Controls.Add(gbSystem_Services);
        gbSystem.Controls.Add(gbSystem_Processes);
        gbSystem.Controls.Add(gbSystem_Threads);
        gbSystem.Controls.Add(gbSystem_Handles);
        gbSystem.Dock = DockStyle.Top;
        gbSystem.ForeColor = SystemColors.HotTrack;
        gbSystem.Location = new Point(453, 3);
        gbSystem.Name = "gbSystem";
        gbSystem.Padding = new Padding(6, 2, 1, 3);
        tlpDetails.SetRowSpan(gbSystem, 2);
        gbSystem.Size = new Size(144, 120);
        gbSystem.TabIndex = 0;
        gbSystem.TabStop = false;
        gbSystem.Text = "System Totals";
        // 
        // gbSystem_Label6
        // 
        gbSystem_Label6.ForeColor = SystemColors.ControlText;
        gbSystem_Label6.Location = new Point(9, 93);
        gbSystem_Label6.Name = "gbSystem_Label6";
        gbSystem_Label6.Size = new Size(60, 15);
        gbSystem_Label6.TabIndex = 0;
        gbSystem_Label6.Text = "Up Time:";
        // 
        // gbSystem_Label5
        // 
        gbSystem_Label5.ForeColor = SystemColors.ControlText;
        gbSystem_Label5.Location = new Point(9, 78);
        gbSystem_Label5.Name = "gbSystem_Label5";
        gbSystem_Label5.Size = new Size(60, 15);
        gbSystem_Label5.TabIndex = 1;
        gbSystem_Label5.Text = "Devices:";
        // 
        // gbSystem_Label4
        // 
        gbSystem_Label4.ForeColor = SystemColors.ControlText;
        gbSystem_Label4.Location = new Point(9, 63);
        gbSystem_Label4.Name = "gbSystem_Label4";
        gbSystem_Label4.Size = new Size(60, 15);
        gbSystem_Label4.TabIndex = 2;
        gbSystem_Label4.Text = "Services:";
        // 
        // gbSystem_Label3
        // 
        gbSystem_Label3.ForeColor = SystemColors.ControlText;
        gbSystem_Label3.Location = new Point(9, 48);
        gbSystem_Label3.Name = "gbSystem_Label3";
        gbSystem_Label3.Size = new Size(65, 15);
        gbSystem_Label3.TabIndex = 3;
        gbSystem_Label3.Text = "Processes:";
        // 
        // gbSystem_Label2
        // 
        gbSystem_Label2.ForeColor = SystemColors.ControlText;
        gbSystem_Label2.Location = new Point(9, 33);
        gbSystem_Label2.Name = "gbSystem_Label2";
        gbSystem_Label2.Size = new Size(60, 15);
        gbSystem_Label2.TabIndex = 4;
        gbSystem_Label2.Text = "Threads:";
        // 
        // gbSystem_Label1
        // 
        gbSystem_Label1.ForeColor = SystemColors.ControlText;
        gbSystem_Label1.Location = new Point(9, 18);
        gbSystem_Label1.Name = "gbSystem_Label1";
        gbSystem_Label1.Size = new Size(60, 15);
        gbSystem_Label1.TabIndex = 5;
        gbSystem_Label1.Text = "Handles:";
        // 
        // gbSystem_UpTime
        // 
        gbSystem_UpTime.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbSystem_UpTime.ForeColor = SystemColors.ControlText;
        gbSystem_UpTime.Location = new Point(65, 93);
        gbSystem_UpTime.Name = "gbSystem_UpTime";
        gbSystem_UpTime.Size = new Size(75, 15);
        gbSystem_UpTime.TabIndex = 6;
        gbSystem_UpTime.Text = "0";
        gbSystem_UpTime.TextAlign = ContentAlignment.TopRight;
        // 
        // gbSystem_Devices
        // 
        gbSystem_Devices.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbSystem_Devices.ForeColor = SystemColors.ControlText;
        gbSystem_Devices.Location = new Point(65, 78);
        gbSystem_Devices.Name = "gbSystem_Devices";
        gbSystem_Devices.Size = new Size(75, 15);
        gbSystem_Devices.TabIndex = 7;
        gbSystem_Devices.Text = "0";
        gbSystem_Devices.TextAlign = ContentAlignment.TopRight;
        // 
        // gbSystem_Services
        // 
        gbSystem_Services.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbSystem_Services.ForeColor = SystemColors.ControlText;
        gbSystem_Services.Location = new Point(65, 63);
        gbSystem_Services.Name = "gbSystem_Services";
        gbSystem_Services.Size = new Size(75, 15);
        gbSystem_Services.TabIndex = 8;
        gbSystem_Services.Text = "0";
        gbSystem_Services.TextAlign = ContentAlignment.TopRight;
        // 
        // gbSystem_Processes
        // 
        gbSystem_Processes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbSystem_Processes.ForeColor = SystemColors.ControlText;
        gbSystem_Processes.Location = new Point(70, 48);
        gbSystem_Processes.Name = "gbSystem_Processes";
        gbSystem_Processes.Size = new Size(70, 15);
        gbSystem_Processes.TabIndex = 9;
        gbSystem_Processes.Text = "0";
        gbSystem_Processes.TextAlign = ContentAlignment.TopRight;
        // 
        // gbSystem_Threads
        // 
        gbSystem_Threads.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbSystem_Threads.ForeColor = SystemColors.ControlText;
        gbSystem_Threads.Location = new Point(65, 33);
        gbSystem_Threads.Name = "gbSystem_Threads";
        gbSystem_Threads.Size = new Size(75, 15);
        gbSystem_Threads.TabIndex = 10;
        gbSystem_Threads.Text = "0";
        gbSystem_Threads.TextAlign = ContentAlignment.TopRight;
        // 
        // gbSystem_Handles
        // 
        gbSystem_Handles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbSystem_Handles.ForeColor = SystemColors.ControlText;
        gbSystem_Handles.Location = new Point(65, 18);
        gbSystem_Handles.Name = "gbSystem_Handles";
        gbSystem_Handles.Size = new Size(75, 15);
        gbSystem_Handles.TabIndex = 11;
        gbSystem_Handles.Text = "0";
        gbSystem_Handles.TextAlign = ContentAlignment.TopRight;
        // 
        // gbIOtranf
        // 
        gbIOtranf.Controls.Add(gbIOtranf_Label3);
        gbIOtranf.Controls.Add(gbIOtranf_Others);
        gbIOtranf.Controls.Add(gbIOtranf_Label2);
        gbIOtranf.Controls.Add(gbIOtranf_Label1);
        gbIOtranf.Controls.Add(gbIOtranf_Writes);
        gbIOtranf.Controls.Add(gbIOtranf_Reads);
        gbIOtranf.Dock = DockStyle.Fill;
        gbIOtranf.ForeColor = SystemColors.HotTrack;
        gbIOtranf.Location = new Point(303, 79);
        gbIOtranf.Name = "gbIOtranf";
        gbIOtranf.Padding = new Padding(6, 2, 1, 3);
        gbIOtranf.Size = new Size(144, 70);
        gbIOtranf.TabIndex = 1;
        gbIOtranf.TabStop = false;
        gbIOtranf.Text = "I/O Transfer Delta";
        // 
        // gbIOtranf_Label3
        // 
        gbIOtranf_Label3.ForeColor = SystemColors.ControlText;
        gbIOtranf_Label3.Location = new Point(9, 48);
        gbIOtranf_Label3.Name = "gbIOtranf_Label3";
        gbIOtranf_Label3.Size = new Size(60, 15);
        gbIOtranf_Label3.TabIndex = 0;
        gbIOtranf_Label3.Text = "Others:";
        // 
        // gbIOtranf_Others
        // 
        gbIOtranf_Others.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbIOtranf_Others.ForeColor = SystemColors.ControlText;
        gbIOtranf_Others.Location = new Point(65, 48);
        gbIOtranf_Others.Name = "gbIOtranf_Others";
        gbIOtranf_Others.Size = new Size(75, 15);
        gbIOtranf_Others.TabIndex = 1;
        gbIOtranf_Others.Text = "0 Kb.";
        gbIOtranf_Others.TextAlign = ContentAlignment.TopRight;
        // 
        // gbIOtranf_Label2
        // 
        gbIOtranf_Label2.ForeColor = SystemColors.ControlText;
        gbIOtranf_Label2.Location = new Point(9, 33);
        gbIOtranf_Label2.Name = "gbIOtranf_Label2";
        gbIOtranf_Label2.Size = new Size(60, 15);
        gbIOtranf_Label2.TabIndex = 2;
        gbIOtranf_Label2.Text = "Writes:";
        // 
        // gbIOtranf_Label1
        // 
        gbIOtranf_Label1.ForeColor = SystemColors.ControlText;
        gbIOtranf_Label1.Location = new Point(9, 18);
        gbIOtranf_Label1.Name = "gbIOtranf_Label1";
        gbIOtranf_Label1.Size = new Size(60, 15);
        gbIOtranf_Label1.TabIndex = 3;
        gbIOtranf_Label1.Text = "Reads:";
        // 
        // gbIOtranf_Writes
        // 
        gbIOtranf_Writes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbIOtranf_Writes.ForeColor = SystemColors.ControlText;
        gbIOtranf_Writes.Location = new Point(65, 33);
        gbIOtranf_Writes.Name = "gbIOtranf_Writes";
        gbIOtranf_Writes.Size = new Size(75, 15);
        gbIOtranf_Writes.TabIndex = 4;
        gbIOtranf_Writes.Text = "0 Kb.";
        gbIOtranf_Writes.TextAlign = ContentAlignment.TopRight;
        // 
        // gbIOtranf_Reads
        // 
        gbIOtranf_Reads.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbIOtranf_Reads.ForeColor = SystemColors.ControlText;
        gbIOtranf_Reads.Location = new Point(65, 18);
        gbIOtranf_Reads.Name = "gbIOtranf_Reads";
        gbIOtranf_Reads.Size = new Size(75, 15);
        gbIOtranf_Reads.TabIndex = 5;
        gbIOtranf_Reads.Text = "0 Kb.";
        gbIOtranf_Reads.TextAlign = ContentAlignment.TopRight;
        // 
        // gbCommit
        // 
        gbCommit.Controls.Add(gbCommit_Label3);
        gbCommit.Controls.Add(gbCommit_Peak);
        gbCommit.Controls.Add(gbCommit_Label2);
        gbCommit.Controls.Add(gbCommit_Label1);
        gbCommit.Controls.Add(gbCommit_Current);
        gbCommit.Controls.Add(gbCommit_Limit);
        gbCommit.Dock = DockStyle.Fill;
        gbCommit.ForeColor = SystemColors.HotTrack;
        gbCommit.Location = new Point(3, 79);
        gbCommit.Name = "gbCommit";
        gbCommit.Padding = new Padding(6, 2, 1, 3);
        gbCommit.Size = new Size(144, 70);
        gbCommit.TabIndex = 2;
        gbCommit.TabStop = false;
        gbCommit.Text = "Commit Charge";
        // 
        // gbCommit_Label3
        // 
        gbCommit_Label3.ForeColor = SystemColors.ControlText;
        gbCommit_Label3.Location = new Point(9, 48);
        gbCommit_Label3.Name = "gbCommit_Label3";
        gbCommit_Label3.Size = new Size(55, 15);
        gbCommit_Label3.TabIndex = 0;
        gbCommit_Label3.Text = "Peak:";
        // 
        // gbCommit_Peak
        // 
        gbCommit_Peak.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbCommit_Peak.ForeColor = SystemColors.ControlText;
        gbCommit_Peak.Location = new Point(65, 48);
        gbCommit_Peak.Name = "gbCommit_Peak";
        gbCommit_Peak.Size = new Size(75, 15);
        gbCommit_Peak.TabIndex = 1;
        gbCommit_Peak.Text = "0 Kb.";
        gbCommit_Peak.TextAlign = ContentAlignment.TopRight;
        // 
        // gbCommit_Label2
        // 
        gbCommit_Label2.ForeColor = SystemColors.ControlText;
        gbCommit_Label2.Location = new Point(9, 33);
        gbCommit_Label2.Name = "gbCommit_Label2";
        gbCommit_Label2.Size = new Size(55, 15);
        gbCommit_Label2.TabIndex = 2;
        gbCommit_Label2.Text = "Current:";
        // 
        // gbCommit_Label1
        // 
        gbCommit_Label1.ForeColor = SystemColors.ControlText;
        gbCommit_Label1.Location = new Point(9, 18);
        gbCommit_Label1.Name = "gbCommit_Label1";
        gbCommit_Label1.Size = new Size(55, 15);
        gbCommit_Label1.TabIndex = 3;
        gbCommit_Label1.Text = "Limit:";
        // 
        // gbCommit_Current
        // 
        gbCommit_Current.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbCommit_Current.ForeColor = SystemColors.ControlText;
        gbCommit_Current.Location = new Point(65, 33);
        gbCommit_Current.Name = "gbCommit_Current";
        gbCommit_Current.Size = new Size(75, 15);
        gbCommit_Current.TabIndex = 4;
        gbCommit_Current.Text = "0 Kb.";
        gbCommit_Current.TextAlign = ContentAlignment.TopRight;
        // 
        // gbCommit_Limit
        // 
        gbCommit_Limit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbCommit_Limit.ForeColor = SystemColors.ControlText;
        gbCommit_Limit.Location = new Point(65, 18);
        gbCommit_Limit.Name = "gbCommit_Limit";
        gbCommit_Limit.Size = new Size(75, 15);
        gbCommit_Limit.TabIndex = 5;
        gbCommit_Limit.Text = "0 Kb.";
        gbCommit_Limit.TextAlign = ContentAlignment.TopRight;
        // 
        // gbPagefile
        // 
        gbPagefile.Controls.Add(gbPagefile_Label3);
        gbPagefile.Controls.Add(gbPagefile_Peak);
        gbPagefile.Controls.Add(gbPagefile_Label2);
        gbPagefile.Controls.Add(gbPagefile_Label1);
        gbPagefile.Controls.Add(gbPagefile_Current);
        gbPagefile.Controls.Add(gbPagefile_Limit);
        gbPagefile.Dock = DockStyle.Fill;
        gbPagefile.ForeColor = SystemColors.HotTrack;
        gbPagefile.Location = new Point(153, 79);
        gbPagefile.Name = "gbPagefile";
        gbPagefile.Padding = new Padding(6, 2, 1, 3);
        gbPagefile.Size = new Size(144, 70);
        gbPagefile.TabIndex = 3;
        gbPagefile.TabStop = false;
        gbPagefile.Text = "Pagefile Usage";
        // 
        // gbPagefile_Label3
        // 
        gbPagefile_Label3.ForeColor = SystemColors.ControlText;
        gbPagefile_Label3.Location = new Point(9, 48);
        gbPagefile_Label3.Name = "gbPagefile_Label3";
        gbPagefile_Label3.Size = new Size(60, 15);
        gbPagefile_Label3.TabIndex = 0;
        gbPagefile_Label3.Text = "Peak:";
        // 
        // gbPagefile_Peak
        // 
        gbPagefile_Peak.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbPagefile_Peak.ForeColor = SystemColors.ControlText;
        gbPagefile_Peak.Location = new Point(65, 48);
        gbPagefile_Peak.Name = "gbPagefile_Peak";
        gbPagefile_Peak.Size = new Size(75, 15);
        gbPagefile_Peak.TabIndex = 1;
        gbPagefile_Peak.Text = "0 Kb.";
        gbPagefile_Peak.TextAlign = ContentAlignment.TopRight;
        // 
        // gbPagefile_Label2
        // 
        gbPagefile_Label2.ForeColor = SystemColors.ControlText;
        gbPagefile_Label2.Location = new Point(9, 33);
        gbPagefile_Label2.Name = "gbPagefile_Label2";
        gbPagefile_Label2.Size = new Size(60, 15);
        gbPagefile_Label2.TabIndex = 2;
        gbPagefile_Label2.Text = "Current:";
        // 
        // gbPagefile_Label1
        // 
        gbPagefile_Label1.ForeColor = SystemColors.ControlText;
        gbPagefile_Label1.Location = new Point(9, 18);
        gbPagefile_Label1.Name = "gbPagefile_Label1";
        gbPagefile_Label1.Size = new Size(60, 15);
        gbPagefile_Label1.TabIndex = 3;
        gbPagefile_Label1.Text = "Limit:";
        // 
        // gbPagefile_Current
        // 
        gbPagefile_Current.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbPagefile_Current.ForeColor = SystemColors.ControlText;
        gbPagefile_Current.Location = new Point(65, 33);
        gbPagefile_Current.Name = "gbPagefile_Current";
        gbPagefile_Current.Size = new Size(75, 15);
        gbPagefile_Current.TabIndex = 4;
        gbPagefile_Current.Text = "0 Kb.";
        gbPagefile_Current.TextAlign = ContentAlignment.TopRight;
        // 
        // gbPagefile_Limit
        // 
        gbPagefile_Limit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbPagefile_Limit.ForeColor = SystemColors.ControlText;
        gbPagefile_Limit.Location = new Point(65, 18);
        gbPagefile_Limit.Name = "gbPagefile_Limit";
        gbPagefile_Limit.Size = new Size(75, 15);
        gbPagefile_Limit.TabIndex = 5;
        gbPagefile_Limit.Text = "0 Kb.";
        gbPagefile_Limit.TextAlign = ContentAlignment.TopRight;
        // 
        // gbKernel
        // 
        gbKernel.Controls.Add(gbKernel_Label3);
        gbKernel.Controls.Add(gbKernel_NonPaged);
        gbKernel.Controls.Add(gbKernel_Label2);
        gbKernel.Controls.Add(gbKernel_Label1);
        gbKernel.Controls.Add(gbKernel_Paged);
        gbKernel.Controls.Add(gbKernel_Total);
        gbKernel.Dock = DockStyle.Fill;
        gbKernel.ForeColor = SystemColors.HotTrack;
        gbKernel.Location = new Point(153, 3);
        gbKernel.Name = "gbKernel";
        gbKernel.Padding = new Padding(6, 2, 1, 3);
        gbKernel.Size = new Size(144, 70);
        gbKernel.TabIndex = 4;
        gbKernel.TabStop = false;
        gbKernel.Text = "Kernel Memory";
        // 
        // gbKernel_Label3
        // 
        gbKernel_Label3.ForeColor = SystemColors.ControlText;
        gbKernel_Label3.Location = new Point(9, 48);
        gbKernel_Label3.Name = "gbKernel_Label3";
        gbKernel_Label3.Size = new Size(70, 15);
        gbKernel_Label3.TabIndex = 0;
        gbKernel_Label3.Text = "NonPaged:";
        // 
        // gbKernel_NonPaged
        // 
        gbKernel_NonPaged.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbKernel_NonPaged.ForeColor = SystemColors.ControlText;
        gbKernel_NonPaged.Location = new Point(65, 48);
        gbKernel_NonPaged.Name = "gbKernel_NonPaged";
        gbKernel_NonPaged.Size = new Size(75, 15);
        gbKernel_NonPaged.TabIndex = 1;
        gbKernel_NonPaged.Text = "0 Kb.";
        gbKernel_NonPaged.TextAlign = ContentAlignment.TopRight;
        // 
        // gbKernel_Label2
        // 
        gbKernel_Label2.ForeColor = SystemColors.ControlText;
        gbKernel_Label2.Location = new Point(9, 33);
        gbKernel_Label2.Name = "gbKernel_Label2";
        gbKernel_Label2.Size = new Size(60, 15);
        gbKernel_Label2.TabIndex = 2;
        gbKernel_Label2.Text = "Paged:";
        // 
        // gbKernel_Label1
        // 
        gbKernel_Label1.ForeColor = SystemColors.ControlText;
        gbKernel_Label1.Location = new Point(9, 18);
        gbKernel_Label1.Name = "gbKernel_Label1";
        gbKernel_Label1.Size = new Size(60, 15);
        gbKernel_Label1.TabIndex = 3;
        gbKernel_Label1.Text = "Total:";
        // 
        // gbKernel_Paged
        // 
        gbKernel_Paged.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbKernel_Paged.ForeColor = SystemColors.ControlText;
        gbKernel_Paged.Location = new Point(65, 33);
        gbKernel_Paged.Name = "gbKernel_Paged";
        gbKernel_Paged.Size = new Size(75, 15);
        gbKernel_Paged.TabIndex = 4;
        gbKernel_Paged.Text = "0 Kb.";
        gbKernel_Paged.TextAlign = ContentAlignment.TopRight;
        // 
        // gbKernel_Total
        // 
        gbKernel_Total.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbKernel_Total.ForeColor = SystemColors.ControlText;
        gbKernel_Total.Location = new Point(65, 18);
        gbKernel_Total.Name = "gbKernel_Total";
        gbKernel_Total.Size = new Size(75, 15);
        gbKernel_Total.TabIndex = 5;
        gbKernel_Total.Text = "0 Kb.";
        gbKernel_Total.TextAlign = ContentAlignment.TopRight;
        // 
        // gbIOops
        // 
        gbIOops.Controls.Add(gbIOops_Label3);
        gbIOops.Controls.Add(gbIOops_Others);
        gbIOops.Controls.Add(gbIOops_Label2);
        gbIOops.Controls.Add(gbIOops_Label1);
        gbIOops.Controls.Add(gbIOops_Writes);
        gbIOops.Controls.Add(gbIOops_Reads);
        gbIOops.Dock = DockStyle.Fill;
        gbIOops.ForeColor = SystemColors.HotTrack;
        gbIOops.Location = new Point(303, 3);
        gbIOops.Name = "gbIOops";
        gbIOops.Padding = new Padding(6, 2, 1, 3);
        gbIOops.Size = new Size(144, 70);
        gbIOops.TabIndex = 5;
        gbIOops.TabStop = false;
        gbIOops.Text = "I/O Operations Delta";
        // 
        // gbIOops_Label3
        // 
        gbIOops_Label3.ForeColor = SystemColors.ControlText;
        gbIOops_Label3.Location = new Point(9, 48);
        gbIOops_Label3.Name = "gbIOops_Label3";
        gbIOops_Label3.Size = new Size(60, 15);
        gbIOops_Label3.TabIndex = 0;
        gbIOops_Label3.Text = "Others:";
        // 
        // gbIOops_Others
        // 
        gbIOops_Others.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbIOops_Others.ForeColor = SystemColors.ControlText;
        gbIOops_Others.Location = new Point(65, 48);
        gbIOops_Others.Name = "gbIOops_Others";
        gbIOops_Others.Size = new Size(75, 15);
        gbIOops_Others.TabIndex = 1;
        gbIOops_Others.Text = "0";
        gbIOops_Others.TextAlign = ContentAlignment.TopRight;
        // 
        // gbIOops_Label2
        // 
        gbIOops_Label2.ForeColor = SystemColors.ControlText;
        gbIOops_Label2.Location = new Point(9, 33);
        gbIOops_Label2.Name = "gbIOops_Label2";
        gbIOops_Label2.Size = new Size(60, 15);
        gbIOops_Label2.TabIndex = 2;
        gbIOops_Label2.Text = "Writes:";
        // 
        // gbIOops_Label1
        // 
        gbIOops_Label1.ForeColor = SystemColors.ControlText;
        gbIOops_Label1.Location = new Point(9, 18);
        gbIOops_Label1.Name = "gbIOops_Label1";
        gbIOops_Label1.Size = new Size(60, 15);
        gbIOops_Label1.TabIndex = 3;
        gbIOops_Label1.Text = "Reads:";
        // 
        // gbIOops_Writes
        // 
        gbIOops_Writes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbIOops_Writes.ForeColor = SystemColors.ControlText;
        gbIOops_Writes.Location = new Point(65, 33);
        gbIOops_Writes.Name = "gbIOops_Writes";
        gbIOops_Writes.Size = new Size(75, 15);
        gbIOops_Writes.TabIndex = 4;
        gbIOops_Writes.Text = "0";
        gbIOops_Writes.TextAlign = ContentAlignment.TopRight;
        // 
        // gbIOops_Reads
        // 
        gbIOops_Reads.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbIOops_Reads.ForeColor = SystemColors.ControlText;
        gbIOops_Reads.Location = new Point(65, 18);
        gbIOops_Reads.Name = "gbIOops_Reads";
        gbIOops_Reads.Size = new Size(75, 15);
        gbIOops_Reads.TabIndex = 3;
        gbIOops_Reads.TextAlign = ContentAlignment.TopRight;
        // 
        // gbMemory
        // 
        gbMemory.Controls.Add(gbMemory_Label3);
        gbMemory.Controls.Add(gbMemory_Cached);
        gbMemory.Controls.Add(gbMemory_Label2);
        gbMemory.Controls.Add(gbMemory_Label1);
        gbMemory.Controls.Add(gbMemory_Avail);
        gbMemory.Controls.Add(gbMemory_Total);
        gbMemory.Dock = DockStyle.Fill;
        gbMemory.ForeColor = SystemColors.HotTrack;
        gbMemory.Location = new Point(3, 3);
        gbMemory.Name = "gbMemory";
        gbMemory.Padding = new Padding(6, 2, 1, 3);
        gbMemory.Size = new Size(144, 70);
        gbMemory.TabIndex = 6;
        gbMemory.TabStop = false;
        gbMemory.Text = "Physical Memory";
        // 
        // gbMemory_Label3
        // 
        gbMemory_Label3.ForeColor = SystemColors.ControlText;
        gbMemory_Label3.Location = new Point(9, 48);
        gbMemory_Label3.Name = "gbMemory_Label3";
        gbMemory_Label3.Size = new Size(55, 15);
        gbMemory_Label3.TabIndex = 0;
        gbMemory_Label3.Text = "Cached:";
        // 
        // gbMemory_Cached
        // 
        gbMemory_Cached.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbMemory_Cached.ForeColor = SystemColors.ControlText;
        gbMemory_Cached.Location = new Point(65, 48);
        gbMemory_Cached.Name = "gbMemory_Cached";
        gbMemory_Cached.Size = new Size(75, 15);
        gbMemory_Cached.TabIndex = 1;
        gbMemory_Cached.Text = "0 Kb.";
        gbMemory_Cached.TextAlign = ContentAlignment.TopRight;
        // 
        // gbMemory_Label2
        // 
        gbMemory_Label2.ForeColor = SystemColors.ControlText;
        gbMemory_Label2.Location = new Point(9, 33);
        gbMemory_Label2.Name = "gbMemory_Label2";
        gbMemory_Label2.Size = new Size(60, 15);
        gbMemory_Label2.TabIndex = 2;
        gbMemory_Label2.Text = "Available:";
        // 
        // gbMemory_Label1
        // 
        gbMemory_Label1.ForeColor = SystemColors.ControlText;
        gbMemory_Label1.Location = new Point(9, 18);
        gbMemory_Label1.Name = "gbMemory_Label1";
        gbMemory_Label1.Size = new Size(60, 15);
        gbMemory_Label1.TabIndex = 3;
        gbMemory_Label1.Text = "Total:";
        // 
        // gbMemory_Avail
        // 
        gbMemory_Avail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbMemory_Avail.ForeColor = SystemColors.ControlText;
        gbMemory_Avail.Location = new Point(65, 33);
        gbMemory_Avail.Name = "gbMemory_Avail";
        gbMemory_Avail.Size = new Size(75, 15);
        gbMemory_Avail.TabIndex = 4;
        gbMemory_Avail.Text = "0 Kb.";
        gbMemory_Avail.TextAlign = ContentAlignment.TopRight;
        // 
        // gbMemory_Total
        // 
        gbMemory_Total.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbMemory_Total.ForeColor = SystemColors.ControlText;
        gbMemory_Total.Location = new Point(65, 18);
        gbMemory_Total.Name = "gbMemory_Total";
        gbMemory_Total.Size = new Size(75, 15);
        gbMemory_Total.TabIndex = 5;
        gbMemory_Total.Text = "0 Kb.";
        gbMemory_Total.TextAlign = ContentAlignment.TopRight;
        // 
        // tabPerformance
        // 
        Controls.Add(tlpMain);
        Name = "tabPerformance";
        Size = new Size(600, 600);
        Load += OnLoad;
        VisibleChanged += OnVisibleChanged;
        tlpMain.ResumeLayout(false);
        tlpDetails.ResumeLayout(false);
        gbSystem.ResumeLayout(false);
        gbIOtranf.ResumeLayout(false);
        gbCommit.ResumeLayout(false);
        gbPagefile.ResumeLayout(false);
        gbKernel.ResumeLayout(false);
        gbIOops.ResumeLayout(false);
        gbMemory.ResumeLayout(false);
        ResumeLayout(false);
    }
    private void InitializeSettings() {
        chartCpu.SetIndexes("Total", "Kernel");
        chartCpu.BackColorShade = Color.FromArgb(0, 50, 0);
        chartCpu.PenGraph1.Color = Color.Lime;
        chartCpu.ScaleMode = sMkPerfChart.ScaleModes.Absolute;

        chartMem.SetIndexes("Memory", "PageFile");
        chartMem.BackColorShade = Color.FromArgb(50, 50, 0);
        chartMem.PenGraph1.Color = Color.Yellow;
        chartMem.PenGraph2.Color = Color.DarkCyan;
        chartMem.ScaleMode = sMkPerfChart.ScaleModes.Absolute;

        chartIO.SetIndexes("Others", "Reads", "Writes");
        chartIO.BackColorShade = Color.FromArgb(0, 0, 64);
        chartIO.PenGraph1.Color = Color.DodgerBlue;
        chartIO.PenGraph3.Color = Color.DarkSeaGreen;
        chartIO.PenGraph2.Color = Color.Salmon;
        chartIO.ScaleMode = sMkPerfChart.ScaleModes.Relative;
        chartIO.ValuesSuffix = " Kb.";

        chartDisk.SetIndexes("Read", "Write");
        chartDisk.BackColorShade = Color.FromArgb(60, 30, 30);
        chartDisk.PenGraph1.Color = Color.Tan;
        chartDisk.PenGraph2.Color = Color.OrangeRed;
        chartDisk.ScaleMode = sMkPerfChart.ScaleModes.Relative;
        chartDisk.ValuesSuffix = " Kb.";

        chartNet.SetIndexes("Receive", "Sent");
        chartNet.BackColorShade = Color.FromArgb(50, 0, 50);
        chartNet.PenGraph1.Color = Color.SkyBlue;
        chartNet.PenGraph2.Color = Color.LightPink;
        chartNet.ScaleMode = sMkPerfChart.ScaleModes.Relative;
        chartNet.ValuesSuffix = " Kb.";
    }

    private void OnLoad(object? sender, EventArgs e) {
        Resize += OnResizeEventHandler;
        GotFocus += OnResizeEventHandler;
        CascadeDoubleClickHandlers(this);
        System.MetricValueChanged += System_MetricValueChanged;
        System.RefreshCompleted += System_RefreshCompleted;
    }
    private void OnVisibleChanged(object? sender, EventArgs e) {
        if (Visible && Shared.InitComplete && _stopWatch.ElapsedTicks == 0) {
            SuspendLayout(); Refresher(true); ResumeLayout();
        }
    }
    private void CascadeDoubleClickHandlers(Control MainControl) {
        foreach (Control c in MainControl.Controls) {
            c.MouseDoubleClick += OnMouseDoubleClickEventHandler;
            if (c.Controls.Count > 1) CascadeDoubleClickHandlers(c);
        }
    }
    private void OnResizeEventHandler(object? sender, EventArgs e) {
        if (ParentForm == null) return;
        if (ParentForm.WindowState == FormWindowState.Minimized) return;
        if (!ParentForm.Visible || !Visible) return;
        // Accomodate the Table Details columns
        if (Width >= 660 && !gbIOops.Visible) {
            tlpDetails.ColumnStyles[0].Width = 25;
            tlpDetails.ColumnStyles[1].Width = 25;
            tlpDetails.ColumnStyles[2].Width = 26;
            tlpDetails.ColumnStyles[3].Width = 24;
            gbIOops.Visible = true;
            gbIOtranf.Visible = true;
        } else if (Width < 660 && gbIOops.Visible) {
            gbIOops.Visible = false;
            gbIOtranf.Visible = false;
            tlpDetails.ColumnStyles[0].Width = 34;
            tlpDetails.ColumnStyles[1].Width = 34;
            tlpDetails.ColumnStyles[2].Width = 0;
            tlpDetails.ColumnStyles[3].Width = 32;
        }
        // Accomodate the Graphs to be displayed
        setMeterVisible(PerformanceMeters.CPU, tlpMain.Height > 270 || FullScreen);
        setMeterVisible(PerformanceMeters.Mem, tlpMain.Height > 270 || FullScreen);
        setMeterVisible(PerformanceMeters.IO, tlpMain.Height > 370 || FullScreen);
        setMeterVisible(PerformanceMeters.Disk, (tlpMain.Height > 470) || (FullScreen && tlpMain.Height > 370));
        setMeterVisible(PerformanceMeters.Net, (tlpMain.Height > 570) || (FullScreen && tlpMain.Height > 470));
    }
    private void OnMouseDoubleClickEventHandler(object? sender, MouseEventArgs e) {
        if (e.Button == MouseButtons.Left) FullScreen = !FullScreen;
        Invoke(OnMouseDoubleClick, e);
    }
    private void OnMouseUpEventHandler(object? sender, MouseEventArgs e) {
        if (FullScreen) Tag = "";
    }
    private void OnMouseDownEventHandler(object? sender, MouseEventArgs e) {
        if (e.Button == MouseButtons.Left && e.Clicks == 1 && FullScreen) {
            LastMousePoint = e.Location;
            Tag = "Drag";
        }
    }
    private void OnMouseMoveEventHandler(object? sender, MouseEventArgs e) {
        if (ParentForm == null) return;
        if (Tag == null || Tag.ToString() != "Drag") return;
        if (e.Button == MouseButtons.Left && FullScreen) {
            ParentForm.Left = ParentForm.Left + (e.Location.X - LastMousePoint.X);
            ParentForm.Top = ParentForm.Top + (e.Location.Y - LastMousePoint.Y);
        }
    }
    private void System_MetricValueChanged(object sender, Metric metric, MetricChangedEventArgs e) {
        switch (e.MetricName) {
            case "PhysicalTotal": gbMemory_Total.Text = System.PhysicalTotal.ValueFmt; break;
            case "PhysicalAvail": gbMemory_Avail.Text = System.PhysicalAvail.ValueFmt; break;
            case "SystemCached": gbMemory_Cached.Text = System.SystemCached.ValueFmt; break;
            case "CommitTotal": gbCommit_Current.Text = System.CommitTotal.ValueFmt; break;
            case "CommitPeak": gbCommit_Peak.Text = System.CommitPeak.ValueFmt; break;
            case "CommitLimit": gbCommit_Limit.Text = System.CommitLimit.ValueFmt; break;
            case "KernelTotal": gbKernel_Total.Text = System.KernelTotal.ValueFmt; break;
            case "KernelPaged": gbKernel_Paged.Text = System.KernelPaged.ValueFmt; break;
            case "KernelNonPaged": gbKernel_NonPaged.Text = System.KernelNonPaged.ValueFmt; break;
            case "PageFileTotal": gbPagefile_Limit.Text = System.PageFileTotal.ValueFmt; break;
            case "PageFileUsed": gbPagefile_Current.Text = System.PageFileUsed.ValueFmt; break;
            case "PageFilePeak": gbPagefile_Peak.Text = System.PageFilePeak.ValueFmt; break;
            case "ioReadCount": gbIOops_Reads.Text = System.ioReadCount.DeltaFmt; break;
            case "ioReadBytes": gbIOtranf_Reads.Text = System.ioReadBytes.DeltaFmt; break;
            case "ioWriteCount": gbIOops_Writes.Text = System.ioWriteCount.DeltaFmt; break;
            case "ioWriteBytes": gbIOtranf_Writes.Text = System.ioWriteBytes.DeltaFmt; break;
            case "ioOtherCount": gbIOops_Others.Text = System.ioOtherCount.DeltaFmt; break;
            case "ioOtherBytes": gbIOtranf_Others.Text = System.ioOtherBytes.DeltaFmt; break;
            case "HandleCount": gbSystem_Handles.Text = System.HandleCount.ValueFmt; break;
            case "ThreadCount": gbSystem_Threads.Text = System.ThreadCount.ValueFmt; break;
            case "ProcessCount": gbSystem_Processes.Text = System.ProcessCount.ValueFmt; break;
            case "DevicesCount": gbSystem_Devices.Text = System.DevicesCount.ValueFmt; break;
            case "ServicesCount": gbSystem_Services.Text = System.ServicesCount.ValueFmt; break;
        }
    }
    private void System_RefreshCompleted(object? sender, EventArgs e) {
        // Always set these values. regardless we are visible or not.
        gbSystem_UpTime.Text = System.UpTime;
        meterCpu.SetValue(System.CpuUsage.Value);
        chartCpu.AddValue(System.CpuUsage.Value, System.CpuUsageKernel.Value);
        meterMem.SetValue(System.MemoryUsage, System.MemoryUsageString);
        chartMem.AddValue((double)System.MemoryUsage, System.SwapUsage);
        meterIO.SetValue(System.ioDataUsage, System.ioDataUsageString);
        chartIO.AddValue((double)System.ioOtherBytes.Delta / 1024, (double)System.ioWriteBytes.Delta / 1024, (double)System.ioReadBytes.Delta / 1024);
        meterDisk.SetValue(System.DiskUsage, System.DiskUsageString);
        chartDisk.AddValue((double)System.DiskRead.Delta / 1024, (double)System.DiskWrite.Delta / 1024);
        meterNet.SetValue(System.NetworkUsage, System.NetworkUsageString);
        chartNet.AddValue((double)System.NetReceived.Delta / 1024, (double)System.NetSent.Delta / 1024);
        // Always flush the ETW, if its active.
        Shared.ETW.Flush();
    }

    private bool FullScreen {
        get { return !tlpDetails.Visible; }
        set {
            tlpDetails.Visible = !value;
            tlpMain.RowStyles[10].Height = value ? 0 : 150;
            OnResizeEventHandler(this, new());
            foreach (Control c in tlpMain.Controls) {
                if (value) { c.MouseMove += OnMouseMoveEventHandler; } else { c.MouseMove -= OnMouseMoveEventHandler; }
                if (value) { c.MouseDown += OnMouseDownEventHandler; } else { c.MouseDown -= OnMouseDownEventHandler; }
                if (value) { c.MouseUp += OnMouseUpEventHandler; } else { c.MouseUp -= OnMouseUpEventHandler; }
            }
        }
    }
    private void setMeterVisible(PerformanceMeters meter, bool visible) {
        var minWidth = 110;
        switch (meter) {
            case PerformanceMeters.CPU:
                lblCpuMeter.Visible = visible;
                meterCpu.Visible = visible;
                lblCpuChart.Visible = visible & Width > minWidth;
                chartCpu.Visible = visible & Width > minWidth;
                tlpMain.RowStyles[0].Height = visible ? 16 : 0;
                tlpMain.RowStyles[1].Height = visible ? 20 : 0;
                break;
            case PerformanceMeters.Mem:
                lblMemMeter.Visible = visible;
                meterMem.Visible = visible;
                lblMemChart.Visible = visible & Width > minWidth;
                chartMem.Visible = visible & Width > minWidth;
                tlpMain.RowStyles[2].Height = visible ? 16 : 0;
                tlpMain.RowStyles[3].Height = visible ? 20 : 0;
                break;
            case PerformanceMeters.IO:
                lblIOMeter.Visible = visible;
                meterIO.Visible = visible;
                lblIOChart.Visible = visible & Width > minWidth;
                chartIO.Visible = visible & Width > minWidth;
                tlpMain.RowStyles[4].Height = visible ? 16 : 0;
                tlpMain.RowStyles[5].Height = visible ? 20 : 0;
                break;
            case PerformanceMeters.Disk:
                lblDiskMeter.Visible = visible;
                meterDisk.Visible = visible;
                lblDiskChart.Visible = visible & Width > minWidth;
                chartDisk.Visible = visible & Width > minWidth;
                tlpMain.RowStyles[6].Height = visible ? 16 : 0;
                tlpMain.RowStyles[7].Height = visible ? 20 : 0;
                break;
            case PerformanceMeters.Net:
                lblNetMeter.Visible = visible;
                meterNet.Visible = visible;
                lblNetChart.Visible = visible & Width > minWidth;
                chartNet.Visible = visible & Width > minWidth;
                tlpMain.RowStyles[8].Height = visible ? 16 : 0;
                tlpMain.RowStyles[9].Height = visible ? 20 : 0;
                break;
        }

    }
    private enum PerformanceMeters {
        CPU = 1,
        Mem = 2,
        IO = 3,
        Disk = 4,
        Net = 5
    }

    public string Title { get; set; } = "Performance";
    public string Description { get; set; } = "System Performance";
    public string TimmingKey => "Perfs";
    public long TimmingValue => _stopWatch.ElapsedMilliseconds;
    public void ForceRefresh() => ForceRefreshClicked?.Invoke(this, EventArgs.Empty);

    private void RefresherDoWork() {
        RefreshStarts?.Invoke(this, EventArgs.Empty);
        System.Refresh();
        RefreshComplete?.Invoke(this, EventArgs.Empty);
    }
    public void Refresher(bool firstTime = false) {
        _stopWatch.Restart();
        if (InvokeRequired) {
            Invoke(() => RefresherDoWork());
        } else {
            RefresherDoWork();
        }
        _stopWatch.Stop();
    }
    public void LoadSettings() {
        Settings.LoadPerformance();
    }
    public void ApplySettings() {
        chartCpu.SetIndexes("Total", Settings.Performance.ShowKernelTime ? "Kernel" : null);
        chartCpu.BackSolid = Settings.Performance.Solid;
        chartCpu.AntiAliasing = Settings.Performance.AntiAlias;
        chartCpu.ShadeBackground = Settings.Performance.ShadeBackground;
        chartCpu.DisplayAverage = Settings.Performance.DisplayAverages;
        chartCpu.DisplayLegends = Settings.Performance.DisplayLegends;
        chartCpu.DisplayIndexes = Settings.Performance.DisplayIndexes;
        chartCpu.DetailsOnHover = Settings.Performance.DisplayOnHover;
        chartCpu.ValueSpacing = Settings.Performance.ValueSpacing;
        chartCpu.GridSpacing = Settings.Performance.GridSize;
        chartCpu.PenGridVertical.DashStyle = (DashStyle)Settings.Performance.VerticalGridStyle;
        chartCpu.PenGridVertical.Color = Settings.Performance.VerticalGridColor;
        chartCpu.PenGridHorizontal.DashStyle = (DashStyle)Settings.Performance.HorizontalGridStyle;
        chartCpu.PenGridHorizontal.Color = Settings.Performance.HorizontalGridColor;
        chartCpu.PenAverage.DashStyle = (DashStyle)Settings.Performance.AverageLineStyle;
        chartCpu.PenAverage.Color = Settings.Performance.AverageLineColor;
        chartCpu.LightColors = Settings.Performance.LightColors;
        chartMem.CopySettings(chartCpu);
        chartIO.CopySettings(chartCpu);
        chartDisk.CopySettings(chartCpu);
        chartNet.CopySettings(chartCpu);
        meterCpu.LightColors = chartCpu.LightColors;
        meterMem.LightColors = chartCpu.LightColors;
        meterIO.LightColors = chartCpu.LightColors;
        meterDisk.LightColors = chartCpu.LightColors;
        meterNet.LightColors = chartCpu.LightColors;

    }

}

