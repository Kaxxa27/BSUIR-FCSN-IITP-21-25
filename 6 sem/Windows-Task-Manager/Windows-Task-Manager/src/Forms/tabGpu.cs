using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.Versioning;
using sMkTaskManager.Classes;
using sMkTaskManager.Controls;
using System.Drawing.Drawing2D;
namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
internal class tabGpu : UserControl, ITaskManagerTab {
    private readonly Stopwatch _stopWatch = new();
    private readonly TaskManagerGPUCollection GPUs = new();
    private HashSet<string> ColsGpus = new();
    private sMkPerfChart[] Charts;

    private sMkListView lv;
    private ImageList il;
    private Label lblGpuChart;
    private Label lblDedicatedMemory;
    private Label lblSharedMemory;
    private Label lblPowerUsage;
    private Label lblTemperature;
    private ComboBox cboEngine1;
    private ComboBox cboEngine2;
    private sMkPerfChart chartGpu;
    private sMkPerfChart chartEngine1;
    private sMkPerfChart chartEngine2;
    private sMkPerfChart chartPower;
    private sMkPerfChart chartDedicatedMemory;
    private sMkPerfChart chartSharedMemory;
    private sMkPerfChart chartTemperature;
    private TableLayoutPanel tlp;

    public event EventHandler? RefreshStarts;
    public event EventHandler? RefreshComplete;
    public event EventHandler? ForceRefreshClicked;

    private IContainer? components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }

    public tabGpu() {
        InitializeComponent();
        InitializeExtras();
        Extensions.CascadingDoubleBuffer(this);
    }
    private void InitializeComponent() {
        components = new Container();
        il = new ImageList(components);
        tlp = new TableLayoutPanel();
        chartTemperature = new sMkPerfChart();
        lblTemperature = new Label();
        chartPower = new sMkPerfChart();
        lblPowerUsage = new Label();
        chartDedicatedMemory = new sMkPerfChart();
        chartSharedMemory = new sMkPerfChart();
        lblDedicatedMemory = new Label();
        lblSharedMemory = new Label();
        chartEngine1 = new sMkPerfChart();
        chartEngine2 = new sMkPerfChart();
        cboEngine1 = new ComboBox();
        cboEngine2 = new ComboBox();
        chartGpu = new sMkPerfChart();
        lblGpuChart = new Label();
        lv = new sMkListView();
        tlp.SuspendLayout();
        SuspendLayout();
        // 
        // il
        // 
        il.ColorDepth = ColorDepth.Depth32Bit;
        il.ImageSize = new Size(16, 16);
        il.TransparentColor = Color.Transparent;
        // 
        // tlp
        // 
        tlp.ColumnCount = 2;
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tlp.Controls.Add(chartTemperature, 1, 7);
        tlp.Controls.Add(lblTemperature, 1, 6);
        tlp.Controls.Add(chartPower, 0, 7);
        tlp.Controls.Add(lblPowerUsage, 0, 6);
        tlp.Controls.Add(chartDedicatedMemory, 0, 5);
        tlp.Controls.Add(chartSharedMemory, 1, 5);
        tlp.Controls.Add(lblDedicatedMemory, 0, 4);
        tlp.Controls.Add(lblSharedMemory, 1, 4);
        tlp.Controls.Add(chartEngine1, 0, 3);
        tlp.Controls.Add(chartEngine2, 1, 3);
        tlp.Controls.Add(cboEngine1, 0, 2);
        tlp.Controls.Add(cboEngine2, 1, 2);
        tlp.Controls.Add(chartGpu, 0, 1);
        tlp.Controls.Add(lblGpuChart, 0, 0);
        tlp.Controls.Add(lv, 0, 8);
        tlp.Dock = DockStyle.Fill;
        tlp.Location = new Point(0, 0);
        tlp.Name = "tlp";
        tlp.RowCount = 9;
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
        tlp.Size = new Size(600, 600);
        tlp.TabIndex = 0;
        // 
        // chartTemperature
        // 
        chartTemperature.AntiAliasing = false;
        chartTemperature.BackColor = Color.Black;
        chartTemperature.BackColorShade = Color.FromArgb(0, 0, 0);
        chartTemperature.BackSolid = false;
        chartTemperature.BorderStyle = Border3DStyle.Sunken;
        chartTemperature.DetailsOnHover = false;
        chartTemperature.DisplayAverage = false;
        chartTemperature.DisplayIndexes = false;
        chartTemperature.DisplayLegends = false;
        chartTemperature.Dock = DockStyle.Fill;
        chartTemperature.GridSpacing = 10;
        chartTemperature.LegendSpacing = 35;
        chartTemperature.LightColors = false;
        chartTemperature.Location = new Point(302, 409);
        chartTemperature.Margin = new Padding(2);
        chartTemperature.MaxValue = 0D;
        chartTemperature.Name = "chartTemperature";
        chartTemperature.ScaleMode = sMkPerfChart.ScaleModes.Absolute;
        chartTemperature.ShadeBackground = false;
        chartTemperature.Size = new Size(296, 109);
        chartTemperature.TabIndex = 17;
        chartTemperature.TabStop = false;
        chartTemperature.ValueSpacing = 2;
        chartTemperature.ValuesSuffix = "";
        // 
        // lblTemperature
        // 
        lblTemperature.Dock = DockStyle.Fill;
        lblTemperature.Location = new Point(303, 392);
        lblTemperature.Margin = new Padding(3, 1, 3, 0);
        lblTemperature.Name = "lblTemperature";
        lblTemperature.Size = new Size(294, 15);
        lblTemperature.TabIndex = 14;
        lblTemperature.Text = "Temperature";
        lblTemperature.TextAlign = ContentAlignment.BottomLeft;
        // 
        // chartPower
        // 
        chartPower.AntiAliasing = false;
        chartPower.BackColor = Color.Black;
        chartPower.BackColorShade = Color.FromArgb(0, 0, 0);
        chartPower.BackSolid = false;
        chartPower.BorderStyle = Border3DStyle.Sunken;
        chartPower.DetailsOnHover = false;
        chartPower.DisplayAverage = false;
        chartPower.DisplayIndexes = false;
        chartPower.DisplayLegends = false;
        chartPower.Dock = DockStyle.Fill;
        chartPower.GridSpacing = 10;
        chartPower.LegendSpacing = 35;
        chartPower.LightColors = false;
        chartPower.Location = new Point(2, 409);
        chartPower.Margin = new Padding(2);
        chartPower.MaxValue = 0D;
        chartPower.Name = "chartPower";
        chartPower.ScaleMode = sMkPerfChart.ScaleModes.Absolute;
        chartPower.ShadeBackground = false;
        chartPower.Size = new Size(296, 109);
        chartPower.TabIndex = 13;
        chartPower.TabStop = false;
        chartPower.ValueSpacing = 2;
        chartPower.ValuesSuffix = "";
        // 
        // lblPowerUsage
        // 
        lblPowerUsage.Dock = DockStyle.Fill;
        lblPowerUsage.Location = new Point(3, 392);
        lblPowerUsage.Margin = new Padding(3, 1, 3, 0);
        lblPowerUsage.Name = "lblPowerUsage";
        lblPowerUsage.Size = new Size(294, 15);
        lblPowerUsage.TabIndex = 12;
        lblPowerUsage.Text = "Power Usage";
        lblPowerUsage.TextAlign = ContentAlignment.BottomLeft;
        // 
        // chartDedicatedMemory
        // 
        chartDedicatedMemory.AntiAliasing = false;
        chartDedicatedMemory.BackColor = Color.Black;
        chartDedicatedMemory.BackColorShade = Color.FromArgb(0, 0, 0);
        chartDedicatedMemory.BackSolid = false;
        chartDedicatedMemory.BorderStyle = Border3DStyle.Sunken;
        chartDedicatedMemory.DetailsOnHover = false;
        chartDedicatedMemory.DisplayAverage = false;
        chartDedicatedMemory.DisplayIndexes = false;
        chartDedicatedMemory.DisplayLegends = false;
        chartDedicatedMemory.Dock = DockStyle.Fill;
        chartDedicatedMemory.GridSpacing = 10;
        chartDedicatedMemory.LegendSpacing = 35;
        chartDedicatedMemory.LightColors = false;
        chartDedicatedMemory.Location = new Point(2, 280);
        chartDedicatedMemory.Margin = new Padding(2);
        chartDedicatedMemory.MaxValue = 0D;
        chartDedicatedMemory.Name = "chartDedicatedMemory";
        chartDedicatedMemory.ScaleMode = sMkPerfChart.ScaleModes.Absolute;
        chartDedicatedMemory.ShadeBackground = false;
        chartDedicatedMemory.Size = new Size(296, 109);
        chartDedicatedMemory.TabIndex = 11;
        chartDedicatedMemory.TabStop = false;
        chartDedicatedMemory.ValueSpacing = 2;
        chartDedicatedMemory.ValuesSuffix = "";
        // 
        // chartSharedMemory
        // 
        chartSharedMemory.AntiAliasing = false;
        chartSharedMemory.BackColor = Color.Black;
        chartSharedMemory.BackColorShade = Color.FromArgb(0, 0, 0);
        chartSharedMemory.BackSolid = false;
        chartSharedMemory.BorderStyle = Border3DStyle.Sunken;
        chartSharedMemory.DetailsOnHover = false;
        chartSharedMemory.DisplayAverage = false;
        chartSharedMemory.DisplayIndexes = false;
        chartSharedMemory.DisplayLegends = false;
        chartSharedMemory.Dock = DockStyle.Fill;
        chartSharedMemory.GridSpacing = 10;
        chartSharedMemory.LegendSpacing = 35;
        chartSharedMemory.LightColors = false;
        chartSharedMemory.Location = new Point(302, 280);
        chartSharedMemory.Margin = new Padding(2);
        chartSharedMemory.MaxValue = 0D;
        chartSharedMemory.Name = "chartSharedMemory";
        chartSharedMemory.ScaleMode = sMkPerfChart.ScaleModes.Absolute;
        chartSharedMemory.ShadeBackground = false;
        chartSharedMemory.Size = new Size(296, 109);
        chartSharedMemory.TabIndex = 10;
        chartSharedMemory.TabStop = false;
        chartSharedMemory.ValueSpacing = 2;
        chartSharedMemory.ValuesSuffix = "";
        // 
        // lblDedicatedMemory
        // 
        lblDedicatedMemory.Dock = DockStyle.Fill;
        lblDedicatedMemory.Location = new Point(3, 263);
        lblDedicatedMemory.Margin = new Padding(3, 1, 3, 0);
        lblDedicatedMemory.Name = "lblDedicatedMemory";
        lblDedicatedMemory.Size = new Size(294, 15);
        lblDedicatedMemory.TabIndex = 9;
        lblDedicatedMemory.Text = "Dedicated Memory Usage";
        lblDedicatedMemory.TextAlign = ContentAlignment.BottomLeft;
        // 
        // lblSharedMemory
        // 
        lblSharedMemory.Dock = DockStyle.Fill;
        lblSharedMemory.Location = new Point(303, 263);
        lblSharedMemory.Margin = new Padding(3, 1, 3, 0);
        lblSharedMemory.Name = "lblSharedMemory";
        lblSharedMemory.Size = new Size(294, 15);
        lblSharedMemory.TabIndex = 8;
        lblSharedMemory.Text = "Shared Memory Usage";
        lblSharedMemory.TextAlign = ContentAlignment.BottomLeft;
        // 
        // chartEngine1
        // 
        chartEngine1.AntiAliasing = false;
        chartEngine1.BackColor = Color.Black;
        chartEngine1.BackColorShade = Color.FromArgb(0, 0, 0);
        chartEngine1.BackSolid = false;
        chartEngine1.BorderStyle = Border3DStyle.Sunken;
        chartEngine1.DetailsOnHover = false;
        chartEngine1.DisplayAverage = false;
        chartEngine1.DisplayIndexes = false;
        chartEngine1.DisplayLegends = false;
        chartEngine1.Dock = DockStyle.Fill;
        chartEngine1.GridSpacing = 10;
        chartEngine1.LegendSpacing = 35;
        chartEngine1.LightColors = false;
        chartEngine1.Location = new Point(2, 151);
        chartEngine1.Margin = new Padding(2);
        chartEngine1.MaxValue = 0D;
        chartEngine1.Name = "chartEngine1";
        chartEngine1.ScaleMode = sMkPerfChart.ScaleModes.Absolute;
        chartEngine1.ShadeBackground = false;
        chartEngine1.Size = new Size(296, 109);
        chartEngine1.TabIndex = 7;
        chartEngine1.TabStop = false;
        chartEngine1.ValueSpacing = 2;
        chartEngine1.ValuesSuffix = "";
        // 
        // chartEngine2
        // 
        chartEngine2.AntiAliasing = false;
        chartEngine2.BackColor = Color.Black;
        chartEngine2.BackColorShade = Color.FromArgb(0, 0, 0);
        chartEngine2.BackSolid = false;
        chartEngine2.BorderStyle = Border3DStyle.Sunken;
        chartEngine2.DetailsOnHover = false;
        chartEngine2.DisplayAverage = false;
        chartEngine2.DisplayIndexes = false;
        chartEngine2.DisplayLegends = false;
        chartEngine2.Dock = DockStyle.Fill;
        chartEngine2.GridSpacing = 10;
        chartEngine2.LegendSpacing = 35;
        chartEngine2.LightColors = false;
        chartEngine2.Location = new Point(302, 151);
        chartEngine2.Margin = new Padding(2);
        chartEngine2.MaxValue = 0D;
        chartEngine2.Name = "chartEngine2";
        chartEngine2.ScaleMode = sMkPerfChart.ScaleModes.Absolute;
        chartEngine2.ShadeBackground = false;
        chartEngine2.Size = new Size(296, 109);
        chartEngine2.TabIndex = 6;
        chartEngine2.TabStop = false;
        chartEngine2.ValueSpacing = 2;
        chartEngine2.ValuesSuffix = "";
        // 
        // cboEngine1
        // 
        cboEngine1.Dock = DockStyle.Fill;
        cboEngine1.DropDownStyle = ComboBoxStyle.DropDownList;
        cboEngine1.Location = new Point(3, 134);
        cboEngine1.Margin = new Padding(3, 1, 4, 0);
        cboEngine1.Name = "cboEngine1";
        cboEngine1.Size = new Size(294, 23);
        cboEngine1.TabIndex = 18;
        cboEngine1.Sorted = true;
        // 
        // cboEngine2
        // 
        cboEngine2.Dock = DockStyle.Fill;
        cboEngine2.DropDownStyle = ComboBoxStyle.DropDownList;
        cboEngine2.Location = new Point(303, 134);
        cboEngine2.Margin = new Padding(3, 1, 4, 0);
        cboEngine2.Name = "cboEngine2";
        cboEngine2.Size = new Size(294, 23);
        cboEngine2.TabIndex = 19;
        cboEngine2.Sorted = true;
        // 
        // chartGpu
        // 
        chartGpu.AntiAliasing = false;
        chartGpu.BackColor = Color.Black;
        chartGpu.BackColorShade = Color.FromArgb(0, 0, 0);
        chartGpu.BackSolid = false;
        chartGpu.BorderStyle = Border3DStyle.Sunken;
        tlp.SetColumnSpan(chartGpu, 2);
        chartGpu.DetailsOnHover = false;
        chartGpu.DisplayAverage = false;
        chartGpu.DisplayIndexes = false;
        chartGpu.DisplayLegends = false;
        chartGpu.Dock = DockStyle.Fill;
        chartGpu.GridSpacing = 10;
        chartGpu.LegendSpacing = 35;
        chartGpu.LightColors = false;
        chartGpu.Location = new Point(2, 22);
        chartGpu.Margin = new Padding(2);
        chartGpu.MaxValue = 0D;
        chartGpu.Name = "chartGpu";
        chartGpu.ScaleMode = sMkPerfChart.ScaleModes.Absolute;
        chartGpu.ShadeBackground = false;
        chartGpu.Size = new Size(596, 109);
        chartGpu.TabIndex = 5;
        chartGpu.TabStop = false;
        chartGpu.ValueSpacing = 2;
        chartGpu.ValuesSuffix = "";
        // 
        // lblGpuChart
        // 
        tlp.SetColumnSpan(lblGpuChart, 2);
        lblGpuChart.Dock = DockStyle.Fill;
        lblGpuChart.Location = new Point(3, 1);
        lblGpuChart.Margin = new Padding(3, 1, 3, 0);
        lblGpuChart.Name = "lblGpuChart";
        lblGpuChart.Size = new Size(594, 19);
        lblGpuChart.TabIndex = 3;
        lblGpuChart.Text = "GPU Total Usage";
        lblGpuChart.TextAlign = ContentAlignment.BottomLeft;
        // 
        // lv
        // 
        lv.AlternateRowColors = false;
        lv.BorderStyle = BorderStyle.FixedSingle;
        tlp.SetColumnSpan(lv, 2);
        lv.Dock = DockStyle.Fill;
        lv.FullRowSelect = true;
        lv.Location = new Point(3, 523);
        lv.MultiSelect = false;
        lv.Name = "lv";
        lv.Size = new Size(594, 74);
        lv.Sortable = true;
        lv.SortColumn = -1;
        lv.Sorting = SortOrder.Ascending;
        lv.TabIndex = 18;
        lv.UseCompatibleStateImageBehavior = false;
        lv.View = View.Details;
        // 
        // tabGpu
        // 
        Controls.Add(tlp);
        Name = "tabGpu";
        Size = new Size(600, 600);
        tlp.ResumeLayout(false);
        ResumeLayout(false);
    }
    private void InitializeExtras() {
        Charts = new[] { chartDedicatedMemory, chartEngine1, chartEngine2, chartGpu, chartPower, chartSharedMemory, chartTemperature };
        foreach (sMkPerfChart g in Charts) {
            g.SetIndexes("Gpu0");
            g.BackColorShade = Color.FromArgb(0, 0, 0);
            g.ScaleMode = sMkPerfChart.ScaleModes.Absolute;
        }
        chartPower.ScaleMode = sMkPerfChart.ScaleModes.Relative; chartPower.ValuesSuffix = "w";
        chartTemperature.ScaleMode = sMkPerfChart.ScaleModes.Relative; chartTemperature.ValuesSuffix = "c";

        lv.ContentType = typeof(TaskManagerGPU);
        lv.DataSource = GPUs.DataExporter;
        lv.StateImageList = il;
        il.Images.Clear();
        lv.HideSelection = true;

        lv.ColumnReordered += OnListViewColumnReordered;
        lv.SizeChanged += OnListViewSizeChanged;
        lv.ColumnWidthChanging += OnListViewColumnWidthChanging;
        lv.ColumnWidthChanged += OnListViewColumnWidthChanged;
    }

    private void OnListViewColumnReordered(object? sender, ColumnReorderedEventArgs e) {
        if (e.Header!.Text == "ID") { e.Cancel = true; }
        if (e.Header!.Text == "Name") { e.Cancel = true; }
        if (e.NewDisplayIndex == 0) { e.Cancel = true; }
    }
    private void OnListViewSizeChanged(object? sender, EventArgs e) {
        if (lv.Columns.Count >= 1 && lv.Width > 200) {
            lv.Columns[0].Width = Math.Max(50, lv.Width - lv.TotalColumnsWidth(0));
        }
    }
    private void OnListViewColumnWidthChanging(object? sender, ColumnWidthChangingEventArgs e) {
        if (lv.Visible && lv.Columns.Count >= 1 && e.ColumnIndex != 0) {
            lv.Columns[0].Width = Math.Max(50, lv.Width - lv.TotalColumnsWidth(0));
        }
    }
    private void OnListViewColumnWidthChanged(object? sender, ColumnWidthChangedEventArgs e) {
        // Dont force this, its crashing unexpectedly
        // if (e.ColumnIndex == 0) { lv.Columns[0].Width = Math.Max(50, lv.Width - lv.TotalColumnsWidth(0)); }
    }
    private Color PopulateAndGetAdapterColor(short number) {
        var thisColor = number switch {
            1 => Color.CornflowerBlue,
            2 => Color.Khaki,
            3 => Color.LightSteelBlue,
            4 => Color.MediumTurquoise,
            5 => Color.PaleGreen,
            _ => Color.DarkGray,
        };
        if (!il.Images.ContainsKey(number.ToString())) {
            Bitmap bmp = new(16, 16);
            Graphics g = Graphics.FromImage(bmp);
            using (SolidBrush thisBrush = new(thisColor)) {
                g.FillRectangle(thisBrush, 1, 1, 14, 14);
            }
            using (Pen thisPen = new(Color.Black, 1)) {
                g.DrawRectangle(thisPen, 1, 1, 14, 14);
            }
            il.Images.Add(number.ToString(), bmp);
        }
        return thisColor;
    }
    private void PopulateEngines() {
        string default1 = "3D";
        string default2 = "Video Processing";
        foreach (TaskManagerGPU a in GPUs) {
            foreach (TaskManagerGPU.NodeInfo n in a.Nodes) {
                if (!cboEngine1.Items.Contains(n.Name)) cboEngine1.Items.Add(n.Name);
                if (!cboEngine2.Items.Contains(n.Name)) cboEngine2.Items.Add(n.Name);
            }
        }
        if (cboEngine1.Text == "") { cboEngine1.Text = cboEngine1.Items.Contains(default1) ? default1 : ""; }
        if (cboEngine2.Text == "") { cboEngine2.Text = cboEngine2.Items.Contains(default2) ? default2 : ""; }
    }
    private void PopulateGraphIndexes() {
        foreach (sMkPerfChart g in Charts) {
            if (GPUs.Count == 1) { g.SetIndexes("GPU1"); }
            if (GPUs.Count == 2) { g.SetIndexes("GPU1", "GPU2"); }
            if (GPUs.Count == 3) { g.SetIndexes("GPU1", "GPU2", "GPU3"); }
            if (GPUs.Count == 4) { g.SetIndexes("GPU1", "GPU2", "GPU3", "GPU4"); }
            if (GPUs.Count >= 5) { g.SetIndexes("GPU1", "GPU2", "GPU3", "GPU4", "GPU5"); }

            if (GPUs.Count >= 1) g.PenGraph1.Color = GPUs[0].Color;
            if (GPUs.Count >= 2) g.PenGraph2.Color = GPUs[1].Color;
            if (GPUs.Count >= 3) g.PenGraph3.Color = GPUs[2].Color;
            if (GPUs.Count >= 4) g.PenGraph4.Color = GPUs[3].Color;
            if (GPUs.Count >= 5) g.PenGraph5.Color = GPUs[4].Color;
        }
    }
    private void PopulateGraphValues() {
        if (GPUs.Count == 1) {
            chartGpu.AddValue(GPUs[0].UsageValue);
            chartEngine1.AddValue(GPUs[0].EngineUsageValue(cboEngine1.Text));
            chartEngine2.AddValue(GPUs[0].EngineUsageValue(cboEngine2.Text));
            chartDedicatedMemory.AddValue(GPUs[0].DedicatedMemoryUsage);
            chartSharedMemory.AddValue(GPUs[0].SharedMemoryUsage);
            chartTemperature.AddValue(GPUs[0].Temperature);
            chartPower.AddValue(GPUs[0].PowerUsage);
        } else if (GPUs.Count == 2) {
            chartGpu.AddValue(GPUs[0].UsageValue, GPUs[1].UsageValue);
            chartEngine1.AddValue(GPUs[0].EngineUsageValue(cboEngine1.Text), GPUs[1].EngineUsageValue(cboEngine1.Text));
            chartEngine2.AddValue(GPUs[0].EngineUsageValue(cboEngine2.Text), GPUs[1].EngineUsageValue(cboEngine2.Text));
            chartDedicatedMemory.AddValue(GPUs[0].DedicatedMemoryUsage, GPUs[1].DedicatedMemoryUsage);
            chartSharedMemory.AddValue(GPUs[0].SharedMemoryUsage, GPUs[1].SharedMemoryUsage);
            chartPower.AddValue(GPUs[0].PowerUsage, GPUs[1].PowerUsage);
            chartTemperature.AddValue(GPUs[0].Temperature, GPUs[1].Temperature);
        } else if (GPUs.Count == 3) {
            chartGpu.AddValue(GPUs[0].UsageValue, GPUs[1].UsageValue, GPUs[2].UsageValue);
            chartDedicatedMemory.AddValue(GPUs[0].DedicatedMemoryUsage, GPUs[1].DedicatedMemoryUsage, GPUs[2].DedicatedMemoryUsage);
            chartSharedMemory.AddValue(GPUs[0].SharedMemoryUsage, GPUs[1].SharedMemoryUsage, GPUs[2].SharedMemoryUsage);
            chartPower.AddValue(GPUs[0].PowerUsage, GPUs[1].PowerUsage, GPUs[2].PowerUsage);
            chartTemperature.AddValue(GPUs[0].Temperature, GPUs[1].Temperature, GPUs[2].Temperature);
        } else if (GPUs.Count == 4) {
            chartGpu.AddValue(GPUs[0].UsageValue, GPUs[1].UsageValue, GPUs[2].UsageValue, GPUs[3].UsageValue);
            chartDedicatedMemory.AddValue(GPUs[0].DedicatedMemoryUsage, GPUs[1].DedicatedMemoryUsage, GPUs[2].DedicatedMemoryUsage, GPUs[3].DedicatedMemoryUsage);
            chartSharedMemory.AddValue(GPUs[0].SharedMemoryUsage, GPUs[1].SharedMemoryUsage, GPUs[2].SharedMemoryUsage, GPUs[3].SharedMemoryUsage);
            chartPower.AddValue(GPUs[0].PowerUsage, GPUs[1].PowerUsage, GPUs[2].PowerUsage, GPUs[3].PowerUsage);
            chartTemperature.AddValue(GPUs[0].Temperature, GPUs[1].Temperature, GPUs[2].Temperature, GPUs[3].Temperature);
        } else if (GPUs.Count >= 5) {
            chartGpu.AddValue(GPUs[0].UsageValue, GPUs[1].UsageValue, GPUs[2].UsageValue, GPUs[3].UsageValue, GPUs[4].UsageValue);
            chartDedicatedMemory.AddValue(GPUs[0].DedicatedMemoryUsage, GPUs[1].DedicatedMemoryUsage, GPUs[2].DedicatedMemoryUsage, GPUs[3].DedicatedMemoryUsage, GPUs[4].DedicatedMemoryUsage);
            chartSharedMemory.AddValue(GPUs[0].SharedMemoryUsage, GPUs[1].SharedMemoryUsage, GPUs[2].SharedMemoryUsage, GPUs[3].SharedMemoryUsage, GPUs[4].SharedMemoryUsage);
            chartPower.AddValue(GPUs[0].PowerUsage, GPUs[1].PowerUsage, GPUs[2].PowerUsage, GPUs[3].PowerUsage, GPUs[4].PowerUsage);
            chartTemperature.AddValue(GPUs[0].Temperature, GPUs[1].Temperature, GPUs[2].Temperature, GPUs[3].Temperature, GPUs[4].Temperature);
        }
    }

    public void Feature_ForceRefresh() {
        lv.SuspendLayout();
        GPUs.Clear();
        lv.Items.Clear();
        Refresher(true);
        lv.ResumeLayout();
        ForceRefreshClicked?.Invoke(this, EventArgs.Empty);
    }

    public sMkListView? ListView => lv;
    public string Title { get; set; } = "GPUs";
    public string Description { get; set; } = "GPU Performance";
    public string TimmingKey => "GPU";
    public long TimmingValue => _stopWatch.ElapsedMilliseconds;
    public bool CanSelectColumns => true;
    public void ForceRefresh() => Feature_ForceRefresh();
    public TaskManagerColumnTypes ColumnType => TaskManagerColumnTypes.GPUs;
    public ListView.ColumnHeaderCollection? GetColumns() => lv.Columns;
    public void SetColumns(in ListView.ListViewItemCollection colItems) {
        lv.SetColumns(colItems);
        ColsGpus = lv.Columns.Cast<ColumnHeader>().Select(x => x.Name).ToHashSet()!;
    }

    private void RefresherDoWork(bool firstTime = false) {
        RefreshStarts?.Invoke(this, EventArgs.Empty);
        if (GPUs.Count == 0) firstTime = true;

        if (firstTime) {
            short i = 0;
            foreach (TaskManagerGPU a in TaskManagerGPU.GetAdaptersList()) {
                i++;
                if (i > 5) break;
                try {
                    a.Load();
                    a.Color = PopulateAndGetAdapterColor(i);
                    a.StateImageIndex = il.Images.IndexOfKey(i.ToString());
                    GPUs.Add(a);
                } catch (Exception ex) { Shared.DebugTrap(ex, 311); }
            }
            PopulateGraphIndexes();
            PopulateEngines();
            lv.Scrollable = false;
            Title = lv.Items.Count > 1 ? "GPUs" : "GPU";
            tlp.RowStyles[tlp.GetRow(lv)].Height = 30 + (lv.Items.Count * 20) + 10;
            if (lv.Columns.Count > 0) lv.Columns[0].Width = Math.Max(50, lv.Width - lv.TotalColumnsWidth(0));
        } else {
            foreach (TaskManagerGPU g in GPUs) {
                try {
                    g!.Refresh();
                } catch (Exception ex) { Shared.DebugTrap(ex, 312); }
            }
            PopulateGraphValues();
        }

        RefreshComplete?.Invoke(this, EventArgs.Empty);
    }
    public void Refresher(bool firstTime = false) {
        _stopWatch.Restart();
        if (InvokeRequired) {
            Invoke(() => RefresherDoWork(firstTime));
        } else {
            RefresherDoWork();
        }
        _stopWatch.Stop();
    }
    public void LoadSettings() {
        Settings.LoadColsInformation(ColumnType, lv, ref ColsGpus);
        chartGpu.BackSolid = Settings.Performance.Solid;
        chartGpu.AntiAliasing = Settings.Performance.AntiAlias;
        chartGpu.ShadeBackground = false; // Settings.Performance.ShadeBackground;
        chartGpu.DisplayAverage = false;  // Settings.Performance.DisplayAverages;
        chartGpu.DisplayLegends = false;  // Settings.Performance.DisplayLegends;
        chartGpu.DisplayIndexes = true;   // Settings.Performance.DisplayIndexes;
        chartGpu.DetailsOnHover = false;  // Settings.Performance.DisplayOnHover;
        chartGpu.ValueSpacing = Settings.Performance.ValueSpacing;
        chartGpu.GridSpacing = Settings.Performance.GridSize;
        chartGpu.PenGridVertical.DashStyle = (DashStyle)Settings.Performance.VerticalGridStyle;
        chartGpu.PenGridVertical.Color = Settings.Performance.VerticalGridColor;
        chartGpu.PenGridHorizontal.DashStyle = (DashStyle)Settings.Performance.HorizontalGridStyle;
        chartGpu.PenGridHorizontal.Color = Settings.Performance.HorizontalGridColor;
        chartGpu.PenAverage.DashStyle = (DashStyle)Settings.Performance.AverageLineStyle;
        chartGpu.PenAverage.Color = Settings.Performance.AverageLineColor;
        chartGpu.LightColors = Settings.Performance.LightColors;
        chartEngine1.CopySettings(chartGpu);
        chartEngine2.CopySettings(chartGpu);
        chartDedicatedMemory.CopySettings(chartGpu);
        chartSharedMemory.CopySettings(chartGpu);
        chartTemperature.CopySettings(chartGpu);
        chartPower.CopySettings(chartGpu);
    }

}
