using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Versioning;
using sMkTaskManager.Classes;
using sMkTaskManager.Controls;
namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
public partial class frmProcess_Details : Form {
    private Process p1;
    private TaskManagerProcess p2;
    private readonly System.Timers.Timer UpdateTimer = new();
    private readonly HashSet<string> VisibleValues = new();
    private bool _ThreadFlagReSort = false;
    private Int128 _PageFaultsDelta = 0;
    private readonly HashSet<int> _HashThreads = new();
    private readonly HashSet<string> _HashLocked = new();
    private readonly string _numberFormat = "#,0";

    public frmProcess_Details() {
        InitializeComponent();
        InitializeVisibleValues();
        Extensions.CascadingDoubleBuffer(this);
        p_lblWorkingSet.SwitchToBold();
        p_lblPageFaults.SwitchToBold();
        p_lblPrivateBytes.SwitchToBold();
        p_lblUser.SwitchToBold();
        p_lblCpuUsage.SwitchToBold();
        p_lblGDI.SwitchToBold();
        p_lblVirtualMemory.SwitchToBold();
    }
    public int PID {
        get => (p1 == null) ? 0 : p1.Id;
        set {
            if (value != PID && value > Shared.bpi) {
                try {
                    p1 = Process.GetProcessById(value);
                    p2 = new TaskManagerProcess(value) { IgnoreBackColor = true };
                    p2.Load(new(), VisibleValues, true);
                    p2.PropertyChanged += OnProcessPropertyChanged;
                } catch { }
                LoadProcessDetails();
            }
            Tag = value;
        }
    }

    private void InitializeVisibleValues() {
        // Set all the values that we use so we get notification of them.
        VisibleValues.Add("Priority");
        VisibleValues.Add("CreationTime");
        VisibleValues.Add("RunTime");
        VisibleValues.Add("CpuTime");
        VisibleValues.Add("UserTime");
        VisibleValues.Add("KernelTime");
        VisibleValues.Add("WorkingSet");
        VisibleValues.Add("WorkingSetPeak");
        VisibleValues.Add("WorkingSetPrivate");
        VisibleValues.Add("WorkingSetShareable");
        VisibleValues.Add("PagedMemory");
        VisibleValues.Add("PagedMemoryPeak");
        VisibleValues.Add("VirtualMemory");
        VisibleValues.Add("VirtualMemoryPeak");
        VisibleValues.Add("CpuUsage");
        VisibleValues.Add("PrivateBytes");
        VisibleValues.Add("Handles");
        VisibleValues.Add("Threads");
        VisibleValues.Add("PageFaults");
        VisibleValues.Add("GDIObjects");
        VisibleValues.Add("UserObjects");
        VisibleValues.Add("GDIObjectsPeak");
        VisibleValues.Add("UserObjectsPeak");
        VisibleValues.Add("ReadOperations");
        VisibleValues.Add("ReadTransfer");
        VisibleValues.Add("WriteOperations");
        VisibleValues.Add("WriteTransfer");
        VisibleValues.Add("OtherOperations");
        VisibleValues.Add("OtherTransfer");
        VisibleValues.Add("DiskRead");
        VisibleValues.Add("DiskReadDelta");
        VisibleValues.Add("DiskReadRate");
        VisibleValues.Add("DiskWrite");
        VisibleValues.Add("DiskWriteDelta");
        VisibleValues.Add("DiskWriteRate");
        VisibleValues.Add("NetSent");
        VisibleValues.Add("NetSentDelta");
        VisibleValues.Add("NetSentRate");
        VisibleValues.Add("NetRcvd");
        VisibleValues.Add("NetRcvdDelta");
        VisibleValues.Add("NetRcvdRate");
    }
    private void OnLoad(object sender, EventArgs e) {
        UpdateTimer.Elapsed += OnUpdateTimerElapsed;
        Settings.LoadProcDetails();
        try {
            if (Settings.RememberPositions) {
                Width = Math.Max(MinimumSize.Width, Settings.ProcessDetails.Size.Width);
                Height = Math.Max(MinimumSize.Height, Settings.ProcessDetails.Size.Height);
                if (Settings.ProcessDetails.Location.X != 0 || Settings.ProcessDetails.Location.Y != 0) Location = Settings.ProcessDetails.Location;
            }
        } catch { }

        LoadGraphSettings();
        sMkListViewSorter.EnableSorting(lvModules);
        sMkListViewSorter.EnableSorting(lvThreads);
        sMkListViewSorter.EnableSorting(lvLockedFiles);

        try { tbSpeed.Value = Settings.ProcessDetails.UpdateSpeed; } catch { tbSpeed.Value = 1; }
        try { tc.SelectTab(Settings.ProcessDetails.LastTab); } catch { }
        tbSpeed_ValueChanged(tbSpeed, EventArgs.Empty);
    }
    private void OnFormClosing(object sender, FormClosingEventArgs e) {
        UpdateTimer.Stop();
        VisibleValues.Clear();
        if (Settings.RememberPositions && WindowState == FormWindowState.Normal) {
            Settings.ProcessDetails.Size = Size;
            Settings.ProcessDetails.Location = Location;
        }
        Settings.ProcessDetails.LastTab = tc.SelectedIndex;
        Settings.ProcessDetails.UpdateSpeed = tbSpeed.Value;
        Settings.SaveProcDetails();
        try { Dispose(); } catch { }
    }
    private void OnProcessPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        switch (e.PropertyName) {
            case "Priority": g_lblCPUPriority.Text = p2.Priority; break;
            case "CreationTime":
                g_lblCPUCreation.Text = p2.CreationTime;
                g_lblCPUTimeTotal.Text = p2.CpuTime;
                g_lblCPUTimeUser.Text = p2.UserTime;
                g_lblCPUTimeKernel.Text = p2.KernelTime;
                break;
            case "CpuTime": g_lblCPUTimeTotal.Text = p2.CpuTime; break;
            case "UserTime": g_lblCPUTimeUser.Text = p2.UserTime; break;
            case "KernelTime": g_lblCPUTimeKernel.Text = p2.KernelTime; break;
            case "RunTime": g_lblCPURunning.Text = p2.RunTime; break;
            case "WorkingSet": g_lblMemWS.Text = p2.WorkingSet; break;
            case "WorkingSetPeak": g_lblMemWSpeak.Text = p2.WorkingSetPeak; break;
            case "WorkingSetPrivate": g_lblMemWSpriv.Text = p2.WorkingSetPrivate; break;
            case "WorkingSetShareable": g_lblMemWSshare.Text = p2.WorkingSetShareable; break;
            case "PagedMemory": g_lblPagedMemory.Text = p2.PagedMemory; break;
            case "VirtualMemory": g_lblVirtualMemory.Text = p2.VirtualMemory; break;
            case "GDIObjects": g_lblResGDI.Text = p2.GDIObjects.ToString(_numberFormat); break;
            case "UserObjects": g_lblResUser.Text = p2.UserObjects.ToString(_numberFormat); break;
            case "PageFaults":
                g_lblPageFaultsDelta.Tag ??= p2.PageFaults;
                g_lblPageFaults.Text = p2.PageFaults.ToString(_numberFormat);
                unchecked { _PageFaultsDelta = p2.PageFaults - Convert.ToUInt64(g_lblPageFaultsDelta.Tag); };
                g_lblPageFaultsDelta.Text = _PageFaultsDelta.ToString(_numberFormat);
                g_lblPageFaultsDelta.Tag = p2.PageFaults;
                break;
            case "Handles":
                g_lblResHandles.Text = p2.Handles.ToString(_numberFormat);
                g_lblResHandlesPeak.Tag ??= 0;
                if (p2.Handles > Convert.ToInt32(g_lblResHandlesPeak.Tag)) {
                    g_lblResHandlesPeak.Tag = p2.Handles;
                    g_lblResHandlesPeak.Text = p2.Handles.ToString(_numberFormat);
                }
                break;
            case "Threads":
                g_lblResThreads.Text = p2.Threads.ToString(_numberFormat);
                g_lblResThreadsPeak.Tag ??= 0;
                if (p2.Threads > Convert.ToInt32(g_lblResThreadsPeak.Tag)) {
                    g_lblResThreadsPeak.Tag = p2.Threads;
                    g_lblResThreadsPeak.Text = p2.Threads.ToString(_numberFormat);
                }
                break;
            case "ReadOperations": i_lblReadCount.Text = p2.ReadOperations; break;
            case "WriteOperations": i_lblWriteCount.Text = p2.WriteOperations; break;
            case "OtherOperations": i_lblOtherCount.Text = p2.OtherOperations; break;
            case "ReadTransfer": i_lblReadBytes.Text = p2.ReadTransfer; break;
            case "WriteTransfer": i_lblWriteBytes.Text = p2.WriteTransfer; break;
            case "OtherTransfer": i_lblOtherBytes.Text = p2.OtherTransfer; break;
            case "DiskRead": d_lblDiskRead.Text = p2.DiskRead; d_lblDiskReadDelta.Text = p2.DiskReadDelta; break;
            case "DiskWrite": d_lblDiskWrite.Text = p2.DiskWrite; d_lblDiskWriteDelta.Text = p2.DiskWriteDelta; break;
            case "DiskReadRate": d_lblDiskReadRate.Text = p2.DiskReadRate; break;
            case "DiskWriteRate": d_lblDiskWriteRate.Text = p2.DiskWriteRate; break;
            case "NetSent": d_lblNetSend.Text = p2.NetSent; d_lblNetSendDelta.Text = p2.NetSentDelta; break;
            case "NetRcvd": d_lblNetRcvd.Text = p2.NetRcvd; d_lblNetRcvdDelta.Text = p2.NetRcvdDelta; break;
            case "NetSentRate": d_lblNetSendRate.Text = p2.NetSentRate; break;
            case "NetRcvdRate": d_lblNetRcvdRate.Text = p2.NetRcvdRate; break;
        }
    }
    private void OnUpdateTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e) {
        if (p2 != null && !p1.HasExited) {
            p1.Refresh();
            BeginInvoke(() => p2.Update(new(), VisibleValues, true));
            BeginInvoke(() => Refresh_General());
            //BeginInvoke(() => Refresh_Threads(false));
            //BeginInvoke(() => Refresh_LockedFile(false));
        }
    }

    private void btnOK_Click(object sender, EventArgs e) { Close(); }
    private void btnCancel_Click(object sender, EventArgs e) { Close(); }
    private void tbSpeed_ValueChanged(object sender, EventArgs e) {
        if (tbSpeed.Value > 0) {
            UpdateTimer.Interval = tbSpeed.Value * 1000;
            lblSpeedValue.Text = tbSpeed.Value + "s";
            if (!UpdateTimer.Enabled) UpdateTimer.Start();
        } else {
            lblSpeedValue.Text = "Halt";
            UpdateTimer.Stop();
        }
    }

    private void LoadGraphSettings() {
        // Set all the graph colors.
        p_ChartCPU.BackColorShade = Color.FromArgb(0, 50, 0);
        p_ChartCPU.PenGraph1.Color = Color.Lime;
        p_ChartWS.BackColorShade = Color.FromArgb(50, 50, 0);
        p_ChartWS.PenGraph1.Color = Color.Yellow;
        p_ChartPB.BackColorShade = Color.FromArgb(64, 32, 0);
        p_ChartPB.PenGraph1.Color = Color.Orange;
        p_ChartVM.BackColorShade = Color.FromArgb(50, 0, 50);
        p_ChartVM.PenGraph1.Color = Color.Fuchsia;
        p_ChartPF.BackColorShade = Color.FromArgb(50, 0, 0);
        p_ChartPF.PenGraph1.Color = Color.Red;
        p_ChartGDI.BackColorShade = Color.FromArgb(0, 64, 64);
        p_ChartGDI.PenGraph1.Color = Color.Turquoise;
        p_ChartUser.BackColorShade = Color.FromArgb(0, 0, 64);
        p_ChartUser.PenGraph1.Color = Color.DodgerBlue;
        i_ChartReads.BackColorShade = Color.FromArgb(0, 50, 0);
        i_ChartReads.PenGraph1.Color = Color.Lime;
        i_ChartWrites.BackColorShade = Color.FromArgb(50, 0, 0);
        i_ChartWrites.PenGraph1.Color = Color.Red;
        i_ChartOthers.BackColorShade = Color.FromArgb(0, 0, 64);
        i_ChartOthers.PenGraph1.Color = Color.DodgerBlue;
        d_ChartDisk.BackColorShade = Color.FromArgb(60, 30, 30);
        d_ChartDisk.PenGraph1.Color = Color.Tan;
        d_ChartDisk.PenGraph2.Color = Color.OrangeRed;
        d_ChartNet.BackColorShade = Color.FromArgb(50, 0, 50);
        d_ChartNet.PenGraph1.Color = Color.SkyBlue;
        d_ChartNet.PenGraph2.Color = Color.LightPink;
        // We will set the same settings as the Performance Charts 
        i_ChartReads.BackSolid = Settings.Performance.Solid;
        i_ChartReads.ShadeBackground = Settings.Performance.ShadeBackground;
        i_ChartReads.AntiAliasing = Settings.Performance.AntiAlias;
        i_ChartReads.DisplayAverage = Settings.Performance.DisplayAverages;
        i_ChartReads.DisplayLegends = Settings.Performance.DisplayLegends;
        i_ChartReads.DisplayIndexes = Settings.Performance.DisplayIndexes;
        i_ChartReads.DetailsOnHover = Settings.Performance.DisplayOnHover;
        i_ChartReads.ValueSpacing = Settings.Performance.ValueSpacing;
        i_ChartReads.GridSpacing = Settings.Performance.GridSize;
        i_ChartReads.LightColors = Settings.Performance.LightColors;
        i_ChartReads.PenGridVertical.Color = Settings.Performance.VerticalGridColor;
        i_ChartReads.PenGridVertical.DashStyle = (System.Drawing.Drawing2D.DashStyle)Settings.Performance.VerticalGridStyle;
        i_ChartReads.PenGridHorizontal.Color = Settings.Performance.HorizontalGridColor;
        i_ChartReads.PenGridHorizontal.DashStyle = (System.Drawing.Drawing2D.DashStyle)Settings.Performance.HorizontalGridStyle;
        i_ChartReads.PenAverage.Color = Settings.Performance.AverageLineColor;
        i_ChartReads.PenAverage.DashStyle = (System.Drawing.Drawing2D.DashStyle)Settings.Performance.AverageLineStyle;
        // Then we will copy those settings to all other graphs.
        i_ChartWrites.CopySettings(i_ChartReads);
        i_ChartOthers.CopySettings(i_ChartReads);
        p_ChartCPU.CopySettings(i_ChartReads);
        p_ChartWS.CopySettings(i_ChartReads);
        p_ChartPB.CopySettings(i_ChartReads);
        p_ChartVM.CopySettings(i_ChartReads);
        p_ChartPF.CopySettings(i_ChartReads);
        p_ChartGDI.CopySettings(i_ChartReads);
        p_ChartUser.CopySettings(i_ChartReads);
        d_ChartDisk.CopySettings(i_ChartReads);
        d_ChartNet.CopySettings(i_ChartReads);
        // We have to set the Indexes and Suffixes for all the graphs.
        i_ChartReads.SetIndexes(""); i_ChartReads.ValuesSuffix = " K";
        i_ChartWrites.SetIndexes(""); i_ChartWrites.ValuesSuffix = " K";
        i_ChartOthers.SetIndexes(""); i_ChartOthers.ValuesSuffix = " K";
        p_ChartCPU.SetIndexes("");
        p_ChartWS.SetIndexes(""); p_ChartWS.ValuesSuffix = " K";
        p_ChartPB.SetIndexes(""); p_ChartPB.ValuesSuffix = " K";
        p_ChartVM.SetIndexes(""); p_ChartVM.ValuesSuffix = " K";
        p_ChartPF.SetIndexes(""); p_ChartPF.ValuesSuffix = "";
        p_ChartGDI.SetIndexes(""); p_ChartGDI.ValuesSuffix = "";
        p_ChartUser.SetIndexes(""); p_ChartUser.ValuesSuffix = "";
        d_ChartDisk.SetIndexes("Read", "Write"); d_ChartDisk.ValuesSuffix = " Kb.";
        d_ChartNet.SetIndexes("Receive", "Sent"); d_ChartNet.ValuesSuffix = " Kb.";
        // Set LightColors for Performance Metters as well.
        i_PerfReads.LightColors = i_ChartReads.LightColors;
        i_PerfWrites.LightColors = i_ChartReads.LightColors;
        i_PerfOthers.LightColors = i_ChartReads.LightColors;
        d_PerfDisk.LightColors = i_ChartReads.LightColors;
        d_PerfNet.LightColors = i_ChartReads.LightColors;
        // Do not allow big Value Spacing on here.
        if (p_ChartWS.ValueSpacing > 2) p_ChartWS.ValueSpacing = 2;
        if (p_ChartPB.ValueSpacing > 2) p_ChartPB.ValueSpacing = 2;
        if (p_ChartVM.ValueSpacing > 2) p_ChartVM.ValueSpacing = 2;
        if (p_ChartPF.ValueSpacing > 2) p_ChartPF.ValueSpacing = 2;
        if (p_ChartGDI.ValueSpacing > 2) p_ChartPF.ValueSpacing = 2;
        if (p_ChartUser.ValueSpacing > 2) p_ChartPF.ValueSpacing = 2;
    }
    private void LoadProcessDetails() {
        if (p1 == null || p2 == null) return;
        // Set Title
        try {
            Text = $"Process Details: {Path.GetFileName(p1.Modules[0].FileName)}:{p1.Id}";
        } catch {
            Text = $"Process Details: {p2.Name}:{p2.ID}";
        }
        // Set Top Fixed Values
        g_txtPID.Text = p2.ID;
        g_txtName.Text = p2.Name;
        g_txtPath.Text = p2.ImagePath;
        g_txtDescription.Text = p2.Description;
        try {
            g_txtVersion.Text = p1.MainModule!.FileVersionInfo.ProductVersion;
        } catch { g_txtVersion.Text = "n/a."; }
        try {
            g_txtCompany.Text = p1.MainModule!.FileVersionInfo.CompanyName;
        } catch { g_txtCompany.Text = "n/a."; }
        // Set Binary Type
        try {
            API.BINARY_TYPE binType = new();
            if (API.GetBinaryType(g_txtPath.Text, ref binType)) {
                g_txtType.Text = GetBinaryStr(binType);
            } else if (Shared.Is64Bits()) {
                // on 64bits it seems to fail when pointing to an actual 64bit executable.
                g_txtType.Text = GetBinaryStr(API.BINARY_TYPE.SCS_64BIT_BINARY) + " *";
            } else {
                g_txtType.Text = "Unknown";
            }
        } catch { g_txtType.Text = "n/a."; }
        // Set Initial Values, we need this, in case they dont update...
        p2.ForceRaiseChange(VisibleValues);
        // Branches, Icon and Modules
        try { if (p1.HasExited) return; } catch { return; }
        LoadProcessIcon(p1);
        LoadProcessModules(p1);
        Refresh_General();
        Refresh_Threads(true);
        Refresh_LockedFile(false);
    }
    private void LoadProcessIcon(in Process p) {
        if (p1 == null) return;
        try {
            if (File.Exists(p.Modules[0].FileName)) {
                IntPtr[] IconPtr = new IntPtr[1];
                if (API.ExtractIconEx(p.Modules[0].FileName, 0, IconPtr, null, 1) > 0) {
                    g_pbIcon.Image = Icon.FromHandle(IconPtr[0]).ToBitmap();
                    API.DestroyIcon(IconPtr[0]);
                } else {
                    g_pbIcon.Image = Resources.Resources.pbProcessDetails;
                }
            }
        } catch (Exception ex) { Debug.WriteLine("Error Getting LargeIcon For PID {0}: {1}", p.Id, ex.Message); }
    }
    private void LoadProcessModules(in Process p) {
        if (p1 == null || p == null) return;
        try { if (p.Modules.Count < 1 || p.HasExited) return; } catch { return; }
        lvModules.BeginUpdate();
        lvModules.Items.Clear();
        foreach (ProcessModule m in p.Modules) {
            try {
                ListViewItem itm = lvModules.Items.Add(m.ModuleName);
                API.BINARY_TYPE binType = new();
                API.GetBinaryType(m.FileName, ref binType);
                itm.SubItems.Add(m.FileVersionInfo.ProductVersion);
                itm.SubItems.Add(GetBinaryStr(binType, true));
                itm.SubItems.Add("0x" + Convert.ToString(m.BaseAddress.ToInt64(), 16).ToUpper().ToLower());
                itm.SubItems.Add(m.ModuleMemorySize / 1024.0 + " K");
                itm.SubItems.Add(m.FileVersionInfo.FileDescription);
                itm.SubItems.Add(m.FileName);
                itm.SubItems.Add(m.FileVersionInfo.CompanyName);
                itm.SubItems.Add(m.FileVersionInfo.Language);
            } catch (Exception ex) { Shared.DebugTrap(ex); }
        }
        foreach (ColumnHeader c in lvModules.Columns) c.Width = -2;
        lvModules.EndUpdate();
    }
    private void Refresh_General() {
        if (p2 == null) return;
        // Perf Counters
        p_ChartCPU.AddValue(Convert.ToDouble(p2.CpuUsage));
        p_lblCpuUsage.Text = p2.CpuUsage + "%";
        p_ChartWS.MaxValue = Math.Round(Convert.ToDouble(p2.WorkingSetPeakValue) / 1024, 0);
        p_ChartWS.AddValue(Convert.ToDouble(p2.WorkingSetValue / 1024));
        p_lblWorkingSet.Text = p2.WorkingSet;
        p_ChartPB.MaxValue = Math.Round(Convert.ToDouble(p2.PagedMemoryPeakValue) / 1024, 0);
        p_ChartPB.AddValue(Convert.ToDouble(p2.PagedMemoryValue / 1024));
        p_lblPrivateBytes.Text = p2.PagedMemory;
        p_ChartVM.MaxValue = Math.Round(Convert.ToDouble(p2.VirtualMemoryPeakValue) / 1024, 0);
        p_ChartVM.AddValue(Convert.ToDouble(p2.VirtualMemoryValue / 1024));
        p_lblVirtualMemory.Text = p2.VirtualMemory;
        p_ChartPF.AddValue(_PageFaultsDelta);
        p_lblPageFaults.Text = Convert.ToString(_PageFaultsDelta);
        p_ChartGDI.AddValue(p2.GDIObjects);
        p_lblGDI.Text = Convert.ToString(p2.GDIObjects);
        p_ChartUser.AddValue(p2.UserObjects);
        p_lblUser.Text = Convert.ToString(p2.UserObjects);
        // I/O Counters
        i_PerfReads.SetValue(p2.ReadOperationsDeltaValue, p2.ReadOperationsDelta);
        i_PerfWrites.SetValue(p2.WriteOperationsDeltaValue, p2.WriteOperationsDelta);
        i_PerfOthers.SetValue(p2.OtherOperationsDeltaValue, p2.OtherOperationsDelta);
        i_ChartReads.AddValue(Convert.ToDouble(p2.ReadTransferDeltaValue / 1024));
        i_ChartWrites.AddValue(Convert.ToDouble(p2.WriteTransferDeltaValue / 1024));
        i_ChartOthers.AddValue(Convert.ToDouble(p2.OtherTransferDeltaValue / 1024));
        i_lblReadCountDelta.Text = p2.ReadOperationsDelta;
        i_lblWriteCountDelta.Text = p2.WriteOperationsDelta;
        i_lblOtherCountDelta.Text = p2.OtherOperationsDelta;
        i_lblReadBytesDelta.Text = p2.ReadTransferDelta;
        i_lblWriteBytesDelta.Text = p2.WriteTransferDelta;
        i_lblOtherBytesDelta.Text = p2.OtherTransferDelta;
        // Disk & Network
        d_ChartDisk.AddValue(Convert.ToDouble(p2.DiskReadDeltaValue) / 1024, Convert.ToDouble(p2.DiskWriteDeltaValue) / 1024);
        d_ChartNet.AddValue(Convert.ToDouble(p2.NetRcvdDeltaValue) / 1024, Convert.ToDouble(p2.NetSentDeltaValue) / 1024);
        d_PerfDisk.SetValue((p2.DiskReadDeltaValue + p2.DiskWriteDeltaValue) / 1024, (p2.DiskReadDeltaValue + p2.DiskWriteDeltaValue == 0) ? "Idle" : string.Format("{0:#,0} Kb.", (p2.DiskReadDeltaValue + p2.DiskWriteDeltaValue) / 1024));
        d_PerfNet.SetValue((p2.NetSentDeltaValue + p2.NetRcvdDeltaValue) / 1024, (p2.NetSentDeltaValue + p2.NetRcvdDeltaValue == 0) ? "Idle" : string.Format("{0:#,0} Kb.", (p2.NetSentDeltaValue + p2.NetRcvdDeltaValue) / 1024));
    }
    private void Refresh_Threads(bool firstTime = false) {
        if (p1 == null) return;
        try { if (p1.Threads.Count < 1 || p1.HasExited) return; } catch { return; }
        if (lvThreads.Items.Count == 0) firstTime = true;
        if (tc.SelectedTab != tpThreads && !firstTime) return;
        if (firstTime) lvThreads.BeginUpdate();
        _HashThreads.Clear();

        foreach (ProcessThread t in p1.Threads) {
            ListViewItem? itm = null;
            _HashThreads.Add(t.Id);
            foreach (ListViewItem i in lvThreads.Items) {
                if ((int)i.Tag == t.Id) {
                    itm = i;
                    if (itm.BackColor == Settings.Highlights.NewColor) itm.BackColor = Color.Empty;
                    break;
                }
            }
            if (itm == null) {
                itm = new() { Tag = t.Id, Text = t.Id.ToString() };
                itm.Name = itm.Text;
                if (!firstTime && Settings.Highlights.NewItems) itm.BackColor = Settings.Highlights.NewColor;
                lvThreads.Items.Add(itm);
            }
            foreach (ColumnHeader c in lvThreads.Columns) {
                if (c.Tag == null) continue;
                if (c.Tag.ToString() == "TID") continue;
                CheckThreadData(c, ref itm, t);
            }
        }
        // Clear items not on the hashTable anymore...
        foreach (ListViewItem itm in lvThreads.Items) {
            if (!_HashThreads.Contains(Convert.ToInt32(itm.Tag))) {
                if (itm.BackColor == Settings.Highlights.RemovedColor || !Settings.Highlights.RemovedItems) {
                    lvThreads.Items.Remove(itm);
                } else {
                    itm.BackColor = Settings.Highlights.RemovedColor;
                }
            }
        }

        if (_ThreadFlagReSort) { _ThreadFlagReSort = false; lvThreads.Sort(); }
        if (firstTime) {
            foreach (ColumnHeader c in lvThreads.Columns) { c.Width = -2; }
            lvThreads.Columns[0].Width = -1;
            if (lvThreads.Columns[0].Width >= 0 && lvThreads.Columns[0].Width < 10) { lvThreads.Columns[0].Width = 60; }
            lvThreads.Columns[^1].Width = -2;
            lvThreads.EndUpdate();
        }

    }
    private void Refresh_LockedFile(bool firstTime = false) {
        if (p1 == null) return;
        if (lvLockedFiles.Items.Count == 0) firstTime = true;
        if (tc.SelectedTab != tpLocked && !firstTime) return;
        if (firstTime) lvLockedFiles.BeginUpdate();
        _HashLocked.Clear();
        /* This implementation is horrible, find a better way */
        //foreach (string l in Handles.GetFilesLockedBy(p1)) {
        //    if (!File.Exists(l)) continue;
        //    ListViewItem? itm = null;
        //    _HashLocked.Add(l);
        //    foreach (ListViewItem i in lvLockedFiles.Items) {
        //        if (i.Tag.ToString() == l) {
        //            itm = i;
        //            if (itm.BackColor == Settings.Highlights.NewColor) itm.BackColor = Color.Empty;
        //            break;
        //        }
        //    }
        //    if (itm == null) {
        //        itm = new ListViewItem { Tag = l, Text = Path.GetFileName(l) };
        //        itm.SubItems.Add(l);
        //        if (!firstTime && Settings.Highlights.NewItems) itm.BackColor = Settings.Highlights.NewColor;
        //        lvLockedFiles.Items.Add(itm);
        //    }
        //}
        // Clear items not on the hashTable anymore...
        foreach (ListViewItem itm in lvLockedFiles.Items) {
            if (!_HashLocked.Contains(Convert.ToString(itm.Tag)!)) {
                if (itm.BackColor == Settings.Highlights.RemovedColor || !Settings.Highlights.RemovedItems) {
                    lvLockedFiles.Items.Remove(itm);
                } else {
                    itm.BackColor = Settings.Highlights.RemovedColor;
                }
            }
        }
        lvLockedFiles.Sort();
        if (firstTime) {
            foreach (ColumnHeader c in lvLockedFiles.Columns) { c.Width = -2; }
            lvLockedFiles.Columns[0].Width = -1;
            if (lvLockedFiles.Columns[0].Width >= 0 && lvLockedFiles.Columns[0].Width < 10) { lvLockedFiles.Columns[0].Width = 60; }
            lvLockedFiles.Columns[^1].Width = -2;
            lvLockedFiles.EndUpdate();
        }

    }
    private void CheckThreadData(in ColumnHeader c, ref ListViewItem itm, in ProcessThread t) {
        if (c.Tag == null) return;
        string Ident = c.Tag.ToString()!;
        string m_ThreadValue = "";
        try {
            switch (Ident) {
                case "TID": m_ThreadValue = t.Id.ToString(); break;
                case "State": m_ThreadValue = t.ThreadState.ToString(); break;
                case "Priority": m_ThreadValue = t.PriorityLevel.ToString(); break;
                case "StartTime": m_ThreadValue = t.StartTime.ToString(); break;
                case "WaitReason": m_ThreadValue = (t.ThreadState == System.Diagnostics.ThreadState.Wait) ? t.WaitReason.ToString() : ""; break;
                case "RunTime": m_ThreadValue = Shared.TimeDiff(t.StartTime.Ticks); break;
            }
        } catch { }

        if (!itm.SubItems.ContainsKey(Ident)) {
            ListViewItem.ListViewSubItem subitm = new() {
                Name = Ident,
                Text = m_ThreadValue,
                BackColor = itm.BackColor
            };
            if (itm.SubItems.Count >= c.Index) itm.SubItems.Insert(c.Index, subitm);
            _ThreadFlagReSort = _ThreadFlagReSort || c.Index == ((sMkColumnSorter?)lvThreads.Tag)?.SortColumn;
        } else if (!itm.SubItems[Ident]!.Text.Equals(m_ThreadValue)) {
            itm.SubItems[Ident]!.Text = m_ThreadValue;
            _ThreadFlagReSort = _ThreadFlagReSort || c.Index == ((sMkColumnSorter?)lvThreads.Tag)?.SortColumn;
        }
    }
    private static string GetBinaryStr(in API.BINARY_TYPE lpBinaryType, bool shortFormat = false) {
        return lpBinaryType switch {
            API.BINARY_TYPE.SCS_32BIT_BINARY => (shortFormat) ? "32-bit Application" : "32-bit Windows Based Application.",
            API.BINARY_TYPE.SCS_64BIT_BINARY => (shortFormat) ? "64-bit Application" : "64-bit Windows Based Application.",
            API.BINARY_TYPE.SCS_DOS_BINARY => (shortFormat) ? "MS-DOS Application" : "MS-DOS Based Application.",
            API.BINARY_TYPE.SCS_WOW_BINARY => (shortFormat) ? "16-bit Application" : "16-bit Windows Based Application.",
            API.BINARY_TYPE.SCS_PIF_BINARY => (shortFormat) ? "PIF File" : "PIF File that executes MS-DOS Based Applcation.",
            API.BINARY_TYPE.SCS_POSIX_BINARY => (shortFormat) ? "POSIX Application" : "POSIX Based Application.",
            API.BINARY_TYPE.SCS_OS216_BINARY => (shortFormat) ? "16-bit OS/2 Application" : "16-bit OS/2 Based Application.",
            _ => "Unknown",
        };
    }

}

