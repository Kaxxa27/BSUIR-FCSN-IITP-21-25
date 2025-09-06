using System.Runtime.CompilerServices;
using sMkTaskManager.Classes;
using sMkTaskManager.Controls;
namespace sMkTaskManager.Forms;

public interface ITaskManagerTab {
    public abstract event EventHandler? ForceRefreshClicked;
    public abstract event EventHandler? RefreshStarts;
    public abstract event EventHandler? RefreshComplete;

    public sMkListView? ListView { get => null; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Image? Icon { get => null; }
    public string TimmingKey { get => string.Empty; }
    public long TimmingValue { get => 0; }
    public bool CanSelectColumns { get => false; }
    public TaskManagerColumnTypes ColumnType { get => TaskManagerColumnTypes.None; }

    public void Refresher(bool firstTime = false);
    public void ForceRefresh() { }
    public void LoadSettings() { }
    public bool SaveSettings() { return true; }
    public void ApplySettings() { }
    public void SetColumns(in ListView.ListViewItemCollection colItems) { }
    public ListView.ColumnHeaderCollection? GetColumns() { return null; }

}

public abstract class TaskManagerTab : UserControl, ITaskManagerTab {
    public virtual string Title { get; set; }
    public virtual string Description { get; set; }

    public abstract event EventHandler? ForceRefreshClicked;
    public abstract event EventHandler? RefreshStarts;
    public abstract event EventHandler? RefreshComplete;

    public virtual void Refresher(bool firstTime = false) { }
}