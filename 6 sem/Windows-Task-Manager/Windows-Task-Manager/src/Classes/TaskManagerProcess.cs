using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Principal;
using System.Text;

namespace sMkTaskManager.Classes;

[SupportedOSPlatform("windows")]
internal class TaskManagerProcess : IEquatable<TaskManagerProcess>, INotifyPropertyChanged {
    private readonly int _PID;
    private IntPtr _pHandle = IntPtr.Zero;

    public TaskManagerProcess(int pid) {
        _PID = pid;
        LastUpdated = DateTime.Now.Ticks;
        LastChanged = LastUpdated;
        PreviousUpdate = LastUpdated;
        PropertyChanged += MyPropertyChanged;
    }
    public TaskManagerProcess(IntPtr pid) : this(pid.ToInt32()) { }

    public string ID => _PID.ToString();
    public int PID => _PID;
    private Color _BackColor;
    public Color BackColor { get => _BackColor; set { SetField(ref _BackColor, value); } }
    public long LastUpdated { get; set; }
    public long LastChanged { get; set; }
    public long PreviousUpdate { get; set; } = 0;
    public bool NotifyChanges { get; set; } = true;
    public int ImageIndex { get; set; } = -1;
    public string ImageKey { get; set; } = "";
    public bool IgnoreBackColor { get; set; } = false;
    public Icon? Icon { get; private set; } = null;

    private bool _Suspended;
    public bool Suspended { get => _Suspended; set { SetField(ref _Suspended, value); BackColor = value ? Settings.Highlights.FrozenColor : Color.Empty; } }
    /* Primary Basic Properties */
    private string _Name = "", _Description = "", _ImagePath = "", _CommandLine = "";
    private string _Affinity = "", _Priority = "", _Username = "", _RunTime = "", _CpuUsage = "";
    private uint _SessionID, _Handles, _Threads; private ulong _PageFaults;
    private int _GDIObjects, _UserObjects, _GDIObjectsPeak, _UserObjectsPeak;
    private IntPtr _ParentPID;
    public string Name { get => _Name; set { SetField(ref _Name, value); } }
    public IntPtr ParentPID { get => _ParentPID; set { SetField(ref _ParentPID, value); } }
    public string Description { get => _Description; set { SetField(ref _Description, value); } }
    public string ImagePath { get => _ImagePath; set { SetField(ref _ImagePath, value); } }
    public string CommandLine { get => _CommandLine; set { SetField(ref _CommandLine, value); } }
    public string Affinity { get => _Affinity; set { SetField(ref _Affinity, value); } }
    public string Priority { get => _Priority; set { SetField(ref _Priority, value); } }
    public string Username { get => _Username; set { SetField(ref _Username, value); } }
    public string RunTime { get => (_PID == 0) ? "n/a." : _RunTime; set { SetField(ref _RunTime, value); } }
    public string CpuUsage { get => _CpuUsage; set { SetField(ref _CpuUsage, value); } }
    public uint SessionID { get => _SessionID; set { SetField(ref _SessionID, value); } }
    public uint Handles { get => _Handles; set { SetField(ref _Handles, value); } }
    public uint Threads { get => _Threads; set { SetField(ref _Threads, value); } }
    public ulong PageFaults { get => _PageFaults; set { SetField(ref _PageFaults, value); } }
    public int GDIObjects { get => _GDIObjects; set { SetField(ref _GDIObjects, value); } }
    public int UserObjects { get => _UserObjects; set { SetField(ref _UserObjects, value); } }
    public int GDIObjectsPeak { get => _GDIObjectsPeak; set { SetField(ref _GDIObjectsPeak, value); } }
    public int UserObjectsPeak { get => _UserObjectsPeak; set { SetField(ref _UserObjectsPeak, value); } }
    /* Times Related Properties */
    private TimeSpan _CreationTimeValue, _CpuTimeValue, _UserTimeValue, _KernelTimeValue, _prevCpuTimeValue;
    public TimeSpan CreationTimeValue { get => _CreationTimeValue; set { SetField(ref _CreationTimeValue, value); } }
    public string CreationTime => (_PID == 0) ? "n/a." : new DateTime(_CreationTimeValue.Ticks).ToString();
    public TimeSpan CpuTimeValue { get => _CpuTimeValue; set { SetField(ref _CpuTimeValue, value); } }
    public string CpuTime => (_PID == 0) ? "n/a." : Shared.TimeSpanToElapsed(_CpuTimeValue);
    public TimeSpan UserTimeValue { get => _UserTimeValue; set { SetField(ref _UserTimeValue, value); } }
    public string UserTime => (_PID == 0) ? "n/a." : Shared.TimeSpanToElapsed(_UserTimeValue);
    public TimeSpan KernelTimeValue { get => _KernelTimeValue; set { SetField(ref _KernelTimeValue, value); } }
    public string KernelTime => (_PID == 0) ? "n/a." : Shared.TimeSpanToElapsed(_KernelTimeValue);
    /* Memory Related Properties */
    private ulong _PagedPoolValue, _PagedPoolPeakValue, _NonPagedPoolValue, _NonPagedPoolPeakValue;
    private ulong _PagedMemoryValue, _PagedMemoryPeakValue, _VirtualMemoryValue, _VirtualMemoryPeakValue;
    private ulong _WorkingSetValue, _WorkingSetPeakValue, _WorkingSetPrivateValue, _WorkingSetShareableValue, _PrivateBytesValue;
    public ulong PagedPoolValue { get => _PagedPoolValue; set { SetField(ref _PagedPoolValue, value); } }
    public string PagedPool => FormatValue(_PagedPoolValue, FormatValueTypes.KBytes);
    public ulong PagedPoolPeakValue { get => _PagedPoolPeakValue; set { SetField(ref _PagedPoolPeakValue, value); } }
    public string PagedPoolPeak => FormatValue(_PagedPoolPeakValue, FormatValueTypes.KBytes);
    public ulong NonPagedPoolValue { get => _NonPagedPoolValue; set { SetField(ref _NonPagedPoolValue, value); } }
    public string NonPagedPool => FormatValue(_NonPagedPoolValue, FormatValueTypes.KBytes);
    public ulong NonPagedPoolPeakValue { get => _NonPagedPoolPeakValue; set { SetField(ref _NonPagedPoolPeakValue, value); } }
    public string NonPagedPoolPeak => FormatValue(_NonPagedPoolPeakValue, FormatValueTypes.KBytes);
    public ulong PagedMemoryValue { get => _PagedMemoryValue; set { SetField(ref _PagedMemoryValue, value); } }
    public string PagedMemory => FormatValue(_PagedMemoryValue, FormatValueTypes.KBytes);
    public ulong PagedMemoryPeakValue { get => _PagedMemoryPeakValue; set { SetField(ref _PagedMemoryPeakValue, value); } }
    public string PagedMemoryPeak => FormatValue(_PagedMemoryPeakValue, FormatValueTypes.KBytes);
    public ulong VirtualMemoryValue { get => _VirtualMemoryValue; set { SetField(ref _VirtualMemoryValue, value); } }
    public string VirtualMemory => FormatValue(_VirtualMemoryValue, FormatValueTypes.KBytes);
    public ulong VirtualMemoryPeakValue { get => _VirtualMemoryPeakValue; set { SetField(ref _VirtualMemoryPeakValue, value); } }
    public string VirtualMemoryPeak => FormatValue(_VirtualMemoryPeakValue, FormatValueTypes.KBytes);
    public ulong WorkingSetValue { get => _WorkingSetValue; set { SetField(ref _WorkingSetValue, value); } }
    public string WorkingSet => FormatValue(_WorkingSetValue, FormatValueTypes.KBytes);
    public ulong WorkingSetPeakValue { get => _WorkingSetPeakValue; set { SetField(ref _WorkingSetPeakValue, value); } }
    public string WorkingSetPeak => FormatValue(_WorkingSetPeakValue, FormatValueTypes.KBytes);
    public ulong WorkingSetPrivateValue { get => _WorkingSetPrivateValue; set { SetField(ref _WorkingSetPrivateValue, value); } }
    public string WorkingSetPrivate => FormatValue(_WorkingSetPrivateValue, FormatValueTypes.KBytes);
    public ulong WorkingSetShareableValue { get => _WorkingSetShareableValue; set { SetField(ref _WorkingSetShareableValue, value); } }
    public string WorkingSetShareable => FormatValue(_WorkingSetShareableValue, FormatValueTypes.KBytes);
    public ulong PrivateBytesValue { get => _PrivateBytesValue; set { SetField(ref _PrivateBytesValue, value); } }
    public string PrivateBytes => FormatValue(_PrivateBytesValue, FormatValueTypes.KBytes);
    /* I/O Related Properties */
    private ulong _ReadTransferValue, _WriteTransferValue, _OtherTransferValue;
    private ulong _ReadTransferDeltaValue, _WriteTransferDeltaValue, _OtherTransferDeltaValue;
    private ulong _ReadOperationsValue, _WriteOperationsValue, _OtherOperationsValue;
    private ulong _ReadOperationsDeltaValue, _WriteOperationsDeltaValue, _OtherOperationsDeltaValue;
    public ulong ReadTransferValue { get => _ReadTransferValue; set { SetField(ref _ReadTransferValue, value); } }
    public string ReadTransfer => FormatValue(_ReadTransferValue, FormatValueTypes.AutoBytes, true);
    public ulong ReadTransferDeltaValue { get => _ReadTransferDeltaValue; set { SetField(ref _ReadTransferDeltaValue, value); } }
    public string ReadTransferDelta => FormatValue(_ReadTransferDeltaValue, FormatValueTypes.AutoBytes, true);
    public ulong ReadOperationsValue { get => _ReadOperationsValue; set { SetField(ref _ReadOperationsValue, value); } }
    public string ReadOperations => FormatValue(_ReadOperationsValue, FormatValueTypes.Number, true);
    public ulong ReadOperationsDeltaValue { get => _ReadOperationsDeltaValue; set { SetField(ref _ReadOperationsDeltaValue, value); } }
    public string ReadOperationsDelta => FormatValue(_ReadOperationsDeltaValue, FormatValueTypes.Number, true);
    public ulong WriteTransferValue { get => _WriteTransferValue; set { SetField(ref _WriteTransferValue, value); } }
    public string WriteTransfer => FormatValue(_WriteTransferValue, FormatValueTypes.AutoBytes, true);
    public ulong WriteTransferDeltaValue { get => _WriteTransferDeltaValue; set { SetField(ref _WriteTransferDeltaValue, value); } }
    public string WriteTransferDelta => FormatValue(_WriteTransferDeltaValue, FormatValueTypes.AutoBytes, true);
    public ulong WriteOperationsValue { get => _WriteOperationsValue; set { SetField(ref _WriteOperationsValue, value); } }
    public string WriteOperations => FormatValue(_WriteOperationsValue, FormatValueTypes.Number, true);
    public ulong WriteOperationsDeltaValue { get => _WriteOperationsDeltaValue; set { SetField(ref _WriteOperationsDeltaValue, value); } }
    public string WriteOperationsDelta => FormatValue(_WriteOperationsDeltaValue, FormatValueTypes.Number, true);
    public ulong OtherTransferValue { get => _OtherTransferValue; set { SetField(ref _OtherTransferValue, value); } }
    public string OtherTransfer => FormatValue(_OtherTransferValue, FormatValueTypes.AutoBytes, true);
    public ulong OtherTransferDeltaValue { get => _OtherTransferDeltaValue; set { SetField(ref _OtherTransferDeltaValue, value); } }
    public string OtherTransferDelta => FormatValue(_OtherTransferDeltaValue, FormatValueTypes.AutoBytes, true);
    public ulong OtherOperationsValue { get => _OtherOperationsValue; set { SetField(ref _OtherOperationsValue, value); } }
    public string OtherOperations => FormatValue(_OtherOperationsValue, FormatValueTypes.Number, true);
    public ulong OtherOperationsDeltaValue { get => _OtherOperationsDeltaValue; set { SetField(ref _OtherOperationsDeltaValue, value); } }
    public string OtherOperationsDelta => FormatValue(_OtherOperationsDeltaValue, FormatValueTypes.Number, true);
    /* Disk Related Properties */
    private ulong _DiskReadValue, _DiskWriteValue, _DiskReadDeltaValue, _DiskWriteDeltaValue, _DiskReadRateValue, _DiskWriteRateValue;
    public ulong DiskReadValue { get => _DiskReadValue; set { SetField(ref _DiskReadValue, value); } }
    public string DiskRead => FormatValue(_DiskReadValue, FormatValueTypes.AutoBytes);
    public ulong DiskReadDeltaValue { get => _DiskReadDeltaValue; set { SetField(ref _DiskReadDeltaValue, value); } }
    public string DiskReadDelta => FormatValue(_DiskReadDeltaValue, FormatValueTypes.AutoBytes);
    public ulong DiskReadRateValue { get => _DiskReadRateValue; set { SetField(ref _DiskReadRateValue, value); } }
    public string DiskReadRate => FormatValue(_DiskReadRateValue, FormatValueTypes.Kbps);
    public ulong DiskWriteValue { get => _DiskWriteValue; set { SetField(ref _DiskWriteValue, value); } }
    public string DiskWrite => FormatValue(_DiskWriteValue, FormatValueTypes.AutoBytes);
    public ulong DiskWriteDeltaValue { get => _DiskWriteDeltaValue; set { SetField(ref _DiskWriteDeltaValue, value); } }
    public string DiskWriteDelta => FormatValue(_DiskWriteDeltaValue, FormatValueTypes.AutoBytes);
    public ulong DiskWriteRateValue { get => _DiskWriteRateValue; set { SetField(ref _DiskWriteRateValue, value); } }
    public string DiskWriteRate => FormatValue(_DiskWriteRateValue, FormatValueTypes.Kbps);
    /* Net Related Properties */
    private ulong _NetSentValue, _NetRcvdValue, _NetSentDeltaValue, _NetRcvdDeltaValue, _NetSentRateValue, _NetRcvdRateValue;
    public ulong NetSentValue { get => _NetSentValue; set { SetField(ref _NetSentValue, value); } }
    public string NetSent => FormatValue(_NetSentValue, FormatValueTypes.AutoBytes);
    public ulong NetSentDeltaValue { get => _NetSentDeltaValue; set { SetField(ref _NetSentDeltaValue, value); } }
    public string NetSentDelta => FormatValue(_NetSentDeltaValue, FormatValueTypes.AutoBytes);
    public ulong NetSentRateValue { get => _NetSentRateValue; set { SetField(ref _NetSentRateValue, value); } }
    public string NetSentRate => FormatValue(_NetSentRateValue, FormatValueTypes.Kbps);
    public ulong NetRcvdValue { get => _NetRcvdValue; set { SetField(ref _NetRcvdValue, value); } }
    public string NetRcvd => FormatValue(_NetRcvdValue, FormatValueTypes.AutoBytes);
    public ulong NetRcvdDeltaValue { get => _NetRcvdDeltaValue; set { SetField(ref _NetRcvdDeltaValue, value); } }
    public string NetRcvdDelta => FormatValue(_NetRcvdDeltaValue, FormatValueTypes.AutoBytes);
    public ulong NetRcvdRateValue { get => _NetRcvdRateValue; set { SetField(ref _NetRcvdRateValue, value); } }
    public string NetRcvdRate => FormatValue(_NetRcvdRateValue, FormatValueTypes.Kbps);
    /* Product Related Properties */
    private FileVersionInfo? _FileVersionInfo = null;
    public string ProductName => (_FileVersionInfo == null || _FileVersionInfo.ProductName == null) ? "n/a." : _FileVersionInfo.ProductName;
    public string ProductVersion => (_FileVersionInfo == null || _FileVersionInfo.ProductVersion == null) ? "n/a." : _FileVersionInfo.ProductVersion;
    public string ProductCompany => (_FileVersionInfo == null || _FileVersionInfo.CompanyName == null) ? "n/a." : _FileVersionInfo.CompanyName;
    public string ProductLanguage => (_FileVersionInfo == null || _FileVersionInfo.Language == null) ? "n/a." : _FileVersionInfo.Language;

    /* Public Methods */
    public event PropertyChangedEventHandler? PropertyChanged;
    public bool Equals(TaskManagerProcess? other) {
        return _PID == other?._PID;
    }
    public override bool Equals(object? obj) {
        return Equals(obj as TaskManagerProcess);
    }
    public override int GetHashCode() {
        return _PID;
    }
    public void Load(API.SYSTEM_PROCESS_INFORMATION spi, in HashSet<string> vv, bool getNewSPI = false) {
        if (getNewSPI) GetSpecificSPI(_PID, out spi);
        NotifyChanges = false;

        // Load values that are fixed to this process and wont change...
        if (_PID > Shared.bpi) {
            Name = spi.ImageName.Buffer ?? "";
            SessionID = spi.SessionId;
            ParentPID = spi.InheritedFromUniqueProcessId;

            _pHandle = API.OpenProcess(API.ProcessAccessFlags.QueryInformation, false, PID);
            if (_pHandle == IntPtr.Zero) {
                ImagePath = "No Access"; Username = "No Access"; CommandLine = "No Access";
            } else {
                // Load Process ImagePath
                try {
                    uint sbCapacity = 5000;
                    StringBuilder sbImagePath = new((int)sbCapacity);
                    API.QueryFullProcessImageName(_pHandle, 0, sbImagePath, ref sbCapacity);
                    ImagePath = sbImagePath.ToString();
                    sbImagePath.Clear();
                } catch { ImagePath = "n/a."; }
                // Load Username
                if (API.OpenProcessToken(_pHandle, 0x0008, out nint _tHandle)) {  /* TOKEN_QUERY = 0x0008 */
                    using WindowsIdentity wi = new(_tHandle);
                    Username = Shared.ToTitleCase(wi.Name.Contains('\\') ? wi.Name[(wi.Name.IndexOf('\\') + 1)..] : wi.Name);
                } else { Username = "n/a."; }
                if (_tHandle != IntPtr.Zero) API.CloseHandle(_tHandle);
                // Load Process Description
                if (File.Exists(ImagePath)) {
                    _FileVersionInfo = FileVersionInfo.GetVersionInfo(ImagePath);
                    Description = (_FileVersionInfo == null || _FileVersionInfo.FileDescription == null) ? "n/a." : _FileVersionInfo.FileDescription;
                }
                // Load Process CommandLine 
                // TODO: We must implement this
                CommandLine = "Not Implemented...";
            }
            if (_pHandle != IntPtr.Zero) API.CloseHandle(_pHandle);
        } else {
            if (_PID == 0) { Name = "Idle"; Description = "Idle Processor"; }
            if (_PID == 4) { Name = "System"; Description = "NT Kernel & System"; }
            SessionID = 0;
            ParentPID = 0;
            Priority = "Normal";
            Affinity = "All";
            Username = Shared.GetSystemAccount();
        }

        // And then update the rest of values...
        Update(spi, vv);
        NotifyChanges = true;
    }
    public void Update(API.SYSTEM_PROCESS_INFORMATION spi, in HashSet<string> vv, bool getNewSPI = false) {
        // vv is short for visibleValues and contains the cols definition we are showing, so we dont update if we dont need.
        if (getNewSPI) GetSpecificSPI(_PID, out spi);
        PreviousUpdate = LastUpdated;
        LastUpdated = DateTime.Now.Ticks;
        // Check if imSuspended
        Suspended = imSuspended(spi.Threads);
        // Handles & Threads Count
        if (vv.Contains("Handles")) Handles = spi.HandleCount;
        if (vv.Contains("Threads")) Threads = spi.NumberOfThreads;
        // General Memory Values
        if (vv.Contains("WorkingSet") || vv.Contains("WorkingSetPeak") || vv.Contains("WorkingSetPrivate") || vv.Contains("WorkingSetShareable")) {
            WorkingSetValue = spi.WorkingSetSize;
            WorkingSetPeakValue = spi.PeakWorkingSetSize;
            WorkingSetPrivateValue = spi.WorkingSetPrivateSize;
            WorkingSetShareableValue = spi.WorkingSetSize - spi.WorkingSetPrivateSize;
        }
        if (vv.Contains("PagedMemory")) PagedMemoryValue = spi.PagefileUsage;
        if (vv.Contains("PagedMemoryPeak")) PagedMemoryPeakValue = spi.PeakPagefileUsage;
        if (vv.Contains("PagedPool")) PagedPoolValue = spi.QuotaPagedPoolUsage;
        if (vv.Contains("PagedPoolPeak")) PagedPoolPeakValue = spi.QuotaPeakPagedPoolUsage;
        if (vv.Contains("NonPagedPool")) NonPagedPoolValue = spi.QuotaNonPagedPoolUsage;
        if (vv.Contains("NonPagedPoolPeak")) NonPagedPoolPeakValue = spi.QuotaPeakNonPagedPoolUsage;
        if (vv.Contains("PageFaults")) PageFaults = spi.PageFaultCount;
        if (vv.Contains("PrivateBytes")) PrivateBytesValue = spi.PrivatePageCount;
        if (vv.Contains("VirtualMemory")) VirtualMemoryValue = spi.VirtualSize;
        if (vv.Contains("VirtualMemoryPeak")) VirtualMemoryPeakValue = spi.PeakVirtualSize;
        // I/O Counters Values
        if (vv.Contains("ReadTransfer") || vv.Contains("ReadTransferDelta")) {
            ReadTransferDeltaValue = (ReadTransferValue == 0) ? 0 : spi.ReadTransferCount - ReadTransferValue;
            ReadTransferValue = spi.ReadTransferCount;
        }
        if (vv.Contains("ReadOperations") || vv.Contains("ReadOperationsDelta")) {
            ReadOperationsDeltaValue = (ReadOperationsValue == 0) ? 0 : spi.ReadOperationCount - ReadOperationsValue;
            ReadOperationsValue = spi.ReadOperationCount;
        }
        if (vv.Contains("WriteTransfer") || vv.Contains("WriteTransferDelta")) {
            WriteTransferDeltaValue = (WriteTransferValue == 0) ? 0 : spi.WriteTransferCount - WriteTransferValue;
            WriteTransferValue = spi.WriteTransferCount;
        }
        if (vv.Contains("WriteOperations") || vv.Contains("WriteOperationsDelta")) {
            WriteOperationsDeltaValue = (WriteOperationsValue == 0) ? 0 : spi.WriteOperationCount - WriteOperationsValue;
            WriteOperationsValue = spi.WriteOperationCount;
        }
        if (vv.Contains("OtherTransfer") || vv.Contains("OtherTransferDelta")) {
            OtherTransferDeltaValue = (OtherTransferValue == 0) ? 0 : spi.OtherTransferCount - OtherTransferValue;
            OtherTransferValue = spi.OtherTransferCount;
        }
        if (vv.Contains("OtherOperations") || vv.Contains("OtherOperationsDelta")) {
            OtherOperationsDeltaValue = (OtherOperationsValue == 0) ? 0 : spi.OtherOperationCount - OtherOperationsValue;
            OtherOperationsValue = spi.OtherOperationCount;
        }
        // ETW Data Values, if running
        if (Shared.ETW.Running) {
            if (vv.Contains("DiskRead") || vv.Contains("DiskReadDelta") || vv.Contains("DiskReadRate")) {
                DiskReadDeltaValue = (DiskReadValue == 0) ? 0 : Shared.ETW.Stats(_PID).DiskReaded - DiskReadValue;
                DiskReadValue = Shared.ETW.Stats(_PID).DiskReaded;
                DiskReadRateValue = CalculateRateValue(DiskReadDeltaValue);
            }
            if (vv.Contains("DiskWrite") || vv.Contains("DiskWriteDelta") || vv.Contains("DiskWriteRate")) {
                DiskWriteDeltaValue = (DiskWriteValue == 0) ? 0 : Shared.ETW.Stats(_PID).DiskWroted - DiskWriteValue;
                DiskWriteValue = Shared.ETW.Stats(_PID).DiskWroted;
                DiskWriteRateValue = CalculateRateValue(DiskWriteDeltaValue);
            }
            if (vv.Contains("NetSent") || vv.Contains("NetSentDelta") || vv.Contains("NetSentRate")) {
                NetSentDeltaValue = (NetSentValue == 0) ? 0 : Shared.ETW.Stats(_PID).NetSent - NetSentValue;
                NetSentValue = Shared.ETW.Stats(_PID).NetSent;
                NetSentRateValue = CalculateRateValue(NetSentDeltaValue);
            }
            if (vv.Contains("NetReceived") || vv.Contains("NetReceivedDelta") || vv.Contains("NetReceivedRate")) {
                NetRcvdDeltaValue = (NetRcvdValue == 0) ? 0 : Shared.ETW.Stats(_PID).NetReceived - NetRcvdValue;
                NetRcvdValue = Shared.ETW.Stats(_PID).NetReceived;
                NetRcvdRateValue = CalculateRateValue(NetRcvdDeltaValue);
            }
        }
        // These values should only be read for >bpi
        if (PID > Shared.bpi) {
            _pHandle = API.OpenProcess(API.ProcessAccessFlags.QueryInformation, false, PID);
            // Process Objects
            if (vv.Contains("GDIObjects")) GDIObjects = API.GetGuiResources(_pHandle, 0);
            if (vv.Contains("UserObjects")) UserObjects = API.GetGuiResources(_pHandle, 1);
            if (vv.Contains("GDIObjectsPeak")) GDIObjectsPeak = API.GetGuiResources(_pHandle, 2);
            if (vv.Contains("UserObjectsPeak")) UserObjectsPeak = API.GetGuiResources(_pHandle, 4);
            // Process Priority and Affinity
            if (vv.Contains("Priority")) Priority = ((ProcessPriorityClass)API.GetPriorityClass(_pHandle)).ToString();
            if (vv.Contains("Affinity")) {
                API.GetProcessAffinityMask(_pHandle, out nint _AffinityMask, out _);
                if (Convert.ToString(_AffinityMask.ToInt32(), 2).Equals(Shared.TotalProcessorsBin)) {
                    Affinity = "All";
                } else {
                    Affinity = (_AffinityMask == 0) ? "None" : GetAffinitiesString(_AffinityMask);
                }
            }
            API.CloseHandle(_pHandle);
        }
        // Process Times & CPU Usage
        if (vv.Contains("CpuUsage") || vv.Contains("CpuTime") || vv.Contains("UserTime") || vv.Contains("KernelTime") || vv.Contains("CreationTime") || vv.Contains("RunTime")) {
            if (_CreationTimeValue.Ticks == 0) CreationTimeValue = new TimeSpan(DateTime.FromFileTime(Convert.ToInt64(spi.CreateTime)).Ticks);
            RunTime = Shared.TimeDiff(CreationTimeValue.Ticks, 2);
            KernelTimeValue = new TimeSpan(Convert.ToInt64(spi.KernelTime));
            UserTimeValue = new TimeSpan(Convert.ToInt64(spi.UserTime));
            CpuTimeValue = KernelTimeValue + UserTimeValue;
            // CPU Usage
            // No need to get them again.  if (_PID == 0) API.GetSystemTimes(ref _CpuTimeValue, ref _KernelTimeValue, ref _KernelTimeValue);
            if (_prevCpuTimeValue.Ticks == 0) _prevCpuTimeValue = _CpuTimeValue;
            double _rawCpuUsage = (double)(_CpuTimeValue.Ticks - _prevCpuTimeValue.Ticks) * 100 / (LastUpdated - PreviousUpdate) / Environment.ProcessorCount;
            if (_rawCpuUsage > 99.9) _rawCpuUsage = 99.9;
            if (_rawCpuUsage < 0) _rawCpuUsage = 0;
            CpuUsage = _rawCpuUsage.ToString("00.0");
            _prevCpuTimeValue = _CpuTimeValue;
        }
        // Define whatever or not we should change the item color
        if (!IgnoreBackColor) {
            if (NotifyChanges && LastUpdated <= LastChanged) {
                if (Suspended && Settings.Highlights.FrozenItems) {
                    BackColor = Settings.Highlights.FrozenColor;
                } else {
                    BackColor = Settings.Highlights.ChangingItems ? Settings.Highlights.ChangingColor : Color.Empty;
                }
            } else if (Suspended) {
                if (Settings.Highlights.FrozenItems) BackColor = Settings.Highlights.FrozenColor;
            } else {
                BackColor = Color.Empty;
            }
        }
    }
    public void ForceRaiseChange(HashSet<string> visibleValues) {
        if (!NotifyChanges) return;
        foreach (string PropertyName in visibleValues) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
    public bool DumpFile(string dumpFilename) {
        try {
            FileStream ProcDumpFile = File.Open(dumpFilename, FileMode.OpenOrCreate);
            bool res = API.MiniDumpWriteDump(Process.GetProcessById(_PID).Handle, _PID, ProcDumpFile.SafeFileHandle.DangerousGetHandle(), API.DumpTypes.DumpWithFullMemory, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            ProcDumpFile.Close();
            return res;
        } catch {
            return false;
        }
    }
    public void Kill() {
        try {
            if (_PID < Shared.bpi) return;
            IntPtr pHandle = API.OpenProcess(API.ProcessAccessFlags.Terminate, false, _PID);
            if (pHandle != IntPtr.Zero) API.TerminateProcess(pHandle, 0);
            API.CloseHandle(pHandle);
        } catch {
            Debug.WriteLine("Failed To Kill Process PID " + PID);
        }
    }
    public void Pause() {
        if (_PID < Shared.bpi) return;
        if (Suspended) return;
        API.ThreadEntry32 uThread = new();
        uThread.dwSize = Marshal.SizeOf(uThread);
        IntPtr hSnapShot = API.CreateToolhelp32Snapshot(API.SnapshotFlags.Thread, (uint)_PID);
        if (API.Thread32First(hSnapShot, ref uThread)) {
            do {
                if (uThread.th32OwnerProcessID != _PID) continue;
                _ = API.SuspendThread(API.OpenThread(API.ThreadAccessFlags.SUSPEND_RESUME, false, uThread.th32ThreadID));
            } while (API.Thread32Next(hSnapShot, ref uThread));
        }
        API.CloseHandle(hSnapShot);
        Suspended = imSuspended(Process.GetProcessById(_PID).Threads);
    }
    public void Resume() {
        if (_PID < Shared.bpi) return;
        API.ThreadEntry32 uThread = new();
        uThread.dwSize = Marshal.SizeOf(uThread);
        IntPtr hSnapShot = API.CreateToolhelp32Snapshot(API.SnapshotFlags.Thread, (uint)_PID);
        if (API.Thread32First(hSnapShot, ref uThread)) {
            do {
                if (uThread.th32OwnerProcessID != _PID) continue;
                _ = API.ResumeThread(API.OpenThread(API.ThreadAccessFlags.SUSPEND_RESUME, false, uThread.th32ThreadID));
            } while (API.Thread32Next(hSnapShot, ref uThread));
        }
        API.CloseHandle(hSnapShot);
        Suspended = imSuspended(Process.GetProcessById(_PID).Threads);
    }
    public Icon? GetIcon() {
        if (Icon != null) return Icon;
        ImagePath ??= Process.GetProcessById(_PID)!.MainModule!.FileName;
        if (File.Exists(ImagePath)) {
            IntPtr[] IconPtr = new IntPtr[1];
            if (API.ExtractIconEx(ImagePath, 0, null, IconPtr, 1) > 0) {
                Icon = (Icon?)Icon.FromHandle(IconPtr[0]).Clone();
                API.DestroyIcon(IconPtr[0]);
            }
        }
        return Icon;
    }

    /* Private Methods */
    private void MyPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        if (!NotifyChanges) return;
        if (e.PropertyName == null) return;
        if (e.PropertyName.Equals("LastChanged")) return;
        if (e.PropertyName.Equals("LastUpdated")) return;
        if (e.PropertyName.Equals("PreviousUpdate")) return;
        if (e.PropertyName.Equals("BackColor")) return;
        if (e.PropertyName.Equals("RunTime")) return;
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
    private string FormatValue(ulong value, FormatValueTypes format, bool skipBPI = false) {
        if (skipBPI && _PID < Shared.bpi) { return "n/a."; }
        switch (format) {
            case FormatValueTypes.Literal: return value.ToString();
            case FormatValueTypes.Number: return string.Format("{0:#,0}", value);
            case FormatValueTypes.Bytes: return string.Format("{0:#,0}", value) + " B";
            case FormatValueTypes.KBytes: return string.Format("{0:#,0}", value / 1024d) + " K";
            case FormatValueTypes.MBytes: return string.Format("{0:#,0}", value / 1024d / 1024d) + " M";
            case FormatValueTypes.Kbps: return string.Format("{0:#,0.0}", value / 1024d) + " Kb/s";
            case FormatValueTypes.AutoBytes:
                if (value < 10240) {
                    return string.Format("{0:#,0}", value) + " B";
                } else if (value < 1024000) {
                    return string.Format("{0:#,0.0}", value / 1024d) + " K";
                } else {
                    return string.Format("{0:#,0.0}", (value / 1024d / 1024d)) + " M";
                }
            default: return value.ToString();
        }

    }
    private enum FormatValueTypes {
        Literal = 0,
        Number = 1,
        Bytes = 11,
        KBytes = 12,
        MBytes = 13,
        AutoBytes = 15,
        Kbps = 22,
    }
    private static bool imSuspended(in ProcessThreadCollection Threads) {
        foreach (ProcessThread t in Threads) {
            if (t.ThreadState != System.Diagnostics.ThreadState.Wait) return false;
            if (t.ThreadState == System.Diagnostics.ThreadState.Wait && t.WaitReason != ThreadWaitReason.Suspended) return false;
        }
        return true;
    }
    private static bool imSuspended(in API.SYSTEM_EXTENDED_THREAD_INFORMATION[] Threads) {
        foreach (API.SYSTEM_EXTENDED_THREAD_INFORMATION t in Threads) {
            if (t.ThreadInfo.State != 5) return false;
            if (t.ThreadInfo.State == 5 && t.ThreadInfo.WaitReason != 5) return false;
        }
        return true;
    }
    private ulong CalculateRateValue(in ulong DeltaValue) {
        if (DeltaValue == 0) return 0;
        if ((LastUpdated - PreviousUpdate) <= 0) return DeltaValue;
        return (ulong)Math.Round(DeltaValue / ((double)(LastUpdated - PreviousUpdate) / TimeSpan.TicksPerSecond));
    }

    /* Public Static Methods */
    public static void GetSpecificSPI(int PID, out API.SYSTEM_PROCESS_INFORMATION spi) {
        spi = new();
        IntPtr hmain = IntPtr.Zero;
        if (GetProcessesPointer(ref hmain)) {
            spi = (API.SYSTEM_PROCESS_INFORMATION)Marshal.PtrToStructure(hmain, typeof(API.SYSTEM_PROCESS_INFORMATION))!;
            IntPtr lastOffset = hmain;
            while (spi.NextEntryOffset >= 0) {
                if (spi.UniqueProcessId == PID) {
                    Array.Resize(ref spi.Threads, (int)spi.NumberOfThreads);
                    break;
                }
                if (spi.NextEntryOffset > 0) {
                    lastOffset += (IntPtr)spi.NextEntryOffset;
                    spi = (API.SYSTEM_PROCESS_INFORMATION)Marshal.PtrToStructure(lastOffset, typeof(API.SYSTEM_PROCESS_INFORMATION))!;
                } else {
                    break;
                }
            }
            Marshal.FreeHGlobal(hmain);
        }
    }
    public static bool GetProcessesPointer(ref IntPtr Handle) {
        int hMainInfoSize = 1000000;
        Handle = Marshal.AllocHGlobal(hMainInfoSize);
        API.NTSTATUS NTstatus = API.NtQuerySystemInformation(API.SYSTEM_INFORMATION_CLASS.SystemExtendedProcessInformation, Handle, hMainInfoSize, out int nLength);
        while (NTstatus == API.NTSTATUS.InfoLengthMismatch) { // STATUS_INFO_LENGTH_MISMATCH
            hMainInfoSize = nLength;
            Marshal.FreeHGlobal(Handle);
            Handle = Marshal.AllocHGlobal(nLength);
            NTstatus = API.NtQuerySystemInformation(API.SYSTEM_INFORMATION_CLASS.SystemExtendedProcessInformation, Handle, hMainInfoSize, out nLength);
        }
        return (NTstatus == 0);
    }
    public static string GetAffinitiesString(IntPtr bitMask) {
        // Example: 111001 return 1,2,3,6 - the position of the numbers 1
        StringBuilder m_StringBuilder = new();
        string binMask = Convert.ToString(bitMask.ToInt32(), 2);
        for (int i = 0; i < binMask.Length; i++) {
            if (binMask[i] == '1') m_StringBuilder.Append((i + 1) + ",");
        }
        return m_StringBuilder.ToString().Trim(',').Trim();
    }

}

[SupportedOSPlatform("windows")]
internal class TaskManagerProcessCollection : BindingList<TaskManagerProcess> {
    public BindingSource DataExporter { get; init; }
    public TaskManagerProcessCollection() {
        DataExporter = new(this, null) { RaiseListChangedEvents = true };
        AllowRemove = true;
        RaiseListChangedEvents = true;
    }

    public bool Contains(IntPtr PID) => Contains(PID.ToInt32());
    public bool Contains(int PID) {
        foreach (TaskManagerProcess i in Items) {
            if (i.PID == PID) return true;
        }
        return false;
    }

    public bool Contains(IntPtr PID, ref TaskManagerProcess theProcess) => Contains(PID.ToInt32(), ref theProcess);
    public bool Contains(int PID, ref TaskManagerProcess theProcess) {
        foreach (TaskManagerProcess i in Items) {
            if (i.PID == PID) { theProcess = i; return true; }
        }
        return false;
    }

    public TaskManagerProcess? GetProcess(IntPtr PID) => GetProcess(PID.ToInt32());
    public TaskManagerProcess? GetProcess(int PID) {
        foreach (TaskManagerProcess i in Items) {
            if (i.PID == PID) return i;
        }
        return null;
    }

}
