using System.ComponentModel;
using System.Runtime.Versioning;
namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
public partial class frmUser_Message : Form {

    public frmUser_Message() {
        InitializeComponent();
    }

    private void frmUser_Message_Load(object sender, EventArgs e) {
        txtTitle.Focus();
        txtIcon.SelectedIndex = 0;
        btnOk.Enabled = false;
    }
    private void txtTitle_TextChanged(object sender, EventArgs e) {
        btnOk.Enabled = !string.IsNullOrEmpty(txtTitle.Text.Trim()) && !string.IsNullOrEmpty(txtMessage.Text.Trim());
    }
    private void txtMessage_TextChanged(object sender, EventArgs e) {
        btnOk.Enabled = !string.IsNullOrEmpty(txtTitle.Text.Trim()) && !string.IsNullOrEmpty(txtMessage.Text.Trim());
    }

}
