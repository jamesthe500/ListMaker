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
            OpenAList();
            ShoppingList sList = new ShoppingList();

            GetListName(sList);
            PromtLoop(sList);
        }

        private static void OpenAList()
        {
            Console.WriteLine("Open (O) a list or create a new (N) one?");
            string response = Console.ReadLine().ToLower();
            switch (response)
            {
                case "open":
                    string[] availableFiles = Directory.GetFiles(@"c:\Created_Lists\");
                    // TODO present the available .csvs as an ordered list, prompt user to choose one, load that csv as the list to be used.
                    Console.WriteLine("opening");
                    break;
            }
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
