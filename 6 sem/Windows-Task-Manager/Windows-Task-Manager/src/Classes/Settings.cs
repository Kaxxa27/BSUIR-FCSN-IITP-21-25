using Microsoft.Win32;
using sMkTaskManager.Controls;
using System.Configuration;
using System.Runtime.Versioning;

namespace sMkTaskManager.Classes;

[SupportedOSPlatform("windows")]
internal static class Settings {
    private static readonly System.Text.UTF8Encoding _Encoding = new();
    private static readonly System.Text.StringBuilder _StringBuilder = new();
    private static readonly string ExePath = Application.ExecutablePath;
    private static readonly string ActualFile = Application.ExecutablePath + ".config";
    private static readonly string RegKey = "Software\\sMk Tools\\sMk TaskManager3";

    public static bool RememberPositions = true;
    public static bool RememberActiveTab = true;
    public static bool StartMinimized = false;
    public static bool AlwaysOnTop = false;
    public static HighlightsClass Highlights = new();
    public static PerformanceClass Performance = new();
    public static NetworkingClass Networking = new();
    public static ProcessDetailsClass ProcessDetails = new();
    public static MainWindowClass MainWindow = new();
    public static bool IconsInProcess = true;
    public static bool TimmingInStatus = true;
    public static bool ServicesInStatus = false;
    public static short UpdateMethod = 3;
    public static int UpdateSpeed = 1000;
    public static bool inFullScreen = false;
    public static int ActiveTab = 0;
    public static bool ToTrayWhenMinimized = false;
    public static bool ToTrayWhenClosed = false;
    public static bool ShowCPUOnTray = false;
    public static bool MinimizeWhenClosing = false;
    public static bool DblClickToRestore = true;
    public static bool ShowAllProcess = false;
    public static bool ShowSummaryView = false;
    public static bool AlternateRowColors = false;
    public static bool StoreInFile = false;
    public static List<string> CheckedInterfaces = new();
    public static List<int> CustomColors = new();

    public class HighlightsClass {
        public bool NewItems = true;
        public bool ChangingItems = true;
        public bool RemovedItems = true;
        public bool FrozenItems = true;
        public Color NewColor = Color.LightGreen;
        public Color ChangingColor = Color.PeachPuff;
        public Color RemovedColor = Color.LightCoral;
        public Color FrozenColor = Color.Orchid;
    }
    public class PerformanceClass {
        public bool Solid = true;
        public bool AntiAlias = false;
        public bool ShadeBackground = true;
        public bool DisplayAverages = false;
        public bool DisplayIndexes = false;
        public bool DisplayLegends = false;
        public bool DisplayOnHover = false;
        public bool LightColors = false;
        public int ValueSpacing = 4;
        public int GridSize = 10;
        public int VerticalGridStyle = 0;
        public Color VerticalGridColor = Color.Green;
        public int HorizontalGridStyle = 0;
        public Color HorizontalGridColor = Color.Green;
        public int AverageLineStyle = 5;
        public Color AverageLineColor = Color.Orange;
        public bool ShowKernelTime = true;
        public bool SeparateCPUs = false;
    }
    public class NetworkingClass {
        public bool Solid = false;
        public bool AntiAlias = false;
        public bool ShadeBackground = true;
        public bool DisplayAverages = true;
        public bool DisplayIndexes = true;
        public bool DisplayLegends = true;
        public bool LightColors = false;
        public int ValueSpacing = 4;
        public int GridSize = 10;
        public int VerticalGridStyle = 5;
        public Color VerticalGridColor = Color.Green;
        public int HorizontalGridStyle = 5;
        public Color HorizontalGridColor = Color.Green;
        public int AverageLineStyle = 5;
        public Color AverageLineColor = Color.Orange;
        public Color UploadColor = Color.Fuchsia;
        public Color DownloadColor = Color.Cyan;
        public bool KeepUpdating = false;
    }
    public class ProcessDetailsClass {
        public Size Size = new(500, 550);
        public Point Location = new(0, 0);
        public int UpdateSpeed = 1;
        public int LastTab = 0;
    }
    public class MainWindowClass {
        public Size Size = new(760, 750);
        public Point Location = new(0, 0);
        public bool Maximized = false;
    }

    public static bool SaveAll() {
        return SaveGeneral() & SaveHighlights() & SavePerformance();
    }
    public static bool SaveGeneral() {
        _StringBuilder.Clear();
        try {
            _StringBuilder.Append(Convert.ToInt16(RememberPositions) + ",");
            _StringBuilder.Append(Convert.ToInt16(RememberActiveTab) + ",");
            _StringBuilder.Append(Convert.ToInt16(AlwaysOnTop) + ",");
            _StringBuilder.Append(Convert.ToInt16(IconsInProcess) + ",");
            _StringBuilder.Append(Convert.ToInt16(TimmingInStatus) + ",");
            _StringBuilder.Append(Convert.ToInt16(ServicesInStatus) + ",");
            _StringBuilder.Append(Convert.ToInt16(ShowSummaryView) + ",");
            _StringBuilder.Append(UpdateMethod + ",");
            _StringBuilder.Append(UpdateSpeed + ",");
            _StringBuilder.Append(Convert.ToInt16(inFullScreen) + ",");
            _StringBuilder.Append(ActiveTab + ",");
            _StringBuilder.Append(Convert.ToInt16(MinimizeWhenClosing) + ",");
            _StringBuilder.Append(Convert.ToInt16(ToTrayWhenMinimized) + ",");
            _StringBuilder.Append(Convert.ToInt16(ToTrayWhenClosed) + ",");
            _StringBuilder.Append(Convert.ToInt16(DblClickToRestore) + ",");
            _StringBuilder.Append(Convert.ToInt16(ShowAllProcess) + ",");
            _StringBuilder.Append(Convert.ToInt16(StartMinimized) + ",");
            _StringBuilder.Append(Convert.ToInt16(AlternateRowColors) + ",");
            _StringBuilder.Append(Convert.ToInt16(ShowCPUOnTray) + ",");
            return Write("General", FixStringToWrite(_StringBuilder));
        } catch (Exception e) { Shared.DebugTrap(e); return false; }
    }
    public static bool SaveMainWindow() {
        _StringBuilder.Clear();
        try {
            _StringBuilder.Append(MainWindow.Size.Width + "," + MainWindow.Size.Height + ",");
            _StringBuilder.Append(MainWindow.Location.X + "," + MainWindow.Location.Y + ",");
            _StringBuilder.Append(Convert.ToInt32(MainWindow.Maximized) + ",");
            return Write("winMain", FixStringToWrite(_StringBuilder));
        } catch (Exception e) { Shared.DebugTrap(e); return false; }
    }
    public static bool SaveHighlights() {
        _StringBuilder.Clear();
        try {
            _StringBuilder.Append(Convert.ToInt16(Highlights.NewItems) + ",");
            _StringBuilder.Append(Highlights.NewColor.ToArgb() + ",");
            _StringBuilder.Append(Convert.ToInt16(Highlights.ChangingItems) + ",");
            _StringBuilder.Append(Highlights.ChangingColor.ToArgb() + ",");
            _StringBuilder.Append(Convert.ToInt16(Highlights.RemovedItems) + ",");
            _StringBuilder.Append(Highlights.RemovedColor.ToArgb() + ",");
            _StringBuilder.Append(Convert.ToInt16(Highlights.FrozenItems) + ",");
            _StringBuilder.Append(Highlights.FrozenColor.ToArgb() + ",");
            return Write("Highlights", FixStringToWrite(_StringBuilder));
        } catch (Exception e) { Shared.DebugTrap(e); return false; }
    }
    public static bool SavePerformance() {
        _StringBuilder.Clear();
        try {
            _StringBuilder.Append(Convert.ToInt16(Performance.Solid) + ",");
            _StringBuilder.Append(Convert.ToInt16(Performance.AntiAlias) + ",");
            _StringBuilder.Append(Convert.ToInt16(Performance.ShadeBackground) + ",");
            _StringBuilder.Append(Convert.ToInt16(Performance.DisplayAverages) + ",");
            _StringBuilder.Append(Convert.ToInt16(Performance.DisplayIndexes) + ",");
            _StringBuilder.Append(Convert.ToInt16(Performance.DisplayLegends) + ",");
            _StringBuilder.Append(Convert.ToInt16(Performance.DisplayOnHover) + ",");
            _StringBuilder.Append(Convert.ToInt16(Performance.LightColors) + ",");
            _StringBuilder.Append(Performance.ValueSpacing + ",");
            _StringBuilder.Append(Performance.GridSize + ",");
            _StringBuilder.Append(Performance.VerticalGridStyle + ",");
            _StringBuilder.Append(Performance.VerticalGridColor.ToArgb() + ",");
            _StringBuilder.Append(Performance.HorizontalGridStyle + ",");
            _StringBuilder.Append(Performance.HorizontalGridColor.ToArgb() + ",");
            _StringBuilder.Append(Performance.AverageLineStyle + ",");
            _StringBuilder.Append(Performance.AverageLineColor.ToArgb() + ",");
            _StringBuilder.Append(Convert.ToInt16(Performance.ShowKernelTime) + ",");
            _StringBuilder.Append(Convert.ToInt16(Performance.SeparateCPUs) + ",");
            return Write("Performance", FixStringToWrite(_StringBuilder));
        } catch (Exception e) { Shared.DebugTrap(e); return false; }
    }
    public static bool SaveNetworking() {
        _StringBuilder.Clear();
        try {
            _StringBuilder.Append(Convert.ToInt16(Networking.Solid) + ",");
            _StringBuilder.Append(Convert.ToInt16(Networking.AntiAlias) + ",");
            _StringBuilder.Append(Convert.ToInt16(Networking.ShadeBackground) + ",");
            _StringBuilder.Append(Convert.ToInt16(Networking.DisplayAverages) + ",");
            _StringBuilder.Append(Convert.ToInt16(Networking.DisplayIndexes) + ",");
            _StringBuilder.Append(Convert.ToInt16(Networking.DisplayLegends) + ",");
            _StringBuilder.Append(Convert.ToInt16(Networking.LightColors) + ",");
            _StringBuilder.Append(Networking.ValueSpacing + ",");
            _StringBuilder.Append(Networking.GridSize + ",");
            _StringBuilder.Append(Networking.VerticalGridStyle + ",");
            _StringBuilder.Append(Networking.VerticalGridColor.ToArgb() + ",");
            _StringBuilder.Append(Networking.HorizontalGridStyle + ",");
            _StringBuilder.Append(Networking.HorizontalGridColor.ToArgb() + ",");
            _StringBuilder.Append(Networking.AverageLineStyle + ",");
            _StringBuilder.Append(Networking.AverageLineColor.ToArgb() + ",");
            _StringBuilder.Append(Networking.UploadColor.ToArgb() + ",");
            _StringBuilder.Append(Networking.DownloadColor.ToArgb() + ",");
            _StringBuilder.Append(Convert.ToInt16(Networking.KeepUpdating) + ",");
            return Write("Networking", FixStringToWrite(_StringBuilder));
        } catch (Exception e) { Shared.DebugTrap(e); return false; }
    }
    public static bool SaveInterfaces() {
        _StringBuilder.Clear();
        try {
            foreach (string s in CheckedInterfaces) {
                _StringBuilder.Append(s + ",");
            }
            return Write("Selected Nics", FixStringToWrite(_StringBuilder));
        } catch (Exception e) { Shared.DebugTrap(e); return false; }
    }
    public static bool SaveCustomColors() {
        _StringBuilder.Clear();
        try {
            foreach (int c in CustomColors) {
                if (c == 16777215) continue;
                if (c == 0) continue;
                _StringBuilder.Append(c + ",");
            }
            return Write("Custom Colors", _StringBuilder.ToString().TrimEnd(','));
        } catch (Exception e) { Shared.DebugTrap(e); return false; }
    }
    public static bool SaveProcDetails() {
        _StringBuilder.Clear();
        try {
            _StringBuilder.Append(ProcessDetails.Size.Width + "," + ProcessDetails.Size.Height + ",");
            _StringBuilder.Append(ProcessDetails.Location.X + "," + ProcessDetails.Location.Y + ",");
            _StringBuilder.Append(ProcessDetails.UpdateSpeed + ",");
            _StringBuilder.Append(ProcessDetails.LastTab + ",");
            return Write("winProcess", FixStringToWrite(_StringBuilder));
        } catch (Exception e) { Shared.DebugTrap(e); return false; }
    }
    public static bool SaveColsInformation(string strName, in sMkListView lv) {
        _StringBuilder.Clear();
        try {
            foreach (ColumnHeader c in lv.Columns) {
                TaskManagerColumn cc = new(c);
                if (lv.Sortable && lv.SortColumn == c.Index) cc.SortOrder = lv.Sorting;
                _StringBuilder.AppendLine(cc.GetChunk());
            }
            return Write(strName, _StringBuilder.ToString(), RegistryValueKind.Binary);
        } catch (Exception e) { Shared.DebugTrap(e); return false; }
    }

    public static bool LoadAll() {
        return LoadGeneral() & LoadHighlights();
    }
    public static bool LoadGeneral() {
        try {
            string[] ChunkValues = Read("General", "").Split(',');
            if (ChunkValues.Length < 3) return false;
            RememberPositions = ChunkValues[0] != "0";
            RememberActiveTab = ChunkValues[1] != "0";
            AlwaysOnTop = ChunkValues[2] != "0";
            IconsInProcess = ChunkValues[3] != "0";
            TimmingInStatus = ChunkValues[4] != "0";
            ServicesInStatus = ChunkValues[5] != "0";
            ShowSummaryView = ChunkValues[6] != "0";
            UpdateMethod = short.Parse(ChunkValues[7]);
            UpdateSpeed = int.Parse(ChunkValues[8]);
            inFullScreen = ChunkValues[9] != "0";
            ActiveTab = int.Parse(ChunkValues[10]);
            MinimizeWhenClosing = ChunkValues[11] != "0";
            ToTrayWhenMinimized = ChunkValues[12] != "0";
            ToTrayWhenClosed = ChunkValues[13] != "0";
            DblClickToRestore = ChunkValues[14] != "0";
            ShowAllProcess = ChunkValues[15] != "0";
            StartMinimized = ChunkValues[16] != "0";
            AlternateRowColors = ChunkValues[17] != "0";
            ShowCPUOnTray = ChunkValues[18] != "0";
            return true;
        } catch (Exception e) { Shared.DebugTrap(e); return false; }

    }
    public static bool LoadMainWindow() {
        try {
            string[] ChunkValues = Read("winMain", "").Split(',');
            if (ChunkValues.Length < 3) return false;
            MainWindow.Size = new Size(int.Parse(ChunkValues[0]), int.Parse(ChunkValues[1]));
            MainWindow.Location = new Point(int.Parse(ChunkValues[2]), int.Parse(ChunkValues[3]));
            MainWindow.Maximized = ChunkValues[4] != "0";
            return true;
        } catch (Exception e) { Shared.DebugTrap(e); return false; }
    }
    public static bool LoadHighlights() {
        try {
            string[] ChunkValues = Read("Highlights", "").Split(',');
            if (ChunkValues.Length < 3) return false;
            Highlights.NewItems = ChunkValues[0] != "0";
            Highlights.NewColor = Color.FromArgb(int.Parse(ChunkValues[1]));
            Highlights.ChangingItems = ChunkValues[2] != "0";
            Highlights.ChangingColor = Color.FromArgb(int.Parse(ChunkValues[3]));
            Highlights.RemovedItems = ChunkValues[4] != "0";
            Highlights.RemovedColor = Color.FromArgb(int.Parse(ChunkValues[5]));
            Highlights.FrozenItems = ChunkValues[6] != "0";
            Highlights.FrozenColor = Color.FromArgb(int.Parse(ChunkValues[7]));
            return true;
        } catch (Exception e) { Shared.DebugTrap(e); return false; }
    }
    public static bool LoadPerformance() {
        try {
            string[] ChunkValues = Read("Performance", "").Split(',');
            if (ChunkValues.Length < 3) return false;
            Performance.Solid = ChunkValues[0] != "0";
            Performance.AntiAlias = ChunkValues[1] != "0";
            Performance.ShadeBackground = ChunkValues[2] != "0";
            Performance.DisplayAverages = ChunkValues[3] != "0";
            Performance.DisplayIndexes = ChunkValues[4] != "0";
            Performance.DisplayLegends = ChunkValues[5] != "0";
            Performance.DisplayOnHover = ChunkValues[6] != "0";
            Performance.LightColors = ChunkValues[7] != "0";
            Performance.ValueSpacing = int.Parse(ChunkValues[8]);
            Performance.GridSize = int.Parse(ChunkValues[9]);
            Performance.VerticalGridStyle = int.Parse(ChunkValues[10]);
            Performance.VerticalGridColor = Color.FromArgb(int.Parse(ChunkValues[11]));
            Performance.HorizontalGridStyle = int.Parse(ChunkValues[12]);
            Performance.HorizontalGridColor = Color.FromArgb(int.Parse(ChunkValues[13]));
            Performance.AverageLineStyle = int.Parse(ChunkValues[14]);
            Performance.AverageLineColor = Color.FromArgb(int.Parse(ChunkValues[15]));
            Performance.ShowKernelTime = ChunkValues[16] != "0";
            Performance.SeparateCPUs = ChunkValues[17] != "0";
            return true;
        } catch (Exception e) { Shared.DebugTrap(e); return false; }
    }
    public static bool LoadNetworking() {
        try {
            string[] ChunkValues = Read("Networking", "").Split(',');
            if (ChunkValues.Length < 3) return false;
            Networking.Solid = ChunkValues[0] != "0";
            Networking.AntiAlias = ChunkValues[1] != "0";
            Networking.ShadeBackground = ChunkValues[2] != "0";
            Networking.DisplayAverages = ChunkValues[3] != "0";
            Networking.DisplayIndexes = ChunkValues[4] != "0";
            Networking.DisplayLegends = ChunkValues[5] != "0";
            Networking.LightColors = ChunkValues[6] != "0";
            Networking.ValueSpacing = int.Parse(ChunkValues[7]);
            Networking.GridSize = int.Parse(ChunkValues[8]);
            Networking.VerticalGridStyle = int.Parse(ChunkValues[9]);
            Networking.VerticalGridColor = Color.FromArgb(int.Parse(ChunkValues[10]));
            Networking.HorizontalGridStyle = int.Parse(ChunkValues[11]);
            Networking.HorizontalGridColor = Color.FromArgb(int.Parse(ChunkValues[12]));
            Networking.AverageLineStyle = int.Parse(ChunkValues[13]);
            Networking.AverageLineColor = Color.FromArgb(int.Parse(ChunkValues[14]));
            Networking.UploadColor = Color.FromArgb(int.Parse(ChunkValues[15]));
            Networking.DownloadColor = Color.FromArgb(int.Parse(ChunkValues[16]));
            Networking.KeepUpdating = ChunkValues[17] != "0";
            return true;
        } catch (Exception e) { Shared.DebugTrap(e); return false; }
    }
    public static bool LoadInterfaces() {
        try {
            string[] ChunkValues = Read("Selected Nics", "").Split(',');
            CheckedInterfaces.Clear();
            foreach (string s in ChunkValues) {
                CheckedInterfaces.Add(s);
            }
            return true;
        } catch (Exception e) { Shared.DebugTrap(e); return false; }
    }
    public static bool LoadCustomColors() {
        try {
            string[] ChunkValues = Read("Custom Colors", "9498256,8421616,12180223,14053594").Split(",".ToCharArray());
            CustomColors.Clear();
            for (int i = 0; i < ChunkValues.Length; i++) {
                CustomColors.Add(int.Parse(ChunkValues[i]));
            }
            return true;
        } catch (Exception e) { Shared.DebugTrap(e); return false; }
    }
    public static bool LoadProcDetails() {
        try {
            string[] ChunkValues = Read("winProcess", "").Split(',');
            if (ChunkValues.Length < 3) return false;
            ProcessDetails.Size = new Size(int.Parse(ChunkValues[0]), int.Parse(ChunkValues[1]));
            ProcessDetails.Location = new Point(int.Parse(ChunkValues[2]), int.Parse(ChunkValues[3]));
            ProcessDetails.UpdateSpeed = int.Parse(ChunkValues[4]);
            ProcessDetails.LastTab = int.Parse(ChunkValues[5]);
            return true;
        } catch (Exception e) { Shared.DebugTrap(e); return false; }
    }
    public static bool LoadColsInformation(TaskManagerColumnTypes Type, sMkListView lv, ref HashSet<string> destinationTable) {
        string strName;
        switch (Type) {
            case TaskManagerColumnTypes.Process: strName = "colsProcess"; break;
            case TaskManagerColumnTypes.Services: strName = "colsServices"; break;
            case TaskManagerColumnTypes.Connections: strName = "colsConnections"; break;
            case TaskManagerColumnTypes.Ports: strName = "colsPorts"; break;
            case TaskManagerColumnTypes.Users: strName = "colsUsers"; break;
            case TaskManagerColumnTypes.Applications: strName = "colsApps"; break;
            case TaskManagerColumnTypes.Nics: strName = "colsNics"; break;
            case TaskManagerColumnTypes.GPUs: strName = "colsGPUs"; break;
            default: return false;
        }
        try {
            string allValues = Read(strName, "", RegistryValueKind.Binary).Trim();
            if (allValues.Length < 10) allValues = TaskManagerColumn.GetDefaultColumnsChunks(Type);

            foreach (string colValues in allValues.Split(Environment.NewLine)) {
                if (colValues.Trim().Length < 5) continue;
                TaskManagerColumn c = new(colValues.Trim());
                int colExist = -1;
                foreach (ColumnHeader col in lv.Columns) {
                    if (col.Tag?.ToString()!.ToLower() == c.Tag.ToString().ToLower()) { colExist = col.Index; }
                }
                if (colExist == -1) {
                    // If the column is not on the control then add it, This is used for Process & Services
                    ColumnHeader newCol = new() {
                        Name = c.Tag.ToString(),
                        Tag = c.Tag,
                        Text = c.Title,
                        Width = c.Width,
                        TextAlign = c.Align
                    };
                    if (c.Index <= lv.Columns.Count) { lv.Columns.Insert(c.Index, newCol); } else { lv.Columns.Add(newCol); }
                } else {
                    // Otherwise just set the position and width, This is used for Connections and Ports
                    lv.Columns[colExist].Name = c.Tag.ToString();
                    lv.Columns[colExist].Width = c.Width;
                    lv.Columns[colExist].DisplayIndex = c.Index;
                }
                // Check if we need to sort by this column, and do it.
                if (lv.Sortable && !(c.SortOrder == SortOrder.None)) { lv.SetSort(c.Index, c.SortOrder); }
            }
        } catch (Exception e) {
            Shared.DebugTrap(e); return false;
        } finally {
            destinationTable.Clear();
            foreach (ColumnHeader c in lv.Columns) { destinationTable.Add(c.Tag!.ToString()!); }
        }
        return true;
    }

    private static string Read(string valueName, string defaultValue, RegistryValueKind ValueKind = RegistryValueKind.String) {
        StoreInFile = File.Exists(ActualFile);
        if (StoreInFile) {
            return ReadXML(valueName, defaultValue, ValueKind);
        } else {
            return ReadRegistry(valueName, defaultValue, ValueKind);
        }
    }
    private static string ReadXML(string valueName, string defaultValue, RegistryValueKind ValueKind = RegistryValueKind.String) {
        string retValue;
        try {
            var configFile = ConfigurationManager.OpenExeConfiguration(ExePath);
            var settings = configFile.AppSettings.Settings;
            if (settings[valueName] == null || string.IsNullOrEmpty(settings[valueName].Value)) {
                retValue = defaultValue;
            } else {
                if (ValueKind == RegistryValueKind.Binary) {
                    retValue = _Encoding.GetString(Convert.FromBase64String(settings[valueName].Value));
                } else {
                    retValue = settings[valueName].Value;
                }
            }
        } catch (Exception e) {
            Shared.DebugTrap(e);
            retValue = defaultValue;
        }
        return retValue;
    }
    private static string ReadRegistry(string valueName, string defaultValue, RegistryValueKind ValueKind = RegistryValueKind.String) {
        string retValue = defaultValue;
        RegistryKey ParentKey = Registry.CurrentUser;
        try {
            var Key = ParentKey.OpenSubKey(RegKey, false);
            if (Key == null || Key.GetValue(valueName) == null) {
                retValue = defaultValue;
            } else if (string.IsNullOrEmpty(Key.GetValue(valueName)!.ToString()!.Trim())) {
                retValue = defaultValue;
            } else {
                if (ValueKind == RegistryValueKind.Binary) {
                    retValue = _Encoding.GetString((byte[])Key.GetValue(valueName)!);
                } else {
                    retValue = Key.GetValue(valueName)!.ToString()!.Trim();
                }
            }
            Key?.Close();
        } catch (Exception e) {
            Shared.DebugTrap(e);
            retValue = defaultValue;
        } finally {
            ParentKey.Close();
        }
        return retValue;
    }

    private static bool Write(string ValueName, string Value, RegistryValueKind ValueKind = RegistryValueKind.String) {
        if (StoreInFile) {
            return WriteXML(ValueName, Value, ValueKind);
        } else {
            return WriteRegistry(ValueName, Value, ValueKind);
        }
    }
    private static bool WriteXML(string ValueName, string Value, RegistryValueKind ValueKind = RegistryValueKind.String) {
        try {
            if (ValueKind == RegistryValueKind.Binary) { Value = Convert.ToBase64String(_Encoding.GetBytes(Value)); }
            var configFile = ConfigurationManager.OpenExeConfiguration(ExePath);
            var settings = configFile.AppSettings.Settings;
            if (settings[ValueName] == null) {
                settings.Add(ValueName, Value);
            } else {
                settings[ValueName].Value = Value;
            }
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            return true;
        } catch (Exception ex) {
            Shared.DebugTrap(ex);
            return false;
        }
    }
    private static bool WriteRegistry(string ValueName, string Value, RegistryValueKind ValueKind = RegistryValueKind.String) {
        RegistryKey ParentKey = Registry.CurrentUser;
        try {
            RegistryKey? Key = ParentKey.OpenSubKey(RegKey, true);
            Key ??= ParentKey.CreateSubKey(RegKey);
            if (ValueKind == RegistryValueKind.Binary) {
                Key.SetValue(ValueName, _Encoding.GetBytes(Value), ValueKind);
            } else {
                Key.SetValue(ValueName, Value, ValueKind);
            }
            Key.Close();
            ParentKey.Close();
            return true;
        } catch (Exception ex) {
            Shared.DebugTrap(ex);
            ParentKey.Close();
            return false;
        }
    }

    private static string FixStringToWrite(System.Text.StringBuilder Original) {
        return Original.ToString().Replace("-1,", "1,").TrimEnd(',');
    }

}
