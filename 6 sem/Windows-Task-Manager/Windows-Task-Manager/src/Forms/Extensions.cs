using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Principal;

namespace sMkTaskManager;

[SupportedOSPlatform("windows")]
internal static class Extensions {
    public static Random RandomGenerator = new();

    // Friendly functions to make it easier add items and separators to menus
    public static ToolStripMenuItem AddMenuItem(this ToolStripItemCollection parent, string text) => (ToolStripMenuItem)parent.Add(text);
    public static int AddMenuItem(this ToolStripItemCollection parent, string text, string name) => parent.Add(new ToolStripMenuItem(text, null, null, name));
    public static ToolStripSeparator AddSeparator(this ToolStripItemCollection parent) {
        var separator = new ToolStripSeparator();
        parent.Add(separator);
        return separator;
    }
    public static ToolStripSeparator AddSeparator(this ToolStripItemCollection parent, string name) {
        var separator = new ToolStripSeparator() { Name = name };
        parent.Add(separator);
        return separator;
    }

    public static void SetDoubleBuffered(Control c) {
        if (SystemInformation.TerminalServerSession) return;
        PropertyInfo? aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
        aProp?.SetValue(c, true, null);
    }
    public static void CascadingDoubleBuffer(Control c) {
        var p = c.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
        p?.SetValue(c, true, null);
        foreach (Control cc in c.Controls) CascadingDoubleBuffer(cc);
    }

    public static void StartMeasure(Stopwatch? tmr) {
        if (!Debugger.IsAttached) return;
        tmr ??= new();
        tmr.Start();
    }
    public static void StopMeasure(Stopwatch? tmr, [System.Runtime.CompilerServices.CallerMemberName] string methodName = "") {
        if (!Debugger.IsAttached) return;
        tmr?.Stop();
        Debug.WriteLine($"- {methodName} Time: {tmr?.ElapsedMilliseconds}ms.");
    }

    public static bool IsAdminRole() {
        try {
            using WindowsIdentity identity = WindowsIdentity.GetCurrent();
            return (new WindowsPrincipal(identity)).IsInRole(WindowsBuiltInRole.Administrator);
        } catch {
            return false;
        }
    }
    public static bool IsWindows10OrGreater(int build = -1) {
        return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
    }
    public static bool UseImmersiveDarkMode(IntPtr handle, bool enabled) {
        var DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
        var DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
        if (IsWindows10OrGreater(17763)) {
            var attribute = DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
            if (IsWindows10OrGreater(18985)) { attribute = DWMWA_USE_IMMERSIVE_DARK_MODE; }

            int useImmersiveDarkMode = enabled ? 1 : 0;
            return DwmSetWindowAttribute(handle, attribute, ref useImmersiveDarkMode, sizeof(int)) == 0;
        }
        return false;
    }
    public static bool UseImmersiveRoundCorner(IntPtr handle, int DWM_WINDOW_CORNER_PREFERENCE) {
        var DWMWA_WINDOW_CORNER_PREFERENCE = 33;
        if (IsWindows10OrGreater(17763)) {
            return DwmSetWindowAttribute(handle, DWMWA_WINDOW_CORNER_PREFERENCE, ref DWM_WINDOW_CORNER_PREFERENCE, sizeof(int)) == 0;
        }
        return false;
    }

    public static void SwitchToBold(this Label c) { if (c.Font.Style != FontStyle.Bold) c.Font = new Font(c.Font, FontStyle.Bold); }
    public static void SwitchToBold(this TextBox c) { if (c.Font.Style != FontStyle.Bold) c.Font = new Font(c.Font, FontStyle.Bold); }
    public static void SwitchToBold(this ToolStripMenuItem c) { if (c.Font.Style != FontStyle.Bold) c.Font = new Font(c.Font, FontStyle.Bold); }
    public static void SwitchToBold(this Control c) { if (c.Font.Style != FontStyle.Bold) c.Font = new Font(c.Font, FontStyle.Bold); }

    [DllImport("dwmapi.dll")]
    public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

}
