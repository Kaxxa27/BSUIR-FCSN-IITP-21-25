using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Runtime.Versioning;
namespace sMkTaskManager.Controls;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
public class sMkPerfChartOld : UserControl {
    private readonly ContextMenuStrip mnuStyle = new();
    private readonly ContextMenuStrip mnuGrid = new();
    private readonly ColorDialog cd = new();

    private const int MAX_VALUE_COUNT = 1024;// Keep only a maximum MAX_VALUE_COUNT amount of values
    private int _GridScrollOffset = 0;       // Offset value for the scrolling grid
    private double _MaxValue = 0;            // The highest displayed value, required for Relative Scale Mode
    private double _MaxStrictValue = 0;      // The highest displayed value, required for Strict Scale Mode
    private double _AverageValue1 = 0;       // The average value for first index
    private double _AverageValue2 = 0;       // The average value for second index
    private int _VisibleValues = 0;          // Amount of visible values (calculated from control width and value spacing)
    private string _FirstIndex = "";
    private string _SecondIndex = "";
    private double _MaxLegendValue;
    private string _LegendSuffix = "";
    private readonly StringFormat _LegendStringFormat = new();
    private readonly Font _LegendStringFont = new(DefaultFont.FontFamily, 7);
    private readonly List<double> _Values = new(MAX_VALUE_COUNT);
    private readonly List<double> _ValuesSecond = new(MAX_VALUE_COUNT);

    public sMkPerfChartOld() {
        InitializeComponents();

        // Set Optimized Double Buffer to reduce flickering
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        // Redraw when resized
        SetStyle(ControlStyles.ResizeRedraw, true);
        // Initialize some more values
        _LegendStringFormat.Alignment = StringAlignment.Far;
        _LegendStringFormat.LineAlignment = StringAlignment.Center;
        PenGraph.DashStyle = DashStyle.Solid;
        PenSecondGraph.DashStyle = DashStyle.Solid;
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
    public double LastValue { get; private set; } = 0;
    public double LastSecondValue { get; private set; } = 0;
    public bool UseTwoValues { get; set; } = false;
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
    public int ValueSpacing { get; set; } = 2;
    public bool AntiAliasing { get; set; } = true;
    public int GridSpacing { get; set; } = 10;
    public Pen PenGraph { get; set; } = new Pen(Color.Lime, 1);
    public Pen PenSecondGraph { get; set; } = new Pen(Color.Red, 1);
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
        _MaxValue = 0;
        _Values.Clear();
        _ValuesSecond.Clear();
        AddValue(0d, 0d);
    }
    public void AddValue(Int128 Value1) {
        AddValue((double)Value1);
    }
    public void AddValue(Int128 Value1, Int128 Value2) {
        AddValue((double)Value1, (double)Value2);
    }

    public void AddValue(double Value1, double Value2 = 0) {
        // Ensure that values are not larger than 100 in Absolute Scale
        if (ScaleMode == ScaleModes.Absolute && Value1 > 100) Value1 = 100;
        if (ScaleMode == ScaleModes.Absolute && Value2 > 100) Value1 = 100;
        // Check if new value is the current max value
        if (ScaleMode == ScaleModes.Strict && Value1 > _MaxStrictValue) Value1 = _MaxStrictValue;
        if (Value1 > _MaxValue) _MaxValue = Value1;
        // Compute last Value
        LastValue = Value1;
        // Add new value always to first position
        _Values.Insert(0, Math.Max(Value1, 0));
        // Remove last item if maximum value count is reached
        if (_Values.Count > MAX_VALUE_COUNT) _Values.RemoveAt(MAX_VALUE_COUNT);
        // Do the same but for the second value, if enabled
        if (UseTwoValues) {
            if (ScaleMode == ScaleModes.Strict && Value2 > _MaxStrictValue) Value2 = _MaxStrictValue;
            LastSecondValue = Value2;
            _ValuesSecond.Insert(0, Math.Max(Value2, 0));
            if (Value2 > _MaxValue) _MaxValue = Value2;
            if (_ValuesSecond.Count > MAX_VALUE_COUNT) _ValuesSecond.RemoveAt(MAX_VALUE_COUNT);
        }
        // Calculate horizontal grid offset for scrolling effect
        _GridScrollOffset += ValueSpacing;
        if (_GridScrollOffset > GridSpacing) _GridScrollOffset %= GridSpacing;
        // Invalidate for redraw.
        Invalidate();
    }
    public void SetIndexes(string First, string Second = "") {
        _FirstIndex = string.IsNullOrEmpty(First) ? "" : First + ": ";
        _SecondIndex = string.IsNullOrEmpty(Second) ? "" : Second + ": ";
    }
    public void CopySettings(sMkPerfChartOld OtherChart, bool IncludeEverything = false) {
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
            PenGraph.Color = OtherChart.PenGraph.Color;
            PenSecondGraph.Color = OtherChart.PenSecondGraph.Color;
            SetIndexes(OtherChart._FirstIndex, OtherChart._SecondIndex);
            ValuesSuffix = OtherChart.ValuesSuffix;
            UseTwoValues = OtherChart.UseTwoValues;
            ScaleMode = OtherChart.ScaleMode;
        }
    }

    protected override void OnPaint(PaintEventArgs e) {
        Stopwatch tmr = new();
        Extensions.StartMeasure(tmr);
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
        Extensions.StopMeasure(tmr, Name);
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
        _VisibleValues = Math.Min(((DisplayLegends ? Width - LegendSpacing : Width) / ValueSpacing) + 1, _Values.Count);
        // Calculate Max Value, if needed
        if (ScaleMode != ScaleModes.Absolute && _MaxValue != LastValue) CalcHighestValue();
        // Dirty little "trick": initialize the first previous Point outside the bounds
        Point currPoint = new();
        Point prevPoint = new(Width + ValueSpacing, Height);
        // Connect all visible values with lines
        for (int i = 0; i < _VisibleValues; i++) {
            currPoint.X = prevPoint.X - ValueSpacing;
            currPoint.Y = CalcVerticalPosition(_Values[i]);
            // If we need to solidify the area, do it now.
            if (BackSolid) {
                using SolidBrush thisBrush = new(Color.FromArgb((LightColors ? 150 : 100), PenGraph.Color.R, PenGraph.Color.G, PenGraph.Color.B));
                Point[] PolyPoints = {
                    new Point(prevPoint.X, Height),
                    new Point(prevPoint.X, prevPoint.Y),
                    new Point(currPoint.X, currPoint.Y),
                    new Point(currPoint.X, Height)
                };
                g.FillPolygon(thisBrush, PolyPoints);
            }
            // And now draw the line in top of it.
            g.DrawLine(PenGraph, prevPoint, currPoint);
            prevPoint = currPoint;
        }
        // Now draw second values, if wanted.
        if (UseTwoValues) {
            currPoint = new Point();
            prevPoint = new Point(Width + ValueSpacing, Height);
            for (int i = 0; i < _VisibleValues; i++) {
                currPoint.X = prevPoint.X - ValueSpacing;
                currPoint.Y = CalcVerticalPosition(_ValuesSecond[i]);
                // If we need to solidify the area, do it now.
                if (BackSolid) {
                    using SolidBrush thisBrush = new(Color.FromArgb((LightColors ? 200 : 150), PenSecondGraph.Color.R, PenSecondGraph.Color.G, PenSecondGraph.Color.B));
                    Point[] PolyPoints = {
                        new Point(prevPoint.X, Height),
                        new Point(prevPoint.X, prevPoint.Y),
                        new Point(currPoint.X, currPoint.Y),
                        new Point(currPoint.X, Height)
                    };
                    g.FillPolygon(thisBrush, PolyPoints);
                }
                // And now draw the line in top of it.
                g.DrawLine(PenSecondGraph, prevPoint, currPoint);
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

        // Only calc the average value if we are going to use it.
        if (PenAverage.DashStyle != DashStyle.Custom || DisplayAverage) {
            if (_Values.Count >= 2) _AverageValue1 = _Values.GetRange(1, Math.Max(_VisibleValues - 2, 1)).Average();
            if (UseTwoValues && _ValuesSecond.Count > 2) _AverageValue2 = _ValuesSecond.GetRange(1, Math.Max(_VisibleValues - 2, 1)).Average();
        }
        // Only draw average line when possible (visibleValues) and needed (style setting)
        if (_VisibleValues > 1 && PenAverage.DashStyle != DashStyle.Custom) {
            g.DrawLine(PenAverage, 2, CalcVerticalPosition(_AverageValue1), Width - 3, CalcVerticalPosition(_AverageValue1));
            if (UseTwoValues) {
                g.DrawLine(PenAverage, 2, CalcVerticalPosition(_AverageValue2), Width - 3, CalcVerticalPosition(_AverageValue2));
            }
        }
        // Draw Average Values
        if (_VisibleValues > 1 && DisplayAverage) {
            using Brush sb = new SolidBrush(PenLegend.Color);
            if (UseTwoValues) {
                g.DrawString("Avg: " + Math.Round(_AverageValue1) + ((ScaleMode == ScaleModes.Absolute) ? "%" : ValuesSuffix), Font, sb, ((DisplayLegends & DisplayIndexes) ? LegendSpacing + 115 : (DisplayLegends ? LegendSpacing + 5 : (DisplayIndexes ? 115 : 4.0F))), 2.0F);
                g.DrawString("Avg: " + Math.Round(_AverageValue2) + ((ScaleMode == ScaleModes.Absolute) ? "%" : ValuesSuffix), Font, sb, ((DisplayLegends & DisplayIndexes) ? LegendSpacing + 115 : (DisplayLegends ? LegendSpacing + 5 : (DisplayIndexes ? 115 : 4.0F))), 15.0F);
            } else {
                g.DrawString("Avg:   " + Math.Round(_AverageValue1) + ((ScaleMode == ScaleModes.Absolute) ? "%" : ValuesSuffix), Font, sb, ((DisplayLegends & DisplayIndexes) ? LegendSpacing + 115 : (DisplayLegends ? LegendSpacing + 5 : (DisplayIndexes ? 115 : 4.0F))), 2.0F);
                g.DrawString("Peak: " + Math.Round(_MaxValue, 1) + ((ScaleMode == ScaleModes.Absolute) ? "%" : ValuesSuffix), Font, sb, ((DisplayLegends & DisplayIndexes) ? LegendSpacing + 115 : (DisplayLegends ? LegendSpacing + 5 : (DisplayIndexes ? 115 : 4.0F))), 15.0F);
            }
        }
    }
    private void DrawIndexes(Graphics g) {
        if (DetailsOnHover && !ClientRectangle.Contains(PointToClient(Cursor.Position))) return;
        // Draw Indexes Values
        if (_VisibleValues > 0 && DisplayIndexes) {
            using Brush sb = new SolidBrush(PenGraph.Color);
            g.DrawString(_FirstIndex + Math.Round(LastValue, 1).ToString() + ((ScaleMode == ScaleModes.Absolute) ? "%" : ValuesSuffix), Font, sb, (DisplayLegends ? LegendSpacing + 5 : 4.0F), 2.0F);
            if (UseTwoValues) {
                using Brush sb2 = new SolidBrush(PenSecondGraph.Color);
                g.DrawString(_SecondIndex + Math.Round(LastSecondValue, 1).ToString() + ((ScaleMode == ScaleModes.Absolute) ? "%" : ValuesSuffix), Font, sb2, (DisplayLegends ? LegendSpacing + 5 : 4.0F), 15.0F);
            }
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

    private double CalcHighestValue() {
        _MaxValue = 0;
        for (int i = 0; i < _VisibleValues; i++) {
            if (_Values[i] > _MaxValue) _MaxValue = _Values[i];
            if (UseTwoValues && _ValuesSecond[i] > _MaxValue) _MaxValue = _ValuesSecond[i];
        }
        return _MaxValue;
    }
    private int CalcVerticalPosition(double value) {
        double result = 0;

        if (ScaleMode == ScaleModes.Absolute) {
            result = value * (Height - 4) / 100;
        } else if (ScaleMode == ScaleModes.Relative) {
            result = (_MaxValue > 0) ? (value * (Height - 4) / _MaxValue) : 0;
        } else if (ScaleMode == ScaleModes.Strict) {
            result = (_MaxStrictValue > 0) ? (value * (Height - 4) / _MaxStrictValue) : 0;
        }

        result = Math.Round((Height - 2) - result, 0);
        if (result >= (Height - 3)) result = Height - 3;
        return Convert.ToInt32(result);
    }
    private double CalcMaxLegendValue() {
        if (ScaleMode == ScaleModes.Strict) return _MaxStrictValue;
        if (ScaleMode == ScaleModes.Relative) return (_MaxValue > 999) ? _MaxValue / 1024 : _MaxValue;
        return 100;
    }
    private string CalcLegendSuffix() {
        if (ScaleMode == ScaleModes.Absolute) return "%";
        if (ScaleMode == ScaleModes.Strict) return ValuesSuffix.Trim();
        if (ScaleMode == ScaleModes.Relative) return (_MaxValue > 999) ? ValuesSuffix.Trim().Replace("K", "M") : ValuesSuffix.Trim();
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