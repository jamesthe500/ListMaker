using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListMaker
{
    // delegate is its own .cs file. 
    // public delegate void NameChangedDelegate(string existingName, string newName);
    // this delegate can take any method that returns void and takes two strings

    // The above is unconventional, but legal. Convention is all delegates pass the name of the sender and an array of all params
    public delegate void NameChangedDelegate(object sender, NameChangedEventArgs args);

}
