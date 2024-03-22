using RPGCombat.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Application.Classes
{
    public class Character:Entity, ICharacter
    {
        public double Damage { get; set; } = 100;
        public double Healing { get; set; } = 100;
        public int MaxRange { get; set; }

        public double CalculateDamage(int targetLevel)
        {
            if (targetLevel >= this.Level + 5)
            {
                return Damage * 0.5;
            }
            else if (targetLevel < this.Level + 5)
            {
                return Damage * 2;
            }
            else
            {
                return Damage;
            }
        }

        public Task<string> DealDamage(IEntity target)
        {
            var totalDamage = CalculateDamage(target.Level);
            var validTarget = (target.Id != this.Id && this.IsAlive);
            if (validTarget && IsInRange(target.XYLocation).Result)
            {
                target.Health -= totalDamage;
                target.IsAlive = target.Health <= 0 ? false : true;
                return Task.FromResult($"Dealt {totalDamage} to {nameof(target)}");
            }

            return Task.FromResult($"Unable to Damage {nameof(target)}");
        }

        public Task<string> HealDamage(IEntity target)
        {
            bool validTarget = (target.IsAlive && target.Id == this.Id);
            if (validTarget)
            {
                target.Health += Healing;
                return Task.FromResult($" {nameof(target)} healed {Healing}");
            }
            return Task.FromResult($"Unable to Heal {nameof(target)}");
        }


        public Task<bool> IsInRange(int[] targetXYLocation)
        {
            var xLocationDifference = Math.Abs(targetXYLocation[0] - this.XYLocation[0]);
            var yLocationDifference = Math.Abs(targetXYLocation[1] - this.XYLocation[1]);
            if(xLocationDifference > MaxRange || yLocationDifference > MaxRange)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    }
}
