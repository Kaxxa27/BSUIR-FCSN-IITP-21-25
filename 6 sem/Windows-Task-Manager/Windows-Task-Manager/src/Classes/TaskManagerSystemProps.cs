using System.Collections;
using System.Runtime.Versioning;
using Microsoft.Win32;
namespace sMkTaskManager.Classes;

[SupportedOSPlatform("windows")]
internal class TaskManagerSystemProps {
    public string WindowsName { get; private set; }
    public string WindowsEdition { get; private set; }
    public string WindowsVersion { get; private set; }
    public DateTime InstallDate { get; private set; }
    public string RegisterUser { get; private set; }
    public string RegisterCompany { get; private set; }
    public string RegisterKey { get; private set; }
    public string SystemManufacturer { get; private set; }
    public string SystemProductName { get; private set; }
    public string ProcessorVendor { get; private set; }
    public string ProcessorName { get; private set; }
    public string ProcessorFamily { get; private set; }
    public int ProcessorCount { get; private set; }
    public string ProcessorSpeed { get; private set; }
    public int TotalMemory { get; private set; }
    public bool OEMInfo { get; private set; } = false;
    public Image? OEMLogo { get; private set; }
    public string OEMManufacturer { get; private set; }
    public string OEMSupportPhone { get; private set; }
    public string OEMSupportHours { get; private set; }
    public string OEMSupportURL { get; private set; }

    public void Refresh() {
        // Get Windows Version Information
        RegistryKey? rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", false);
        if (rk != null) {
            try { WindowsName = $"Microsoft {rk.GetValue("ProductName")}"; } catch { }
            try { WindowsEdition = $"{rk.GetValue("EditionID")} Edition"; } catch { }
            try { WindowsVersion = $"{rk.GetValue("DisplayVersion")} - Build: {rk.GetValue("CurrentBuild")}.{rk.GetValue("UBR")}"; } catch { }
            try { InstallDate = DateTime.UnixEpoch.AddSeconds(Convert.ToInt64(rk.GetValue("InstallDate"))).ToLocalTime(); } catch { }
            try { RegisterUser = rk.GetValue("RegisteredOwner")?.ToString()!; } catch { }
            try { RegisterCompany = rk.GetValue("RegisteredOrganization")?.ToString()!; } catch { }
            try { RegisterKey = DecodeProductKey((byte[])rk.GetValue("DigitalProductId")!); } catch { }
        }
        rk?.Close();
        // Get Processor Information
        RegistryKey? rk2 = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0", false);
        if (rk2 != null) {
            try { ProcessorVendor = rk2.GetValue("VendorIdentifier")?.ToString()!; } catch { }
            try { ProcessorName = rk2.GetValue("ProcessorNameString")?.ToString()!; } catch { }
            try { ProcessorFamily = rk2.GetValue("Identifier")?.ToString()!; } catch { }
            try { ProcessorSpeed = rk2.GetValue("~MHz")?.ToString()!; } catch { }
        }
        rk2?.Close();
        // Get System Information
        RegistryKey? rk3 = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\BIOS", false);
        if (rk3 != null) {
            try { SystemManufacturer = rk3.GetValue("SystemManufacturer")?.ToString()!; } catch { }
            try { SystemProductName = rk3.GetValue("SystemProductName")?.ToString()!; } catch { }
        }
        rk3?.Close();
        // Get OEM Information
        RegistryKey? rk4 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", false);
        if (rk4 != null) {
            if (File.Exists(rk4.GetValue("Logo")?.ToString()!)) {
                try { OEMLogo = Image.FromFile(rk4.GetValue("Logo")?.ToString()!); } catch { OEMLogo = null; }
            }
            try { OEMManufacturer = rk4.GetValue("Manufacturer")?.ToString()!; } catch { }
            try { OEMSupportPhone = rk4.GetValue("SupportPhone")?.ToString()!; } catch { }
            try { OEMSupportHours = rk4.GetValue("SupportHours")?.ToString()!; } catch { }
            try { OEMSupportURL = rk4.GetValue("SupportURL")?.ToString()!; } catch { }
            OEMInfo = (OEMManufacturer != "" || OEMSupportPhone != "" || OEMSupportURL != "");
        } else { OEMInfo = false; }
        rk4?.Close();
        // Get Total Memory
        if (API.GetPhysicallyInstalledSystemMemory(out long memKb)) {
            TotalMemory = (int)(memKb / 1024 / 1024);
        } else { TotalMemory = 0; }

    }
    private static string DecodeProductKey(byte[] digitalProductId) {
        if (digitalProductId == null) return string.Empty;
        if (digitalProductId.Length < 20) return string.Empty;
        // Offset of first byte of encoded product key in 'DigitalProductIdxxx" REG_BINARY value. Offset = 34H.
        const int keyStartIndex = 52;
        // Offset of last byte of encoded product key in 'DigitalProductIdxxx" REG_BINARY value. Offset = 43H.
        const int keyEndIndex = keyStartIndex + 15;
        // Possible alpha-numeric characters in product key.
        char[] digits = new char[] { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'M', 'P', 'Q', 'R', 'T', 'V', 'W', 'X', 'Y', '2', '3', '4', '6', '7', '8', '9', };
        // Length of decoded product key
        const int decodeLength = 29;
        // Length of decoded product key in byte-form.
        // Each byte represents 2 chars.
        const int decodeStringLength = 15;
        // Array of containing the decoded product key.
        char[] decodedChars = new char[decodeLength];
        // Extract byte 52 to 67 inclusive.
        ArrayList hexPid = new();
        for (int i = keyStartIndex; i <= keyEndIndex; i++) {
            hexPid.Add(digitalProductId[i]);
        }
        for (int i = decodeLength - 1; i >= 0; i--) {
            // Every sixth char is a separator.
            if ((i + 1) % 6 == 0) {
                decodedChars[i] = '-';
            } else {
                // Do the actual decoding.
                int digitMapIndex = 0;
                for (int j = decodeStringLength - 1; j >= 0; j--) {
                    int byteValue = (digitMapIndex << 8) | (byte)hexPid[j]!;
                    hexPid[j] = (byte)(byteValue / 24);
                    digitMapIndex = byteValue % 24;
                    decodedChars[i] = digits[digitMapIndex];
                }
            }
        }
        return new string(decodedChars);
    }

}