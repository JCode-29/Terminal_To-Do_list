using System.Data.SQLite;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Xml.Linq;

string dbPath = Path.Combine("SQL_saves", "To-Do_database.db");
Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

if (!File.Exists(dbPath))
{
    SQLiteConnection.CreateFile(dbPath);
}

using var connection = new SQLiteConnection($"Data Source={dbPath};Version=3");
connection.Open();

using var create_table = new SQLiteCommand(connection);
create_table.CommandText = 
@" CREATE TABLE IF NOT EXISTS ""To-Do_list""
(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Task TEXT NOT NULL,
    Done_mark VARCHAR(6) NOT NULL
)";
create_table.ExecuteNonQuery();

//from tutorial
static void conn()
{
    //Creating a path, folder and file
    string dbPath = Path.Combine("SQL_saves", "To-Do_database.db");
    Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

    if (!File.Exists(dbPath))
    {
        SQLiteConnection.CreateFile(dbPath);
    }

    //connecting database
    using var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;");
    conn.Open();
    
    //makes a table with this (id, name, grade, enrolled)
    using var cmd = new SQLiteCommand(conn);
    cmd.CommandText = @"
    CREATE TABLE IF NOT EXISTS Students 
    (
        Id       INTEGER PRIMARY KEY AUTOINCREMENT,
        Name     TEXT    NOT NULL,
        Grade    REAL    NOT NULL,
        Enrolled TEXT    NOT NULL
    )";

    //add some info to table
    using var insert = new SQLiteCommand(conn);
    insert.CommandText = "INSERT INTO Students (Name, Grade, Enrolled) VALUES (@name, @grade, @date)";
    insert.Parameters.AddWithValue("@name",  "Alice");
    insert.Parameters.AddWithValue("@grade", 92.5);
    insert.Parameters.AddWithValue("@date",  "2025-09-01");
    insert.ExecuteNonQuery();
    cmd.ExecuteNonQuery();

    insert.Prepare();
    insert.Parameters["@name"].Value  = "Bob";
    insert.Parameters["@grade"].Value = 88.0;
    insert.Parameters["@date"].Value  = "2025-09-02";
    insert.ExecuteNonQuery();

    //read the data
    using var select = new SQLiteCommand(conn);
    select.CommandText = "SELECT Id, Name, Grade FROM Students WHERE Grade > @min";
    select.Parameters.AddWithValue("@min", 80.0);

    //puts those read data to console
    using SQLiteDataReader reader = select.ExecuteReader();
    while (reader.Read())
    {
        int    id    = reader.GetInt32(0);
        string name  = reader.GetString(1);
        double grade = reader.GetDouble(2);
        Console.WriteLine($"{id}\t{name}\t{grade}");
    }

    //put a difrent data into existing line
    using var update = new SQLiteCommand(conn);
    update.CommandText = "UPDATE Students SET Grade = @grade WHERE Id = @id";
    update.Parameters.AddWithValue("@grade", 95.0);
    update.Parameters.AddWithValue("@id", 1);
    int rowsAffected = update.ExecuteNonQuery();

    //delete lines
    using var del = new SQLiteCommand(conn);
    del.CommandText = "DELETE FROM Students WHERE Id = @id";
    del.Parameters.AddWithValue("@id", 1);
    del.ExecuteNonQuery();
}
static void writeSomeLines() //it will write "-" thru whole size of terminal width
{
    for(int i = 0; i < Console.WindowWidth; i++)
    {
        Console.Write("-");
    }
    Console.WriteLine();
}

static void add(SQLiteConnection connection) //Put info to database
{
    string userText;
    
    writeSomeLines();
    Console.Write("type your goal: ");
    userText = Console.ReadLine()!;
    Console.WriteLine();

    try
    {
        using var insert = new SQLiteCommand(connection);
        insert.CommandText = @"INSERT INTO ""To-Do_list"" (Task, Done_mark) VALUES (@Task, @Done_mark)";
        insert.Parameters.AddWithValue("@Task",  userText);
        insert.Parameters.AddWithValue("@Done_mark", "undone");
        insert.ExecuteNonQuery();

        Console.WriteLine("Added successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("ERROR in add function", ex);
    }

}
static void list(SQLiteConnection connection)
{
    writeSomeLines();

    using var show = new SQLiteCommand(connection);
    show.CommandText = @"SELECT * FROM ""To-Do_list""";
    using var reader = show.ExecuteReader();

    while(reader.Read())
    {
        int id = reader.GetInt32(0);
        string text = reader.GetString(1);
        string done = reader.GetString(2);
        Console.WriteLine($"{id} {text} {(done == "done" ? "done" : "undone")}");
    }
}/*
static void remove(SQLiteConnection connection)
{
    writeSomeLines();
    //codeList();    
    
    Console.Write("\nWhat line you want to remove -> ");
    List <string> lines = new List<string>(File.ReadAllLines(path));
    try
    {
        lines.RemoveAt(Convert.ToByte(Console.ReadLine()));
        File.WriteAllLines(path, lines);
        Console.WriteLine("Text remove succesfuly");
    }
    catch
    {
        Console.WriteLine("ERROR in remove function");
    }

}

static void done()
{
    writeSomeLines();
    //codeList(); 

    Console.Write("\nWhat line you want to mark -> ");
    List <string> markers = new List<string>(File.ReadAllLines(path));

    try
    {
        int userInput = Convert.ToInt16(Console.ReadLine());

        if(markers[userInput].Contains("- undone") == true)
        {
            markers[userInput] = markers[userInput].Replace("- undone", "- done");
        }
        else
        {
            markers[userInput] = markers[userInput].Replace("- done", "- undone");
        }

        File.WriteAllLines(path, markers);
    }
    catch
    {
        Console.WriteLine("ERROR in done function");
    }
}
static void help()
{
    writeSomeLines();

    Console.WriteLine("\"Add\" - add something you write");
    Console.WriteLine("\"List\" - give you all info about your save text");
    Console.WriteLine("\"Remove\" - remove text base on your will");
    Console.WriteLine("\"Done\" - mark your selected text: unmark -> mark, mark -> unmark");
    Console.WriteLine("\"Help\" - you know what that command do :->");
    Console.WriteLine("\"Exit\" - you will close the program");
    Console.WriteLine("     - You can write the command with big or small leters");
}
static void exit()
{
    Environment.Exit(0); //immediately close terminal
}

//Give for user 1 command to get know all other commands
writeSomeLines();
Console.WriteLine("Type \"help\" for showing all commands.");

//checking, and meaby creating, folder and files
string? folder = Path.GetDirectoryName(path); //Create a folder if it is not here //string? -> this string might be null
if (!string.IsNullOrEmpty(folder))
{
    Directory.CreateDirectory(folder);
}

if (!File.Exists(path)) //after that it will create file
{
    using(File.Create(path)) {}
}

//conn();

//main loop 

while (true)
{
    writeSomeLines();

    switch (Console.ReadLine()!.ToLower())
    {
        case "add":
            add();
            break;

        case "list":
            list();
            break;

        case "remove":
            remove();
            break;

        case "done":
            done();
            break;

        case "help":
            help();
            break;

        case "exit":
            exit();
            break;

        default:
            Console.WriteLine("Incorrect command, try again.");
            break;
    }
}*/

add(connection);
list(connection);
//remove(connection);
list(connection);