using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
namespace sMkTaskManager.Classes;

[SupportedOSPlatform("windows")]
internal class TaskManagerNic : IEquatable<TaskManagerNic>, INotifyPropertyChanged {
    private readonly string _Ident = "";
    private string _Name = "", _Description = "", _MacAddress = "", _State = "", _Speed = "";
    private NetworkInterfaceType _TypeValue;
    private long _SpeedValue, _RcvdValue, _SentValue, _RcvdDelta, _SentDelta, _RcvdRate, _SentRate;
    private Color _BackColor = Color.Empty;

    public TaskManagerNic() {
        LastUpdated = DateTime.Now.Ticks;
        LastChanged = LastUpdated;
        PreviousUpdate = LastUpdated;
        PropertyChanged += MyPropertyChanged;
    }
    public TaskManagerNic(string ident) : this() {
        _Ident = ident;
    }

    /* Public Properties */
    public string ID => _Ident.ToString();
    public string Ident => _Ident.ToString();
    public long LastUpdated { get; set; }
    public long LastChanged { get; set; }
    public long PreviousUpdate { get; set; }
    public bool NotifyChanges { get; set; } = true;
    public Color BackColor { get => _BackColor; set { SetField(ref _BackColor, value); } }

    public string Name { get => _Name; set { SetField(ref _Name, value); } }
    public string Description { get => _Description; set { SetField(ref _Description, value); } }
    public string State { get => _State; set { SetField(ref _State, value); } }
    public string MacAddress { get => _MacAddress; set { SetField(ref _MacAddress, value); } }
    public string Type { get => _TypeValue.ToString(); }
    public NetworkInterfaceType TypeValue { get => _TypeValue; set { SetField(ref _TypeValue, value); } }
    public string Speed { get => _Speed; }
    public long SpeedValue { get => _SpeedValue; set { SetField(ref _SpeedValue, value); } }

    public string Rcvd { get => FormatBytes(_RcvdValue); }
    public long RcvdValue { get => _RcvdValue; set { SetField(ref _RcvdValue, value); } }
    public string RcvdDelta { get => FormatDelta(_RcvdDelta); }
    public long RcvdDeltaValue { get => _RcvdDelta; set { SetField(ref _RcvdDelta, value); } }
    public string RcvdRate { get => string.Format("{0:#,0.0} Kb/s.", ((double)_RcvdRate / 1024)); }
    public long RcvdRateValue { get => _RcvdRate; set { SetField(ref _RcvdRate, value); } }
    public string Sent { get => FormatBytes(_SentValue); }
    public long SentValue { get => _SentValue; set { SetField(ref _SentValue, value); } }
    public string SentDelta { get => FormatDelta(_SentDelta); }
    public long SentDeltaValue { get => _SentDelta; set { SetField(ref _SentDelta, value); } }
    public string SentRate { get => string.Format("{0:#,0.0} Kb/s.", _SentRate / 1024); }
    public long SentRateValue { get => _SentRate; set { SetField(ref _SentRate, value); } }

    /* Public Methods */
    public event PropertyChangedEventHandler? PropertyChanged;
    public bool Equals(TaskManagerNic? other) => _Ident == other?._Ident;
    public override bool Equals(object? obj) => Equals(obj as TaskManagerNic);
    public override int GetHashCode() => _Ident.GetHashCode();
    public void Load(in NetworkInterface sourceNetworkInterface) {
        NotifyChanges = false;
        Update(sourceNetworkInterface);
        NotifyChanges = true;
    }

    public void Update(in NetworkInterface sourceNetworkInterface) {
        PreviousUpdate = LastUpdated;
        LastUpdated = DateTime.Now.Ticks;

        Name = sourceNetworkInterface.Name;
        Description = sourceNetworkInterface.Description;
        TypeValue = sourceNetworkInterface.NetworkInterfaceType;
        State = sourceNetworkInterface.OperationalStatus.ToString();
        MacAddress = sourceNetworkInterface.GetPhysicalAddress().ToString();
        // First calculate the SpeedString and then set the SpeedValue
        if (sourceNetworkInterface.Speed >= 1000000000) {
            _Speed = Math.Round(sourceNetworkInterface.Speed / 1000000000d) + " Gbps";
        } else if (sourceNetworkInterface.Speed >= 1000000) {
            _Speed = Math.Round(sourceNetworkInterface.Speed / 1000000d) + " Mbps";
        } else {
            _Speed = Math.Round(sourceNetworkInterface.Speed / 1000d) + " Kbps";
        }
        SpeedValue = sourceNetworkInterface.Speed;
        // Calculate Rcvd and Sent Traffic.
        RcvdDeltaValue = (RcvdValue == 0) ? 0 : Math.Max(0L, sourceNetworkInterface.GetIPStatistics().BytesReceived - RcvdValue);
        RcvdRateValue = CalculateRateValue(RcvdDeltaValue);
        RcvdValue = sourceNetworkInterface.GetIPStatistics().BytesReceived;
        SentDeltaValue = (SentValue == 0) ? 0 : Math.Max(0L, sourceNetworkInterface.GetIPStatistics().BytesSent - SentValue);
        SentRateValue = CalculateRateValue(SentDeltaValue);
        SentValue = sourceNetworkInterface.GetIPStatistics().BytesSent;
    }
    public static List<NetworkInterface> GetInterfaces(bool onlyUp = false) {
        List<NetworkInterface> retArray = new();
        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces()) {
            if (onlyUp && nic.OperationalStatus != OperationalStatus.Up) continue;
            switch (nic.NetworkInterfaceType) {
                case NetworkInterfaceType.Ethernet: retArray.Add(nic); break;
                case NetworkInterfaceType.Ethernet3Megabit: retArray.Add(nic); break;
                case NetworkInterfaceType.GigabitEthernet: retArray.Add(nic); break;
                case NetworkInterfaceType.FastEthernetFx: retArray.Add(nic); break;
                case NetworkInterfaceType.FastEthernetT: retArray.Add(nic); break;
                case NetworkInterfaceType.Wireless80211: retArray.Add(nic); break;
                default: continue;
            }
        }
        return retArray;
    }

    /* Private Methods */
    private void MyPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        if (!NotifyChanges) return;
        if (e.PropertyName == null) return;
        LastChanged = LastUpdated;
    }
    private void OnPropertyChanged(PropertyChangedEventArgs e) { if (NotifyChanges) PropertyChanged?.Invoke(this, e); }
    private void SetField<T>(ref T field, T newValue, [CallerMemberName] string propertyName = "", string[]? alsoNotifyProperties = null) {
        if (!EqualityComparer<T>.Default.Equals(field, newValue)) {
            field = newValue;
            LastChanged = LastUpdated;
            // Since all properties ending with Value should be read as the NonValue<string> Property
            if (propertyName.EndsWith("Value")) propertyName = propertyName.Replace("Value", "");
            if (NotifyChanges) OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            if (NotifyChanges && alsoNotifyProperties != null) {
                foreach (string ap in alsoNotifyProperties) { OnPropertyChanged(new PropertyChangedEventArgs(ap)); }
            }
        }
    }
    private static string FormatBytes(long value) {
        if (value / 1024 / 1024 > 2) {
            return string.Format("{0:#,0.0} Mb.", (double)value / 1024 / 1024);
        } else {
            return string.Format("{0:#,0.0} Kb.", (double)value / 1024);
        }
    }
    private static string FormatDelta(long value) {
        if (value == 0) {
            return "0.0 Kb.";
        } else if (value / 1024 / 1024 > 2) {
            return string.Format("+ {0:#,0.0} Mb.", (double)value / 1024 / 1024);
        } else {
            return string.Format("+ {0:#,0.0} Kb.", (double)value / 1024);
        }
    }
    private long CalculateRateValue(in long DeltaValue) {
        if (DeltaValue == 0) return 0;
        if ((LastUpdated - PreviousUpdate) <= 0) return DeltaValue;
        return ((long)(DeltaValue / ((double)(LastUpdated - PreviousUpdate) / TimeSpan.TicksPerSecond)));
    }

}

[SupportedOSPlatform("windows")]
internal class TaskManagerNicsCollection : BindingList<TaskManagerNic> {
    public BindingSource DataExporter { get; init; }
    public TaskManagerNicsCollection() {
        DataExporter = new(this, null) { RaiseListChangedEvents = true };
        AllowRemove = true;
        RaiseListChangedEvents = true;
    }

    public bool Contains(string id) {
        foreach (TaskManagerNic i in Items) {
            if (i.Ident == id) { return true; }
        }
        return false;
    }
    public TaskManagerNic this[string id] {
        get {
            foreach (TaskManagerNic i in Items) {
                if (i.Ident == id) { return i; }
            }
            return default!;
        }
    }
    public TaskManagerNic GetNic(string id) {
        foreach (TaskManagerNic i in Items) {
            if (i.Ident == id) return i;
        }
        return null!;
    }

}