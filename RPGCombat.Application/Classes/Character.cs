using RPGCombat.Application.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Application.Classes
{
    public class Character : Entity, ICharacter
    {
        public Character() { }
        public double DamageDealt { get; set; } = 100;
        public int MaxRange { get; set; }

        public double CalculateDamage(int targetLevel)
        {
            if(targetLevel >= this.Level + 5)
            {
                return DamageDealt * 0.5;
            }
            else if(targetLevel < this.Level + 5)
            {
                return DamageDealt * 2;
            }
            else
            {
                return DamageDealt;
            }
        }

        public Task<string> DealDamage(Entity target)
        {
            var totalDamage = CalculateDamage(target.Level);
            if(target.Id != this.Id && this.IsAlive)
            {
                target.Health -= totalDamage;
                target.IsAlive = target.Health <= 0 ? false : true;
                return Task.FromResult($"Dealt {totalDamage} to {nameof(target)}");
            }

            return Task.FromResult($"Unable to Damage {nameof(target)}");
        }

        public Task<string> HealDamage(Entity target)
        {
            if(target.IsAlive && target.Id == this.Id)
            {
                target.Health += 100;
                return Task.FromResult($" {nameof(target)} healed {100}");
            }
            return Task.FromResult($"Unable to Heal {nameof(target)}");
        }
    }
}
