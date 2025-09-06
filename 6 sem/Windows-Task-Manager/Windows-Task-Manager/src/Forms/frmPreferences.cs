using System.ComponentModel;
using System.Runtime.Versioning;
using sMkTaskManager.Classes;
namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
public partial class frmPreferences : Form {

    public frmPreferences() {
        InitializeComponent();
    }

    private void onLoad(object sender, EventArgs e) {
        if (Owner == null) { CenterToScreen(); }
        LoadSettings();
        Settings.LoadCustomColors();
        cd.CustomColors = Settings.CustomColors.ToArray();
        g_chkStoreXML.Checked = Settings.StoreInFile;
    }
    private void onFormClosing(object sender, FormClosingEventArgs e) {
        Settings.CustomColors = cd.CustomColors.ToList();
        Settings.SaveCustomColors();
    }
    private void btnOk_Click(object sender, EventArgs e) {
        SaveSettings();
        if (Owner != null && Owner.GetType() == typeof(frmMain)) ((frmMain)Owner).Settings_Apply();
        DialogResult = DialogResult.OK;
    }
    private void btnApply_Click(object sender, EventArgs e) {
        SaveSettings();
        if (Owner != null && Owner.GetType() == typeof(frmMain)) ((frmMain)Owner).Settings_Apply();
    }
    private void btnCancel_Click(object sender, EventArgs e) {
        DialogResult = DialogResult.Cancel;
        Close();
    }
    private void btnColors_Click(object sender, EventArgs e) {
        cd.Color = ((Button)sender).BackColor;
        if (cd.ShowDialog(this) == DialogResult.OK) ((Button)sender).BackColor = cd.Color;
    }
    private void tbValues_Scroll(object sender, EventArgs e) {
        if (sender == null) return;
        if (sender == p_tbGridSize) p_lblGridSize.Text = p_tbGridSize.Value.ToString();
        if (sender == n_tbGridSize) n_lblGridSize.Text = n_tbGridSize.Value.ToString();
        if (sender == p_tbValueSpacing) p_lblValueSpacing.Text = p_tbValueSpacing.Value.ToString();
        if (sender == n_tbValueSpacing) n_lblValueSpacing.Text = n_tbValueSpacing.Value.ToString();
    }

    public void LoadSettings() {
        // General Settings
        try { g_chkStartMinimized.Checked = Settings.StartMinimized; } catch { }
        try { g_chkWindowOnTop.Checked = Settings.AlwaysOnTop; } catch { }
        try { g_chkMinimizeClose.Checked = Settings.MinimizeWhenClosing; } catch { }
        try { g_chkRememberWindow.Checked = Settings.RememberPositions; } catch { }
        try { g_chkShowIcons.Checked = Settings.IconsInProcess; } catch { }
        try { g_chkServicesStatus.Checked = Settings.ServicesInStatus; } catch { }
        try { g_chkTimmingStatus.Checked = Settings.TimmingInStatus; } catch { }
        try { g_chkAlternateRowColors.Checked = Settings.AlternateRowColors; } catch { }
        try { t_chkHideToTray.Checked = Settings.ToTrayWhenMinimized; } catch { }
        try { t_chkCloseToTray.Checked = Settings.ToTrayWhenClosed; } catch { }
        try { t_chkRequireDobleClick.Checked = Settings.DblClickToRestore; } catch { }
        try { t_chkShowCPU.Checked = Settings.ShowCPUOnTray; } catch { }
        // Highlight Items Checks & Colors
        try { g_chkHighlightNew.Checked = Settings.Highlights.NewItems; } catch { }
        try { g_chkHighlightChanges.Checked = Settings.Highlights.ChangingItems; } catch { }
        try { g_chkHighlightRemoved.Checked = Settings.Highlights.RemovedItems; } catch { }
        try { g_chkHighlightFrozen.Checked = Settings.Highlights.FrozenItems; } catch { }
        try { g_btnHighlightNew.BackColor = Settings.Highlights.NewColor; } catch { }
        try { g_btnHighlightChanges.BackColor = Settings.Highlights.ChangingColor; } catch { }
        try { g_btnHighlightRemoved.BackColor = Settings.Highlights.RemovedColor; } catch { }
        try { g_btnHighlightFrozen.BackColor = Settings.Highlights.FrozenColor; } catch { }
        // Performance Graphs Settings
        try { p_chkSolidGraphs.Checked = Settings.Performance.Solid; } catch { }
        try { p_chkAntiAlias.Checked = Settings.Performance.AntiAlias; } catch { }
        try { p_chkShadeBackground.Checked = Settings.Performance.ShadeBackground; } catch { }
        try { p_chkAvgValue.Checked = Settings.Performance.DisplayAverages; } catch { }
        try { p_chkShowLegends.Checked = Settings.Performance.DisplayLegends; } catch { }
        try { p_chkShowIndexes.Checked = Settings.Performance.DisplayIndexes; } catch { }
        try { p_chkOnlyOnHover.Checked = Settings.Performance.DisplayOnHover; } catch { }
        try { p_tbValueSpacing.Value = Settings.Performance.ValueSpacing; } catch { }
        try { p_tbGridSize.Value = Settings.Performance.GridSize; } catch { }
        try { p_chkLightBackground.Checked = Settings.Performance.LightColors; } catch { }
        try { p_chkKernelTime.Checked = Settings.Performance.ShowKernelTime; } catch { }
        try { p_chkSeparateCpu.Checked = Settings.Performance.SeparateCPUs; } catch { }
        try { p_GridVertical.SelectedIndex = Settings.Performance.VerticalGridStyle; } catch { }
        try { p_GridVerticalColor.BackColor = Settings.Performance.VerticalGridColor; } catch { }
        try { p_GridHorizontal.SelectedIndex = Settings.Performance.HorizontalGridStyle; } catch { }
        try { p_GridHorizontalColor.BackColor = Settings.Performance.HorizontalGridColor; } catch { }
        try { p_AverageLine.SelectedIndex = Settings.Performance.AverageLineStyle; } catch { }
        try { p_AverageLineColor.BackColor = Settings.Performance.AverageLineColor; } catch { }
        try { p_lblGridSize.Text = p_tbGridSize.Value.ToString(); } catch { }
        try { p_lblValueSpacing.Text = p_tbValueSpacing.Value.ToString(); } catch { }
        // Networking Graphs Settings
        try { n_chkKeepDrawing.Checked = Settings.Networking.KeepUpdating; } catch { }
        try { n_chkSolidGraphs.Checked = Settings.Networking.Solid; } catch { }
        try { n_chkAntiAlias.Checked = Settings.Networking.AntiAlias; } catch { }
        try { n_chkShadeBackground.Checked = Settings.Networking.ShadeBackground; } catch { }
        try { n_chkAvgValue.Checked = Settings.Networking.DisplayAverages; } catch { }
        try { n_chkShowIndexes.Checked = Settings.Networking.DisplayIndexes; } catch { }
        try { n_chkShowLegends.Checked = Settings.Networking.DisplayLegends; } catch { }
        try { n_tbValueSpacing.Value = Settings.Networking.ValueSpacing; } catch { }
        try { n_tbGridSize.Value = Settings.Networking.GridSize; } catch { }
        try { n_chkLightBackground.Checked = Settings.Networking.LightColors; } catch { }
        try { n_GridVertical.SelectedIndex = Settings.Networking.VerticalGridStyle; } catch { }
        try { n_GridVerticalColor.BackColor = Settings.Networking.VerticalGridColor; } catch { }
        try { n_GridHorizontal.SelectedIndex = Settings.Networking.HorizontalGridStyle; } catch { }
        try { n_GridHorizontalColor.BackColor = Settings.Networking.HorizontalGridColor; } catch { }
        try { n_AverageLine.SelectedIndex = Settings.Networking.AverageLineStyle; } catch { }
        try { n_AverageLineColor.BackColor = Settings.Networking.AverageLineColor; } catch { }
        try { n_btnUpColor.BackColor = Settings.Networking.UploadColor; } catch { }
        try { n_btnDnColor.BackColor = Settings.Networking.DownloadColor; } catch { }
        try { n_lblGridSize.Text = n_tbGridSize.Value.ToString(); } catch { }
        try { n_lblValueSpacing.Text = n_tbValueSpacing.Value.ToString(); } catch { }

    }
    public void SaveSettings() {
        // General Settings
        Settings.RememberActiveTab = true;
        Settings.RememberPositions = g_chkRememberWindow.Checked;
        Settings.StartMinimized = g_chkStartMinimized.Checked;
        Settings.AlwaysOnTop = g_chkWindowOnTop.Checked;
        Settings.MinimizeWhenClosing = g_chkMinimizeClose.Checked;
        Settings.IconsInProcess = g_chkShowIcons.Checked;
        Settings.ServicesInStatus = g_chkServicesStatus.Checked;
        Settings.TimmingInStatus = g_chkTimmingStatus.Checked;
        Settings.AlternateRowColors = g_chkAlternateRowColors.Checked;
        Settings.ToTrayWhenMinimized = t_chkHideToTray.Checked;
        Settings.ToTrayWhenClosed = t_chkCloseToTray.Checked;
        Settings.ShowCPUOnTray = t_chkShowCPU.Checked;
        Settings.DblClickToRestore = t_chkRequireDobleClick.Checked;
        // Highlight Items Checks & Colors
        Settings.Highlights.NewItems = g_chkHighlightNew.Checked;
        Settings.Highlights.ChangingItems = g_chkHighlightChanges.Checked;
        Settings.Highlights.RemovedItems = g_chkHighlightRemoved.Checked;
        Settings.Highlights.FrozenItems = g_chkHighlightFrozen.Checked;
        Settings.Highlights.NewColor = g_btnHighlightNew.BackColor;
        Settings.Highlights.ChangingColor = g_btnHighlightChanges.BackColor;
        Settings.Highlights.RemovedColor = g_btnHighlightRemoved.BackColor;
        Settings.Highlights.FrozenColor = g_btnHighlightFrozen.BackColor;
        // Performance Graphs Settings
        Settings.Performance.Solid = p_chkSolidGraphs.Checked;
        Settings.Performance.AntiAlias = p_chkAntiAlias.Checked;
        Settings.Performance.ShadeBackground = p_chkShadeBackground.Checked;
        Settings.Performance.DisplayAverages = p_chkAvgValue.Checked;
        Settings.Performance.DisplayLegends = p_chkShowLegends.Checked;
        Settings.Performance.DisplayIndexes = p_chkShowIndexes.Checked;
        Settings.Performance.DisplayOnHover = p_chkOnlyOnHover.Checked;
        Settings.Performance.ValueSpacing = p_tbValueSpacing.Value;
        Settings.Performance.GridSize = p_tbGridSize.Value;
        Settings.Performance.LightColors = p_chkLightBackground.Checked;
        Settings.Performance.ShowKernelTime = p_chkKernelTime.Checked;
        Settings.Performance.SeparateCPUs = p_chkSeparateCpu.Checked;
        Settings.Performance.VerticalGridStyle = p_GridVertical.SelectedIndex;
        Settings.Performance.VerticalGridColor = p_GridVerticalColor.BackColor;
        Settings.Performance.HorizontalGridStyle = p_GridHorizontal.SelectedIndex;
        Settings.Performance.HorizontalGridColor = p_GridHorizontalColor.BackColor;
        Settings.Performance.AverageLineStyle = p_AverageLine.SelectedIndex;
        Settings.Performance.AverageLineColor = p_AverageLineColor.BackColor;
        // Networking Graphs Settings
        Settings.Networking.KeepUpdating = n_chkKeepDrawing.Checked;
        Settings.Networking.Solid = n_chkSolidGraphs.Checked;
        Settings.Networking.AntiAlias = n_chkAntiAlias.Checked;
        Settings.Networking.ShadeBackground = n_chkShadeBackground.Checked;
        Settings.Networking.DisplayAverages = n_chkAvgValue.Checked;
        Settings.Networking.DisplayIndexes = n_chkShowIndexes.Checked;
        Settings.Networking.DisplayLegends = n_chkShowLegends.Checked;
        Settings.Networking.ValueSpacing = n_tbValueSpacing.Value;
        Settings.Networking.GridSize = n_tbGridSize.Value;
        Settings.Networking.LightColors = n_chkLightBackground.Checked;
        Settings.Networking.VerticalGridStyle = n_GridVertical.SelectedIndex;
        Settings.Networking.VerticalGridColor = n_GridVerticalColor.BackColor;
        Settings.Networking.HorizontalGridStyle = n_GridHorizontal.SelectedIndex;
        Settings.Networking.HorizontalGridColor = n_GridHorizontalColor.BackColor;
        Settings.Networking.AverageLineStyle = n_AverageLine.SelectedIndex;
        Settings.Networking.AverageLineColor = n_AverageLineColor.BackColor;
        Settings.Networking.UploadColor = n_btnUpColor.BackColor;
        Settings.Networking.DownloadColor = n_btnDnColor.BackColor;

        Settings.StoreInFile = g_chkStoreXML.Checked;
        Settings.SaveAll();
    }

}