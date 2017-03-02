using System;
using System.Collections.Generic;

namespace ListMaker
{
    public class ExitingEventArgs : EventArgs
    {
        public string listName { get; set; }
        public List<string> listContents { get; set; }
        //public object method { get; set; }
    }
}