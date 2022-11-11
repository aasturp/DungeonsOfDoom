using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    internal abstract class Character 
    {
        public virtual int Health { get; set; }
        public bool IsAlive { get { return Health > 0; } }
    }
}
