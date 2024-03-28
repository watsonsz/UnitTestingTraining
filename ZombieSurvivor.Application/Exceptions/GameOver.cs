using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvivor.Application.Exceptions
{
    public class GameOver:Exception
    {
        public GameOver(List<string> history,string message = "All Survivors have Died: Game Over") : base(message)
        {
            History = history;
        }

        public List<string> History { get; set; }
    }
}
