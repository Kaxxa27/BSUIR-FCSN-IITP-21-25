namespace sMkTaskManager.Classes;

internal class TaskManagerValuesBase {

    public class TaskManagerMetricChangedEventArgs {
        public TaskManagerMetricChangedEventArgs(Metric metric, MetricChangedEventArgs e) {
            this.metric = metric;
            this.e = e;
        }
        public Metric metric { get; set; }
        public MetricChangedEventArgs e { get; set; }
    }

    public TaskManagerValuesBase() {
        // Attach the event handlers for all the Metrics discovered
        var properties = GetType().GetProperties().Where(p => p.PropertyType == typeof(Metric));
        foreach (var p in properties) {
            var metric = (Metric)p.GetValue(this)!;
            metric.AnyChanged += OnMetricChanged;
            metric.ValueChanged += OnMetricValueChanged;
            metric.DeltaChanged += OnMetricDeltaChanged;
        }
    }

    protected void OnMetricChanged(Metric sender, MetricChangedEventArgs e) { if (!CancellingEvents) MetricChanged?.Invoke(this, sender, e); }
    protected void OnMetricValueChanged(Metric sender, MetricChangedEventArgs e) { if (!CancellingEvents) MetricValueChanged?.Invoke(this, sender, e); }
    protected void OnMetricDeltaChanged(Metric sender, MetricChangedEventArgs e) { if (!CancellingEvents) MetricDeltaChanged?.Invoke(this, sender, e); }
    public event MetricEventHandler? MetricChanged;
    public event MetricEventHandler? MetricValueChanged;
    public event MetricEventHandler? MetricDeltaChanged;
    public delegate void MetricEventHandler(object sender, Metric metric, MetricChangedEventArgs e);

    public void SetValue(Metric m, Int128 newValue) => m.SetValue(newValue);
    public bool CancellingEvents = false;
    public long LastUpdate = 0;
}
