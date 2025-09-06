namespace sMkTaskManager.Forms;
partial class frmColumns {

    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }

    private void InitializeComponent() {
        lblHelp = new Label();
        lv = new ListView();
        columnHeader1 = new ColumnHeader();
        btnDefaults = new Button();
        btnCancel = new Button();
        btnOk = new Button();
        SuspendLayout();
        // 
        // lblHelp
        // 
        lblHelp.Location = new Point(3, 7);
        lblHelp.Margin = new Padding(0);
        lblHelp.Name = "lblHelp";
        lblHelp.Size = new Size(310, 32);
        lblHelp.TabIndex = 0;
        lblHelp.Text = "Check the columns that you would like to make visible.\r\nYou can reorder and resize them on the ListView itself.";
        // 
        // lv
        // 
        lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lv.BorderStyle = BorderStyle.FixedSingle;
        lv.CheckBoxes = true;
        lv.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
        lv.FullRowSelect = true;
        lv.HeaderStyle = ColumnHeaderStyle.None;
        lv.LabelWrap = false;
        lv.Location = new Point(6, 44);
        lv.Name = "lv";
        lv.Size = new Size(293, 312);
        lv.TabIndex = 1;
        lv.UseCompatibleStateImageBehavior = false;
        lv.View = View.Details;
        lv.ItemCheck += lv_ItemCheck;
        // 
        // columnHeader1
        // 
        columnHeader1.Width = 220;
        // 
        // btnDefaults
        // 
        btnDefaults.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnDefaults.Location = new Point(6, 362);
        btnDefaults.Margin = new Padding(3, 3, 3, 0);
        btnDefaults.Name = "btnDefaults";
        btnDefaults.Size = new Size(70, 30);
        btnDefaults.TabIndex = 6;
        btnDefaults.Text = "Defaults";
        btnDefaults.UseVisualStyleBackColor = true;
        btnDefaults.Click += btnDefaults_Click;
        // 
        // btnCancel
        // 
        btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new Point(229, 362);
        btnCancel.Margin = new Padding(3, 3, 3, 0);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(70, 30);
        btnCancel.TabIndex = 11;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;
        // 
        // btnOk
        // 
        btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnOk.Location = new Point(156, 362);
        btnOk.Margin = new Padding(3, 3, 3, 0);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(70, 30);
        btnOk.TabIndex = 10;
        btnOk.Text = "OK";
        btnOk.UseVisualStyleBackColor = true;
        btnOk.Click += btnOk_Click;
        // 
        // frmColumns
        // 
        AcceptButton = btnOk;
        CancelButton = btnCancel;
        ClientSize = new Size(304, 391);
        Controls.Add(lv);
        Controls.Add(btnDefaults);
        Controls.Add(btnOk);
        Controls.Add(btnCancel);
        Controls.Add(lblHelp);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "frmColumns";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Columns Settings";
        Load += frmColumns_Load;
        ResumeLayout(false);
    }

    private Label lblHelp;
    private ListView lv;
    private Button btnDefaults;
    private Button btnCancel;
    private Button btnOk;
    private ColumnHeader columnHeader1;
}
