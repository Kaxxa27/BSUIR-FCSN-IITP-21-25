using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
namespace sMkTaskManager.Classes;

[SupportedOSPlatform("windows")]
internal class TaskManagerConnection : IEquatable<TaskManagerConnection>, INotifyPropertyChanged {
    const int AF_INET = 2;
    const int AF_INET6 = 23;

    private string _Ident = "", _ProcessName = "", _LifeTime = "", _ImageKey = "";
    private Int128 _SentValue, _ReceivedValue, _SentDeltaValue, _ReceivedDeltaValue, _SentRateValue, _ReceivedRateValue;
    private ProtocolClass _Protocol;
    private ConnectionState _State;
    private IPEndPoint _Local = new(0, 0);
    private IPEndPoint _Remote = new(0, 0);
    private Color _BackColor = Color.Empty;
    private int _ImageIndex = 0, _PID;

    public TaskManagerConnection() {
        LastUpdated = DateTime.Now.Ticks;
        LastChanged = LastUpdated;
        CreationTime = LastUpdated;
        PreviousUpdate = LastUpdated;
        PropertyChanged += MyPropertyChanged;
    }
    public TaskManagerConnection(string connIdent) : this() {
        _Ident = connIdent;
    }

    /* Public Properties */
    public string ID => _Ident;
    public string Ident => _Ident;
    public long LastUpdated { get; set; }
    public long LastChanged { get; set; }
    public long CreationTime { get; set; }
    public long PreviousUpdate { get; set; } = 0;
    public bool NotifyChanges { get; set; } = true;
    public bool AlreadyCreated { get; set; } = false;
    public API.MIB_TCPROW? BaseTCProw { get; set; }
    public bool isIPv6 { get; set; } = false;
    public int PID { get => _PID; set { SetField(ref _PID, value); } }
    public string ProcessName { get => _ProcessName; set { SetField(ref _ProcessName, value); } }
    public Color BackColor { get => _BackColor; set { SetField(ref _BackColor, value); } }
    public int ImageIndex { get => _ImageIndex; set { SetField(ref _ImageIndex, value); } }
    public string ImageKey { get => _ImageKey; set { SetField(ref _ImageKey, value); } }

    public string LifeTime { get => AlreadyCreated ? "~ " + _LifeTime : _LifeTime; set { SetField(ref _LifeTime, value); } }
    public IPEndPoint Local { get => _Local; set { SetField(ref _Local, value, nameof(Local), new[] { nameof(LocalAddr), nameof(LocalPort) }); } }
    public IPEndPoint Remote { get => _Remote; set { SetField(ref _Remote, value, nameof(Remote), new[] { nameof(RemoteAddr), nameof(RemotePort) }); } }
    public string LocalAddr { get => isIPv6 ? "[" + _Local.Address.ToString() + "]" : _Local.Address.ToString(); }
    public string LocalPort { get => _Local.Port.ToString(); }
    public string RemoteAddr { get => isIPv6 ? "[" + _Remote.Address.ToString() + "]" : _Remote.Address.ToString(); }
    public string RemotePort { get => _Remote.Port.ToString(); }
    public ConnectionState State { get => _State; set { SetField(ref _State, value, nameof(State), new[] { nameof(StateString) }); } }
    public string StateString { get => (_State == 0) ? "Stateless" : Shared.ToTitleCase(_State.ToString().Replace("_", " ")); }
    public ProtocolClass Protocol { get => _Protocol; set { SetField(ref _Protocol, value, nameof(Protocol), new[] { nameof(ProtocolString) }); } }
    public string ProtocolString { get => _Protocol.ToString(); }

    public string Sent { get => FormatValue(_SentValue); }
    public Int128 SentValue { get => _SentValue; set { SetField(ref _SentValue, value); } }
    public string SentDelta { get => FormatValue(_SentDeltaValue); }
    public Int128 SentDeltaValue { get => _SentDeltaValue; set { SetField(ref _SentDeltaValue, value); } }
    public string SentRate { get => FormatValue(_SentRateValue, "/s"); }
    public Int128 SentRateValue { get => _SentRateValue; set { SetField(ref _SentRateValue, value); } }
    public string Received { get => FormatValue(_ReceivedValue); }
    public Int128 ReceivedValue { get => _ReceivedValue; set { SetField(ref _ReceivedValue, value); } }
    public string ReceivedDelta { get => FormatValue(_ReceivedDeltaValue); }
    public Int128 ReceivedDeltaValue { get => _ReceivedDeltaValue; set { SetField(ref _ReceivedDeltaValue, value); } }
    public string ReceivedRate { get => FormatValue(_ReceivedRateValue, "/s"); }
    public Int128 ReceivedRateValue { get => _ReceivedRateValue; set { SetField(ref _ReceivedRateValue, value); } }

    /* Public Methods */
    public event PropertyChangedEventHandler? PropertyChanged;
    public bool Equals(TaskManagerConnection? other) => _Ident == other?._Ident;
    public override bool Equals(object? obj) => Equals(obj as TaskManagerConnection);
    public override int GetHashCode() => _Ident.GetHashCode();
    public void CalculateHashIdent() {
        _Ident = Convert.ToInt32(_Protocol) + "-" + _Local.Address.ToString() + ":" + _Local.Port + "-" + _Remote.Address.ToString() + ":" + _Remote.Port;
    }
    public bool Close() {
        if (BaseTCProw == null) return false;
        API.MIB_TCPROW newRow = BaseTCProw;
        newRow.State = 12;
        return API.SetTcpEntry(newRow) == 0;
    }
    public void Load(in TaskManagerConnection sourceConnection) {
        NotifyChanges = false;
        isIPv6 = sourceConnection.isIPv6;
        Local = sourceConnection.Local;
        Remote = sourceConnection.Remote;
        Protocol = sourceConnection.Protocol;
        AlreadyCreated = sourceConnection.AlreadyCreated;
        CreationTime = sourceConnection.CreationTime;
        if (sourceConnection.BaseTCProw != null) BaseTCProw = sourceConnection.BaseTCProw;
        CalculateHashIdent();
        Update(sourceConnection);
        NotifyChanges = true;
    }
    public void Update(in TaskManagerConnection sourceConnection) {
        PreviousUpdate = LastUpdated;
        LastUpdated = DateTime.Now.Ticks;

        State = sourceConnection.State;
        if (PID != sourceConnection.PID) {
            PID = sourceConnection.PID;
            ProcessName = GetProcessName(PID);
        } else if (PID == 0 && ProcessName.Equals("")) { ProcessName = GetProcessName(PID); }

        LifeTime = Shared.TimeDiff(CreationTime, 1);

        SentDeltaValue = Shared.ETW.NetStats(Ident[2..]).Sent - SentValue;
        ReceivedDeltaValue = Shared.ETW.NetStats(Ident[2..]).Received - ReceivedValue;
        SentValue = Shared.ETW.NetStats(Ident[2..]).Sent;
        ReceivedValue = Shared.ETW.NetStats(Ident[2..]).Received;
        SentRateValue = CalculateRateValue(SentDeltaValue);
        ReceivedRateValue = CalculateRateValue(ReceivedDeltaValue);

        if (NotifyChanges && LastUpdated <= LastChanged) {
            BackColor = Settings.Highlights.ChangingItems ? Settings.Highlights.ChangingColor : Color.Empty;
        } else {
            BackColor = Color.Empty;
        }

    }
    public static HashSet<TaskManagerConnection> GetTcpListeners() {
        HashSet<TaskManagerConnection> retValue = new();
        int buffSize = 0;
        // How much memory do we need?
        int retCode = API.GetExtendedTcpTable(nint.Zero, ref buffSize, true, AF_INET, API.TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_LISTENER, 0);
        if (retCode == 50) return retValue; // ERROR_NOT_SUPPORTED
        IntPtr buffTable = Marshal.AllocHGlobal(buffSize);
        // Actually Get The Values & Num Entries
        if (API.GetExtendedTcpTable(buffTable, ref buffSize, true, AF_INET, API.TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_LISTENER, 0) == 0) {
            API.MIB_TCPTABLE_OWNER_PID tcpTable = (API.MIB_TCPTABLE_OWNER_PID)Marshal.PtrToStructure(buffTable, typeof(API.MIB_TCPTABLE_OWNER_PID))!;
            for (uint i = 0; i < tcpTable.dwNumEntries; i++) {
                IntPtr rowPtr = new(buffTable + Marshal.SizeOf(tcpTable.dwNumEntries) + (i * Marshal.SizeOf(typeof(API.MIB_TCPROW_OWNER_PID))));
                API.MIB_TCPROW_OWNER_PID tcpRow = (API.MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(API.MIB_TCPROW_OWNER_PID))!;
                TaskManagerConnection row = new() {
                    BaseTCProw = (API.MIB_TCPROW?)Marshal.PtrToStructure(rowPtr, typeof(API.MIB_TCPROW)),
                    NotifyChanges = false,
                    Protocol = ProtocolClass.TCP,
                    State = (ConnectionState)tcpRow.State,
                    Local = new IPEndPoint(tcpRow.LocalAddr, GetAccuratePortNumber(tcpRow.LocalPort)),
                    Remote = new IPEndPoint(tcpRow.RemoteAddr, GetAccuratePortNumber(tcpRow.RemotePort)),
                    PID = tcpRow.OwningPid
                };
                row.CalculateHashIdent();
                retValue.Add(row);
            }
        }
        Marshal.FreeHGlobal(buffTable);
        return retValue;
    }
    public static HashSet<TaskManagerConnection> GetTcp6Listeners() {
        HashSet<TaskManagerConnection> retValue = new();
        int buffSize = 0;
        // How much memory do we need?
        int retCode = API.GetExtendedTcpTable(IntPtr.Zero, ref buffSize, true, AF_INET6, API.TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_LISTENER, 0);
        if (retCode == 50) return retValue; // ERROR_NOT_SUPPORTED
        IntPtr buffTable = Marshal.AllocHGlobal(buffSize);
        // Actually Get The Values & Num Entries
        if (API.GetExtendedTcpTable(buffTable, ref buffSize, true, AF_INET6, API.TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_LISTENER, 0) == 0) {
            API.MIB_TCP6TABLE_OWNER_PID tcpTable = (API.MIB_TCP6TABLE_OWNER_PID)Marshal.PtrToStructure(buffTable, typeof(API.MIB_TCP6TABLE_OWNER_PID))!;
            for (uint i = 0; i < tcpTable.dwNumEntries; i++) {
                IntPtr rowPtr = new(buffTable + Marshal.SizeOf(tcpTable.dwNumEntries) + (i * Marshal.SizeOf(typeof(API.MIB_TCP6ROW_OWNER_PID))));
                API.MIB_TCP6ROW_OWNER_PID tcpRow = (API.MIB_TCP6ROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(API.MIB_TCP6ROW_OWNER_PID))!;
                TaskManagerConnection row = new() {
                    NotifyChanges = false,
                    isIPv6 = true,
                    Protocol = ProtocolClass.TCPv6,
                    State = (ConnectionState)tcpRow.State,
                    Local = new IPEndPoint(new IPAddress(tcpRow.LocalAddr), GetAccuratePortNumber(tcpRow.LocalPort)),
                    Remote = new IPEndPoint(new IPAddress(tcpRow.RemoteAddr), GetAccuratePortNumber(tcpRow.RemotePort)),
                    PID = tcpRow.OwningPid
                };
                row.CalculateHashIdent();
                retValue.Add(row);
            }
        }
        Marshal.FreeHGlobal(buffTable);
        return retValue;
    }
    public static HashSet<TaskManagerConnection> GetTcpConnections() {
        HashSet<TaskManagerConnection> retValue = new();
        int buffSize = 0;
        // How much memory do we need?
        int retCode = API.GetExtendedTcpTable(IntPtr.Zero, ref buffSize, true, AF_INET, API.TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_CONNECTIONS, 0);
        if (retCode == 50) return retValue; // ERROR_NOT_SUPPORTED
        IntPtr buffTable = Marshal.AllocHGlobal(buffSize);
        // Actually Get The Values & Num Entries
        if (API.GetExtendedTcpTable(buffTable, ref buffSize, true, AF_INET, API.TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_CONNECTIONS, 0) == 0) {
            API.MIB_TCPTABLE_OWNER_PID tcpTable = (API.MIB_TCPTABLE_OWNER_PID)Marshal.PtrToStructure(buffTable, typeof(API.MIB_TCPTABLE_OWNER_PID))!;
            for (uint i = 0; i < tcpTable.dwNumEntries; i++) {
                IntPtr rowPtr = new(buffTable + Marshal.SizeOf(tcpTable.dwNumEntries) + (i * Marshal.SizeOf(typeof(API.MIB_TCPROW_OWNER_PID))));
                API.MIB_TCPROW_OWNER_PID tcpRow = (API.MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(API.MIB_TCPROW_OWNER_PID))!;
                TaskManagerConnection row = new() {
                    BaseTCProw = (API.MIB_TCPROW?)Marshal.PtrToStructure(rowPtr, typeof(API.MIB_TCPROW)),
                    NotifyChanges = false,
                    Protocol = ProtocolClass.TCP,
                    State = (ConnectionState)tcpRow.State,
                    Local = new IPEndPoint(tcpRow.LocalAddr, GetAccuratePortNumber(tcpRow.LocalPort)),
                    Remote = new IPEndPoint(tcpRow.RemoteAddr, GetAccuratePortNumber(tcpRow.RemotePort)),
                    PID = tcpRow.OwningPid
                };
                row.CalculateHashIdent();
                retValue.Add(row);
            }
        }
        Marshal.FreeHGlobal(buffTable);
        return retValue;
    }
    public static HashSet<TaskManagerConnection> GetTcp6Connections() {
        HashSet<TaskManagerConnection> retValue = new();
        int buffSize = 0;
        // How much memory do we need?
        int retCode = API.GetExtendedTcpTable(IntPtr.Zero, ref buffSize, true, AF_INET6, API.TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_CONNECTIONS, 0);
        if (retCode == 50) return retValue; // ERROR_NOT_SUPPORTED
        IntPtr buffTable = Marshal.AllocHGlobal(buffSize);
        // Actually Get The Values & Num Entries
        if (API.GetExtendedTcpTable(buffTable, ref buffSize, true, AF_INET6, API.TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_CONNECTIONS, 0) == 0) {
            API.MIB_TCP6TABLE_OWNER_PID tcpTable = (API.MIB_TCP6TABLE_OWNER_PID)Marshal.PtrToStructure(buffTable, typeof(API.MIB_TCP6TABLE_OWNER_PID))!;
            for (uint i = 0; i < tcpTable.dwNumEntries; i++) {
                IntPtr rowPtr = new(buffTable + Marshal.SizeOf(tcpTable.dwNumEntries) + (i * Marshal.SizeOf(typeof(API.MIB_TCP6ROW_OWNER_PID))));
                API.MIB_TCP6ROW_OWNER_PID tcpRow = (API.MIB_TCP6ROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(API.MIB_TCP6ROW_OWNER_PID))!;
                TaskManagerConnection row = new() {
                    NotifyChanges = false,
                    isIPv6 = true,
                    Protocol = ProtocolClass.TCPv6,
                    State = (ConnectionState)tcpRow.State,
                    Local = new IPEndPoint(new IPAddress(tcpRow.LocalAddr), GetAccuratePortNumber(tcpRow.LocalPort)),
                    Remote = new IPEndPoint(new IPAddress(tcpRow.LocalAddr), GetAccuratePortNumber(tcpRow.RemotePort)),
                    PID = tcpRow.OwningPid
                };
                row.CalculateHashIdent();
                retValue.Add(row);
            }
        }
        Marshal.FreeHGlobal(buffTable);
        return retValue;
    }
    public static HashSet<TaskManagerConnection> GetUdpConnections() {
        HashSet<TaskManagerConnection> retValue = new();
        int buffSize = 0;
        // How much memory do we need?
        int retCode = API.GetExtendedUdpTable(IntPtr.Zero, ref buffSize, true, AF_INET, API.UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0);
        if (retCode == 50) return retValue; // ERROR_NOT_SUPPORTED
        IntPtr buffTable = Marshal.AllocHGlobal(buffSize);
        // Actually Get The Values & Num Entries
        if (API.GetExtendedUdpTable(buffTable, ref buffSize, true, AF_INET, API.UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0) == 0) {
            API.MIB_UDPTABLE_OWNER_PID udpTable = (API.MIB_UDPTABLE_OWNER_PID)Marshal.PtrToStructure(buffTable, typeof(API.MIB_UDPTABLE_OWNER_PID))!;
            for (uint i = 0; i < udpTable.dwNumEntries; i++) {
                IntPtr rowPtr = new(buffTable + Marshal.SizeOf(udpTable.dwNumEntries) + (i * Marshal.SizeOf(typeof(API.MIB_UDPROW_OWNER_PID))));
                API.MIB_UDPROW_OWNER_PID udpRow = (API.MIB_UDPROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(API.MIB_UDPROW_OWNER_PID))!;
                TaskManagerConnection row = new() {
                    NotifyChanges = false,
                    Protocol = ProtocolClass.UDP,
                    State = 0,
                    Local = new IPEndPoint(udpRow.LocalAddr, GetAccuratePortNumber(udpRow.LocalPort)),
                    Remote = new IPEndPoint(0, 0),
                    PID = udpRow.OwningPid
                };
                row.CalculateHashIdent();
                retValue.Add(row);
            }
        }
        Marshal.FreeHGlobal(buffTable);
        return retValue;
    }
    public static HashSet<TaskManagerConnection> GetUdp6Connections() {
        HashSet<TaskManagerConnection> retValue = new();
        int buffSize = 0;
        // How much memory do we need?
        int retCode = API.GetExtendedUdpTable(IntPtr.Zero, ref buffSize, true, AF_INET6, API.UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0);
        if (retCode == 50) return retValue; // ERROR_NOT_SUPPORTED
        IntPtr buffTable = Marshal.AllocHGlobal(buffSize);
        // Actually Get The Values & Num Entries
        if (API.GetExtendedUdpTable(buffTable, ref buffSize, true, AF_INET6, API.UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0) == 0) {
            API.MIB_UDP6TABLE_OWNER_PID udpTable = (API.MIB_UDP6TABLE_OWNER_PID)Marshal.PtrToStructure(buffTable, typeof(API.MIB_UDP6TABLE_OWNER_PID))!;
            for (uint i = 0; i < udpTable.dwNumEntries; i++) {
                IntPtr rowPtr = new(buffTable + Marshal.SizeOf(udpTable.dwNumEntries) + (i * Marshal.SizeOf(typeof(API.MIB_UDP6ROW_OWNER_PID))));
                API.MIB_UDP6ROW_OWNER_PID udpRow = (API.MIB_UDP6ROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(API.MIB_UDP6ROW_OWNER_PID))!;
                TaskManagerConnection row = new() {
                    NotifyChanges = false,
                    isIPv6 = true,
                    Protocol = ProtocolClass.UDPv6,
                    State = 0,
                    Local = new IPEndPoint(new IPAddress(udpRow.LocalAddr), GetAccuratePortNumber(udpRow.LocalPort)),
                    Remote = new IPEndPoint(0, 0),
                    PID = udpRow.OwningPid
                };
                row.CalculateHashIdent();
                retValue.Add(row);
            }
        }
        Marshal.FreeHGlobal(buffTable);
        return retValue;
    }
    public static HashSet<TaskManagerConnection> GetAllListeners(bool IncludeIPv6 = false) {
        HashSet<TaskManagerConnection> retValue = new();
        retValue.UnionWith(GetTcpListeners());
        if (IncludeIPv6) retValue.UnionWith(GetTcp6Listeners());
        return retValue;
    }
    public static HashSet<TaskManagerConnection> GetAllConnections(bool IncludeUDP, bool IncludeIPv6) {
        HashSet<TaskManagerConnection> retValue = new();
        retValue.UnionWith(GetTcpConnections());
        if (IncludeIPv6) retValue.UnionWith(GetTcp6Connections());
        if (IncludeUDP) retValue.UnionWith(GetUdpConnections());
        if (IncludeUDP && IncludeIPv6) retValue.UnionWith(GetUdp6Connections());
        return retValue;
    }
    public enum ProtocolClass {
        TCP = 1,
        UDP = 2,
        TCPv6 = 3,
        UDPv6 = 4
    }
    public enum ConnectionState : int {
        CLOSED = 1,
        LISTENING = 2,
        SYN_SENT = 3,
        SYN_RCVD = 4,
        ESTABLISHED = 5,
        FIN_WAIT1 = 6,
        FIN_WAIT2 = 7,
        CLOSE_WAIT = 8,
        CLOSING = 9,
        LAST_ACK = 10,
        TIME_WAIT = 11,
        DELETE_TCB = 12
    }

    /* Private Methods */
    private void MyPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        if (!NotifyChanges) return;
        if (e.PropertyName == null) return;
        if (e.PropertyName.Equals("LifeTime")) return;
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
    private Int128 CalculateRateValue(in Int128 DeltaValue) {
        if (DeltaValue == 0) return 0;
        if ((LastUpdated - PreviousUpdate) <= 0) return DeltaValue;
        return DeltaValue / (ulong)(LastUpdated - PreviousUpdate) / TimeSpan.TicksPerSecond;
    }
    private static string FormatValue(in Int128 value, in string suffix = "") {
        if (value < 10240) {
            return string.Format("{0:#,0} B" + suffix, value);
        } else if (value < 1024 * 1024) {
            return string.Format("{0:#,0} Kb" + suffix, (double)(value / 1024));
        } else {
            return string.Format("{0:#,0.0} Mb" + suffix, (double)(value / 1024 / 1024));
        }
    }
    internal static string GetProcessName(int PID) {
        try {
            if (PID == 0) { return "None"; }
            Process p = Process.GetProcessById(PID);
            if (PID <= Shared.bpi) { return p.ProcessName; }
            try {
                return Path.GetFileName(p.MainModule!.FileName);
            } catch (Exception ex) {
                Debug.WriteLine("Failed to GetProcessName for PID {0}: {1}", PID, ex.Message);
                return p.ProcessName;
            }
        } catch (Exception ex) {
            Debug.WriteLine("Failed to GetProcessName for PID {0}: {1}", PID, ex.Message);
            return "Unknown";
        }
    }
    internal static int GetAccuratePortNumber(uint DWord) {
        byte[] Bytes = BitConverter.GetBytes(DWord);
        return (Bytes[0] << 8) + Bytes[1] + (Bytes[2] << 24) + (Bytes[3] << 16);
    }

}

[SupportedOSPlatform("windows")]
internal class TaskManagerConnectionCollection : BindingList<TaskManagerConnection> {
    public BindingSource DataExporter { get; init; }
    public TaskManagerConnectionCollection() {
        DataExporter = new(this, null) { RaiseListChangedEvents = true };
        AllowRemove = true;
        RaiseListChangedEvents = true;
    }
    public bool Contains(string connIdent) {
        foreach (TaskManagerConnection i in Items) {
            if (i.Ident == connIdent) { return true; }
        }
        return false;
    }
    public TaskManagerConnection? GetConnection(string connIdent) {
        foreach (TaskManagerConnection i in Items) {
            if (i.Ident == connIdent) return i;
        }
        return null;
    }
}