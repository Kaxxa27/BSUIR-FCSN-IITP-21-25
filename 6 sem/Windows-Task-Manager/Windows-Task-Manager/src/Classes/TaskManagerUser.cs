using System.Net;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Diagnostics;

namespace sMkTaskManager.Classes;

[SupportedOSPlatform("windows")]
internal class TaskManagerUser : IEquatable<TaskManagerUser>, INotifyPropertyChanged {

    private string _Status = "", _Client = "", _Session = "", _Domain = "", _Username = "";
    private DateTime _ConnectTimeValue, _DisconnectTimeValue, _LastInputTimeValue, _LogonTimeValue;
    private int _ID, _StatusNum = -1, _TotalProcesses = 0;
    private Color _BackColor = Color.Empty; private int _ImageIndex = 0; private string _ImageKey = "";

    public TaskManagerUser() {
        LastUpdated = DateTime.Now.Ticks;
        LastChanged = LastUpdated;
        PreviousUpdate = LastUpdated;
    }
    public TaskManagerUser(int sessionId) : this() {
        _ID = sessionId;
    }
    public TaskManagerUser(string sessionId) : this() {
        _ID = int.Parse(sessionId);
    }

    /* Public Properties */
    public string ID => _ID.ToString();
    public string Ident => _ID.ToString();
    public long LastUpdated { get; set; }
    public long LastChanged { get; set; }
    public long PreviousUpdate { get; set; }
    public bool NotifyChanges { get; set; } = true;
    public Color BackColor { get => _BackColor; set { SetField(ref _BackColor, value); } }
    public int ImageIndex { get => _ImageIndex; set { SetField(ref _ImageIndex, value); } }
    public string ImageKey { get => _ImageKey; set { SetField(ref _ImageKey, value); } }

    public string Status { get => _Status; set { SetField(ref _Status, value); } }
    public int StatusNum { get => _StatusNum; set { SetField(ref _StatusNum, value); } }
    public string Client { get => _Client; set { SetField(ref _Client, value); } }
    public string Session { get => _Session; set { SetField(ref _Session, value); } }
    public string Username { get => _Username; set { SetField(ref _Username, value, nameof(Username), new[] { nameof(User) }); } }
    public string Domain { get => _Domain; set { SetField(ref _Domain, value, nameof(Domain), new[] { nameof(User) }); } }
    public string User { get => (_Domain == "") ? _Username : _Domain + "\\" + _Username; }

    public string LogonTime { get => (_LogonTimeValue == default) ? "n/a." : _LogonTimeValue.ToShortDateString() + " " + _LogonTimeValue.ToLongTimeString(); }
    public DateTime LogonTimeValue { get => _LogonTimeValue; set { SetField(ref _LogonTimeValue, value); } }
    public string ConnectTime { get => (_ConnectTimeValue == default) ? "n/a." : _ConnectTimeValue.ToShortDateString() + " " + _ConnectTimeValue.ToLongTimeString(); }
    public DateTime ConnectTimeValue { get => _ConnectTimeValue; set { SetField(ref _ConnectTimeValue, value); } }
    public string LastInputTime { get => (_LastInputTimeValue == default) ? "n/a." : _LastInputTimeValue.ToShortDateString() + " " + _LastInputTimeValue.ToLongTimeString(); }
    public DateTime LastInputTimeValue { get => _LastInputTimeValue; set { SetField(ref _LastInputTimeValue, value); } }
    public string DisconnectTime { get => (_DisconnectTimeValue == default) ? "n/a." : _DisconnectTimeValue.ToShortDateString() + " " + _DisconnectTimeValue.ToLongTimeString(); }
    public DateTime DisconnectTimeValue { get => _DisconnectTimeValue; set { SetField(ref _DisconnectTimeValue, value); } }
    public int TotalProcesses { get => _TotalProcesses; set { SetField(ref _TotalProcesses, value); } }

    public int ResolutionHorizontal { get; set; } = 0;
    public int ResolutionVertical { get; set; } = 0;
    public int ResolutionColorDeph { get; set; } = 0;
    public IPAddress ClientAddress { get; set; } = null!;

    /* Public Methods */
    public event PropertyChangedEventHandler? PropertyChanged;
    public bool Equals(TaskManagerUser? other) => _ID == other?._ID;
    public override bool Equals(object? obj) => Equals(obj as TaskManagerUser);
    public override int GetHashCode() => _ID.GetHashCode();
    public void Load(in TaskManagerUser sourceConnection) {
        NotifyChanges = false;
        _ID = sourceConnection._ID;
        Update(sourceConnection);
        NotifyChanges = true;
    }
    public void Update(in TaskManagerUser sourceConnection) {
        PreviousUpdate = LastUpdated;
        LastUpdated = DateTime.Now.Ticks;

        Username = sourceConnection.Username;
        Domain = sourceConnection.Domain;
        Status = sourceConnection.Status;
        StatusNum = sourceConnection.StatusNum;
        ImageIndex = sourceConnection.ImageIndex;
        ImageKey = sourceConnection.ImageKey;
        Client = sourceConnection.Client;
        Session = sourceConnection.Session;
        TotalProcesses = sourceConnection.TotalProcesses;

        if (NotifyChanges && LastUpdated <= LastChanged) {
            BackColor = Settings.Highlights.ChangingItems ? Settings.Highlights.ChangingColor : Color.Empty;
        } else {
            BackColor = Color.Empty;
        }
    }
    public void QueryUpdate() {
        PreviousUpdate = LastUpdated;
        LastUpdated = DateTime.Now.Ticks;
        int infoLength = 0;

        if (Environment.OSVersion.Version >= new Version(6, 0)) {
            IntPtr thisInfoPtr = IntPtr.Zero;
            if (API.WTSQuerySessionInformation(IntPtr.Zero, Convert.ToInt32(_ID), API.WTS_INFO_CLASS.WTSSessionInfo, ref thisInfoPtr, ref infoLength)) {
                API.WTS_INFO tmp = (API.WTS_INFO)Marshal.PtrToStructure(thisInfoPtr, typeof(API.WTS_INFO))!;
                StatusNum = (int)tmp.State;
                Status = GetConnectionStateString(tmp.State);
                ImageIndex = ((int)tmp.State == 4) ? 1 : 0;
                Username = tmp.UserName;
                Domain = tmp.Domain;
                Session = tmp.WinStationName;
                LogonTimeValue = DateTime.FromFileTime(tmp.LogonTime);
                ConnectTimeValue = DateTime.FromFileTime(tmp.ConnectTime);
                LastInputTimeValue = DateTime.FromFileTime(tmp.LastInputTime);
                DisconnectTimeValue = DateTime.FromFileTime(tmp.DisconnectTime);
                API.WTSFreeMemory(thisInfoPtr);
            }
        } else {
            IntPtr thisInfoPtr = IntPtr.Zero;
            if (API.WTSQuerySessionInformation(IntPtr.Zero, Convert.ToInt32(_ID), API.WTS_INFO_CLASS.WTSUserName, ref thisInfoPtr, ref infoLength)) {
                Username = Marshal.PtrToStringAuto(thisInfoPtr)!;
                API.WTSFreeMemory(thisInfoPtr);
            }
            if (API.WTSQuerySessionInformation(IntPtr.Zero, Convert.ToInt32(_ID), API.WTS_INFO_CLASS.WTSDomainName, ref thisInfoPtr, ref infoLength)) {
                Domain = Marshal.PtrToStringAuto(thisInfoPtr)!;
                API.WTSFreeMemory(thisInfoPtr);
            }
            if (API.WTSQuerySessionInformation(IntPtr.Zero, Convert.ToInt32(_ID), API.WTS_INFO_CLASS.WTSWinStationName, ref thisInfoPtr, ref infoLength)) {
                Session = Marshal.PtrToStringAuto(thisInfoPtr)!;
                API.WTSFreeMemory(thisInfoPtr);
            }
            if (API.WTSQuerySessionInformation(IntPtr.Zero, Convert.ToInt32(_ID), API.WTS_INFO_CLASS.WTSConnectState, ref thisInfoPtr, ref infoLength)) {
                StatusNum = Marshal.ReadInt32(thisInfoPtr);
                Status = GetConnectionStateString((API.WTS_CONNECTSTATE_CLASS)StatusNum);
                ImageIndex = (Marshal.ReadInt32(thisInfoPtr) == 4) ? 1 : 0;
                API.WTSFreeMemory(thisInfoPtr);
            }
            API.WINSTATIONINFORMATIONW thisInfoStruct = new();
            _ = API.WinStationQueryInformation(IntPtr.Zero, Convert.ToInt32(_ID), 8, ref thisInfoStruct, Marshal.SizeOf(thisInfoStruct), ref infoLength);
            LogonTimeValue = DateTime.FromFileTime(thisInfoStruct.LoginTime);
            ConnectTimeValue = DateTime.FromFileTime(thisInfoStruct.ConnectTime);
            LastInputTimeValue = DateTime.FromFileTime(thisInfoStruct.LastInputTime);
            DisconnectTimeValue = DateTime.FromFileTime(thisInfoStruct.DisconnectTime);
        }

        // Common, Client Display
        IntPtr thisDispPtr = IntPtr.Zero;
        if (API.WTSQuerySessionInformation(IntPtr.Zero, Convert.ToInt32(_ID), API.WTS_INFO_CLASS.WTSClientDisplay, ref thisDispPtr, ref infoLength)) {
            API.WTS_CLIENT_DISPLAY tmp = (API.WTS_CLIENT_DISPLAY)Marshal.PtrToStructure(thisDispPtr, typeof(API.WTS_CLIENT_DISPLAY))!;
            ResolutionHorizontal = tmp.HorizontalResolution;
            ResolutionVertical = tmp.VerticalResolution;
            switch (tmp.ColorDepth) {
                case 1: ResolutionColorDeph = 4; break;
                case 2: ResolutionColorDeph = 8; break;
                case 4: ResolutionColorDeph = 16; break;
                case 8: ResolutionColorDeph = 24; break;
                case 16: ResolutionColorDeph = 15; break;
            }
            API.WTSFreeMemory(thisDispPtr);
        }
        // Common, Client Address
        IntPtr thisAddrPtr = IntPtr.Zero;
        if (API.WTSQuerySessionInformation(IntPtr.Zero, Convert.ToInt32(_ID), API.WTS_INFO_CLASS.WTSClientAddress, ref thisAddrPtr, ref infoLength)) {
            API.WTS_CLIENT_ADDRESS tmp = (API.WTS_CLIENT_ADDRESS)Marshal.PtrToStructure(thisAddrPtr, typeof(API.WTS_CLIENT_ADDRESS))!;
            if (tmp.AddressFamily == 4) {
                byte[] address = new byte[4];
                Array.Copy(tmp.Address, 2, address, 0, 4);
                ClientAddress = new IPAddress(address);
            }
            API.WTSFreeMemory(thisAddrPtr);
        }

        if (NotifyChanges && LastUpdated <= LastChanged) {
            BackColor = Settings.Highlights.ChangingItems ? Settings.Highlights.ChangingColor : Color.Empty;
        } else {
            BackColor = Color.Empty;
        }
    }
    public bool Connect(bool bWait, string pPassword) {
        try {
            return API.WTSConnectSession((nuint)_ID, 0, pPassword, bWait);
        } catch (Exception ex) {
            Shared.DebugTrap(ex, 4561); return false;
        }
    }
    public bool LogOff(bool bWait) {
        try {
            return API.WTSLogoffSession(IntPtr.Zero, _ID, bWait);
        } catch (Exception ex) {
            Shared.DebugTrap(ex, 4562); return false;
        }

    }
    public bool Disconnect(bool bWait) {
        try {
            return API.WTSDisconnectSession(IntPtr.Zero, _ID, bWait);
        } catch (Exception ex) {
            Shared.DebugTrap(ex, 4563); return false;
        }
    }
    public bool Message(string Title, string Text, Microsoft.VisualBasic.MsgBoxStyle Style) {
        try {
            API.RemoteMessageBoxResult res = API.RemoteMessageBoxResult.Ok;
            return API.WTSSendMessage(IntPtr.Zero, _ID, Title, Title.Length * Marshal.SystemDefaultCharSize, Text, Text.Length * Marshal.SystemDefaultCharSize, (int)Style, 0, ref res, false);
        } catch (Exception ex) {
            Shared.DebugTrap(ex, 4565); return false;
        }
    }
    public bool CanDisconnect() {
        return (API.WTS_CONNECTSTATE_CLASS)StatusNum switch {
            API.WTS_CONNECTSTATE_CLASS.WTSDisconnected => false,
            API.WTS_CONNECTSTATE_CLASS.WTSDown => false,
            _ => true,
        };
    }
    public bool CanLogOff() {
        return true;
    }
    public bool CanConnect() {
        if (Environment.OSVersion.Version >= new Version(6, 0)) {
            return true;
        } else {
            return false;
        }
    }
    public static HashSet<TaskManagerUser> GetUsers() {
        // Get Processes Information, we have to count them all
        IntPtr ptrProcessInfo = IntPtr.Zero;
        int intProcessCount = 0;
        Dictionary<int, int> _AllProcesses = new();
        if (API.WTSEnumerateProcesses(IntPtr.Zero, 0, 1, ref ptrProcessInfo, ref intProcessCount) > 0) {
            // Get the length in bytes of each structure...
            long lngPtrPos = ptrProcessInfo.ToInt64();
            for (int intCount = 0; intCount < intProcessCount; intCount++) {
                API.WTS_PROCESS_INFO strucProcessInfo;
                strucProcessInfo = (API.WTS_PROCESS_INFO)Marshal.PtrToStructure(new IntPtr(lngPtrPos), typeof(API.WTS_PROCESS_INFO))!;
                if (!_AllProcesses.ContainsKey(strucProcessInfo.ProcessId)) {
                    _AllProcesses.Add(strucProcessInfo.ProcessId, strucProcessInfo.SessionId);
                }
                lngPtrPos += Marshal.SizeOf(strucProcessInfo);
            }
            API.WTSFreeMemory(ptrProcessInfo);
        }

        HashSet<TaskManagerUser> retValue = new();
        IntPtr ppSessionInfo = IntPtr.Zero;
        int FRetCount = 0;
        if (API.WTSEnumerateSessions(IntPtr.Zero, 0, 1, ref ppSessionInfo, ref FRetCount) != 0) {
            API.WTS_SESSION_INFO[] sessionInfo = new API.WTS_SESSION_INFO[FRetCount + 1];
            var DataSize = Marshal.SizeOf(new API.WTS_SESSION_INFO());
            nint current = ppSessionInfo;
            for (int i = 0; i < FRetCount; i++) { // Step i + 1
                sessionInfo[i] = (API.WTS_SESSION_INFO)Marshal.PtrToStructure(new IntPtr(current), typeof(API.WTS_SESSION_INFO))!;
                current += DataSize;
            }
            API.WTSFreeMemory(ppSessionInfo);
            for (int i = 0; i < sessionInfo.GetUpperBound(0); i++) {
                if (sessionInfo[i].SessionID == 0) continue;
                TaskManagerUser thisUser = new(sessionInfo[i].SessionID.ToString()) {
                    Session = sessionInfo[i].pWinStationName,
                    StatusNum = (int)sessionInfo[i].State,
                    ImageIndex = ((int)sessionInfo[i].State == 4) ? 1 : 0,
                    Status = GetConnectionStateString(sessionInfo[i].State)
                };
                int infoLength = 0;
                IntPtr thisInfoPtr = IntPtr.Zero;
                if (Environment.OSVersion.Version >= new Version(6, 0)) {
                    if (API.WTSQuerySessionInformation(IntPtr.Zero, sessionInfo[i].SessionID, API.WTS_INFO_CLASS.WTSSessionInfo, ref thisInfoPtr, ref infoLength)) {
                        API.WTS_INFO tmp = (API.WTS_INFO)Marshal.PtrToStructure(thisInfoPtr, typeof(API.WTS_INFO))!;
                        thisUser.Username = tmp.UserName;
                        thisUser.Domain = tmp.Domain;
                        thisUser.LogonTimeValue = DateTime.FromFileTime(tmp.LogonTime);
                        thisUser.ConnectTimeValue = DateTime.FromFileTime(tmp.ConnectTime);
                        thisUser.LastInputTimeValue = DateTime.FromFileTime(tmp.LastInputTime);
                        thisUser.DisconnectTimeValue = DateTime.FromFileTime(tmp.DisconnectTime);
                        API.WTSFreeMemory(thisInfoPtr);
                    }
                } else {
                    if (API.WTSQuerySessionInformation(IntPtr.Zero, sessionInfo[i].SessionID, API.WTS_INFO_CLASS.WTSUserName, ref thisInfoPtr, ref infoLength)) {
                        thisUser.Username = Marshal.PtrToStringAuto(thisInfoPtr)!;
                        API.WTSFreeMemory(thisInfoPtr);
                    }
                    if (API.WTSQuerySessionInformation(IntPtr.Zero, sessionInfo[i].SessionID, API.WTS_INFO_CLASS.WTSDomainName, ref thisInfoPtr, ref infoLength)) {
                        thisUser.Domain = Marshal.PtrToStringAuto(thisInfoPtr)!;
                        API.WTSFreeMemory(thisInfoPtr);
                    }
                    API.WINSTATIONINFORMATIONW thisInfoStruct = new();
                    _ = API.WinStationQueryInformation(IntPtr.Zero, sessionInfo[i].SessionID, 8, ref thisInfoStruct, Marshal.SizeOf(thisInfoStruct), ref infoLength);
                    thisUser.LogonTimeValue = DateTime.FromFileTime(thisInfoStruct.LoginTime);
                    thisUser.ConnectTimeValue = DateTime.FromFileTime(thisInfoStruct.ConnectTime);
                    thisUser.LastInputTimeValue = DateTime.FromFileTime(thisInfoStruct.LastInputTime);
                    thisUser.DisconnectTimeValue = DateTime.FromFileTime(thisInfoStruct.DisconnectTime);
                }
                // Attach Processes
                foreach (KeyValuePair<int, int> kv in _AllProcesses) {
                    if (kv.Value == thisUser._ID) thisUser.TotalProcesses += 1;
                }
                retValue.Add(thisUser);
            }
        } else { Debug.WriteLine("No data retruned"); }
        _AllProcesses.Clear();

        return retValue;
    }

    /* Private Methods */
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
    private static string GetConnectionStateString(API.WTS_CONNECTSTATE_CLASS State) {
        return State switch {
            API.WTS_CONNECTSTATE_CLASS.WTSActive => "Active",
            API.WTS_CONNECTSTATE_CLASS.WTSConnected => "Connected",
            API.WTS_CONNECTSTATE_CLASS.WTSConnectQuery => "Query",
            API.WTS_CONNECTSTATE_CLASS.WTSDisconnected => "Disconnected",
            API.WTS_CONNECTSTATE_CLASS.WTSDown => "Down",
            API.WTS_CONNECTSTATE_CLASS.WTSIdle => "Idle",
            API.WTS_CONNECTSTATE_CLASS.WTSInit => "Initializing",
            API.WTS_CONNECTSTATE_CLASS.WTSListen => "Listen",
            API.WTS_CONNECTSTATE_CLASS.WTSReset => "Reset",
            API.WTS_CONNECTSTATE_CLASS.WTSShadow => "Shadowing",
            _ => "Unknown",
        };
    }

}

[SupportedOSPlatform("windows")]
internal class TaskManagerUserCollection : BindingList<TaskManagerUser> {
    public BindingSource DataExporter { get; init; }
    public TaskManagerUserCollection() {
        DataExporter = new(this, null) { RaiseListChangedEvents = true };
        AllowRemove = true;
        RaiseListChangedEvents = true;
    }

    public bool Contains(string sessionId) {
        foreach (TaskManagerUser i in Items) {
            if (i.Ident == sessionId) { return true; }
        }
        return false;
    }
    public TaskManagerUser this[string sessionId] {
        get {
            foreach (TaskManagerUser i in Items) {
                if (i.Ident == sessionId) { return i; }
            }
            return default!;
        }
    }
    public TaskManagerUser GetUser(string sessionId) {
        foreach (TaskManagerUser i in Items) {
            if (i.Ident == sessionId) return i;
        }
        return null!;
    }

}