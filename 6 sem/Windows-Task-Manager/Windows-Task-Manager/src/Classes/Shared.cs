using System.Diagnostics;
using System.Runtime.Versioning;
using System.Runtime.CompilerServices;
using System.Globalization;
using sMkTaskManager.Classes;
namespace sMkTaskManager;

[SupportedOSPlatform("windows")]
internal static partial class Shared {
    private static string _SystemAccount = "";

    public static int bpi = 20; // Base PID Ignore
    public static List<string> skipProcess = new(new[] { "audiodg" });
    public static List<string> skipServices = new();
    public static string TotalProcessorsBin = "".PadLeft(Environment.ProcessorCount, '1');
    public static string DebuggerCmd = "";
    public static TaskManagetETW ETW = new();

    public static bool IsNumeric(string value) => double.TryParse(value, out _);
    public static bool IsNumeric(this object value) => double.TryParse(Convert.ToString(value), out _);
    public static bool IsInteger(string value) => value.All(char.IsNumber);
    public static bool IsInteger(this object value) => Convert.ToString(value)!.All(char.IsNumber);
    public static bool IsBetween<T>(this T value, T min, T max) where T : IComparable<T> => (min.CompareTo(value) <= 0) && (value.CompareTo(max) <= 0);

    public static string ToTitleCase(string text) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
    public static string TimeSpanToElapsed(TimeSpan lpTimeSpan) {
        return string.Format("{0,3:D2}:{1,2:D2}:{2,2:D2}", Convert.ToInt32(lpTimeSpan.Hours + (Math.Floor(lpTimeSpan.TotalDays) * 24)), lpTimeSpan.Minutes, lpTimeSpan.Seconds);
    }
    public static string TimeDiff(long startTime, short Format = 1) {
        TimeSpan upX = new(DateTime.Now.Ticks - startTime);
        return Format switch {
            1 => string.Format("{0}d {1,2:D2}:{2,2:D2}:{3,2:D2}", upX.Days, upX.Hours, upX.Minutes, upX.Seconds),
            2 => string.Format("{0}d {1,2:D2}h {2,2:D2}m", upX.Days, upX.Hours, upX.Minutes),
            3 => string.Format("{0}d {1,2:D2}h {2,2:D2}m {3,2:D2}s", upX.Days, upX.Hours, upX.Minutes, upX.Seconds),
            _ => "",
        };
    }

    public static void NotImplemented([CallerMemberName] string feature = "") {
        MessageBox.Show("This feature is not implemented yet.", feature, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    public static void DebugTrap(Exception e, int Code = 0, [CallerMemberName] string Method = "") {
        Debug.WriteLine($"*** Error at `{Method}` - Code: {Code} - Threw: {e.Message}");
        Debug.WriteLine(e.ToString());
    }

    public static string GetSystemAccount() {
        if (_SystemAccount != "") return _SystemAccount;
        try {
            System.Security.Principal.SecurityIdentifier sid = new(System.Security.Principal.WellKnownSidType.LocalSystemSid, null);
            _SystemAccount = sid.Translate(typeof(System.Security.Principal.NTAccount)).ToString();
            if (_SystemAccount.IndexOf("\\") > 0) _SystemAccount = _SystemAccount[(_SystemAccount.LastIndexOf("\\") + 1)..];
            _SystemAccount = ToTitleCase(_SystemAccount);
            Debug.WriteLine("Getting System Account... Result: " + _SystemAccount);
        } catch (Exception ex) {
            DebugTrap(ex); _SystemAccount = "Error";
        }
        return _SystemAccount!;
    }
    public static void GetDebuggerCmd() {
        string SubKey = "Software\\Microsoft\\Windows NT\\CurrentVersion\\AeDebug";
        string _Debugger = "";
        Microsoft.Win32.RegistryKey ParentKey = Microsoft.Win32.Registry.LocalMachine;
        try {
            Microsoft.Win32.RegistryKey? Key = ParentKey.OpenSubKey(SubKey, false);
            if (Key != null && Key.GetValue("Debugger") != null && !string.IsNullOrEmpty(Key.GetValue("Debugger")!.ToString())) {
                _Debugger = Key.GetValue("Debugger")!.ToString()!.Trim();
            }
            Key?.Close();
        } catch { } finally { ParentKey.Close(); }

        if (!string.IsNullOrEmpty(_Debugger)) {
            try {
                if (_Debugger.Contains('"')) {
                    _Debugger = _Debugger.TrimStart('\"');
                    _Debugger = _Debugger[.._Debugger.IndexOf('"')];
                    _Debugger = _Debugger.Trim('"');
                }
            } catch { _Debugger = ""; }
        }
        DebuggerCmd = _Debugger.Trim();
    }

    public static void BitClear(ref long MyByte, long MyBit) {
        long BitMask;
        BitMask = Convert.ToInt64(Math.Pow(2, MyBit - 1));
        MyByte &= ~BitMask;
    }
    public static void BitSet(ref long MyByte, long MyBit) {
        long BitMask;
        BitMask = Convert.ToInt64(Math.Pow(2, MyBit - 1));
        MyByte |= BitMask;
    }
    public static void BitToggle(ref long MyByte, long MyBit) {
        long BitMask;
        BitMask = Convert.ToInt64(Math.Pow(2, MyBit - 1));
        MyByte ^= BitMask;
    }
    public static bool BitExamine(long MyByte, long MyBit) {
        long BitMask;
        BitMask = Convert.ToInt64(Math.Pow(2, MyBit - 1));
        return (MyByte & BitMask) > 0;
    }

    public static bool Is64Bits() {
        return System.Runtime.InteropServices.Marshal.SizeOf(typeof(IntPtr)) == 8;
    }
    public static bool AddPrivilege(string privilege) {
        // const int SE_PRIVILEGE_DISABLED = 0x00000000;
        const int SE_PRIVILEGE_ENABLED = 0x00000002;
        const int TOKEN_QUERY = 0x00000008;
        const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;

        IntPtr hproc = Classes.API.GetCurrentProcess();
        if (Classes.API.OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out IntPtr htok)) {
            Classes.API.TOKEN_PRIVILEGES tp;
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = SE_PRIVILEGE_ENABLED;
            if (Classes.API.LookupPrivilegeValue(null, privilege, ref tp.Luid)) {
                return Classes.API.AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            } else { return false; }
        } else { return false; }
    }
}

[SupportedOSPlatform("windows")]
internal class CpuUsage {
    private API.FILETIME _idleTime, _kernTime, _userTime;
    private long _oldCpuUsage = 0, _oldKernUsage = 0, _oldUserUsage = 0;
    private double _rawIdleTime, _rawKernTime, _rawUserTime;
    private int _CpuUsage, _UserUsage, _KernelUsage;
    private long _now;
    private readonly int Processors = Environment.ProcessorCount;

    public void Refresh(long sinceWhen) {
        API.GetSystemTimes(ref _idleTime, ref _kernTime, ref _userTime);
        _now = DateTime.Now.Ticks;

        if (_oldCpuUsage == 0) {
            _oldCpuUsage = _idleTime.Ticks; _oldKernUsage = _kernTime.Ticks; _oldUserUsage = _userTime.Ticks;
            _CpuUsage = 0; _UserUsage = 0; _KernelUsage = 0;
            return;
        }
        unchecked {
            _rawIdleTime = (((_idleTime.Ticks - _oldCpuUsage) * 100) / (sinceWhen - _now)) / Processors;
            _rawKernTime = (((_kernTime.Ticks - _oldKernUsage) * 100) / (sinceWhen - _now)) / Processors;
            _rawUserTime = (((_userTime.Ticks - _oldUserUsage) * 100) / (sinceWhen - _now)) / Processors;

            _oldCpuUsage = _idleTime.Ticks;
            _oldKernUsage = _kernTime.Ticks;
            _oldUserUsage = _userTime.Ticks;

            _CpuUsage = (int)Math.Min(100, 100 - Math.Abs(_rawIdleTime));
            _UserUsage = (int)Math.Min(100, Math.Abs(_rawUserTime));
            _KernelUsage = (int)Math.Min(100, Math.Abs(_rawKernTime) - Math.Abs(_rawIdleTime));
        }
    }

    public int Usage => _CpuUsage;
    public int UserUsage => _UserUsage;
    public int KernelUsage => _KernelUsage;

}
