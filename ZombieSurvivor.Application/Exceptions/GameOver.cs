using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvivor.Application.Exceptions
{
    public class GameOver:Exception
    {
        public GameOver(string message = "All Survivors have Died: Game Over"):base(message) { }
    }
}
