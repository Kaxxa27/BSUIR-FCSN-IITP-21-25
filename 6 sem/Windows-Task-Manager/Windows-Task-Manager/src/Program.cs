using sMkTaskManager.Forms;
namespace sMkTaskManager;

[System.Runtime.Versioning.SupportedOSPlatform("windows")]
internal static class Program {

    [STAThread]
    static void Main() {
        // Create a Mutex for Single Stance run, Local Session
        Mutex mutex = new(true, @"Local\sMkTaskManager", out bool firstInstance);
        GC.KeepAlive(mutex);
        // Override this with possible command line arguments.
        foreach (string arg in Environment.GetCommandLineArgs()) {
            firstInstance |= arg.ToUpper() == "/NEW" || arg.ToUpper() == "NEW";
            firstInstance |= arg.ToUpper() == "/NEWINSTANCE" || arg.ToUpper() == "NEWINSTANCE";
        }
        // Try to bring up the existing instance
        if (!firstInstance) { Shared.BroadcastRestoreInstance(); return; }

        // Otherwise continue with normal application startup
        ApplicationConfiguration.Initialize();
        // Check if we have admin privileges to run or not
        if (!Extensions.IsAdminRole()) {
            var str = "This application requires Elevated Privileges to function." + "\r\n" +
            "Running without them will cause the application to become unstable or crash." + "\r\n\r\n" +
            "Do you want to continue running this application anyway?";
            if (MessageBox.Show(str, "Required Elevated Privileges", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) {
                Application.Exit();
            }
        }
        // Spawn the main form.
        Application.Run(new frmMain());
    }
}
