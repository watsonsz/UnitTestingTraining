using RPGCombat.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Application.Classes
{
    public class MeleeCharacter:Character
    {
        public MeleeCharacter()
        {
            MaxRange = 2;
        }
    }

    public class RangeCharacter : Character
    {
        public RangeCharacter()
        {
            MaxRange = 20;
        }
    }
}
