using System.Diagnostics;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.Diagnostics.Tracing.Session;
namespace sMkTaskManager.Classes;

internal class TaskManagetETW {
    private Thread? etwThread;
    private TraceEventSession? kernelSession;
    private readonly Dictionary<int, EtwStats> _AllStats = new() { { 0, new() } };
    private readonly Dictionary<int, EtwDiskStats> _DiskStats = new();
    private readonly Dictionary<string, EtwNetStats> _NetStats = new();

    public string sessionName = "sMkTaskManager";
    public System.Threading.ThreadState? State => etwThread?.ThreadState;
    public bool Running => etwThread?.ThreadState == (System.Threading.ThreadState.Running | System.Threading.ThreadState.Background);
    public static bool IsElevated => TraceEventSession.IsElevated() ?? false;

    public EtwStats Stats(int PID) => _AllStats.ContainsKey(PID) ? _AllStats[PID] : new EtwStats();
    public EtwStats Stats(uint PID) => _AllStats.ContainsKey((int)PID) ? _AllStats[(int)PID] : new EtwStats();
    public EtwNetStats NetStats(string Hash) => _NetStats.ContainsKey(Hash) ? _NetStats[Hash] : new EtwNetStats();
    public EtwDiskStats DiskStats(int DiskNumber) => _DiskStats.ContainsKey(DiskNumber) ? _DiskStats[DiskNumber] : new EtwDiskStats();

    public bool Start() {
        if (!Running && IsElevated) {
            StartMonitor();
            Debug.WriteLine("ETW has been Started");
        }
        return Running;
    }
    public bool Stop() {
        if (etwThread != null) {
            StopSession();
            Debug.WriteLine("ETW Disposed");
        }
        return !Running;
    }
    public void Flush() {
        kernelSession?.Flush();
    }

    private void StartMonitor() {
        etwThread = new Thread(StartSession) {
            Name = "sMkETW",
            IsBackground = true,
            Priority = ThreadPriority.Normal
        };
        etwThread.Start();
    }
    private void StartSession() {
        kernelSession = new TraceEventSession(sessionName);
        kernelSession.EnableKernelProvider(
          KernelTraceEventParser.Keywords.DiskIO |
          KernelTraceEventParser.Keywords.NetworkTCPIP 
        );

        kernelSession.Source.Kernel.DiskIORead += OnDiskRead;
        kernelSession.Source.Kernel.DiskIOWrite += OnDiskWrite;

        kernelSession.Source.Kernel.TcpIpRecv += OnTcpIpRecv;
        kernelSession.Source.Kernel.TcpIpRecvIPV6 += OnTcpIpRecvIPV6;
        kernelSession.Source.Kernel.UdpIpRecv += OnUdpIpRecv;
        kernelSession.Source.Kernel.UdpIpRecvIPV6 += OnUdpIpRecvIPV6;

        kernelSession.Source.Kernel.TcpIpSend += OnTcpIpSend;
        kernelSession.Source.Kernel.TcpIpSendIPV6 += OnTcpIpSendIPV6;
        kernelSession.Source.Kernel.UdpIpSend += OnUdpIpSend;
        kernelSession.Source.Kernel.UdpIpSendIPV6 += OnUdpIpSendIPV6;
        
        kernelSession.Source.Process();
    }
    private void StopSession() {
        kernelSession?.Dispose();
    }

    private void OnDiskRead(DiskIOTraceData obj) {
        _AllStats[0].DiskReaded += (uint)obj.TransferSize;
        // Per Disk
        if (!_DiskStats.ContainsKey(obj.DiskNumber)) _DiskStats.Add(obj.DiskNumber, new());
        _DiskStats[obj.DiskNumber].Readed += (uint)obj.TransferSize;
        // Per Process
        if (obj.ProcessID > 0) {
            if (!_AllStats.ContainsKey(obj.ProcessID)) _AllStats.Add(obj.ProcessID, new());
            _AllStats[obj.ProcessID].DiskReaded += (uint)obj.TransferSize;
        }
    }
    private void OnDiskWrite(DiskIOTraceData obj) {
        _AllStats[0].DiskWroted += (uint)obj.TransferSize;
        // Per Disk
        if (!_DiskStats.ContainsKey(obj.DiskNumber)) _DiskStats.Add(obj.DiskNumber, new());
        _DiskStats[obj.DiskNumber].Wroted += (uint)obj.TransferSize;
        // Per Process
        if (obj.ProcessID > 0) {
            if (!_AllStats.ContainsKey(obj.ProcessID)) _AllStats.Add(obj.ProcessID, new());
            _AllStats[obj.ProcessID].DiskWroted += (uint)obj.TransferSize;
        }
    }
    private void OnTcpIpSend(TcpIpSendTraceData mof) { ProcessNetworkPacket(mof.ProcessID, mof.size, $"{mof.saddr}:{mof.sport}-{mof.daddr}:{mof.dport}", true); }
    private void OnTcpIpSendIPV6(TcpIpV6SendTraceData mof) { ProcessNetworkPacket(mof.ProcessID, mof.size, $"{mof.saddr}:{mof.sport}-{mof.daddr}:{mof.dport}", true); }
    private void OnTcpIpRecv(TcpIpTraceData mof) { ProcessNetworkPacket(mof.ProcessID, mof.size, $"{mof.saddr}:{mof.sport}-{mof.daddr}:{mof.dport}", false); }
    private void OnTcpIpRecvIPV6(TcpIpV6TraceData mof) { ProcessNetworkPacket(mof.ProcessID, mof.size, $"{mof.saddr}:{mof.sport}-{mof.daddr}:{mof.dport}", false); }
    private void OnUdpIpSend(UdpIpTraceData mof) { ProcessNetworkPacket(mof.ProcessID, mof.size, $"{mof.saddr}:{mof.sport}-{mof.daddr}:{mof.dport}", true); }
    private void OnUdpIpSendIPV6(UpdIpV6TraceData mof) { ProcessNetworkPacket(mof.ProcessID, mof.size, $"{mof.saddr}:{mof.sport}-{mof.daddr}:{mof.dport}", true); }
    private void OnUdpIpRecv(UdpIpTraceData mof) { ProcessNetworkPacket(mof.ProcessID, mof.size, $"{mof.saddr}:{mof.sport}-{mof.daddr}:{mof.dport}", false); }
    private void OnUdpIpRecvIPV6(UpdIpV6TraceData mof) { ProcessNetworkPacket(mof.ProcessID, mof.size, $"{mof.saddr}:{mof.sport}-{mof.daddr}:{mof.dport}", false); }
    private void ProcessNetworkPacket(in int ProcessID, in int Size, in string Hash, in bool isSent) {
        if (Size <= 0) return;
        /* Common for all Network Events, just prepare */
        if (ProcessID > 0 && !_AllStats.ContainsKey(ProcessID)) _AllStats.Add(ProcessID, new EtwStats());
        if (!_NetStats.ContainsKey(Hash)) _NetStats.Add(Hash, new EtwNetStats());

        if (isSent) {
            /* Compute to NetSent */
            _AllStats[0].NetSent += (uint)Size;
            if (ProcessID > 0) _AllStats[ProcessID].NetSent += (uint)Size;
            _NetStats[Hash].Sent += (uint)Size;
        } else {
            /* Compute to NetReceived */
            _AllStats[0].NetReceived += (uint)Size;
            if (ProcessID > 0) _AllStats[ProcessID].NetReceived += (uint)Size;
            _NetStats[Hash].Received += (uint)Size;

        }

    }

    public class EtwStats {
        public ulong DiskReaded = 0;
        public ulong DiskWroted = 0;
        public ulong NetSent = 0;
        public ulong NetReceived = 0;
    }
    public class EtwDiskStats {
        public ulong Readed = 0;
        public ulong Wroted = 0;
    }
    public class EtwNetStats {
        public ulong Sent = 0;
        public ulong Received = 0;
    }

}