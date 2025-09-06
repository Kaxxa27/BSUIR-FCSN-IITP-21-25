using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.Versioning;
namespace sMkTaskManager.Forms;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
public partial class frmAbout : Form {

    public frmAbout() {
        InitializeComponent();
        Load += eLoad;
        KeyDown += (o, e) => { if (e.KeyCode is Keys.Escape or Keys.Enter or Keys.Space) Close(); };
        btnOk.Click += (o, e) => { Close(); };
       // lnkDonate.LinkClicked += eLinkClicked;
       // lnkFeedback.LinkClicked += eLinkClicked;
        lnkGithub.LinkClicked += eLinkClicked;
       // lnkLicense.LinkClicked += eLinkClicked;
    }
    private static string _versionFull = "";
    private static string _versionShort = "";
    private static readonly int _bannerHeight = 70;
    private static readonly int _bannerDivider = 6;

    private void eLoad(object? sender, EventArgs e) {
        //Version version = Assembly.GetExecutingAssembly().GetName().Version!;
        //_versionFull = "v" + version?.Major + "." + version?.Minor + "." + version?.Build;
        //_versionShort = "v" + version?.Major + "." + version?.Minor;
        //lblProduct.Text = ProductName + " " + _versionFull;
        //lblBuild.Text = lblBuild.Text.Replace("xxxx1", version?.Revision.ToString());
        ///* Set Build Time, if possible */
        //try {
        //    long buildTime = long.Parse(ApplicationInfo.BuildMark);
        //    lblBuild.Text = lblBuild.Text.Replace("xxxx2", new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(buildTime).ToLocalTime().ToString());
        //} catch { lblBuild.Text = lblBuild.Text.Replace("xxxx2", "n/a."); };
        //lblCopyright.Text = "Copyright © 2024 Yauheni Kakhnouski";
        //pbBanner.Image = CreateBanner(pbBanner);
    }
    private void eLinkClicked(object? sender, LinkLabelLinkClickedEventArgs e) {
        if (sender == null) return;
        string uri = "";
        switch ((sender as LinkLabel)!.Name) {
            case nameof(lnkGithub): uri = @"https://github.com/Kaxxa27"; break;
            // case nameof(lnkDonate): uri = @"https://github.com/sponsors/creizlein"; break;
            // case nameof(lnkFeedback): uri = @"https://github.com/sMkTools/sMkTaskManager/issues/new"; break;
            // case nameof(lnkLicense): uri = @"http://github.com/sMkTools/sMkTaskManager/blob/stable/LICENSE.txt"; break;
        }
        if (!string.IsNullOrEmpty(uri)) {
            Process.Start("explorer.exe", $"\"{uri}\"");
        }
    }
    private Bitmap CreateBanner(Control obj) {
        obj.BackColor = Color.Empty;
        obj.Height = _bannerHeight + _bannerDivider;
        Bitmap myBitmap = new(obj.Width, obj.Height);
        using (Graphics g = Graphics.FromImage(myBitmap)) {
            g.InterpolationMode = InterpolationMode.High;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            g.CompositingQuality = CompositingQuality.HighQuality;
            // Draw Background Gradient
            Color color1 = Color.FromArgb(87, 97, 105);
            Color color2 = Color.FromArgb(161, 169, 174);
            LinearGradientBrush br1 = new(g.VisibleClipBounds, color1, color2, 0f);
            ColorBlend cblend = new(3) {
                Colors = new Color[3] { color1, color2, color1 },
                Positions = new float[3] { 0f, 0.7f, 1f }
            };
            br1.InterpolationColors = cblend;
            g.FillRectangle(br1, g.VisibleClipBounds);
            // Draw Icon
            g.DrawImage(Resources.Resources.AppIcon_Cyan, 40, 10, 48, 48);
            // Draw Product Name and Version
            GraphicsPath p = new();
            p.AddString(_versionShort, new FontFamily("Arial"), (int)FontStyle.Bold, 26, new Point(350, 30), null);
            // g.DrawPath(Pens.Black, p);
            g.FillPath(Brushes.LightGray, p);
            p = new();
            p.AddString(ProductName, new FontFamily("Arial"), (int)FontStyle.Bold, 28, new Point(90, 15), null);
            // g.DrawPath(Pens.Black, p);
            g.FillPath(Brushes.White, p);
            g.DrawString("Copyright © 2023 sMkDesigns", new Font(FontFamily.GenericSansSerif, 5), Brushes.LightGray, 4, 60);
            // Draw Divider Line
            color1 = Color.FromArgb(249, 114, 4);
            color2 = Color.FromArgb(255, 213, 0);
            cblend = new(3) {
                Colors = new Color[3] { color1, color2, color1 },
                Positions = new float[3] { 0f, 0.5f, 1f }
            };
            br1.InterpolationColors = cblend;
            g.FillRectangle(br1, 0, g.VisibleClipBounds.Height - _bannerDivider, g.VisibleClipBounds.Width, _bannerDivider);
            br1.Dispose();
        }
        return myBitmap;
    }

    private class ApplicationInfo {
        public static string Company { get { return GetExecutingAssemblyAttribute<AssemblyCompanyAttribute>(a => a.Company); } }
        public static string Product { get { return GetExecutingAssemblyAttribute<AssemblyProductAttribute>(a => a.Product); } }
        public static string Copyright { get { return GetExecutingAssemblyAttribute<AssemblyCopyrightAttribute>(a => a.Copyright); } }
        public static string Trademark { get { return GetExecutingAssemblyAttribute<AssemblyTrademarkAttribute>(a => a.Trademark); } }
        public static string Title { get { return GetExecutingAssemblyAttribute<AssemblyTitleAttribute>(a => a.Title); } }
        public static string Description { get { return GetExecutingAssemblyAttribute<AssemblyDescriptionAttribute>(a => a.Description); } }
        public static string Configuration { get { return GetExecutingAssemblyAttribute<AssemblyConfigurationAttribute>(a => a.Configuration); } }
        public static string FileVersion { get { return GetExecutingAssemblyAttribute<AssemblyFileVersionAttribute>(a => a.Version); } }
        public static string BuildMark { get { return GetExecutingAssemblyAttribute<BuildMarkAttribute>(a => a.BuildMark); } }
        public static string GitCommit { get { return GetExecutingAssemblyAttribute<GitCommitAttribute>(a => a.GitCommit); } }

        public static Version Version { get { return Assembly.GetExecutingAssembly().GetName().Version!; } }
        public static string VersionFull { get { return Version.ToString(); } }
        public static string VersionMajor { get { return Version.Major.ToString(); } }
        public static string VersionMinor { get { return Version.Minor.ToString(); } }
        public static string VersionBuild { get { return Version.Build.ToString(); } }
        public static string VersionRevision { get { return Version.Revision.ToString(); } }

        private static string GetExecutingAssemblyAttribute<T>(Func<T, string> value) where T : Attribute {
            T attribute = (T)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(T))!;
            return value.Invoke(attribute);
        }
    }

}
