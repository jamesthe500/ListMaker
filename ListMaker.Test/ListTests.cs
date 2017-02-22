using System;
using ListMaker;
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

        
        }
    }
}
