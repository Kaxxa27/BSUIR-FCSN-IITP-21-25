namespace sMkTaskManager.Forms;

partial class frmProcess_Affinity {

    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }

    private void InitializeComponent() {
        btnOk = new Button();
        btnCancel = new Button();
        chkAll = new CheckBox();
        Label1 = new Label();
        flowPanel = new FlowLayoutPanel();
        chkCPU0 = new CheckBox();
        flowPanel.SuspendLayout();
        SuspendLayout();
        // 
        // btnOk
        // 
        btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnOk.Location = new Point(352, 157);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(75, 23);
        btnOk.TabIndex = 3;
        btnOk.Text = "OK";
        btnOk.UseVisualStyleBackColor = true;
        btnOk.Click += btnOk_Click;
        // 
        // btnCancel
        // 
        btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(433, 158);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(75, 23);
        btnCancel.TabIndex = 4;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;
        // 
        // chkAll
        // 
        chkAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        chkAll.AutoSize = true;
        chkAll.Location = new Point(7, 163);
        chkAll.Name = "chkAll";
        chkAll.Size = new Size(99, 19);
        chkAll.TabIndex = 2;
        chkAll.Text = "All Processors";
        chkAll.UseVisualStyleBackColor = true;
        chkAll.CheckedChanged += chkAll_CheckedChanged;
        // 
        // Label1
        // 
        Label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        Label1.Location = new Point(4, 7);
        Label1.Name = "Label1";
        Label1.Size = new Size(500, 17);
        Label1.TabIndex = 0;
        Label1.Text = "The Processor Affinity setting controls which CPUs the process will be allowed to execute on.";
        // 
        // flowPanel
        // 
        flowPanel.Controls.Add(chkCPU0);
        flowPanel.FlowDirection = FlowDirection.TopDown;
        flowPanel.Location = new Point(8, 29);
        flowPanel.Name = "flowPanel";
        flowPanel.Size = new Size(500, 122);
        flowPanel.TabIndex = 1;
        // 
        // chkCPU0
        // 
        chkCPU0.AutoSize = true;
        chkCPU0.Location = new Point(3, 2);
        chkCPU0.Margin = new Padding(3, 2, 20, 0);
        chkCPU0.Name = "chkCPU0";
        chkCPU0.Size = new Size(58, 19);
        chkCPU0.TabIndex = 1;
        chkCPU0.Tag = "0";
        chkCPU0.Text = "CPU 1";
        chkCPU0.UseVisualStyleBackColor = true;
        chkCPU0.CheckedChanged += chkCPU_CheckedChanged;
        // 
        // frmProcess_Affinity
        // 
        AcceptButton = btnOk;
        CancelButton = btnCancel;
        ClientSize = new Size(516, 187);
        Controls.Add(flowPanel);
        Controls.Add(Label1);
        Controls.Add(chkAll);
        Controls.Add(btnCancel);
        Controls.Add(btnOk);
        Icon = Resources.Resources.frmProcess_Affinity;
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "frmProcess_Affinity";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Processor Affinity";
        Load += frmProcess_Affinity_Load;
        flowPanel.ResumeLayout(false);
        flowPanel.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private Button btnOk;
    private Button btnCancel;
    private CheckBox chkAll;
    private Label Label1;
    private FlowLayoutPanel flowPanel;
    private CheckBox chkCPU0;
}
