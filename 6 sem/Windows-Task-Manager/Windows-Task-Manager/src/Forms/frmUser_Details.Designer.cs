namespace sMkTaskManager.Forms;
partial class frmUser_Details {
    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }

    private Button btnOk;
    private Button btnRefresh;
    private PictureBox pb;
    private Label txtUsername;
    private Label txtSessionID;
    private Label txtState;
    private Label lbl01;
    private Label lbl02;
    private Label lbl03;
    private GroupBox gb1;
    private Label txtClientName;
    private Label txtClientAddress;
    private Label txtClientDisplay;
    private Label lbl04;
    private Label lbl05;
    private Label lbl06;
    private Label txtLogonTime;
    private Label txtConnectTime;
    private Label txtDisconnectTime;
    private Label lbl07;
    private Label lbl08;
    private Label lbl09;
    private GroupBox gb2;
    private Label txtLastInputTime;
    private Label lbl10;
    private Label txtLastInputAgo;
    private Label txtLogonAgo;
    private Label txtConnectAgo;
    private Label txtDisconnectAgo;

    private void InitializeComponent() {
        btnOk = new Button();
        btnRefresh = new Button();
        pb = new PictureBox();
        txtUsername = new Label();
        txtSessionID = new Label();
        txtState = new Label();
        lbl01 = new Label();
        lbl02 = new Label();
        lbl03 = new Label();
        gb1 = new GroupBox();
        txtClientName = new Label();
        txtClientAddress = new Label();
        txtClientDisplay = new Label();
        lbl04 = new Label();
        lbl05 = new Label();
        lbl06 = new Label();
        txtLogonTime = new Label();
        txtConnectTime = new Label();
        txtDisconnectTime = new Label();
        lbl07 = new Label();
        lbl08 = new Label();
        lbl09 = new Label();
        gb2 = new GroupBox();
        txtLastInputTime = new Label();
        lbl10 = new Label();
        txtLastInputAgo = new Label();
        txtLogonAgo = new Label();
        txtConnectAgo = new Label();
        txtDisconnectAgo = new Label();
        ((System.ComponentModel.ISupportInitialize)pb).BeginInit();
        SuspendLayout();
        // 
        // btnOk
        // 
        btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnOk.DialogResult = DialogResult.OK;
        btnOk.Location = new Point(350, 189);
        btnOk.Margin = new Padding(4, 3, 4, 3);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(88, 30);
        btnOk.TabIndex = 27;
        btnOk.Text = "OK";
        btnOk.UseVisualStyleBackColor = true;
        btnOk.Click += btnOk_Click;
        // 
        // btnRefresh
        // 
        btnRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnRefresh.Location = new Point(256, 189);
        btnRefresh.Margin = new Padding(4, 3, 4, 3);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(88, 30);
        btnRefresh.TabIndex = 26;
        btnRefresh.Text = "Refresh";
        btnRefresh.UseVisualStyleBackColor = true;
        btnRefresh.Click += btnRefresh_Click;
        // 
        // pb
        // 
        pb.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        pb.Image = Resources.Resources.pbUserDetails;
        pb.Location = new Point(386, 6);
        pb.Margin = new Padding(4, 3, 4, 3);
        pb.Name = "pb";
        pb.Size = new Size(48, 48);
        pb.SizeMode = PictureBoxSizeMode.AutoSize;
        pb.TabIndex = 29;
        pb.TabStop = false;
        // 
        // txtUsername
        // 
        txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtUsername.Location = new Point(105, 6);
        txtUsername.Margin = new Padding(4, 0, 4, 0);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(273, 20);
        txtUsername.TabIndex = 1;
        txtUsername.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtSessionID
        // 
        txtSessionID.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtSessionID.Location = new Point(105, 22);
        txtSessionID.Margin = new Padding(4, 0, 4, 0);
        txtSessionID.Name = "txtSessionID";
        txtSessionID.Size = new Size(273, 20);
        txtSessionID.TabIndex = 3;
        txtSessionID.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtState
        // 
        txtState.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtState.ForeColor = Color.Maroon;
        txtState.Location = new Point(105, 38);
        txtState.Margin = new Padding(4, 0, 4, 0);
        txtState.Name = "txtState";
        txtState.Size = new Size(273, 20);
        txtState.TabIndex = 5;
        txtState.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lbl01
        // 
        lbl01.Location = new Point(5, 6);
        lbl01.Margin = new Padding(4, 0, 4, 0);
        lbl01.Name = "lbl01";
        lbl01.Size = new Size(82, 20);
        lbl01.TabIndex = 0;
        lbl01.Text = "User name:";
        lbl01.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lbl02
        // 
        lbl02.Location = new Point(5, 22);
        lbl02.Margin = new Padding(4, 0, 4, 0);
        lbl02.Name = "lbl02";
        lbl02.Size = new Size(82, 20);
        lbl02.TabIndex = 2;
        lbl02.Text = "Session ID:";
        lbl02.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lbl03
        // 
        lbl03.Location = new Point(5, 38);
        lbl03.Margin = new Padding(4, 0, 4, 0);
        lbl03.Name = "lbl03";
        lbl03.Size = new Size(82, 20);
        lbl03.TabIndex = 4;
        lbl03.Text = "State:";
        lbl03.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // gb1
        // 
        gb1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gb1.Location = new Point(5, 56);
        gb1.Margin = new Padding(4, 3, 4, 3);
        gb1.Name = "gb1";
        gb1.Padding = new Padding(4, 3, 4, 3);
        gb1.Size = new Size(431, 5);
        gb1.TabIndex = 6;
        gb1.TabStop = false;
        // 
        // txtClientName
        // 
        txtClientName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtClientName.Location = new Point(105, 64);
        txtClientName.Margin = new Padding(4, 0, 4, 0);
        txtClientName.Name = "txtClientName";
        txtClientName.Size = new Size(320, 20);
        txtClientName.TabIndex = 8;
        txtClientName.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtClientAddress
        // 
        txtClientAddress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtClientAddress.Location = new Point(105, 80);
        txtClientAddress.Margin = new Padding(4, 0, 4, 0);
        txtClientAddress.Name = "txtClientAddress";
        txtClientAddress.Size = new Size(320, 20);
        txtClientAddress.TabIndex = 10;
        txtClientAddress.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtClientDisplay
        // 
        txtClientDisplay.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtClientDisplay.Location = new Point(105, 96);
        txtClientDisplay.Margin = new Padding(4, 0, 4, 0);
        txtClientDisplay.Name = "txtClientDisplay";
        txtClientDisplay.Size = new Size(320, 20);
        txtClientDisplay.TabIndex = 12;
        txtClientDisplay.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lbl04
        // 
        lbl04.Location = new Point(5, 64);
        lbl04.Margin = new Padding(4, 0, 4, 0);
        lbl04.Name = "lbl04";
        lbl04.Size = new Size(99, 20);
        lbl04.TabIndex = 7;
        lbl04.Text = "Client name:";
        lbl04.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lbl05
        // 
        lbl05.Location = new Point(5, 80);
        lbl05.Margin = new Padding(4, 0, 4, 0);
        lbl05.Name = "lbl05";
        lbl05.Size = new Size(99, 20);
        lbl05.TabIndex = 9;
        lbl05.Text = "Client address:";
        lbl05.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lbl06
        // 
        lbl06.Location = new Point(5, 96);
        lbl06.Margin = new Padding(4, 0, 4, 0);
        lbl06.Name = "lbl06";
        lbl06.Size = new Size(99, 20);
        lbl06.TabIndex = 11;
        lbl06.Text = "Client display:";
        lbl06.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtLogonTime
        // 
        txtLogonTime.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtLogonTime.Location = new Point(105, 122);
        txtLogonTime.Margin = new Padding(4, 0, 4, 0);
        txtLogonTime.Name = "txtLogonTime";
        txtLogonTime.Size = new Size(171, 20);
        txtLogonTime.TabIndex = 15;
        txtLogonTime.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtConnectTime
        // 
        txtConnectTime.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtConnectTime.Location = new Point(105, 138);
        txtConnectTime.Margin = new Padding(4, 0, 4, 0);
        txtConnectTime.Name = "txtConnectTime";
        txtConnectTime.Size = new Size(171, 20);
        txtConnectTime.TabIndex = 18;
        txtConnectTime.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtDisconnectTime
        // 
        txtDisconnectTime.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtDisconnectTime.Location = new Point(105, 154);
        txtDisconnectTime.Margin = new Padding(4, 0, 4, 0);
        txtDisconnectTime.Name = "txtDisconnectTime";
        txtDisconnectTime.Size = new Size(171, 20);
        txtDisconnectTime.TabIndex = 21;
        txtDisconnectTime.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lbl07
        // 
        lbl07.Location = new Point(5, 122);
        lbl07.Margin = new Padding(4, 0, 4, 0);
        lbl07.Name = "lbl07";
        lbl07.Size = new Size(99, 20);
        lbl07.TabIndex = 14;
        lbl07.Text = "Logon time:";
        lbl07.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lbl08
        // 
        lbl08.Location = new Point(5, 138);
        lbl08.Margin = new Padding(4, 0, 4, 0);
        lbl08.Name = "lbl08";
        lbl08.Size = new Size(99, 20);
        lbl08.TabIndex = 17;
        lbl08.Text = "Connect time:";
        lbl08.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lbl09
        // 
        lbl09.Location = new Point(5, 154);
        lbl09.Margin = new Padding(4, 0, 4, 0);
        lbl09.Name = "lbl09";
        lbl09.Size = new Size(117, 20);
        lbl09.TabIndex = 20;
        lbl09.Text = "Disconnect time:";
        lbl09.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // gb2
        // 
        gb2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gb2.Location = new Point(5, 114);
        gb2.Margin = new Padding(4, 3, 4, 3);
        gb2.Name = "gb2";
        gb2.Padding = new Padding(4, 3, 4, 3);
        gb2.Size = new Size(431, 5);
        gb2.TabIndex = 13;
        gb2.TabStop = false;
        // 
        // txtLastInputTime
        // 
        txtLastInputTime.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtLastInputTime.Location = new Point(105, 170);
        txtLastInputTime.Margin = new Padding(4, 0, 4, 0);
        txtLastInputTime.Name = "txtLastInputTime";
        txtLastInputTime.Size = new Size(171, 20);
        txtLastInputTime.TabIndex = 24;
        txtLastInputTime.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lbl10
        // 
        lbl10.Location = new Point(5, 170);
        lbl10.Margin = new Padding(4, 0, 4, 0);
        lbl10.Name = "lbl10";
        lbl10.Size = new Size(117, 20);
        lbl10.TabIndex = 23;
        lbl10.Text = "Last input time:";
        lbl10.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtLastInputAgo
        // 
        txtLastInputAgo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        txtLastInputAgo.Location = new Point(279, 170);
        txtLastInputAgo.Margin = new Padding(4, 0, 4, 0);
        txtLastInputAgo.Name = "txtLastInputAgo";
        txtLastInputAgo.Size = new Size(150, 20);
        txtLastInputAgo.TabIndex = 25;
        txtLastInputAgo.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtLogonAgo
        // 
        txtLogonAgo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        txtLogonAgo.Location = new Point(279, 122);
        txtLogonAgo.Margin = new Padding(4, 0, 4, 0);
        txtLogonAgo.Name = "txtLogonAgo";
        txtLogonAgo.Size = new Size(150, 20);
        txtLogonAgo.TabIndex = 16;
        txtLogonAgo.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtConnectAgo
        // 
        txtConnectAgo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        txtConnectAgo.Location = new Point(279, 138);
        txtConnectAgo.Margin = new Padding(4, 0, 4, 0);
        txtConnectAgo.Name = "txtConnectAgo";
        txtConnectAgo.Size = new Size(150, 20);
        txtConnectAgo.TabIndex = 19;
        txtConnectAgo.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtDisconnectAgo
        // 
        txtDisconnectAgo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        txtDisconnectAgo.Location = new Point(279, 154);
        txtDisconnectAgo.Margin = new Padding(4, 0, 4, 0);
        txtDisconnectAgo.Name = "txtDisconnectAgo";
        txtDisconnectAgo.Size = new Size(150, 20);
        txtDisconnectAgo.TabIndex = 22;
        txtDisconnectAgo.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // frmUser_Details
        // 
        AcceptButton = btnOk;
        CancelButton = btnOk;
        ClientSize = new Size(444, 221);
        Controls.Add(txtClientName);
        Controls.Add(txtClientAddress);
        Controls.Add(txtClientDisplay);
        Controls.Add(txtUsername);
        Controls.Add(txtSessionID);
        Controls.Add(txtState);
        Controls.Add(txtLastInputTime);
        Controls.Add(txtLogonTime);
        Controls.Add(txtConnectTime);
        Controls.Add(txtDisconnectTime);
        Controls.Add(txtLastInputAgo);
        Controls.Add(txtLogonAgo);
        Controls.Add(txtConnectAgo);
        Controls.Add(txtDisconnectAgo);
        Controls.Add(lbl10);
        Controls.Add(lbl07);
        Controls.Add(lbl08);
        Controls.Add(lbl09);
        Controls.Add(gb2);
        Controls.Add(lbl04);
        Controls.Add(lbl05);
        Controls.Add(lbl06);
        Controls.Add(gb1);
        Controls.Add(lbl01);
        Controls.Add(lbl02);
        Controls.Add(lbl03);
        Controls.Add(pb);
        Controls.Add(btnRefresh);
        Controls.Add(btnOk);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Icon = Resources.Resources.frmUser_Details;
        Margin = new Padding(4, 3, 4, 3);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "frmUser_Details";
        ShowIcon = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "User Session Details...";
        TopMost = true;
        Load += frmUser_Details_Load;
        ((System.ComponentModel.ISupportInitialize)pb).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
