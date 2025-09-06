using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.Versioning;
using sMkTaskManager.Classes;
using sMkTaskManager.Controls;
namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
internal class tabNetworking : UserControl, ITaskManagerTab {
    private readonly Stopwatch _stopWatch = new();
    private readonly Dictionary<string, string> _IPstatsValues = new();
    private readonly Dictionary<string, string> _TCPstatsValues = new();
    private readonly Dictionary<string, string> _UDPstatsValues = new();
    private readonly Dictionary<string, string> _ICMPstatsValues = new();
    private HashSet<string> ColsNics = new();
    private readonly HashSet<string> HashNics = new();
    private readonly TaskManagerNicsCollection Nics = new();

    private TabControl tc;
    private TabPage tpUsage;
    private TabPage tpIP;
    private TabPage tpTCP;
    private TabPage tpUDP;
    private TabPage tpICMP;
    private sMkListView lv;
    private sMkListView lvIP;
    private sMkListView lvTCP;
    private sMkListView lvUDP;
    private sMkListView lvICMP;
    private GroupBox gb1;
    private GroupBox gb2;
    private GroupBox gb3;
    private sMkPerfChart pChart1;
    private sMkPerfChart pChart2;
    private sMkPerfChart pChart3;
    private Button btnForceRefresh;

    public event EventHandler? ForceRefreshClicked;
    public event EventHandler? RefreshStarts;
    public event EventHandler? RefreshComplete;

    private IContainer? components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }

    public tabNetworking() {
        InitializeComponent();
        InitializeExtras();
        Extensions.CascadingDoubleBuffer(this);
    }
    private void InitializeComponent() {
        components = new Container();
        tc = new TabControl();
        tpUsage = new TabPage();
        lv = new sMkListView();
        gb1 = new GroupBox();
        pChart1 = new sMkPerfChart();
        gb2 = new GroupBox();
        pChart2 = new sMkPerfChart();
        gb3 = new GroupBox();
        pChart3 = new sMkPerfChart();
        tpIP = new TabPage();
        lvIP = new sMkListView();
        tpTCP = new TabPage();
        lvTCP = new sMkListView();
        tpUDP = new TabPage();
        lvUDP = new sMkListView();
        tpICMP = new TabPage();
        lvICMP = new sMkListView();
        btnForceRefresh = new Button();
        tc.SuspendLayout();
        tpUsage.SuspendLayout();
        gb1.SuspendLayout();
        gb2.SuspendLayout();
        gb3.SuspendLayout();
        tpIP.SuspendLayout();
        tpTCP.SuspendLayout();
        tpUDP.SuspendLayout();
        tpICMP.SuspendLayout();
        SuspendLayout();
        // 
        // tc
        // 
        tc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tc.Appearance = TabAppearance.FlatButtons;
        tc.Controls.Add(tpUsage);
        tc.Controls.Add(tpIP);
        tc.Controls.Add(tpTCP);
        tc.Controls.Add(tpUDP);
        tc.Controls.Add(tpICMP);
        tc.Location = new Point(2, 6);
        tc.Margin = new Padding(6);
        tc.Name = "tc";
        tc.Padding = new Point(4, 3);
        tc.SelectedIndex = 0;
        tc.Size = new Size(596, 588);
        tc.TabIndex = 0;
        // 
        // tpUsage
        // 
        tpUsage.BackColor = SystemColors.Control;
        tpUsage.Controls.Add(lv);
        tpUsage.Controls.Add(gb1);
        tpUsage.Controls.Add(gb2);
        tpUsage.Controls.Add(gb3);
        tpUsage.Location = new Point(4, 27);
        tpUsage.Margin = new Padding(0);
        tpUsage.Name = "tpUsage";
        tpUsage.Size = new Size(588, 557);
        tpUsage.TabIndex = 0;
        tpUsage.Text = "Network Usage";
        // 
        // lv
        // 
        lv.AlternateRowColors = false;
        lv.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lv.CheckBoxes = true;
        lv.FullRowSelect = true;
        lv.LabelWrap = false;
        lv.Location = new Point(0, 437);
        lv.Margin = new Padding(0, 6, 0, 0);
        lv.Name = "lv";
        lv.ShowGroups = false;
        lv.Size = new Size(588, 120);
        lv.Sortable = true;
        lv.SortColumn = -1;
        lv.Sorting = SortOrder.Ascending;
        lv.TabIndex = 0;
        lv.UseCompatibleStateImageBehavior = false;
        lv.View = View.Details;
        // 
        // gb1
        // 
        gb1.Controls.Add(pChart1);
        gb1.Dock = DockStyle.Top;
        gb1.Location = new Point(0, 200);
        gb1.Name = "gb1";
        gb1.Padding = new Padding(0);
        gb1.Size = new Size(588, 100);
        gb1.TabIndex = 0;
        gb1.TabStop = false;
        gb1.Text = "Network Name";
        gb1.Visible = false;
        // 
        // pChart1
        // 
        pChart1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        pChart1.AntiAliasing = false;
        pChart1.BackColor = Color.Black;
        pChart1.BackColorShade = Color.FromArgb(0, 0, 0);
        pChart1.BackSolid = false;
        pChart1.BorderStyle = Border3DStyle.Sunken;
        pChart1.GridSpacing = 10;
        pChart1.LegendSpacing = 35;
        pChart1.LightColors = false;
        pChart1.Location = new Point(5, 16);
        pChart1.MaxValue = 0D;
        pChart1.Name = "pChart1";
        pChart1.ScaleMode = sMkPerfChart.ScaleModes.Relative;
        pChart1.Size = new Size(578, 78);
        pChart1.TabIndex = 1;
        pChart1.ValueSpacing = 2;
        pChart1.ValuesSuffix = "";
        // 
        // gb2
        // 
        gb2.Controls.Add(pChart2);
        gb2.Dock = DockStyle.Top;
        gb2.Location = new Point(0, 100);
        gb2.Name = "gb2";
        gb2.Padding = new Padding(0);
        gb2.Size = new Size(588, 100);
        gb2.TabIndex = 0;
        gb2.TabStop = false;
        gb2.Text = "Network Name";
        gb2.Visible = false;
        // 
        // pChart2
        // 
        pChart2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        pChart2.AntiAliasing = false;
        pChart2.BackColor = Color.Black;
        pChart2.BackColorShade = Color.FromArgb(0, 0, 0);
        pChart2.BackSolid = false;
        pChart2.BorderStyle = Border3DStyle.Sunken;
        pChart2.GridSpacing = 10;
        pChart2.LegendSpacing = 35;
        pChart2.LightColors = false;
        pChart2.Location = new Point(5, 16);
        pChart2.MaxValue = 0D;
        pChart2.Name = "pChart2";
        pChart2.ScaleMode = sMkPerfChart.ScaleModes.Relative;
        pChart2.Size = new Size(578, 78);
        pChart2.TabIndex = 1;
        pChart2.ValueSpacing = 2;
        pChart2.ValuesSuffix = "";
        // 
        // gb3
        // 
        gb3.Controls.Add(pChart3);
        gb3.Dock = DockStyle.Top;
        gb3.Location = new Point(0, 0);
        gb3.Name = "gb3";
        gb3.Padding = new Padding(0);
        gb3.Size = new Size(588, 100);
        gb3.TabIndex = 0;
        gb3.TabStop = false;
        gb3.Text = "Network Name";
        gb3.Visible = false;
        // 
        // pChart3
        // 
        pChart3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        pChart3.AntiAliasing = false;
        pChart3.BackColor = Color.Black;
        pChart3.BackColorShade = Color.FromArgb(0, 0, 0);
        pChart3.BackSolid = false;
        pChart3.BorderStyle = Border3DStyle.Sunken;
        pChart3.GridSpacing = 10;
        pChart3.LegendSpacing = 35;
        pChart3.LightColors = false;
        pChart3.Location = new Point(5, 16);
        pChart3.MaxValue = 0D;
        pChart3.Name = "pChart3";
        pChart3.ScaleMode = sMkPerfChart.ScaleModes.Relative;
        pChart3.Size = new Size(578, 78);
        pChart3.TabIndex = 1;
        pChart3.ValueSpacing = 2;
        pChart3.ValuesSuffix = "";
        // 
        // tpIP
        // 
        tpIP.BackColor = SystemColors.Control;
        tpIP.Controls.Add(lvIP);
        tpIP.Location = new Point(4, 27);
        tpIP.Margin = new Padding(0);
        tpIP.Name = "tpIP";
        tpIP.Size = new Size(588, 557);
        tpIP.TabIndex = 1;
        tpIP.Text = "IP Statistics";
        // 
        // lvIP
        // 
        lvIP.AlternateRowColors = false;
        lvIP.Dock = DockStyle.Fill;
        lvIP.FullRowSelect = true;
        lvIP.GridLines = true;
        lvIP.LabelWrap = false;
        lvIP.Location = new Point(0, 0);
        lvIP.Margin = new Padding(0);
        lvIP.MultiSelect = false;
        lvIP.Name = "lvIP";
        lvIP.ShowItemToolTips = true;
        lvIP.Size = new Size(588, 557);
        lvIP.Sortable = true;
        lvIP.SortColumn = -1;
        lvIP.Sorting = SortOrder.Ascending;
        lvIP.TabIndex = 0;
        lvIP.UseCompatibleStateImageBehavior = false;
        lvIP.View = View.Details;
        // 
        // tpTCP
        // 
        tpTCP.BackColor = SystemColors.Control;
        tpTCP.Controls.Add(lvTCP);
        tpTCP.Location = new Point(4, 27);
        tpTCP.Margin = new Padding(0);
        tpTCP.Name = "tpTCP";
        tpTCP.Size = new Size(588, 557);
        tpTCP.TabIndex = 2;
        tpTCP.Text = "TCP Statistics";
        // 
        // lvTCP
        // 
        lvTCP.AlternateRowColors = false;
        lvTCP.Dock = DockStyle.Fill;
        lvTCP.FullRowSelect = true;
        lvTCP.GridLines = true;
        lvTCP.LabelWrap = false;
        lvTCP.Location = new Point(0, 0);
        lvTCP.Margin = new Padding(0);
        lvTCP.MultiSelect = false;
        lvTCP.Name = "lvTCP";
        lvTCP.ShowItemToolTips = true;
        lvTCP.Size = new Size(588, 557);
        lvTCP.Sortable = true;
        lvTCP.SortColumn = -1;
        lvTCP.Sorting = SortOrder.Ascending;
        lvTCP.TabIndex = 1;
        lvTCP.UseCompatibleStateImageBehavior = false;
        lvTCP.View = View.Details;
        // 
        // tpUDP
        // 
        tpUDP.BackColor = SystemColors.Control;
        tpUDP.Controls.Add(lvUDP);
        tpUDP.Location = new Point(4, 27);
        tpUDP.Margin = new Padding(0);
        tpUDP.Name = "tpUDP";
        tpUDP.Size = new Size(588, 557);
        tpUDP.TabIndex = 3;
        tpUDP.Text = "UDP Statistics";
        // 
        // lvUDP
        // 
        lvUDP.AlternateRowColors = false;
        lvUDP.Dock = DockStyle.Fill;
        lvUDP.FullRowSelect = true;
        lvUDP.GridLines = true;
        lvUDP.LabelWrap = false;
        lvUDP.Location = new Point(0, 0);
        lvUDP.Margin = new Padding(0);
        lvUDP.MultiSelect = false;
        lvUDP.Name = "lvUDP";
        lvUDP.ShowItemToolTips = true;
        lvUDP.Size = new Size(588, 557);
        lvUDP.Sortable = true;
        lvUDP.SortColumn = -1;
        lvUDP.Sorting = SortOrder.Ascending;
        lvUDP.TabIndex = 1;
        lvUDP.UseCompatibleStateImageBehavior = false;
        lvUDP.View = View.Details;
        // 
        // tpICMP
        // 
        tpICMP.BackColor = SystemColors.Control;
        tpICMP.Controls.Add(lvICMP);
        tpICMP.Location = new Point(4, 27);
        tpICMP.Margin = new Padding(0);
        tpICMP.Name = "tpICMP";
        tpICMP.Size = new Size(588, 557);
        tpICMP.TabIndex = 4;
        tpICMP.Text = "ICMP Statistics";
        // 
        // lvICMP
        // 
        lvICMP.AlternateRowColors = false;
        lvICMP.Dock = DockStyle.Fill;
        lvICMP.FullRowSelect = true;
        lvICMP.GridLines = true;
        lvICMP.LabelWrap = false;
        lvICMP.Location = new Point(0, 0);
        lvICMP.Margin = new Padding(0);
        lvICMP.MultiSelect = false;
        lvICMP.Name = "lvICMP";
        lvICMP.ShowItemToolTips = true;
        lvICMP.Size = new Size(588, 557);
        lvICMP.Sortable = true;
        lvICMP.SortColumn = -1;
        lvICMP.Sorting = SortOrder.Ascending;
        lvICMP.TabIndex = 1;
        lvICMP.UseCompatibleStateImageBehavior = false;
        lvICMP.View = View.Details;
        // 
        // btnForceRefresh
        // 
        btnForceRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnForceRefresh.Location = new Point(505, 6);
        btnForceRefresh.Margin = new Padding(3, 3, 0, 3);
        btnForceRefresh.Name = "btnForceRefresh";
        btnForceRefresh.Size = new Size(89, 23);
        btnForceRefresh.TabIndex = 5;
        btnForceRefresh.Text = "Force Refresh";
        // 
        // tabNetworking
        // 
        Controls.Add(btnForceRefresh);
        Controls.Add(tc);
        Name = "tabNetworking";
        Size = new Size(600, 600);
        tc.ResumeLayout(false);
        tpUsage.ResumeLayout(false);
        gb1.ResumeLayout(false);
        gb2.ResumeLayout(false);
        gb3.ResumeLayout(false);
        tpIP.ResumeLayout(false);
        tpTCP.ResumeLayout(false);
        tpUDP.ResumeLayout(false);
        tpICMP.ResumeLayout(false);
        ResumeLayout(false);
    }
    private void InitializeExtras() {
        lv.ContentType = typeof(TaskManagerNic);
        lv.DataSource = Nics.DataExporter;
        lv.SpaceFirstValue = false;
        KeyPress += OnKeyPress;
        SizeChanged += OnSizeChanged;
        VisibleChanged += OnVisibleChanged;
        tc.SelectedIndexChanged += onTabControlSelectedTabChanged;
        btnForceRefresh.Click += OnButtonClicked;
        lv.ItemCheck += OnListViewItemCheck;
        lv.ItemChecked += OnListViewItemChecked;
        lv.ColumnReordered += OnListViewColumnReordered;
        tpUsage.SizeChanged += onTabPageSizeChanged;
        // Override any previous set value for the perfCharts
        foreach (sMkPerfChart c in new[] { pChart1, pChart2, pChart3 }) {
            c.Tag = "";
            c.PenGraph1.Color = Color.Cyan;
            c.PenGraph2.Color = Color.Fuchsia;
            c.DisplayAverage = false;
            c.ScaleMode = sMkPerfChart.ScaleModes.Relative;
            c.PenAverage.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            c.ShadeBackground = false;
            c.DisplayLegends = true;
            c.DisplayIndexes = true;
            c.ValuesSuffix = " K";
            c.SetIndexes("Dn", "Up");
        }
    }

    private void OnKeyPress(object? sender, KeyPressEventArgs e) { }
    private void OnVisibleChanged(object? sender, EventArgs e) { }
    private void OnSizeChanged(object? sender, EventArgs e) {
        foreach (TabPage tp in tc.TabPages) {
            if (tc.Width < 550) {
                tp.Text = tp.Text.Replace("Statistics", "Stats");
            } else {
                tp.Text = tp.Text.Replace("Stats", "Statistics");
            }
        }
    }
    private void OnButtonClicked(object? sender, EventArgs e) {
        if (sender == null) return;
        if (sender == btnForceRefresh) { BeginInvoke(Feature_ForceRefresh); return; }
    }
    private void onTabControlSelectedTabChanged(object? sender, EventArgs e) {
        if (!Shared.InitComplete || !Visible) return;
        if (tc.SelectedTab == tpIP && lvIP.Items.Count == 0) Feature_UpdateIPstats();
        if (tc.SelectedTab == tpTCP && lvTCP.Items.Count == 0) Feature_UpdateTCPstats();
        if (tc.SelectedTab == tpUDP && lvUDP.Items.Count == 0) Feature_UpdateUDPstats();
        if (tc.SelectedTab == tpICMP && lvICMP.Items.Count == 0) Feature_UpdateICMPstats();
    }
    private void onTabPageSizeChanged(object? sender, EventArgs e) {
        tpUsage.SuspendLayout();
        if (lv.CheckedItems.Count >= 3) {
            gb1.Height = Convert.ToInt32((tpUsage.Height - (lv.Height + lv.Margin.Top)) / 3.0);
            gb2.Height = gb1.Height;
            gb3.Height = gb1.Height;
        } else if (lv.CheckedItems.Count == 2) {
            gb1.Height = Convert.ToInt32((tpUsage.Height - (lv.Height + lv.Margin.Top)) / 2.0);
            gb2.Height = gb1.Height;
        } else if (lv.CheckedItems.Count == 1) {
            gb1.Height = tpUsage.Height - (lv.Height + lv.Margin.Top);
        }
        tpUsage.ResumeLayout(false);
    }
    private void OnListViewItemCheck(object? sender, ItemCheckEventArgs e) {
        if (lv.CheckedItems.Count >= 3 && e.NewValue == CheckState.Checked) e.NewValue = CheckState.Unchecked;
        if (lv.CheckedItems.Count <= 1 && e.NewValue == CheckState.Unchecked) e.NewValue = CheckState.Checked;
    }
    private void OnListViewItemChecked(object? sender, ItemCheckedEventArgs e) {
        tpUsage.SuspendLayout();
        gb1.Visible = lv.CheckedItems.Count >= 1;
        gb2.Visible = lv.CheckedItems.Count >= 2;
        gb3.Visible = lv.CheckedItems.Count >= 3;
        pChart1.Tag = (lv.CheckedItems.Count >= 1) ? lv.CheckedItems[0].Name : "";
        gb1.Text = (lv.CheckedItems.Count >= 1) ? lv.CheckedItems[0].Text : "";
        pChart2.Tag = (lv.CheckedItems.Count >= 2) ? lv.CheckedItems[1].Name : "";
        gb2.Text = (lv.CheckedItems.Count >= 2) ? lv.CheckedItems[1].Text : "";
        pChart3.Tag = (lv.CheckedItems.Count >= 3) ? lv.CheckedItems[2].Name : "";
        gb3.Text = (lv.CheckedItems.Count >= 3) ? lv.CheckedItems[2].Text : "";
        onTabPageSizeChanged(tpUsage, EventArgs.Empty);
        if (lv.Focused) {
            if (e.Item.Checked && !Settings.CheckedInterfaces.Contains(e.Item.Name)) Settings.CheckedInterfaces.Add(e.Item.Name);
            if (!e.Item.Checked && Settings.CheckedInterfaces.Contains(e.Item.Name)) Settings.CheckedInterfaces.Remove(e.Item.Name);
            Settings.SaveInterfaces();
        }
        tpUsage.ResumeLayout();
    }
    private void OnListViewColumnReordered(object? sender, ColumnReorderedEventArgs e) {
        if (e.Header!.Text == "Name") { e.Cancel = true; }
        if (e.NewDisplayIndex == 0) { e.Cancel = true; }
    }
    private void StatsSetValueNames() {
        if (_IPstatsValues.Count == 0) {
            _IPstatsValues.Add("Interfaces", "dwNumIf");
            _IPstatsValues.Add("IP Addresses", "dwNumAddr");
            _IPstatsValues.Add("IP Forwarding", "dwForwarding");
            _IPstatsValues.Add("Routes", "dwNumRoutes");
            _IPstatsValues.Add("Default TTL", "dwDefaultTTL");
            _IPstatsValues.Add("Datagrams Received", "dwReasmTimeout");
            _IPstatsValues.Add("Datagrams Forwarded", "dwInAddrErrors");
            _IPstatsValues.Add("Header Errors", "dwInReceives");
            _IPstatsValues.Add("Address Errors", "dwInHdrErrors");
            _IPstatsValues.Add("Unknown Protocols Receive", "dwForwDatagrams");
            _IPstatsValues.Add("Received Datagrams Discarded", "dwInUnknownProtos");
            _IPstatsValues.Add("Received Datagrams Delivered", "dwInDiscards");
            _IPstatsValues.Add("Output Requests", "dwInDelivers");
            _IPstatsValues.Add("Routing Discards", "dwOutRequests");
            _IPstatsValues.Add("Output Packets Discarded", "dwRoutingDiscards");
            _IPstatsValues.Add("Output Packet No Route", "dwOutDiscards");
            _IPstatsValues.Add("Reassembly Time-out", "dwOutNoRoutes");
            _IPstatsValues.Add("Reassembly Required", "dwReasmReqds");
            _IPstatsValues.Add("Reassembly Success", "dwReasmOks");
            _IPstatsValues.Add("Reassembly Failures", "dwReasmFails");
            _IPstatsValues.Add("Datagrams Successfully Fragmented", "dwFragOks");
            _IPstatsValues.Add("Datagrams Failing Fragmentation", "dwFragFails");
            _IPstatsValues.Add("Frags Created", "dwFragCreates");
        }
        if (_TCPstatsValues.Count == 0) {
            _TCPstatsValues.Add("Active Opens", "dwActiveOpens");
            _TCPstatsValues.Add("Passive Opens", "dwPassiveOpens");
            _TCPstatsValues.Add("Current Connection Count", "dwCurrEstab");
            _TCPstatsValues.Add("Failed Connection Attempts", "dwAttemptFails");
            _TCPstatsValues.Add("Established Connection Reset", "dwEstabResets");
            _TCPstatsValues.Add("Cumulative Connections", "dwNumConns");
            _TCPstatsValues.Add("Maximum Connections", "dwMaxConn");
            _TCPstatsValues.Add("Segments Received", "dwInSegs");
            _TCPstatsValues.Add("Segments Sent", "dwOutSegs");
            _TCPstatsValues.Add("Segments Retransmitte", "dwRetransSegs");
            _TCPstatsValues.Add("Segments Sent With Reset Flag", "dwOutRsts");
            _TCPstatsValues.Add("Time-out Algorithm", "dwRtoAlgorithm");
            _TCPstatsValues.Add("Min Retransmission Time-out", "dwRtoMin");
            _TCPstatsValues.Add("Max Retransmission Time-out", "dwRtoMax");
            _TCPstatsValues.Add("Errors", "dwInErrs");
        }
        if (_UDPstatsValues.Count == 0) {
            _UDPstatsValues.Add("Datagrams Received", "dwInDatagrams");
            _UDPstatsValues.Add("Datagrams Sent", "dwOutDatagrams");
            _UDPstatsValues.Add("Receive Error", "dwInErrors");
            _UDPstatsValues.Add("No Ports", "dwNoPorts");
            _UDPstatsValues.Add("Entries In UDP Listener Table", "dwNumAddrs");
        }
        if (_ICMPstatsValues.Count == 0) {
            _ICMPstatsValues.Add("Messages", "dwMsgs");
            _ICMPstatsValues.Add("Errors", "dwErrors");
            _ICMPstatsValues.Add("Destination Unreachable", "dwDestUnreachs");
            _ICMPstatsValues.Add("Time Exceeded", "dwTimeExcds");
            _ICMPstatsValues.Add("Parameter Problems", "dwParmProbs");
            _ICMPstatsValues.Add("Source Quenches", "dwSrcQuenchs");
            _ICMPstatsValues.Add("Redirects", "dwRedirects");
            _ICMPstatsValues.Add("Echos", "dwEchos");
            _ICMPstatsValues.Add("Echo Replies", "dwEchoReps");
            _ICMPstatsValues.Add("Timestamps", "dwTimestamps");
            _ICMPstatsValues.Add("Timestamp Replies", "dwTimestampReps");
            _ICMPstatsValues.Add("Address Masks", "dwAddrMasks");
            _ICMPstatsValues.Add("Address Mask Replies", "dwAddrMaskReps");
        }
    }
    private void Feature_UpdateIPstats() {
        try {
            ListViewItem? i;
            API.MIB_IPSTATS s4 = new();
            API.MIB_IPSTATS s6 = new();
            TaskManagerNetwork.GetIPStatistics(ref s4, ref s6);
            if (_IPstatsValues.Count == 0) StatsSetValueNames();
            // Set Initial Columns
            if (lvIP.Columns.Count == 0) {
                lvIP.Columns.Add("IP Parameter", 250);
                lvIP.Columns.Add("IPv4 Value", 100, HorizontalAlignment.Right);
                lvIP.Columns.Add("IPv6 Value", 100, HorizontalAlignment.Right);
            }
            // Add or Update all Values
            lvIP.SuspendLayout();
            foreach (KeyValuePair<string, string> e in _IPstatsValues) {
                if (lvIP.Items.ContainsKey(e.Value)) {
                    i = lvIP.Items[e.Value];
                } else {
                    i = new ListViewItem { Name = e.Value, Text = e.Key };
                    i.SubItems.Add("n/a.");
                    i.SubItems.Add("n/a.");
                    lvIP.Items.Add(i);
                }
                string newValue1 = s4.GetType().GetField(e.Value)!.GetValue(s4)!.ToString()!;
                string newValue2 = s6.GetType().GetField(e.Value)!.GetValue(s6)!.ToString()!;
                if (i.SubItems[1].Text.Equals(newValue1) && i.SubItems[2].Text.Equals(newValue2)) {
                    if (i.BackColor == Settings.Highlights.ChangingColor) i.BackColor = Color.Empty;
                } else {
                    if (!Settings.Highlights.ChangingItems && i.BackColor == Settings.Highlights.ChangingColor) i.BackColor = Color.Empty;
                    if (Settings.Highlights.ChangingItems && !i.SubItems[1].Text.Equals("n/a.") && !i.SubItems[2].Text.Equals("n/a.")) i.BackColor = Settings.Highlights.ChangingColor;
                    i.SubItems[1].Text = newValue1;
                    i.SubItems[2].Text = newValue2;
                }
            }
        } catch (Exception ex) { Shared.DebugTrap(ex, 121); } finally { lvIP.ResumeLayout(); }
    }
    private void Feature_UpdateTCPstats() {
        try {
            ListViewItem? i;
            API.MIB_TCPSTATS t4 = new();
            API.MIB_TCPSTATS t6 = new();
            TaskManagerNetwork.GetTCPStatistics(ref t4, ref t6);
            if (_TCPstatsValues.Count == 0) StatsSetValueNames();
            // Set Initial Columns
            if (lvTCP.Columns.Count == 0) {
                lvTCP.Columns.Add("TCP Parameter", 250);
                lvTCP.Columns.Add("IPv4 Value", 100, HorizontalAlignment.Right);
                lvTCP.Columns.Add("IPv6 Value", 100, HorizontalAlignment.Right);
            }
            lvTCP.SuspendLayout();
            // Add or Update TCP Values
            foreach (KeyValuePair<string, string> e in _TCPstatsValues) {
                if (lvTCP.Items.ContainsKey(e.Value)) {
                    i = lvTCP.Items[e.Value];
                } else {
                    i = new ListViewItem { Name = e.Value, Text = e.Key };
                    i.SubItems.Add("n/a.");
                    i.SubItems.Add("n/a.");
                    lvTCP.Items.Add(i);
                }
                string newValue1 = t4.GetType().GetField(e.Value)!.GetValue(t4)!.ToString()!;
                string newValue2 = t6.GetType().GetField(e.Value)!.GetValue(t6)!.ToString()!;
                if (i.SubItems[1].Text.Equals(newValue1) && i.SubItems[2].Text.Equals(newValue2)) {
                    if (i.BackColor == Settings.Highlights.ChangingColor) i.BackColor = Color.Empty;
                } else {
                    if (!Settings.Highlights.ChangingItems && i.BackColor == Settings.Highlights.ChangingColor) i.BackColor = Color.Empty;
                    if (Settings.Highlights.ChangingItems && !i.SubItems[1].Text.Equals("n/a.") && !i.SubItems[2].Text.Equals("n/a.")) i.BackColor = Settings.Highlights.ChangingColor;
                    i.SubItems[1].Text = newValue1;
                    i.SubItems[2].Text = newValue2;
                }
            }
        } catch (Exception ex) { Shared.DebugTrap(ex, 122); } finally { lvTCP.ResumeLayout(); }
    }
    private void Feature_UpdateUDPstats() {
        try {
            ListViewItem? i;
            API.MIB_UDPSTATS u4 = new();
            API.MIB_UDPSTATS u6 = new();
            TaskManagerNetwork.GetUDPStatistics(ref u4, ref u6);
            if (_UDPstatsValues.Count == 0) StatsSetValueNames();
            // Set Initial Columns
            if (lvUDP.Columns.Count == 0) {
                lvUDP.Columns.Add("UDP Parameter", 250);
                lvUDP.Columns.Add("IPv4 Value", 100, HorizontalAlignment.Right);
                lvUDP.Columns.Add("IPv6 Value", 100, HorizontalAlignment.Right);
            }
            lvUDP.SuspendLayout();
            // Add or Update UDP Values
            foreach (KeyValuePair<string, string> e in _UDPstatsValues) {
                if (lvUDP.Items.ContainsKey(e.Value)) {
                    i = lvUDP.Items[e.Value];
                } else {
                    i = new ListViewItem { Name = e.Value, Text = e.Key };
                    i.SubItems.Add("n/a.");
                    i.SubItems.Add("n/a.");
                    lvUDP.Items.Add(i);
                }
                string newValue1 = u4.GetType().GetField(e.Value)!.GetValue(u4)!.ToString()!;
                string newValue2 = u6.GetType().GetField(e.Value)!.GetValue(u6)!.ToString()!;
                if (i.SubItems[1].Text.Equals(newValue1) && i.SubItems[2].Text.Equals(newValue2)) {
                    if (i.BackColor == Settings.Highlights.ChangingColor) i.BackColor = Color.Empty;
                } else {
                    if (!Settings.Highlights.ChangingItems && i.BackColor == Settings.Highlights.ChangingColor) i.BackColor = Color.Empty;
                    if (Settings.Highlights.ChangingItems && !i.SubItems[1].Text.Equals("n/a.") && !i.SubItems[2].Text.Equals("n/a.")) i.BackColor = Settings.Highlights.ChangingColor;
                    i.SubItems[1].Text = newValue1;
                    i.SubItems[2].Text = newValue2;
                }
            }
        } catch (Exception ex) { Shared.DebugTrap(ex, 123); } finally { lvUDP.ResumeLayout(); }
    }
    private void Feature_UpdateICMPstats() {
        try {
            ListViewItem? i;
            API.MIB_ICMPINFO s4 = new();
            TaskManagerNetwork.GetICMPv4Statistics(ref s4);
            if (_ICMPstatsValues.Count == 0) StatsSetValueNames();
            // Set Initial Columns
            if (lvICMP.Columns.Count == 0) {
                lvICMP.Columns.Add("ICMP Parameter", 250);
                lvICMP.Columns.Add("Received", 100, HorizontalAlignment.Right);
                lvICMP.Columns.Add("Sent", 100, HorizontalAlignment.Right);
            }
            lvICMP.SuspendLayout();
            // Add or Update all Values
            foreach (KeyValuePair<string, string> e in _ICMPstatsValues) {
                if (lvICMP.Items.ContainsKey(e.Value)) {
                    i = lvICMP.Items[e.Value];
                } else {
                    i = new ListViewItem { Name = e.Value, Text = e.Key };
                    i.SubItems.Add("n/a.");
                    i.SubItems.Add("n/a.");
                    i.SubItems.Add("n/a.");
                    i.SubItems.Add("n/a.");
                    lvICMP.Items.Add(i);
                }
                string newValue1 = s4.icmpInStats.GetType().GetField(e.Value)!.GetValue(s4.icmpInStats)!.ToString()!;
                string newValue2 = s4.icmpOutStats.GetType().GetField(e.Value)!.GetValue(s4.icmpOutStats)!.ToString()!;
                if (i.SubItems[1].Text.Equals(newValue1) && i.SubItems[2].Text.Equals(newValue2)) {
                    if (i.BackColor == Settings.Highlights.ChangingColor) i.BackColor = Color.Empty;
                } else {
                    if (!Settings.Highlights.ChangingItems && i.BackColor == Settings.Highlights.ChangingColor) i.BackColor = Color.Empty;
                    if (Settings.Highlights.ChangingItems && !i.SubItems[1].Text.Equals("n/a.") && !i.SubItems[2].Text.Equals("n/a.")) i.BackColor = Settings.Highlights.ChangingColor;
                    i.SubItems[1].Text = newValue1;
                    i.SubItems[2].Text = newValue2;
                }
            }
        } catch (Exception ex) { Shared.DebugTrap(ex, 124); } finally { lvICMP.ResumeLayout(); }
    }
    private void Feature_ForceRefresh() {
        lv.SuspendLayout();
        Nics.Clear();
        lv.Items.Clear();
        Refresher(true);
        lv.ResumeLayout();
        ForceRefreshClicked?.Invoke(this, EventArgs.Empty);
    }

    public sMkListView? ListView => lv;
    public string Title { get; set; } = "Networking";
    public string Description { get; set; } = "Networking";
    public string TimmingKey => "Net";
    public long TimmingValue => _stopWatch.ElapsedMilliseconds;
    public bool CanSelectColumns => true;
    public TaskManagerColumnTypes ColumnType => TaskManagerColumnTypes.Nics;
    public ListView.ColumnHeaderCollection? GetColumns() => lv.Columns;
    public void ForceRefresh() => btnForceRefresh.PerformClick();
    public void SetColumns(in ListView.ListViewItemCollection colItems) {
        lv.SetColumns(colItems);
        ColsNics = lv.Columns.Cast<ColumnHeader>().Select(x => x.Name).ToHashSet()!;
    }

    private void RefresherDoWork(bool firstTime = false) {
        RefreshStarts?.Invoke(this, EventArgs.Empty);
        if (lv.Items.Count == 0) firstTime = true;

        // Store last round items and initialize new ones
        HashSet<string> LastRun = new();
        LastRun.UnionWith(HashNics);
        HashNics.Clear();
        // Iterate through all the items
        foreach (System.Net.NetworkInformation.NetworkInterface i in TaskManagerNic.GetInterfaces(true)) {
            TaskManagerNic thisNIC = new(i.Id);
            HashNics.Add(i.Id);
            if (Nics.Contains(i.Id)) {
                thisNIC = Nics.GetNic(i.Id);
                try {
                    thisNIC.Update(i);
                } catch (Exception ex) { Shared.DebugTrap(ex, 047); }
            } else {
                try {
                    thisNIC.Load(i);
                } catch (Exception ex) { Shared.DebugTrap(ex, 048); }
                Nics.Add(thisNIC);
            }
            if (pChart1.Tag != null && pChart1.Tag.Equals(thisNIC.Ident)) pChart1.AddValue(thisNIC.RcvdRateValue / 1024d, thisNIC.SentRateValue / 1024d);
            if (pChart2.Tag != null && pChart2.Tag.Equals(thisNIC.Ident)) pChart2.AddValue(thisNIC.RcvdRateValue / 1024d, thisNIC.SentRateValue / 1024d);
            if (pChart3.Tag != null && pChart3.Tag.Equals(thisNIC.Ident)) pChart3.AddValue(thisNIC.RcvdRateValue / 1024d, thisNIC.SentRateValue / 1024d);
        }
        // Clean out old Items
        LastRun.ExceptWith(HashNics);
        for (int i = 0; i < LastRun.Count; i++) {
            TaskManagerNic thisNIC = Nics.GetNic(LastRun.ElementAtOrDefault(i)!);
            if (thisNIC == null) continue;
            lv.RemoveItemByKey(thisNIC.Ident);
            Nics.Remove(thisNIC);
        }
        // Load Checked Interfaces...
        if (firstTime) {
            foreach (ListViewItem i in lv.Items) {
                i.Checked = Settings.CheckedInterfaces.Contains(i.Name);
                if (lv.CheckedItems.Count >= 3) break;
            }
            if (lv.CheckedItems.Count == 0 && lv.Items.Count > 0) {
                foreach (ListViewItem i in lv.Items) {
                    i.Checked = true;
                    if (lv.CheckedItems.Count >= 3) break;
                }
            }
        }
        // Update Statistics, if needed
        if (tc.SelectedTab == tpIP) Feature_UpdateIPstats();
        if (tc.SelectedTab == tpTCP) Feature_UpdateTCPstats();
        if (tc.SelectedTab == tpUDP) Feature_UpdateUDPstats();
        if (tc.SelectedTab == tpICMP) Feature_UpdateICMPstats();
        RefreshComplete?.Invoke(this, EventArgs.Empty);
    }
    public void Refresher(bool firstTime = false) {
        _stopWatch.Restart();
        if (Visible || firstTime || Settings.Networking.KeepUpdating) {
            if (InvokeRequired) {
                Invoke(() => RefresherDoWork(firstTime));
            } else {
                RefresherDoWork(firstTime);
            }
        }
        _stopWatch.Stop();
    }
    public void LoadSettings() {
        Settings.LoadInterfaces();
        Settings.LoadNetworking();
        Settings.LoadColsInformation(ColumnType, lv, ref ColsNics);
    }
    public bool SaveSettings() {
        return Settings.SaveNetworking() && Settings.SaveInterfaces();
        // && Settings.SaveColsInformation("colsNics", lv);
    }
    public void ApplySettings() {
        pChart1.BackSolid = Settings.Networking.Solid;
        pChart1.AntiAliasing = Settings.Networking.AntiAlias;
        pChart1.ShadeBackground = Settings.Networking.ShadeBackground;
        pChart1.DisplayAverage = Settings.Networking.DisplayAverages;
        pChart1.DisplayLegends = Settings.Networking.DisplayLegends;
        pChart1.DisplayIndexes = Settings.Networking.DisplayIndexes;
        pChart1.ValueSpacing = Settings.Networking.ValueSpacing;
        pChart1.GridSpacing = Settings.Networking.GridSize;
        pChart1.PenGridVertical.DashStyle = (System.Drawing.Drawing2D.DashStyle)Settings.Networking.VerticalGridStyle;
        pChart1.PenGridVertical.Color = Settings.Networking.VerticalGridColor;
        pChart1.PenGridHorizontal.DashStyle = (System.Drawing.Drawing2D.DashStyle)Settings.Networking.HorizontalGridStyle;
        pChart1.PenGridHorizontal.Color = Settings.Networking.HorizontalGridColor;
        pChart1.PenAverage.DashStyle = (System.Drawing.Drawing2D.DashStyle)Settings.Networking.AverageLineStyle;
        pChart1.PenAverage.Color = Settings.Networking.AverageLineColor;
        pChart1.LightColors = Settings.Networking.LightColors;
        pChart1.PenGraph1.Color = Settings.Networking.DownloadColor;
        pChart1.PenGraph2.Color = Settings.Networking.UploadColor;
        pChart2.CopySettings(pChart1);
        pChart3.CopySettings(pChart1);
    }

}
