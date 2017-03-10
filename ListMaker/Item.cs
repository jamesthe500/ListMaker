namespace ListMaker
{
    internal class Item
    {
        private string line;

         public Item(string line)
        {
            var split = line.Split(',');
            string itemInList = split[1].Trim();
            System.Console.WriteLine(itemInList);
        }
    }
}