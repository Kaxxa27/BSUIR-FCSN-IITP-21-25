using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using static sMkTaskManager.Classes.API;
namespace sMkTaskManager.Classes;

[SupportedOSPlatform("windows")]
internal class TaskManagerDevice : IComparable, IComparable<TaskManagerDevice> {
    public TaskManagerDevice(TaskManagerDeviceClass cls, string name, SP_DEVINFO_DATA data, bool connected) {
        Class = cls;
        Name = name;
        Data = data;
        ImageKey = data.ClassGuid + "-" + data.DevInst;
        Present = connected;
    }

    public string Name { get; }
    public TaskManagerDeviceClass Class { get; }
    public SP_DEVINFO_DATA Data { get; }
    public bool Present { get; }
    public bool Disabled { get => ProblemCode == 22; }
    public string InstanceID { get; set; } = "";
    public string Service { get; set; } = "";
    public int ProblemCode { get; set; } = 0;
    public string ImageKey { get; }
    public override string ToString() => Name;

    int IComparable.CompareTo(object? obj) => CompareTo(obj as TaskManagerDevice);
    public int CompareTo(TaskManagerDevice? other) => Name.CompareTo(other?.Name);

}

[SupportedOSPlatform("windows")]
internal class TaskManagerDeviceClass : IComparable, IComparable<TaskManagerDeviceClass> {
    private static readonly DEVPROPKEY DEVPKEY_Device_DeviceDesc = new("a45c254e-df1c-4efd-8020-67d146a850e0", 2);
    private static readonly DEVPROPKEY DEVPKEY_Device_Service = new("a45c254e-df1c-4efd-8020-67d146a850e0", 6);
    private static readonly DEVPROPKEY DEVPKEY_Device_ClassGuid = new("a45c254e-df1c-4efd-8020-67d146a850e0", 10);
    private static readonly DEVPROPKEY DEVPKEY_Device_FriendlyName = new("a45c254e-df1c-4efd-8020-67d146a850e0", 14);
    private static readonly DEVPROPKEY DEVPKEY_Device_InstallState = new("a45c254e-df1c-4efd-8020-67d146a850e0", 36);
    private static readonly DEVPROPKEY DEVPKEY_DeviceClass_IconPath = new("259abffc-50a7-47ce-af08-68c9a7d73366", 12);
    private static readonly DEVPROPKEY DEVPKEY_Device_ProblemCode = new("4340a6c5-93fa-4706-972c-7b648008a5a7", 3);
    private static readonly DEVPROPKEY DEVPKEY_Device_InstanceId = new("78c34fc8-104a-4aca-9ea4-524d52996e57", 256);
    private readonly List<TaskManagerDevice> _devices = new();

    public TaskManagerDeviceClass(Guid classId, string description, string iconPath) {
        ClassId = classId;
        Description = description;
        IconPath = iconPath;
    }

    public Guid ClassId { get; }
    public string Description { get; }
    public string IconPath { get; private set; }
    public IReadOnlyList<TaskManagerDevice> Devices => _devices;

    int IComparable.CompareTo(object? obj) => CompareTo(obj as TaskManagerDeviceClass);
    public int CompareTo(TaskManagerDeviceClass? other) => Description.CompareTo(other?.Description);

    public static IReadOnlyList<TaskManagerDeviceClass> Load(TaskManagerDeviceFilter fiter, ref ImageList il) {
        var list = new List<TaskManagerDeviceClass>();
        var hdevinfo = SetupDiGetClassDevs(IntPtr.Zero, null, IntPtr.Zero, fiter);

        try {
            var data = new SP_DEVINFO_DATA { cbSize = Marshal.SizeOf<SP_DEVINFO_DATA>() };
            int index = 0;
            while (SetupDiEnumDeviceInfo(hdevinfo, index, ref data)) {
                index++;
                var classId = GetGuidProperty(hdevinfo, ref data, DEVPKEY_Device_ClassGuid);
                if (classId == Guid.Empty) continue;
                var cls = list.FirstOrDefault(c => c.ClassId == classId);
                if (cls == null) {
                    string classDescription = GetClassDescription(classId);
                    string classIconPath = GetStringProperty(classId, DEVPKEY_DeviceClass_IconPath);
                    cls = new TaskManagerDeviceClass(classId, classDescription, classIconPath);
                    list.Add(cls);
                }

                string name = GetStringProperty(hdevinfo, ref data, DEVPKEY_Device_FriendlyName);
                if (string.IsNullOrWhiteSpace(name)) { name = GetStringProperty(hdevinfo, ref data, DEVPKEY_Device_DeviceDesc); }
                int state = GetIntProperty(hdevinfo, ref data, DEVPKEY_Device_InstallState);

                var dev = new TaskManagerDevice(cls, name, data, state == 0) {
                    Service = GetStringProperty(hdevinfo, ref data, DEVPKEY_Device_Service),
                    InstanceID = GetStringProperty(hdevinfo, ref data, DEVPKEY_Device_InstanceId),
                    ProblemCode = GetIntProperty(hdevinfo, ref data, DEVPKEY_Device_ProblemCode)
                };
                if (il.Images.ContainsKey(dev.ImageKey)) {
                    // We better overwrite the images each time
                    il.Images.RemoveByKey(dev.ImageKey);
                }
                if (!il.Images.ContainsKey(dev.ImageKey)) {
                    SetupDiLoadDeviceIcon(hdevinfo, ref data, 16, 16, 0, out IntPtr devIcon);
                    if (devIcon != IntPtr.Zero) {
                        // We need to dim the icon if state!=0 
                        if (dev.Present) {
                            il.Images.Add(dev.ImageKey, Icon.FromHandle(devIcon));
                        } else {
                            Icon icn = Icon.FromHandle(devIcon);
                            il.Images.Add(dev.ImageKey, SetImageOpacity(icn.ToBitmap(), 0.60F));
                            icn.Dispose();
                        }
                    } else {
                        // Draw a generic icon 
                        il.Images.Add(dev.ImageKey, new Bitmap(16, 16));
                    }
                    if (devIcon != IntPtr.Zero) { DestroyIcon(devIcon); }
                    // We need to check if down, overlay icon
                    if (dev.ProblemCode > 0 && il.Images[dev.ImageKey] != null) {
                        var icn = il.Images[dev.ImageKey]!;
                        var g = Graphics.FromImage(icn);
                        if (dev.ProblemCode == 22) {
                            g.DrawImage(Resources.Resources.Device_Down, 0, 0);
                        } else {
                            g.DrawImage(Resources.Resources.Device_Warning, 0, 0);
                        }
                        il.Images.RemoveByKey(dev.ImageKey);
                        il.Images.Add(dev.ImageKey, icn);
                        g.Dispose(); icn.Dispose();
                    }

                }
                cls._devices.Add(dev);
            }
        } finally {
            if (hdevinfo != IntPtr.Zero) { SetupDiDestroyDeviceInfoList(hdevinfo); }
        }

        foreach (var cls in list) {
            cls._devices.Sort();
        }
        list.Sort();
        return list;
    }
    private static string GetClassDescription(Guid classId) {
        SetupDiGetClassDescription(ref classId, IntPtr.Zero, 0, out int size);
        if (size == 0) return string.Empty;
        var ptr = Marshal.AllocCoTaskMem(size * 2);
        try {
            if (SetupDiGetClassDescription(ref classId, ptr, size, out size)) {
                return Marshal.PtrToStringUni(ptr, size - 1);
            }
        } finally { Marshal.FreeCoTaskMem(ptr); }
        return string.Empty;
    }
    private static string GetStringProperty(IntPtr hdevinfo, ref SP_DEVINFO_DATA data, DEVPROPKEY pk) {
        SetupDiGetDeviceProperty(hdevinfo, ref data, ref pk, out int _, IntPtr.Zero, 0, out int size, 0);
        if (size == 0) return string.Empty;
        var ptr = Marshal.AllocCoTaskMem(size);
        try {
            if (SetupDiGetDeviceProperty(hdevinfo, ref data, ref pk, out int propertyType, ptr, size, out size, 0)) {
                return Marshal.PtrToStringUni(ptr, (size / 2) - 1);
            }
        } finally { Marshal.FreeCoTaskMem(ptr); }
        return string.Empty;
    }
    private static int GetIntProperty(IntPtr hdevinfo, ref SP_DEVINFO_DATA data, DEVPROPKEY pk) {
        SetupDiGetDeviceProperty(hdevinfo, ref data, ref pk, out int _, IntPtr.Zero, 0, out int size, 0);
        if (size == 0) { return -2; } // NOT FOUND 
        var ptr = Marshal.AllocCoTaskMem(size);
        try {
            if (SetupDiGetDeviceProperty(hdevinfo, ref data, ref pk, out int propertyType, ptr, size, out size, 0)) {
                return Marshal.ReadInt32(ptr);
            }
        } finally { Marshal.FreeCoTaskMem(ptr); }
        return -1; // NOT SUPPORTED
    }
    private static Guid GetGuidProperty(IntPtr hdevinfo, ref SP_DEVINFO_DATA data, DEVPROPKEY pk) {
        SetupDiGetDeviceProperty(hdevinfo, ref data, ref pk, out int _, out Guid guid, 16, out int _, 0);
        return guid;
    }
    private static string GetStringProperty(Guid ClassGuid, DEVPROPKEY pk) {
        SetupDiGetClassProperty(ClassGuid, ref pk, out int _, IntPtr.Zero, 0, out int size, 1);
        if (size == 0) { return string.Empty; }
        var ptr = Marshal.AllocCoTaskMem(size);
        try {
            if (SetupDiGetClassProperty(ClassGuid, ref pk, out int propertyType, ptr, size, out size, 1)) {
                return Marshal.PtrToStringUni(ptr, (size / 2) - 1);
            }
        } finally { Marshal.FreeCoTaskMem(ptr); }
        return string.Empty;
    }
    private static Image SetImageOpacity(Image image, float opacity) {
        try {
            Bitmap bmp = new(image.Width, image.Height);
            using (Graphics gfx = Graphics.FromImage(bmp)) {
                ColorMatrix matrix = new() { Matrix33 = opacity };
                ImageAttributes attributes = new();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
            }
            return bmp;
        } catch { return image; }
    }

}

[Flags]
internal enum TaskManagerDeviceFilter {
    Default = 1,
    Present = 2,
    AllClasses = 4,
    Profile = 8,
    DeviceInterface = 16
}