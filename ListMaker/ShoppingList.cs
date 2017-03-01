using System;
using System.Collections.Generic;
using System.IO;

namespace ListMaker
{
    public class ShoppingList
    {
        public ShoppingList() // this is a custom constructor that makes a new instance of list w/every instance of Shopping list
        {
            shoppingList = new List<string>();
        }

        public void AddToList()
        { 
            try
            {
                Console.WriteLine("");
                Console.WriteLine("Add an item to the list.");
                shoppingList.Add(Console.ReadLine());
            }
            catch (ArgumentNullException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        public void AddToList(string listItem) // this overload allows the application to make additions to the list.
        {
            shoppingList.Add(listItem);
        }

        public void RemoveFromList()
        {
            int itemNum = 10000;
            Console.WriteLine("");
            Console.WriteLine("Which number would you like removed?");
            try
            {
                itemNum = Convert.ToInt32(Console.ReadLine());
                itemNum--;
            }
            catch (FormatException)
            {
                Console.WriteLine("");
                Console.WriteLine("Please choose a number.");
                return;
            }

            try
            {
                shoppingList.RemoveAt(itemNum);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("");
                Console.WriteLine("Please choose a number from the list.");
                return;

            }
            
        }
        public void RemoveFromList(int itemNumber)
        {
            shoppingList.RemoveAt(itemNumber);
        }



        public int PrintList(TextWriter destination)
        {
            //Console.WriteLine(shoppingList.Name);
            for (int i = 0; i < shoppingList.Count; i++)
            {
                int listNum = i + 1; // i++ iterated the int, but i + 1 did not.
                destination.WriteLine($"{listNum}: {shoppingList[i]}");
            }

            return shoppingList.Count;
        }        

        public string Name
        {
            get
            {
                return _name; // This is where you could always convert the new property ToLower
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name cannot be null or empty");
                }

                if (_name != value)
                {
                    NameChangedEventArgs args = new NameChangedEventArgs();
                    args.ExistingName = _name;
                    args.NewName = value;

                    NameChanged(this, args);

                    _name = value;
                } 
            }
        }
        public event NameChangedDelegate NameChanged; // a public member of the delegate type is invoked so other parts of the code can pass assign it code that needs to be invoked elsewhere
        // changing it to an event precents assignments, which prevents wiping out subscribers. it needs to be left of a "+=" or "-="

        public string _name; // if you have a property that does stuff, you can't use the auto implement option,
        // so you have to make a field to hold the value outside of the property. convention is for "_" at the beginning of private fields.

        List<string> shoppingList = new List<string>();
    }
}