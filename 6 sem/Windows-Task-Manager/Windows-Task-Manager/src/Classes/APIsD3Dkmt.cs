using System.Runtime.Versioning;
using System.Runtime.InteropServices;
namespace sMkTaskManager.Classes;

[SupportedOSPlatform("windows")]
internal unsafe static partial class API {

    public static bool IsBitSet(this uint data, int pos) {
        return ((data >> pos) & 1) != 0;
    }
    public static uint GetValue(this uint data, int pos, int len) {
        return (data >> pos) & ((1U << len) - 1);
    }
    public static uint SetBit(this uint data, int pos, bool val) {
        if (val) {
            data |= (1U << pos);
        } else {
            data &= ~(1U << pos);
        }

        return data;
    }
    public static T ToObject<T>(this byte[] bytes) {
        int bufferSize = Marshal.SizeOf(typeof(T));
        IntPtr bufferPtr = Marshal.AllocHGlobal(bufferSize);

        try {
            Marshal.Copy(bytes, 0, bufferPtr, bufferSize);
            return (T)Marshal.PtrToStructure(bufferPtr, typeof(T))!;
        } finally {
            Marshal.FreeHGlobal(bufferPtr);
        }
    }
    public static T ToObject<T>(this IntPtr pointer) {
        if (typeof(T).IsEnum)
            return (T)Enum.ToObject(typeof(T), Marshal.ReadInt32(pointer));

        if (typeof(T).IsValueType)
            return (T)Marshal.PtrToStructure(pointer, typeof(T))!;

        throw new ArgumentException(null, typeof(T).Name);
    }
    public static IntPtr ToPointer(this object obj) {
        if (!obj.GetType().IsValueType) throw new ArgumentException(null, obj.GetType().Name);
        var bufferPtr = Marshal.AllocHGlobal(Marshal.SizeOf(obj));
        Marshal.StructureToPtr(obj, bufferPtr, false);
        return bufferPtr;
    }

    public enum KMTQUERYADAPTERINFOTYPE : uint {
        KMTQAITYPE_UMDRIVERPRIVATE = 0,
        KMTQAITYPE_UMDRIVERNAME = 1,
        KMTQAITYPE_UMOPENGLINFO = 2,
        KMTQAITYPE_GETSEGMENTSIZE = 3,
        KMTQAITYPE_ADAPTERGUID = 4,
        KMTQAITYPE_FLIPQUEUEINFO = 5,
        KMTQAITYPE_ADAPTERADDRESS = 6,
        KMTQAITYPE_SETWORKINGSETINFO = 7,
        KMTQAITYPE_ADAPTERREGISTRYINFO = 8,
        KMTQAITYPE_CURRENTDISPLAYMODE = 9,
        KMTQAITYPE_MODELIST = 10,
        KMTQAITYPE_CHECKDRIVERUPDATESTATUS = 11,
        KMTQAITYPE_VIRTUALADDRESSINFO = 12,
        KMTQAITYPE_DRIVERVERSION = 13,
        KMTQAITYPE_ADAPTERTYPE = 15,
        KMTQAITYPE_OUTPUTDUPLCONTEXTSCOUNT = 16,
        KMTQAITYPE_WDDM_1_2_CAPS = 17,
        KMTQAITYPE_UMD_DRIVER_VERSION = 18,
        KMTQAITYPE_DIRECTFLIP_SUPPORT = 19,
        KMTQAITYPE_MULTIPLANEOVERLAY_SUPPORT = 20,
        KMTQAITYPE_DLIST_DRIVER_NAME = 21,
        KMTQAITYPE_WDDM_1_3_CAPS = 22,
        KMTQAITYPE_MULTIPLANEOVERLAY_HUD_SUPPORT = 23,
        KMTQAITYPE_WDDM_2_0_CAPS = 24,
        KMTQAITYPE_NODEMETADATA = 25,
        KMTQAITYPE_CPDRIVERNAME = 26,
        KMTQAITYPE_XBOX = 27,
        KMTQAITYPE_INDEPENDENTFLIP_SUPPORT = 28,
        KMTQAITYPE_MIRACASTCOMPANIONDRIVERNAME = 29,
        KMTQAITYPE_PHYSICALADAPTERCOUNT = 30,
        KMTQAITYPE_PHYSICALADAPTERDEVICEIDS = 31,
        KMTQAITYPE_DRIVERCAPS_EXT = 32,
        KMTQAITYPE_QUERY_MIRACAST_DRIVER_TYPE = 33,
        KMTQAITYPE_QUERY_GPUMMU_CAPS = 34,
        KMTQAITYPE_QUERY_MULTIPLANEOVERLAY_DECODE_SUPPORT = 35,
        KMTQAITYPE_QUERY_HW_PROTECTION_TEARDOWN_COUNT = 36,
        KMTQAITYPE_QUERY_ISBADDRIVERFORHWPROTECTIONDISABLED = 37,
        KMTQAITYPE_MULTIPLANEOVERLAY_SECONDARY_SUPPORT = 38,
        KMTQAITYPE_INDEPENDENTFLIP_SECONDARY_SUPPORT = 39,
        KMTQAITYPE_PANELFITTER_SUPPORT = 40,
        KMTQAITYPE_PHYSICALADAPTERPNPKEY = 41,
        KMTQAITYPE_GETSEGMENTGROUPSIZE = 42,
        KMTQAITYPE_MPO3DDI_SUPPORT = 43,
        KMTQAITYPE_HWDRM_SUPPORT = 44,
        KMTQAITYPE_MPOKERNELCAPS_SUPPORT = 45,
        KMTQAITYPE_MULTIPLANEOVERLAY_STRETCH_SUPPORT = 46,
        KMTQAITYPE_GET_DEVICE_VIDPN_OWNERSHIP_INFO = 47,
        KMTQAITYPE_QUERYREGISTRY = 48,
        KMTQAITYPE_KMD_DRIVER_VERSION = 49,
        KMTQAITYPE_BLOCKLIST_KERNEL = 50,
        KMTQAITYPE_BLOCKLIST_RUNTIME = 51,
        KMTQAITYPE_ADAPTERGUID_RENDER = 52,
        KMTQAITYPE_ADAPTERADDRESS_RENDER = 53,
        KMTQAITYPE_ADAPTERREGISTRYINFO_RENDER = 54,
        KMTQAITYPE_CHECKDRIVERUPDATESTATUS_RENDER = 55,
        KMTQAITYPE_DRIVERVERSION_RENDER = 56,
        KMTQAITYPE_ADAPTERTYPE_RENDER = 57,
        KMTQAITYPE_WDDM_1_2_CAPS_RENDER = 58,
        KMTQAITYPE_WDDM_1_3_CAPS_RENDER = 59,
        KMTQAITYPE_QUERY_ADAPTER_UNIQUE_GUID = 60,
        KMTQAITYPE_NODEPERFDATA = 61,
        KMTQAITYPE_ADAPTERPERFDATA = 62,
        KMTQAITYPE_ADAPTERPERFDATA_CAPS = 63,
        KMTQUITYPE_GPUVERSION = 64,
        KMTQAITYPE_DRIVER_DESCRIPTION = 65,
        KMTQAITYPE_DRIVER_DESCRIPTION_RENDER = 66,
        KMTQAITYPE_SCANOUT_CAPS = 67,
        KMTQAITYPE_DISPLAY_UMDRIVERNAME = 71,
        KMTQAITYPE_PARAVIRTUALIZATION_RENDER = 68,
        KMTQAITYPE_SERVICENAME = 69,
        KMTQAITYPE_WDDM_2_7_CAPS = 70,
        KMTQAITYPE_TRACKEDWORKLOAD_SUPPORT = 72,
        KMTQAITYPE_HYBRID_DLIST_DLL_SUPPORT = 73,
        KMTQAITYPE_DISPLAY_CAPS = 74,
        KMTQAITYPE_WDDM_2_9_CAPS = 75,
        KMTQAITYPE_CROSSADAPTERRESOURCE_SUPPORT = 76,
        KMTQAITYPE_WDDM_3_0_CAPS = 77,
        KMTQAITYPE_WSAUMDIMAGENAME = 78,
        KMTQAITYPE_VGPUINTERFACEID = 79,
        KMTQAITYPE_WDDM_3_1_CAPS = 80,
    }
    public enum D3DKMT_QUERYSTATISTICS_TYPE : uint {
        D3DKMT_QUERYSTATISTICS_ADAPTER = 0,
        D3DKMT_QUERYSTATISTICS_PROCESS = 1,
        D3DKMT_QUERYSTATISTICS_PROCESS_ADAPTER = 2,
        D3DKMT_QUERYSTATISTICS_SEGMENT = 3,
        D3DKMT_QUERYSTATISTICS_PROCESS_SEGMENT = 4,
        D3DKMT_QUERYSTATISTICS_NODE = 5,
        D3DKMT_QUERYSTATISTICS_PROCESS_NODE = 6,
        D3DKMT_QUERYSTATISTICS_VIDPNSOURCE = 7,
        D3DKMT_QUERYSTATISTICS_PROCESS_VIDPNSOURCE = 8,
        D3DKMT_QUERYSTATISTICS_PROCESS_SEGMENT_GROUP = 9,
        D3DKMT_QUERYSTATISTICS_PHYSICAL_ADAPTER = 10,
        D3DKMT_QUERYSTATISTICS_ADAPTER2 = 11,
        D3DKMT_QUERYSTATISTICS_SEGMENT2 = 12,
        D3DKMT_QUERYSTATISTICS_PROCESS_ADAPTER2 = 13,
        D3DKMT_QUERYSTATISTICS_PROCESS_SEGMENT2 = 14,
        D3DKMT_QUERYSTATISTICS_PROCESS_SEGMENT_GROUP2 = 15,
        D3DKMT_QUERYSTATISTICS_SEGMENT_USAGE = 16,
        D3DKMT_QUERYSTATISTICS_SEGMENT_GROUP_USAGE = 17,
        D3DKMT_QUERYSTATISTICS_NODE2 = 18,
        D3DKMT_QUERYSTATISTICS_PROCESS_NODE2 = 19,
    }
    public enum D3DKMT_QUERYSTATISTICS_SEGMENT_TYPE : uint {
        D3DKMT_QUERYSTATISTICS_SEGMENT_TYPE_APERTURE = 0,
        D3DKMT_QUERYSTATISTICS_SEGMENT_TYPE_MEMORY = 1,
        D3DKMT_QUERYSTATISTICS_SEGMENT_TYPE_SYSMEM = 2
    }
    public enum D3DKMT_MEMORY_SEGMENT_GROUP : uint {
        D3DKMT_MEMORY_SEGMENT_GROUP_LOCAL = 0,
        D3DKMT_MEMORY_SEGMENT_GROUP_NON_LOCAL = 1
    }
    public enum DXGK_ENGINE_TYPE {
        DXGK_ENGINE_TYPE_OTHER = 0,
        DXGK_ENGINE_TYPE_3D = 1,
        DXGK_ENGINE_TYPE_VIDEO_DECODE = 2,
        DXGK_ENGINE_TYPE_VIDEO_ENCODE = 3,
        DXGK_ENGINE_TYPE_VIDEO_PROCESSING = 4,
        DXGK_ENGINE_TYPE_SCENE_ASSEMBLY = 5,
        DXGK_ENGINE_TYPE_COPY = 6,
        DXGK_ENGINE_TYPE_OVERLAY = 7,
        DXGK_ENGINE_TYPE_CRYPTO,
        DXGK_ENGINE_TYPE_MAX
    }

    [StructLayout(LayoutKind.Sequential)] public struct LUID {
        public uint LowPart;
        public int HighPart;
    }
    [StructLayout(LayoutKind.Sequential)] public struct D3DKMT_CLOSEADAPTER {
        public uint hAdapter;
    }
    [StructLayout(LayoutKind.Sequential)] public struct D3DKMT_ENUMADAPTERS {
        public uint NumAdapters;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] public D3DKMT_ADAPTERINFO[] Adapters;
    }
    [StructLayout(LayoutKind.Sequential)] public struct D3DKMT_ADAPTERINFO {
        public uint hAdapter;
        public LUID AdapterLuid;
        public uint NumOfSources;
        public bool bPresentMoveRegionsPreferred;
    }
    [StructLayout(LayoutKind.Sequential)] public struct D3DKMT_QUERYADAPTERINFO {
        public uint hAdapter;
        public KMTQUERYADAPTERINFOTYPE Type;
        public IntPtr pPrivateDriverData;
        public uint PrivateDriverDataSize;
    }
    [StructLayout(LayoutKind.Sequential)] public struct D3DKMT_QUERYVIDEOMEMORYINFO {
        public nuint hProcess;
        public uint hAdapter;
        public D3DKMT_MEMORY_SEGMENT_GROUP MemorySegmentGroup;
        public ulong Budget;
        public ulong CurrentUsage;
        public ulong CurrentReservation;
        public ulong AvailableForReservation;
        public uint PhysicalAdapterIndex;
    }
    [StructLayout(LayoutKind.Sequential)] public struct D3DKMT_QUERYSTATISTICS {
        public D3DKMT_QUERYSTATISTICS_TYPE Type;
        public LUID Luid;
        public nuint hProcess;
        public D3DKMT_QUERYSTATISTICS_RESULT QueryResult;
        public D3DKMT_QUERYSTATISTICS_QUERY_ELEMENT QueryElement;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)] public struct D3DKMT_QUERYSTATISTICS_RESULT {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 776)] public byte[] data;
        public readonly D3DKMT_QUERYSTATISTICS_ADAPTER_INFORMATION AdapterInformation => data.ToObject<D3DKMT_QUERYSTATISTICS_ADAPTER_INFORMATION>();
        public readonly D3DKMT_QUERYSTATISTICS_SEGMENT_INFORMATION SegmentInformation => data.ToObject<D3DKMT_QUERYSTATISTICS_SEGMENT_INFORMATION>();
        public readonly D3DKMT_QUERYSTATISTICS_NODE_INFORMATION NodeInformation => data.ToObject<D3DKMT_QUERYSTATISTICS_NODE_INFORMATION>();
    }
    [StructLayout(LayoutKind.Explicit)] public struct D3DKMT_QUERYSTATISTICS_QUERY_ELEMENT {
        [FieldOffset(0)] public uint SegmentId;
        [FieldOffset(0)] public uint NodeId;
        [FieldOffset(0)] public uint VidPnSourceId;
    }
    [StructLayout(LayoutKind.Sequential)] public struct D3DKMT_QUERYSTATISTICS_ADAPTER_INFORMATION {
        public uint NbSegments;
        public uint NodeCount;
        public uint VidPnSourceCount;
        public uint VSyncEnabled;
        public uint TdrDetectedCount;
        public long ZeroLengthDmaBuffers;
        public ulong RestartedPeriod;
        public short ReferenceDmaBuffer;
        public short Renaming;
        public short Preparation;
        public short PagingFault;
        public short PagingTransfer;
        public short SwizzlingRange;
        public short Locks;
        public short Allocations;
        public short Terminations;
        public short Flags;
    }
    [StructLayout(LayoutKind.Sequential)] public struct D3DKMT_QUERYSTATISTICS_SEGMENT_INFORMATION {
        public ulong CommitLimit;
        public ulong BytesCommitted;
        public ulong BytesResident;
        public ulong Memory_TotalBytesEvicted;
        public uint Memory_AllocsCommitted;
        public uint Memory_AllocsResident;
        public uint Aperture;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)] public ulong[] TotalBytesEvictedByPriority;
        public ulong SystemMemoryEndAddress;
        public ulong PowerFlags;
        public ulong SegmentProperties;
    }
    [StructLayout(LayoutKind.Sequential)] public unsafe struct D3DKMT_QUERYSTATISTICS_NODE_INFORMATION {
        public D3DKMT_QUERYSTATISTICS_PROCESS_NODE_INFORMATION GlobalInformation;
        public D3DKMT_QUERYSTATISTICS_PROCESS_NODE_INFORMATION SystemInformation;
        public fixed ulong Reserved[8];
    }
    [StructLayout(LayoutKind.Sequential)] public unsafe struct D3DKMT_QUERYSTATISTICS_PROCESS_NODE_INFORMATION {
        [MarshalAs(UnmanagedType.I8, SizeConst = 8)] public long RunningTime;
        public uint ContextSwitch;
    }
    [StructLayout(LayoutKind.Sequential)] public struct D3DKMT_ADAPTERTYPE {
        public uint AdapterType;
        public readonly bool RenderSupported => AdapterType.IsBitSet(0);
        public readonly bool DisplaySupported => AdapterType.IsBitSet(1);
        public readonly bool SoftwareDevice => AdapterType.IsBitSet(2);
        public readonly bool PostDevice => AdapterType.IsBitSet(3);
        public readonly bool HybridDiscrete => AdapterType.IsBitSet(4);
        public readonly bool HybridIntegrated => AdapterType.IsBitSet(5);
        public readonly bool IndirectDisplayDevice => AdapterType.IsBitSet(6);
        public readonly bool Paravirtualized => AdapterType.IsBitSet(7);
        public readonly bool ACGSupported => AdapterType.IsBitSet(8);
        public readonly bool SupportSetTimingsFromVidPn => AdapterType.IsBitSet(9);
        public readonly bool Detachable => AdapterType.IsBitSet(10);
        public readonly bool ComputeOnly => AdapterType.IsBitSet(11);
        public readonly bool Prototype => AdapterType.IsBitSet(12);
        public readonly bool RuntimePowerManagement => AdapterType.IsBitSet(13);
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)] public struct D3DKMT_ADAPTERREGISTRYINFO {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] public string AdapterString;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] public string BiosString;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] public string DacType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] public string ChipType;
    }
    [StructLayout(LayoutKind.Sequential)] public struct D3DKMT_ADAPTER_PERFDATA {
        public uint PhysicalAdapterIndex;
        public ulong MemoryFrequency;
        public ulong MaxMemoryFrequency;
        public ulong MaxMemoryFrequencyOC;
        public ulong MemoryBandwidth;
        public ulong PCIEBandwidth;
        public uint FanRPM;
        public uint Power;
        public uint Temperature;
        public byte PowerStateOverride;
    }
    [StructLayout(LayoutKind.Sequential)] public struct D3DKMT_SEGMENTSIZEINFO {
        public ulong DedicatedVideoMemorySize;
        public ulong DedicatedSystemMemorySize;
        public ulong SharedSystemMemorySize;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)] public struct D3DKMT_NODEMETADATA {
        public uint NodeOrdinalAndAdapterIndex;
        public DXGK_NODEMETADATA NodeData;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)] public struct DXGK_NODEMETADATA {
        public DXGK_ENGINE_TYPE EngineType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string FriendlyName;
        public uint Flags;
        public byte GpuMmuSupported;
        public byte IoMmuSupported;
    }

    [System.Security.SuppressUnmanagedCodeSecurity()] [DllImport("gdi32", ExactSpelling = true)]
    public static extern unsafe NTSTATUS D3DKMTCloseAdapter(ref D3DKMT_CLOSEADAPTER unnamedParam1);
    [System.Security.SuppressUnmanagedCodeSecurity()] [DllImport("gdi32", ExactSpelling = true)]
    public static extern unsafe NTSTATUS D3DKMTEnumAdapters(ref D3DKMT_ENUMADAPTERS unnamedParam1);
    [System.Security.SuppressUnmanagedCodeSecurity()] [DllImport("gdi32", ExactSpelling = true)]
    public static extern unsafe NTSTATUS D3DKMTQueryAdapterInfo(ref D3DKMT_QUERYADAPTERINFO unnamedParam1);
    [System.Security.SuppressUnmanagedCodeSecurity()] [DllImport("gdi32", ExactSpelling = true)]
    public static extern unsafe NTSTATUS D3DKMTQueryStatistics(ref D3DKMT_QUERYSTATISTICS unnamedParam1);
    [System.Security.SuppressUnmanagedCodeSecurity()] [DllImport("gdi32", ExactSpelling = true)]
    public static extern unsafe NTSTATUS D3DKMTQueryVideoMemoryInfo(ref D3DKMT_QUERYVIDEOMEMORYINFO unnamedParam1);

}
