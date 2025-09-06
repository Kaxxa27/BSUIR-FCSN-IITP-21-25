using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.ServiceProcess;
using sMkTaskManager.Forms;
namespace sMkTaskManager.Classes;

[SupportedOSPlatform("windows")]
internal class TaskManagerService : IEquatable<TaskManagerService>, INotifyPropertyChanged {
    private ServiceController _BaseService;
    private ServiceControllerStatus _StatusCode;
    private ServiceStartMode _StartupCode;
    private readonly string _Ident;
    private string _PID = "", _Name = "", _Description = "", _Logon = "", _ImagePath = "", _CommandLine = "";
    private Color _ForeColor = Color.Empty, _BackColor = Color.Empty;
    private int _ImageIndex = 0;

    public TaskManagerService(string serviceIdent) {
        _Ident = serviceIdent;
        LastUpdated = DateTime.Now.Ticks;
        LastChanged = LastUpdated;
        PreviousUpdate = LastUpdated;
        PropertyChanged += MyPropertyChanged;
    }

    /* Public Properties */
    public bool CanStop { get; set; }
    public bool CanStart { get; set; }
    public bool CanPause { get; set; }
    public bool CanResume { get; set; }
    public long LastUpdated { get; set; }
    public long LastChanged { get; set; }
    public long PreviousUpdate { get; set; } = 0;
    public bool NotifyChanges { get; set; } = true;
    public string ID => _Ident;
    public string Ident => _Ident;

    public string PID { get => _PID; set { SetField(ref _PID, value); } }
    public string Name { get => _Name; set { SetField(ref _Name, value); } }
    public string Description { get => _Description; set { SetField(ref _Description, value); } }
    public string Logon {
        get => Shared.ToTitleCase(_Logon[(_Logon.IndexOf("\\") + 1)..].Trim().ToLower().Replace("service", " service").Replace("system", " system"));
        set { SetField(ref _Logon, value); }
    }
    public string ImagePath { get => _ImagePath; set { SetField(ref _ImagePath, value); } }
    public string CommandLine { get => _CommandLine; set { SetField(ref _CommandLine, value); } }
    public string Status => _StatusCode.ToString();
    public ServiceControllerStatus StatusCode { get => _StatusCode; set { SetField(ref _StatusCode, value, nameof(Status)); } }
    public string Startup => _StartupCode.ToString();
    public ServiceStartMode StartupCode { get => _StartupCode; set { SetField(ref _StartupCode, value, nameof(Startup)); } }
    public Color ForeColor { get => _ForeColor; set { SetField(ref _ForeColor, value); } }
    public Color BackColor { get => _BackColor; set { SetField(ref _BackColor, value); } }
    public int ImageIndex { get => _ImageIndex; set { SetField(ref _ImageIndex, value); } }
    public ServiceController[] DependentServices => (_BaseService == null) ? Array.Empty<ServiceController>() : _BaseService.DependentServices;
    public ServiceController[] ServicesDependedOn => (_BaseService == null) ? Array.Empty<ServiceController>() : _BaseService.ServicesDependedOn;

    /* Public Methods */
    public event PropertyChangedEventHandler? PropertyChanged;
    public bool Equals(TaskManagerService? other) => _Ident == other?._Ident;
    public override bool Equals(object? obj) => Equals(obj as TaskManagerService);
    public override int GetHashCode() => _Ident.GetHashCode();
    public void Load(ServiceController s) {
        NotifyChanges = false;
        _BaseService = s;
        Update();
        NotifyChanges = true;
    }
    public void Update() {
        try {
            LastUpdated = DateTime.Now.Ticks;
            _BaseService.Refresh();
            // Note: we get the description only the first time, thus Name is empty
            GetServiceInfo(Ident, string.IsNullOrEmpty(Name), out SERVICE_INFO si);
            Name = _BaseService.DisplayName;
            StatusCode = _BaseService.Status;
            StartupCode = si.StartType;
            Logon = si.Logon!;
            CommandLine = si.BinaryPathName!.Replace("\"", "");
            if (si.BinaryPathName.StartsWith('"')) {
                ImagePath = si.BinaryPathName[1..si.BinaryPathName.IndexOf('"', 1)];
            } else {
                ImagePath = si.BinaryPathName.Contains(' ') ? si.BinaryPathName[..si.BinaryPathName.IndexOf(' ')] : si.BinaryPathName;
            }
            CanPause = _BaseService.CanPauseAndContinue && _BaseService.Status == ServiceControllerStatus.Running;
            CanResume = _BaseService.CanPauseAndContinue && _BaseService.Status == ServiceControllerStatus.Paused;
            CanStop = _BaseService.CanStop && (_BaseService.Status == ServiceControllerStatus.Running || _BaseService.Status == ServiceControllerStatus.Paused);
            CanStart = (_BaseService.Status == ServiceControllerStatus.Stopped) && !(si.StartType == ServiceStartMode.Disabled);
            ImageIndex = (si.StartType == ServiceStartMode.Disabled) ? 2 : ((_BaseService.Status == ServiceControllerStatus.Running) ? 0 : 1);
            if (si.Description != "") { Description = si.Description!; }
            PID = (si.ProcessID == 0) ? "" : si.ProcessID.ToString();
            ForeColor = StartupCode == ServiceStartMode.Disabled ? SystemColors.GrayText : Color.Empty;

            if (NotifyChanges && LastUpdated <= LastChanged) {
                BackColor = Settings.Highlights.ChangingItems ? Settings.Highlights.ChangingColor : Color.Empty;
            } else {
                BackColor = Color.Empty;
            }

        } catch (Exception ex) { Shared.DebugTrap(ex); }

    }
    public bool Stop() {
        if (!CanStop) return false;
        bool res = true;
        using (frmService_Control frmControl = new(_Ident)) {
            frmControl.WaitFor = API.SERVICE_STATE.SERVICE_STOPPED;
            frmControl.ServiceAction = "stop";
            frmControl.ServiceName = _Name;
            frmControl.StartPosition = FormStartPosition.CenterParent;
            try {
                if (!ServiceStop(_Ident)) { frmControl.ErrorString = API.GetLastErrorStr(); res = false; }
                frmControl.ShowDialog();
            } catch (Exception ex) {
                res = false; Shared.DebugTrap(ex);
            } finally { Update(); }
        }
        return res;
    }
    public bool Start(string Params = "") {
        if (!CanStart) return false;
        if (StatusCode == ServiceControllerStatus.Paused) return Resume();
        bool res = true;
        using (frmService_Control frmControl = new(this.Ident)) {
            frmControl.WaitFor = API.SERVICE_STATE.SERVICE_RUNNING;
            frmControl.ServiceAction = "start";
            frmControl.ServiceName = _Name;
            frmControl.StartPosition = FormStartPosition.CenterParent;
            try {
                if (!ServiceStart(_Ident, Params)) { frmControl.ErrorString = API.GetLastErrorStr(); res = false; }
                frmControl.ShowDialog();
            } catch (Exception ex) {
                res = false; Shared.DebugTrap(ex);
            } finally { Update(); }
        }
        return res;
    }
    public bool Restart() {
        if (!CanStop) { return true; }
        return Stop() && Start();
    }
    public bool Pause() {
        if (!CanPause) return false;
        bool res = true;
        using (frmService_Control frmControl = new(_Ident)) {
            frmControl.WaitFor = API.SERVICE_STATE.SERVICE_PAUSED;
            frmControl.ServiceAction = "pause";
            frmControl.ServiceName = _Name;
            frmControl.StartPosition = FormStartPosition.CenterParent;
            try {
                if (!ServicePause(_Ident)) { frmControl.ErrorString = API.GetLastErrorStr(); res = false; }
                frmControl.ShowDialog();
            } catch (Exception ex) {
                res = false; Shared.DebugTrap(ex);
            } finally { Update(); }
        }
        return res;
    }
    public bool Resume() {
        if (!(StatusCode == ServiceControllerStatus.Paused)) return false;
        bool res = true;
        using (frmService_Control frmControl = new(_Ident)) {
            frmControl.WaitFor = API.SERVICE_STATE.SERVICE_RUNNING;
            frmControl.ServiceAction = "continue";
            frmControl.ServiceName = _Name;
            frmControl.StartPosition = FormStartPosition.CenterParent;
            try {
                if (!ServiceContinue(_Ident)) { frmControl.ErrorString = API.GetLastErrorStr(); res = false; }
                frmControl.ShowDialog();
            } catch (Exception ex) {
                res = false; Shared.DebugTrap(ex);
            } finally { Update(); }
        }
        return res;
    }
    public bool ChangeStartUp(ServiceStartMode mewMode) {
        const uint SERVICE_NO_CHANGE = 0xFFFFFFFFU;
        IntPtr hscManager = API.OpenSCManager(".", null, API.SERVICE_ACCESS.SERVICE_GENERIC_ALL);
        if (hscManager == IntPtr.Zero) { throw API.GetLastError(); }
        IntPtr hService = API.OpenService(hscManager, _Ident, API.SERVICE_ACCESS.SERVICE_QUERY_CONFIG | API.SERVICE_ACCESS.SERVICE_CHANGE_CONFIG);
        if (hService == IntPtr.Zero) { API.CloseServiceHandle(hscManager); throw API.GetLastError(); }
        bool result = API.ChangeServiceConfig(hService, SERVICE_NO_CHANGE, mewMode, SERVICE_NO_CHANGE, null, null, 0, null, null, null, null);
        API.CloseServiceHandle(hService);
        API.CloseServiceHandle(hscManager);
        if (result) { Update(); }
        return result;
    }
    public struct SERVICE_INFO {
        // Custom Structure to store all the values i really need
        public ServiceType ServiceType;
        public ServiceStartMode StartType;
        public int ErrorControl;
        public string BinaryPathName;
        public string LoadOrderGroup;
        public int TagId;
        public string Description;
        public string Logon;
        public uint ProcessID;
    }

    /* Private Methods */
    private void MyPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        if (!NotifyChanges) return;
        if (e.PropertyName == null) return;
        if (e.PropertyName.Equals("LastChanged")) return;
        if (e.PropertyName.Equals("LastUpdated")) return;
        if (e.PropertyName.Equals("PreviousUpdate")) return;
        LastChanged = LastUpdated;
    }
    private void OnPropertyChanged(PropertyChangedEventArgs e) { if (NotifyChanges) PropertyChanged?.Invoke(this, e); }
    private void SetField<T>(ref T field, T newValue, [CallerMemberName] string propertyName = "", string[]? alsoNotifyProperties = null) {
        if (!EqualityComparer<T>.Default.Equals(field, newValue)) {
            field = newValue;
            // Since all properties ending with Value should be read as the NonValue<string> Property
            if (propertyName.EndsWith("Value")) propertyName = propertyName.Replace("Value", "");
            if (NotifyChanges) OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            if (NotifyChanges && alsoNotifyProperties != null) {
                foreach (string ap in alsoNotifyProperties) { OnPropertyChanged(new PropertyChangedEventArgs(ap)); }
            }
        }
    }
    private static bool GetServiceInfo(string serviceIdent, bool incDescription, out SERVICE_INFO si) {
        if (serviceIdent.Equals("")) { si = default; return false; }
        si = new SERVICE_INFO();
        API.SetLastError(0);
        // Open Handlers
        IntPtr hscManager = API.OpenSCManager(".", null, API.SERVICE_ACCESS.SERVICE_GENERIC_ALL);
        if (hscManager == IntPtr.Zero) { throw API.GetLastError(); }
        IntPtr hService = API.OpenService(hscManager, serviceIdent, API.SERVICE_ACCESS.SERVICE_QUERY_CONFIG | API.SERVICE_ACCESS.SERVICE_QUERY_STATUS);
        if (hService == IntPtr.Zero) { API.CloseServiceHandle(hscManager); throw API.GetLastError(); }
        int bytesNeeded = 0;
        // Service Config
        API.QueryServiceConfig(hService, IntPtr.Zero, 0, ref bytesNeeded);
        IntPtr qscPtr = Marshal.AllocCoTaskMem(bytesNeeded);
        if (API.QueryServiceConfig(hService, qscPtr, bytesNeeded, ref bytesNeeded)) {
            API.QUERY_SERVICE_CONFIG qscs = (API.QUERY_SERVICE_CONFIG)Marshal.PtrToStructure(qscPtr, typeof(API.QUERY_SERVICE_CONFIG))!;
            si.ServiceType = qscs.dwServiceType;
            si.StartType = qscs.dwStartType;
            si.ErrorControl = qscs.dwErrorControl;
            si.BinaryPathName = qscs.lpBinaryPathName;
            si.LoadOrderGroup = qscs.lpLoadOrderGroup;
            si.TagId = qscs.dwTagId;
            si.Logon = (qscs.lpServiceStartName != "") ? qscs.lpServiceStartName : "LocalSystem";
            si.Description = "";
            Marshal.FreeCoTaskMem(qscPtr);

        } else { throw API.GetLastError(); }
        // Service Description
        if (incDescription) {
            API.QueryServiceConfig2(hService, 1, IntPtr.Zero, 0, ref bytesNeeded); // SERVICE_CONFIG_DESCRIPTION = 1
            IntPtr lpsdPtr = Marshal.AllocCoTaskMem(bytesNeeded);
            if (API.QueryServiceConfig2(hService, 1, lpsdPtr, bytesNeeded, ref bytesNeeded)) {
                API.QUERY_SERVICE_DESCRIPTION lpsd = (API.QUERY_SERVICE_DESCRIPTION)Marshal.PtrToStructure(lpsdPtr, typeof(API.QUERY_SERVICE_DESCRIPTION))!;
                si.Description = lpsd.lpDescription;
                Marshal.FreeCoTaskMem(lpsdPtr);
            }
        }
        // Service Status Process
        API.QueryServiceStatusEx(hService, 0, IntPtr.Zero, 0, ref bytesNeeded);
        IntPtr sspPtr = Marshal.AllocCoTaskMem(bytesNeeded);
        if (API.QueryServiceStatusEx(hService, 0, sspPtr, bytesNeeded, ref bytesNeeded)) {
            API.SERVICE_STATUS_PROCESS ssp = (API.SERVICE_STATUS_PROCESS)Marshal.PtrToStructure(sspPtr, typeof(API.SERVICE_STATUS_PROCESS))!;
            si.ProcessID = ssp.processID;
            Marshal.FreeCoTaskMem(sspPtr);
        }
        // Free Handles & Return
        API.CloseServiceHandle(hService);
        API.CloseServiceHandle(hscManager);
        return true;
    }
    private static bool ServiceStart(string serviceIdent, string Parameters = "") {
        if (serviceIdent.Equals("")) return true;
        API.SetLastError(0);
        IntPtr hscManager = API.OpenSCManager(".", null, API.SERVICE_ACCESS.SERVICE_GENERIC_ALL);
        if (hscManager == IntPtr.Zero) { throw API.GetLastError(); }
        IntPtr hService = API.OpenService(hscManager, serviceIdent, API.SERVICE_ACCESS.SERVICE_ALL_ACCESS);
        bool result;
        if (hService != IntPtr.Zero) {
            if (string.IsNullOrEmpty(Parameters.Trim())) {
                result = API.StartService(hService, 0, null);
            } else {
                result = API.StartService(hService, 1, new[] { Parameters });
            }
            API.CloseServiceHandle(hService);
            API.CloseServiceHandle(hscManager);
        } else {
            API.CloseServiceHandle(hscManager);
            throw API.GetLastError();
        }
        return result;
    }
    private static bool ServiceStop(string serviceIdent) {
        if (serviceIdent.Equals("")) return true;
        API.SetLastError(0);
        IntPtr hscManager = API.OpenSCManager(".", null, API.SERVICE_ACCESS.SERVICE_GENERIC_ALL);
        if (hscManager == IntPtr.Zero) { throw API.GetLastError(); }
        IntPtr hService = API.OpenService(hscManager, serviceIdent, API.SERVICE_ACCESS.SERVICE_ALL_ACCESS);
        bool result;
        if (hService != IntPtr.Zero) {
            API.SERVICE_STATUS newStatus = new();
            result = API.ControlService(hService, API.SERVICE_CONTROL.STOP, newStatus);
            API.CloseServiceHandle(hService);
            API.CloseServiceHandle(hscManager);
        } else {
            API.CloseServiceHandle(hscManager);
            throw API.GetLastError();
        }
        return result;
    }
    private static bool ServicePause(string serviceIdent) {
        if (serviceIdent.Equals("")) return true;
        API.SetLastError(0);
        IntPtr hscManager = API.OpenSCManager(".", null, API.SERVICE_ACCESS.SERVICE_GENERIC_ALL);
        if (hscManager == IntPtr.Zero) { throw API.GetLastError(); }
        IntPtr hService = API.OpenService(hscManager, serviceIdent, API.SERVICE_ACCESS.SERVICE_ALL_ACCESS);
        bool result;
        if (hService != IntPtr.Zero) {
            API.SERVICE_STATUS newStatus = new();
            result = API.ControlService(hService, API.SERVICE_CONTROL.PAUSE, newStatus);
            API.CloseServiceHandle(hService);
            API.CloseServiceHandle(hscManager);
        } else {
            API.CloseServiceHandle(hscManager);
            throw API.GetLastError();
        }
        return result;
    }
    private static bool ServiceContinue(string serviceIdent) {
        if (serviceIdent.Equals("")) return true;
        API.SetLastError(0);
        IntPtr hscManager = API.OpenSCManager(".", null, API.SERVICE_ACCESS.SERVICE_GENERIC_ALL);
        if (hscManager == IntPtr.Zero) { throw API.GetLastError(); }
        IntPtr hService = API.OpenService(hscManager, serviceIdent, API.SERVICE_ACCESS.SERVICE_ALL_ACCESS);
        bool result;
        if (hService != IntPtr.Zero) {
            API.SERVICE_STATUS newStatus = new();
            result = API.ControlService(hService, API.SERVICE_CONTROL.CONTINUE, newStatus);
            API.CloseServiceHandle(hService);
            API.CloseServiceHandle(hscManager);
        } else {
            API.CloseServiceHandle(hscManager);
            throw API.GetLastError();
        }
        return result;
    }

}

[SupportedOSPlatform("windows")]
internal class TaskManagerServicesCollection : BindingList<TaskManagerService> {
    public BindingSource DataExporter { get; init; }
    public TaskManagerServicesCollection() {
        DataExporter = new(this, null) { RaiseListChangedEvents = true };
        AllowRemove = true;
        RaiseListChangedEvents = true;
    }

    public bool Contains(string serviceIdent) {
        foreach (TaskManagerService i in Items) {
            if (i.Ident == serviceIdent) { return true; }
        }
        return false;
    }

    public TaskManagerService? GetService(string serviceIdent) {
        foreach (TaskManagerService i in Items) {
            if (i.Ident == serviceIdent) return i;
        }
        return null;
    }
    public TaskManagerService? this[string serviceIdent] => Items.Where(_ => _.Ident == serviceIdent).FirstOrDefault();

}
