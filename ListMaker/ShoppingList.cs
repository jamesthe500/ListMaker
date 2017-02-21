using System.Collections.Generic;

namespace ListMaker
{
    class ShoppingList
    {
        public ShoppingList() // this is a custom constructor that makes a new instance of list w/every instance of Shopping list
        {
            shoppingList = new List<string>();
        }

        public void AddToList(string listItem)
        {
            shoppingList.Add(listItem);
        }

        List<string> shoppingList = new List<string>();
    }
}