using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Application.Classes
{
    public class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
            Level = 1;
            Health = 1000;
            IsAlive = true;
        }

        public Guid Id { get; set; }
        public int Level { get; set; }
        public double Health { get; set; }
        public bool IsAlive { get; set; }
        public int[] Location { get; set; }
    }
}
