using RPGCombat.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Application.Classes
{
    public abstract class BaseClass: IBaseClass
    {
        public BaseClass()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public int[] XYLocation { get; set; }
        public abstract double Health { get; set; }
    }
}
