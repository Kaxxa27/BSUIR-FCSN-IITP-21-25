using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Versioning;
namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
public partial class frmProcess_Files : Form {
    private Process? p = null;

    public frmProcess_Files() {
        InitializeComponent();
        Load += OnLoad;
        KeyPress += OnKeyPress;
    }

    private void OnLoad(object? sender, EventArgs e) {
        LoadProcessFiles();
    }
    private void OnKeyPress(object? sender, KeyPressEventArgs e) {
        if (e.KeyChar == Convert.ToChar(Keys.Escape)) { Close(); }
    }
    private void btnClose_Click(object? sender, EventArgs e) {
        Close();
    }

    public int PID {
        get { return (p == null) ? 0 : p.Id; }
        set { p = Process.GetProcessById(value); if (Visible) { LoadProcessFiles(); } }
    }
    private void LoadProcessFiles() { }
}
