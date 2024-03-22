using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Application.Contracts
{
    public interface IBaseClass
    {
        public Guid Id { get; set; }
        public double Health { get; set; }
        public int[] XYLocation { get; set; }
    }
}
