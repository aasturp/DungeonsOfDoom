namespace DungeonsOfDoom
{
    class Player : Character
    {
        public Player(int health, int x, int y)
        {
            Health = health;
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public List<IBackpack> Backpack { get; } = new List<IBackpack>(); 
    }
}
