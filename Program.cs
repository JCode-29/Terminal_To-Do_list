const string path = "Lists/to-do_list.txt";


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

}
static void remove()
{

}
static void help()
{
    
}
static void exit()
{

}

static void writeSomeLines() //it will write "-" thru whole size of terminal width
{
    for(int i = 0; i < Console.WindowWidth; i++)
    {
        Console.Write("-");
    }
    Console.WriteLine();
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