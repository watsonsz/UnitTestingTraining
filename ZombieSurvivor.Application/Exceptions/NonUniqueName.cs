using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvivor.Application.Exceptions
{
    public class NonUniqueName: Exception
    {
        public NonUniqueName(string message = "Survivors must have a unique Name"):base(message) { }
    }
}
