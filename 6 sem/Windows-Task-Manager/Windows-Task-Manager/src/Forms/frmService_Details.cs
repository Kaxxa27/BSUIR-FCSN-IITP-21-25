using System.ComponentModel;
using System.Runtime.Versioning;
using System.ServiceProcess;
namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
public partial class frmService_Details : Form {
    private Classes.TaskManagerService? s;

    public frmService_Details() {
        InitializeComponent();
        ilDependencies.Images.Add("Gear", Resources.Resources.Service_Gear);
        ilDependencies.Images.Add("Device", Resources.Resources.Service_Device);
    }
    public string SID {
        get {
            return s == null ? "" : s.Ident;
        }
        set {
            if (!string.IsNullOrEmpty(value.Trim()) && value != SID) {
                s = default;
                foreach (ServiceController serv in ServiceController.GetServices()) {
                    if (value == serv.ServiceName) {
                        s = new Classes.TaskManagerService(value);
                        s.Load(serv);
                        break;
                    }
                }
                LoadServiceDetails();
                LoadServiceDependencies();
            }
        }
    }

    private bool Modified {
        get { return btnApply.Enabled; }
        set { btnApply.Enabled = value; }
    }
    private void frmService_Details_Load(object sender, EventArgs e) {
        Modified = false;
        if (s != null) s.PropertyChanged += s_PropertyChanged;
    }
    private void txtStartMethod_SelectedIndexChanged(object sender, EventArgs e) {
        Modified = true;
    }
    private void s_PropertyChanged(object? sender, PropertyChangedEventArgs e) {
        if (s == null) return;
        if (e.PropertyName == "Status") txtStatus.Text = s.Status;
    }
    private void btnOk_Click(object sender, EventArgs e) {
        if (Modified) { btnApply.PerformClick(); }
        Close();
    }
    private void btnCancel_Click(object sender, EventArgs e) {
        DialogResult = DialogResult.Cancel;
        Close();
    }
    private void btnApply_Click(object sender, EventArgs e) {
        if (Modified && s != null) {
            if (txtStartMethod.SelectedIndex == 1) { txtStartMethod.SelectedIndex = 0; }
            switch (txtStartMethod.SelectedIndex) {
                case 0: { Modified = !s.ChangeStartUp(ServiceStartMode.Automatic); break; }
                case 1: { Modified = !s.ChangeStartUp(ServiceStartMode.Automatic); break; }
                case 2: { Modified = !s.ChangeStartUp(ServiceStartMode.Manual); break; }
                case 3: { Modified = !s.ChangeStartUp(ServiceStartMode.Disabled); break; }
            }
        }
        btnCheckEnables();
    }
    private void btnStart_Click(object sender, EventArgs e) {
        if (s != null && s.CanStart) s.Start(txtStartParams.Text);
        btnCheckEnables();
    }
    private void btnStop_Click(object sender, EventArgs e) {
        if (s != null && s.CanStop) s.Stop();
        btnCheckEnables();
    }
    private void btnPause_Click(object sender, EventArgs e) {
        if (s != null && s.CanPause) s.Pause();
        btnCheckEnables();

    }
    private void btnResume_Click(object sender, EventArgs e) {
        if (s != null && s.CanResume) s.Resume();
        btnCheckEnables();
    }

    private void btnCheckEnables() {
        btnStart.Enabled = s != null && s.CanStart;
        btnStop.Enabled = s != null && s.CanStop;
        btnPause.Enabled = s != null && s.CanPause;
        btnResume.Enabled = s != null && s.CanResume;
        txtStartParams.Enabled = btnStart.Enabled;
    }
    private void LoadServiceDetails() {
        Modified = false;
        txtStartParams.Text = "";
        txtServiceName.Text = s != null ? s.Ident : "";
        txtDisplayName.Text = s != null ? s.Name : "";
        txtLogonAs.Text = s != null ? s.Logon : "";
        txtCommandLine.Text = s != null ? s.CommandLine : "";
        txtDescription.Text = s != null ? s.Description : "";
        txtStatus.Text = s != null ? s.Status : "";
        if (s == null) txtStartMethod.SelectedItem = null;
        if (s?.StartupCode == ServiceStartMode.Automatic) txtStartMethod.SelectedIndex = 0;
        if (s?.StartupCode == ServiceStartMode.Manual) txtStartMethod.SelectedIndex = 2;
        if (s?.StartupCode == ServiceStartMode.Disabled) txtStartMethod.SelectedIndex = 3;
        btnCheckEnables();
        Modified = false;
    }
    private void LoadServiceDependencies() {
        tvDependsOn.Nodes.Clear();
        tvDependsTo.Nodes.Clear();
        if (s == null) return;
        foreach (ServiceController serv in s.ServicesDependedOn) {
            TreeNode newNode = tvDependsOn.Nodes.Add(serv.DisplayName);
            newNode.StateImageIndex = (Convert.ToInt32(serv.ServiceType) < 9) ? 1 : 0;
            AddChildDependsOn(serv, ref newNode);
        }
        foreach (ServiceController serv in s.DependentServices) {
            TreeNode newNode = tvDependsTo.Nodes.Add(serv.DisplayName);
            newNode.StateImageIndex = (Convert.ToInt32(serv.ServiceType) < 9) ? 1 : 0;
            AddChildDependsTo(serv, ref newNode);
        }
        tvDependsOn.Enabled = tvDependsOn.Nodes.Count > 0;
        tvDependsOn.Indent = tvDependsOn.Enabled ? 15 : 40;
        tvDependsOn.ShowLines = tvDependsOn.Enabled;
        tvDependsTo.Enabled = tvDependsTo.Nodes.Count > 0;
        tvDependsTo.Indent = tvDependsTo.Enabled ? 15 : 40;
        tvDependsTo.ShowLines = tvDependsTo.Enabled;

        if (!tvDependsOn.Enabled) { tvDependsOn.Nodes.Add("<No Dependencies>"); }
        if (!tvDependsTo.Enabled) { tvDependsTo.Nodes.Add("<No Dependencies>"); }

    }
    private void AddChildDependsOn(ServiceController baseService, ref TreeNode ParentNode) {
        foreach (ServiceController serv in baseService.ServicesDependedOn) {
            TreeNode newNode = ParentNode.Nodes.Add(serv.DisplayName);
            newNode.StateImageIndex = (Convert.ToInt32(serv.ServiceType) < 9) ? 1 : 0;
            AddChildDependsOn(serv, ref newNode);
        }
    }
    private void AddChildDependsTo(ServiceController baseService, ref TreeNode ParentNode) {
        foreach (ServiceController serv in baseService.DependentServices) {
            TreeNode newNode = ParentNode.Nodes.Add(serv.DisplayName);
            newNode.StateImageIndex = (Convert.ToInt32(serv.ServiceType) < 9) ? 1 : 0;
            AddChildDependsTo(serv, ref newNode);
        }
    }
}
