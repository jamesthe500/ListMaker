using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListMaker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            ShoppingList sList = new ShoppingList();

            List<string> oldList = OpenAList();
            sList.importList(oldList);

            GetListName(sList);
            PromtLoop(sList);
        }

        private static List<string> OpenAList()
        {
            Console.WriteLine("Open (O) a list or create a new (N) one?");
            List<string> archiveList = new List<string>();
            string response = Console.ReadLine().ToLower();
            switch (response)
            {
                case "open":
                    archiveList = openCSVOptions();
                    return archiveList;
                case "o":
                    archiveList = openCSVOptions();
                    return archiveList;
                default :
                    return archiveList;
               
            }
        }

        private static List<string> openCSVOptions()
        {
            List<string> archivedList = new List<string>();
            string[] availableFiles = Directory.GetFiles(@"c:\Created_Lists\");

            foreach (var listFile in availableFiles)
            {
                bool isCSV = listFile.ToLower().EndsWith(".csv");
                if (isCSV)
                {
                    int first = listFile.LastIndexOf(@"\") + 1;
                    int last = listFile.ToLower().IndexOf(".csv");

                    string justCSVFile = listFile.Substring(first, last - first);

                    Console.WriteLine(justCSVFile);
                }
            }           

            // TODO need to understand this line better.
            //object chosenFile = File.ReadLines(@"c:\Created_Lists\" + fileChoice + ".csv").Select(line => new Item(line)).ToList();

            // TODO NEXT put a try/catch here from null entry (create new/exit?) wrong text entered, others?
            IEnumerable<string> chosenFile = null;
            int tries = 3;
            bool success = false;
            while (!success && tries > 0)
            {
                tries--;
                try
                {
                    Console.WriteLine("");
                    Console.WriteLine("Which one?");
                    string fileChoice = Console.ReadLine();
                    chosenFile = File.ReadLines(@"c:\Created_Lists\" + fileChoice + ".csv");
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("");
                    Console.WriteLine("You've got to put something in. Care to try again? <y/n>");
                    string userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "y" || userResponse == "yes")
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }

                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("");
                    Console.WriteLine("That wasn't on the list. Care to try again? <y/n>");
                    string userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "y" || userResponse == "yes")
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }

                }
                
                success = true;
            }

            if (chosenFile == null)
            {
                return archivedList;
            }

            // if a list is loaded, it will find the text in the second cell of each row and add it to the list
            foreach (var item in chosenFile)
            {
                int last = item.LastIndexOf(@",");
                int first = item.IndexOf(", ") + 2;

                string justItem = item.Substring(first, last - first);
                archivedList.Add(justItem);
            }
            return archivedList;
            
        }


        private static void PromtLoop(ShoppingList sList)
        {
            sList.ExitingProgram += OnExit; // Pre-requisite, adds the subscriber to ExitingProgram

            DecisionResult choosing = new DecisionResult();
            var choice = choosing.Ask();

            while (choice != DecisionResult.DecisionCode.Exit)
            {
                switch (choice)
                {
                    case DecisionResult.DecisionCode.Add:
                        sList.AddToList();
                        Console.WriteLine("");
                        Console.WriteLine(sList.Name);
                        sList.PrintList(Console.Out);
                        Console.WriteLine("");
                        choice = choosing.Ask();
                        break;
                    case DecisionResult.DecisionCode.Subtract:
                        sList.RemoveFromList();
                        Console.WriteLine("");
                        Console.WriteLine(sList.Name); // Why won't it work to call this in the .PrintList() method?
                        sList.PrintList(Console.Out);
                        Console.WriteLine("");
                        choice = choosing.Ask();
                        break;
                }
            }
            sList.ExitNow(); // 1. This is the trigger of the event, goto ShoppingList
        }

        private static void GetListName(ShoppingList sList)
        {
            //sList.NameChanged += new NameChangedDelegate(OnNameChanged); // Whenever somone invokes this delegate instance, call OnNameChanged
            // code below is equivalent to that above. C# does the above behind the scenes.
            sList.NameChanged += OnNameChanged;

            sList._name = "Default list"; // sets the field, rather than the property which doesn't fire the event.

            try
            {
                Console.WriteLine("Name your shopping list.");
                sList.Name = Console.ReadLine();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void OnExit(object sender, ExitingEventArgs args) // here's where the args are packaged. not su
        {
            ShoppingList shopList = new ShoppingList();

            bool success = false;
            int tries = 3;
            while (!success && tries > 0)
            {
                try
                {
                    using (StreamWriter outputFile = File.CreateText(@"c:\Created_Lists\" + args.listName + "-List.txt"))
                    {
                        shopList.PrintList(outputFile, args.listContents); // Some troube calling as the list itself was hard to access
                    }

                    using (StreamWriter outputFile = File.CreateText(@"c:\Created_Lists\" + args.listName + "-List.csv"))
                    {
                        shopList.CSVList(outputFile, args.listContents);
                    }
                    success = true;
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine($"The file wasn't saved. There may be illegal characters in your list name \"{args.listName}\". Try another (y/n)?");
                    string userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "y" || userResponse == "yes")
                    {
                        Console.WriteLine("Enter the new name.");
                        args.listName = Console.ReadLine();
                    }
                    tries--;
                    if (tries <= 0)
                    {
                        Console.WriteLine("This didn't work. Sorry, closing without saving.");
                    }

                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"The file wasn't saved. There were illegal characters in your list name \"{args.listName}\". Try another (y/n)?");
                    string userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "y" || userResponse == "yes")
                    {
                        Console.WriteLine("Enter the new name.");
                        args.listName = Console.ReadLine();
                    }
                    tries--;
                    if (tries <= 0)
                    {
                        Console.WriteLine("This didn't work. Sorry, closing without saving.");
                    }

                }
                catch (NotSupportedException)
                {
                    Console.WriteLine($"The file wasn't saved. There may be illegal characters in your list name, probably at the beginning. \"{args.listName}\". Try another (y/n)?");
                    string userResponse = Console.ReadLine().ToLower();
                    if (userResponse == "y" || userResponse == "yes")
                    {
                        Console.WriteLine("Enter the new name.");
                        args.listName = Console.ReadLine();
                    }
                    tries--;
                    if (tries <= 0)
                    {
                        Console.WriteLine("This didn't work. Sorry, closing without saving.");
                    }

                }
            }
        }

        static void OnNameChanged(object sender, NameChangedEventArgs args)
        {
            Console.WriteLine($"Shopping list name changing from \"{args.ExistingName}\" to \"{args.NewName}\"");
        }
    }
}
