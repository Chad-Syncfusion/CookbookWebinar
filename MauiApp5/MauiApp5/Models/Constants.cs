using SQLite;

namespace MauiApp5.Models;


public abstract class Constants
{
    public const string DatabaseFilename = "todos.db3";

    public const SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
        
    public static string RecipePath = FileSystem.AppDataDirectory;
}