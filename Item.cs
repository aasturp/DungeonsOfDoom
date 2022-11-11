namespace DungeonsOfDoom
{
    abstract class Item : IBackpack 
    {
        public Item(string name) 
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
