namespace DungeonsOfDoom
{
    abstract class Monster : Character, IBackpack
    {
        public Monster(string name, int health)
        {
            Name = name;
            Health = health;
            MonsterCounter++;
        }

        public string Name { get; set; }

        public static int MonsterCounter { get; set; }

        public override int Health { get => base.Health; set { base.Health = value;
                if (value <= 0)
                {
                    MonsterCounter--;
                }
            } }

    }
}
