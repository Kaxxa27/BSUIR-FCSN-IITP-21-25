using System.ComponentModel;
using System.Runtime.Versioning;

namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
public partial class frmProcess_Affinity : Form {
    public long BitMask = 0;
    private bool m_Ignoring = false;

    public frmProcess_Affinity() {
        InitializeComponent();

        for (int i = 1; i < Environment.ProcessorCount; i++) {
            CheckBox c = new() {
                AutoSize = true,
                Margin = chkCPU0.Margin,
                Text = "CPU " + (i+1).ToString(),
                Tag = i,
            };
            flowPanel.Controls.Add(c);
            if (c.Enabled) c.CheckedChanged += chkCPU_CheckedChanged;
        }
    }

    private void frmProcess_Affinity_Load(object sender, EventArgs e) {
        chkCPU_CheckedChanged(null, EventArgs.Empty);
        foreach (CheckBox c in flowPanel.Controls) {
            c.Checked = Shared.BitExamine(BitMask, Convert.ToInt64(c.Tag) + 1);
        }

    }
    private void btnOk_Click(object sender, EventArgs e) {
        DialogResult = DialogResult.OK;
        BitMask = 0;
        foreach (CheckBox c in flowPanel.Controls) {
            if (c.Enabled && c.Checked) {
                Shared.BitSet(ref BitMask, Convert.ToInt32(c.Tag) + 1);
            }
        }


    }
    private void btnCancel_Click(object sender, EventArgs e) {
        DialogResult = DialogResult.Cancel;
        Close();
    }
    private void chkAll_CheckedChanged(object sender, EventArgs e) {
        if (m_Ignoring) return;
        m_Ignoring = true;
        foreach (CheckBox c in flowPanel.Controls) {
            if (c.Enabled) c.Checked = chkAll.Checked;
        }
        m_Ignoring = false;
    }
    private void chkCPU_CheckedChanged(object? sender, EventArgs e) {
        // Check if All processors have been checked or if there is at least one
        bool all = true;
        bool hasone = false;
        foreach (CheckBox c in flowPanel.Controls) {
            if (c.Enabled && !c.Checked) { all = false; }
            if (c.Enabled && c.Checked) { hasone = true; }
        }
        btnOk.Enabled = hasone;
        if (m_Ignoring) return;
        m_Ignoring = true;
        if (all && !chkAll.Checked) { chkAll.Checked = true; }
        if (!all && chkAll.Checked) { chkAll.Checked = false; }
        m_Ignoring = false;
    }

}
