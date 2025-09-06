using System.Diagnostics;
using System.Runtime.InteropServices;
namespace sMkTaskManager.Classes;

internal static class ETW {

    [StructLayout(LayoutKind.Sequential)]
    private struct WNODE_HEADER {
        public uint BufferSize;
        public uint ProviderId;
        public ulong HistoricalContext;
        public IntPtr TimeStamp;
        public Guid Guid;
        public uint ClientContext;
        public uint Flags;
    }
    [StructLayout(LayoutKind.Sequential)]
    private struct EVENT_RECORD {
        public EVENT_HEADER EventHeader;
        public ETW_BUFFER_CONTEXT BufferContext;
        public ushort ExtendedDataCount;
        public ushort UserDataLength;
        public IntPtr ExtendedData;
        public IntPtr UserData;
        public IntPtr UserContext;
    }
    [StructLayout(LayoutKind.Sequential)]
    private struct EVENT_HEADER {
        public ushort Size;
        public ushort HeaderType;
        public ushort Flags;
        public ushort EventProperty;
        public uint ThreadId;
        public uint ProcessId;
        public long TimeStamp;
        public Guid ProviderId;
        public ushort Id;
        public byte Version;
        public byte Channel;
        public byte Level;
        public byte Opcode;
        public ushort Task;
        public ulong Keyword;
        public uint KernelTime;
        public uint UserTime;
        public Guid ActivityId;
    }
    [StructLayout(LayoutKind.Sequential)]
    private struct EVENT_TRACE {
        public EVENT_TRACE_HEADER Header;
        public uint InstanceId;
        public uint ParentInstanceId;
        public Guid ParentGuid;
        public IntPtr MofData;
        public int MofLength;
        public ETW_BUFFER_CONTEXT BufferContext;
    }
    [StructLayout(LayoutKind.Sequential)]
    private struct EVENT_TRACE_HEADER {
        public ushort Size;
        public ushort FieldTypeFlags;
        public byte Type;
        public byte Level;
        public ushort Version;
        public uint ThreadId;
        public uint ProcessId;
        public long TimeStamp;
        public Guid Guid;
        public uint KernelTime;
        public uint UserTime;
    }
    [StructLayout(LayoutKind.Sequential)]
    private struct EVENT_TRACE_PROPERTIES {
        public WNODE_HEADER Wnode;
        public uint BufferSize;
        public uint MinimumBuffers;
        public uint MaximumBuffers;
        public uint MaximumFileSize;
        public uint LogFileMode;
        public uint FlushTimer;
        public uint EnableFlags;
        public int AgeLimit;
        public UInt32 NumberOfBuffers;
        public uint FreeBuffers;
        public uint EventsLost;
        public uint BuffersWritten;
        public uint LogBuffersLost;
        public uint RealTimeBuffersLost;
        public IntPtr LoggerThreadId;
        public uint LogFileNameOffset;
        public uint LoggerNameOffset;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct EVENT_TRACE_LOGFILE {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string LogFileName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string LoggerName;
        public Int64 CurrentTime;
        public uint BuffersRead;
        public uint LogFileMode;
        public EVENT_TRACE CurrentEvent; // EVENT_TRACE for the current event. Nulled-out when we are opening files. [FieldOffset(0x18)]
        public TRACE_LOGFILE_HEADER LogfileHeader; // [FieldOffset(0x70)]
        public EventTraceBufferCallback BufferCallback; // callback before each buffer is read [FieldOffset(0x180)]
        public Int32 BufferSize;
        public Int32 Filled;
        public Int32 EventsLost;
        public EventTraceEventCallback EventCallback; // callback for every 'event', each line of the trace moduleFile [FieldOffset(0x190)]
        public Int32 IsKernelTrace; // TRUE for kernel logfile
        public IntPtr Context; // reserved for internal use
    }
    [StructLayout(LayoutKind.Explicit)]
    private struct EVENT_FILTER_DESCRIPTOR {
        [FieldOffset(0)] public byte Ptr;
        [FieldOffset(8)] public int Size;
        [FieldOffset(12)] public int Type;
    }
    [StructLayout(LayoutKind.Sequential)]
    private struct ETW_BUFFER_CONTEXT {
        public byte ProcessorNumber;
        public byte Alignment;
        public ushort LoggerId;
    }
    [StructLayout(LayoutKind.Sequential)]
    private struct TRACE_LOGFILE_HEADER {
        public uint BufferSize;
        public uint Version;
        public uint ProviderVersion;
        public uint NumberOfProcessors;
        public long EndTime;
        public uint TimerResolution;
        public uint MaximumFileSize;
        public uint LogFileMode;
        public uint BuffersWritten;
        public uint StartBuffers;
        public uint PointerSize;
        public uint EventsLost;
        public uint CpuSpeedInMHz;
        public IntPtr LoggerName;
        public IntPtr LogFileName;
        public TIME_ZONE_INFORMATION TimeZone;
        public long BootTime;
        public long PerfFreq;
        public long StartTime;
        public uint ReservedFlags;
        public uint BuffersLost;
    }
    [StructLayout(LayoutKind.Sequential, Size = 0xAC, CharSet = CharSet.Unicode)]
    private struct TIME_ZONE_INFORMATION {
        public uint bias;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string standardName;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U2, SizeConst = 8)]
        public UInt16[] standardDate;
        public uint standardBias;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string daylightName;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U2, SizeConst = 8)]
        public UInt16[] daylightDate;
        public uint daylightBias;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct DiskIo_TypeGroup1 {
        public uint DiskNumber;
        public uint IrpFlags;
        public uint TransferSize;
        public uint ResponseTime;
        public ulong ByteOffset;
        public IntPtr FileObject;
        public IntPtr Irp;
        public ulong HighResResponseTime;
        public uint IssuingThreadId;
    }
    [StructLayout(LayoutKind.Sequential)]
    private struct TcpIpOrUdpIp_IPV4_Header {
        public uint PID;
        public uint size;
        public uint daddr;
        public uint saddr;
        public ushort dport;
        public ushort sport;
    }
    [StructLayout(LayoutKind.Sequential)]
    private struct TcpIpOrUdpIp_IPV6_Header {
        public uint PID;
        public uint size;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] public byte[] daddr;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] public byte[] saddr;
        public ushort dport;
        public ushort sport;
    }

    [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern uint StartTrace(ref UIntPtr TraceHandle, string InstanceName, EVENT_TRACE_PROPERTIES Properties);
    [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern UIntPtr OpenTrace(ref EVENT_TRACE_LOGFILE Logfile);
    [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern uint ControlTrace(UIntPtr TraceHandle, string InstanceName, EVENT_TRACE_PROPERTIES Properties, uint ControlCode);
    [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern uint ProcessTrace(UIntPtr[] HandleArray, uint HandleCount, IntPtr StartTime, IntPtr EndTime);
    [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern uint CloseTrace(UIntPtr TraceHandle);

    private delegate void EventTraceEventCallback(EVENT_TRACE rawData);
    private delegate bool EventTraceBufferCallback(EVENT_TRACE_LOGFILE Logfile);

    private const string KERNEL_LOGGER_NAME = "NT Kernel Logger";
    private const int WNODE_FLAG_TRACED_GUID = 0x20000;
    private const int EVENT_TRACE_REAL_TIME_MODE = 0x100;
    private const int EVENT_TRACE_CONTROL_STOP = 1;
    private const int EVENT_TRACE_CONTROL_UPDATE = 2;
    private const int EVENT_TRACE_CONTROL_FLUSH = 3;
    private const int EVENT_TRACE_FLAG_DISK_IO = 0x100;
    private const int EVENT_TRACE_FLAG_DISK_FILE_IO = 0x200;
    private const int EVENT_TRACE_FLAG_NETWORK_TCPIP = 0x10000;
    private const int EVENT_TRACE_TYPE_IO_READ = 0xA;
    private const int EVENT_TRACE_TYPE_IO_WRITE = 0xB;
    private const int EVENT_TRACE_TYPE_IO_FLUSH = 0xE;
    private const int EVENT_TRACE_TYPE_SEND = 0xA;
    private const int EVENT_TRACE_TYPE_RECEIVE = 0xB;
    private const int EVENT_TRACE_TYPE_SEND_IPV6 = 0xA+16;
    private const int EVENT_TRACE_TYPE_RECEIVE_IPV6 = 0xB+16;

    private static readonly Guid SystemTraceControlGuid = Guid.Parse("{9e814aad-3204-11d2-9a82-006008a86939}");
    private static readonly Guid KernelRundownGuid = Guid.Parse("{3b9c9951-3480-4220-9377-9c8e5184f5cd}");
    private static readonly Guid DiskIoGuid = Guid.Parse("{3d6fa8d4-fe05-11d0-9dda-00c04fd7ba7c}");
    private static readonly Guid FileIoGuid = Guid.Parse("{90cbdc39-4a3e-11d1-84f4-0000f80464e3}");
    private static readonly Guid TcpIpGuid = Guid.Parse("{9a280ac0-c8e0-11d1-84e2-00c04fb998a2}");
    private static readonly Guid UdpIpGuid = Guid.Parse("{bf3a50c5-a9c9-4988-a005-2df0b7c80f80}");

    private static UIntPtr EtwHandle;
    private static bool EtwActive;
    private static Thread? EtwThread;
    private static EVENT_TRACE_PROPERTIES EtwTraceProperties = new();
    private static readonly Dictionary<uint, EtwStats> _AllStats = new() { { 0, new() } };
    private static readonly Dictionary<string, EtwNetStats> _NetStats = new();

    private static bool EtwStartSession() {
        if (EtwActive) return true;
        EtwTraceProperties.Wnode.BufferSize = Convert.ToUInt32(Marshal.SizeOf(EtwTraceProperties) + (KERNEL_LOGGER_NAME.Length * 2) + 2);
        EtwTraceProperties.Wnode.Guid = SystemTraceControlGuid;
        EtwTraceProperties.Wnode.ClientContext = 1;
        EtwTraceProperties.Wnode.Flags = WNODE_FLAG_TRACED_GUID;
        EtwTraceProperties.LogFileMode = EVENT_TRACE_REAL_TIME_MODE;
        EtwTraceProperties.EnableFlags = EVENT_TRACE_FLAG_DISK_IO | EVENT_TRACE_FLAG_NETWORK_TCPIP;
        EtwTraceProperties.LogFileNameOffset = 0;
        EtwTraceProperties.LoggerNameOffset = Convert.ToUInt32(Marshal.SizeOf(EtwTraceProperties));
        EtwTraceProperties.FlushTimer = 1;
        EtwTraceProperties.MinimumBuffers = 1;
        EtwHandle = 0;
        var result = StartTrace(ref EtwHandle, KERNEL_LOGGER_NAME, EtwTraceProperties);
        Debug.WriteLine("* ETW StartTrace Handle: {0} Result: {1}.", EtwHandle, result);
        EtwActive = (result == 0 || result == 183);
        return EtwActive;
    }
    private static bool EtwStopSession() {
        if (!EtwActive) return true;
        EtwTraceProperties.LogFileNameOffset = 0;
        var result = ControlTrace(EtwHandle, KERNEL_LOGGER_NAME, EtwTraceProperties, EVENT_TRACE_CONTROL_STOP);
        Debug.WriteLine("* ETW ControlTrace STOP Result: {1}.", result);
        EtwActive = (result == 0);
        return !EtwActive;
    }
    private static void EtwStartMonitor() {
        EtwThread = new(EtwEventMonitor) {
            Name = "EtwThread",
            IsBackground = true
        };
        EtwThread.Start();
    }
    private static void EtwStopMonitor() {
        try {
            // I dont think we should even cancel the threaed at this point
            // EtwThread?.Join();
        } catch { }
    }
    private static void EtwEventMonitor() {
        EVENT_TRACE_LOGFILE logFile = new() {
            LoggerName = KERNEL_LOGGER_NAME,
            LogFileMode = 0x100, // PROCESS_TRACE_MODE_REAL_TIME
            EventCallback = new EventTraceEventCallback(EtwEventCallback),
            BufferCallback = new EventTraceBufferCallback(EtwBufferCallback)
        };
        var ptrHandle = OpenTrace(ref logFile);
        Debug.WriteLine("* ETW OpenTrace: " + ptrHandle);
        while (EtwActive) {
            var ptResult = ProcessTrace(new[] { ptrHandle }, 1, IntPtr.Zero, IntPtr.Zero);
            if (ptResult == 0) {
                if (!EtwActive) break;
            } else if (ptResult == 4201) { // ERROR_WMI_INSTANCE_NOT_FOUND
                Debug.WriteLine("* ETW ProcessTrace: 4201");
                Thread.Sleep(250);
                EtwStartSession();
                Thread.Sleep(250);
            } else {
                Debug.WriteLine("* ETW ProcessTrace: " + ptResult);
                break;
            }
        }
        Debug.WriteLine("* ETW CloseTrace: " + CloseTrace(ptrHandle));
    }
    private static void EtwEventCallback(EVENT_TRACE d) {
        if (!EtwActive) return;
        if (d.Header.Guid == DiskIoGuid) {
            // Disk IO Event
            DiskIo_TypeGroup1 mof = (DiskIo_TypeGroup1)Marshal.PtrToStructure(d.MofData, typeof(DiskIo_TypeGroup1))!;
            switch (d.Header.Type) {
                case EVENT_TRACE_TYPE_IO_READ:
                    _AllStats[0].DiskReaded += mof.TransferSize;
                    if (d.Header.ProcessId > 0 && !_AllStats.ContainsKey(d.Header.ProcessId)) {
                        _AllStats.Add(d.Header.ProcessId, new EtwStats());
                    }
                    if (d.Header.ProcessId > 0) {
                        _AllStats[d.Header.ProcessId].DiskReaded += mof.TransferSize;
                    }
                    break;
                case EVENT_TRACE_TYPE_IO_WRITE:
                    _AllStats[0].DiskWroted += mof.TransferSize;
                    if (d.Header.ProcessId > 0 && !_AllStats.ContainsKey(d.Header.ProcessId)) {
                        _AllStats.Add(d.Header.ProcessId, new EtwStats());
                    }
                    if (d.Header.ProcessId > 0) {
                        _AllStats[d.Header.ProcessId].DiskWroted += mof.TransferSize;
                    }
                    break;
            }
        } else if (d.Header.Guid == TcpIpGuid || d.Header.Guid == UdpIpGuid) {
            // TCP or UDP Event
            // Debug.WriteLine("IPv6 SEND: PID: {0}, Size: {1}, Local: [{2}]:{3}, Dest: [{4}]:5", mof.PID, mof.size, New Net.IPAddress(mof.saddr).ToString, TaskManagerConnection.GetAccuratePortNumber(mof.sport), New Net.IPAddress(mof.daddr), TaskManagerConnection.GetAccuratePortNumber(mof.dport))
            if (d.Header.Type == EVENT_TRACE_TYPE_SEND_IPV6 || d.Header.Type == EVENT_TRACE_TYPE_RECEIVE_IPV6) {
                // Send or Receive IPv6
                TcpIpOrUdpIp_IPV6_Header mof = (TcpIpOrUdpIp_IPV6_Header)Marshal.PtrToStructure(d.MofData, typeof(TcpIpOrUdpIp_IPV6_Header))!;
                if (mof.PID > 0 && !_AllStats.ContainsKey(mof.PID)) _AllStats.Add(mof.PID, new EtwStats());
                var _hash = GenerateConnectionHash(mof.saddr, mof.sport, mof.daddr, mof.dport);
                if (!_NetStats.ContainsKey(_hash)) _NetStats.Add(_hash, new EtwNetStats());
                if (d.Header.Type == EVENT_TRACE_TYPE_SEND) {
                    _AllStats[0].NetSent += mof.size;
                    if (mof.PID > 0) _AllStats[mof.PID].NetSent += mof.size;
                    _NetStats[_hash].Sent += mof.size;
                } else if (d.Header.Type == EVENT_TRACE_TYPE_RECEIVE) {
                    _AllStats[0].NetReceived += mof.size;
                    if (mof.PID > 0) _AllStats[mof.PID].NetReceived += mof.size;
                    _NetStats[_hash].Received += mof.size;
                }
            } else if (d.Header.Type == EVENT_TRACE_TYPE_SEND || d.Header.Type == EVENT_TRACE_TYPE_RECEIVE) {
                // Send or Receive IPv4
                TcpIpOrUdpIp_IPV4_Header mof = (TcpIpOrUdpIp_IPV4_Header)Marshal.PtrToStructure(d.MofData, typeof(TcpIpOrUdpIp_IPV4_Header))!;
                if (mof.PID > 0 && !_AllStats.ContainsKey(mof.PID)) _AllStats.Add(mof.PID, new EtwStats());
                var _hash = GenerateConnectionHash(mof.saddr,mof.sport,mof.daddr,mof.dport);
                if (!_NetStats.ContainsKey(_hash)) _NetStats.Add(_hash, new EtwNetStats());
                if (d.Header.Type == EVENT_TRACE_TYPE_SEND) {
                    _AllStats[0].NetSent += mof.size;
                    if (mof.PID > 0) _AllStats[mof.PID].NetSent += mof.size;
                    _NetStats[_hash].Sent += mof.size;
                } else if (d.Header.Type == EVENT_TRACE_TYPE_RECEIVE) {
                    _AllStats[0].NetReceived += mof.size;
                    if (mof.PID > 0) _AllStats[mof.PID].NetReceived += mof.size;
                    _NetStats[_hash].Received += mof.size;
                }

            }
        }

    }
    private static bool EtwBufferCallback(EVENT_TRACE_LOGFILE logfile) { return !EtwActive; }
    private static string GenerateConnectionHash(in byte[] saddr, in ushort sport, in byte[] daddr, in ushort dport) {
        return $"{ new System.Net.IPAddress(saddr) }:{ GetAccuratePortNumber(sport) }-{new System.Net.IPAddress(daddr)}:{GetAccuratePortNumber(dport)}";
    }
    private static string GenerateConnectionHash(in uint saddr, in ushort sport, in uint daddr, in ushort dport) {
        return $"{new System.Net.IPAddress(saddr)}:{GetAccuratePortNumber(sport)}-{new System.Net.IPAddress(daddr)}:{GetAccuratePortNumber(dport)}";
    }
    private static int GetAccuratePortNumber(in uint DWord) {
        byte[] Bytes = BitConverter.GetBytes(DWord);
        return (Bytes[0] << 8) + Bytes[1] + (Bytes[2] << 24) + (Bytes[3] << 16);
    }

    public static void Flush() {
        if (EtwActive) _ = ControlTrace(EtwHandle, KERNEL_LOGGER_NAME, EtwTraceProperties, EVENT_TRACE_CONTROL_FLUSH);
    }
    public static bool Start() {
        if (!EtwActive) {
            if (EtwStartSession()) {
                EtwActive = true;
                EtwStartMonitor();
            }
        }
        return EtwActive;
    }
    public static bool Stop() {
        if (EtwActive) {
            EtwActive = false;
            EtwStopMonitor();
            EtwStopSession();
        }
        return !EtwActive;
    }
    public static bool Running => EtwActive;
    public static EtwStats Stats(int PID) => _AllStats.ContainsKey((uint)PID) ? _AllStats[(uint)PID] : new EtwStats();
    public static EtwStats Stats(uint PID) => _AllStats.ContainsKey(PID) ? _AllStats[PID] : new EtwStats();
    public static EtwNetStats NetStats(string Hash) => _NetStats.ContainsKey(Hash) ? _NetStats[Hash] : new EtwNetStats();

    public class EtwStats {
        public ulong DiskReaded = 0;
        public ulong DiskWroted = 0;
        public ulong NetSent = 0;
        public ulong NetReceived = 0;
    }
    public class EtwNetStats {
        public ulong Sent = 0;
        public ulong Received = 0;
    }

}
