using System;

namespace DungeonsOfDoom
{
    class ConsoleGame
    {
        Room[,] world;
        Player player;

        public void Play()
        {
            Console.CursorVisible = false;
            CreatePlayer();
            CreateWorld();
            
            do
            {
                Console.Clear();
                DisplayWorld();
                DisplayStats();
                AskForMovement();
                Monster opponent = LookForEnemy();
                if (opponent != null)
                {
                    Attack(opponent);
                }
                LookForItem();
            } while (player.IsAlive && Monster.MonsterCounter > 0);

            GameOver();
        }

        public Monster LookForEnemy()
        {
            if (world[player.X, player.Y].MonsterInRoom != null)
            {
                return world[player.X, player.Y].MonsterInRoom;
            } 
            return null;
        }
        private void Attack(Monster opponent)
        {
            opponent.Health -= 10;

            if (opponent.IsAlive)
            {
                switch (opponent.Name)
                {
                    case "Orc":
                        player.Health -= 10;
                        break;
                    case "Skeleton":
                        if (player.Health > (opponent.Health * 2))
                            player.Health -= 1;
                        else
                             player.Health -= 5;
                        break;
                    default:
                        break;
                }
                
            } 
            else
            {
                player.Backpack.Add(opponent);
                world[player.X, player.Y].MonsterInRoom = null;
                
            }
        }

        private void LookForItem()
        {
            if (world[player.X, player.Y].ItemInRoom != null)
            {
                player.Backpack.Add(world[player.X, player.Y].ItemInRoom);
                world[player.X, player.Y].ItemInRoom = null;
            }
        }

        private void CreatePlayer()
        {
            player = new Player(30, 0, 0);
        }

        private void CreateWorld() 
        {
            world = new Room[20, 5]; 
            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                    world[x, y] = new Room();
                    int percentage = RandomUtils.Randomizer(1, 100);
                    if (percentage < 5)
                        world[x, y].ItemInRoom = new HealingPotion("HealingPotion");
                    else if (percentage < 10)
                    {
                        world[x, y].MonsterInRoom = new Skeleton("Skeleton", 5);
                        
                    }
                    else if (percentage < 20)
                        world[x, y].ItemInRoom = new Weapon("Sword");
                    else if (percentage < 30)
                    {
                        world[x, y].MonsterInRoom = new Orc("Orc", 20);
                        
                    }
                }
            }
        }

        private void DisplayWorld() 
        {
            for (int y = 0; y < world.GetLength(1); y++) 
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                    Room room = world[x, y];
                    if (player.X == x && player.Y == y)
                        Console.Write("P");
                    else if (room.MonsterInRoom != null)
                    {
                        if (room.MonsterInRoom.Name == "Skeleton")
                        {
                             Console.Write("S");
                             
                        }
                        else if (room.MonsterInRoom.Name == "Orc")
                        {
                            Console.Write("O");
                        }
                    }
                    else if (room.ItemInRoom != null)
                    {
                        if (room.ItemInRoom.Name == "Sword")
                        {
                            Console.Write("W");
                        }
                        else if (room.ItemInRoom.Name == "HealingPotion")
                        {
                            Console.Write("H");

                        }
                    }
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        private void DisplayStats()
        {
            Console.WriteLine($"Player health: {player.Health}");
            Console.WriteLine("Backpack: ");

            foreach (var item in player.Backpack)
            {
                Console.WriteLine($"{item.Name}");
            }

            Console.WriteLine($"Amount of monsters: {Monster.MonsterCounter}");
        }

        private void AskForMovement()
        {
            int newX = player.X;
            int newY = player.Y;
            bool isValidKey = true;

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.RightArrow: newX++; break; 
                case ConsoleKey.LeftArrow: newX--; break;
                case ConsoleKey.UpArrow: newY--; break;
                case ConsoleKey.DownArrow: newY++; break;
                default: isValidKey = false; break; 
            }

            if (isValidKey && 
                newX >= 0 && newX < world.GetLength(0) &&
                newY >= 0 && newY < world.GetLength(1))
            {
                player.X = newX;
                player.Y = newY;
            }
        }

        private void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Game over...");
            Console.ReadKey();
            Monster.MonsterCounter = 0;
            Play();
        }

    }
}
