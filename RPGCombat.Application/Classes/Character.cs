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

        public Task DealDamage(Entity target)
        {
            if(target.Id != this.Id && this.IsAlive)
            {
                target.Health -= CalculateDamage(target.Level);
                target.IsAlive = target.Health <= 0 ? false : true;
            }
            
            return Task.CompletedTask;
        }

        public Task HealDamage(Entity target)
        {
            if(target.IsAlive && target.Id == this.Id)
            {
                target.Health += 100;
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
