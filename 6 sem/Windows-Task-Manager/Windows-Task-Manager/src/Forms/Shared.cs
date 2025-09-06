using sMkTaskManager.Classes;
using sMkTaskManager.Forms;
using System.Diagnostics;
using System.Runtime.Versioning;
namespace sMkTaskManager;

[SupportedOSPlatform("windows")]
internal static partial class Shared {

    internal static frmMain MainForm;
    internal static bool InitComplete = false;
    internal static bool LoadComplete = false;
    internal static bool RealExit = true;
    internal static Process CurrentProcess = Process.GetCurrentProcess();
    internal static int CurrentSessionID = Process.GetCurrentProcess().SessionId;
    internal static uint PrivateMsgID = 0;
    internal static int CpuLoad = 0;
    internal static ImageList ilProcesses = new() { ColorDepth = ColorDepth.Depth32Bit, ImageSize = new Size(16, 16), TransparentColor = Color.Transparent };

    internal static void SetStatusText(string value = "", ToolStripLabel? obj = null) {
        if (MainForm == null) FindOwnerForm();
        MainForm?.SetStatusText(value, obj);
    }
    internal static void FindOwnerForm() {
        if (MainForm == null) {
            foreach (Form f in Application.OpenForms) {
                if (f.Name == "frmMain") { MainForm = (frmMain)f; break; }
            }
        }
    }
    internal static void OpenProcessForm(int PID) {
        if (MainForm == null) FindOwnerForm();
        if (MainForm == null) return;
        bool xFound = false;
        MainForm.Cursor = Cursors.WaitCursor;
        foreach (Form child in Application.OpenForms) {
            if (child == null || child.Name == null) continue;
            if (child.Name.Equals("frmProc-" + PID)) {
                xFound = true;
                child.Focus(); child.Show();
                if (child.WindowState == FormWindowState.Minimized) child.WindowState = FormWindowState.Normal;
                break;
            }
        }
        if (!xFound) {
            frmProcess_Details frm = new() {
                Name = "frmProc-" + PID,
                Tag = PID,
                PID = PID,
                TopMost = false,
                ShowInTaskbar = true
            };
            frm.Show();
        }
        MainForm.Cursor = Cursors.Default;
    }
    internal static bool BusyCursor {
        get {
            return !(MainForm?.Cursor == Cursors.Default);
        }
        set {
            if (MainForm == null) FindOwnerForm();
            if (MainForm == null) return;
            MainForm.Cursor = value ? Cursors.AppStarting : Cursors.Default;
        }
    }
    internal static List<Color> TimmingsColors = new() {
        Color.FromArgb(255, 192, 192), Color.FromArgb(255, 224, 192), Color.FromArgb(255, 255, 192),
        Color.FromArgb(192, 255, 192), Color.FromArgb(192, 255, 255), Color.FromArgb(192, 192, 255),
        Color.FromArgb(255, 192, 255), Color.FromArgb(192, 192, 192)
    };
    internal static string ProcessIconImageKey(int PID, string processName, string? processExecutable = null) {
        string imageIdentifier = PID + "-" + processName;
        if (ilProcesses.Images.ContainsKey(imageIdentifier)) return imageIdentifier;
        try {
            if (string.IsNullOrEmpty(processExecutable)) {
                processExecutable = Process.GetProcessById(PID).MainModule?.FileName;
            }
            // If we cannot get the filename or it doesnt exist then set a black icon.
            if (string.IsNullOrEmpty(processExecutable) || !File.Exists(processExecutable)) {
                ilProcesses.Images.Add(imageIdentifier, Resources.Resources.Process_Black);
                return imageIdentifier;
            }
            // Otherwise try to extract the icon from the executable, if fail, set a white icon.
            IntPtr[] IconPtr = new IntPtr[1];
            if (API.ExtractIconEx(processExecutable, 0, null, IconPtr, 1) > 0) {
                ilProcesses.Images.Add(imageIdentifier, Icon.FromHandle(IconPtr[0]));
                API.DestroyIcon(IconPtr[0]);
            } else {
                ilProcesses.Images.Add(imageIdentifier, Resources.Resources.Process_Info);
            }
        } catch (Exception ex) {
            Debug.WriteLine("Failed GetProcessIcon for PID {0}: {1}", PID, ex.Message);
            if (!ilProcesses.Images.ContainsKey(imageIdentifier)) ilProcesses.Images.Add(imageIdentifier, Resources.Resources.Process_Black);
        }
        return imageIdentifier;
    }
    internal static void ShrinkMainProcess() {
        try {
            CurrentProcess.MaxWorkingSet = CurrentProcess.MaxWorkingSet;
        } catch (Exception ex) {
            Debug.WriteLine("Warning: ShrinkMainProcess Said {0}", ex.Message);
        }
    }
    internal static bool CheckExistingInstance(Process curProcess) {
        foreach (string arg in Environment.GetCommandLineArgs()) {
            if (arg.ToUpper() == "/NEW" || arg.ToUpper() == "NEW") { return false; }
            if (arg.ToUpper() == "/NEWINSTANCE" || arg.ToUpper() == "NEWINSTANCE") { return false; }
        }
        var myName = Path.GetFileName(curProcess.MainModule!.FileName).Replace(".exe", "");
        foreach (Process p in Process.GetProcessesByName(myName)) {
            try {
                if (p.Id == curProcess.Id || p.HasExited || p.SessionId != curProcess.SessionId || p.MainModule == null) continue;
                if (p.MainModule.FileName == curProcess.MainModule!.FileName) {
                    Debug.WriteLine("Im a second instance of: " + p.MainModule.FileName);
                    int BSF_IGNORECURRENTTASK = 0x2;
                    int BSF_POSTMESSAGE = 0x10;
                    int BSM_APPLICATIONS = 0x8;
                    PrivateMsgID = API.RegisterWindowMessage(Application.ExecutablePath.Replace(@"\", "_"));
                    _ = API.BroadcastSystemMessage(BSF_IGNORECURRENTTASK | BSF_POSTMESSAGE, ref BSM_APPLICATIONS, PrivateMsgID, 1, 0);
                    if (p.MainWindowHandle > 0) API.SetForegroundWindow(p.MainWindowHandle);
                    return true;
                }
            } catch { }
        }
        return false;
    }
    internal static void BroadcastRestoreInstance() {
        int BSF_IGNORECURRENTTASK = 0x2;
        int BSF_POSTMESSAGE = 0x10;
        int BSM_APPLICATIONS = 0x8;
        PrivateMsgID = API.RegisterWindowMessage(Application.ExecutablePath.Replace(@"\", "_"));
        Debug.WriteLine($"Broadcasting Message to {PrivateMsgID}.");
        _ = API.BroadcastSystemMessage(BSF_IGNORECURRENTTASK | BSF_POSTMESSAGE, ref BSM_APPLICATIONS, PrivateMsgID, 1, 0);
    }

}

[SupportedOSPlatform("windows")]
internal static class Tabs {
    internal static Dictionary<string, ITaskManagerTab> Tab = new();
    internal static T? GetTab<T>() {
        foreach (KeyValuePair<string, ITaskManagerTab> t in Tab) {
            if (t.Value.GetType() == typeof(T)) { return (T)t.Value; }
        }
        return default;
    }
    internal static ITaskManagerTab? GetTab(string tabKey) {
        return Tab.FirstOrDefault(t => t.Key == tabKey).Value;
    }
    internal static bool Contains(string tabKey) {
        return Tab.ContainsKey(tabKey);
    }
    internal static ITaskManagerTab? ActiveTab {
        get {
            foreach (ITaskManagerTab t in Tab.Values) {
                if (((Control)t).Parent == null) continue;
                if (((Control)t).Parent!.Visible) return t;
            }
            return default;
        }
    }
}