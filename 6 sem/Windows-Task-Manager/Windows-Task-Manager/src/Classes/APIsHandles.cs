using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
namespace sMkTaskManager.Classes;

[SupportedOSPlatform("windows")]
internal unsafe static partial class Handles {
    const int MAX_PATH = 260;
    const int DUPLICATE_SAME_ACCESS = 0x2;
    const int CNST_SYSTEM_HANDLE_INFORMATION = 16;
    const uint STATUS_INFO_LENGTH_MISMATCH = 0xC0000004U;

    public static List<string> GetFilesLockedBy(Process process) {
        try {
            var retFiles = new List<string>();
            foreach (SYSTEM_HANDLE_INFORMATION h in GetHandles(process)) {
                try {
                    var File = GetFilePath(h, process);
                    if (File != null) {
                        retFiles.Add(File);
                    }
                } catch (Exception ex) { Shared.DebugTrap(ex, 1901); }
            }
            return retFiles;
        } catch (Exception ex) { Shared.DebugTrap(ex, 1902); return new List<string>(); }
    }
    public static List<SYSTEM_HANDLE_INFORMATION> GetHandles(Process process) {
        uint nStatus = 0;
        int nHandleInfoSize = 0x10000;
        IntPtr ipHandlePointer = Marshal.AllocHGlobal(nHandleInfoSize);
        int nLength = 0;

        while (InlineAssignHelper(ref nStatus, NtQuerySystemInformation(CNST_SYSTEM_HANDLE_INFORMATION, ipHandlePointer, nHandleInfoSize, ref nLength)) == STATUS_INFO_LENGTH_MISMATCH) {
            nHandleInfoSize = nLength;
            Marshal.FreeHGlobal(ipHandlePointer);
            ipHandlePointer = Marshal.AllocHGlobal(nLength);
        }

        long lHandleCount;
        IntPtr ipHandle;
        if (Shared.Is64Bits()) {
            lHandleCount = Marshal.ReadInt64(ipHandlePointer);
            ipHandle = new IntPtr(ipHandlePointer.ToInt64() + 8);
        } else {
            lHandleCount = Marshal.ReadInt32(ipHandlePointer);
            ipHandle = new IntPtr(ipHandlePointer.ToInt32() + 4);
        }

        List<SYSTEM_HANDLE_INFORMATION> lstHandles = new();
        for (long lIndex = 0; lIndex < lHandleCount; lIndex++) {
            SYSTEM_HANDLE_INFORMATION? shHandle = new();
            if (Shared.Is64Bits()) {
                shHandle = (SYSTEM_HANDLE_INFORMATION?)Marshal.PtrToStructure(ipHandle, shHandle.GetType());
                ipHandle = new IntPtr(ipHandle.ToInt64() + Marshal.SizeOf(shHandle) + 8);
            } else {
                ipHandle = new IntPtr(ipHandle.ToInt64() + Marshal.SizeOf(shHandle));
                shHandle = (SYSTEM_HANDLE_INFORMATION?)Marshal.PtrToStructure(ipHandle, shHandle.GetType());
            }
            if (shHandle == null) continue;
            if (shHandle?.ProcessID != process.Id) continue;
            lstHandles.Add(shHandle.Value);
        }
        Marshal.FreeHGlobal(ipHandlePointer);
        return lstHandles;
    }
    public static string? GetFilePath(SYSTEM_HANDLE_INFORMATION sYSTEM_HANDLE_INFORMATION, Process process) {
        if (sYSTEM_HANDLE_INFORMATION.GrantedAccess == 0x12019F || sYSTEM_HANDLE_INFORMATION.GrantedAccess == 0x1A019F || sYSTEM_HANDLE_INFORMATION.GrantedAccess == 0x120189 || sYSTEM_HANDLE_INFORMATION.GrantedAccess == 0x100000) {
            return null;
        }

        IntPtr m_ipProcessHwnd = OpenProcess(ProcessAccessFlags.All, false, process.Id);
        IntPtr ipHandle = IntPtr.Zero;
        var objBasic = new OBJECT_BASIC_INFORMATION();
        IntPtr ipBasic;
        var objObjectType = new OBJECT_TYPE_INFORMATION();
        IntPtr ipObjectType;
        var objObjectName = new OBJECT_NAME_INFORMATION();
        IntPtr ipObjectName;
        string strObjectName = "";
        int nLength = 0;
        int nReturn = 0;
        IntPtr ipTemp;

        if (!DuplicateHandle(m_ipProcessHwnd, sYSTEM_HANDLE_INFORMATION.Handle, GetCurrentProcess(), ref ipHandle, 0, false, DUPLICATE_SAME_ACCESS)) {
            return null;
        }

        ipBasic = Marshal.AllocHGlobal(Marshal.SizeOf(objBasic));
        _ = NtQueryObject(ipHandle, Convert.ToInt32(ObjectInformationClass.ObjectBasicInformation), ipBasic, Marshal.SizeOf(objBasic), ref nLength);
        objBasic = (OBJECT_BASIC_INFORMATION)Marshal.PtrToStructure(ipBasic, objBasic.GetType())!;
        Marshal.FreeHGlobal(ipBasic);

        ipObjectType = Marshal.AllocHGlobal(objBasic.TypeInformationLength);
        nLength = objBasic.TypeInformationLength;
        while (Convert.ToUInt32(InlineAssignHelper(ref nReturn, NtQueryObject(ipHandle, Convert.ToInt32(ObjectInformationClass.ObjectTypeInformation), ipObjectType, nLength, ref nLength))) == STATUS_INFO_LENGTH_MISMATCH) {
            Marshal.FreeHGlobal(ipObjectType);
            ipObjectType = Marshal.AllocHGlobal(nLength);
        }

        objObjectType = (OBJECT_TYPE_INFORMATION)Marshal.PtrToStructure(ipObjectType, objObjectType.GetType())!;
        if (Shared.Is64Bits()) {
            ipTemp = new IntPtr(Convert.ToInt64(objObjectType.Name.Buffer.ToString(), 10) >> 32);
        } else {
            ipTemp = objObjectType.Name.Buffer;
        }

        string strObjectTypeName = Marshal.PtrToStringUni(ipTemp, objObjectType.Name.Length >> 1);
        Marshal.FreeHGlobal(ipObjectType);
        if (strObjectTypeName != "File") { return null; }

        // nLength = objBasic.NameInformationLength
        nLength = 0x1000;
        ipObjectName = Marshal.AllocHGlobal(nLength);
        while (Convert.ToUInt32(InlineAssignHelper(ref nReturn, NtQueryObject(ipHandle, Convert.ToInt32(ObjectInformationClass.ObjectNameInformation), ipObjectName, nLength, ref nLength))) == STATUS_INFO_LENGTH_MISMATCH) {
            Marshal.FreeHGlobal(ipObjectName);
            ipObjectName = Marshal.AllocHGlobal(nLength);
        }
        objObjectName = (OBJECT_NAME_INFORMATION)Marshal.PtrToStructure(ipObjectName, objObjectName.GetType())!;

        if (Shared.Is64Bits()) {
            ipTemp = new IntPtr(Convert.ToInt64(objObjectName.Name.Buffer.ToString(), 10) >> 32);
        } else {
            ipTemp = objObjectName.Name.Buffer;
        }
        Marshal.FreeHGlobal(ipObjectName);

        if (ipTemp != IntPtr.Zero) {
            byte[] baTemp = new byte[nLength];
            try {
                Marshal.Copy(ipTemp, baTemp, 0, nLength);
                strObjectName = Marshal.PtrToStringUni(Shared.Is64Bits() ? new IntPtr(ipTemp.ToInt64()) : new IntPtr(ipTemp.ToInt32()))!;
            } catch (AccessViolationException) {
                return null;
            } finally {
                _ = CloseHandle(ipHandle);
            }
        }

        return GetRegularFileNameFromDevice(strObjectName);

    }
    public static T InlineAssignHelper<T>(ref T target, T value) {
        target = value;
        return value;
    }
    public static string GetRegularFileNameFromDevice(string strRawName) {
        string strFileName = strRawName;
        foreach (string strDrivePath in Environment.GetLogicalDrives()) {
            StringBuilder sbTargetPath = new(MAX_PATH);
            if (QueryDosDevice(strDrivePath[..2], sbTargetPath, MAX_PATH) == 0) {
                return strRawName;
            }
            string strTargetPath = sbTargetPath.ToString();
            if (strFileName.StartsWith(strTargetPath)) {
                strFileName = strFileName.Replace(strTargetPath, strDrivePath[..2]);
                break;
            }
        }
        return strFileName;
    }
    public static string GetRegularKeyName(string strInternalKey) {
        strInternalKey = strInternalKey.Replace("\\REGISTRY\\MACHINE\\SYSTEM\\CURRENTCONTROLSET\\HARDWARE PROFILES\\CURRENT", "HKCC");
        strInternalKey = strInternalKey.Replace("\\REGISTRY\\MACHINE\\SOFTWARE\\CLASSES", "HKCR");
        strInternalKey = strInternalKey.Replace("\\REGISTRY\\USER\\S", "HKU\\S");
        strInternalKey = strInternalKey.Replace("\\REGISTRY\\MACHINE", "HKLM");
        strInternalKey = strInternalKey.Replace("\\REGISTRY\\USER", "HKCU");
        return strInternalKey;
    }

    public enum ObjectInformationClass : int {
        ObjectBasicInformation = 0,
        ObjectNameInformation = 1,
        ObjectTypeInformation = 2,
        ObjectAllTypesInformation = 3,
        ObjectHandleInformation = 4
    }
    [Flags()] public enum ProcessAccessFlags : uint {
        All = 0x1F0FFF,
        Terminate = 0x1,
        CreateThread = 0x2,
        VMOperation = 0x8,
        VMRead = 0x10,
        VMWrite = 0x20,
        DupHandle = 0x40,
        SetInformation = 0x200,
        QueryInformation = 0x400,
        Synchronize = 0x100000
    }

    [StructLayout(LayoutKind.Sequential)] public struct OBJECT_BASIC_INFORMATION {
        public int Attributes;
        public int GrantedAccess;
        public int HandleCount;
        public int PointerCount;
        public int PagedPoolUsage;
        public int NonPagedPoolUsage;
        public int Reserved1;
        public int Reserved2;
        public int Reserved3;
        public int NameInformationLength;
        public int TypeInformationLength;
        public int SecurityDescriptorLength;
        public System.Runtime.InteropServices.ComTypes.FILETIME CreateTime;
    }
    [StructLayout(LayoutKind.Sequential)] public struct OBJECT_TYPE_INFORMATION {
        public UNICODE_STRING Name;
        public int TotalNumberOfObjects;
        public int TotalNumberOfHandles;
        public int TotalPagedPoolUsage;
        public int TotalNonPagedPoolUsage;
        public int TotalNamePoolUsage;
        public int TotalHandleTableUsage;
        public int HighWaterNumberOfObjects;
        public int HighWaterNumberOfHandles;
        public int HighWaterPagedPoolUsage;
        public int HighWaterNonPagedPoolUsage;
        public int HighWaterNamePoolUsage;
        public int HighWaterHandleTableUsage;
        public int InvalidAttributes;
        public GENERIC_MAPPING GenericMapping;
        public int ValidAccessMask;
        public bool SecurityRequired;
        public bool MaintainHandleCount;
        public int PoolType;
        public int DefaultPagedPoolCharge;
        public int DefaultNonPagedPoolCharge;
    }
    [StructLayout(LayoutKind.Sequential)] public struct OBJECT_NAME_INFORMATION {
        public UNICODE_STRING Name;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)] public struct UNICODE_STRING {
        public ushort Length;
        public ushort MaximumLength;
        public IntPtr Buffer;
    }
    [StructLayout(LayoutKind.Sequential)] public struct GENERIC_MAPPING {
        public int GenericRead;
        public int GenericWrite;
        public int GenericExecute;
        public int GenericAll;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)] public struct SYSTEM_HANDLE_INFORMATION {
        public int ProcessID;
        public byte ObjectTypeNumber;
        public byte Flags;
        public ushort Handle;
        public int Object_Pointer;
        public uint GrantedAccess;
    }

    [System.Security.SuppressUnmanagedCodeSecurity()] [DllImport("ntdll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int NtQueryObject(IntPtr ObjectHandle, int ObjectInformationClass, IntPtr ObjectInformation, int ObjectInformationLength, ref int returnLength);
    [System.Security.SuppressUnmanagedCodeSecurity()] [DllImport("ntdll.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern uint NtQuerySystemInformation(int SystemInformationClass, IntPtr SystemInformation, int SystemInformationLength, ref int returnLength);
    [System.Security.SuppressUnmanagedCodeSecurity()] [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern uint QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, int ucchMax);
    [System.Security.SuppressUnmanagedCodeSecurity()] [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr GetCurrentProcess();
    [System.Security.SuppressUnmanagedCodeSecurity()] [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);
    [System.Security.SuppressUnmanagedCodeSecurity()] [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern bool DuplicateHandle(IntPtr hSourceProcessHandle, ushort hSourceHandle, IntPtr hTargetProcessHandle, ref IntPtr lpTargetHandle, uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwOptions);
    [System.Security.SuppressUnmanagedCodeSecurity()] [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int CloseHandle(IntPtr hObject);

}
