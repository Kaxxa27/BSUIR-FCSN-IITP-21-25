namespace sMkTaskManager.Forms;

partial class frmService_Control {
    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }
    private void InitializeComponent() {
        components = new System.ComponentModel.Container();
        gb = new GroupBox();
        lbl3 = new Label();
        pb = new ProgressBar();
        lbl2 = new Label();
        lbl1 = new Label();
        Timer1 = new System.Windows.Forms.Timer(components);
        gb.SuspendLayout();
        SuspendLayout();
        // 
        // gb
        // 
        gb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        gb.Controls.Add(lbl3);
        gb.Controls.Add(pb);
        gb.Controls.Add(lbl2);
        gb.Controls.Add(lbl1);
        gb.Location = new Point(3, -5);
        gb.Margin = new Padding(0);
        gb.Name = "gb";
        gb.Padding = new Padding(0);
        gb.Size = new Size(377, 123);
        gb.TabIndex = 0;
        gb.TabStop = false;
        // 
        // lbl3
        // 
        lbl3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbl3.Location = new Point(7, 74);
        lbl3.Name = "lbl3";
        lbl3.Size = new Size(364, 41);
        lbl3.TabIndex = 3;
        lbl3.Text = "[Last Error]";
        lbl3.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // pb
        // 
        pb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pb.Location = new Point(7, 52);
        pb.Maximum = 10;
        pb.Name = "pb";
        pb.Size = new Size(364, 18);
        pb.Style = ProgressBarStyle.Marquee;
        pb.TabIndex = 2;
        pb.Value = 10;
        // 
        // lbl2
        // 
        lbl2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbl2.Location = new Point(8, 31);
        lbl2.Name = "lbl2";
        lbl2.Size = new Size(362, 18);
        lbl2.TabIndex = 1;
        lbl2.Text = "[Service Name]";
        lbl2.TextAlign = ContentAlignment.TopCenter;
        // 
        // lbl1
        // 
        lbl1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbl1.Location = new Point(3, 13);
        lbl1.Name = "lbl1";
        lbl1.Size = new Size(371, 18);
        lbl1.TabIndex = 0;
        lbl1.Text = "Task Manager is attempting to [action] the following service...";
        lbl1.TextAlign = ContentAlignment.TopCenter;
        // 
        // Timer1
        // 
        Timer1.Enabled = true;
        Timer1.Interval = 500;
        Timer1.Tick += Timer1_Tick;
        // 
        // frmService_Control
        // 
        ClientSize = new Size(384, 121);
        Controls.Add(gb);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        KeyPreview = true;
        Name = "frmService_Control";
        ShowIcon = false;
        ShowInTaskbar = false;
        Text = "Service Control";
        TopMost = true;
        Load += frmLoad;
        FormClosing += frmFormClosing;
        KeyDown += frmKeyDown;
        gb.ResumeLayout(false);
        ResumeLayout(false);
    }

    private GroupBox gb;
    private Label lbl2;
    private Label lbl1;
    private Label lbl3;
    private System.Windows.Forms.Timer Timer1;
    private ProgressBar pb;

}
