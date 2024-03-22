using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Application.Contracts
{
    public interface IEntity
    {
        public Guid Id { get; set; }
        public int Level { get; set; }
        public double Health { get; set; }
        public bool IsAlive { get; set; }
        public int[] XYLocation { get; set; }
    }
}
