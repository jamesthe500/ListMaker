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

            //sList.NameChanged += new NameChangedDelegate(OnNameChanged); // Whenever somone invokes this delegate instance, call OnNameChanged
            // code below is equivalent to that above. C# does the above behind the scenes.
            sList.NameChanged += OnNameChanged;
            sList.ExitingProgram += OnExit; // Pre-requisite, adds the subscriber to ExitingProgram

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


            DecisionResult choosing = new DecisionResult();
            var choice = choosing.Ask();

            // Console.WriteLine($"You said {choice}"); // comes out as string value.

            // Put the output in a streamwriter var called outputFile

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

            // not using because of the using statuement above.
            //outputFile.Close(); // closes the stream, allowing it to actually write.

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
