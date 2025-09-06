namespace sMkTaskManager.Forms;
partial class frmUser_Message {
    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }

    private void InitializeComponent() {
        PictureBox1 = new PictureBox();
        Label2 = new Label();
        Label3 = new Label();
        txtMessage = new TextBox();
        txtTitle = new TextBox();
        btnCancel = new Button();
        btnOk = new Button();
        Label1 = new Label();
        txtIcon = new ComboBox();
        ((System.ComponentModel.ISupportInitialize)PictureBox1).BeginInit();
        SuspendLayout();
        // 
        // PictureBox1
        // 
        PictureBox1.Dock = DockStyle.Top;
        PictureBox1.Image = Resources.Resources.frmUser_BannerMessage;
        PictureBox1.Location = new Point(0, 0);
        PictureBox1.Name = "PictureBox1";
        PictureBox1.Size = new Size(314, 60);
        PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        PictureBox1.TabIndex = 1;
        PictureBox1.TabStop = false;
        // 
        // Label2
        // 
        Label2.AutoSize = true;
        Label2.Location = new Point(4, 71);
        Label2.Name = "Label2";
        Label2.Size = new Size(32, 15);
        Label2.TabIndex = 0;
        Label2.Text = "Title:";
        // 
        // Label3
        // 
        Label3.AutoSize = true;
        Label3.Location = new Point(4, 97);
        Label3.Name = "Label3";
        Label3.Size = new Size(56, 15);
        Label3.TabIndex = 2;
        Label3.Text = "Message:";
        // 
        // txtMessage
        // 
        txtMessage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtMessage.Location = new Point(67, 94);
        txtMessage.MaxLength = 500;
        txtMessage.Multiline = true;
        txtMessage.Name = "txtMessage";
        txtMessage.Size = new Size(239, 85);
        txtMessage.TabIndex = 3;
        txtMessage.TextChanged += txtMessage_TextChanged;
        // 
        // txtTitle
        // 
        txtTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtTitle.Location = new Point(67, 67);
        txtTitle.MaxLength = 250;
        txtTitle.Name = "txtTitle";
        txtTitle.Size = new Size(239, 23);
        txtTitle.TabIndex = 1;
        txtTitle.WordWrap = false;
        txtTitle.TextChanged += txtTitle_TextChanged;
        // 
        // btnCancel
        // 
        btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(231, 220);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(75, 23);
        btnCancel.TabIndex = 7;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        // 
        // btnOk
        // 
        btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnOk.DialogResult = DialogResult.OK;
        btnOk.Location = new Point(150, 220);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(75, 23);
        btnOk.TabIndex = 6;
        btnOk.Text = "OK";
        btnOk.UseVisualStyleBackColor = true;
        // 
        // Label1
        // 
        Label1.AutoSize = true;
        Label1.Location = new Point(4, 188);
        Label1.Name = "Label1";
        Label1.Size = new Size(33, 15);
        Label1.TabIndex = 4;
        Label1.Text = "Icon:";
        // 
        // txtIcon
        // 
        txtIcon.DropDownStyle = ComboBoxStyle.DropDownList;
        txtIcon.FormattingEnabled = true;
        txtIcon.Items.AddRange(new object[] { "None", "Error", "Information", "Question", "Warning" });
        txtIcon.Location = new Point(67, 183);
        txtIcon.Name = "txtIcon";
        txtIcon.Size = new Size(239, 23);
        txtIcon.TabIndex = 5;
        // 
        // frmUser_Message
        // 
        AcceptButton = btnOk;
        CancelButton = btnCancel;
        ClientSize = new Size(314, 251);
        Controls.Add(txtIcon);
        Controls.Add(btnOk);
        Controls.Add(btnCancel);
        Controls.Add(txtTitle);
        Controls.Add(txtMessage);
        Controls.Add(PictureBox1);
        Controls.Add(Label1);
        Controls.Add(Label3);
        Controls.Add(Label2);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Icon = Resources.Resources.frmUser_Message;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "frmUser_Message";
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Send Message";
        Load += frmUser_Message_Load;
        ((System.ComponentModel.ISupportInitialize)PictureBox1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private PictureBox PictureBox1;
    private Label Label2;
    private Label Label3;
    private Button btnCancel;
    private Button btnOk;
    private Label Label1;
    internal TextBox txtMessage;
    internal TextBox txtTitle;
    internal ComboBox txtIcon;

}
