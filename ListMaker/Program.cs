using System;
using System.Collections.Generic;
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
            sList.NameChanged = new NameChangedDelegate(OnNameChanged); // Whenever somone invokes this delegate instance, call OnNameChanged
            sList._name = "Default list";
            Console.WriteLine("Name your shopping list.");
            sList.Name = Console.ReadLine();

            

            DecisionResult choosing = new DecisionResult();
            var choice = choosing.Ask();

            // Console.WriteLine($"You said {choice}"); // comes out as string value.

            

            while(choice != DecisionResult.DecisionCode.Exit)
            {
                switch (choice)
                {
                    case DecisionResult.DecisionCode.Add:
                        sList.AddToList();
                        Console.WriteLine("");
                        Console.WriteLine(sList.Name);
                        sList.PrintList();
                        Console.WriteLine("");
                        choice = choosing.Ask();
                        break;
                    case DecisionResult.DecisionCode.Subtract:
                        sList.RemoveFromList();
                        Console.WriteLine("");
                        Console.WriteLine(sList.Name); // Why won't it work to call this in the .PrintList() method?
                        sList.PrintList();
                        Console.WriteLine("");
                        choice = choosing.Ask();
                        break;
                }
            }

           
        }

        static void OnNameChanged(string existingName, string newName)
        {
            Console.WriteLine($"Shopping list name changing from \"{existingName}\" to \"{newName}\"");
        }
    }
}
