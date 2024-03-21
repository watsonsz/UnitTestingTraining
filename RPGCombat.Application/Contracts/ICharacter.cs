using RPGCombat.Application.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Application.Contracts
{
    public interface ICharacter
    {
        public Task DealDamage(Entity target);
        public Task HealDamage(Entity target);
        public double CalculateDamage(int targetLevel);
    }
}
