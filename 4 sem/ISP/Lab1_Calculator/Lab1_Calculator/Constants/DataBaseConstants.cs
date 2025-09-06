
using System.Diagnostics;

namespace Lab1_Calculator.Constants;
public class DataBaseConstants
{
    public const string DatabaseFilename = "BrigadeSQLite.db";

    public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    //public static string DatabasePath =>
    //    Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    public static string DatabasePath
    {
        get
        {
            Debug.WriteLine(Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename));
            return Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
        }
    }


}
