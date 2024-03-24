using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvivor.Application.Exceptions
{
    public class FullBag:Exception
    {
        public FullBag(string message = "Bag cannot carry anymore!"): base(message)
        {
           
        }
    }
}
