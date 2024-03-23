using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieSurvivor.Application.Contracts;

namespace ZombieSurvivor.Application.Classes
{
    public class Survivor : ISurvivor
    {
        public const int MAX_ACTIONS_PER_TURN = 3;
        public const int MAX_WOUNDS = 2;
        
        public string Name { get; set; }
        public bool isDead { get; set; } = false;
        private int _wounds;
        public int Wounds { get => _wounds; 
            set 
            { 
                _wounds = value;
                if(_wounds >= MAX_WOUNDS)
                {
                    isDead = true;
                }
            }
        }
        public int ActionsTaken { get; set; } = 0;

        public Task TakeAction(ISurvivor.ActionTypes type)
        {
            if(ActionsTaken < MAX_ACTIONS_PER_TURN && !isDead)
            {
                //perform actions
                ActionsTaken++;
            }
            return Task.CompletedTask;
        }
    }
}
