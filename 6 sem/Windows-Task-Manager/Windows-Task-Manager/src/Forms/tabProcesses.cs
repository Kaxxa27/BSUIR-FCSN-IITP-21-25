using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.Versioning;
using sMkTaskManager.Classes;
using sMkTaskManager.Controls;
using System.Runtime.InteropServices;
namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
internal class tabProcesses : UserControl, ITaskManagerTab {
    private readonly Stopwatch _stopWatch = new();
    private HashSet<string> ColsProcesses = new();
    private readonly HashSet<int> HashProcesses = new();
    private readonly TaskManagerProcessCollection Processes = new();

    private sMkListView lv;
    private CheckBox btnAllUsers;
    private Button btnForceRefresh;
    private Button btnDetails;
    private Button btnProperties;
    private Button btnKill;
    private Label lblText;
    private ColumnHeader lvc_PID;
    private ColumnHeader lvc_Name;
    private ContextMenuStrip cms;

    public event EventHandler? RefreshStarts;
    public event EventHandler? RefreshComplete;
    public event EventHandler? ForceRefreshClicked;

    private IContainer? components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }

    public tabProcesses() {
        InitializeComponent();
        InitializeContextMenu();
        Extensions.CascadingDoubleBuffer(this);
        lv!.ContentType = typeof(TaskManagerProcess);
        lv!.DataSource = Processes.DataExporter;
        lv!.SpaceFirstValue = Settings.IconsInProcess;
    }
    private void InitializeComponent() {
        components = new Container();
        lv = new sMkListView();
        lvc_PID = new ColumnHeader();
        lvc_Name = new ColumnHeader();
        cms = new ContextMenuStrip(components);
        btnDetails = new Button();
        btnProperties = new Button();
        btnKill = new Button();
        btnForceRefresh = new Button();
        lblText = new Label();
        btnAllUsers = new CheckBox();
        SuspendLayout();
        // 
        // lv
        // 
        lv.AllowColumnReorder = true;
        lv.AlternateRowColors = false;
        lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lv.Columns.AddRange(new ColumnHeader[] { lvc_PID, lvc_Name });
        lv.ContextMenuStrip = cms;
        lv.FullRowSelect = true;
        lv.GridLines = true;
        lv.Location = new Point(6, 6);
        lv.Margin = new Padding(6, 6, 6, 0);
        lv.Name = "lv";
        lv.ShowGroups = false;
        lv.Size = new Size(588, 559);
        lv.Sortable = true;
        lv.SortColumn = 0;
        lv.Sorting = SortOrder.Ascending;
        lv.TabIndex = 0;
        lv.UseCompatibleStateImageBehavior = false;
        lv.View = View.Details;
        lv.ColumnReordered += OnListViewColumnReordered;
        lv.SelectedIndexChanged += OnListViewSelectedIndexChanged;
        lv.KeyDown += OnListViewKeyDown;
        lv.KeyPress += OnListViewKeyPress;
        lv.MouseDoubleClick += OnListViewMouseDoubleClick;
        // 
        // lvc_PID
        // 
        lvc_PID.Tag = "PID";
        lvc_PID.Text = "PID";
        // 
        // lvc_Name
        // 
        lvc_Name.Tag = "Name";
        lvc_Name.Text = "Name";
        lvc_Name.Width = 150;
        // 
        // cms
        // 
        cms.Name = "cms";
        cms.Size = new Size(61, 4);
        // 
        // btnDetails
        // 
        btnDetails.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnDetails.Enabled = false;
        btnDetails.Location = new Point(6, 571);
        btnDetails.Margin = new Padding(6, 6, 0, 6);
        btnDetails.Name = "btnDetails";
        btnDetails.Size = new Size(75, 30);
        btnDetails.TabIndex = 1;
        btnDetails.Text = "Details";
        btnDetails.UseVisualStyleBackColor = true;
        btnDetails.Click += OnButtonClicked;
        // 
        // btnProperties
        // 
        btnProperties.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnProperties.Enabled = false;
        btnProperties.Location = new Point(84, 571);
        btnProperties.Margin = new Padding(3, 6, 0, 6);
        btnProperties.Name = "btnProperties";
        btnProperties.Size = new Size(75, 30);
        btnProperties.TabIndex = 2;
        btnProperties.Text = "Prop";
        btnProperties.UseVisualStyleBackColor = true;
        btnProperties.Click += OnButtonClicked;
        // 
        // btnKill
        // 
        btnKill.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnKill.Enabled = false;
        btnKill.Location = new Point(162, 571);
        btnKill.Margin = new Padding(3, 6, 0, 6);
        btnKill.Name = "btnKill";
        btnKill.Size = new Size(78, 30);
        btnKill.TabIndex = 3;
        btnKill.Text = "Kill Process";
        btnKill.UseVisualStyleBackColor = true;
        btnKill.Click += OnButtonClicked;
        // 
        // btnForceRefresh
        // 
        btnForceRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnForceRefresh.Location = new Point(504, 571);
        btnForceRefresh.Margin = new Padding(0, 6, 6, 6);
        btnForceRefresh.Name = "btnForceRefresh";
        btnForceRefresh.Size = new Size(90, 30);
        btnForceRefresh.TabIndex = 6;
        btnForceRefresh.Text = "Force Refresh";
        btnForceRefresh.UseVisualStyleBackColor = true;
        btnForceRefresh.Click += OnButtonClicked;
        // 
        // lblText
        // 
        lblText.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lblText.AutoEllipsis = true;
        lblText.ForeColor = SystemColors.HotTrack;
        lblText.Location = new Point(338, 575);
        lblText.Name = "lblText";
        lblText.Size = new Size(163, 16);
        lblText.TabIndex = 5;
        // 
        // btnAllUsers
        // 
        btnAllUsers.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnAllUsers.Appearance = Appearance.Button;
        btnAllUsers.Location = new Point(243, 571);
        btnAllUsers.Name = "btnAllUsers";
        btnAllUsers.Size = new Size(92, 30);
        btnAllUsers.TabIndex = 4;
        btnAllUsers.Text = "Show all Users";
        btnAllUsers.TextAlign = ContentAlignment.MiddleCenter;
        btnAllUsers.UseVisualStyleBackColor = true;
        btnAllUsers.CheckedChanged += OnButtonClicked;
        // 
        // tabProcesses
        // 
        Controls.Add(btnAllUsers);
        Controls.Add(lblText);
        Controls.Add(btnForceRefresh);
        Controls.Add(btnKill);
        Controls.Add(btnProperties);
        Controls.Add(btnDetails);
        Controls.Add(lv);
        Name = "tabProcesses";
        Size = new Size(600, 600);
        KeyPress += OnKeyPress;
        VisibleChanged += OnVisibleChanged;
        ResumeLayout(false);
    }
    private void InitializeContextMenu() {
        cms.Items.Clear();
        cms.Items.AddMenuItem("&More Details").Name = "Details";
        cms.Items.AddMenuItem("File Prope&rties").Name = "Properties";
        cms.Items.AddMenuItem("&Open Location").Name = "Location";
        cms.Items.AddMenuItem("F&ind Parent Process").Name = "Parent";
        cms.Items.AddSeparator();
        cms.Items.AddMenuItem("&Kill Process").Name = "Kill";
        cms.Items.AddMenuItem("Free&ze Process").Name = "Pause";
        cms.Items.AddMenuItem("&Resume Process").Name = "Resume";
        cms.Items.AddSeparator();
        cms.Items.AddMenuItem("&Debug Process").Name = "Debug";
        cms.Items.AddMenuItem("Create D&ump File").Name = "Dump";
        cms.Items.AddSeparator();
        cms.Items.AddMenuItem("Set &Priority").Name = "Priority";
        cms.Items.AddMenuItem("Set &Affinity...").Name = "Affinity";
        cms.Items.AddSeparator();
        cms.Items.AddMenuItem("Search &Online").Name = "Online";
        cms.Items.AddMenuItem("Show &Locked Files").Name = "Files";
        ((ToolStripMenuItem)cms.Items["Priority"]).DropDownItems.AddMenuItem("RealTime").Name = "Priority1";
        ((ToolStripMenuItem)cms.Items["Priority"]).DropDownItems.AddMenuItem("High").Name = "Priority2";
        ((ToolStripMenuItem)cms.Items["Priority"]).DropDownItems.AddMenuItem("Above Normal").Name = "Priority3";
        ((ToolStripMenuItem)cms.Items["Priority"]).DropDownItems.AddMenuItem("Normal").Name = "Priority4";
        ((ToolStripMenuItem)cms.Items["Priority"]).DropDownItems.AddMenuItem("Below Normal").Name = "Priority5";
        ((ToolStripMenuItem)cms.Items["Priority"]).DropDownItems.AddMenuItem("Low").Name = "Priority6";
        ((ToolStripMenuItem)cms.Items["Priority"]).DropDownOpening += OnContextPriorityOpening;
        ((ToolStripMenuItem)cms.Items["Priority"]).DropDownItemClicked += OnContextItemClicked;
        ((ToolStripMenuItem)cms.Items["Details"]).SwitchToBold();
        cms.Opening += OnContextOpening;
        cms.ItemClicked += OnContextItemClicked;
    }

    private void OnKeyPress(object? sender, KeyPressEventArgs e) {
        lv.Focus();
        OnListViewKeyPress(lv, e);
    }
    private void OnVisibleChanged(object? sender, EventArgs e) {
        if (Visible && lv.Items.Count == 0 && Shared.InitComplete) {
            SuspendLayout(); Refresher(true); ResumeLayout();
        }
    }
    private void OnContextOpening(object? sender, CancelEventArgs e) {
        e.Cancel = lv.SelectedItems.Count == 0;

        try {
            // We should not allow actions on PID < bpi
            for (int i = 0; i < lv.SelectedItems.Count; i++) {
                if ((i + 1) > lv.SelectedItems.Count) { break; }
                if (int.Parse(lv.SelectedItems[i].Name) < Shared.bpi) {
                    if (lv.SelectedItems.Count == 1 && i == 0) {
                        // If its the only one selected then dont deselect it, just cancel 
                        e.Cancel = true;
                        return;
                    }
                    // Otherwise unselect it
                    lv.SelectedItems[i].Selected = false;
                    i -= 1;
                }
            }
            e.Cancel = lv.SelectedItems.Count == 0;
        } catch { Debug.WriteLine("Error Code 008a"); }

        cms.Items["Kill"].Enabled = lv.SelectedItems.Count >= 1;
        cms.Items["Pause"].Enabled = lv.SelectedItems.Count >= 1;
        cms.Items["Resume"].Enabled = lv.SelectedItems.Count >= 1;
        cms.Items["Priority"].Enabled = lv.SelectedItems.Count >= 1;
        cms.Items["Affinity"].Enabled = lv.SelectedItems.Count == 1;
        cms.Items["Details"].Enabled = lv.SelectedItems.Count == 1;
        cms.Items["Properties"].Enabled = lv.SelectedItems.Count == 1;
        cms.Items["Location"].Enabled = lv.SelectedItems.Count == 1;
        cms.Items["Parent"].Enabled = lv.SelectedItems.Count == 1;
        cms.Items["Debug"].Enabled = lv.SelectedItems.Count == 1;
        cms.Items["Dump"].Enabled = lv.SelectedItems.Count == 1;
        cms.Items["Files"].Enabled = lv.SelectedItems.Count == 1;
        cms.Items["Online"].Enabled = lv.SelectedItems.Count == 1;
    }
    private void OnContextPriorityOpening(object? sender, EventArgs e) {
        ProcessPriorityClass curPriority = 0;
        foreach (ListViewItem itm in lv.SelectedItems) {
            try {
                if (itm.Name == "" || !Shared.IsInteger(itm.Name)) continue;
                // This will trigger for the first item, so we save it
                if (curPriority == 0) { curPriority = Process.GetProcessById(int.Parse(itm.Name)).PriorityClass; continue; }
                // If any of the other process is diffferent, we set it to 0 and leave, they are not all equal.
                if (curPriority != Process.GetProcessById(int.Parse(itm.Name)).PriorityClass) { curPriority = 0; break; }
            } catch { }
        }
        try {
            ((ToolStripMenuItem)((ToolStripMenuItem)sender!).DropDownItems["Priority1"]).Checked = curPriority == ProcessPriorityClass.RealTime;
            ((ToolStripMenuItem)((ToolStripMenuItem)sender!).DropDownItems["Priority2"]).Checked = curPriority == ProcessPriorityClass.High;
            ((ToolStripMenuItem)((ToolStripMenuItem)sender!).DropDownItems["Priority3"]).Checked = curPriority == ProcessPriorityClass.AboveNormal;
            ((ToolStripMenuItem)((ToolStripMenuItem)sender!).DropDownItems["Priority4"]).Checked = curPriority == ProcessPriorityClass.Normal;
            ((ToolStripMenuItem)((ToolStripMenuItem)sender!).DropDownItems["Priority5"]).Checked = curPriority == ProcessPriorityClass.BelowNormal;
            ((ToolStripMenuItem)((ToolStripMenuItem)sender!).DropDownItems["Priority6"]).Checked = curPriority == ProcessPriorityClass.Idle;
        } catch { }
    }
    private void OnContextItemClicked(object? sender, ToolStripItemClickedEventArgs e) {
        if (e.ClickedItem == null) return;
        if (!e.ClickedItem.Enabled) return;
        switch (e.ClickedItem.Name) {
            case "Details": BeginInvoke(Feature_OpenDetails); break;
            case "Properties": BeginInvoke(Feature_OpenFileProperties); break;
            case "Location": BeginInvoke(Feature_OpenFileLocation); break;
            case "Parent": BeginInvoke(Feature_ProcessFindParent); break;
            case "Kill": BeginInvoke(Feature_ProcessKill); break;
            case "Pause": BeginInvoke(Feature_ProcessPause); break;
            case "Resume": BeginInvoke(Feature_ProcessResume); break;
            case "Debug": BeginInvoke(Feature_ProcessDebug); break;
            case "Dump": BeginInvoke(Feature_ProcessDump); break;
            case "Affinity": BeginInvoke(Feature_ProcessAffinity); break;
            case "Priority1": BeginInvoke(Feature_ProcessSetPriority, ProcessPriorityClass.RealTime); break;
            case "Priority2": BeginInvoke(Feature_ProcessSetPriority, ProcessPriorityClass.High); break;
            case "Priority3": BeginInvoke(Feature_ProcessSetPriority, ProcessPriorityClass.AboveNormal); break;
            case "Priority4": BeginInvoke(Feature_ProcessSetPriority, ProcessPriorityClass.Normal); break;
            case "Priority5": BeginInvoke(Feature_ProcessSetPriority, ProcessPriorityClass.BelowNormal); break;
            case "Priority6": BeginInvoke(Feature_ProcessSetPriority, ProcessPriorityClass.Idle); break;
            case "Online": BeginInvoke(Feature_OpenOnline); break;
            case "Files": BeginInvoke(Feature_OpenLockedFiles); break;
            default: break;
        }
    }
    private void OnButtonClicked(object? sender, EventArgs e) {
        if (sender == null) return;
        if (sender == btnDetails) { Feature_OpenDetails(); return; }
        if (sender == btnProperties) { Feature_OpenFileProperties(); return; }
        if (sender == btnKill) { Feature_ProcessKill(); return; }
        if (sender == btnForceRefresh) { Feature_ForceRefresh(); return; }
        if (sender == btnAllUsers) { Settings.ShowAllProcess = btnAllUsers.Checked; Feature_ForceRefresh(); return; }
    }
    private void OnListViewKeyDown(object? sender, KeyEventArgs e) {
        if (e.Control && e.KeyCode == Keys.A) {
            e.Handled = true;
            foreach (sMkListViewItem itm in lv.Items) { itm.Selected = true; }
        }
        if (e.KeyCode == Keys.Enter && !e.Handled) {
            e.Handled = true;
            btnDetails.PerformClick();
        }
    }
    private void OnListViewKeyPress(object? sender, KeyPressEventArgs e) {
        lv.KeyJumper("Name", ref e);
    }
    private void OnListViewColumnReordered(object? sender, ColumnReorderedEventArgs e) {
        if (e.Header!.Text == "PID") {
            e.Cancel = true;
        } else if (e.NewDisplayIndex == 0) {
            e.Cancel = true;
        }
    }
    private void OnListViewMouseDoubleClick(object? sender, MouseEventArgs e) {
        if (e.Button == MouseButtons.Left) {
            ListViewHitTestInfo ht = lv.HitTest(e.X, e.Y);
            if (ht.Item != null && ht.SubItem != null && ht.SubItem.Name.Equals("ParentPID")) {
                Feature_ProcessFindParent(); return;
            } else if (lv.SelectedItems.Count > 0) {
                Feature_OpenDetails(); return;
            }
        }
    }
    private void OnListViewSelectedIndexChanged(object? sender, EventArgs e) {
        btnDetails.Enabled = lv.SelectedItems.Count == 1;
        btnProperties.Enabled = lv.SelectedItems.Count == 1;
        btnKill.Enabled = lv.SelectedItems.Count > 0;
        //if (ShowProcessSummary && lv.SelectedItems.Count > 0) {
        //    TaskManagerProcess thisProcess = m_Processes.GetProcess(int.Parse(lv.SelectedItems[0].Name));
        //    if (thisProcess != null) {
        //        proc_SummaryView.Values = thisProcess.GetValuesFromTags(proc_SummaryView.RowsTags);
        //    }
        //    thisProcess = null;
        //} else if (ShowProcessSummary && proc_ListView.SelectedItems.Count == 0) {
        //    proc_SummaryView.ClearValues();
        //}
        RefreshInfoText();
    }

    public void Feature_ForceRefresh() {
        lv.SuspendLayout();
        Processes.Clear();
        lv.Items.Clear();
        Refresher(true);
        lv.ResumeLayout();

        ForceRefreshClicked?.Invoke(this, EventArgs.Empty);
        OnListViewSelectedIndexChanged(lv, new EventArgs());
    }
    public void Feature_OpenDetails() {
        if (lv.SelectedItems.Count > 0) {
            if (int.Parse(lv.SelectedItems[0].Name) < Shared.bpi) { return; }
            Shared.OpenProcessForm(int.Parse(lv.SelectedItems[0].Name));
        }
    }
    public void Feature_OpenFileProperties() {
        if (lv.SelectedItems.Count < 1) return;
        if (string.IsNullOrEmpty(lv.SelectedItems[0].Name)) return;
        if (int.Parse(lv.SelectedItems[0].Name) < Shared.bpi) return;
        try {
            string file = Process.GetProcessById(int.Parse(lv.SelectedItems[0].Name)).Modules[0].FileName;
            if (File.Exists(file)) {
                API.SHELLEXECUTEINFO sei = new();
                sei.cbSize = Marshal.SizeOf(sei);
                sei.lpVerb = "properties";
                sei.lpFile = file;
                sei.nShow = 5;
                sei.fMask = 12;
                API.ShellExecuteEx(ref sei);
            }
        } catch (Exception ex) { Shared.DebugTrap(ex, 009); }


    }
    public void Feature_OpenFileLocation() {
        if (lv.SelectedItems.Count < 1) return;
        if (string.IsNullOrEmpty(lv.SelectedItems[0].Name)) return;
        if (int.Parse(lv.SelectedItems[0].Name) < Shared.bpi) return;
        try {
            string file = Process.GetProcessById(int.Parse(lv.SelectedItems[0].Name)).Modules[0].FileName;
            if (File.Exists(file)) API.ShellExecute(Handle, "open", Path.GetDirectoryName(file)!, "", "", 1);
        } catch (Exception ex) { Shared.DebugTrap(ex, 010); }

    }
    public void Feature_OpenLockedFiles() {
        if (lv.SelectedItems.Count < 1) return;
        if (string.IsNullOrEmpty(lv.SelectedItems[0].Name)) return;
        if (int.Parse(lv.SelectedItems[0].Name) < Shared.bpi) return;

        using frmProcess_Files pf = new();
        pf.PID = int.Parse(lv.SelectedItems[0].Name);
        pf.StartPosition = FormStartPosition.CenterParent;
        pf.ShowDialog(this);
    }
    public void Feature_OpenOnline() {
        if (lv.SelectedItems.Count < 1) return;
        try {
            string QueryAddress = "http://www.google.com/search?q=" + Processes.GetProcess(int.Parse(lv.SelectedItems[0].Name))?.Name;
            Process.Start(QueryAddress);
        } catch { }
    }
    public void Feature_ProcessKill() {
        if (lv.SelectedItems.Count < 1) return;
        if (lv.SelectedItems.Count > 1) {
            if (!(MessageBox.Show("Are you sure you want to kill all selected processes?", ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)) {
                return;
            }
        }
        foreach (ListViewItem itm in lv.SelectedItems) {
            try {
                if (int.Parse(itm.Name) < Shared.bpi) continue;
                Processes.GetProcess(int.Parse(itm.Name))?.Kill();
                Shared.SetStatusText("Process " + itm.Name + " Terminated...");
            } catch { }
        }
        if (lv.SelectedItems.Count > 1) Shared.SetStatusText("Selected Processes Terminated...");
        Refresher();
    }
    public void Feature_ProcessPause() {
        if (lv.SelectedItems.Count < 1) return;
        if (lv.SelectedItems.Count > 1) {
            if (!(MessageBox.Show("Are you sure you want to freeze multiple selected processes?", ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)) {
                return;
            }
        }
        foreach (ListViewItem itm in lv.SelectedItems) {
            try {
                if (int.Parse(itm.Name) < Shared.bpi) continue;
                Processes.GetProcess(int.Parse(itm.Name))?.Pause();
                Shared.SetStatusText("Process " + itm.Name + " Paused...");
            } catch { }
        }
        if (lv.SelectedItems.Count > 1) Shared.SetStatusText("Selected Processes Paused...");
        Refresher();
    }
    public void Feature_ProcessResume() {
        if (lv.SelectedItems.Count < 1) return;
        foreach (ListViewItem itm in lv.SelectedItems) {
            try {
                if (int.Parse(itm.Name) < Shared.bpi) continue;
                Processes.GetProcess(int.Parse(itm.Name))?.Pause();
                Shared.SetStatusText("Process " + itm.Name + " Resumed...");
            } catch { }
        }
        if (lv.SelectedItems.Count > 1) Shared.SetStatusText("Selected Processes Resumed...");
        Refresher();
    }
    public void Feature_ProcessDebug() {
        if (lv.SelectedItems.Count < 1) return;
        if (string.IsNullOrEmpty(lv.SelectedItems[0].Name)) return;
        if (int.Parse(lv.SelectedItems[0].Name) < Shared.bpi) return;
        if (Shared.DebuggerCmd == "") { Shared.GetDebuggerCmd(); }
        if (Shared.DebuggerCmd == "") { MessageBox.Show("Unable to locate debugger", "Debug Process", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
        if (MessageBox.Show("WARNING: Debugging this process may result in loss of data." + Environment.NewLine + "Are you sure you wish to attach the debugger?", "Debug Process", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK) {
            ProcessStartInfo psi = new() {
                FileName = Shared.DebuggerCmd,
                Arguments = "-p " + lv.SelectedItems[0].Name,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Normal
            };
            Process.Start(psi);
        }
    }
    public void Feature_ProcessDump() {
        if (lv.SelectedItems.Count < 1) return;
        if (string.IsNullOrEmpty(lv.SelectedItems[0].Name)) return;
        if (int.Parse(lv.SelectedItems[0].Name) < Shared.bpi) return;

        TaskManagerProcess p = Processes.GetProcess(int.Parse(lv.SelectedItems[0].Name))!;
        if (p == null) return;
        SaveFileDialog fsd = new() {
            DefaultExt = "dmp",
            FileName = p.Name.Replace(".exe", ""),
            Filter = "Dump File (*.dmp)|*.dmp",
            OverwritePrompt = true,
            ValidateNames = true
        };
        if (fsd.ShowDialog() == DialogResult.OK) {
            Cursor = Cursors.AppStarting;
            if (p.DumpFile(fsd.FileName)) {
                Shared.SetStatusText("Process Dump Complete: " + fsd.FileName + " ... ");
            } else {
                Shared.SetStatusText("Process Dump Failed ... ");
            }
            Cursor = Cursors.Default;
        }
    }
    public void Feature_ProcessAffinity() {
        if (lv.SelectedItems.Count < 1) return;
        using frmProcess_Affinity pa = new();
        try {
            pa.BitMask = Convert.ToInt64(Process.GetProcessById(int.Parse(lv.SelectedItems[0].Name)).ProcessorAffinity);
        } catch (Exception ex) {
            Shared.SetStatusText($"Cant get current affinity for this process: {ex.Message}");
            pa.Dispose(); return;
        }
        pa.StartPosition = FormStartPosition.CenterParent;
        pa.ShowDialog(this);
        if (pa.DialogResult == DialogResult.OK) {
            int total = 0;
            var theBitMask = pa.BitMask;
            foreach (ListViewItem itm in lv.SelectedItems) {
                try {
                    if (int.Parse(itm.Name) < Shared.bpi) continue;
                    Process p = Process.GetProcessById(int.Parse(itm.Name));
                    if (p.ProcessorAffinity.ToString() == theBitMask.ToString()) { continue; }
                    p.ProcessorAffinity = (nint)theBitMask;
                    p.Dispose(); total++;
                } catch (Exception ex) {
                    MessageBox.Show("Error setting process affinity for PID " + int.Parse(itm.Name) + Environment.NewLine + ex.Message, "Unable to set process affinity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (total > 0) Shared.SetStatusText($"Process{(total > 1 ? "es" : "")} affinity set to: {theBitMask} ...");
        }
    }
    public void Feature_ProcessSetPriority(ProcessPriorityClass newPriority) {
        if (lv.SelectedItems.Count < 1) return;
        int total = 0;
        foreach (ListViewItem itm in lv.SelectedItems) {
            try {
                if (string.IsNullOrEmpty(itm.Name)) continue;
                if (int.Parse(itm.Name) < Shared.bpi) continue;
                Process p = Process.GetProcessById(int.Parse(itm.Name));
                if (p.PriorityClass != newPriority) { p.PriorityClass = newPriority; }
                p.Dispose(); total++;
            } catch (Exception ex) {
                MessageBox.Show("Error setting process priority for PID " + int.Parse(itm.Name) + Environment.NewLine + ex.Message, "Unable to set process priority", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        if (total > 0) Shared.SetStatusText($"Process{(total > 1 ? "es" : "")} priority set to: {newPriority} ...");
    }
    public void Feature_ProcessFindParent() {
        if (lv.SelectedItems.Count < 1) return;
        if (string.IsNullOrEmpty(lv.SelectedItems[0].Name)) return;
        if (int.Parse(lv.SelectedItems[0].Name) < Shared.bpi) return;

        string? PID = Processes.GetProcess(int.Parse(lv.SelectedItems[0].Name))?.ParentPID.ToString();
        if (string.IsNullOrEmpty(PID)) return;
        if (!lv.Items.ContainsKey(PID)) { Shared.SetStatusText($"Sorry, PID {PID} not in Process List"); return; }

        lv.SelectedItems.Clear();
        lv.Items[PID].Selected = true;
        lv.Items[PID].Focused = true;
        lv.Items[PID].EnsureVisible();
    }

    public sMkListView ListView => lv;
    public string Title { get; set; } = "Processes";
    public string Description { get; set; } = "Processes";
    public string TimmingKey => "Procs";
    public long TimmingValue => _stopWatch.ElapsedMilliseconds;
    public bool CanSelectColumns => true;
    public TaskManagerColumnTypes ColumnType => TaskManagerColumnTypes.Process;
    public void ForceRefresh() => btnForceRefresh.PerformClick();
    public ListView.ColumnHeaderCollection? GetColumns() => lv.Columns;
    public void SetColumns(in ListView.ListViewItemCollection colItems) {
        lv.SetColumns(colItems);
        ColsProcesses = lv.Columns.Cast<ColumnHeader>().Select(x => x.Name).ToHashSet()!;
    }

    public bool AllUsers => btnAllUsers.Checked;
    public void RefreshInfoText() {
        lblText.Text = string.Format("Total: {0}, Selected: {1}", lv.Items.Count, lv.SelectedItems.Count);
    }
    public string InfoText {
        get { return lblText.Text; }
        set { lblText.Text = value; }
    }

    private void RefresherDoWork(bool firstTime = false) {
        RefreshStarts?.Invoke(this, EventArgs.Empty);
        if (lv.Items.Count == 0) firstTime = true;

        // Store last round items and initialize new ones
        HashSet<int> LastRun = new();
        LastRun.UnionWith(HashProcesses);
        HashProcesses.Clear();
        // Allocate Main Pointer and offset
        nint lastOffset = 0;
        IntPtr hmain = IntPtr.Zero;
        if (TaskManagerProcess.GetProcessesPointer(ref hmain)) {
            API.SYSTEM_PROCESS_INFORMATION spi;
            spi = (API.SYSTEM_PROCESS_INFORMATION)Marshal.PtrToStructure(hmain, typeof(API.SYSTEM_PROCESS_INFORMATION))!;
            Array.Resize(ref spi.Threads, (int)spi.NumberOfThreads);
            lastOffset = hmain;
            while (spi.NextEntryOffset >= 0) {
                if (AllUsers || spi.SessionId == Shared.CurrentSessionID || spi.UniqueProcessId == 0) {
                    TaskManagerProcess? thisProcess;
                    HashProcesses.Add(spi.UniqueProcessId.ToInt32());
                    if (Processes.Contains(spi.UniqueProcessId)) {
                        thisProcess = Processes.GetProcess(spi.UniqueProcessId)!;
                        if (thisProcess.BackColor == Settings.Highlights.NewColor) thisProcess.BackColor = Color.Empty;
                        try {
                            thisProcess.Update(spi, ColsProcesses);
                        } catch (Exception ex) { Shared.DebugTrap(ex, 021); }
                    } else {
                        thisProcess = new TaskManagerProcess(spi.UniqueProcessId);
                        try {
                            thisProcess.Load(spi, ColsProcesses);
                        } catch (Exception ex) { Shared.DebugTrap(ex, 022); }
                        if (Settings.Highlights.NewItems && !firstTime) thisProcess.BackColor = Settings.Highlights.NewColor;

                        if (spi.UniqueProcessId == 0) {
                            thisProcess.ImageIndex = 0;
                        } else {
                            if (spi.UniqueProcessId > Shared.bpi && Settings.IconsInProcess) {
                                thisProcess.ImageIndex = -1;
                                thisProcess.ImageKey = Shared.ProcessIconImageKey(thisProcess.PID, thisProcess.Name, thisProcess.ImagePath);
                            } else { thisProcess.ImageIndex = 1; }
                        }
                        Processes.Add(thisProcess);
                    }
                }
                if (spi.NextEntryOffset > 0) {
                    lastOffset += (nint)spi.NextEntryOffset;
                    spi = (API.SYSTEM_PROCESS_INFORMATION)Marshal.PtrToStructure(lastOffset, typeof(API.SYSTEM_PROCESS_INFORMATION))!;
                    Array.Resize(ref spi.Threads, (int)spi.NumberOfThreads);
                } else { break; }
            }
            Marshal.FreeHGlobal(hmain);
        }
        // Clean old Items
        LastRun.ExceptWith(HashProcesses);
        foreach (var pid in LastRun) {
            TaskManagerProcess? thisProcess = Processes.GetProcess(pid);
            if (thisProcess == null) continue;
            if (thisProcess.BackColor == Settings.Highlights.RemovedColor || Settings.Highlights.RemovedItems == false) {
                lv.RemoveItemByKey(thisProcess.ID);
                Processes.Remove(thisProcess);
            } else {
                thisProcess.BackColor = Settings.Highlights.RemovedColor;
                HashProcesses.Add(thisProcess.PID);
            }
        }
        // Set LV First Column Width, only if firstTime
        if (firstTime) lv.Columns[0].Width = -1;
        if (firstTime) RefreshInfoText();
        RefreshComplete?.Invoke(this, EventArgs.Empty);
    }
    public void Refresher(bool firstTime = false) {
        _stopWatch.Restart();
        if (Visible || firstTime) {
            if (InvokeRequired) {
                Invoke(() => RefresherDoWork(firstTime));
            } else {
                RefresherDoWork(firstTime);
            }
        }
        _stopWatch.Stop();
    }
    public void LoadSettings() {
        Settings.LoadColsInformation(TaskManagerColumnTypes.Process, lv, ref ColsProcesses);
    }
    public bool SaveSettings() {
        return Settings.SaveColsInformation("colsProcess", lv);
    }
    public void ApplySettings() {
        lv.AlternateRowColors = Settings.AlternateRowColors;
        lv.SmallImageList = Settings.IconsInProcess ? Shared.ilProcesses : null;
        lv.SpaceFirstValue = Settings.IconsInProcess;
        btnAllUsers.Checked = Settings.ShowAllProcess;
    }

}