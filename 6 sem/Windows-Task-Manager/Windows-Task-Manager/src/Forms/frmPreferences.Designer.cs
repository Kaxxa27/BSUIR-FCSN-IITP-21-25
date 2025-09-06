namespace sMkTaskManager.Forms;

partial class frmPreferences {

    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) { components.Dispose(); }
        base.Dispose(disposing);
    }

    private void InitializeComponent() {
        tc = new TabControl();
        tpGeneral = new TabPage();
        g_chkStoreXML = new CheckBox();
        g_gbTrayIcon = new GroupBox();
        t_chkShowCPU = new CheckBox();
        t_chkRequireDobleClick = new CheckBox();
        t_chkCloseToTray = new CheckBox();
        t_chkHideToTray = new CheckBox();
        g_btnHighlightFrozen = new Button();
        g_btnHighlightRemoved = new Button();
        g_btnHighlightChanges = new Button();
        g_chkHighlightFrozen = new CheckBox();
        g_chkHighlightRemoved = new CheckBox();
        g_chkHighlightChanges = new CheckBox();
        g_chkHighlightNew = new CheckBox();
        g_btnHighlightNew = new Button();
        g_chkShowIcons = new CheckBox();
        g_chkAlternateRowColors = new CheckBox();
        g_chkServicesStatus = new CheckBox();
        g_chkTimmingStatus = new CheckBox();
        g_chkRememberWindow = new CheckBox();
        g_chkMinimizeClose = new CheckBox();
        g_chkWindowOnTop = new CheckBox();
        g_chkStartMinimized = new CheckBox();
        tpPerformance = new TabPage();
        p_chkSeparateCpu = new CheckBox();
        p_chkKernelTime = new CheckBox();
        p_tbGridSize = new TrackBar();
        p_tbValueSpacing = new TrackBar();
        p_AverageLine = new ComboBox();
        p_lblGridSize = new Label();
        p_lblValueSpacing = new Label();
        p_Label5 = new Label();
        p_Label4 = new Label();
        p_Label3 = new Label();
        p_GridHorizontal = new ComboBox();
        p_GridVertical = new ComboBox();
        p_Label2 = new Label();
        p_AverageLineColor = new Button();
        p_GridHorizontalColor = new Button();
        p_GridVerticalColor = new Button();
        p_Label1 = new Label();
        p_lblNote = new Label();
        p_chkLightBackground = new CheckBox();
        p_chkOnlyOnHover = new CheckBox();
        p_chkShowLegends = new CheckBox();
        p_chkShowIndexes = new CheckBox();
        p_chkAvgValue = new CheckBox();
        p_chkShadeBackground = new CheckBox();
        p_chkAntiAlias = new CheckBox();
        p_chkSolidGraphs = new CheckBox();
        tpNetworking = new TabPage();
        n_Label7 = new Label();
        n_Label6 = new Label();
        n_btnDnColor = new Button();
        n_btnUpColor = new Button();
        n_chkKeepDrawing = new CheckBox();
        n_chkLightBackground = new CheckBox();
        n_chkShowLegends = new CheckBox();
        n_chkShowIndexes = new CheckBox();
        n_tbGridSize = new TrackBar();
        n_tbValueSpacing = new TrackBar();
        n_AverageLine = new ComboBox();
        n_lblGridSize = new Label();
        n_lblValueSpacing = new Label();
        n_Label5 = new Label();
        n_Label4 = new Label();
        n_Label3 = new Label();
        n_GridHorizontal = new ComboBox();
        n_GridVertical = new ComboBox();
        n_Label2 = new Label();
        n_AverageLineColor = new Button();
        n_GridHorizontalColor = new Button();
        n_GridVerticalColor = new Button();
        n_Label1 = new Label();
        n_chkAvgValue = new CheckBox();
        n_chkShadeBackground = new CheckBox();
        n_chkAntiAlias = new CheckBox();
        n_chkSolidGraphs = new CheckBox();
        btnCancel = new Button();
        btnApply = new Button();
        btnOk = new Button();
        cd = new ColorDialog();
        tc.SuspendLayout();
        tpGeneral.SuspendLayout();
        g_gbTrayIcon.SuspendLayout();
        tpPerformance.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)p_tbGridSize).BeginInit();
        ((System.ComponentModel.ISupportInitialize)p_tbValueSpacing).BeginInit();
        tpNetworking.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)n_tbGridSize).BeginInit();
        ((System.ComponentModel.ISupportInitialize)n_tbValueSpacing).BeginInit();
        SuspendLayout();
        // 
        // tc
        // 
        tc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tc.Controls.Add(tpGeneral);
       // tc.Controls.Add(tpPerformance);
       // tc.Controls.Add(tpNetworking);
        tc.Location = new Point(5, 4);
        tc.Margin = new Padding(0);
        tc.Name = "tc";
        tc.SelectedIndex = 0;
        tc.Size = new Size(456, 215);
        tc.TabIndex = 0;
        // 
        // tpGeneral
        // 
      //  tpGeneral.Controls.Add(g_chkStoreXML);
       // tpGeneral.Controls.Add(g_gbTrayIcon);
        tpGeneral.Controls.Add(g_btnHighlightFrozen);
        tpGeneral.Controls.Add(g_btnHighlightRemoved);
        tpGeneral.Controls.Add(g_btnHighlightChanges);
        tpGeneral.Controls.Add(g_chkHighlightFrozen);
        tpGeneral.Controls.Add(g_chkHighlightRemoved);
        tpGeneral.Controls.Add(g_chkHighlightChanges);
        tpGeneral.Controls.Add(g_chkHighlightNew);
        tpGeneral.Controls.Add(g_btnHighlightNew);
      //  tpGeneral.Controls.Add(g_chkShowIcons);
        //tpGeneral.Controls.Add(g_chkAlternateRowColors);
        //tpGeneral.Controls.Add(g_chkServicesStatus);
        //tpGeneral.Controls.Add(g_chkTimmingStatus);
        //tpGeneral.Controls.Add(g_chkRememberWindow);
        //tpGeneral.Controls.Add(g_chkMinimizeClose);
        //tpGeneral.Controls.Add(g_chkWindowOnTop);
        //tpGeneral.Controls.Add(g_chkStartMinimized);
        tpGeneral.Location = new Point(4, 24);
        tpGeneral.Name = "tpGeneral";
        tpGeneral.Padding = new Padding(3);
        tpGeneral.Size = new Size(448, 187);
        tpGeneral.TabIndex = 0;
        tpGeneral.Text = "General";
        tpGeneral.UseVisualStyleBackColor = true;
        // 
        // g_chkStoreXML
        // 
        g_chkStoreXML.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        g_chkStoreXML.AutoSize = true;
        g_chkStoreXML.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        g_chkStoreXML.Location = new Point(6, 168);
        g_chkStoreXML.Margin = new Padding(3, 0, 3, 0);
        g_chkStoreXML.Name = "g_chkStoreXML";
        g_chkStoreXML.Size = new Size(187, 17);
        g_chkStoreXML.TabIndex = 8;
        g_chkStoreXML.Text = "Store settings in file instead of registry";
        g_chkStoreXML.TextAlign = ContentAlignment.TopLeft;
        g_chkStoreXML.UseVisualStyleBackColor = true;
        // 
        // g_gbTrayIcon
        // 
        g_gbTrayIcon.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        g_gbTrayIcon.Controls.Add(t_chkShowCPU);
        g_gbTrayIcon.Controls.Add(t_chkRequireDobleClick);
        g_gbTrayIcon.Controls.Add(t_chkCloseToTray);
        g_gbTrayIcon.Controls.Add(t_chkHideToTray);
        g_gbTrayIcon.Location = new Point(245, 91);
        g_gbTrayIcon.Name = "g_gbTrayIcon";
        g_gbTrayIcon.Size = new Size(200, 93);
        g_gbTrayIcon.TabIndex = 17;
        g_gbTrayIcon.TabStop = false;
        g_gbTrayIcon.Text = "Tray Icon";
        // 
        // t_chkShowCPU
        // 
        t_chkShowCPU.AutoSize = true;
        t_chkShowCPU.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        t_chkShowCPU.Location = new Point(10, 71);
        t_chkShowCPU.Margin = new Padding(3, 0, 3, 0);
        t_chkShowCPU.Name = "t_chkShowCPU";
        t_chkShowCPU.Size = new Size(180, 17);
        t_chkShowCPU.TabIndex = 3;
        t_chkShowCPU.Text = "Display CPU usage as the icon";
        t_chkShowCPU.TextAlign = ContentAlignment.TopLeft;
        t_chkShowCPU.UseVisualStyleBackColor = true;
        // 
        // t_chkRequireDobleClick
        // 
        t_chkRequireDobleClick.AutoSize = true;
        t_chkRequireDobleClick.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        t_chkRequireDobleClick.Location = new Point(10, 54);
        t_chkRequireDobleClick.Margin = new Padding(3, 0, 3, 0);
        t_chkRequireDobleClick.Name = "t_chkRequireDobleClick";
        t_chkRequireDobleClick.Size = new Size(177, 17);
        t_chkRequireDobleClick.TabIndex = 2;
        t_chkRequireDobleClick.Text = "Require doble click to restore";
        t_chkRequireDobleClick.TextAlign = ContentAlignment.TopLeft;
        t_chkRequireDobleClick.UseVisualStyleBackColor = true;
        // 
        // t_chkCloseToTray
        // 
        t_chkCloseToTray.AutoSize = true;
        t_chkCloseToTray.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        t_chkCloseToTray.Location = new Point(10, 37);
        t_chkCloseToTray.Margin = new Padding(3, 0, 3, 0);
        t_chkCloseToTray.Name = "t_chkCloseToTray";
        t_chkCloseToTray.Size = new Size(154, 17);
        t_chkCloseToTray.TabIndex = 1;
        t_chkCloseToTray.Text = "Hide to tray when closed";
        t_chkCloseToTray.TextAlign = ContentAlignment.TopLeft;
        t_chkCloseToTray.UseVisualStyleBackColor = true;
        // 
        // t_chkHideToTray
        // 
        t_chkHideToTray.AutoSize = true;
        t_chkHideToTray.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        t_chkHideToTray.Location = new Point(10, 20);
        t_chkHideToTray.Margin = new Padding(3, 0, 3, 0);
        t_chkHideToTray.Name = "t_chkHideToTray";
        t_chkHideToTray.Size = new Size(173, 17);
        t_chkHideToTray.TabIndex = 0;
        t_chkHideToTray.Text = "Hide to tray when minimized";
        t_chkHideToTray.TextAlign = ContentAlignment.TopLeft;
        t_chkHideToTray.UseVisualStyleBackColor = true;
        // 
        // g_btnHighlightFrozen
        // 
        g_btnHighlightFrozen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        g_btnHighlightFrozen.BackColor = Color.Orchid;
        g_btnHighlightFrozen.Location = new Point(210, 75);
        g_btnHighlightFrozen.Name = "g_btnHighlightFrozen";
        g_btnHighlightFrozen.Size = new Size(30, 25);
        g_btnHighlightFrozen.TabIndex = 16;
        g_btnHighlightFrozen.UseVisualStyleBackColor = false;
        g_btnHighlightFrozen.Click += btnColors_Click;
        // 
        // g_btnHighlightRemoved
        // 
        g_btnHighlightRemoved.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        g_btnHighlightRemoved.BackColor = Color.LightCoral;
        g_btnHighlightRemoved.Location = new Point(210, 25);
        g_btnHighlightRemoved.Name = "g_btnHighlightRemoved";
        g_btnHighlightRemoved.Size = new Size(30, 25);
        g_btnHighlightRemoved.TabIndex = 14;
        g_btnHighlightRemoved.UseVisualStyleBackColor = false;
        g_btnHighlightRemoved.Click += btnColors_Click;
        // 
        // g_btnHighlightChanges
        // 
        g_btnHighlightChanges.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        g_btnHighlightChanges.BackColor = Color.PeachPuff;
        g_btnHighlightChanges.Location = new Point(210, 50);
        g_btnHighlightChanges.Name = "g_btnHighlightChanges";
        g_btnHighlightChanges.Size = new Size(30, 25);
        g_btnHighlightChanges.TabIndex = 12;
        g_btnHighlightChanges.UseVisualStyleBackColor = false;
        g_btnHighlightChanges.Click += btnColors_Click;
        // 
        // g_chkHighlightFrozen
        // 
        g_chkHighlightFrozen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        g_chkHighlightFrozen.AutoSize = true;
        g_chkHighlightFrozen.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        g_chkHighlightFrozen.Location = new Point(32, 75);
        g_chkHighlightFrozen.Margin = new Padding(3, 0, 3, 0);
        g_chkHighlightFrozen.Name = "g_chkHighlightFrozen";
        g_chkHighlightFrozen.Size = new Size(141, 20);
        g_chkHighlightFrozen.TabIndex = 15;
        g_chkHighlightFrozen.Text = "Highlight frozen items";
        g_chkHighlightFrozen.TextAlign = ContentAlignment.TopLeft;
        g_chkHighlightFrozen.UseVisualStyleBackColor = true;
        // 
        // g_chkHighlightRemoved
        // 
        g_chkHighlightRemoved.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        g_chkHighlightRemoved.AutoSize = true;
        g_chkHighlightRemoved.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        g_chkHighlightRemoved.Location = new Point(37, 25);
        g_chkHighlightRemoved.Margin = new Padding(3, 0, 3, 0);
        g_chkHighlightRemoved.Name = "g_chkHighlightRemoved";
        g_chkHighlightRemoved.Size = new Size(152, 20);
        g_chkHighlightRemoved.TabIndex = 13;
        g_chkHighlightRemoved.Text = "Highlight removed items";
        g_chkHighlightRemoved.TextAlign = ContentAlignment.TopLeft;
        g_chkHighlightRemoved.UseVisualStyleBackColor = true;
        // 
        // g_chkHighlightChanges
        // 
        g_chkHighlightChanges.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        g_chkHighlightChanges.AutoSize = true;
        g_chkHighlightChanges.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        g_chkHighlightChanges.Location = new Point(34, 50);
        g_chkHighlightChanges.Margin = new Padding(3, 0, 3, 0);
        g_chkHighlightChanges.Name = "g_chkHighlightChanges";
        g_chkHighlightChanges.Size = new Size(157, 17);
        g_chkHighlightChanges.TabIndex = 11;
        g_chkHighlightChanges.Text = "Highlight changing items";
        g_chkHighlightChanges.TextAlign = ContentAlignment.TopLeft;
        g_chkHighlightChanges.UseVisualStyleBackColor = true;
        // 
        // g_chkHighlightNew
        // 
        g_chkHighlightNew.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        g_chkHighlightNew.AutoSize = true;
        g_chkHighlightNew.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        g_chkHighlightNew.Location = new Point(30, 0);
        g_chkHighlightNew.Margin = new Padding(3, 0, 3, 0);
        g_chkHighlightNew.Name = "g_chkHighlightNew";
        g_chkHighlightNew.Size = new Size(130, 20);
        g_chkHighlightNew.TabIndex = 9;
        g_chkHighlightNew.Text = "Highlight new items";
        g_chkHighlightNew.TextAlign = ContentAlignment.TopLeft;
        g_chkHighlightNew.UseVisualStyleBackColor = true;
        // 
        // g_btnHighlightNew
        // 
        g_btnHighlightNew.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        g_btnHighlightNew.BackColor = Color.LightGreen;
        g_btnHighlightNew.Location = new Point(210, 0);
        g_btnHighlightNew.Name = "g_btnHighlightNew";
        g_btnHighlightNew.Size = new Size(30, 25);
        g_btnHighlightNew.TabIndex = 10;
        g_btnHighlightNew.UseVisualStyleBackColor = false;
        g_btnHighlightNew.Click += btnColors_Click;
        // 
        // g_chkShowIcons
        // 
        g_chkShowIcons.Anchor = AnchorStyles.Left;
        g_chkShowIcons.AutoSize = true;
        g_chkShowIcons.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        g_chkShowIcons.Location = new Point(6, 87);
        g_chkShowIcons.Margin = new Padding(3, 0, 3, 0);
        g_chkShowIcons.Name = "g_chkShowIcons";
        g_chkShowIcons.Size = new Size(152, 17);
        g_chkShowIcons.TabIndex = 4;
        g_chkShowIcons.Text = "Show icons of processes";
        g_chkShowIcons.TextAlign = ContentAlignment.TopLeft;
        g_chkShowIcons.UseVisualStyleBackColor = true;
        // 
        // g_chkAlternateRowColors
        // 
        g_chkAlternateRowColors.Anchor = AnchorStyles.Left;
        g_chkAlternateRowColors.AutoSize = true;
        g_chkAlternateRowColors.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        g_chkAlternateRowColors.Location = new Point(6, 104);
        g_chkAlternateRowColors.Margin = new Padding(3, 0, 3, 0);
        g_chkAlternateRowColors.Name = "g_chkAlternateRowColors";
        g_chkAlternateRowColors.Size = new Size(189, 17);
        g_chkAlternateRowColors.TabIndex = 5;
        g_chkAlternateRowColors.Text = "Alternate rows color in listviews";
        g_chkAlternateRowColors.TextAlign = ContentAlignment.TopLeft;
        g_chkAlternateRowColors.UseVisualStyleBackColor = true;
        // 
        // g_chkServicesStatus
        // 
        g_chkServicesStatus.Anchor = AnchorStyles.Left;
        g_chkServicesStatus.AutoSize = true;
        g_chkServicesStatus.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        g_chkServicesStatus.Location = new Point(6, 121);
        g_chkServicesStatus.Margin = new Padding(3, 0, 3, 0);
        g_chkServicesStatus.Name = "g_chkServicesStatus";
        g_chkServicesStatus.Size = new Size(197, 17);
        g_chkServicesStatus.TabIndex = 6;
        g_chkServicesStatus.Text = "Display running services in status";
        g_chkServicesStatus.TextAlign = ContentAlignment.TopLeft;
        g_chkServicesStatus.UseVisualStyleBackColor = true;
        // 
        // g_chkTimmingStatus
        // 
        g_chkTimmingStatus.Anchor = AnchorStyles.Left;
        g_chkTimmingStatus.AutoSize = true;
        g_chkTimmingStatus.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        g_chkTimmingStatus.Location = new Point(6, 138);
        g_chkTimmingStatus.Margin = new Padding(3, 0, 3, 0);
        g_chkTimmingStatus.Name = "g_chkTimmingStatus";
        g_chkTimmingStatus.Size = new Size(219, 17);
        g_chkTimmingStatus.TabIndex = 7;
        g_chkTimmingStatus.Text = "Display timming information in status";
        g_chkTimmingStatus.TextAlign = ContentAlignment.TopLeft;
        g_chkTimmingStatus.UseVisualStyleBackColor = true;
        // 
        // g_chkRememberWindow
        // 
        g_chkRememberWindow.AutoSize = true;
        g_chkRememberWindow.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        g_chkRememberWindow.Location = new Point(6, 57);
        g_chkRememberWindow.Margin = new Padding(3, 0, 3, 0);
        g_chkRememberWindow.Name = "g_chkRememberWindow";
        g_chkRememberWindow.Size = new Size(205, 17);
        g_chkRememberWindow.TabIndex = 3;
        g_chkRememberWindow.Text = "Remember window position && size";
        g_chkRememberWindow.TextAlign = ContentAlignment.TopLeft;
        g_chkRememberWindow.UseVisualStyleBackColor = true;
        // 
        // g_chkMinimizeClose
        // 
        g_chkMinimizeClose.AutoSize = true;
        g_chkMinimizeClose.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        g_chkMinimizeClose.Location = new Point(6, 40);
        g_chkMinimizeClose.Margin = new Padding(3, 0, 3, 0);
        g_chkMinimizeClose.Name = "g_chkMinimizeClose";
        g_chkMinimizeClose.Size = new Size(189, 17);
        g_chkMinimizeClose.TabIndex = 2;
        g_chkMinimizeClose.Text = "Minimize window when closing";
        g_chkMinimizeClose.TextAlign = ContentAlignment.TopLeft;
        g_chkMinimizeClose.UseVisualStyleBackColor = true;
        // 
        // g_chkWindowOnTop
        // 
        g_chkWindowOnTop.AutoSize = true;
        g_chkWindowOnTop.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        g_chkWindowOnTop.Location = new Point(6, 23);
        g_chkWindowOnTop.Margin = new Padding(3, 0, 3, 0);
        g_chkWindowOnTop.Name = "g_chkWindowOnTop";
        g_chkWindowOnTop.Size = new Size(156, 17);
        g_chkWindowOnTop.TabIndex = 1;
        g_chkWindowOnTop.Text = "Window is always on top";
        g_chkWindowOnTop.TextAlign = ContentAlignment.TopLeft;
        g_chkWindowOnTop.UseVisualStyleBackColor = true;
        // 
        // g_chkStartMinimized
        // 
        g_chkStartMinimized.AutoSize = true;
        g_chkStartMinimized.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        g_chkStartMinimized.Location = new Point(6, 6);
        g_chkStartMinimized.Margin = new Padding(3, 0, 3, 0);
        g_chkStartMinimized.Name = "g_chkStartMinimized";
        g_chkStartMinimized.Size = new Size(152, 17);
        g_chkStartMinimized.TabIndex = 0;
        g_chkStartMinimized.Text = "Start program minimized";
        g_chkStartMinimized.TextAlign = ContentAlignment.TopLeft;
        g_chkStartMinimized.UseVisualStyleBackColor = true;
        // 
        // tpPerformance
        // 
        tpPerformance.Controls.Add(p_chkSeparateCpu);
        tpPerformance.Controls.Add(p_chkKernelTime);
        tpPerformance.Controls.Add(p_tbGridSize);
        tpPerformance.Controls.Add(p_tbValueSpacing);
        tpPerformance.Controls.Add(p_AverageLine);
        tpPerformance.Controls.Add(p_lblGridSize);
        tpPerformance.Controls.Add(p_lblValueSpacing);
        tpPerformance.Controls.Add(p_Label5);
        tpPerformance.Controls.Add(p_Label4);
        tpPerformance.Controls.Add(p_Label3);
        tpPerformance.Controls.Add(p_GridHorizontal);
        tpPerformance.Controls.Add(p_GridVertical);
        tpPerformance.Controls.Add(p_Label2);
        tpPerformance.Controls.Add(p_AverageLineColor);
        tpPerformance.Controls.Add(p_GridHorizontalColor);
        tpPerformance.Controls.Add(p_GridVerticalColor);
        tpPerformance.Controls.Add(p_Label1);
        tpPerformance.Controls.Add(p_lblNote);
        tpPerformance.Controls.Add(p_chkLightBackground);
        tpPerformance.Controls.Add(p_chkOnlyOnHover);
        tpPerformance.Controls.Add(p_chkShowLegends);
        tpPerformance.Controls.Add(p_chkShowIndexes);
        tpPerformance.Controls.Add(p_chkAvgValue);
        tpPerformance.Controls.Add(p_chkShadeBackground);
        tpPerformance.Controls.Add(p_chkAntiAlias);
        tpPerformance.Controls.Add(p_chkSolidGraphs);
        tpPerformance.Location = new Point(4, 24);
        tpPerformance.Name = "tpPerformance";
        tpPerformance.Padding = new Padding(3);
        tpPerformance.Size = new Size(448, 187);
        tpPerformance.TabIndex = 1;
        tpPerformance.Text = "Performance Graphs";
        tpPerformance.UseVisualStyleBackColor = true;
        // 
        // p_chkSeparateCpu
        // 
        p_chkSeparateCpu.AutoSize = true;
        p_chkSeparateCpu.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        p_chkSeparateCpu.Location = new Point(230, 125);
        p_chkSeparateCpu.Margin = new Padding(3, 0, 3, 0);
        p_chkSeparateCpu.Name = "p_chkSeparateCpu";
        p_chkSeparateCpu.Size = new Size(189, 17);
        p_chkSeparateCpu.TabIndex = 25;
        p_chkSeparateCpu.Text = "Display one graph per poccesor";
        p_chkSeparateCpu.TextAlign = ContentAlignment.TopLeft;
        p_chkSeparateCpu.UseVisualStyleBackColor = true;
        // 
        // p_chkKernelTime
        // 
        p_chkKernelTime.AutoSize = true;
        p_chkKernelTime.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        p_chkKernelTime.Location = new Point(230, 108);
        p_chkKernelTime.Margin = new Padding(3, 0, 3, 0);
        p_chkKernelTime.Name = "p_chkKernelTime";
        p_chkKernelTime.Size = new Size(147, 17);
        p_chkKernelTime.TabIndex = 24;
        p_chkKernelTime.Text = "Display CPU kernel time";
        p_chkKernelTime.TextAlign = ContentAlignment.TopLeft;
        p_chkKernelTime.UseVisualStyleBackColor = true;
        // 
        // p_tbGridSize
        // 
        p_tbGridSize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        p_tbGridSize.AutoSize = false;
        p_tbGridSize.BackColor = SystemColors.Window;
        p_tbGridSize.Location = new Point(313, 88);
        p_tbGridSize.Name = "p_tbGridSize";
        p_tbGridSize.Size = new Size(102, 21);
        p_tbGridSize.TabIndex = 22;
        p_tbGridSize.TickStyle = TickStyle.None;
        p_tbGridSize.Value = 10;
        p_tbGridSize.Minimum = 2;
        p_tbGridSize.Maximum = 30;
        p_tbGridSize.Scroll += tbValues_Scroll;
        // 
        // p_tbValueSpacing
        // 
        p_tbValueSpacing.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        p_tbValueSpacing.AutoSize = false;
        p_tbValueSpacing.BackColor = SystemColors.Window;
        p_tbValueSpacing.Location = new Point(313, 68);
        p_tbValueSpacing.Name = "p_tbValueSpacing";
        p_tbValueSpacing.Size = new Size(102, 21);
        p_tbValueSpacing.TabIndex = 20;
        p_tbValueSpacing.TickStyle = TickStyle.None;
        p_tbValueSpacing.Value = 2;
        p_tbValueSpacing.Minimum = 1;
        p_tbValueSpacing.Maximum = 20;
        p_tbValueSpacing.Scroll += tbValues_Scroll;
        // 
        // p_AverageLine
        // 
        p_AverageLine.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        p_AverageLine.DropDownStyle = ComboBoxStyle.DropDownList;
        p_AverageLine.DropDownWidth = 80;
        p_AverageLine.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        p_AverageLine.Items.AddRange(new object[] { "Solid", "Dash", "Dot", "DashDot", "DashDotDot", "None" });
        p_AverageLine.Location = new Point(319, 44);
        p_AverageLine.Name = "p_AverageLine";
        p_AverageLine.Size = new Size(90, 21);
        p_AverageLine.TabIndex = 18;
        // 
        // p_lblGridSize
        // 
        p_lblGridSize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        p_lblGridSize.Location = new Point(412, 85);
        p_lblGridSize.Name = "p_lblGridSize";
        p_lblGridSize.Size = new Size(30, 20);
        p_lblGridSize.TabIndex = 23;
        p_lblGridSize.Text = "20";
        p_lblGridSize.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // p_lblValueSpacing
        // 
        p_lblValueSpacing.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        p_lblValueSpacing.Location = new Point(412, 65);
        p_lblValueSpacing.Name = "p_lblValueSpacing";
        p_lblValueSpacing.Size = new Size(30, 20);
        p_lblValueSpacing.TabIndex = 21;
        p_lblValueSpacing.Text = "20";
        p_lblValueSpacing.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // p_Label5
        // 
        p_Label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        p_Label5.Location = new Point(230, 88);
        p_Label5.Name = "p_Label5";
        p_Label5.Size = new Size(90, 17);
        p_Label5.TabIndex = 13;
        p_Label5.Text = "Grid size:";
        // 
        // p_Label4
        // 
        p_Label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        p_Label4.Location = new Point(230, 68);
        p_Label4.Name = "p_Label4";
        p_Label4.Size = new Size(90, 17);
        p_Label4.TabIndex = 12;
        p_Label4.Text = "Value spacing:";
        // 
        // p_Label3
        // 
        p_Label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        p_Label3.Location = new Point(230, 48);
        p_Label3.Name = "p_Label3";
        p_Label3.Size = new Size(90, 17);
        p_Label3.TabIndex = 11;
        p_Label3.Text = "Average line:";
        // 
        // p_GridHorizontal
        // 
        p_GridHorizontal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        p_GridHorizontal.DropDownStyle = ComboBoxStyle.DropDownList;
        p_GridHorizontal.DropDownWidth = 80;
        p_GridHorizontal.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        p_GridHorizontal.Items.AddRange(new object[] { "Solid", "Dash", "Dot", "DashDot", "DashDotDot", "None" });
        p_GridHorizontal.Location = new Point(319, 25);
        p_GridHorizontal.Name = "p_GridHorizontal";
        p_GridHorizontal.Size = new Size(90, 21);
        p_GridHorizontal.TabIndex = 16;
        // 
        // p_GridVertical
        // 
        p_GridVertical.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        p_GridVertical.DropDownStyle = ComboBoxStyle.DropDownList;
        p_GridVertical.DropDownWidth = 80;
        p_GridVertical.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        p_GridVertical.Items.AddRange(new object[] { "Solid", "Dash", "Dot", "DashDot", "DashDotDot", "None" });
        p_GridVertical.Location = new Point(319, 5);
        p_GridVertical.Name = "p_GridVertical";
        p_GridVertical.Size = new Size(90, 21);
        p_GridVertical.TabIndex = 14;
        // 
        // p_Label2
        // 
        p_Label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        p_Label2.Location = new Point(230, 28);
        p_Label2.Name = "p_Label2";
        p_Label2.Size = new Size(90, 17);
        p_Label2.TabIndex = 10;
        p_Label2.Text = "Horizontal grid:";
        // 
        // p_AverageLineColor
        // 
        p_AverageLineColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        p_AverageLineColor.BackColor = Color.Coral;
        p_AverageLineColor.Location = new Point(412, 45);
        p_AverageLineColor.Name = "p_AverageLineColor";
        p_AverageLineColor.Size = new Size(30, 20);
        p_AverageLineColor.TabIndex = 19;
        p_AverageLineColor.UseVisualStyleBackColor = false;
        p_AverageLineColor.Click += btnColors_Click;
        // 
        // p_GridHorizontalColor
        // 
        p_GridHorizontalColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        p_GridHorizontalColor.BackColor = Color.Green;
        p_GridHorizontalColor.Location = new Point(412, 25);
        p_GridHorizontalColor.Name = "p_GridHorizontalColor";
        p_GridHorizontalColor.Size = new Size(30, 20);
        p_GridHorizontalColor.TabIndex = 17;
        p_GridHorizontalColor.UseVisualStyleBackColor = false;
        p_GridHorizontalColor.Click += btnColors_Click;
        // 
        // p_GridVerticalColor
        // 
        p_GridVerticalColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        p_GridVerticalColor.BackColor = Color.Green;
        p_GridVerticalColor.Location = new Point(412, 5);
        p_GridVerticalColor.Name = "p_GridVerticalColor";
        p_GridVerticalColor.Size = new Size(30, 20);
        p_GridVerticalColor.TabIndex = 15;
        p_GridVerticalColor.UseVisualStyleBackColor = false;
        p_GridVerticalColor.Click += btnColors_Click;
        // 
        // p_Label1
        // 
        p_Label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        p_Label1.Location = new Point(230, 8);
        p_Label1.Name = "p_Label1";
        p_Label1.Size = new Size(90, 17);
        p_Label1.TabIndex = 9;
        p_Label1.Text = "Vertical grid:";
        // 
        // p_lblNote
        // 
        p_lblNote.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        p_lblNote.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        p_lblNote.ForeColor = SystemColors.HotTrack;
        p_lblNote.Location = new Point(2, 158);
        p_lblNote.Name = "p_lblNote";
        p_lblNote.Size = new Size(400, 28);
        p_lblNote.TabIndex = 8;
        p_lblNote.Text = "Note: This will replace any individual graph setting when changed.\r\nYou can use also use right click on a graph to customize individually.";
        // 
        // p_chkLightBackground
        // 
        p_chkLightBackground.AutoSize = true;
        p_chkLightBackground.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        p_chkLightBackground.Location = new Point(6, 125);
        p_chkLightBackground.Margin = new Padding(3, 0, 3, 0);
        p_chkLightBackground.Name = "p_chkLightBackground";
        p_chkLightBackground.Size = new Size(152, 17);
        p_chkLightBackground.TabIndex = 7;
        p_chkLightBackground.Text = "Light background colors";
        p_chkLightBackground.TextAlign = ContentAlignment.TopLeft;
        p_chkLightBackground.UseVisualStyleBackColor = true;
        // 
        // p_chkOnlyOnHover
        // 
        p_chkOnlyOnHover.AutoSize = true;
        p_chkOnlyOnHover.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        p_chkOnlyOnHover.Location = new Point(6, 108);
        p_chkOnlyOnHover.Margin = new Padding(3, 0, 3, 0);
        p_chkOnlyOnHover.Name = "p_chkOnlyOnHover";
        p_chkOnlyOnHover.Size = new Size(174, 17);
        p_chkOnlyOnHover.TabIndex = 6;
        p_chkOnlyOnHover.Text = "Display details only on hover";
        p_chkOnlyOnHover.TextAlign = ContentAlignment.TopLeft;
        p_chkOnlyOnHover.UseVisualStyleBackColor = true;
        // 
        // p_chkShowLegends
        // 
        p_chkShowLegends.AutoSize = true;
        p_chkShowLegends.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        p_chkShowLegends.Location = new Point(6, 91);
        p_chkShowLegends.Margin = new Padding(3, 0, 3, 0);
        p_chkShowLegends.Name = "p_chkShowLegends";
        p_chkShowLegends.Size = new Size(158, 17);
        p_chkShowLegends.TabIndex = 5;
        p_chkShowLegends.Text = "Display legends on graph";
        p_chkShowLegends.TextAlign = ContentAlignment.TopLeft;
        p_chkShowLegends.UseVisualStyleBackColor = true;
        // 
        // p_chkShowIndexes
        // 
        p_chkShowIndexes.AutoSize = true;
        p_chkShowIndexes.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        p_chkShowIndexes.Location = new Point(6, 74);
        p_chkShowIndexes.Margin = new Padding(3, 0, 3, 0);
        p_chkShowIndexes.Name = "p_chkShowIndexes";
        p_chkShowIndexes.Size = new Size(140, 17);
        p_chkShowIndexes.TabIndex = 4;
        p_chkShowIndexes.Text = "Display indexes values";
        p_chkShowIndexes.TextAlign = ContentAlignment.TopLeft;
        p_chkShowIndexes.UseVisualStyleBackColor = true;
        // 
        // p_chkAvgValue
        // 
        p_chkAvgValue.AutoSize = true;
        p_chkAvgValue.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        p_chkAvgValue.Location = new Point(6, 57);
        p_chkAvgValue.Margin = new Padding(3, 0, 3, 0);
        p_chkAvgValue.Name = "p_chkAvgValue";
        p_chkAvgValue.Size = new Size(141, 17);
        p_chkAvgValue.TabIndex = 3;
        p_chkAvgValue.Text = "Display average values";
        p_chkAvgValue.TextAlign = ContentAlignment.TopLeft;
        p_chkAvgValue.UseVisualStyleBackColor = true;
        // 
        // p_chkShadeBackground
        // 
        p_chkShadeBackground.AutoSize = true;
        p_chkShadeBackground.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        p_chkShadeBackground.Location = new Point(6, 40);
        p_chkShadeBackground.Margin = new Padding(3, 0, 3, 0);
        p_chkShadeBackground.Name = "p_chkShadeBackground";
        p_chkShadeBackground.Size = new Size(153, 17);
        p_chkShadeBackground.TabIndex = 2;
        p_chkShadeBackground.Text = "Shade background color";
        p_chkShadeBackground.TextAlign = ContentAlignment.TopLeft;
        p_chkShadeBackground.UseVisualStyleBackColor = true;
        // 
        // p_chkAntiAlias
        // 
        p_chkAntiAlias.AutoSize = true;
        p_chkAntiAlias.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        p_chkAntiAlias.Location = new Point(6, 23);
        p_chkAntiAlias.Margin = new Padding(3, 0, 3, 0);
        p_chkAntiAlias.Name = "p_chkAntiAlias";
        p_chkAntiAlias.Size = new Size(124, 17);
        p_chkAntiAlias.TabIndex = 1;
        p_chkAntiAlias.Text = "Enable antialiasing";
        p_chkAntiAlias.TextAlign = ContentAlignment.TopLeft;
        p_chkAntiAlias.UseVisualStyleBackColor = true;
        // 
        // p_chkSolidGraphs
        // 
        p_chkSolidGraphs.AutoSize = true;
        p_chkSolidGraphs.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        p_chkSolidGraphs.Location = new Point(6, 6);
        p_chkSolidGraphs.Margin = new Padding(3, 0, 3, 0);
        p_chkSolidGraphs.Name = "p_chkSolidGraphs";
        p_chkSolidGraphs.Size = new Size(112, 17);
        p_chkSolidGraphs.TabIndex = 0;
        p_chkSolidGraphs.Text = "Solid graph style";
        p_chkSolidGraphs.TextAlign = ContentAlignment.TopLeft;
        p_chkSolidGraphs.UseVisualStyleBackColor = true;
        // 
        // tpNetworking
        // 
        tpNetworking.Controls.Add(n_Label7);
        tpNetworking.Controls.Add(n_Label6);
        tpNetworking.Controls.Add(n_btnDnColor);
        tpNetworking.Controls.Add(n_btnUpColor);
        tpNetworking.Controls.Add(n_chkKeepDrawing);
        tpNetworking.Controls.Add(n_chkLightBackground);
        tpNetworking.Controls.Add(n_chkShowLegends);
        tpNetworking.Controls.Add(n_chkShowIndexes);
        tpNetworking.Controls.Add(n_tbGridSize);
        tpNetworking.Controls.Add(n_tbValueSpacing);
        tpNetworking.Controls.Add(n_AverageLine);
        tpNetworking.Controls.Add(n_lblGridSize);
        tpNetworking.Controls.Add(n_lblValueSpacing);
        tpNetworking.Controls.Add(n_Label5);
        tpNetworking.Controls.Add(n_Label4);
        tpNetworking.Controls.Add(n_Label3);
        tpNetworking.Controls.Add(n_GridHorizontal);
        tpNetworking.Controls.Add(n_GridVertical);
        tpNetworking.Controls.Add(n_Label2);
        tpNetworking.Controls.Add(n_AverageLineColor);
        tpNetworking.Controls.Add(n_GridHorizontalColor);
        tpNetworking.Controls.Add(n_GridVerticalColor);
        tpNetworking.Controls.Add(n_Label1);
        tpNetworking.Controls.Add(n_chkAvgValue);
        tpNetworking.Controls.Add(n_chkShadeBackground);
        tpNetworking.Controls.Add(n_chkAntiAlias);
        tpNetworking.Controls.Add(n_chkSolidGraphs);
        tpNetworking.Location = new Point(4, 24);
        tpNetworking.Name = "tpNetworking";
        tpNetworking.Size = new Size(448, 187);
        tpNetworking.TabIndex = 2;
        tpNetworking.Text = "Networking Graphs";
        tpNetworking.UseVisualStyleBackColor = true;
        // 
        // n_Label7
        // 
        n_Label7.Location = new Point(256, 127);
        n_Label7.Name = "n_Label7";
        n_Label7.Size = new Size(162, 17);
        n_Label7.TabIndex = 26;
        n_Label7.Text = "Download bandwith color";
        n_Label7.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // n_Label6
        // 
        n_Label6.Location = new Point(256, 109);
        n_Label6.Name = "n_Label6";
        n_Label6.Size = new Size(162, 17);
        n_Label6.TabIndex = 24;
        n_Label6.Text = "Upload bandwith color";
        n_Label6.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // n_btnDnColor
        // 
        n_btnDnColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        n_btnDnColor.BackColor = Color.Cyan;
        n_btnDnColor.Location = new Point(230, 126);
        n_btnDnColor.Name = "n_btnDnColor";
        n_btnDnColor.Size = new Size(25, 18);
        n_btnDnColor.TabIndex = 25;
        n_btnDnColor.UseVisualStyleBackColor = false;
        n_btnDnColor.Click += btnColors_Click;
        // 
        // n_btnUpColor
        // 
        n_btnUpColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        n_btnUpColor.BackColor = Color.Fuchsia;
        n_btnUpColor.Location = new Point(230, 109);
        n_btnUpColor.Name = "n_btnUpColor";
        n_btnUpColor.Size = new Size(25, 18);
        n_btnUpColor.TabIndex = 23;
        n_btnUpColor.UseVisualStyleBackColor = false;
        n_btnUpColor.Click += btnColors_Click;
        // 
        // n_chkKeepDrawing
        // 
        n_chkKeepDrawing.AutoSize = true;
        n_chkKeepDrawing.CheckAlign = ContentAlignment.TopLeft;
        n_chkKeepDrawing.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        n_chkKeepDrawing.Location = new Point(6, 128);
        n_chkKeepDrawing.Margin = new Padding(3, 0, 3, 0);
        n_chkKeepDrawing.Name = "n_chkKeepDrawing";
        n_chkKeepDrawing.Size = new Size(199, 30);
        n_chkKeepDrawing.TabIndex = 7;
        n_chkKeepDrawing.Text = "Keep updating when not in focus\r\n(may incrase CPU usage)";
        n_chkKeepDrawing.TextAlign = ContentAlignment.TopLeft;
        n_chkKeepDrawing.UseVisualStyleBackColor = true;
        // 
        // n_chkLightBackground
        // 
        n_chkLightBackground.AutoSize = true;
        n_chkLightBackground.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        n_chkLightBackground.Location = new Point(6, 108);
        n_chkLightBackground.Margin = new Padding(3, 0, 3, 0);
        n_chkLightBackground.Name = "n_chkLightBackground";
        n_chkLightBackground.Size = new Size(152, 17);
        n_chkLightBackground.TabIndex = 6;
        n_chkLightBackground.Text = "Light background colors";
        n_chkLightBackground.TextAlign = ContentAlignment.TopLeft;
        n_chkLightBackground.UseVisualStyleBackColor = true;
        // 
        // n_chkShowLegends
        // 
        n_chkShowLegends.AutoSize = true;
        n_chkShowLegends.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        n_chkShowLegends.Location = new Point(6, 91);
        n_chkShowLegends.Margin = new Padding(3, 0, 3, 0);
        n_chkShowLegends.Name = "n_chkShowLegends";
        n_chkShowLegends.Size = new Size(158, 17);
        n_chkShowLegends.TabIndex = 5;
        n_chkShowLegends.Text = "Display legends on graph";
        n_chkShowLegends.TextAlign = ContentAlignment.TopLeft;
        n_chkShowLegends.UseVisualStyleBackColor = true;
        // 
        // n_chkShowIndexes
        // 
        n_chkShowIndexes.AutoSize = true;
        n_chkShowIndexes.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        n_chkShowIndexes.Location = new Point(6, 74);
        n_chkShowIndexes.Margin = new Padding(3, 0, 3, 0);
        n_chkShowIndexes.Name = "n_chkShowIndexes";
        n_chkShowIndexes.Size = new Size(140, 17);
        n_chkShowIndexes.TabIndex = 4;
        n_chkShowIndexes.Text = "Display indexes values";
        n_chkShowIndexes.TextAlign = ContentAlignment.TopLeft;
        n_chkShowIndexes.UseVisualStyleBackColor = true;
        // 
        // n_tbGridSize
        // 
        n_tbGridSize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        n_tbGridSize.AutoSize = false;
        n_tbGridSize.BackColor = SystemColors.Window;
        n_tbGridSize.Location = new Point(313, 88);
        n_tbGridSize.Name = "n_tbGridSize";
        n_tbGridSize.Size = new Size(102, 21);
        n_tbGridSize.TabIndex = 21;
        n_tbGridSize.TickStyle = TickStyle.None;
        n_tbGridSize.Value = 10;
        n_tbGridSize.Minimum = 2;
        n_tbGridSize.Maximum = 30;
        n_tbGridSize.Scroll += tbValues_Scroll;
        // 
        // n_tbValueSpacing
        // 
        n_tbValueSpacing.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        n_tbValueSpacing.AutoSize = false;
        n_tbValueSpacing.BackColor = SystemColors.Window;
        n_tbValueSpacing.Location = new Point(313, 68);
        n_tbValueSpacing.Name = "n_tbValueSpacing";
        n_tbValueSpacing.Size = new Size(102, 21);
        n_tbValueSpacing.TabIndex = 19;
        n_tbValueSpacing.TickStyle = TickStyle.None;
        n_tbValueSpacing.Value = 2;
        n_tbValueSpacing.Minimum = 1;
        n_tbValueSpacing.Maximum = 20;
        n_tbValueSpacing.Scroll += tbValues_Scroll;
        // 
        // n_AverageLine
        // 
        n_AverageLine.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        n_AverageLine.DropDownStyle = ComboBoxStyle.DropDownList;
        n_AverageLine.DropDownWidth = 80;
        n_AverageLine.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        n_AverageLine.Items.AddRange(new object[] { "Solid", "Dash", "Dot", "DashDot", "DashDotDot", "None" });
        n_AverageLine.Location = new Point(319, 44);
        n_AverageLine.Name = "n_AverageLine";
        n_AverageLine.Size = new Size(90, 21);
        n_AverageLine.TabIndex = 17;
        // 
        // n_lblGridSize
        // 
        n_lblGridSize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        n_lblGridSize.Location = new Point(412, 85);
        n_lblGridSize.Name = "n_lblGridSize";
        n_lblGridSize.Size = new Size(30, 20);
        n_lblGridSize.TabIndex = 22;
        n_lblGridSize.Text = "20";
        n_lblGridSize.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // n_lblValueSpacing
        // 
        n_lblValueSpacing.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        n_lblValueSpacing.Location = new Point(412, 65);
        n_lblValueSpacing.Name = "n_lblValueSpacing";
        n_lblValueSpacing.Size = new Size(30, 20);
        n_lblValueSpacing.TabIndex = 20;
        n_lblValueSpacing.Text = "20";
        n_lblValueSpacing.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // n_Label5
        // 
        n_Label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        n_Label5.Location = new Point(230, 88);
        n_Label5.Name = "n_Label5";
        n_Label5.Size = new Size(90, 17);
        n_Label5.TabIndex = 12;
        n_Label5.Text = "Grid size:";
        // 
        // n_Label4
        // 
        n_Label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        n_Label4.Location = new Point(230, 68);
        n_Label4.Name = "n_Label4";
        n_Label4.Size = new Size(90, 17);
        n_Label4.TabIndex = 11;
        n_Label4.Text = "Value spacing:";
        // 
        // n_Label3
        // 
        n_Label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        n_Label3.Location = new Point(230, 48);
        n_Label3.Name = "n_Label3";
        n_Label3.Size = new Size(90, 17);
        n_Label3.TabIndex = 10;
        n_Label3.Text = "Average line:";
        // 
        // n_GridHorizontal
        // 
        n_GridHorizontal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        n_GridHorizontal.DropDownStyle = ComboBoxStyle.DropDownList;
        n_GridHorizontal.DropDownWidth = 80;
        n_GridHorizontal.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        n_GridHorizontal.Items.AddRange(new object[] { "Solid", "Dash", "Dot", "DashDot", "DashDotDot", "None" });
        n_GridHorizontal.Location = new Point(319, 25);
        n_GridHorizontal.Name = "n_GridHorizontal";
        n_GridHorizontal.Size = new Size(90, 21);
        n_GridHorizontal.TabIndex = 15;
        // 
        // n_GridVertical
        // 
        n_GridVertical.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        n_GridVertical.DropDownStyle = ComboBoxStyle.DropDownList;
        n_GridVertical.DropDownWidth = 80;
        n_GridVertical.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        n_GridVertical.Items.AddRange(new object[] { "Solid", "Dash", "Dot", "DashDot", "DashDotDot", "None" });
        n_GridVertical.Location = new Point(319, 5);
        n_GridVertical.Name = "n_GridVertical";
        n_GridVertical.Size = new Size(90, 21);
        n_GridVertical.TabIndex = 13;
        // 
        // n_Label2
        // 
        n_Label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        n_Label2.Location = new Point(230, 28);
        n_Label2.Name = "n_Label2";
        n_Label2.Size = new Size(90, 17);
        n_Label2.TabIndex = 9;
        n_Label2.Text = "Horizontal grid:";
        // 
        // n_AverageLineColor
        // 
        n_AverageLineColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        n_AverageLineColor.BackColor = Color.Coral;
        n_AverageLineColor.Location = new Point(412, 45);
        n_AverageLineColor.Name = "n_AverageLineColor";
        n_AverageLineColor.Size = new Size(30, 20);
        n_AverageLineColor.TabIndex = 18;
        n_AverageLineColor.UseVisualStyleBackColor = false;
        n_AverageLineColor.Click += btnColors_Click;
        // 
        // n_GridHorizontalColor
        // 
        n_GridHorizontalColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        n_GridHorizontalColor.BackColor = Color.Green;
        n_GridHorizontalColor.Location = new Point(412, 25);
        n_GridHorizontalColor.Name = "n_GridHorizontalColor";
        n_GridHorizontalColor.Size = new Size(30, 20);
        n_GridHorizontalColor.TabIndex = 16;
        n_GridHorizontalColor.UseVisualStyleBackColor = false;
        n_GridHorizontalColor.Click += btnColors_Click;
        // 
        // n_GridVerticalColor
        // 
        n_GridVerticalColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        n_GridVerticalColor.BackColor = Color.Green;
        n_GridVerticalColor.Location = new Point(412, 5);
        n_GridVerticalColor.Name = "n_GridVerticalColor";
        n_GridVerticalColor.Size = new Size(30, 20);
        n_GridVerticalColor.TabIndex = 14;
        n_GridVerticalColor.UseVisualStyleBackColor = false;
        n_GridVerticalColor.Click += btnColors_Click;
        // 
        // n_Label1
        // 
        n_Label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        n_Label1.Location = new Point(230, 8);
        n_Label1.Name = "n_Label1";
        n_Label1.Size = new Size(90, 17);
        n_Label1.TabIndex = 8;
        n_Label1.Text = "Vertical grid:";
        // 
        // n_chkAvgValue
        // 
        n_chkAvgValue.AutoSize = true;
        n_chkAvgValue.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        n_chkAvgValue.Location = new Point(6, 57);
        n_chkAvgValue.Margin = new Padding(3, 0, 3, 0);
        n_chkAvgValue.Name = "n_chkAvgValue";
        n_chkAvgValue.Size = new Size(141, 17);
        n_chkAvgValue.TabIndex = 3;
        n_chkAvgValue.Text = "Display average values";
        n_chkAvgValue.TextAlign = ContentAlignment.TopLeft;
        n_chkAvgValue.UseVisualStyleBackColor = true;
        // 
        // n_chkShadeBackground
        // 
        n_chkShadeBackground.AutoSize = true;
        n_chkShadeBackground.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        n_chkShadeBackground.Location = new Point(6, 40);
        n_chkShadeBackground.Margin = new Padding(3, 0, 3, 0);
        n_chkShadeBackground.Name = "n_chkShadeBackground";
        n_chkShadeBackground.Size = new Size(153, 17);
        n_chkShadeBackground.TabIndex = 2;
        n_chkShadeBackground.Text = "Shade background color";
        n_chkShadeBackground.TextAlign = ContentAlignment.TopLeft;
        n_chkShadeBackground.UseVisualStyleBackColor = true;
        // 
        // n_chkAntiAlias
        // 
        n_chkAntiAlias.AutoSize = true;
        n_chkAntiAlias.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        n_chkAntiAlias.Location = new Point(6, 23);
        n_chkAntiAlias.Margin = new Padding(3, 0, 3, 0);
        n_chkAntiAlias.Name = "n_chkAntiAlias";
        n_chkAntiAlias.Size = new Size(124, 17);
        n_chkAntiAlias.TabIndex = 1;
        n_chkAntiAlias.Text = "Enable antialiasing";
        n_chkAntiAlias.TextAlign = ContentAlignment.TopLeft;
        n_chkAntiAlias.UseVisualStyleBackColor = true;
        // 
        // n_chkSolidGraphs
        // 
        n_chkSolidGraphs.AutoSize = true;
        n_chkSolidGraphs.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        n_chkSolidGraphs.Location = new Point(6, 6);
        n_chkSolidGraphs.Margin = new Padding(3, 0, 3, 0);
        n_chkSolidGraphs.Name = "n_chkSolidGraphs";
        n_chkSolidGraphs.Size = new Size(112, 17);
        n_chkSolidGraphs.TabIndex = 0;
        n_chkSolidGraphs.Text = "Solid graph style";
        n_chkSolidGraphs.TextAlign = ContentAlignment.TopLeft;
        n_chkSolidGraphs.UseVisualStyleBackColor = true;
        // 
        // btnCancel
        // 
        btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancel.Location = new Point(175, 140);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(75, 30);
        btnCancel.TabIndex = 3;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += btnCancel_Click;
        // 
        // btnApply
        // 
        btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnApply.Location = new Point(90, 140);
        btnApply.Name = "btnApply";
        btnApply.Size = new Size(75, 30);
        btnApply.TabIndex = 2;
        btnApply.Text = "Apply";
        btnApply.UseVisualStyleBackColor = true;
        btnApply.Click += btnApply_Click;
        // 
        // btnOk
        // 
        btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnOk.Location = new Point(10, 140);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(75, 30);
        btnOk.TabIndex = 1;
        btnOk.Text = "OK";
        btnOk.UseVisualStyleBackColor = true;
        btnOk.Click += btnOk_Click;
        // 
        // cd
        // 
        cd.AnyColor = true;
        cd.FullOpen = true;
        // 
        // frmPreferences
        // 
        AcceptButton = btnOk;
        CancelButton = btnCancel;
        ClientSize = new Size(270, 180);
        Controls.Add(btnOk);
        Controls.Add(btnApply);
        Controls.Add(btnCancel);
        Controls.Add(tc);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        HelpButton = true;
        Icon = Resources.Resources.frmPreferences;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "frmPreferences";
        ShowInTaskbar = false;
        SizeGripStyle = SizeGripStyle.Hide;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Preferences";
        TopMost = true;
        FormClosing += onFormClosing;
        Load += onLoad;
        tc.ResumeLayout(false);
        tpGeneral.ResumeLayout(false);
        tpGeneral.PerformLayout();
        g_gbTrayIcon.ResumeLayout(false);
        g_gbTrayIcon.PerformLayout();
        tpPerformance.ResumeLayout(false);
        tpPerformance.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)p_tbGridSize).EndInit();
        ((System.ComponentModel.ISupportInitialize)p_tbValueSpacing).EndInit();
        tpNetworking.ResumeLayout(false);
        tpNetworking.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)n_tbGridSize).EndInit();
        ((System.ComponentModel.ISupportInitialize)n_tbValueSpacing).EndInit();
        ResumeLayout(false);
    }

    private TabPage tpGeneral;
    private TabPage tpPerformance;
    private TabPage tpNetworking;
    private Button btnCancel;
    private Button btnApply;
    private Button btnOk;
    internal TabControl tc;
    private CheckBox g_chkStartMinimized;
    private CheckBox g_chkWindowOnTop;
    private CheckBox g_chkShowIcons;
    private CheckBox g_chkAlternateRowColors;
    private CheckBox g_chkServicesStatus;
    private CheckBox g_chkTimmingStatus;
    private CheckBox g_chkRememberWindow;
    private CheckBox g_chkMinimizeClose;
    private Button g_btnHighlightNew;
    private Button g_btnHighlightFrozen;
    private Button g_btnHighlightRemoved;
    private Button g_btnHighlightChanges;
    private CheckBox g_chkHighlightFrozen;
    private CheckBox g_chkHighlightRemoved;
    private CheckBox g_chkHighlightChanges;
    private CheckBox g_chkHighlightNew;
    private GroupBox g_gbTrayIcon;
    private CheckBox t_chkShowCPU;
    private CheckBox t_chkRequireDobleClick;
    private CheckBox t_chkCloseToTray;
    private CheckBox t_chkHideToTray;
    private CheckBox g_chkStoreXML;
    private CheckBox p_chkOnlyOnHover;
    private CheckBox p_chkShowLegends;
    private CheckBox p_chkShowIndexes;
    private CheckBox p_chkAvgValue;
    private CheckBox p_chkShadeBackground;
    private CheckBox p_chkAntiAlias;
    private CheckBox p_chkSolidGraphs;
    private CheckBox p_chkLightBackground;
    private Label p_lblNote;
    private Label p_Label1;
    private Button p_AverageLineColor;
    private Button p_GridHorizontalColor;
    private Button p_GridVerticalColor;
    private ComboBox p_GridVertical;
    private ComboBox p_GridHorizontal;
    private ComboBox p_AverageLine;
    private Label p_Label3;
    private Label p_Label2;
    private Label p_Label5;
    private Label p_Label4;
    private Label p_lblGridSize;
    private Label p_lblValueSpacing;
    private TrackBar p_tbValueSpacing;
    private TrackBar p_tbGridSize;
    private ColorDialog cd;
    private CheckBox p_chkSeparateCpu;
    private CheckBox p_chkKernelTime;
    private CheckBox n_chkLightBackground;
    private CheckBox n_chkShowLegends;
    private CheckBox n_chkShowIndexes;
    private TrackBar n_tbGridSize;
    private TrackBar n_tbValueSpacing;
    private ComboBox n_AverageLine;
    private Label n_lblGridSize;
    private Label n_lblValueSpacing;
    private Label n_Label5;
    private Label n_Label4;
    private Label n_Label3;
    private ComboBox n_GridHorizontal;
    private ComboBox n_GridVertical;
    private Label n_Label2;
    private Button n_AverageLineColor;
    private Button n_GridHorizontalColor;
    private Button n_GridVerticalColor;
    private Label n_Label1;
    private CheckBox n_chkAvgValue;
    private CheckBox n_chkShadeBackground;
    private CheckBox n_chkAntiAlias;
    private CheckBox n_chkSolidGraphs;
    private Button n_btnDnColor;
    private Button n_btnUpColor;
    private Label n_Label7;
    private Label n_Label6;
    private CheckBox n_chkKeepDrawing;
}
