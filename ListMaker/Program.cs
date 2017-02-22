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

            sList.AddToList("Goodie Bar");
            sList.AddToList("Mook");

            sList.PrintList();

        }
    }
}
