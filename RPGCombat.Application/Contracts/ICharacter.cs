using RPGCombat.Application.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Application.Contracts
{
    public interface ICharacter: IEntity
    {
        public double Damage { get; set; }
        public double Healing { get; set; }
        public int MaxRange { get; set; }
        public Task<bool> IsInRange(int[] targetXYLocation);
        public Task<string> DealDamage(IEntity target);
        public Task<string> HealDamage(IEntity target);
        public double CalculateDamage(int targetLevel);
    }
}
