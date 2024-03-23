using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvivor.Application.Contracts
{
        public interface ISurvivor
        {
            public string Name { get; set; }
            public bool isDead { get; set; }
            public abstract int Wounds { get; set; }
            public abstract int ActionsTaken { get; set; }
            public enum ActionTypes
            {
                Attack,
                Hide,
                Heal
            }
            public Task TakeAction(ActionTypes type);

        }
    
}
