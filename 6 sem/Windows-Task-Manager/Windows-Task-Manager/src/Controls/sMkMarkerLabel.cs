namespace sMkTaskManager.Controls;

[System.Runtime.Versioning.SupportedOSPlatform("windows")]
public class sMkMarkerLabel : Label {
    private bool _drawIcon = false;
    private readonly System.Timers.Timer tmrTimeout = new() { Enabled = false, Interval = 3000, AutoReset = false };

    public sMkMarkerLabel() : base() {
        ShowMark = true;
        MarkColor = Color.FromKnownColor(KnownColor.Highlight);
        Paint += OnPaint;
        tmrTimeout.Elapsed += OnTimerElapsed;
    }
    public bool ShowMark { get; set; }
    public Color MarkColor { get; set; }
    public new string Text {
        get { return base.Text; }
        set {
            if (value.Equals(Text)) {
                if (_drawIcon) { _drawIcon = false; tmrTimeout.Stop(); Invalidate(); }
            } else {
                _drawIcon = true;
                base.Text = value;
                tmrTimeout.Stop(); tmrTimeout.Start();
            }
        }
    }

    private void OnPaint(object? sender, PaintEventArgs e) {
        if (_drawIcon && Visible && ShowMark && Width > 10 && Height > 10) {
            e.Graphics.DrawLine(new Pen(MarkColor, 2), Width - 1, 6, Width - 1, Height - 5);
        }
    }
    private void OnTimerElapsed(object? sender, EventArgs e) {
        _drawIcon = false;
        Invalidate();
        tmrTimeout.Stop();
    }

}
