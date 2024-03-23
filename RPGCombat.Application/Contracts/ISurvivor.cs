using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Application.Contracts
{
    public interface ISurvivor
    {
        public const int MAX_ACTIONS_PER_TURN = 3;
        public string Name { get; set; }
        public bool isDead { get; set; }
        public abstract int Wounds { get; set; }
        public abstract int ActionsTaken { get; set; }
        public enum ActionTypes;
        public Task TakeAction(ActionTypes type);
        
    }
}
