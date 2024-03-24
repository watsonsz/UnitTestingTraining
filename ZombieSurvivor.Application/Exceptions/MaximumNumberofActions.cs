using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvivor.Application.Exceptions
{
    public class MaximumNumberofActions:Exception
    {
        public MaximumNumberofActions(string message = "Cannot take another action this turn"):base(message)
        {
            
        }
    }
}
