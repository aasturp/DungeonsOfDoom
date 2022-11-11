using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    static internal class RandomUtils
    {
        static Random random = new Random();

        public static int Randomizer(int max, int min)
        {
            return random.Next(max, min);
        }
    }
}
