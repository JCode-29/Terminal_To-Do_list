using System.ComponentModel;
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

    static List<Tasks> jsonList() //Pro ulechčení práce, přidej, pokud budeš používat více - čtení a nahrání do listu
    {
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
        return tasks;
    }

    public class Tasks()
    {
        public int id {get; set;}
        public string? task {get; set;} //"?" - toto znaménko znamená, že to může být null
        public bool done {get; set;}
    }


    static void add()
    {
        string userText;
        int nextID;
        
        writeSomeLines();
        Console.Write("type your goal: ");

        try
        {
            userText = Console.ReadLine()!;
            Console.WriteLine();

            List<Tasks> tasks = jsonList();

            if(tasks.Count > 0)
            {
                nextID = tasks[tasks.Count - 1].id + 1;
            }
            else
            {
                nextID = 0;
            }

            //without nextID, the first input will collaps
            tasks.Add(new Tasks {id = nextID, task = userText, done = false });
            File.WriteAllText(path, JsonSerializer.Serialize(tasks));

            Console.WriteLine("Task added successfuly.");
        }
        catch(Exception ex)
        {
            Console.WriteLine("ERROR in add function: " + ex);
        }

    }
    static void list()
    {
        writeSomeLines();

        List<Tasks> tasks = jsonList();

        if(tasks.Count == 0)
        {
            Console.WriteLine("Nothing to list");
            return;
        }

        for(int i = 0; i < tasks.Count; i++)
        {
            Console.WriteLine($"ID: {tasks[i].id} - Done: {tasks[i].done} - {tasks[i].task} ");
        }
    }
    static void remove()
    {
        writeSomeLines();

        List<Tasks> tasks = jsonList();
        if(tasks.Count == 0)
        {
            Console.WriteLine("There is nothing to remove");
            return;
        }

        list();
        writeSomeLines();
        
        Console.Write("\nWhat line you want to remove, chose with ID -> ");
        
        try
        {
            int userInput = Convert.ToInt32(Console.ReadLine());
            tasks.RemoveAll(t => t.id == userInput); //t => t.id == userInput - vymaže vše, co se rovná userInput. Něco jako for cyklus s if příkazem 
            File.WriteAllText(path, JsonSerializer.Serialize(tasks));
        }
        catch
        {
            Console.WriteLine("ERROR in remove function");
        }

    }
    static void done()
    {
        writeSomeLines();

        List<Tasks> tasks = jsonList();
        if(tasks.Count == 0)
        {
            Console.WriteLine("There is nothing to mark");
            return;
        }

        list();
        writeSomeLines();

        Console.Write("\nWhat line you want to mark, put ID here-> ");

        try
        {
            int userInput = Convert.ToInt32(Console.ReadLine());

            var markTask = tasks.Find(t => t.id == userInput)!;
            if(markTask.done == true)
            {
                markTask.done = false;
            }
            else
            {
                markTask.done = true;
            }

            File.WriteAllText(path, JsonSerializer.Serialize(tasks));
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
    
    static void Main()
    {
        writeSomeLines();
        Console.WriteLine("Type \"help\" for showing all commands.");

        File.Create(path).Close();

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
    }
}