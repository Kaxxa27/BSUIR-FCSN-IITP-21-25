namespace sMkTaskManager.Forms;
partial class frmUser_Connect {
    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }

    private void InitializeComponent() {
        lbl01 = new Label();
        lbl02 = new Label();
        lbl03 = new Label();
        pbBanner = new PictureBox();
        txtPassword = new TextBox();
        txtUsername = new TextBox();
        btnCancel = new Button();
        btnOk = new Button();
        ((System.ComponentModel.ISupportInitialize)pbBanner).BeginInit();
        SuspendLayout();
        // 
        // lbl01
        // 
        lbl01.AutoSize = true;
        lbl01.Location = new Point(6, 69);
        lbl01.Name = "lbl01";
        lbl01.Size = new Size(302, 15);
        lbl01.TabIndex = 0;
        lbl01.Text = "Enter the selected user's password in order to connect ...";
        // 
        // lbl02
        // 
        lbl02.AutoSize = true;
        lbl02.Location = new Point(6, 98);
        lbl02.Name = "lbl02";
        lbl02.Size = new Size(63, 15);
        lbl02.TabIndex = 1;
        lbl02.Text = "Username:";
        // 
        // lbl03
        // 
        lbl03.AutoSize = true;
        lbl03.Location = new Point(6, 125);
        lbl03.Name = "lbl03";
        lbl03.Size = new Size(60, 15);
        lbl03.TabIndex = 3;
        lbl03.Text = "Password:";
        // 
        // pbBanner
        // 
        pbBanner.Dock = DockStyle.Top;
        pbBanner.Image = Resources.Resources.frmUser_BannerConnect;
        pbBanner.Location = new Point(0, 0);
        pbBanner.Name = "pbBanner";
        pbBanner.Size = new Size(314, 60);
        pbBanner.SizeMode = PictureBoxSizeMode.AutoSize;
        pbBanner.TabIndex = 1;
        pbBanner.TabStop = false;
        // 
        // txtPassword
        // 
        txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtPassword.Location = new Point(75, 121);
        txtPassword.MaxLength = 250;
        txtPassword.Name = "txtPassword";
        txtPassword.PasswordChar = '*';
        txtPassword.Size = new Size(231, 23);
        txtPassword.TabIndex = 4;
        txtPassword.WordWrap = false;
        // 
        // txtUsername
        // 
        txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtUsername.Location = new Point(75, 94);
        txtUsername.MaxLength = 250;
        txtUsername.Name = "txtUsername";
        txtUsername.ReadOnly = true;
        txtUsername.Size = new Size(231, 23);
        txtUsername.TabIndex = 2;
        txtUsername.WordWrap = false;
        // 
        // btnCancel
        // 
        btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(231, 154);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(75, 23);
        btnCancel.TabIndex = 6;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        // 
        // btnOk
        // 
        btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnOk.DialogResult = DialogResult.OK;
        btnOk.Location = new Point(150, 154);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(75, 23);
        btnOk.TabIndex = 5;
        btnOk.Text = "OK";
        btnOk.UseVisualStyleBackColor = true;
        // 
        // frmUser_Connect
        // 
        AcceptButton = btnOk;
        CancelButton = btnCancel;
        ClientSize = new Size(314, 185);
        Controls.Add(btnOk);
        Controls.Add(btnCancel);
        Controls.Add(txtUsername);
        Controls.Add(txtPassword);
        Controls.Add(pbBanner);
        Controls.Add(lbl03);
        Controls.Add(lbl02);
        Controls.Add(lbl01);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Icon = Resources.Resources.frmUser_Connect;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "frmUser_Connect";
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Connect Password";
        ((System.ComponentModel.ISupportInitialize)pbBanner).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lbl01;
    private Label lbl02;
    private Label lbl03;
    private PictureBox pbBanner;
    private Button btnCancel;
    private Button btnOk;
    internal TextBox txtPassword;
    internal TextBox txtUsername;

}
