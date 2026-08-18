const string path = "Lists/to-do_list.txt";

static void writeSomeLines() //it will write "-" thru whole size of terminal width
{
    for(int i = 0; i < Console.WindowWidth; i++)
    {
        Console.Write("-");
    }
    Console.WriteLine();
}

static void add()
{
    string userText;
    
    writeSomeLines();
    Console.Write("type your goal: ");
    userText = Console.ReadLine();
    Console.WriteLine();

    try
    {
        File.AppendAllText(path, Environment.NewLine + userText);
        Console.WriteLine("Added successfully!");
    }
    catch
    {
        Console.WriteLine("ERROR in add function");
    }

}
static void list()
{
    writeSomeLines();

    for(int i = 0; i < File.ReadAllLines(path).Length; i++)
    {
        Console.WriteLine(File.ReadAllLines(path)[i]);
    }
}
static void remove()
{
    writeSomeLines();

    for(int i = 0; i < File.ReadAllLines(path).Length; i++)
    {
        Console.WriteLine(i + " - " + File.ReadAllLines(path)[i]);
    }
    
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
static void help()
{
    writeSomeLines();

    Console.WriteLine("\"Add\" - add something you write");
    Console.WriteLine("\"List\" - give you all info about your save text");
    Console.WriteLine("\"Remove\" - remove text base on your will");
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
}