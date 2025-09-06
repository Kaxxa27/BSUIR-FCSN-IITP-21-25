namespace sMkTaskManager.Forms;
partial class frmService_Details {

    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }
    private void InitializeComponent() {
        components = new System.ComponentModel.Container();
        lbl01 = new Label();
        lbl02 = new Label();
        lbl05 = new Label();
        lbl04 = new Label();
        lbl03 = new Label();
        pb = new PictureBox();
        txtServiceName = new TextBox();
        txtDisplayName = new TextBox();
        txtLogonAs = new TextBox();
        txtCommandLine = new TextBox();
        txtDescription = new TextBox();
        lbl08 = new Label();
        lbl06 = new Label();
        lbl10 = new Label();
        txtStartMethod = new ComboBox();
        txtStatus = new Label();
        btnStart = new Button();
        btnStop = new Button();
        btnPause = new Button();
        btnResume = new Button();
        gbLine1 = new GroupBox();
        gbLine2 = new GroupBox();
        btnApply = new Button();
        btnCancel = new Button();
        btnOk = new Button();
        lbl11 = new Label();
        lbl12 = new Label();
        lbl09 = new Label();
        txtStartParams = new TextBox();
        tvDependsOn = new TreeView();
        ilDependencies = new ImageList(components);
        tvDependsTo = new TreeView();
        ((System.ComponentModel.ISupportInitialize)pb).BeginInit();
        SuspendLayout();
        // 
        // lbl01
        // 
        lbl01.Location = new Point(9, 10);
        lbl01.Margin = new Padding(4, 0, 4, 0);
        lbl01.Name = "lbl01";
        lbl01.Size = new Size(99, 15);
        lbl01.TabIndex = 0;
        lbl01.Text = "Service Name:";
        lbl01.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lbl02
        // 
        lbl02.Location = new Point(9, 31);
        lbl02.Margin = new Padding(4, 0, 4, 0);
        lbl02.Name = "lbl02";
        lbl02.Size = new Size(99, 15);
        lbl02.TabIndex = 2;
        lbl02.Text = "Display Name:";
        lbl02.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lbl05
        // 
        lbl05.Location = new Point(9, 94);
        lbl05.Margin = new Padding(4, 0, 4, 0);
        lbl05.Name = "lbl05";
        lbl05.Size = new Size(99, 15);
        lbl05.TabIndex = 8;
        lbl05.Text = "Description:";
        lbl05.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lbl04
        // 
        lbl04.Location = new Point(9, 73);
        lbl04.Margin = new Padding(4, 0, 4, 0);
        lbl04.Name = "lbl04";
        lbl04.Size = new Size(99, 15);
        lbl04.TabIndex = 6;
        lbl04.Text = "Command Line:";
        lbl04.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lbl03
        // 
        lbl03.Location = new Point(9, 52);
        lbl03.Margin = new Padding(4, 0, 4, 0);
        lbl03.Name = "lbl03";
        lbl03.Size = new Size(99, 15);
        lbl03.TabIndex = 4;
        lbl03.Text = "Run/Logon As:";
        lbl03.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // pb
        // 
        pb.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        pb.Image = Resources.Resources.Service;
        pb.Location = new Point(394, 8);
        pb.Margin = new Padding(4, 3, 4, 3);
        pb.Name = "pb";
        pb.Size = new Size(48, 48);
        pb.SizeMode = PictureBoxSizeMode.AutoSize;
        pb.TabIndex = 5;
        pb.TabStop = false;
        // 
        // txtServiceName
        // 
        txtServiceName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtServiceName.BorderStyle = BorderStyle.None;
        txtServiceName.Location = new Point(115, 9);
        txtServiceName.Margin = new Padding(4, 3, 4, 3);
        txtServiceName.Name = "txtServiceName";
        txtServiceName.ReadOnly = true;
        txtServiceName.Size = new Size(265, 16);
        txtServiceName.TabIndex = 1;
        txtServiceName.Text = "x";
        txtServiceName.WordWrap = false;
        // 
        // txtDisplayName
        // 
        txtDisplayName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtDisplayName.BorderStyle = BorderStyle.None;
        txtDisplayName.Location = new Point(115, 30);
        txtDisplayName.Margin = new Padding(4, 3, 4, 3);
        txtDisplayName.Name = "txtDisplayName";
        txtDisplayName.ReadOnly = true;
        txtDisplayName.Size = new Size(265, 16);
        txtDisplayName.TabIndex = 3;
        txtDisplayName.Text = "x";
        txtDisplayName.WordWrap = false;
        // 
        // txtLogonAs
        // 
        txtLogonAs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtLogonAs.BorderStyle = BorderStyle.None;
        txtLogonAs.Location = new Point(115, 51);
        txtLogonAs.Margin = new Padding(4, 3, 4, 3);
        txtLogonAs.Name = "txtLogonAs";
        txtLogonAs.ReadOnly = true;
        txtLogonAs.Size = new Size(265, 16);
        txtLogonAs.TabIndex = 5;
        txtLogonAs.Text = "x";
        txtLogonAs.WordWrap = false;
        // 
        // txtCommandLine
        // 
        txtCommandLine.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtCommandLine.BorderStyle = BorderStyle.None;
        txtCommandLine.Location = new Point(115, 72);
        txtCommandLine.Margin = new Padding(4, 3, 4, 3);
        txtCommandLine.Name = "txtCommandLine";
        txtCommandLine.ReadOnly = true;
        txtCommandLine.Size = new Size(326, 16);
        txtCommandLine.TabIndex = 7;
        txtCommandLine.Text = "x";
        txtCommandLine.WordWrap = false;
        // 
        // txtDescription
        // 
        txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtDescription.BorderStyle = BorderStyle.None;
        txtDescription.Location = new Point(112, 94);
        txtDescription.Margin = new Padding(4, 3, 4, 3);
        txtDescription.Multiline = true;
        txtDescription.Name = "txtDescription";
        txtDescription.ReadOnly = true;
        txtDescription.ScrollBars = ScrollBars.Vertical;
        txtDescription.Size = new Size(328, 46);
        txtDescription.TabIndex = 9;
        txtDescription.Text = "x";
        // 
        // lbl08
        // 
        lbl08.Location = new Point(10, 185);
        lbl08.Margin = new Padding(4, 0, 4, 6);
        lbl08.Name = "lbl08";
        lbl08.Size = new Size(100, 18);
        lbl08.TabIndex = 13;
        lbl08.Text = "Startup Method:";
        lbl08.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lbl06
        // 
        lbl06.Location = new Point(9, 160);
        lbl06.Margin = new Padding(4, 0, 4, 6);
        lbl06.Name = "lbl06";
        lbl06.Size = new Size(100, 18);
        lbl06.TabIndex = 11;
        lbl06.Text = "Service Status:";
        lbl06.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lbl10
        // 
        lbl10.Location = new Point(10, 240);
        lbl10.Margin = new Padding(4, 0, 4, 6);
        lbl10.Name = "lbl10";
        lbl10.Size = new Size(100, 18);
        lbl10.TabIndex = 17;
        lbl10.Text = "Service Control:";
        lbl10.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtStartMethod
        // 
        txtStartMethod.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtStartMethod.DropDownStyle = ComboBoxStyle.DropDownList;
        txtStartMethod.FormattingEnabled = true;
        txtStartMethod.Items.AddRange(new object[] { "Automatic", "Automatic (Delayed Start)", "Manual", "Disabled" });
        txtStartMethod.Location = new Point(115, 182);
        txtStartMethod.Margin = new Padding(4, 3, 4, 3);
        txtStartMethod.Name = "txtStartMethod";
        txtStartMethod.Size = new Size(326, 23);
        txtStartMethod.TabIndex = 14;
        txtStartMethod.SelectedIndexChanged += txtStartMethod_SelectedIndexChanged;
        // 
        // txtStatus
        // 
        txtStatus.ForeColor = Color.Maroon;
        txtStatus.Location = new Point(115, 160);
        txtStatus.Margin = new Padding(4, 0, 4, 0);
        txtStatus.Name = "txtStatus";
        txtStatus.Size = new Size(340, 18);
        txtStatus.TabIndex = 12;
        txtStatus.Text = "Status";
        txtStatus.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // btnStart
        // 
        btnStart.Anchor = AnchorStyles.Top;
        btnStart.Location = new Point(114, 237);
        btnStart.Margin = new Padding(4, 3, 4, 3);
        btnStart.Name = "btnStart";
        btnStart.Size = new Size(80, 24);
        btnStart.TabIndex = 18;
        btnStart.Text = "Start";
        btnStart.UseVisualStyleBackColor = true;
        btnStart.Click += btnStart_Click;
        // 
        // btnStop
        // 
        btnStop.Anchor = AnchorStyles.Top;
        btnStop.Location = new Point(197, 237);
        btnStop.Margin = new Padding(4, 3, 4, 3);
        btnStop.Name = "btnStop";
        btnStop.Size = new Size(80, 24);
        btnStop.TabIndex = 19;
        btnStop.Text = "Stop";
        btnStop.UseVisualStyleBackColor = true;
        btnStop.Click += btnStop_Click;
        // 
        // btnPause
        // 
        btnPause.Anchor = AnchorStyles.Top;
        btnPause.Location = new Point(279, 237);
        btnPause.Margin = new Padding(4, 3, 4, 3);
        btnPause.Name = "btnPause";
        btnPause.Size = new Size(80, 24);
        btnPause.TabIndex = 20;
        btnPause.Text = "Pause";
        btnPause.UseVisualStyleBackColor = true;
        btnPause.Click += btnPause_Click;
        // 
        // btnResume
        // 
        btnResume.Anchor = AnchorStyles.Top;
        btnResume.Location = new Point(362, 237);
        btnResume.Margin = new Padding(4, 3, 4, 3);
        btnResume.Name = "btnResume";
        btnResume.Size = new Size(80, 24);
        btnResume.TabIndex = 21;
        btnResume.Text = "Resume";
        btnResume.UseVisualStyleBackColor = true;
        btnResume.Click += btnResume_Click;
        // 
        // gbLine1
        // 
        gbLine1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbLine1.Location = new Point(8, 146);
        gbLine1.Margin = new Padding(4, 3, 4, 3);
        gbLine1.Name = "gbLine1";
        gbLine1.Padding = new Padding(4, 3, 4, 3);
        gbLine1.Size = new Size(436, 5);
        gbLine1.TabIndex = 10;
        gbLine1.TabStop = false;
        // 
        // gbLine2
        // 
        gbLine2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbLine2.Location = new Point(8, 269);
        gbLine2.Margin = new Padding(4, 3, 4, 3);
        gbLine2.Name = "gbLine2";
        gbLine2.Padding = new Padding(4, 3, 4, 3);
        gbLine2.Size = new Size(436, 5);
        gbLine2.TabIndex = 22;
        gbLine2.TabStop = false;
        // 
        // btnApply
        // 
        btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnApply.Location = new Point(356, 467);
        btnApply.Margin = new Padding(4, 3, 4, 3);
        btnApply.Name = "btnApply";
        btnApply.Size = new Size(88, 27);
        btnApply.TabIndex = 29;
        btnApply.Text = "Apply";
        btnApply.UseVisualStyleBackColor = true;
        btnApply.Click += btnApply_Click;
        // 
        // btnCancel
        // 
        btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(261, 467);
        btnCancel.Margin = new Padding(4, 3, 4, 3);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(88, 27);
        btnCancel.TabIndex = 28;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;
        // 
        // btnOk
        // 
        btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnOk.DialogResult = DialogResult.OK;
        btnOk.Location = new Point(167, 467);
        btnOk.Margin = new Padding(4, 3, 4, 3);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(88, 27);
        btnOk.TabIndex = 27;
        btnOk.Text = "OK";
        btnOk.UseVisualStyleBackColor = true;
        btnOk.Click += btnOk_Click;
        // 
        // lbl11
        // 
        lbl11.Location = new Point(6, 281);
        lbl11.Margin = new Padding(4, 0, 4, 0);
        lbl11.Name = "lbl11";
        lbl11.Size = new Size(216, 18);
        lbl11.TabIndex = 23;
        lbl11.Text = "This service depends on:";
        lbl11.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lbl12
        // 
        lbl12.Location = new Point(228, 281);
        lbl12.Margin = new Padding(4, 0, 4, 0);
        lbl12.Name = "lbl12";
        lbl12.Size = new Size(215, 18);
        lbl12.TabIndex = 25;
        lbl12.Text = "These following depends on this:";
        lbl12.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lbl09
        // 
        lbl09.Location = new Point(9, 212);
        lbl09.Margin = new Padding(4, 0, 4, 6);
        lbl09.Name = "lbl09";
        lbl09.Size = new Size(100, 18);
        lbl09.TabIndex = 15;
        lbl09.Text = "Start Parameters:";
        lbl09.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtStartParams
        // 
        txtStartParams.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtStartParams.BorderStyle = BorderStyle.FixedSingle;
        txtStartParams.Location = new Point(115, 210);
        txtStartParams.Margin = new Padding(4, 3, 4, 3);
        txtStartParams.Name = "txtStartParams";
        txtStartParams.Size = new Size(326, 23);
        txtStartParams.TabIndex = 16;
        // 
        // tvDependsOn
        // 
        tvDependsOn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        tvDependsOn.BorderStyle = BorderStyle.FixedSingle;
        tvDependsOn.FullRowSelect = true;
        tvDependsOn.Indent = 15;
        tvDependsOn.Location = new Point(7, 302);
        tvDependsOn.Margin = new Padding(4, 3, 4, 3);
        tvDependsOn.Name = "tvDependsOn";
        tvDependsOn.Size = new Size(215, 157);
        tvDependsOn.StateImageList = ilDependencies;
        tvDependsOn.TabIndex = 24;
        // 
        // ilDependencies
        // 
        ilDependencies.ColorDepth = ColorDepth.Depth32Bit;
        ilDependencies.ImageSize = new Size(16, 16);
        ilDependencies.TransparentColor = Color.Transparent;
        // 
        // tvDependsTo
        // 
        tvDependsTo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        tvDependsTo.BorderStyle = BorderStyle.FixedSingle;
        tvDependsTo.FullRowSelect = true;
        tvDependsTo.Indent = 15;
        tvDependsTo.Location = new Point(228, 302);
        tvDependsTo.Margin = new Padding(4, 3, 4, 3);
        tvDependsTo.Name = "tvDependsTo";
        tvDependsTo.Size = new Size(215, 157);
        tvDependsTo.StateImageList = ilDependencies;
        tvDependsTo.TabIndex = 26;
        // 
        // frmService_Details
        // 
        AcceptButton = btnOk;
        CancelButton = btnCancel;
        ClientSize = new Size(451, 501);
        Controls.Add(tvDependsTo);
        Controls.Add(tvDependsOn);
        Controls.Add(txtStartParams);
        Controls.Add(lbl09);
        Controls.Add(lbl12);
        Controls.Add(lbl11);
        Controls.Add(txtServiceName);
        Controls.Add(txtDisplayName);
        Controls.Add(txtLogonAs);
        Controls.Add(btnOk);
        Controls.Add(lbl01);
        Controls.Add(gbLine2);
        Controls.Add(btnCancel);
        Controls.Add(lbl02);
        Controls.Add(btnApply);
        Controls.Add(btnResume);
        Controls.Add(lbl05);
        Controls.Add(txtDescription);
        Controls.Add(btnPause);
        Controls.Add(txtCommandLine);
        Controls.Add(lbl04);
        Controls.Add(lbl08);
        Controls.Add(btnStop);
        Controls.Add(lbl06);
        Controls.Add(lbl03);
        Controls.Add(lbl10);
        Controls.Add(btnStart);
        Controls.Add(txtStartMethod);
        Controls.Add(pb);
        Controls.Add(gbLine1);
        Controls.Add(txtStatus);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = Resources.Resources.frmService_Details;
        Margin = new Padding(4, 3, 4, 3);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "frmService_Details";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Service Details...";
        Load += frmService_Details_Load;
        ((System.ComponentModel.ISupportInitialize)pb).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lbl01;
    private Label lbl02;
    private Label lbl05;
    private Label lbl04;
    private Label lbl03;
    private PictureBox pb;
    private TextBox txtServiceName;
    private TextBox txtDisplayName;
    private TextBox txtLogonAs;
    private TextBox txtCommandLine;
    private TextBox txtDescription;
    private Label lbl08;
    private Label lbl06;
    private Label lbl10;
    private ComboBox txtStartMethod;
    private Label txtStatus;
    private Button btnStart;
    private Button btnStop;
    private Button btnPause;
    private Button btnResume;
    private GroupBox gbLine1;
    private GroupBox gbLine2;
    private Label lbl11;
    private Label lbl12;
    private Label lbl09;
    private TextBox txtStartParams;
    private Button btnApply;
    private Button btnCancel;
    private Button btnOk;
    private TreeView tvDependsOn;
    private TreeView tvDependsTo;
    private ImageList ilDependencies;
}
