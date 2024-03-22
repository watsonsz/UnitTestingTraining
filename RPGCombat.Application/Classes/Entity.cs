using RPGCombat.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Application.Classes
{
    public class Entity : IEntity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public int Level { get; set; } = 1;
        public double Health { get; set; } = 1000;
        public bool IsAlive { get; set; } = true;
        public int[] XYLocation { get; set; }
    }
}
