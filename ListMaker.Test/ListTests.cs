using System;
using ListMaker;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ListMaker.Test
{
    [TestClass]
    public class ListTests
    {
        [TestMethod]
        public void RightListCount()
        {
            ShoppingList sList = new ShoppingList();

            sList.AddToList("Goodie Bar");
            sList.AddToList("Mook");
            int listCount = sList.PrintList();
            Assert.AreEqual(listCount, 2);

            sList.RemoveFromList(1);
            listCount = sList.PrintList();
            Assert.AreEqual(1, listCount);
        }

        [TestMethod]      
        public void NameChanges()
        {
            ShoppingList sList = new ShoppingList();

            sList._name = "Name A";
            sList.Name = "Name B";

            Assert.IsTrue(wasNameChanged);
            Assert.AreEqual("Name B", sList.Name);
        }

        private static bool wasNameChanged = false;

        static void WhenNameChanged(object sender, NameChangedEventArgs args)
        {
            ListTests.wasNameChanged = true;
        }
    }
}
