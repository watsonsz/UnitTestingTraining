using RPGCombat.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Application.Classes
{
    public abstract class Entity : BaseClass, IEntity
    {
        public Entity()
        {
            Health = 1000;
        }
        public int Level { get; set; } = 1;
        public bool IsAlive { get; set; } = true;
        
    }
}
