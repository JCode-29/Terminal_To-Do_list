// See https://aka.ms/new-console-template for more information
Console.WriteLine("Type \"help\" for showing all commands.");

const string path = "Lists/to-do_list.txt";

string? folder = Path.GetDirectoryName(path); //Create a folder if it is not here //string? -> this string might be null
if (!string.IsNullOrEmpty(folder))
{
    Directory.CreateDirectory(folder);
}

if (!File.Exists(path)) //after that it will create file
{
    using(File.Create(path)) {}
}

static void add()
{
    
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

while (true)
{
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