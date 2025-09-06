using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace sMkTaskManager.Classes;

internal class Metric {
    private Int128 _Value, _Delta;
    private bool _isFirst = true;
    private readonly string _Name;

    public Metric(string name) { _Name = name; }
    public Metric(string name, MetricFormats format) { _Name = name; SetFormat(format); }
    public Metric(string name, string formatString) { _Name = name; FormatString = formatString; }

    protected virtual void OnValueChanged(MetricChangedEventArgs e) { AnyChanged?.Invoke(this, e); ValueChanged?.Invoke(this, e); }
    protected virtual void OnDeltaChanged(MetricChangedEventArgs e) { AnyChanged?.Invoke(this, e); DeltaChanged?.Invoke(this, e); }
    public event EventHandler? AnyChanged, ValueChanged, DeltaChanged;
    public delegate void EventHandler(Metric sender, MetricChangedEventArgs e);

    public Int128 Value => _Value;
    public Int128 Delta => _Delta;
    public string FormatString { get; set; } = "{0}";
    public MetricFormats Format { get; set; } = 0;
    public string ValueFmt => string.Format(FormatString, _Value / Divider);
    public string DeltaFmt => string.Format(FormatString, _Delta / Divider);
    public string FullString() => (Title == "") ? $"{Name}: {ValueFmt}" : $"{Title}: {ValueFmt}";
    public override string ToString() => _Value.ToString();
    public int Divider { get; set; } = 1;
    public string Title { get; set; } = "";
    public string Name => _Name;

    public void SetFormat(MetricFormats format) {
        Format = format;
        switch (format) {
            case MetricFormats.None: { FormatString = "{0}"; Divider = 1; break; }
            case MetricFormats.Numeric: { FormatString = "{0:#,0}"; Divider = 1; break; }
            case MetricFormats.Kb: { FormatString = "{0:#,0} Kb."; Divider = 1024; break; }
            case MetricFormats.Mb: { FormatString = "{0:#,0} Mb."; Divider = 1024 * 1024; break; }
            case MetricFormats.Gb: { FormatString = "{0:#,0} Gb."; Divider = 1024 * 1024 * 1024; break; }
        }
    }
    public void SetValue(Int128 newValue) {
        if (!_isFirst && !_Delta.Equals(newValue - _Value)) {
            MetricChangedEventArgs e = new(_Name, MetricValueTypes.Delta, _Delta, newValue - _Value);
            _Delta = newValue - _Value;
            OnDeltaChanged(e);
        }
        if (!_Value.Equals(newValue)) {
            MetricChangedEventArgs e = new(_Name, MetricValueTypes.Value, _Value, newValue);
            _Value = newValue;
            OnValueChanged(e);
        }
        _isFirst = false;
    }
    public void IncrementValue(Int128 value) {
        SetValue(_Value + value);
    }

}

internal enum MetricFormats {
    None = 0,
    Numeric = 1,
    Kb = 3,
    Mb = 4,
    Gb = 5,
}

internal enum MetricValueTypes {
    Value = 1,
    Delta = 2
}

internal class MetricChangedEventArgs : EventArgs {
    public MetricChangedEventArgs(string metricName, MetricValueTypes valueType, Int128 oldValue, Int128 newValue) {
        MetricName = metricName;
        ValueType = valueType;
        OldValue = oldValue;
        NewValue = newValue;
    }
    public string MetricName { get; set; }
    public MetricValueTypes ValueType { get; set; }
    public Int128 OldValue { get; set; }
    public Int128 NewValue { get; set; }
}

/* A new test implementation */
internal interface IMetric<T> {
    T Value { get; }
    T Delta { get; }
    string ValueFmt { get; }
    string DeltaFmt { get; }
    void SetValue(T newValue);
}

interface IMetricListener {
    void AnyChanged<Tvalue>(MetricNew<Tvalue> sender, MetricNewChangedEventArgs<Tvalue> e, [CallerMemberName] string PropertyName = "") where Tvalue : INumber<Tvalue>;
    void DeltaChanged<Tvalue>(MetricNew<Tvalue> sender, MetricNewChangedEventArgs<Tvalue> e, [CallerMemberName] string PropertyName = "") where Tvalue : INumber<Tvalue>;
    void ValueChanged<Tvalue>(MetricNew<Tvalue> sender, MetricNewChangedEventArgs<Tvalue> e, [CallerMemberName] string PropertyName = "") where Tvalue : INumber<Tvalue>;
}

internal class MetricNewChangedEventArgs<T> : EventArgs {
    public MetricNewChangedEventArgs(MetricValueTypes valueType, T oldValue, T newValue) {
        ValueType = valueType;
        OldValue = oldValue;
        NewValue = newValue;
    }
    public MetricValueTypes ValueType { get; set; }
    public T OldValue { get; set; }
    public T NewValue { get; set; }
}


internal class MetricNewString : IMetric<string> {
    private string _Value = "";

    protected virtual void OnValueChanged(MetricNewChangedEventArgs<string> e) { AnyChanged?.Invoke(this, e); ValueChanged?.Invoke(this, e); }
    protected virtual void OnDeltaChanged(MetricNewChangedEventArgs<string> e) { AnyChanged?.Invoke(this, e); DeltaChanged?.Invoke(this, e); }
    public event EventHandler? AnyChanged, ValueChanged, DeltaChanged;
    public delegate void EventHandler(MetricNewString sender, MetricNewChangedEventArgs<string> e);

    public string Value => _Value;
    public string Delta => _Value;
    public string ValueFmt => _Value;
    public string DeltaFmt => _Value;

    public void SetValue(string newValue) {
        if (!_Value.Equals(newValue)) {
            MetricNewChangedEventArgs<string> e = new(MetricValueTypes.Value, _Value, newValue);
            _Value = newValue;
            OnValueChanged(e);
        }
    }

}
internal class MetricNew<Tvalue> : IMetric<Tvalue> where Tvalue : INumber<Tvalue> {
    
    private Tvalue _Value = Tvalue.Zero;
    private Tvalue _Delta = Tvalue.Zero;
    private bool _isFirst = true;

    private readonly IMetricListener? _listener;

    public MetricNew(IMetricListener listener) {
        _listener = listener;
    }

    public MetricNew() {

    }

    public Tvalue Value => _Value;
    public Tvalue Delta => _Delta;
    public string FormatString { get; set; } = "{0}";
    public MetricFormats Format { get; set; } = 0;
    public string ValueFmt => string.Format(FormatString, _Value / Divider);
    public string DeltaFmt => string.Format(FormatString, _Delta / Divider);
    public Tvalue Divider { get; set; } = Tvalue.One;
    public override string ToString() => _Value.ToString()!;

    protected virtual void OnValueChanged(MetricNewChangedEventArgs<Tvalue> e) { AnyChanged?.Invoke(this, e); ValueChanged?.Invoke(this, e); }
    protected virtual void OnDeltaChanged(MetricNewChangedEventArgs<Tvalue> e) { AnyChanged?.Invoke(this, e); DeltaChanged?.Invoke(this, e); }
    public event EventHandler? AnyChanged, ValueChanged, DeltaChanged;
    public delegate void EventHandler(MetricNew<Tvalue> sender, MetricNewChangedEventArgs<Tvalue> e);

    public void SetFormat(MetricFormats format) {
        Format = format;
        switch (format) {
            case MetricFormats.None: { FormatString = "{0}"; Divider = Tvalue.One; break; }
            case MetricFormats.Numeric: { FormatString = "{0:#,0}"; Divider = Tvalue.One; break; }
            case MetricFormats.Kb: { FormatString = "{0:#,0} Kb."; Divider = Tvalue.CreateChecked(1024); break; }
            case MetricFormats.Mb: { FormatString = "{0:#,0} Mb."; Divider = Tvalue.CreateChecked(1024 * 1024); break; }
            case MetricFormats.Gb: { FormatString = "{0:#,0} Gb."; Divider = Tvalue.CreateChecked(1024 * 1024 * 1024); break; }
        }
    }
    public void SetValue(Tvalue newValue) {
        if (!_isFirst && !_Delta.Equals(newValue - _Value)) {
            MetricNewChangedEventArgs<Tvalue> e = new(MetricValueTypes.Delta, _Delta, newValue - _Value);
            _Delta = newValue - _Value;
            // OnDeltaChanged(e);
            _listener?.DeltaChanged(this, e);
        }
        if (!_Value.Equals(newValue)) {
            MetricNewChangedEventArgs<Tvalue> e = new(MetricValueTypes.Value, _Value, newValue);
            _Value = newValue;
            // OnValueChanged(e);
            _listener?.ValueChanged(this, e);
        }
        _isFirst = false;
    }
    public void IncrementValue(object value) {
        if (value.GetType().IsValueType && Information.IsNumeric(value)) {
            SetValue(_Value + Tvalue.CreateChecked(Convert.ToDouble(value)));
        }
    }

}

