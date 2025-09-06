namespace sMkTaskManager.Forms;

partial class frmAbout {
    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }

    private void InitializeComponent() {
       // pbBanner = new PictureBox();
        //lblBuild = new Label();
        lblCopyright = new Label();
        btnOk = new Button();
        //lblLicense = new Label();
        lblProduct = new Label();
        //lblDivider = new Label();
        lnkGithub = new LinkLabel();
        //lnkLicense = new LinkLabel();
        //lnkDonate = new LinkLabel();
        //lnkFeedback = new LinkLabel();
       // ((System.ComponentModel.ISupportInitialize)pbBanner).BeginInit();
        SuspendLayout();
        // 
        // pbBanner
        // 
        //pbBanner.BackColor = SystemColors.ControlDarkDark;
        //pbBanner.Dock = DockStyle.Top;
        //pbBanner.Location = new Point(0, 0);
        //pbBanner.Name = "pbBanner";
        //pbBanner.Size = new Size(434, 75);
        //pbBanner.SizeMode = PictureBoxSizeMode.AutoSize;
        //pbBanner.TabIndex = 0;
        //pbBanner.TabStop = false;
        // 
        // lblBuild
        // 
        //lblBuild.BackColor = Color.Transparent;
        //lblBuild.Location = new Point(12, 102);
        //lblBuild.Name = "lblBuild";
        //lblBuild.Size = new Size(300, 15);
        //lblBuild.TabIndex = 1;
        //lblBuild.Text = "Revision: xxxx1 - Date: xxxx2";
        //lblBuild.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblCopyright
        // 
        lblCopyright.BackColor = Color.Transparent;
        lblCopyright.Location = new Point(12, 0);
        lblCopyright.Name = "lblCopyright";
        lblCopyright.Size = new Size(300, 30);
        lblCopyright.TabIndex = 2;
        lblCopyright.Text = "Copyright © 2024 by Yauheni Kakhnouski";
        lblCopyright.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // btnOk
        // 
        btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnOk.Location = new Point(250, 70);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(75, 30);
        btnOk.TabIndex = 9;
        btnOk.Text = "OK";
        btnOk.UseVisualStyleBackColor = true;
        // 
        // lblLicense
        // 
        //lblLicense.Anchor = AnchorStyles.Top;
        //lblLicense.Location = new Point(12, 147);
        //lblLicense.Name = "lblLicense";
        //lblLicense.Size = new Size(410, 50);
        //lblLicense.TabIndex = 4;
        //lblLicense.Text = "This program comes with absolutely no warranty.\r\nDistributed under the GNU General Public License v3 or later.";
        //lblLicense.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblProduct
        // 
        lblProduct.BackColor = Color.Transparent;
        lblProduct.Location = new Point(12, 35);
        lblProduct.Name = "lblProduct";
        lblProduct.Size = new Size(300, 30);
        lblProduct.TabIndex = 0;
        lblProduct.Text = "Task Manager by Yauheni Kakhnouski";
        lblProduct.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblDivider
        // 
        //lblDivider.BorderStyle = BorderStyle.Fixed3D;
        //lblDivider.Location = new Point(12, 144);
        //lblDivider.Name = "lblDivider";
        //lblDivider.Size = new Size(410, 2);
        //lblDivider.TabIndex = 3;
        //lblDivider.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lnkGithub
        // 
        lnkGithub.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lnkGithub.LinkBehavior = LinkBehavior.HoverUnderline;
        lnkGithub.Location = new Point(5, 70);
        lnkGithub.Name = "lnkGithub";
        lnkGithub.Size = new Size(70, 25);
        lnkGithub.TabIndex = 5;
        lnkGithub.TabStop = true;
        lnkGithub.Text = "Github";
        lnkGithub.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lnkLicense
        // 
        //lnkLicense.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        //lnkLicense.LinkBehavior = LinkBehavior.HoverUnderline;
        //lnkLicense.Location = new Point(118, 212);
        //lnkLicense.Name = "lnkLicense";
        //lnkLicense.Size = new Size(50, 18);
        //lnkLicense.TabIndex = 7;
        //lnkLicense.TabStop = true;
        //lnkLicense.Text = "License";
        //lnkLicense.TextAlign = ContentAlignment.MiddleCenter;
        //// 
        //// lnkDonate
        //// 
        //lnkDonate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        //lnkDonate.LinkBehavior = LinkBehavior.HoverUnderline;
        //lnkDonate.Location = new Point(168, 212);
        //lnkDonate.Name = "lnkDonate";
        //lnkDonate.Size = new Size(50, 18);
        //lnkDonate.TabIndex = 8;
        //lnkDonate.TabStop = true;
        //lnkDonate.Text = "Donate";
        //lnkDonate.TextAlign = ContentAlignment.MiddleCenter;
        //// 
        //// lnkFeedback
        //// 
        //lnkFeedback.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        //lnkFeedback.LinkBehavior = LinkBehavior.HoverUnderline;
        //lnkFeedback.Location = new Point(58, 212);
        //lnkFeedback.Name = "lnkFeedback";
        //lnkFeedback.Size = new Size(60, 18);
        //lnkFeedback.TabIndex = 6;
        //lnkFeedback.TabStop = true;
        //lnkFeedback.Text = "Feedback";
        //lnkFeedback.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // frmAbout
        // 
        //Controls.Add(lnkLicense);
        //Controls.Add(lnkFeedback);
        //Controls.Add(lnkDonate);
        Controls.Add(lnkGithub);
        //Controls.Add(lblDivider);
        Controls.Add(lblProduct);
        //Controls.Add(lblLicense);
        Controls.Add(btnOk);
        //Controls.Add(lblBuild);
        Controls.Add(lblCopyright);
        //Controls.Add(pbBanner);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        KeyPreview = true;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "frmAbout";
        ShowIcon = false;
        SizeGripStyle = SizeGripStyle.Hide;
        StartPosition = FormStartPosition.CenterParent;
        Size = new Size(350, 150);
        Text = "About";
       // ((System.ComponentModel.ISupportInitialize)pbBanner).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    //private PictureBox pbBanner;
    //private Label lblBuild;
    private Label lblCopyright;
    private Button btnOk;
   // private Label lblLicense;
    private Label lblProduct;
  //  private Label lblDivider;
    private LinkLabel lnkGithub;
    //private LinkLabel lnkLicense;
    //private LinkLabel lnkDonate;
    //private LinkLabel lnkFeedback;
}
