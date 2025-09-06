using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using static sMkTaskManager.Classes.API;
namespace sMkTaskManager.Classes;

[SupportedOSPlatform("windows")]
internal class TaskManagerGPU : IDisposable, IEquatable<TaskManagerGPU>, INotifyPropertyChanged {
    private Color _BackColor = Color.Empty;
    private int _ImageIndex = 0, _StateImageIndex = 0;
    private string _ImageKey = "", _Name = "", _ChipType = "";
    private D3DKMT_ADAPTERTYPE? _AdapterType;
    private D3DKMT_ADAPTER_PERFDATA _PerformanceData;
    private D3DKMT_ADAPTERREGISTRYINFO? _RegistryInfo;
    private D3DKMT_SEGMENTSIZEINFO? _SegmentSize;
    private D3DKMT_QUERYSTATISTICS_ADAPTER_INFORMATION? _Information;

    public TaskManagerGPU(D3DKMT_ADAPTERINFO AdapterInfo) {
        LastUpdated = DateTime.Now.Ticks;
        LastChanged = LastUpdated;
        PreviousUpdate = LastUpdated;
        Info = AdapterInfo;
        Nodes = Array.Empty<NodeInfo>();
        _PerformanceData = new() { PhysicalAdapterIndex = 0 };
    }

    /* Public Properties */
    public string ID => Handle.ToString();
    public uint Handle => Info.hAdapter;
    public long LastUpdated { get; set; }
    public long LastChanged { get; set; }
    public long PreviousUpdate { get; set; }
    public bool NotifyChanges { get; set; } = true;
    public Color Color { get; set; } = Color.LimeGreen;
    public Color BackColor { get => _BackColor; set { SetField(ref _BackColor, value); } }
    public int ImageIndex { get => _ImageIndex; set { SetField(ref _ImageIndex, value); } }
    public int StateImageIndex { get => _StateImageIndex; set { SetField(ref _StateImageIndex, value); } }
    public string ImageKey { get => _ImageKey; set { SetField(ref _ImageKey, value); } }
    public string Name { get => _Name; set { SetField(ref _Name, value); } }
    public string ChipType { get => _ChipType; set { SetField(ref _ChipType, value); } }
    public uint SegmentCount => Information.NbSegments;
    public uint NodeCount => Information.NodeCount;
    public D3DKMT_ADAPTERINFO Info;
    public D3DKMT_ADAPTERTYPE Type => getAdapterType();
    public D3DKMT_ADAPTERREGISTRYINFO RegistryInfo => getAdapterRegistryInfo();
    public D3DKMT_SEGMENTSIZEINFO SegmentSize => getSegmentSize();
    public D3DKMT_QUERYSTATISTICS_ADAPTER_INFORMATION Information => getAdapterInformation();
    public D3DKMT_ADAPTER_PERFDATA PerformanceData => _PerformanceData;
    public NodeInfo[] Nodes;
    public struct NodeInfo {
        public NodeInfo(ulong Id, string Name) {
            this.Id = Id;
            this.Name = Name;
            UsageValue = 0;
        }
        public void UpdateUsage(long newRunningTime, DateTime newQueryTime) {
            UsageValue = CalculateUsage(newRunningTime, newQueryTime, RunningTime, QueryTime);
            RunningTime = newRunningTime; QueryTime = newQueryTime;
        }
        public readonly ulong Id;
        public readonly string Name;
        public long RunningTime { get; private set; }
        public DateTime QueryTime { get; private set; }
        public float UsageValue { get; private set; }
        public readonly string Usage => UsageValue.ToString("00.0");
    }

    private long _RunningTime; private uint _FanSpeed; private DateTime _QueryTime;
    private float _UsageValue, _PowerUsage, _Temperature;
    public long RunningTime { get => _RunningTime; set { SetField(ref _RunningTime, value); } }
    public DateTime QueryTime { get => _QueryTime; set { SetField(ref _QueryTime, value); } }
    public float UsageValue { get => _UsageValue; set { SetField(ref _UsageValue, value); } }
    public string Usage => UsageValue.ToString("00.0");
    public float PowerUsage { get => _PowerUsage; set { SetField(ref _PowerUsage, value); } }
    public float Temperature { get => _Temperature; set { SetField(ref _Temperature, value); } }
    public uint FanSpeed { get => _FanSpeed; set { SetField(ref _FanSpeed, value); } }
    /* Shared Memory */
    private ulong _SharedMemoryTotalValue, _SharedMemoryUsedValue, _SharedMemoryFreeValue;
    public ulong SharedMemoryTotalValue { get => _SharedMemoryTotalValue; set { SetField(ref _SharedMemoryTotalValue, value); } }
    public string SharedMemoryTotal => string.Format("{0:#,0}", _SharedMemoryTotalValue / 1024 / 1024) + " Mb.";
    public ulong SharedMemoryUsedValue { get => _SharedMemoryUsedValue; set { SetField(ref _SharedMemoryUsedValue, value); } }
    public string SharedMemoryUsed => string.Format("{0:#,0}", _SharedMemoryUsedValue / 1024 / 1024) + " Mb.";
    public ulong SharedMemoryFreeValue { get => _SharedMemoryFreeValue; set { SetField(ref _SharedMemoryFreeValue, value); } }
    public string SharedMemoryFree => string.Format("{0:#,0}", _SharedMemoryFreeValue / 1024 / 1024) + " Mb.";
    public int SharedMemoryUsage => (SharedMemoryTotalValue > 0) ? (int)(100 * SharedMemoryUsedValue / SharedMemoryTotalValue) : 0;
    /* Dedicated Memory */
    private ulong _DedicatedMemoryTotalValue, _DedicatedMemoryUsedValue, _DedicatedMemoryFreeValue;
    public ulong DedicatedMemoryTotalValue { get => _DedicatedMemoryTotalValue; set { SetField(ref _DedicatedMemoryTotalValue, value); } }
    public string DedicatedMemoryTotal => string.Format("{0:#,0}", _DedicatedMemoryTotalValue / 1024 / 1024) + " Mb.";
    public ulong DedicatedMemoryUsedValue { get => _DedicatedMemoryUsedValue; set { SetField(ref _DedicatedMemoryUsedValue, value); } }
    public string DedicatedMemoryUsed => string.Format("{0:#,0}", _DedicatedMemoryUsedValue / 1024 / 1024) + " Mb.";
    public ulong DedicatedMemoryFreeValue { get => _DedicatedMemoryFreeValue; set { SetField(ref _DedicatedMemoryFreeValue, value); } }
    public string DedicatedMemoryFree => string.Format("{0:#,0}", _DedicatedMemoryFreeValue / 1024 / 1024) + " Mb.";
    public int DedicatedMemoryUsage => (DedicatedMemoryTotalValue > 0) ? (int)(100 * DedicatedMemoryUsedValue / DedicatedMemoryTotalValue) : 0;

    /* Public Methods */
    public event PropertyChangedEventHandler? PropertyChanged;
    public bool Equals(TaskManagerGPU? other) => Handle == other?.Handle;
    public override bool Equals(object? obj) => Equals(obj as TaskManagerGPU);
    public override int GetHashCode() => Handle.GetHashCode();
    public void Dispose() {
        CloseAdapter();
        // GC.SuppressFinalize(this);
    }
    public static List<TaskManagerGPU> GetAdaptersList() {
        var result = new List<TaskManagerGPU>();
        var adapters = new D3DKMT_ENUMADAPTERS();
        if (D3DKMTEnumAdapters(ref adapters) == NTSTATUS.Success) {
            for (int i = 0; i < adapters.NumAdapters; i++) {
                var newAdapter = new TaskManagerGPU(adapters.Adapters[i]);
                if (!newAdapter.Type.SoftwareDevice && newAdapter.Type.RenderSupported) {
                    result.Add(newAdapter);
                } else {
                    newAdapter.Dispose();
                }
            }
        }
        return result;
    }
    public void Load() {
        getAdapterType();
        getAdapterRegistryInfo();
        getSegmentSize();
        getAdapterInformation();
        ChipType = RegistryInfo.ChipType;
        if (!string.IsNullOrEmpty(RegistryInfo.AdapterString)) {
            Name = RegistryInfo.AdapterString;
        } else if (!string.IsNullOrEmpty(RegistryInfo.BiosString)) {
            Name = RegistryInfo.BiosString;
        } else {
            Name = "Unsuported Adapter";
        }
        Name = Name.Replace("Intel(R)", "Intel").Replace("NVIDIA", "nVidia").Replace("Laptop GPU", "");
        DedicatedMemoryTotalValue = SegmentSize.DedicatedVideoMemorySize;
        SharedMemoryTotalValue = SegmentSize.SharedSystemMemorySize;
        Nodes = new NodeInfo[NodeCount];
        LoadNodes();
        Refresh();
    }
    public void LoadNodes() {
        for (uint nodeId = 0; nodeId < NodeCount; nodeId++) {
            var nodeMetaData = queryNodeMetadata(nodeId);
            Nodes[nodeId] = new NodeInfo(nodeId, GetNodeEngineTypeString(nodeMetaData));
        }
    }
    public void Refresh() {
        getPerformanceData();
        RefreshNodes();
        RefreshSegments();
        PowerUsage = (float)(_PerformanceData.Power / 10.0);
        Temperature = (float)(_PerformanceData.Temperature / 10.0);
        FanSpeed = _PerformanceData.FanRPM;
    }
    public void RefreshNodes() {
        long newRunningTime = 0;
        DateTime newQueryTime = DateTime.Now;
        for (uint nodeId = 0; nodeId < NodeCount; nodeId++) {
            var nodeInformation = queryStatisticsNode(nodeId);
            Nodes[nodeId].UpdateUsage(nodeInformation.GlobalInformation.RunningTime, newQueryTime);
            newRunningTime += Nodes[nodeId].RunningTime;
        }
        UsageValue = CalculateUsage(newRunningTime, newQueryTime, RunningTime, QueryTime);
        RunningTime = newRunningTime; QueryTime = newQueryTime;
    }
    public void RefreshSegments() {
        ulong _SharedMemoryUsed = 0, _DedicatedMemoryUsed = 0;

        for (uint segmentId = 0; segmentId < SegmentCount; segmentId++) {
            var segmentInformation = queryStatisticsSegment(segmentId);
            if (segmentInformation.Aperture == 1) {
                _SharedMemoryUsed += segmentInformation.BytesResident;
            } else {
                _DedicatedMemoryUsed += segmentInformation.BytesResident;
            }
        }
        unchecked {
            SharedMemoryUsedValue = _SharedMemoryUsed + (1024 * 1024 * 10);
            DedicatedMemoryUsedValue = _DedicatedMemoryUsed + (1024 * 1024 * 1000);
            if (SharedMemoryUsedValue > SharedMemoryTotalValue) SharedMemoryUsedValue = SharedMemoryTotalValue;
            if (DedicatedMemoryUsedValue > DedicatedMemoryTotalValue) DedicatedMemoryUsedValue = DedicatedMemoryTotalValue;
            SharedMemoryFreeValue = SharedMemoryTotalValue - SharedMemoryUsedValue;
            DedicatedMemoryFreeValue = DedicatedMemoryTotalValue - DedicatedMemoryUsedValue;
        }

    }
    public string EngineUsage(string engineName) {
        foreach (NodeInfo n in Nodes) {
            if (n.Name.Equals(engineName)) return n.Usage;
        }
        return "00.0";
    }
    public float EngineUsageValue(string engineName) {
        foreach (NodeInfo n in Nodes) {
            if (n.Name.Equals(engineName)) return n.UsageValue;
        }
        return 0;
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
    private static float CalculateUsage(long newRunningTime, DateTime newQueryTime, long prevRunningTime, DateTime prevQueryTime) {
        // Used to skip the first value, as we cant calculate.
        if (newRunningTime < prevRunningTime) { newRunningTime = prevRunningTime; newQueryTime = prevQueryTime; }
        long runningDiff = newRunningTime - prevRunningTime;
        long timeDiff = newQueryTime.Ticks - prevQueryTime.Ticks;
        return (runningDiff > 0) ? 100f * runningDiff / timeDiff : 0;
    }
    private static string GetNodeEngineTypeString(D3DKMT_NODEMETADATA nodeMetaData) {
        return nodeMetaData.NodeData.EngineType switch {
            DXGK_ENGINE_TYPE.DXGK_ENGINE_TYPE_OTHER => (!string.IsNullOrWhiteSpace(nodeMetaData.NodeData.FriendlyName) ? nodeMetaData.NodeData.FriendlyName : "Other"),
            DXGK_ENGINE_TYPE.DXGK_ENGINE_TYPE_3D => "3D",
            DXGK_ENGINE_TYPE.DXGK_ENGINE_TYPE_VIDEO_DECODE => "Video Decode",
            DXGK_ENGINE_TYPE.DXGK_ENGINE_TYPE_VIDEO_ENCODE => "Video Encode",
            DXGK_ENGINE_TYPE.DXGK_ENGINE_TYPE_VIDEO_PROCESSING => "Video Processing",
            DXGK_ENGINE_TYPE.DXGK_ENGINE_TYPE_SCENE_ASSEMBLY => "Scene Assembly",
            DXGK_ENGINE_TYPE.DXGK_ENGINE_TYPE_COPY => "Copy",
            DXGK_ENGINE_TYPE.DXGK_ENGINE_TYPE_OVERLAY => "Overlay",
            DXGK_ENGINE_TYPE.DXGK_ENGINE_TYPE_CRYPTO => "Crypto",
            _ => "Unknown"
        };
    }

    private bool CloseAdapter() {
        D3DKMT_CLOSEADAPTER closeAdaperStruct;
        closeAdaperStruct.hAdapter = Handle;
        return D3DKMTCloseAdapter(ref closeAdaperStruct) == NTSTATUS.Success;
    }
    private D3DKMT_ADAPTERTYPE getAdapterType() {
        if (!_AdapterType.HasValue) {
            _AdapterType = queryAdapterInfo<D3DKMT_ADAPTERTYPE>(KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_ADAPTERTYPE);
        }
        return _AdapterType.Value;
    }
    private D3DKMT_ADAPTERREGISTRYINFO getAdapterRegistryInfo() {
        if (!_RegistryInfo.HasValue) {
            _RegistryInfo = queryAdapterInfo<D3DKMT_ADAPTERREGISTRYINFO>(KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_ADAPTERREGISTRYINFO);
        }
        return _RegistryInfo.Value;
    }
    private D3DKMT_SEGMENTSIZEINFO getSegmentSize() {
        if (!_SegmentSize.HasValue) {
            _SegmentSize = queryAdapterInfo<D3DKMT_SEGMENTSIZEINFO>(KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_GETSEGMENTSIZE);
        }
        return _SegmentSize.Value;
    }
    private D3DKMT_QUERYSTATISTICS_ADAPTER_INFORMATION getAdapterInformation() {
        if (!_Information.HasValue) {
            _Information = queryStatisticsAdapter();
        }
        return _Information.Value;
    }
    private D3DKMT_ADAPTER_PERFDATA getPerformanceData() {
        //_PerformanceData = queryAdapterInfo<D3DKMT_ADAPTER_PERFDATA>(KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_ADAPTERPERFDATA);
        //return _PerformanceData;
        queryAdapterInfo(KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_ADAPTERPERFDATA, ref _PerformanceData);
        return _PerformanceData;
    }
    private T queryAdapterInfo<T>(KMTQUERYADAPTERINFOTYPE requestType) where T : struct {
        int dataSize = 4;
        if (!typeof(T).IsEnum) { dataSize = Marshal.SizeOf<T>(); }
        var dataBuf = Marshal.AllocHGlobal(dataSize);

        try {
            if (queryAdapterInfo(requestType, dataBuf, dataSize)) { return dataBuf.ToObject<T>(); }
        } finally {
            Marshal.FreeHGlobal(dataBuf);
        }

        return new T();
    }
    private bool queryAdapterInfo(KMTQUERYADAPTERINFOTYPE requestType, IntPtr dataBuf, int dataSize) {
        var queryStruct = new D3DKMT_QUERYADAPTERINFO() {
            hAdapter = Handle,
            Type = requestType,
            pPrivateDriverData = dataBuf,
            PrivateDriverDataSize = (uint)dataSize
        };
        return (D3DKMTQueryAdapterInfo(ref queryStruct) == NTSTATUS.Success);
    }
    private void queryAdapterInfo<T>(KMTQUERYADAPTERINFOTYPE requestType, ref T requestStruct) where T : struct {
        var bufferPtr = requestStruct.ToPointer();
        if (queryAdapterInfo(requestType, bufferPtr, Marshal.SizeOf<T>())) {
            requestStruct = bufferPtr.ToObject<T>();
            Marshal.FreeHGlobal(bufferPtr);
        }
    }
    private D3DKMT_QUERYSTATISTICS_ADAPTER_INFORMATION queryStatisticsAdapter() {
        var queryStruct = new D3DKMT_QUERYSTATISTICS {
            Type = D3DKMT_QUERYSTATISTICS_TYPE.D3DKMT_QUERYSTATISTICS_ADAPTER,
            Luid = Info.AdapterLuid,
            hProcess = 0U
        };

        if (D3DKMTQueryStatistics(ref queryStruct) == NTSTATUS.Success) {
            return queryStruct.QueryResult.AdapterInformation;
        } else {
            return new D3DKMT_QUERYSTATISTICS_ADAPTER_INFORMATION();
        }
    }
    private D3DKMT_NODEMETADATA queryNodeMetadata(uint nodeId) {
        IntPtr nodeMetaDataPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(D3DKMT_NODEMETADATA)));
        var nodeMetaDataResult = new D3DKMT_NODEMETADATA { NodeOrdinalAndAdapterIndex = nodeId };
        Marshal.StructureToPtr(nodeMetaDataResult, nodeMetaDataPtr, true);

        var queryAdapterInfo = new D3DKMT_QUERYADAPTERINFO {
            hAdapter = Handle,
            Type = KMTQUERYADAPTERINFOTYPE.KMTQAITYPE_NODEMETADATA,
            pPrivateDriverData = nodeMetaDataPtr,
            PrivateDriverDataSize = (uint)Marshal.SizeOf(typeof(D3DKMT_NODEMETADATA))
        };

        if (D3DKMTQueryAdapterInfo(ref queryAdapterInfo) == NTSTATUS.Success) {
            nodeMetaDataResult = Marshal.PtrToStructure<D3DKMT_NODEMETADATA>(nodeMetaDataPtr);
            Marshal.FreeHGlobal(nodeMetaDataPtr);
        }
        return nodeMetaDataResult;
    }
    private D3DKMT_QUERYSTATISTICS_NODE_INFORMATION queryStatisticsNode(uint nodeId) {
        var queryElement = new D3DKMT_QUERYSTATISTICS_QUERY_ELEMENT { NodeId = nodeId };
        var queryStatistics = new D3DKMT_QUERYSTATISTICS {
            Luid = Info.AdapterLuid,
            Type = D3DKMT_QUERYSTATISTICS_TYPE.D3DKMT_QUERYSTATISTICS_NODE,
            QueryElement = queryElement
        };
        D3DKMTQueryStatistics(ref queryStatistics);
        return queryStatistics.QueryResult.NodeInformation;
    }
    private D3DKMT_QUERYSTATISTICS_SEGMENT_INFORMATION queryStatisticsSegment(uint segmentId) {
        var queryElement = new D3DKMT_QUERYSTATISTICS_QUERY_ELEMENT { SegmentId = segmentId };
        var queryStatistics = new D3DKMT_QUERYSTATISTICS {
            Luid = Info.AdapterLuid,
            Type = D3DKMT_QUERYSTATISTICS_TYPE.D3DKMT_QUERYSTATISTICS_SEGMENT,
            QueryElement = queryElement
        };
        D3DKMTQueryStatistics(ref queryStatistics);
        return queryStatistics.QueryResult.SegmentInformation;
    }
}

[SupportedOSPlatform("windows")]
internal class TaskManagerGPUCollection : BindingList<TaskManagerGPU> {
    public BindingSource DataExporter { get; init; }
    public TaskManagerGPUCollection() {
        DataExporter = new(this, null) { RaiseListChangedEvents = true };
        AllowRemove = true;
        RaiseListChangedEvents = true;
    }
    public bool Contains(uint adapterHandle) {
        foreach (TaskManagerGPU i in Items) {
            if (i.Handle == adapterHandle) { return true; }
        }
        return false;
    }
    public bool Contains(uint adapterHandle, out TaskManagerGPU? theGPU) {
        foreach (TaskManagerGPU i in Items) {
            if (i.Handle == adapterHandle) { theGPU = i; return true; }
        }
        theGPU = null; return false;
    }
    public TaskManagerGPU? GetGpu(uint adapterHandle) {
        foreach (TaskManagerGPU i in Items) {
            if (i.Handle == adapterHandle) return i;
        }
        return null;
    }
}
