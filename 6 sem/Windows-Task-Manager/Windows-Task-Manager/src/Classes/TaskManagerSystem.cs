using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.ServiceProcess;
namespace sMkTaskManager.Classes;

[SupportedOSPlatform("windows")]
internal class TaskManagerSystem : TaskManagerValuesBase {
    private API.SYSTEM_PERFORMANCE_INFORMATION _SPI = new();
    private API.PERFORMANCE_INFORMATION _PI = new();
    private readonly uint Multiplier = 4096U;
    private ulong _sumPageFileTotal, _sumPageFileUsed, _sumPageFilePeak;
    private readonly CpuUsage _Cpu = new();
    private TimeSpan _UpTime;

    public TaskManagerSystem() {
        _PI.cb = (uint)Marshal.SizeOf(_PI);
    }

    public event EventHandler? RefreshStarting;
    public event EventHandler? RefreshCompleted;

    public void Refresh(bool cancellingEvents = false) {
        CancellingEvents = cancellingEvents;
        if (LastUpdate == 0) { LastUpdate = DateTime.Now.Ticks - 10; }
        if (!CancellingEvents) RefreshStarting?.Invoke(this, new());

        // Compute System Uptime
        if (Environment.TickCount > 0) {
            _UpTime = TimeSpan.FromTicks(Environment.TickCount * 10000L);
        } else {
            _UpTime = TimeSpan.FromTicks((int.MaxValue * 10000L) + ((Environment.TickCount & int.MaxValue) * 10000L));
        }

        // Compute Functions Invokes
        if (API.NtQuerySystemInformation(API.SYSTEM_INFORMATION_CLASS.SystemPerformanceInformation, ref _SPI, Marshal.SizeOf(_SPI), out _) == 0) {
            ioReadCount.SetValue(_SPI.IoReadOperationCount);
            ioWriteCount.SetValue(_SPI.IoWriteOperationCount);
            ioOtherCount.SetValue(_SPI.IoOtherOperationCount);
            ioReadBytes.SetValue(_SPI.IoReadTransferCount);
            ioWriteBytes.SetValue(_SPI.IoWriteTransferCount);
            ioOtherBytes.SetValue(_SPI.IoOtherTransferCount);
            SystemCached.SetValue(_SPI.ResidentSystemCachePage * Multiplier);
            ioTotalCount.SetValue(ioReadCount.Value + ioWriteCount.Value + ioOtherCount.Value);
            ioTotalBytes.SetValue(ioReadBytes.Value + ioWriteBytes.Value + ioOtherBytes.Value);

        } else { Debug.WriteLine(Marshal.GetLastPInvokeErrorMessage()); }
        if (API.GetPerformanceInfo(ref _PI, _PI.cb)) {
            HandleCount.SetValue(_PI.HandleCount);
            ThreadCount.SetValue(_PI.ThreadCount);
            ProcessCount.SetValue(_PI.ProcessCount);
            KernelTotal.SetValue(_PI.KernelTotal * Multiplier);
            KernelPaged.SetValue(_PI.KernelPaged * Multiplier);
            KernelNonPaged.SetValue(_PI.KernelNonPaged * Multiplier);
            CommitTotal.SetValue(_PI.CommitTotal * Multiplier);
            CommitLimit.SetValue(_PI.CommitLimit * Multiplier);
            CommitPeak.SetValue(_PI.CommitPeak * Multiplier);
            PhysicalTotal.SetValue(_PI.PhysicalTotal * Multiplier);
            PhysicalAvail.SetValue(_PI.PhysicalAvailable * Multiplier);

        } else { Debug.WriteLine(Marshal.GetLastPInvokeErrorMessage()); }

        // Compute PageFile Data
        ResetPageFile();
        API.EnumPageFiles(EnumPageFileCallback, IntPtr.Zero);
        PageFileTotal.SetValue(_sumPageFileTotal);
        PageFileUsed.SetValue(_sumPageFileUsed);
        PageFilePeak.SetValue(_sumPageFilePeak);

        // Get Devices & Services Count - This adds too much overhead, either with API or NATIVE calls, so we just get it once
        if (DevicesCount.Value == 0) DevicesCount.SetValue(ServiceController.GetDevices().Length);
        if (ServicesCount.Value == 0) ServicesCount.SetValue(ServiceController.GetServices().Length);

        // Get CPU Usage
        _Cpu.Refresh(LastUpdate);
        CpuUsage.SetValue(_Cpu.Usage);
        CpuUsageUser.SetValue(_Cpu.UserUsage);
        CpuUsageKernel.SetValue(_Cpu.KernelUsage);

        // Compute ETW Usages
        if (Shared.ETW.Running) {
            Shared.ETW.Flush();
            DiskRead.SetValue(Shared.ETW.Stats(0).DiskReaded);
            DiskWrite.SetValue(Shared.ETW.Stats(0).DiskWroted);
            NetSent.SetValue(Shared.ETW.Stats(0).NetSent);
            NetReceived.SetValue(Shared.ETW.Stats(0).NetReceived);
        }

        LastUpdate = DateTime.Now.Ticks;
        if (!CancellingEvents) RefreshCompleted?.Invoke(this, new());
        CancellingEvents = false;
    }

    private bool EnumPageFileCallback(IntPtr lpContext, ref API.PAGE_FILE_INFORMATION Info, string Name) {
        _sumPageFileTotal += Info.TotalSize * 4096UL;
        _sumPageFileUsed += Info.TotalInUse * 4096UL;
        _sumPageFilePeak += Info.PeakUsage * 4096UL;
        return true;
    }
    private void ResetPageFile() {
        _sumPageFileTotal = 0;
        _sumPageFileUsed = 0;
        _sumPageFilePeak = 0;
    }

    public string UpTime => string.Format("{0}d {1,2:D2}:{2,2:D2}:{3,2:D2}", _UpTime.Days, _UpTime.Hours, _UpTime.Minutes, _UpTime.Seconds);
    public int MemoryUsage {
        get {
            if (PhysicalTotal.Value - PhysicalAvail.Value <= 0) {
                return 100;
            } else {
                return (int)(100 - ((100 * PhysicalAvail.Value) / PhysicalTotal.Value));
            }
        }
    }
    public int SwapUsage {
        get {
            if (PageFileUsed.Value >= PageFileTotal.Value) {
                return 100;
            } else {
                return (int)(100 * PageFileUsed.Value / PageFileTotal.Value);
            }
        }
    }
    public string MemoryUsageString {
        get {
            if (PhysicalTotal.Value - PhysicalAvail.Value == 0) { return "Full"; }
            if (((PhysicalTotal.Value - PhysicalAvail.Value) / 1024 / 1024) < 2000) { // Less than 2Gb
                return string.Format("{0:#} Mb.", (double)(PhysicalTotal.Value - PhysicalAvail.Value) / 1024 / 1024);
            } else {
                return string.Format("{0:#.00} Gb.", (double)(PhysicalTotal.Value - PhysicalAvail.Value) / 1024 / 1024 / 1024);
            }
        }
    }
    public Int128 ioDataUsage => ioReadBytes.Delta + ioWriteBytes.Delta + ioOtherBytes.Delta;
    public string ioDataUsageString {
        get {
            if (ioDataUsage == 0) {
                return "Idle";
            } else if (ioDataUsage < 2048) {
                return string.Format("{0:#,0} b.", ioDataUsage);
            } else if (ioDataUsage < (1024 * 1024)) {
                return string.Format("{0:#,0} Kb.", (double)(ioDataUsage / 1024));
            } else {
                return string.Format("{0:#.0} Mb.", (double)ioDataUsage / 1024 / 1024);
            }
        }
    }

    public Int128 DiskUsage => DiskRead.Delta + DiskWrite.Delta;
    public string DiskUsageString {
        get {
            if (DiskUsage == 0) {
                return "Idle";
            } else if (DiskUsage < 2048) {
                return string.Format("{0:#,0} b.", DiskUsage);
            } else if (DiskUsage < (1024 * 1024)) {
                return string.Format("{0:#,0} Kb.", (double)DiskUsage / 1024);
            } else {
                return string.Format("{0:#.0} Mb.", (double)DiskUsage / 1024 / 1024);
            }
        }
    }
    public Int128 NetworkUsage => NetSent.Delta + NetReceived.Delta;
    public string NetworkUsageString {
        get {
            if (NetworkUsage == 0) {
                return "Idle";
            } else if (NetworkUsage < 2048) {
                return string.Format("{0:#,0} b.", NetworkUsage);
            } else if (NetworkUsage < (1024 * 1024)) {
                return string.Format("{0:#,0} Kb.", (double)NetworkUsage / 1024);
            } else {
                return string.Format("{0:#.0} Mb.", (double)NetworkUsage / 1024 / 1024);
            }
        }
    }

    // Disk Stats
    public Metric DiskRead { get; private set; } = new("DiskRead", MetricFormats.Kb);
    public Metric DiskWrite { get; private set; } = new("DiskWrite", MetricFormats.Kb);
    // Net Stats
    public Metric NetSent { get; private set; } = new("NetSent", MetricFormats.Kb);
    public Metric NetReceived { get; private set; } = new("NetReceived", MetricFormats.Kb);
    // io Stats
    public Metric ioReadCount { get; private set; } = new("ioReadCount", MetricFormats.Numeric);
    public Metric ioReadBytes { get; private set; } = new("ioReadBytes", MetricFormats.Kb);
    public Metric ioWriteCount { get; private set; } = new("ioWriteCount", MetricFormats.Numeric);
    public Metric ioWriteBytes { get; private set; } = new("ioWriteBytes", MetricFormats.Kb);
    public Metric ioOtherCount { get; private set; } = new("ioOtherCount", MetricFormats.Numeric);
    public Metric ioOtherBytes { get; private set; } = new("ioOtherBytes", MetricFormats.Kb);
    // io Calculated 
    public Metric ioUsageCount { get; private set; } = new("ioUsageCount", MetricFormats.Numeric);
    public Metric ioUsageBytes { get; private set; } = new("ioUsageBytes", MetricFormats.Kb);
    public Metric ioTotalCount { get; private set; } = new("ioTotalCount", MetricFormats.Numeric);
    public Metric ioTotalBytes { get; private set; } = new("ioTotalBytes", MetricFormats.Kb);

    // Commit Memory Stats
    public Metric CommitTotal { get; private set; } = new("CommitTotal", MetricFormats.Kb);
    public Metric CommitLimit { get; private set; } = new("CommitLimit", MetricFormats.Kb);
    public Metric CommitPeak { get; private set; } = new("CommitPeak", MetricFormats.Kb);
    // Physical Memory Stats
    public Metric PhysicalTotal { get; private set; } = new("PhysicalTotal", MetricFormats.Kb);
    public Metric PhysicalAvail { get; private set; } = new("PhysicalAvail", MetricFormats.Kb);
    public Metric SystemCached { get; private set; } = new("SystemCached", MetricFormats.Kb);
    // Kernel Memory Stats
    public Metric KernelTotal { get; private set; } = new("KernelTotal", MetricFormats.Kb);
    public Metric KernelPaged { get; private set; } = new("KernelPaged", MetricFormats.Kb);
    public Metric KernelNonPaged { get; private set; } = new("KernelNonPaged", MetricFormats.Kb);
    // Page File Stats
    public Metric PageFileTotal { get; private set; } = new("PageFileTotal", MetricFormats.Kb);
    public Metric PageFileUsed { get; private set; } = new("PageFileUsed", MetricFormats.Kb);
    public Metric PageFilePeak { get; private set; } = new("PageFilePeak", MetricFormats.Kb);
    // CPU Usage Stats
    public Metric CpuUsage { get; private set; } = new("CpuUsage", MetricFormats.None);
    public Metric CpuUsageUser { get; private set; } = new("CpuUsageUser", MetricFormats.None);
    public Metric CpuUsageKernel { get; private set; } = new("CpuUsageKernel", MetricFormats.None);
    // System Counts Stats
    public Metric HandleCount { get; private set; } = new("HandleCount");
    public Metric ThreadCount { get; private set; } = new("ThreadCount");
    public Metric ProcessCount { get; private set; } = new("ProcessCount");
    public Metric DevicesCount { get; private set; } = new("DevicesCount");
    public Metric ServicesCount { get; private set; } = new("ServicesCount");
}

internal class TaskManagerSystemValuesOld : INotifyPropertyChanged {

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) { PropertyChanged?.Invoke(this, e); }
    protected void SetField<T>(ref T field, T newValue, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = "") {
        if (!EqualityComparer<T>.Default.Equals(field, newValue)) {
            field = newValue;
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
    protected void SetField(ref Int128 field, ref Int128 fieldDelta, Int128 newValue, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = "") {
        if (!fieldDelta.Equals(newValue - field)) {
            fieldDelta = newValue - field;
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName + "Delta"));
        }
        if (!field.Equals(newValue)) {
            field = newValue;
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    // Test
    private Int128 _Test, _TestDelta;
    public Int128 Test { get => _Test; set => SetField(ref _Test, ref _TestDelta, value); }
    public string TestFmt => string.Format("{0:#,0} Kb.", _Test / 1024);
    public Int128 TestDelta => _TestDelta;
    public string TestDeltaFmt => string.Format("{0:#,0} Kb.", TestDelta / 1024);

    // Disk Stats
    private Int128 _DiskRead, _DiskWrite;
    public Int128 DiskRead { get => _DiskRead; set => SetField(ref _DiskRead, value); }
    public string DiskReadFmt => string.Format("{0:#,0} Kb.", _DiskRead / 1024);
    public Int128 DiskWrite { get => _DiskWrite; set => SetField(ref _DiskWrite, value); }
    public string DiskWriteFmt => string.Format("{0:#,0} Kb.", _DiskWrite / 1024);
    // Net Stats
    private Int128 _NetSent, _NetReceived;
    public Int128 NetSent { get => _NetSent; set => SetField(ref _NetSent, value); }
    public string NetSentFmt => string.Format("{0:#,0} Kb.", _NetSent / 1024);
    public Int128 NetReceived { get => _NetReceived; set => SetField(ref _NetReceived, value); }
    public string NetReceivedFmt => string.Format("{0:#,0} Kb.", _NetReceived / 1024);
    // io Stats
    private Int128 _ioReadCount, _ioWriteCount, _ioOtherCount;
    public Int128 ioReadCount { get => _ioReadCount; set => SetField(ref _ioReadCount, value); }
    public string ioReadCountFmt => string.Format("{0:#,0}", _ioReadCount / 1024);
    public Int128 ioWriteCount { get => _ioWriteCount; set => SetField(ref _ioWriteCount, value); }
    public string ioWriteCountFmt => string.Format("{0:#,0}", _ioWriteCount / 1024);
    public Int128 ioOtherCount { get => _ioOtherCount; set => SetField(ref _ioOtherCount, value); }
    public string ioOtherCountFmt => string.Format("{0:#,0}", _ioOtherCount / 1024);
    private Int128 _ioReadBytes, _ioWriteBytes, _ioOtherBytes;
    public Int128 ioReadBytes { get => _ioReadBytes; set => SetField(ref _ioReadBytes, value); }
    public string ioReadBytesFmt => string.Format("{0:#,0} Kb.", _ioReadBytes / 1024);
    public Int128 ioWriteBytes { get => _ioWriteBytes; set => SetField(ref _ioWriteBytes, value); }
    public string ioWriteBytesFmt => string.Format("{0:#,0} Kb.", _ioWriteBytes / 1024);
    public Int128 ioOtherBytes { get => _ioOtherBytes; set => SetField(ref _ioOtherBytes, value); }
    public string ioOtherBytesFmt => string.Format("{0:#,0} Kb.", _ioOtherBytes / 1024);
    // Commit Memory Stats
    private Int128 _CommitTotal, _CommitLimit, _CommitPeak;
    public Int128 CommitTotal { get => _CommitTotal; set => SetField(ref _CommitTotal, value); }
    public string CommitTotalFmt => string.Format("{0:#,0} Kb.", _CommitTotal / 1024);
    public Int128 CommitLimit { get => _CommitLimit; set => SetField(ref _CommitLimit, value); }
    public string CommitLimitFmt => string.Format("{0:#,0} Kb.", _CommitLimit / 1024);
    public Int128 CommitPeak { get => _CommitPeak; set => SetField(ref _CommitPeak, value); }
    public string CommitPeakFmt => string.Format("{0:#,0} Kb.", _CommitPeak / 1024);
    // Physical Memory Stats
    private Int128 _PhysicalTotal, _PhysicalAvail, _SystemCached;
    public Int128 PhysicalTotal { get => _PhysicalTotal; set => SetField(ref _PhysicalTotal, value); }
    public string PhysicalTotalFmt => string.Format("{0:#,0} Kb.", _PhysicalTotal / 1024);
    public Int128 PhysicalAvail { get => _PhysicalAvail; set => SetField(ref _PhysicalAvail, value); }
    public string PhysicalAvailFmt => string.Format("{0:#,0} Kb.", _PhysicalAvail / 1024);
    public Int128 SystemCached { get => _SystemCached; set => SetField(ref _SystemCached, value); }
    public string SystemCachedFmt => string.Format("{0:#,0} Kb.", _SystemCached / 1024);
    // Kernel Memory Stats
    private Int128 _KernelTotal, _KernelPaged, _KernelNonPaged;
    public Int128 KernelTotal { get => _KernelTotal; set => SetField(ref _KernelTotal, value); }
    public string KernelTotalFmt => string.Format("{0:#,0} Kb.", _KernelTotal / 1024);
    public Int128 KernelPaged { get => _KernelPaged; set => SetField(ref _KernelPaged, value); }
    public string KernelPagedFmt => string.Format("{0:#,0} Kb.", _KernelPaged / 1024);
    public Int128 KernelNonPaged { get => _KernelNonPaged; set => SetField(ref _KernelNonPaged, value); }
    public string KernelNonPagedFmt => string.Format("{0:#,0} Kb.", _KernelNonPaged / 1024);
    // Page File Stats
    private Int128 _PageFileTotal, _PageFileUsed, _PageFilePeak;
    public Int128 PageFileTotal { get => _PageFileTotal; set => SetField(ref _PageFileTotal, value); }
    public string PageFileTotalFmt => string.Format("{0:#,0} Kb.", _PageFileTotal / 1024);
    public Int128 PageFileUsed { get => _PageFileUsed; set => SetField(ref _PageFileUsed, value); }
    public string PageFileUsedFmt => string.Format("{0:#,0} Kb.", _PageFileUsed / 1024);
    public Int128 PageFilePeak { get => _PageFilePeak; set => SetField(ref _PageFilePeak, value); }
    public string PageFilePeakFmt => string.Format("{0:#,0} Kb.", _PageFilePeak / 1024);
    // CPU Usage Stats
    private Int128 _CpuUsage, _CpuUsageUser, _CpuUsageKernel;
    public Int128 CpuUsage { get => _CpuUsage; set => SetField(ref _CpuUsage, value); }
    public string CpuUsageFmt => string.Format("{0:#,0} Kb.", _CpuUsage / 1024);
    public Int128 CpuUsageUser { get => _CpuUsageUser; set => SetField(ref _CpuUsageUser, value); }
    public string CpuUsageUserFmt => string.Format("{0:#,0} Kb.", _CpuUsageUser / 1024);
    public Int128 CpuUsageKernel { get => _CpuUsageKernel; set => SetField(ref _CpuUsageKernel, value); }
    public string CpuUsageKernelFmt => string.Format("{0:#,0} Kb.", _CpuUsageKernel / 1024);
    // System Counts Stats
    private Int128 _HandleCount, _ThreadCount, _ProcessCount, _DevicesCount, _ServicesCount;
    public Int128 HandleCount { get => _HandleCount; set => SetField(ref _HandleCount, value); }
    public string HandleCountFmt => _HandleCount.ToString();
    public Int128 ThreadCount { get => _ThreadCount; set => SetField(ref _ThreadCount, value); }
    public string ThreadCountFmt => _ThreadCount.ToString();
    public Int128 ProcessCount { get => _ProcessCount; set => SetField(ref _ProcessCount, value); }
    public string ProcessCountFmt => _ProcessCount.ToString();
    public Int128 DevicesCount { get => _DevicesCount; set => SetField(ref _DevicesCount, value); }
    public string DevicesCountFmt => _DevicesCount.ToString();
    public Int128 ServicesCount { get => _ServicesCount; set => SetField(ref _ServicesCount, value); }
    public string ServicesCountFmt => _ServicesCount.ToString();

    public void ResetPageFile() {
        PageFileTotal = 0;
        PageFileUsed = 0;
        PageFilePeak = 0;
    }

}

