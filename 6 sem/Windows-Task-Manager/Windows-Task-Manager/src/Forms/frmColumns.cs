using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.Versioning;
using sMkTaskManager.Classes;

namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
public partial class frmColumns : Form {
    public TaskManagerColumnTypes ListType { get; private set; }
    public ListView.ListViewItemCollection ColItems => lv.Items;

    public frmColumns() {
        InitializeComponent();
        ListType = TaskManagerColumnTypes.Process;
    }
    public frmColumns(TaskManagerColumnTypes Type) : this() {
        ListType = Type;
        LoadAllColumns();
    }

    private void frmColumns_Load(object sender, EventArgs e) {
        lv.Columns[0].Width = -2;
        lv.Columns[0].Width -= (SystemInformation.VerticalScrollBarWidth+2);
    }
    private void btnOk_Click(object sender, EventArgs e) {
        DialogResult = DialogResult.OK;
        Close();
    }
    private void btnCancel_Click(object sender, EventArgs e) {
        DialogResult = DialogResult.Cancel;
        Close();

    }
    private void btnDefaults_Click(object sender, EventArgs e) {
        lv.BeginUpdate();
        foreach (TaskManagerColumn c in TaskManagerColumn.GetColumnDefinition(ListType)) {
            if (lv.Items.ContainsKey(c.Tag)) lv.Items[c.Tag].Checked = c.Default;
        }
        lv.EndUpdate();
    }
    private void lv_ItemCheck(object sender, ItemCheckEventArgs e) {
        if (lv.Items[e.Index].ForeColor == Color.FromKnownColor(KnownColor.GrayText)) {
            lv.Items[e.Index].ForeColor = Color.FromKnownColor(KnownColor.GrayText);
            e.NewValue = CheckState.Checked;
        }
    }

    internal void LoadAllColumns() {
        lv.Items.Clear();
        foreach (TaskManagerColumn c in TaskManagerColumn.GetColumnDefinition(ListType)) {
            ListViewGroup? itmGroup = null;
            foreach (ListViewGroup g in lv.Groups) {
                if (g.Header == c.Group) { itmGroup = g; break; }
            }
            if (itmGroup == null) {
                itmGroup = new() { Name = c.Group, Header = c.Group };
                lv.Groups.Add(itmGroup);
            }
            ListViewItem itm = new() {
                Name = c.Tag,
                Tag = c.Tag,
                Group = itmGroup,
                Text = c.Label,
                ToolTipText = c.Title,
                ImageIndex = c.Width,
                IndentCount = (int)c.Align
            };
            if (c.Fixed) {
                itm.ForeColor = Color.FromKnownColor(KnownColor.GrayText);
                itm.Checked = true;
            }
            lv.Items.Add(itm);
        }
    }
    internal void LoadCheckedColumns(in ListView.ColumnHeaderCollection Columns) {
        foreach (ColumnHeader c in Columns) {
            if (lv.Items.ContainsKey(c.Tag!.ToString())) {
                lv.Items[c.Tag.ToString()].Checked = true;
                lv.Items[c.Tag.ToString()].ImageIndex = c.Width;
            }
        }
    }

}