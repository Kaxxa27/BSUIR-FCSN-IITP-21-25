using System.Xml.Schema;

namespace sMkTaskManager.Classes;

internal class TaskManagerColumn {
    internal string Label;
    internal string Title;
    internal string Tag;
    internal string Group;
    internal int Width;
    internal HorizontalAlignment Align;
    internal bool Fixed;
    internal bool Default;
    internal int Index = 0;
    internal SortOrder SortOrder = SortOrder.None;

    public TaskManagerColumn(string label, string title, string tag, string group, int width, HorizontalAlignment align, bool isFixed, bool isDefault) {
        Label = label;
        Title = title;
        Tag = tag;
        Group = group;
        Width = width;
        Align = align;
        Fixed = isFixed;
        Default = isDefault;
    }
    public TaskManagerColumn(string label, string title, string tag, string group, int width, int align, int isFixed, int isDefault) {
        Label = label;
        Title = title;
        Tag = tag;
        Group = group;
        Width = width;
        Align = (HorizontalAlignment)align;
        Fixed = isFixed != 0;
        Default = isDefault != 0;
    }

    public TaskManagerColumn(string Chunk) {
        if (Chunk.Split(',').Length < 6) return;
        Chunk = Chunk.Trim('|').Trim();
        string[] ChunkValues = Chunk.Split(',');
        Title = ChunkValues[0];
        Tag = ChunkValues[1];
        Width = int.Parse(ChunkValues[2]);
        Align = (HorizontalAlignment)int.Parse(ChunkValues[3]);
        SortOrder = (SortOrder)int.Parse(ChunkValues[4]);
        Index = int.Parse(ChunkValues[5]);
    }
    public TaskManagerColumn(ColumnHeader lvCol) {
        Title = lvCol.Text;
        Tag = lvCol!.Tag!.ToString()!;
        Width = lvCol.Width;
        Align = lvCol.TextAlign;
        Index = lvCol.DisplayIndex;
    }

    public string GetChunk() {
        return $"|{Title},{Tag},{Width},{(int)Align},{(int)SortOrder},{Index}|";
    }

    public static List<TaskManagerColumn> GetColumnDefinition(TaskManagerColumnTypes type) {
        List<TaskManagerColumn> r = new();

        if (type == TaskManagerColumnTypes.Process) {
            r.AddRange(new TaskManagerColumn[] {
            new("Process ID (PID)", "PID", "PID", "General Information", 50, 0, 1, 1),
            new("Parent PID", "Parent", "ParentPID", "General Information", 50, 0, 0, 0),
            new("Process Name", "Name", "Name", "General Information", 130, 0, 0, 1),
            new("Description", "Description", "Description", "General Information", 150, 0, 0, 1),
            new("Base Priority", "Priority", "Priority", "General Information", 60, 0, 0, 1),
            new("Image Path", "Image Path", "ImagePath", "General Information", 150, 0, 0, 1),
            new("Command Line", "Command Line", "CommandLine", "General Information", 150, 0, 0, 0),
            new("Username", "Username", "Username", "General Information", 90, 0, 0, 1),
            new("Session ID", "Session", "SessionID", "General Information", 50, 2, 0, 0),
            new("CPU Affinity", "Affinity", "Affinity", "General Information", 50, 2, 0, 0),
            new("CPU Usage", "CPU", "CpuUsage", "General Information", 40, 2, 0, 1),
            new("Handles Count", "Handles", "Handles", "General Information", 55, 1, 0, 1),
            new("Threads Count", "Threads", "Threads", "General Information", 55, 1, 0, 1),
            new("GDI Objects", "GDI Objects", "GDIObjects", "General Information", 70, 1, 0, 0),
            new("GDI Objects Peak", "GDI Peak", "GDIObjectsPeak", "General Information", 65, 1, 0, 0),
            new("User Objects", "User Objects", "UserObjects", "General Information", 75, 1, 0, 0),
            new("User Objects Peak", "User Peak", "UserObjectsPeak", "General Information", 70, 1, 0, 0),
            new("Memory Usage", "Mem Usage", "WorkingSet", "Memory Related", 80, 1, 0, 1),
            new("Memory Usage Peak", "Mem Peak", "WorkingSetPeak", "Memory Related", 80, 1, 0, 1),
            new("Virtual Memory", "vMem Usage", "VirtualMemory", "Memory Related", 95, 1, 0, 1),
            new("Virtual Memory Peak", "vMem Peak", "VirtualMemoryPeak", "Memory Related", 90, 1, 0, 0),
            new("Virtual Private Bytes", "Private Bytes", "PrivateBytes", "Memory Related", 75, 1, 0, 0),
            new("Page Faults Count", "Page Faults", "PageFaults", "Memory Related", 90, 1, 0, 0),
            new("Paged Memory", "PF Memory", "PagedMemory", "Memory Related", 90, 1, 0, 1),
            new("Paged Memory Peak", "PF Peak", "PagedMemoryPeak", "Memory Related", 90, 1, 0, 0),
            new("Paged Pool", "Paged Pool", "PagedPool", "Memory Related", 90, 1, 0, 0),
            new("Paged Pool Peak", "Paged Pool Peak", "PagedPoolPeak", "Memory Related", 90, 1, 0, 0),
            new("Non Paged Pool", "Non Paged Pool", "NonPagedPool", "Memory Related", 90, 1, 0, 0),
            new("Non Paged Pool Peak", "Non Paged Pool Peak", "NonPagedPoolPeak", "Memory Related", 90, 1, 0, 0),
            new("Process Running Time", "Run Time", "RunTime", "Timming Information", 80, 1, 0, 1),
            new("Process Creation Time", "Start Time", "CreationTime", "Timming Information", 130, 1, 0, 0),
            new("CPU User Time", "User Time", "UserTime", "Timming Information", 65, 1, 0, 0),
            new("CPU Kernel Time", "Kernel Time", "KernelTime", "Timming Information", 70, 1, 0, 0),
            new("CPU Total Time", "CPU Time", "CpuTime", "Timming Information", 65, 1, 0, 0),
            new("Product Name", "Product", "ProductName", "Version Information", 70, 0, 0, 0),
            new("Product Version", "Version", "ProductVersion", "Version Information", 70, 0, 0, 0),
            new("Product Company", "Company", "ProductCompany", "Version Information", 70, 0, 0, 0),
            new("Product Language", "Language", "ProductLanguage", "Version Information", 70, 0, 0, 0),
            new("Read Operations", "ioReads", "ReadOperations", "I/O Counters", 90, 1, 0, 0),
            new("Read Operations Delta", "ioReads Delta", "ReadOperationsDelta", "I/O Counters", 90, 1, 0, 0),
            new("Read Bytes", "ioRead Bytes", "ReadTransfer", "I/O Counters", 90, 1, 0, 1),
            new("Read Bytes Delta", "ioRead BytesDelta", "ReadTransferDelta", "I/O Counters", 90, 1, 0, 0),
            new("Write Operations", "ioWrites", "WriteOperations", "I/O Counters", 90, 1, 0, 0),
            new("Write Operations Delta", "ioWrites Delta", "WriteOperationsDelta", "I/O Counters", 90, 1, 0, 0),
            new("Write Bytes", "ioWrites Bytes", "WriteTransfer", "I/O Counters", 90, 1, 0, 1),
            new("Write Bytes Delta", "ioWrites BytesDelta", "WriteTransferDelta", "I/O Counters", 90, 1, 0, 0),
            new("Other Operations", "ioOthers", "OtherOperations", "I/O Counters", 90, 1, 0, 0),
            new("Other Operations Delta", "ioOthers Delta", "OtherOperationsDelta", "I/O Counters", 90, 1, 0, 0),
            new("Other Bytes", "ioOthers Bytes", "OtherTransfer", "I/O Counters", 90, 1, 0, 0),
            new("Other Bytes Delta", "ioOthers BytesDelta", "OtherTransferDelta", "I/O Counters", 90, 1, 0, 0),
            new("Disk Read Bytes", "DiskRead", "DiskRead", "Disk & Network", 80, 1, 0, 0),
            new("Disk Read Delta", "DiskRead Delta", "DiskReadDelta", "Disk & Network", 80, 1, 0, 0),
            new("Disk Read Rate", "DiskRead Rate", "DiskReadRate", "Disk & Network", 80, 1, 0, 1),
            new("Disk Write Bytes", "DiskWrite", "DiskWrite", "Disk & Network", 80, 1, 0, 0),
            new("Disk Write Delta", "DiskWrite Delta", "DiskWriteDelta", "Disk & Network", 80, 1, 0, 0),
            new("Disk Write Rate", "DiskWrite Rate", "DiskWriteRate", "Disk & Network", 80, 1, 0, 1),
            new("Network Sent Bytes", "NetSent", "NetSent", "Disk & Network", 80, 1, 0, 0),
            new("Network Sent Delta", "NetSent Delta", "NetSentDelta", "Disk & Network", 80, 1, 0, 0),
            new("Network Sent Rate", "NetSent Rate", "NetSentRate", "Disk & Network", 80, 1, 0, 1),
            new("Network Received Bytes", "NetRcvd", "NetRcvd", "Disk & Network", 80, 1, 0, 0),
            new("Network Received Delta", "NetRcvd Delta", "NetRcvdDelta", "Disk & Network", 80, 1, 0, 0),
            new("Network Received Rate", "NetRcvd Rate", "NetRcvdRate", "Disk & Network", 80, 1, 0, 1),
            });
        } else if (type == TaskManagerColumnTypes.Services) {
            r.AddRange(new TaskManagerColumn[] {
            new("Name", "Name", "Name", "General Information", 180, 0, 1, 1),
            new("Status", "Status", "Status", "General Information", 70, 0, 0, 1),
            new("Process ID", "PID", "PID", "General Information", 50, 0, 0, 1),
            new("Description", "Description", "Description", "General Information", 150, 0, 0, 0),
            new("Startup Mode", "Startup", "Startup", "General Information", 70, 0, 0, 1),
            new("Internal Name", "Internal", "Ident", "General Information", 100, 0, 0, 1),
            new("Logon Account", "Logon As", "Logon", "General Information", 80, 0, 0, 1),
            new("Command Line", "Command Line", "CommandLine", "General Information", 200, 0, 0, 1),
            });
        } else if (type == TaskManagerColumnTypes.Connections) {
            r.AddRange(new TaskManagerColumn[] {
            new("Process ID", "PID", "PID", "General Information", 60, 0, 1, 1),
            new("Process Name", "Process", "ProcessName", "General Information", 80, 0, 0, 1),
            new("Protocol", "Proto", "Protocol", "General Information", 55, 0, 0, 1),
            new("Local Address", "Local Address", "LocalAddr", "General Information", 90, 0, 0, 1),
            new("Local Port", "Local Port", "LocalPort", "General Information", 60, 0, 0, 1),
            new("Remote Address", "Remote Address", "RemoteAddr", "General Information", 100, 0, 0, 1),
            new("Remote Port", "Remote Port", "RemotePort", "General Information", 60, 0, 0, 1),
            new("Connection State", "State", "StateString", "General Information", 75, 0, 0, 1),
            new("Connection LifeTime", "Life Time", "LifeTime", "General Information", 80, 1, 0, 0),
            new("Total Sent", "Sent", "Sent", "Transfer Information", 65, 1, 0, 1),
            new("Total Received", "Received", "Received", "Transfer Information", 65, 1, 0, 1),
            new("Sent Delta", "Sent Delta", "SentDelta", "Transfer Information", 80, 1, 0, 0),
            new("Received Delta", "Received Delta", "ReceivedDelta", "Transfer Information", 86, 1, 0, 0),
            new("Sent Rate", "Sent Rate", "SentRate", "Transfer Information", 80, 1, 0, 0),
            new("Received Rate", "Received Rate", "ReceivedRate", "Transfer Information", 86, 1, 0, 0),
            });
        } else if (type == TaskManagerColumnTypes.Ports) {
            r.AddRange(new TaskManagerColumn[] {
            new("Process ID", "PID", "PID", "General", 60, 0, 1, 1),
            new("Process Name", "Process", "ProcessName", "General", 100, 0, 0, 1),
            new("Protocol", "Proto", "Protocol", "General", 60, 0, 0, 1),
            new("Local Address", "Address", "LocalAddr", "General", 100, 0, 0, 1),
            new("Local Port", "Port", "LocalPort", "General", 60, 0, 0, 1),
            new("Port State", "State", "StateString", "General", 70, 0, 0, 1),
            });
        } else if (type == TaskManagerColumnTypes.Users) {
            r.AddRange(new TaskManagerColumn[] {
            new("Session ID", "ID", "ID", "General", 60, 0, 1, 1),
            new("Username", "Username", "Username", "General", 100, 0, 0, 1),
            new("Status", "Status", "Status", "General", 100, 0, 0, 1),
            new("Client Name", "Client Name", "Client", "General", 100, 0, 0, 1),
            new("Session Type", "Session", "Session", "General", 100, 0, 0, 1),
            new("Total Processes", "Processes", "TotalProcesses", "General", 70, 1, 0, 1),
            });
        } else if (type == TaskManagerColumnTypes.Nics) {
            r.AddRange(new TaskManagerColumn[] {
            new ("Adapter Name", "Name", "Name", "General", 120, 0, 1, 1),
            new ("Description", "Description", "Description", "General", 120, 0, 0, 0),
            new ("Speed", "Speed", "Speed", "General", 80, 2, 0, 1),
            new ("State", "State", "State", "General", 60, 2, 0, 1),
            new ("Type", "Type", "Type", "General", 90, 2, 0, 1),
            new ("Mac Address", "MacAddress", "MacAddress", "General", 90, 1, 0, 0),
            new ("Received Bytes", "Received", "Rcvd", "General", 80, 1, 0, 1),
            new ("Sent Bytes", "Sent", "Sent", "General", 80, 1, 0, 1),
            new ("Received Rate", "Rcvd Rate", "RcvdRate", "General", 80, 1, 0, 1),
            new ("Sent Rate", "Sent Rate", "SentRate", "General", 80, 1, 0, 1),
            new ("Received Delta", "Rcvd Delta", "RcvdDelta", "General", 80, 1, 0, 0),
            new ("Sent Delta", "Sent Delta", "SentDelta", "General", 80, 1, 0, 0),
            });
        } else if (type == TaskManagerColumnTypes.GPUs) {
            r.AddRange(new TaskManagerColumn[] {
            new ("Adapter Name", "Name", "Name", "General", 200, 0, 1, 1),
            new ("Nodes", "Nodes", "NodeCount", "General", 50, 1, 0, 1),
            new ("Segments", "Segments", "SegmentCount", "General", 65, 1, 0, 1),
            new ("Power Usage", "Power", "PowerUsage", "General", 65, 1, 0, 1),
            new ("Temperature", "Temperature", "Temperature", "General", 80, 1, 0, 1),
            new ("Fan Speed", "Fan Speed", "FanSpeed", "General", 80, 1, 0, 1),
            new ("Dedicated Memory Total", "Dedicated Total", "DedicatedMemoryTotal", "Memory", 100, 1, 0, 0),
            new ("Dedicated Memory Used", "Dedicated Used", "DedicatedMemoryUsed", "Memory", 100, 1, 0, 0),
            new ("Dedicated Memory Free", "Dedicated Free", "DedicatedMemoryFree", "Memory", 100, 1, 0, 0),
            new ("Dedicated Memory Usage %", "Dedicated Usage", "DedicatedMemoryUsage", "Memory", 100, 1, 0, 0),
            new ("Shared Memory Total", "Shared Total", "SharedMemoryTotal", "Memory", 100, 1, 0, 0),
            new ("Shared Memory Used", "Shared Used", "SharedMemoryUsed", "Memory", 100, 1, 0, 0),
            new ("Shared Memory Free", "Shared Free", "SharedMemoryFree", "Memory", 100, 1, 0, 0),
            new ("Shared Memory Usage %", "Shared Usage", "SharedMemoryUsage", "Memory", 100, 1, 0, 0),
            });
        }
        return r;
    }
    public static string GetDefaultColumnsChunks(TaskManagerColumnTypes Type) {
        System.Text.StringBuilder retValue = new();
        int newIndex = 0;
        foreach (TaskManagerColumn c in GetColumnDefinition(Type).Where(x => x.Default == true)) {
            c.Index = newIndex;
            retValue.AppendLine(c.GetChunk());
            newIndex++;
        }
        return retValue.ToString();
    }
}
public enum TaskManagerColumnTypes : int {
    None = 0,
    Process = 1,
    Services = 2,
    Connections = 3,
    Ports = 4,
    Users = 5,
    Applications = 6,
    Nics = 7,
    GPUs = 8,
}
