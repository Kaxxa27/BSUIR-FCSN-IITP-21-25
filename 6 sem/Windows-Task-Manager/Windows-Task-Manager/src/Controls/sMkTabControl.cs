using System.Runtime.Versioning;
namespace sMkTaskManager.Controls;

[SupportedOSPlatform("windows")]
public class sMkTabControl : TabControl {
    private const int TCM_SETMINTABWIDTH = 0x1300 + 49;

    public sMkTabControl() {
        HandleCreated += (o, e) => SendMessage((o as TabControl)!.Handle, TCM_SETMINTABWIDTH, IntPtr.Zero, 0);
    }

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern unsafe IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wp, IntPtr lp);
}
