using System.ComponentModel;
using System.Runtime.Versioning;
using sMkTaskManager.Classes;
namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
public partial class frmService_Control : Form {
    private string _Action;
    private readonly string _Service;
    private readonly int _ReturnDelay = 1000;
    private IntPtr hSManager = IntPtr.Zero;
    private IntPtr hService = IntPtr.Zero;
    private API.SERVICE_STATUS dwServiceStatus = new();

    public frmService_Control() {
        InitializeComponent();
        lbl3.Text = "";
        lbl3.Visible = false;
    }
    public frmService_Control(string serviceIdent) : this() {
        _Service = serviceIdent;
    }

    private void frmLoad(object? sender, EventArgs e) {
        Height = lbl3.Visible ? 160 : 116;
        Cursor = Cursors.AppStarting;
        hSManager = API.OpenSCManager(".", null, API.SERVICE_ACCESS.SERVICE_GENERIC_ALL);
        hService = API.OpenService(hSManager, _Service, API.SERVICE_ACCESS.SERVICE_QUERY_STATUS);
        Timer1.Start();
    }
    private void frmFormClosing(object? sender, FormClosingEventArgs e) {
        Timer1.Stop();
        if (hService != IntPtr.Zero) API.CloseServiceHandle(hService);
        if (hSManager != IntPtr.Zero) API.CloseServiceHandle(hSManager);
    }
    private void frmKeyDown(object? sender, KeyEventArgs e) {
        if (e.KeyCode == Keys.Escape) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
    private void Timer1_Tick(object? sender, EventArgs e) {
        if (Timer1.Interval == _ReturnDelay) {
            Close(); return;
        }
        if (hSManager != IntPtr.Zero && hService != IntPtr.Zero) {
            API.QueryServiceStatus(hService, ref dwServiceStatus);
        }
        if (dwServiceStatus.dwCurrentState == (int)WaitFor) {
            Timer1.Interval = _ReturnDelay;
            Close(); return;
        }
    }

    internal string ServiceAction {
        get { return _Action; }
        set { _Action = value; lbl1.Text = $"TaskManager in attempting to {_Action} the following service..."; }
    }
    internal string ServiceName {
        get { return lbl2.Text; }
        set { lbl2.Text = value; }
    }
    internal string ErrorString {
        get { return lbl3.Text; }
        set {
            lbl3.Text = value.Trim();
            lbl3.Visible = !string.IsNullOrEmpty(lbl3.Text);
            Height = lbl3.Visible ? 160 : 116;
        }
    }
    internal API.SERVICE_STATE WaitFor;
}
