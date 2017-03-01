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
            using (StreamWriter outputFile = File.CreateText("latestList.txt"))
            {
                sList.PrintList(outputFile);
            }
            // not using because of the using statuement above.
            //outputFile.Close(); // closes the stream, allowing it to actually write.

        }

        static void OnNameChanged(object sender, NameChangedEventArgs args)
        {
            Console.WriteLine($"Shopping list name changing from \"{args.ExistingName}\" to \"{args.NewName}\"");
        }
    }
}
