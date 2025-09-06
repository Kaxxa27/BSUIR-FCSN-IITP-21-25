namespace sMkTaskManager.Controls;

public class sMkListViewItem : ListViewItem {

    public int ID = 0;
    public bool Pinned = false;
    public bool ValuesChanged = false;
    public bool Freezed = false;
    public DateTime DateAddded = DateTime.Now;
    public long LastUpdated = DateTime.Now.Ticks;

}
