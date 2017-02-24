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
            DecisionResult choosing = new DecisionResult();
            var choice = choosing.Ask();

            // Console.WriteLine($"You said {choice}"); // comes out as string value.

            ShoppingList sList = new ShoppingList();

            while(choice != DecisionResult.DecisionCode.Exit)
            {
                switch (choice)
                {
                    case DecisionResult.DecisionCode.Add:
                        sList.AddToList();
                        sList.PrintList();
                        Console.WriteLine("");
                        choice = choosing.Ask();
                        break;
                    case DecisionResult.DecisionCode.Subtract:
                        Console.WriteLine("Feature coming soon!");
                        Console.WriteLine("");
                        choice = choosing.Ask();
                        break;
                }
            }

           
        }
    }
}
