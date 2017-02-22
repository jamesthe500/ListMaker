using System.Collections.Generic;

namespace ListMaker
{
    public class ShoppingList
    {
        public ShoppingList() // this is a custom constructor that makes a new instance of list w/every instance of Shopping list
        {
            shoppingList = new List<string>();
        }

        public void AddToList(string listItem)
        {
            shoppingList.Add(listItem);
        }

        public int PrintList()
        {
            for (int i = 0; i < shoppingList.Count; i++)
            
            {
                int listNum = i + 1; // i++ iterated the int, but i + 1 did not.
                System.Console.WriteLine($"{listNum}: {shoppingList[i]}");
            }

            return shoppingList.Count;
        }

        List<string> shoppingList = new List<string>();
    }
}