using System.ComponentModel;
using System.Runtime.Versioning;
using sMkTaskManager.Classes;
namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
internal class tabSystem : UserControl, ITaskManagerTab {
    private readonly TaskManagerSystemProps SysProps = new();
    public event EventHandler? RefreshStarts;
    public event EventHandler? RefreshComplete;
    public event EventHandler? ForceRefreshClicked;

    # region Form Controls
    private TableLayoutPanel tlp;
    private Panel pnlInfo;
    private Panel pnlLogos;
    private Label lblSystem;
    private Label lblSystem1;
    private Label lblSystem2;
    private Label lblSystem3;
    private Label lblSystem4;
    private Label lblRegister;
    private Label lblRegister1;
    private Label lblRegister2;
    private Label lblRegister3;
    private Label lblComputer;
    private Label lblComputer1;
    private Label lblComputer2;
    private Label lblComputer3;
    private Label lblComputer4;
    private Label lblComputer5;
    private Label lblManufacturer;
    private PictureBox pbLogo;
    private PictureBox pbManufacturer;
    private Label lblSupport;
    private Label lblSupport1;
    private Label lblSupport2;
    private Label lblSupport3;
    private Label lblSupport4;
    # endregion
    private readonly IContainer? components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }
    protected override void OnPaint(PaintEventArgs e) {
        base.OnPaint(e);
        ControlPaint.DrawBorder3D(e.Graphics, 0, 0, Width, Height, BorderStyle);
    }

    public tabSystem() {
        InitializeComponent();
        InitializeHandlers();
        InitializeExtras();
        Extensions.CascadingDoubleBuffer(this);
    }
    private void InitializeComponent() {
        pnlInfo = new Panel();
        lblSupport = new Label();
        lblSupport1 = new Label();
        lblSupport2 = new Label();
        lblSupport3 = new Label();
        lblSupport4 = new Label();
        lblSystem = new Label();
        lblSystem1 = new Label();
        lblSystem2 = new Label();
        lblSystem3 = new Label();
        lblSystem4 = new Label();
        lblRegister = new Label();
        lblRegister1 = new Label();
        lblRegister2 = new Label();
        lblRegister3 = new Label();
        lblComputer = new Label();
        lblComputer1 = new Label();
        lblComputer2 = new Label();
        lblComputer3 = new Label();
        lblComputer4 = new Label();
        lblComputer5 = new Label();
        pnlLogos = new Panel();
        pbManufacturer = new PictureBox();
        lblManufacturer = new Label();
        pbLogo = new PictureBox();
        tlp = new TableLayoutPanel();
        pnlInfo.SuspendLayout();
        pnlLogos.SuspendLayout();
        ((ISupportInitialize)pbManufacturer).BeginInit();
        ((ISupportInitialize)pbLogo).BeginInit();
        tlp.SuspendLayout();
        SuspendLayout();
        // 
        // pnlInfo
        // 
        pnlInfo.Controls.Add(lblSupport);
        pnlInfo.Controls.Add(lblSupport1);
        pnlInfo.Controls.Add(lblSupport2);
        pnlInfo.Controls.Add(lblSupport3);
        pnlInfo.Controls.Add(lblSupport4);
        pnlInfo.Controls.Add(lblSystem);
        pnlInfo.Controls.Add(lblSystem1);
        pnlInfo.Controls.Add(lblSystem2);
        pnlInfo.Controls.Add(lblSystem3);
        pnlInfo.Controls.Add(lblSystem4);
        pnlInfo.Controls.Add(lblRegister);
        pnlInfo.Controls.Add(lblRegister1);
        pnlInfo.Controls.Add(lblRegister2);
        pnlInfo.Controls.Add(lblRegister3);
        pnlInfo.Controls.Add(lblComputer);
        pnlInfo.Controls.Add(lblComputer1);
        pnlInfo.Controls.Add(lblComputer2);
        pnlInfo.Controls.Add(lblComputer3);
        pnlInfo.Controls.Add(lblComputer4);
        pnlInfo.Controls.Add(lblComputer5);
        pnlInfo.Dock = DockStyle.Fill;
        pnlInfo.Location = new Point(215, 0);
        pnlInfo.Margin = new Padding(0);
        pnlInfo.Name = "pnlInfo";
        pnlInfo.Size = new Size(275, 664);
        pnlInfo.TabIndex = 1;
        // 
        // lblSupport
        // 
        lblSupport.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblSupport.Location = new Point(4, 301);
        lblSupport.Name = "lblSupport";
        lblSupport.Size = new Size(268, 20);
        lblSupport.TabIndex = 15;
        lblSupport.Text = "Support Information:";
        // 
        // lblSupport1
        // 
        lblSupport1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblSupport1.Location = new Point(24, 321);
        lblSupport1.Name = "lblSupport1";
        lblSupport1.Size = new Size(247, 18);
        lblSupport1.TabIndex = 16;
        lblSupport1.Visible = false;
        // 
        // lblSupport2
        // 
        lblSupport2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblSupport2.Location = new Point(24, 339);
        lblSupport2.Name = "lblSupport2";
        lblSupport2.Size = new Size(247, 18);
        lblSupport2.TabIndex = 17;
        lblSupport2.Visible = false;
        // 
        // lblSupport3
        // 
        lblSupport3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblSupport3.Location = new Point(24, 357);
        lblSupport3.Name = "lblSupport3";
        lblSupport3.Size = new Size(247, 18);
        lblSupport3.TabIndex = 18;
        lblSupport3.Visible = false;
        // 
        // lblSupport4
        // 
        lblSupport4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblSupport4.Location = new Point(24, 375);
        lblSupport4.Name = "lblSupport4";
        lblSupport4.Size = new Size(247, 18);
        lblSupport4.TabIndex = 19;
        lblSupport4.Visible = false;
        // 
        // lblSystem
        // 
        lblSystem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblSystem.Location = new Point(4, 1);
        lblSystem.Name = "lblSystem";
        lblSystem.Size = new Size(268, 20);
        lblSystem.TabIndex = 0;
        lblSystem.Text = "System:";
        // 
        // lblSystem1
        // 
        lblSystem1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblSystem1.Location = new Point(24, 21);
        lblSystem1.Name = "lblSystem1";
        lblSystem1.Size = new Size(247, 18);
        lblSystem1.TabIndex = 1;
        // 
        // lblSystem2
        // 
        lblSystem2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblSystem2.Location = new Point(24, 39);
        lblSystem2.Name = "lblSystem2";
        lblSystem2.Size = new Size(247, 18);
        lblSystem2.TabIndex = 2;
        // 
        // lblSystem3
        // 
        lblSystem3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblSystem3.Location = new Point(24, 57);
        lblSystem3.Name = "lblSystem3";
        lblSystem3.Size = new Size(247, 18);
        lblSystem3.TabIndex = 3;
        // 
        // lblSystem4
        // 
        lblSystem4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblSystem4.Location = new Point(24, 75);
        lblSystem4.Name = "lblSystem4";
        lblSystem4.Size = new Size(247, 18);
        lblSystem4.TabIndex = 4;
        // 
        // lblRegister
        // 
        lblRegister.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblRegister.Location = new Point(3, 101);
        lblRegister.Name = "lblRegister";
        lblRegister.Size = new Size(268, 20);
        lblRegister.TabIndex = 5;
        lblRegister.Text = "Registered To:";
        // 
        // lblRegister1
        // 
        lblRegister1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblRegister1.Location = new Point(24, 121);
        lblRegister1.Name = "lblRegister1";
        lblRegister1.Size = new Size(247, 18);
        lblRegister1.TabIndex = 6;
        // 
        // lblRegister2
        // 
        lblRegister2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblRegister2.Location = new Point(24, 139);
        lblRegister2.Name = "lblRegister2";
        lblRegister2.Size = new Size(247, 18);
        lblRegister2.TabIndex = 7;
        // 
        // lblRegister3
        // 
        lblRegister3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblRegister3.Location = new Point(24, 157);
        lblRegister3.Name = "lblRegister3";
        lblRegister3.Size = new Size(247, 18);
        lblRegister3.TabIndex = 8;
        // 
        // lblComputer
        // 
        lblComputer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblComputer.Location = new Point(4, 183);
        lblComputer.Name = "lblComputer";
        lblComputer.Size = new Size(268, 20);
        lblComputer.TabIndex = 9;
        lblComputer.Text = "Computer:";
        // 
        // lblComputer1
        // 
        lblComputer1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblComputer1.Location = new Point(24, 203);
        lblComputer1.Name = "lblComputer1";
        lblComputer1.Size = new Size(247, 18);
        lblComputer1.TabIndex = 10;
        // 
        // lblComputer2
        // 
        lblComputer2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblComputer2.Location = new Point(24, 221);
        lblComputer2.Name = "lblComputer2";
        lblComputer2.Size = new Size(247, 18);
        lblComputer2.TabIndex = 11;
        // 
        // lblComputer3
        // 
        lblComputer3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblComputer3.Location = new Point(24, 239);
        lblComputer3.Name = "lblComputer3";
        lblComputer3.Size = new Size(247, 18);
        lblComputer3.TabIndex = 12;
        // 
        // lblComputer4
        // 
        lblComputer4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblComputer4.Location = new Point(24, 257);
        lblComputer4.Name = "lblComputer4";
        lblComputer4.Size = new Size(247, 18);
        lblComputer4.TabIndex = 13;
        // 
        // lblComputer5
        // 
        lblComputer5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblComputer5.Location = new Point(24, 275);
        lblComputer5.Name = "lblComputer5";
        lblComputer5.Size = new Size(247, 18);
        lblComputer5.TabIndex = 14;
        // 
        // pnlLogos
        // 
        pnlLogos.Controls.Add(pbManufacturer);
        pnlLogos.Controls.Add(lblManufacturer);
        pnlLogos.Controls.Add(pbLogo);
        pnlLogos.Dock = DockStyle.Fill;
        pnlLogos.Location = new Point(0, 0);
        pnlLogos.Margin = new Padding(0);
        pnlLogos.Name = "pnlLogos";
        pnlLogos.Size = new Size(215, 664);
        pnlLogos.TabIndex = 0;
        // 
        // pbManufacturer
        // 
        pbManufacturer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pbManufacturer.Location = new Point(0, 210);
        pbManufacturer.Name = "pbManufacturer";
        pbManufacturer.Size = new Size(215, 130);
        pbManufacturer.SizeMode = PictureBoxSizeMode.CenterImage;
        pbManufacturer.TabIndex = 7;
        pbManufacturer.TabStop = false;
        // 
        // lblManufacturer
        // 
        lblManufacturer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblManufacturer.AutoEllipsis = true;
        lblManufacturer.Location = new Point(1, 194);
        lblManufacturer.Name = "lblManufacturer";
        lblManufacturer.Size = new Size(213, 15);
        lblManufacturer.TabIndex = 0;
        lblManufacturer.Text = "Manufactured and supported by:";
        lblManufacturer.TextAlign = ContentAlignment.BottomCenter;
        // 
        // pbLogo
        // 
        pbLogo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pbLogo.Location = new Point(0, 10);
        pbLogo.Name = "pbLogo";
        pbLogo.Size = new Size(215, 150);
        pbLogo.SizeMode = PictureBoxSizeMode.CenterImage;
        pbLogo.TabIndex = 0;
        pbLogo.TabStop = false;
        // 
        // tlp
        // 
        tlp.ColumnCount = 2;
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44F));
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56F));
        tlp.Controls.Add(pnlInfo, 1, 0);
        tlp.Controls.Add(pnlLogos, 0, 0);
        tlp.Dock = DockStyle.Fill;
        tlp.Location = new Point(10, 15);
        tlp.Name = "tlp";
        tlp.RowCount = 1;
        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tlp.Size = new Size(490, 664);
        tlp.TabIndex = 7;
        // 
        // tabSystem
        // 
        Controls.Add(tlp);
        Name = "tabSystem";
        Padding = new Padding(10, 15, 0, 0);
        Size = new Size(500, 679);
        pnlInfo.ResumeLayout(false);
        pnlLogos.ResumeLayout(false);
        ((ISupportInitialize)pbManufacturer).EndInit();
        ((ISupportInitialize)pbLogo).EndInit();
        tlp.ResumeLayout(false);
        ResumeLayout(false);
    }
    private void InitializeHandlers() {
        VisibleChanged += OnVisibleChanged;
        lblSystem1.MouseDoubleClick += OnLabelMouseDoubleClick;
        lblSystem2.MouseDoubleClick += OnLabelMouseDoubleClick;
        lblSystem3.MouseDoubleClick += OnLabelMouseDoubleClick;
        lblSystem4.MouseDoubleClick += OnLabelMouseDoubleClick;
        lblRegister1.MouseDoubleClick += OnLabelMouseDoubleClick;
        lblRegister2.MouseDoubleClick += OnLabelMouseDoubleClick;
        lblRegister3.MouseDoubleClick += OnLabelMouseDoubleClick;
        lblComputer1.MouseDoubleClick += OnLabelMouseDoubleClick;
        lblComputer2.MouseDoubleClick += OnLabelMouseDoubleClick;
        lblComputer3.MouseDoubleClick += OnLabelMouseDoubleClick;
        lblComputer4.MouseDoubleClick += OnLabelMouseDoubleClick;
        lblComputer5.MouseDoubleClick += OnLabelMouseDoubleClick;
        lblSupport1.MouseDoubleClick += OnLabelMouseDoubleClick;
        lblSupport2.MouseDoubleClick += OnLabelMouseDoubleClick;
        lblSupport3.MouseDoubleClick += OnLabelMouseDoubleClick;
        lblSupport4.MouseDoubleClick += OnLabelMouseDoubleClick;
        pbLogo.MouseDoubleClick += (obj, e) => pbLogo.Image = RandomizeWinLogo();
    }
    private void InitializeExtras() {
        pbLogo.Image = RandomizeWinLogo();
        pbManufacturer.Visible = false;
        lblManufacturer.Visible = false;
        ShowSupportInformation = false;
        lblSupport.Visible = true;
    }
    private Image RandomizeWinLogo() {
        pbLogo.Tag ??= 0;
        Random rnd = new();
        var newInt = rnd.Next(1, 9);
        while (newInt == (int)pbLogo.Tag) { newInt = rnd.Next(1, 9); }
        pbLogo.Tag = newInt;
        return pbLogo.Tag switch {
            1 => Resources.Resources.winpc_logo1,
            2 => Resources.Resources.winpc_logo2,
            3 => Resources.Resources.winpc_logo3,
            4 => Resources.Resources.winpc_logo4,
            5 => Resources.Resources.winpc_logo5,
            6 => Resources.Resources.winpc_logo6,
            7 => Resources.Resources.winpc_logo7,
            8 => Resources.Resources.winpc_logo8,
            _ => Resources.Resources.winpc_logo1,
        };
    }
    private bool ShowSupportInformation {
        set {
            lblSupport.Visible = value;
            lblSupport1.Visible = value;
            lblSupport2.Visible = value;
            lblSupport3.Visible = value;
            lblSupport4.Visible = value;
        }
    }
    private void OnVisibleChanged(object? sender, EventArgs e) {
        if (Visible && string.IsNullOrWhiteSpace(lblSystem.Text) && Shared.InitComplete) {
            SuspendLayout(); Refresher(true); ResumeLayout();
        } else if (Visible && Shared.InitComplete) {
            pbLogo.Image = RandomizeWinLogo();
        }
    }
    private void OnLabelMouseDoubleClick(object? sender, MouseEventArgs e) {
        if (e.Button == MouseButtons.Left && sender is Label lbl) {
            Clipboard.SetText(lbl.Text);
            Shared.SetStatusText("Text copied to clipboard...");
        };
    }

    public void Feature_ForceRefresh() {
        Shared.SetStatusText("System Properties Refreshed...");
        ForceRefreshClicked?.Invoke(this, EventArgs.Empty);
        Refresher(true);
    }
    public void Feature_SupportInformation() {
        var msg = $"Manufacturer:\t{SysProps.OEMManufacturer}\r\nPhone:\t\t{SysProps.OEMSupportPhone}\r\nHours:\t\t{SysProps.OEMSupportHours}\r\nWebsite:\t\t{SysProps.OEMSupportURL}";
        MessageBox.Show(msg, "Support Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public string Title { get; set; } = "PC";
    public string Description { get; set; } = "System Properties";
    public void ForceRefresh() => Feature_ForceRefresh();
    public new Border3DStyle BorderStyle { get; set; } = Border3DStyle.Adjust;

    private void RefresherDoWork(bool firstTime = false) {
        if (lblSystem1.Text == "") firstTime = true;
        if (firstTime) {
            RefreshStarts?.Invoke(this, EventArgs.Empty);
            SysProps.Refresh();
            // Populate Windows Information
            lblSystem1.Text = SysProps.WindowsName;
            lblSystem2.Text = SysProps.WindowsEdition;
            lblSystem3.Text = $"Version {SysProps.WindowsVersion}";
            lblSystem4.Text = $"Installed on: {SysProps.InstallDate.ToLocalTime()}";
            // Populate Registration Information
            lblRegister1.Text = SysProps.RegisterUser;
            lblRegister2.Text = SysProps.RegisterCompany;
            lblRegister3.Text = SysProps.RegisterKey;
            // Populate Computer Information
            lblComputer1.Text = $"{SysProps.SystemManufacturer} {SysProps.SystemProductName}";
            lblComputer2.Text = SysProps.ProcessorVendor.Replace("GenuineIntel", "Genuine Intel");
            lblComputer3.Text = SysProps.ProcessorName;
            lblComputer4.Text = $"{Environment.ProcessorCount} CPU(s) - Speed {SysProps.ProcessorSpeed}Mhz.";
            lblComputer5.Text = $"{SysProps.TotalMemory:0.0}Gb. RAM";
            // Populate Manufacturer information
            pbManufacturer.Image = SysProps.OEMLogo ?? default;
            lblManufacturer.Visible = pbManufacturer.Image != default;
            pbManufacturer.Visible = pbManufacturer.Image != default;
            lblSupport1.Text = SysProps.OEMManufacturer;
            lblSupport2.Text = $"Phone: {SysProps.OEMSupportPhone}";
            lblSupport3.Text = $"Hours: {SysProps.OEMSupportHours}";
            lblSupport4.Text = $"Website: {SysProps.OEMSupportURL}";
            ShowSupportInformation = !string.IsNullOrEmpty(SysProps.OEMManufacturer);
            RefreshComplete?.Invoke(this, EventArgs.Empty);
        }
    }
    public void Refresher(bool firstTime = false) {
        if (lblSystem1.Text == "") firstTime = true;
        if (firstTime) { Invoke(() => RefresherDoWork(firstTime)); }
    }

}