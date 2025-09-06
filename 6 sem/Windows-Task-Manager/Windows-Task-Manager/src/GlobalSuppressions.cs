// This file is used by Code Analysis to maintain SuppressMessage attributes that are applied to this project.
// Project-level suppressions either have no target or are given a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "I Hate It", Scope = "module")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Dont Want to", Scope = "type", Target = "~T:sMkTaskManager.Controls.sMkColumnSorter")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Dont Want to", Scope = "type", Target = "~T:sMkTaskManager.Controls.sMkColumnSorter")]
[assembly: SuppressMessage("Interoperability", "CA1401:P/Invokes should not be visible", Justification = "Dont Want to", Scope = "type", Target = "~T:sMkTaskManager.Controls.sMkListViewHelpers")]
[assembly: SuppressMessage("Interoperability", "SYSLIB1054:Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time", Justification = "<Pending>", Scope = "module")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Dont Want to", Scope = "member", Target = "~M:sMkTaskManager.Classes.TaskManagerUser.CanConnect~System.Boolean")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Dont Want to", Scope = "member", Target = "~M:sMkTaskManager.Classes.TaskManagerUser.CanLogOff~System.Boolean")]
[assembly: SuppressMessage("Design", "CA1069:Enums values should not be duplicated", Justification = "Yes!", Scope = "type", Target = "~T:sMkTaskManager.Classes.API")]
[assembly: SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "Dont Want to", Scope = "type", Target = "~T:sMkTaskManager.Classes.API.WINSTATIONINFORMATIONW")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Dont Want to", Scope = "type", Target = "~T:sMkTaskManager.Classes.ETW")]
[assembly: SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Dont Want to", Scope = "type", Target = "~T:sMkTaskManager.Classes.ETW")]
