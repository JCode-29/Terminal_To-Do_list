using System.Dynamic;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;

class Program
{
    static readonly string path = (Path.Combine("Lists", "To-Do_list.json"));

    static void writeSomeLines() //it will write "-" thru whole size of terminal width
    {
        for(int i = 0; i < Console.WindowWidth; i++)
        {
            Console.Write("-");
        }
        Console.WriteLine();
    }
/*
    static void jsonList() //Pro ulechčení práce, přidej, pokud budeš používat více - čtení a nahrání do listu
    {
        string json = File.ReadAllText(path);
        List<Tasks> readTasks = JsonSerializer.Deserialize<List<Tasks>>(json);
    }
*/
    public class Tasks()
    {
        public int id {get; set;}
        public string? task {get; set;} //"?" - toto znaménko znamená, že to může být null
        public bool done {get; set;}
    }


    static void add()
    {
        string userText;
        
        writeSomeLines();
        Console.Write("type your goal: ");

        try
        {
            userText = Console.ReadLine()!;
            Console.WriteLine();

            string json = File.ReadAllText(path);
            List<Tasks> tasks;

            if (!string.IsNullOrEmpty(json))
            {
                tasks = JsonSerializer.Deserialize<List<Tasks>>(json)!;
            }
            else
            {
                tasks = new List<Tasks>();
            }

            tasks.Add(new Tasks {id = tasks.Count, task = userText, done = false });
            File.WriteAllText(path, JsonSerializer.Serialize(tasks));
        }
        catch(Exception ex)
        {
            Console.WriteLine("ERROR in add function: " + ex);
        }
        //Přidává podle počtu, ne podle posledního ID. Opravit!!!

    }/*
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
        codeList();    
        
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
        codeList(); 

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

    
    static void Main()
    {
        if (File.Exists(path) == false)
        {
            File.WriteAllText(Path.Combine("Lists", "json_To-Do_list.json"), []);
        }

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
        }
    }*/
    static void Main()
    {
        add();
    }
}