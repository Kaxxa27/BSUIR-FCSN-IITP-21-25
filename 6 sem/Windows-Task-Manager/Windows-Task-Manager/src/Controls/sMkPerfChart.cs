using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Runtime.Versioning;
namespace sMkTaskManager.Controls;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
public class sMkPerfChart : UserControl {
    private readonly ContextMenuStrip mnuStyle = new();
    private readonly ContextMenuStrip mnuGrid = new();
    private readonly ColorDialog cd = new();

    private const int MAX_VALUE_COUNT = 1024;// Keep only a maximum MAX_VALUE_COUNT amount of values
    private int _GridScrollOffset = 0;       // Offset value for the scrolling grid
    private double _MaxVisibleValue = 0;     // The highest displayed value, required for Relative Scale Mode
    private double _MaxStrictValue = 0;      // The highest displayed value, required for Strict Scale Mode
    private int _VisibleValues = 0;          // Amount of visible values (calculated from control width and value spacing)
    private double _MaxLegendValue;
    private string _LegendSuffix = "";
    private int _GridSpacing = 10;
    private int _ValueSpacing = 2;
    private readonly StringFormat _LegendStringFormat = new();
    private readonly Font _LegendStringFont = new(DefaultFont.FontFamily, 7);
    private readonly Dictionary<short, List<double>> _Values = new();
    private readonly Dictionary<short, double> _LastValue = new();
    private readonly Dictionary<short, double> _AvgValue = new();
    private readonly Dictionary<short, double> _MaxValue = new();
    private readonly Dictionary<short, string> _Indexes = new();
    private readonly Dictionary<short, Pen> _PenGraphs = new();

    public sMkPerfChart() {
        InitializeComponents();
        // Set Optimized Double Buffer to reduce flickering
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        // Redraw when resized
        SetStyle(ControlStyles.ResizeRedraw, true);
        // Initialize Values and Pens
        _LegendStringFormat.Alignment = StringAlignment.Far;
        _LegendStringFormat.LineAlignment = StringAlignment.Center;
        _PenGraphs.Add(1, new Pen(Color.Lime, 1) { DashStyle = DashStyle.Solid });
        _PenGraphs.Add(2, new Pen(Color.Red, 1) { DashStyle = DashStyle.Solid });
        _PenGraphs.Add(3, new Pen(Color.Cyan, 1) { DashStyle = DashStyle.Solid });
        _PenGraphs.Add(4, new Pen(Color.Yellow, 1) { DashStyle = DashStyle.Solid });
        _PenGraphs.Add(5, new Pen(Color.Blue, 1) { DashStyle = DashStyle.Solid });
        PenAverage.DashStyle = DashStyle.Solid;
        PenGridVertical.DashStyle = DashStyle.Dash;
        PenGridHorizontal.DashStyle = DashStyle.Dash;
        ScaleMode = ScaleModes.Absolute;
    }

    private void InitializeComponents() {
        mnuStyle.SuspendLayout();
        mnuGrid.SuspendLayout();
        SuspendLayout();
        // Style Context Menu Strip
        mnuStyle.Name = "mnuStyle";
        mnuStyle.ShowCheckMargin = true;
        mnuStyle.ShowImageMargin = false;
        mnuStyle.ShowItemToolTips = false;
        mnuStyle.Items.AddRange(new ToolStripItem[] {
            new ToolStripMenuItem("Solid Fill")   {Name = "mnuStyle_Solid"},
            new ToolStripMenuItem("Antialiasing") {Name = "mnuStyle_Antialias"},
            new ToolStripMenuItem("Light Colors") {Name = "mnuStyle_LightColors"},
            new ToolStripMenuItem("Shade Background") {Name = "mnuStyle_ShadeBg"},
            new ToolStripSeparator(),
            new ToolStripMenuItem("Display Indexes")  {Name = "mnuStyle_DispIndexes"},
            new ToolStripMenuItem("Display Legends")  {Name = "mnuStyle_DispLegends"},
            new ToolStripMenuItem("Display Averages") {Name = "mnuStyle_DispAverage"},
            new ToolStripMenuItem("Display only onHover")  {Name = "mnuStyle_DispOnHover"},
            new ToolStripSeparator(),
            new ToolStripMenuItem("Vertical Grid")   {Name = "mnuStyle_GridV", DropDown=mnuGrid},
            new ToolStripMenuItem("Horizontal Grid") {Name = "mnuStyle_GridH", DropDown=mnuGrid},
            new ToolStripSeparator(),
            new ToolStripMenuItem("Average Line") {Name = "mnuStyle_AvgLine", DropDown=mnuGrid},
            new ToolStripSeparator(),
            new ToolStripMenuItem("Clear Graph")  {Name = "mnuStyle_Clear"},
        });
        mnuStyle.Opening += mnuStyle_Opening;
        mnuStyle.ItemClicked += mnuStyle_ItemClicked;
        // Grid Context Menu Strip
        mnuGrid.Name = "mnuGrid";
        mnuGrid.ShowCheckMargin = true;
        mnuGrid.ShowImageMargin = false;
        mnuGrid.Items.AddRange(new ToolStripItem[] {
            new ToolStripMenuItem("None")  {Name = "mnuGrid_None"},
            new ToolStripSeparator(),
            new ToolStripMenuItem("Solid") {Name = "mnuGrid_Solid"},
            new ToolStripMenuItem("Dash")  {Name = "mnuGrid_Dash"},
            new ToolStripMenuItem("Dot")   {Name = "mnuGrid_Dot"},
            new ToolStripMenuItem("Dash Dot")     {Name = "mnuGrid_DashDot"},
            new ToolStripMenuItem("Dash Dot Dot") {Name = "mnuGrid_DashDotDot"},
            new ToolStripSeparator(),
            new ToolStripMenuItem("Change Color") {Name = "mnuGrid_Color"},
        });
        mnuGrid.Opening += mnuGrid_Opening;
        mnuGrid.ItemClicked += mnuGrid_ItemClicked;
        // cd Color Dialog
        cd.AnyColor = true;
        cd.FullOpen = true;
        // Base Component
        MouseEnter += OnMouseEnterOrLeave;
        MouseLeave += OnMouseEnterOrLeave;
        BackColor = Color.Black;
        ContextMenuStrip = mnuStyle;
        Name = "PerfChart";
        Size = new Size(560, 109);
        mnuStyle.ResumeLayout(false);
        mnuGrid.ResumeLayout(false);
        ResumeLayout(false);
    }

    public enum ScaleModes {
        Absolute,
        Relative,
        Strict
    }
    public double? LastValue(short idx) => _LastValue[idx];
    public string? Indexes(short idx) => _Indexes[idx];
    public double MaxValue {
        get { return _MaxStrictValue; }
        set { _MaxStrictValue = (value * 1.1); }
    }
    public string ValuesSuffix { get; set; } = "";
    public int LegendSpacing { get; set; } = 35;
    public bool DisplayAverage { get; set; } = false;
    public bool DisplayIndexes { get; set; } = false;
    public bool DisplayLegends { get; set; } = false;
    public bool DetailsOnHover { get; set; } = false;
    public bool BackSolid { get; set; } = false;
    public Color BackColorShade { get; set; } = Color.Black;
    public bool ShadeBackground { get; set; } = true;
    public ScaleModes ScaleMode { get; set; } = ScaleModes.Absolute;
    public bool AntiAliasing { get; set; } = true;
    public int ValueSpacing { get { return _ValueSpacing; } set { _ValueSpacing = Math.Max(1, value); } }
    public int GridSpacing { get { return _GridSpacing; } set { _GridSpacing = Math.Max(2, value); } }
    public Pen PenGraph1 { get { return _PenGraphs[1]; } set { _PenGraphs[1] = value; } }
    public Pen PenGraph2 { get { return _PenGraphs[2]; } set { _PenGraphs[2] = value; } }
    public Pen PenGraph3 { get { return _PenGraphs[3]; } set { _PenGraphs[3] = value; } }
    public Pen PenGraph4 { get { return _PenGraphs[4]; } set { _PenGraphs[4] = value; } }
    public Pen PenGraph5 { get { return _PenGraphs[5]; } set { _PenGraphs[5] = value; } }
    public Pen PenAverage { get; set; } = new Pen(Color.Orange, 1);
    public Pen PenLegend { get; set; } = new Pen(Color.Yellow, 1);
    public Pen PenGridVertical { get; set; } = new Pen(Color.Green, 1);
    public Pen PenGridHorizontal { get; set; } = new Pen(Color.Green, 1);
    public new Border3DStyle BorderStyle { get; set; } = Border3DStyle.Sunken;
    public bool LightColors {
        get { return !(BackColor == Color.Black); }
        set {
            BackColor = value ? Color.LightGray : Color.Black;
            BorderStyle = value ? Border3DStyle.Flat : Border3DStyle.Sunken;
            BackColorShade = value ? Color.FromArgb(50, BackColorShade) : Color.FromArgb(255, BackColorShade);
            PenLegend.Color = value ? Color.FromArgb(50, 50, 0) : Color.Yellow;
        }
    }

    public void Clear() {
        _MaxVisibleValue = 0;
        for (short i = 1; i <= 5; i++) {
            if (!_Values.ContainsKey(i)) continue;
            _Values[i].Clear(); _AvgValue[i] = 0; _MaxValue[i] = 0;
        }
        AddValue(0d, 0d, 0d, 0d, 0d);
    }
    public void AddValue(Int128 idx1Value, Int128? idx2Value = null, Int128? idx3Value = null, Int128? idx4Value = null, Int128? idx5Value = null) {
        AddValue((double)idx1Value, (double?)idx2Value, (double?)idx3Value, (double?)idx4Value, (double?)idx5Value);
    }
    public void AddValue(int idx1Value, int? idx2Value = null, int? idx3Value = null, int? idx4Value = null, int? idx5Value = null) {
        AddValue((double)idx1Value, idx2Value, idx3Value, idx4Value, idx5Value);
    }

    public void AddValue(double idx1Value, double? idx2Value = null, double? idx3Value = null, double? idx4Value = null, double? idx5Value = null) {
        var vals = new List<double?>() { 0, idx1Value, idx2Value, idx3Value, idx4Value, idx5Value };

        for (short i = 1; i <= 5; i++) {
            // We will avoid all the checks if the Index doesnt exist.
            if (!_Values.ContainsKey(i)) continue;
            // We wont support negative values for now
            if (vals[i].HasValue && vals[i] < 0) vals[i] = 0;
            // Ensure that values are not between 0 and 100 in Absolute Scale
            if (ScaleMode == ScaleModes.Absolute) {
                if (vals[i].HasValue && vals[i] > 100) vals[i] = 100;
            } else if (ScaleMode == ScaleModes.Strict) {
                if (vals[i].HasValue && vals[i] > _MaxStrictValue) vals[i] = _MaxStrictValue;
            }
            // Sanity check, if index exist we need the value!
            if (_Values.ContainsKey(i) && !vals[i].HasValue) vals[i] = 0;
            // Store as LastValue and add to Values at first position
            if (_Values.ContainsKey(i)) { _LastValue[i] = (double)vals[i]!; _Values[i].Insert(0, (double)vals[i]!); }
            // Remove last item if maximum value count is reached
            if (_Values.ContainsKey(i) && _Values[i].Count > MAX_VALUE_COUNT) _Values[i].RemoveAt(MAX_VALUE_COUNT);
        }
        // Calculate horizontal grid offset for scrolling effect
        _GridScrollOffset += ValueSpacing;
        if (_GridScrollOffset > GridSpacing) _GridScrollOffset %= GridSpacing;
        // Invalidate for redraw.
        Invalidate();
    }
    public void SetIndexes(string idx1Name, string? idx2Name = null, string? idx3Name = null, string? idx4Name = null, string? idx5Name = null) {
        _Indexes.Clear(); _Values.Clear(); _AvgValue.Clear(); _MaxValue.Clear();
        _Indexes.Add(1, idx1Name);
        _Values.Add(1, new(MAX_VALUE_COUNT));
        _AvgValue.Add(1, 0); _MaxValue.Add(1, 0);
        if (!string.IsNullOrEmpty(idx2Name)) { _Indexes.Add(2, idx2Name); _Values.Add(2, new(MAX_VALUE_COUNT)); _AvgValue.Add(2, 0); _MaxValue.Add(2, 0); }
        if (!string.IsNullOrEmpty(idx3Name)) { _Indexes.Add(3, idx3Name); _Values.Add(3, new(MAX_VALUE_COUNT)); _AvgValue.Add(3, 0); _MaxValue.Add(3, 0); }
        if (!string.IsNullOrEmpty(idx4Name)) { _Indexes.Add(4, idx4Name); _Values.Add(4, new(MAX_VALUE_COUNT)); _AvgValue.Add(4, 0); _MaxValue.Add(4, 0); }
        if (!string.IsNullOrEmpty(idx5Name)) { _Indexes.Add(5, idx5Name); _Values.Add(5, new(MAX_VALUE_COUNT)); _AvgValue.Add(5, 0); _MaxValue.Add(5, 0); }
        for (short i = 1; i <= _Indexes.Count; i++) {
            if (_Indexes[i].Trim() != "" && !_Indexes[i].Trim().EndsWith(":")) { _Indexes[i] = _Indexes[i] + ": "; }
        }
    }
    public void CopySettings(sMkPerfChart OtherChart, bool IncludeEverything = false) {
        BorderStyle = OtherChart.BorderStyle;
        BackSolid = OtherChart.BackSolid;
        ShadeBackground = OtherChart.ShadeBackground;
        AntiAliasing = OtherChart.AntiAliasing;
        DisplayAverage = OtherChart.DisplayAverage;
        DisplayLegends = OtherChart.DisplayLegends;
        DisplayIndexes = OtherChart.DisplayIndexes;
        DetailsOnHover = OtherChart.DetailsOnHover;
        ValueSpacing = OtherChart.ValueSpacing;
        GridSpacing = OtherChart.GridSpacing;
        LightColors = OtherChart.LightColors;
        PenGridVertical.Color = OtherChart.PenGridVertical.Color;
        PenGridVertical.DashStyle = OtherChart.PenGridVertical.DashStyle;
        PenGridHorizontal.Color = OtherChart.PenGridHorizontal.Color;
        PenGridHorizontal.DashStyle = OtherChart.PenGridHorizontal.DashStyle;
        PenLegend.Color = OtherChart.PenLegend.Color;
        PenAverage.Color = OtherChart.PenAverage.Color;
        PenAverage.DashStyle = OtherChart.PenAverage.DashStyle;
        if (IncludeEverything) {
            BackColor = OtherChart.BackColor;
            BackColorShade = OtherChart.BackColorShade;
            PenGraph1 = OtherChart.PenGraph1;
            PenGraph2 = OtherChart.PenGraph2;
            PenGraph3 = OtherChart.PenGraph3;
            PenGraph4 = OtherChart.PenGraph4;
            PenGraph5 = OtherChart.PenGraph5;
            SetIndexes(OtherChart.Indexes(1)!, OtherChart.Indexes(2), OtherChart.Indexes(3), OtherChart.Indexes(4), OtherChart.Indexes(5));
            ValuesSuffix = OtherChart.ValuesSuffix;
            ScaleMode = OtherChart.ScaleMode;
        }
    }

    private void OnMouseEnterOrLeave(object? sender, EventArgs e) {
        if (DetailsOnHover) Invalidate();
    }
    protected override void OnPaint(PaintEventArgs e) {
        // Base Paint
        base.OnPaint(e);
        // Enable AntiAliasing, if needed
        e.Graphics.SmoothingMode = this.AntiAliasing ? SmoothingMode.AntiAlias : SmoothingMode.Default;
        // Call Custom Draws
        DrawBackAndGrid(e.Graphics);
        DrawChartValues(e.Graphics);
        DrawAverageData(e.Graphics);
        DrawIndexes(e.Graphics);
        DrawLegends(e.Graphics);
        // Draw Border At TOP of everything
        ControlPaint.DrawBorder3D(e.Graphics, 0, 0, Width, Height, BorderStyle);
    }
    private void DrawBackAndGrid(Graphics g) {
        // Draw the background gradient rectangle
        Rectangle baseRectangle = new(0, 0, Width, Height);
        if (ShadeBackground) {
            using Brush TheBrush = new LinearGradientBrush(baseRectangle, this.BackColorShade, this.BackColor, LinearGradientMode.Vertical);
            g.FillRectangle(TheBrush, baseRectangle);
        } else {
            using Brush TheBrush = new SolidBrush(this.BackColor);
            g.FillRectangle(TheBrush, baseRectangle);
        }
        // Draw all visible vertical gridlines (if any)
        if (PenGridVertical.DashStyle != DashStyle.Custom) {
            int i = Width - _GridScrollOffset;
            while (i >= 0) {
                g.DrawLine(PenGridVertical, i, 0, i, Height);
                i -= GridSpacing;
            }
        }
        // Draw all visible horizontal gridlines (if any)
        if (PenGridHorizontal.DashStyle != DashStyle.Custom) {
            int i = 0;
            while (i < Height) {
                g.DrawLine(PenGridHorizontal, 0, i, Width, i);
                i += GridSpacing;
            }
        }
    }
    private void DrawChartValues(Graphics g) {
        // Calculate Visible Values
        if (!_Values.ContainsKey(1)) return;
        _VisibleValues = Math.Min(((DisplayLegends ? Width - LegendSpacing : Width) / ValueSpacing) + 1, _Values[1].Count);
        if (_VisibleValues < 1) return;
        _MaxVisibleValue = 0;
        for (short i = 1; i <= 5; i++) {
            if (!_Values.ContainsKey(i)) continue;
            var vvs = _Values[i].GetRange(0, _VisibleValues); // Take out only the visible values
            if (vvs.Count < 1) continue;
            // Calculate Max Visible Value & Average, only if needed
            if (ScaleMode != ScaleModes.Absolute) _MaxVisibleValue = Math.Max(_MaxVisibleValue, vvs.Max());
            if (DisplayAverage) { _AvgValue[i] = vvs.Average(); _MaxValue[i] = vvs.Max(); }
            // Dirty little "trick": initialize the first previous Point outside the bounds
            Point currPoint = new();
            Point prevPoint = new(Width + ValueSpacing, Height);
            foreach (double v in vvs) {
                // Connect all visible values with lines
                currPoint.X = prevPoint.X - ValueSpacing;
                currPoint.Y = CalcVerticalPosition(v);
                // If we need to solidify the area, do it now.
                if (BackSolid) {
                    using SolidBrush thisBrush = new(Color.FromArgb((LightColors ? 150 : 100), _PenGraphs[i].Color.R, _PenGraphs[i].Color.G, _PenGraphs[i].Color.B));
                    Point[] PolyPoints = {
                        new Point(prevPoint.X, Height),
                        new Point(prevPoint.X, prevPoint.Y),
                        new Point(currPoint.X, currPoint.Y),
                        new Point(currPoint.X, Height)
                    };
                    g.FillPolygon(thisBrush, PolyPoints);
                }
                // And now draw the line on top of it.
                g.DrawLine(_PenGraphs[i], prevPoint, currPoint);
                prevPoint = currPoint;
            }
        }

    }
    private void DrawAverageData(Graphics g) {
        if (DetailsOnHover) {
            if (!ClientRectangle.Contains(PointToClient(Cursor.Position))) {
                Cursor = Cursors.Default; return;
            } else {
                Cursor = Cursors.Help;
            }
        }
        var posX = (DisplayLegends & DisplayIndexes) ? LegendSpacing + 115 : (DisplayLegends ? LegendSpacing + 5 : (DisplayIndexes ? 115 : 4));
        var posY = 2;
        for (short i = 1; i <= 5; i++) {
            if (!_Values.ContainsKey(i)) continue;
            // Only draw average line when possible (visibleValues) and needed (style setting)
            if (_VisibleValues > 1 && PenAverage.DashStyle != DashStyle.Custom) {
                g.DrawLine(PenAverage, 2, CalcVerticalPosition(_AvgValue[i]), Width - 3, CalcVerticalPosition(_AvgValue[i]));
            }
            // Draw Average Values
            if (_VisibleValues > 1 && DisplayAverage) {
                using Brush sb = new SolidBrush(PenLegend.Color);
                g.DrawString("Avg: " + Math.Round(_AvgValue[i]) + ((ScaleMode == ScaleModes.Absolute) ? "%" : ValuesSuffix), Font, sb, posX, posY);
                g.DrawString("Max: " + Math.Round(_MaxValue[i]) + ((ScaleMode == ScaleModes.Absolute) ? "%" : ValuesSuffix), Font, sb, posX + 115, posY);
            }
            // Offset by +13 for the next
            posY += 13;

        }
    }
    private void DrawIndexes(Graphics g) {
        if (DetailsOnHover && !ClientRectangle.Contains(PointToClient(Cursor.Position))) return;
        if (_VisibleValues < 1 || !DisplayIndexes) return;
        // Draw Indexes Values
        var posX = (DisplayLegends ? LegendSpacing + 5 : 4);
        var posY = 2;
        for (short i = 1; i <= 5; i++) {
            if (!_Values.ContainsKey(i)) continue;
            using Brush sb = new SolidBrush(_PenGraphs[i].Color);
            g.DrawString(_Indexes[i] + Math.Round(_LastValue[i], 1) + ((ScaleMode == ScaleModes.Absolute) ? "%" : ValuesSuffix), Font, sb, posX, posY);
            posY += 13;
        }
    }
    private void DrawLegends(Graphics g) {
        if (!DisplayLegends) return;
        if (!CalcLegendSuffix().Equals(_LegendSuffix)) _LegendSuffix = CalcLegendSuffix();
        if (!CalcMaxLegendValue().Equals(_MaxLegendValue)) _MaxLegendValue = CalcMaxLegendValue();

        LegendSpacing = Math.Max(Convert.ToInt32(g.MeasureString(" " + Math.Round(_MaxLegendValue / 2, ((_MaxLegendValue > 5) ? 0 : 1)).ToString() + _LegendSuffix, _LegendStringFont).Width), Convert.ToInt32(g.MeasureString(" " + Math.Round(_MaxLegendValue, ((_MaxLegendValue > 5) ? 0 : 1)).ToString() + _LegendSuffix, _LegendStringFont).Width));
        if (LegendSpacing < 15) LegendSpacing = 15;

        using Brush TheBrush = new SolidBrush(BackColor);
        g.FillRectangle(TheBrush, 0, 0, LegendSpacing, Height);
        g.DrawLine(PenLegend, LegendSpacing, 0, LegendSpacing, Height);

        using Brush sb = new SolidBrush(PenLegend.Color);
        g.DrawString("0" + _LegendSuffix, _LegendStringFont, sb, LegendSpacing - 1, Height - 10, _LegendStringFormat);
        g.DrawString(Math.Round(_MaxLegendValue / 2, ((_MaxLegendValue > 5) ? 0 : 1)).ToString() + _LegendSuffix, _LegendStringFont, sb, LegendSpacing - 1, (float)(Height / 2.0), _LegendStringFormat);
        g.DrawString(Math.Round(_MaxLegendValue, ((_MaxLegendValue > 5) ? 0 : 1)).ToString() + _LegendSuffix, _LegendStringFont, sb, LegendSpacing - 1, 8, _LegendStringFormat);
    }

    private int CalcVerticalPosition(double value) {
        double result = 0;

        if (ScaleMode == ScaleModes.Absolute) {
            result = value * (Height - 4) / 100;
        } else if (ScaleMode == ScaleModes.Relative) {
            result = (_MaxVisibleValue > 0) ? (value * (Height - 4) / _MaxVisibleValue) : 0;
        } else if (ScaleMode == ScaleModes.Strict) {
            result = (_MaxStrictValue > 0) ? (value * (Height - 4) / _MaxStrictValue) : 0;
        }

        result = Math.Round((Height - 2) - result, 0);
        if (result >= (Height - 3)) result = Height - 3;
        return Convert.ToInt32(result);
    }
    private double CalcMaxLegendValue() {
        if (ScaleMode == ScaleModes.Strict) return _MaxStrictValue;
        if (ScaleMode == ScaleModes.Relative) return (_MaxVisibleValue > 1024) ? _MaxVisibleValue / 1024 : _MaxVisibleValue;
        return 100;
    }
    private string CalcLegendSuffix() {
        if (ScaleMode == ScaleModes.Absolute) return "%";
        if (ScaleMode == ScaleModes.Strict) return ValuesSuffix;
        if (ScaleMode == ScaleModes.Relative) return (_MaxVisibleValue > 1024) ? ValuesSuffix.Replace("K", "M") : ValuesSuffix;
        return "";
    }

    private void mnuStyle_Opening(object? sender, System.ComponentModel.CancelEventArgs e) {
        e.Cancel = (!(ModifierKeys == Keys.Control)) && (!(ModifierKeys == Keys.Shift));
        ((ToolStripMenuItem)mnuStyle.Items["mnuStyle_Solid"]).Checked = BackSolid;
        ((ToolStripMenuItem)mnuStyle.Items["mnuStyle_Antialias"]).Checked = AntiAliasing;
        ((ToolStripMenuItem)mnuStyle.Items["mnuStyle_ShadeBg"]).Checked = ShadeBackground;
        ((ToolStripMenuItem)mnuStyle.Items["mnuStyle_LightColors"]).Checked = LightColors;
        ((ToolStripMenuItem)mnuStyle.Items["mnuStyle_DispAverage"]).Checked = DisplayAverage;
        ((ToolStripMenuItem)mnuStyle.Items["mnuStyle_DispLegends"]).Checked = DisplayLegends;
        ((ToolStripMenuItem)mnuStyle.Items["mnuStyle_DispIndexes"]).Checked = DisplayIndexes;
        ((ToolStripMenuItem)mnuStyle.Items["mnuStyle_DispOnHover"]).Checked = DetailsOnHover;
    }
    private void mnuStyle_ItemClicked(object? sender, ToolStripItemClickedEventArgs e) {
        if (e.ClickedItem == null) return;
        switch (e.ClickedItem.Name) {
            case "mnuStyle_Solid": BackSolid = !BackSolid; break;
            case "mnuStyle_Antialias": AntiAliasing = !AntiAliasing; break;
            case "mnuStyle_ShadeBg": ShadeBackground = !ShadeBackground; break;
            case "mnuStyle_LightColors": LightColors = !LightColors; break;
            case "mnuStyle_DispAverage": DisplayAverage = !DisplayAverage; break;
            case "mnuStyle_DispLegends": DisplayLegends = !DisplayLegends; break;
            case "mnuStyle_DispIndexes": DisplayIndexes = !DisplayIndexes; break;
            case "mnuStyle_DispOnHover": DetailsOnHover = !DetailsOnHover; break;
            case "mnuStyle_Clear": Clear(); break;
            default: break;
        }
        Invalidate();
    }
    private void mnuGrid_Opening(object? sender, CancelEventArgs e) {
        if (mnuGrid.OwnerItem == null) { return; }
        Pen? thisPen = null;
        if (mnuGrid.OwnerItem == mnuStyle.Items["mnuStyle_GridV"]) { thisPen = PenGridVertical; }
        if (mnuGrid.OwnerItem == mnuStyle.Items["mnuStyle_GridH"]) { thisPen = PenGridHorizontal; }
        if (mnuGrid.OwnerItem == mnuStyle.Items["mnuStyle_AvgLine"]) { thisPen = PenAverage; }
        if (thisPen == null) { return; }

        ((ToolStripMenuItem)mnuGrid.Items["mnuGrid_Solid"]).Checked = thisPen.DashStyle == DashStyle.Solid;
        ((ToolStripMenuItem)mnuGrid.Items["mnuGrid_Dot"]).Checked = thisPen.DashStyle == DashStyle.Dot;
        ((ToolStripMenuItem)mnuGrid.Items["mnuGrid_Dash"]).Checked = thisPen.DashStyle == DashStyle.Dash;
        ((ToolStripMenuItem)mnuGrid.Items["mnuGrid_DashDot"]).Checked = thisPen.DashStyle == DashStyle.DashDot;
        ((ToolStripMenuItem)mnuGrid.Items["mnuGrid_DashDotDot"]).Checked = thisPen.DashStyle == DashStyle.DashDotDot;
        ((ToolStripMenuItem)mnuGrid.Items["mnuGrid_None"]).Checked = thisPen.DashStyle == DashStyle.Custom;
        ((ToolStripMenuItem)mnuGrid.Items["mnuGrid_Color"]).BackColor = thisPen.Color;

    }
    private void mnuGrid_ItemClicked(object? sender, ToolStripItemClickedEventArgs e) {
        if (mnuGrid.OwnerItem == null) { return; }
        if (e.ClickedItem == null) { return; }
        Pen? thisPen = null;
        if (mnuGrid.OwnerItem == mnuStyle.Items["mnuStyle_GridV"]) { thisPen = PenGridVertical; }
        if (mnuGrid.OwnerItem == mnuStyle.Items["mnuStyle_GridH"]) { thisPen = PenGridHorizontal; }
        if (mnuGrid.OwnerItem == mnuStyle.Items["mnuStyle_AvgLine"]) { thisPen = PenAverage; }
        if (thisPen == null) { return; }

        switch (e.ClickedItem.Name) {
            case "mnuGrid_Solid": thisPen.DashStyle = DashStyle.Solid; break;
            case "mnuGrid_Dot": thisPen.DashStyle = DashStyle.Dot; break;
            case "mnuGrid_Dash": thisPen.DashStyle = DashStyle.Dash; break;
            case "mnuGrid_DashDot": thisPen.DashStyle = DashStyle.DashDot; break;
            case "mnuGrid_DashDotDot": thisPen.DashStyle = DashStyle.DashDotDot; break;
            case "mnuGrid_None": thisPen.DashStyle = DashStyle.Custom; break;
            case "mnuGrid_Color":
                mnuGrid.Close(); mnuStyle.Close();
                cd.Color = thisPen.Color;
                if (cd.ShowDialog(this) == DialogResult.OK) thisPen.Color = cd.Color;
                break;
            default: break;
        }
        Invalidate();

    }

}
