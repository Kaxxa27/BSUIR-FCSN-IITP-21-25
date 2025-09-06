using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace sMkTaskManager.Controls;

[DesignerCategory("Component"), SupportedOSPlatform("windows")]
public class sMkPerfMeter : UserControl {
    private string _strValue = "";
    private Int128 _curValue = 0;
    private readonly int _gridOffset = 10;
    private readonly int _barReserved = 32;
    private readonly StringFormat sf = new();
    private readonly ArrayList _HistoryValues = new();

    public sMkPerfMeter() {
        InitializeComponent();
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        sf.LineAlignment = StringAlignment.Far;
        sf.Alignment = StringAlignment.Center;
        LastValue = 0;
    }
    private void InitializeComponent() {
        SuspendLayout();
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Black;
        Size = new Size(75, 100);
        ResumeLayout(false);
    }

    public new Border3DStyle BorderStyle { get; set; } = Border3DStyle.Sunken;
    public Color BarForeColor { get; set; } = Color.Lime;
    public Color BarBackColor { get; set; } = Color.DarkGreen;
    public ScaleModes ScaleMode { get; set; } = ScaleModes.Absolute;
    public int HistoryValues { get; set; } = 10;
    public bool LightColors {
        get {
            return !(BackColor == Color.Black);
        }
        set {
            BackColor = value ? Color.LightGray : Color.Black;
            BorderStyle = (value ? Border3DStyle.Flat : Border3DStyle.Sunken);
        }
    }
    public Int128 LastValue { get; private set; }

    public void SetValue(Int128 Value, string strValue = "") {
        if (ScaleMode == ScaleModes.Absolute && Value > 100) Value = 100;
        if (ScaleMode == ScaleModes.Absolute && Value < 0) Value = 0;
        if (ScaleMode == ScaleModes.Relative) {
            while (_HistoryValues.Count > HistoryValues) { _HistoryValues.RemoveAt(0); }
            _HistoryValues.Add(Value);
            if (Value != 0) { Value = (100 * Value / GetMaxValue()); }
        }
        if (string.IsNullOrEmpty(strValue)) {
            strValue = Value.ToString() + ((ScaleMode == ScaleModes.Relative) ? "" : "%");
        }
        if (Value != _curValue || strValue != _strValue) {
            _strValue = strValue;
            _curValue = Value;
            LastValue = Value;
            Invalidate();
        }
    }
    public void SetValue(ulong Value, string strValue = "") {
        if (Value > long.MaxValue) Value = long.MaxValue;
        SetValue((long)Value, strValue);
    }

    private Int128 GetMaxValue() {
        Int128 maxValue = 0;
        foreach (Int128 i in _HistoryValues) {
            if (i > maxValue) maxValue = i;
        }
        return maxValue;
    }
    private void DrawChart(Graphics g) {
        Rectangle baseRectangle = new(0, 0, Width, Height);
        using (SolidBrush thisBrush = new(BackColor)) {
            g.FillRectangle(thisBrush, baseRectangle);
        }
        // Draw Filled graph
        int rows = 0;
        using (Pen thisPen = new(BarForeColor, 2)) {
            for (int i = _gridOffset; i <= (Height - _barReserved + _gridOffset); i += 3) {
                g.DrawLine(thisPen, _gridOffset, i, (Width - _gridOffset), i);
                rows += 1;
            }
        }
        // Calculate and draw Unused grid
        int posH = _gridOffset;
        using (Pen thisPen = new(LightColors ? Color.Gray : BarBackColor, 2)) {
            for (int i = 1; i <= Math.Min(rows, (int)(rows * (100 - _curValue) / 100)); i++) {
                g.DrawLine(thisPen, _gridOffset, posH, (Width - _gridOffset), posH);
                posH += 3;
            }
        }
        // Draw Middle Division
        using (Pen thisPen = new(BackColor, 1)) {
            g.DrawLine(thisPen, Convert.ToInt32(Width / 2.0), 0, Convert.ToInt32(Width / 2.0), Height);
        }
        // Draw Text
        using (Font thisFont = new(Font.FontFamily, 8)) {
            using SolidBrush thisBrush = new(LightColors ? Color.Black : BarForeColor);
            g.DrawString(_strValue, thisFont, thisBrush, new Rectangle(0, 0, Width, Height - 5), sf);
        }
        // Draw Border on top
        ControlPaint.DrawBorder3D(g, 0, 0, Width, Height, BorderStyle);
    }
    protected override void OnPaint(PaintEventArgs e) {
        base.OnPaint(e);
        DrawChart(e.Graphics);
    }

    public enum ScaleModes {
        Absolute,
        Relative
    }

}
