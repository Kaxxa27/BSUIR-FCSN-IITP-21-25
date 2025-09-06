using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Versioning;
using sMkTaskManager.Classes;
using sMkTaskManager.Controls;
namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
internal class tabUsers : UserControl, ITaskManagerTab {
    private readonly Stopwatch _stopWatch = new();
    private HashSet<string> ColsUsers = new();
    private readonly HashSet<string> HashUsers = new();
    private readonly TaskManagerUserCollection Users = new();

    private sMkListView lv;
    private Button btnForceRefresh;
    private ContextMenuStrip cms;
    private Button btnDisconnect;
    private Button btnLogoff;
    private Button btnMessage;
    private Button btnProperties;
    private ToolStripMenuItem cmsMessage;
    private ToolStripMenuItem cmsConnect;
    private ToolStripMenuItem cmsDisconnect;
    private ToolStripMenuItem cmsLogoff;
    private ToolStripMenuItem cmsProperties;
    private ToolStripSeparator cmsSeparator1;
    private ToolStripSeparator cmsSeparator2;
    private ImageList il;

    public event EventHandler? ForceRefreshClicked;
    public event EventHandler? RefreshStarts;
    public event EventHandler? RefreshComplete;

    private IContainer? components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }

    public tabUsers() {
        InitializeComponent();
        InitializeExtras();
        Extensions.CascadingDoubleBuffer(this);
    }
    private void InitializeComponent() {
        components = new Container();
        btnForceRefresh = new Button();
        lv = new sMkListView();
        cms = new ContextMenuStrip(components);
        cmsMessage = new ToolStripMenuItem();
        cmsConnect = new ToolStripMenuItem();
        cmsDisconnect = new ToolStripMenuItem();
        cmsLogoff = new ToolStripMenuItem();
        cmsProperties = new ToolStripMenuItem();
        il = new ImageList(components);
        btnDisconnect = new Button();
        btnLogoff = new Button();
        btnMessage = new Button();
        btnProperties = new Button();
        cmsSeparator1 = new ToolStripSeparator();
        cmsSeparator2 = new ToolStripSeparator();
        cms.SuspendLayout();
        SuspendLayout();
        // 
        // btnForceRefresh
        // 
        btnForceRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnForceRefresh.Location = new Point(505, 571);
        btnForceRefresh.Margin = new Padding(3, 3, 0, 3);
        btnForceRefresh.Name = "btnForceRefresh";
        btnForceRefresh.Size = new Size(89, 30);
        btnForceRefresh.TabIndex = 5;
        btnForceRefresh.Text = "Force Refresh";
        // 
        // lv
        // 
        lv.AlternateRowColors = false;
        lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lv.ContextMenuStrip = cms;
        lv.FullRowSelect = true;
        lv.GridLines = true;
        lv.LabelWrap = false;
        lv.Location = new Point(6, 6);
        lv.Margin = new Padding(6, 6, 6, 0);
        lv.Name = "lv";
        lv.ShowGroups = false;
        lv.ShowItemToolTips = true;
        lv.Size = new Size(588, 559);
        lv.SmallImageList = il;
        lv.Sortable = true;
        lv.SortColumn = 0;
        lv.Sorting = SortOrder.Ascending;
        lv.TabIndex = 0;
        lv.UseCompatibleStateImageBehavior = false;
        lv.View = View.Details;
        // 
        // cms
        // 
        cms.Items.AddRange(new ToolStripItem[] { cmsMessage, cmsSeparator1, cmsConnect, cmsDisconnect, cmsLogoff, cmsSeparator2, cmsProperties });
        cms.Name = "cms";
        cms.Size = new Size(159, 126);
        // 
        // cmsMessage
        // 
        cmsMessage.Name = "cmsMessage";
        cmsMessage.Size = new Size(158, 22);
        cmsMessage.Text = "Send &Message...";
        // 
        // cmsConnect
        // 
        cmsConnect.Name = "cmsConnect";
        cmsConnect.Size = new Size(158, 22);
        cmsConnect.Text = "&Connect";
        // 
        // cmsDisconnect
        // 
        cmsDisconnect.Name = "cmsDisconnect";
        cmsDisconnect.Size = new Size(158, 22);
        cmsDisconnect.Text = "&Disconnect";
        // 
        // cmsLogoff
        // 
        cmsLogoff.Name = "cmsLogoff";
        cmsLogoff.Size = new Size(158, 22);
        cmsLogoff.Text = "&Logoff";
        // 
        // cmsProperties
        // 
        cmsProperties.Name = "cmsProperties";
        cmsProperties.Size = new Size(158, 22);
        cmsProperties.Text = "P&roperties";
        // 
        // il
        // 
        il.ColorDepth = ColorDepth.Depth32Bit;
        il.ImageSize = new Size(16, 16);
        il.TransparentColor = Color.Transparent;
        // 
        // btnDisconnect
        // 
        btnDisconnect.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnDisconnect.Enabled = false;
        btnDisconnect.Location = new Point(6, 571);
        btnDisconnect.Margin = new Padding(3, 3, 0, 3);
        btnDisconnect.Name = "btnDisconnect";
        btnDisconnect.Size = new Size(85, 30);
        btnDisconnect.TabIndex = 1;
        btnDisconnect.Text = "Disconnect";
        // 
        // btnLogoff
        // 
        btnLogoff.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnLogoff.Enabled = false;
        btnLogoff.Location = new Point(94, 571);
        btnLogoff.Margin = new Padding(3, 3, 0, 3);
        btnLogoff.Name = "btnLogoff";
        btnLogoff.Size = new Size(65, 30);
        btnLogoff.TabIndex = 2;
        btnLogoff.Text = "Logoff";
        // 
        // btnMessage
        // 
        btnMessage.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnMessage.Enabled = false;
        btnMessage.Location = new Point(162, 571);
        btnMessage.Margin = new Padding(3, 3, 0, 3);
        btnMessage.Name = "btnMessage";
        btnMessage.Size = new Size(95, 30);
        btnMessage.TabIndex = 3;
        btnMessage.Text = "Send Message";
        // 
        // btnProperties
        // 
        btnProperties.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnProperties.Enabled = false;
        btnProperties.Location = new Point(260, 571);
        btnProperties.Margin = new Padding(3, 3, 0, 3);
        btnProperties.Name = "btnProperties";
        btnProperties.Size = new Size(85, 30);
        btnProperties.TabIndex = 4;
        btnProperties.Text = "Properties";
        // 
        // cmsSeparator1
        // 
        cmsSeparator1.Name = "cmsSeparator1";
        cmsSeparator1.Size = new Size(155, 6);
        // 
        // cmsSeparator2
        // 
        cmsSeparator2.Name = "cmsSeparator2";
        cmsSeparator2.Size = new Size(155, 6);
        // 
        // tabUsers
        // 
        Controls.Add(lv);
        Controls.Add(btnDisconnect);
        Controls.Add(btnLogoff);
        Controls.Add(btnMessage);
        Controls.Add(btnProperties);
        Controls.Add(btnForceRefresh);
        Name = "tabUsers";
        Size = new Size(600, 600);
        cms.ResumeLayout(false);
        ResumeLayout(false);
    }
    private void InitializeExtras() {
        il.Images.Clear();
        il.Images.Add(Resources.Resources.User_Active);
        il.Images.Add(Resources.Resources.User_Down);
        lv.ContentType = typeof(TaskManagerUser);
        lv.DataSource = Users.DataExporter;
        lv.SpaceFirstValue = true;
        cmsProperties.SwitchToBold();
        // Add event handlers
        KeyPress += OnKeyPress;
        VisibleChanged += OnVisibleChanged;
        cms.Opening += OnContextOpening;
        cms.ItemClicked += OnContextItemClicked;
        lv.ColumnReordered += OnListViewColumnReordered;
        lv.KeyDown += OnListViewKeyDown;
        lv.KeyPress += OnListViewKeyPress;
        lv.SelectedIndexChanged += OnListViewSelectedIndexChanged;
        lv.MouseDoubleClick += OnListViewMouseDoubleClick;
        btnDisconnect.Click += OnButtonClicked;
        btnLogoff.Click += OnButtonClicked;
        btnMessage.Click += OnButtonClicked;
        btnProperties.Click += OnButtonClicked;
        btnForceRefresh.Click += OnButtonClicked;
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
        cmsProperties.Enabled = lv.SelectedItems.Count == 1;
    }
    private void OnContextItemClicked(object? sender, ToolStripItemClickedEventArgs e) {
        if (e.ClickedItem == null) return;
        if (!e.ClickedItem.Enabled) return;
        switch (e.ClickedItem.Name) {
            case nameof(cmsConnect): { BeginInvoke(Feature_Connect); break; }
            case nameof(cmsDisconnect): { BeginInvoke(Feature_Disconnect); break; }
            case nameof(cmsLogoff): { BeginInvoke(Feature_Logoff); break; }
            case nameof(cmsMessage): { BeginInvoke(Feature_SendMessage); break; }
            case nameof(cmsProperties): { BeginInvoke(Feature_OpenDetails); break; }
            default: break;
        }
    }
    private void OnButtonClicked(object? sender, EventArgs e) {
        if (sender == null) return;
        if (sender == btnDisconnect) { BeginInvoke(Feature_Disconnect); return; }
        if (sender == btnLogoff) { BeginInvoke(Feature_Logoff); return; }
        if (sender == btnMessage) { BeginInvoke(Feature_SendMessage); return; }
        if (sender == btnProperties) { BeginInvoke(Feature_OpenDetails); return; }
        if (sender == btnForceRefresh) { BeginInvoke(Feature_ForceRefresh); return; }
    }
    private void OnListViewKeyDown(object? sender, KeyEventArgs e) {
        if (e.Control && e.KeyCode == Keys.A) {
            e.Handled = true;
            foreach (sMkListViewItem itm in lv.Items) { itm.Selected = true; }
        } else if (e.KeyCode == Keys.Enter && !e.Handled) {
            e.Handled = true;
            cmsProperties.PerformClick();
            e.SuppressKeyPress = true;
        }

    }
    private void OnListViewKeyPress(object? sender, KeyPressEventArgs e) {
        lv.KeyJumper("Username", ref e);
    }
    private void OnListViewColumnReordered(object? sender, ColumnReorderedEventArgs e) {
        if (e.Header!.Text == "ID") { e.Cancel = true; }
        if (e.NewDisplayIndex == 0) { e.Cancel = true; }
    }
    private void OnListViewMouseDoubleClick(object? sender, MouseEventArgs e) {
        if (lv.SelectedItems.Count > 0 && e.Button == MouseButtons.Left) { BeginInvoke(Feature_OpenDetails); }
    }
    private void OnListViewSelectedIndexChanged(object? sender, EventArgs e) {
        cmsConnect.Enabled = false;
        cmsDisconnect.Enabled = false;
        cmsLogoff.Enabled = false;
        cmsProperties.Enabled = lv.SelectedItems.Count == 1;
        btnProperties.Enabled = lv.SelectedItems.Count == 1;
        cmsMessage.Enabled = lv.SelectedItems.Count > 0;

        for (int i = 0; i < lv.SelectedItems.Count; i++) {
            TaskManagerUser tmp = Users.GetUser(lv.SelectedItems[i].Name);
            if (tmp == null) continue;
            if (tmp.CanConnect()) cmsConnect.Enabled = true;
            if (tmp.CanDisconnect()) cmsDisconnect.Enabled = true;
            if (tmp.CanLogOff()) cmsLogoff.Enabled = true;
        }

        btnDisconnect.Enabled = cmsDisconnect.Enabled;
        btnLogoff.Enabled = cmsLogoff.Enabled;
        btnMessage.Enabled = cmsMessage.Enabled;
    }

    public void Feature_ForceRefresh() {
        lv.SuspendLayout();
        Users.Clear();
        lv.Items.Clear();
        Refresher(true);
        lv.ResumeLayout();
        OnListViewSelectedIndexChanged(lv, EventArgs.Empty);
        ForceRefreshClicked?.Invoke(this, EventArgs.Empty);
    }
    public void Feature_Connect() {
        if (lv.SelectedItems.Count != 1)  return;

        using frmUser_Connect uc = new();
        uc.ShowInTaskbar = false;
        uc.txtUsername.Text = Users.GetUser(lv.SelectedItems[0].Name).User;
        uc.txtPassword.UseSystemPasswordChar = true;
        uc.txtPassword.Focus();
        uc.ActiveControl = uc.txtPassword;
        if (uc.ShowDialog(this) == DialogResult.OK) {
            Shared.BusyCursor = true;
            Shared.SetStatusText("Connecting to user ...");
            if (Users.GetUser(lv.SelectedItems[0].Name).Connect(false, uc.txtPassword.Text)) {
                Shared.SetStatusText("Connected to user ...");
            } else {
                Shared.SetStatusText("Failed to connect to user ...");
            }
            lv.Focus();
            Shared.BusyCursor = false;
        }
    }
    public void Feature_Disconnect() {
        if (lv.SelectedItems.Count < 1) return;
        if (lv.SelectedItems.Count > 1) {
            if (!(MessageBox.Show("Are you sure you want to disconnect selected users?", ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) == DialogResult.Yes)) {
                return;
            }
        }
        Shared.BusyCursor = true;
        Shared.SetStatusText("Disconnecting user(s) ...");
        bool allOK = true;
        for (int i = 0; i < lv.SelectedItems.Count; i++) {
            if (!Users.GetUser(lv.SelectedItems[i].Name).CanDisconnect()) continue;
            if (!Users.GetUser(lv.SelectedItems[i].Name).Disconnect(lv.SelectedItems.Count == 1)) allOK = false;
        }
        if (allOK) {
            Shared.SetStatusText("User(s) disconnected ...");
        } else {
            Shared.SetStatusText(lv.SelectedItems.Count > 1 ? "At least one user failed to disconnect ..." : "User failed to disconnect ...");
        }
        lv.Focus();
        Refresher(true);
        OnListViewSelectedIndexChanged(lv, EventArgs.Empty);
        Shared.BusyCursor = false;

    }
    public void Feature_Logoff() {
        if (lv.SelectedItems.Count < 1) return;
        if (lv.SelectedItems.Count > 1) {
            if (!(MessageBox.Show("Are you sure you want to logoff selected users?", ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) == DialogResult.Yes)) {
                return;
            }
        }
        Shared.BusyCursor = true;
        Shared.SetStatusText("Logging off user(s) ...");
        bool allOK = true;
        for (int i = 0; i < lv.SelectedItems.Count; i++) {
            if (!Users.GetUser(lv.SelectedItems[i].Name).CanLogOff()) continue;
            if (!Users.GetUser(lv.SelectedItems[i].Name).LogOff(lv.SelectedItems.Count == 1)) allOK = false;
        }
        if (allOK) {
            Shared.SetStatusText("User(s) logged off ...");
        } else {
            Shared.SetStatusText(lv.SelectedItems.Count > 1 ? "At least one user failed to log off ..." : "User failed to log off...");
        }
        lv.Focus();
        Refresher(true);
        OnListViewSelectedIndexChanged(lv, EventArgs.Empty);
        Shared.BusyCursor = false;
    }
    public void Feature_SendMessage() {
        if (lv.SelectedItems.Count < 1) return;
        using frmUser_Message um = new();
        um.ShowInTaskbar = false;
        if (um.ShowDialog(this) == DialogResult.OK) {
            Shared.BusyCursor = true;
            Shared.SetStatusText("Sending message to user(s) ...");
            bool allOK = true;
            Microsoft.VisualBasic.MsgBoxStyle iStyle = 0;
            switch (um.txtIcon.Text) {
                case "Information": iStyle |= Microsoft.VisualBasic.MsgBoxStyle.Information; break;
                case "Error": iStyle |= Microsoft.VisualBasic.MsgBoxStyle.Critical; break;
                case "Question": iStyle |= Microsoft.VisualBasic.MsgBoxStyle.Question; break;
                case "Warning": iStyle |= Microsoft.VisualBasic.MsgBoxStyle.Exclamation; break;
            }
            for (int i = 0; i < lv.SelectedItems.Count; i++) {
                if (!Users.GetUser(lv.SelectedItems[i].Name).Message(um.txtTitle.Text.Trim(), um.txtMessage.Text.Trim(), iStyle)) allOK = false;
            }
            if (allOK) {
                Shared.SetStatusText("User(s) messaged ...");
            } else {
                Shared.SetStatusText(lv.SelectedItems.Count > 1 ? "At least one user failed to get messaged..." : "User failed to get message ...");
            }
            lv.Focus();
            Shared.BusyCursor = false;
        }
    }
    public void Feature_OpenDetails() {
        if (lv.SelectedItems.Count > 0) {
            using (frmUser_Details ud = new()) {
                ud.ID = lv.SelectedItems[0].Name;
                ud.ShowInTaskbar = false;
                ud.ShowDialog(this);
            }
            OnListViewSelectedIndexChanged(lv, EventArgs.Empty);
        }

    }

    public sMkListView ListView => lv;
    public string Title { get; set; } = "Users";
    public string Description { get; set; } = "User Sessions";
    public string TimmingKey => "Users";
    public long TimmingValue => _stopWatch.ElapsedMilliseconds;
    public bool CanSelectColumns => false;
    public TaskManagerColumnTypes ColumnType => TaskManagerColumnTypes.Users;
    public void ForceRefresh() => btnForceRefresh.PerformClick();
    public ListView.ColumnHeaderCollection? GetColumns() => lv.Columns;
    public void SetColumns(in ListView.ListViewItemCollection colItems) {
        lv.SetColumns(colItems);
        ColsUsers = lv.Columns.Cast<ColumnHeader>().Select(x => x.Name).ToHashSet()!;
    }

    private void RefresherDoWork(bool firstTime = false) {
        RefreshStarts?.Invoke(this, EventArgs.Empty);
        if (lv.Items.Count == 0) firstTime = true;
        // Store last round items and initialize new ones
        HashSet<string> LastRun = new();
        LastRun.UnionWith(HashUsers);
        HashUsers.Clear();
        // Iterate through all the users
        foreach (TaskManagerUser c in TaskManagerUser.GetUsers()) {
            TaskManagerUser thisUser = new();
            HashUsers.Add(c.Ident);
            if (Users.Contains(c.Ident)) {
                thisUser = Users.GetUser(c.Ident);
                if (thisUser.BackColor == Settings.Highlights.NewColor) thisUser.BackColor = Color.Empty;
                try {
                    thisUser.Update(c);
                } catch (Exception ex) { Shared.DebugTrap(ex, 081); }
            } else {
                try {
                    thisUser.Load(c);
                } catch (Exception ex) { Shared.DebugTrap(ex, 082); }
                if (Settings.Highlights.NewItems && !firstTime) thisUser.BackColor = Settings.Highlights.NewColor;
                Users.Add(thisUser);
            }
        }
        // Clean out old Items
        LastRun.ExceptWith(HashUsers);
        for (int i = 0; i < LastRun.Count; i++) {
            TaskManagerUser thisUser = Users.GetUser(LastRun.ElementAtOrDefault(i)!);
            if (thisUser == null) continue;
            if (!Settings.Highlights.RemovedItems || thisUser.BackColor == Settings.Highlights.RemovedColor) {
                lv.RemoveItemByKey(thisUser.Ident);
                Users.Remove(thisUser);
            } else {
                thisUser.BackColor = Settings.Highlights.RemovedColor;
                HashUsers.Add(thisUser.Ident);
            }
        }
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
        Settings.LoadColsInformation(TaskManagerColumnTypes.Users, lv, ref ColsUsers);
    }

}
