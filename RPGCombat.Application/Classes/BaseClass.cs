using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Application.Classes
{
    public abstract class BaseClass
    {
        public BaseClass()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public double Health { get; set; } = 1;
        public int[] XYLocation { get; set; }
    }
}
