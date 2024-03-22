using RPGCombat.Application.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Application.Classes
{
    public abstract class Character:Entity, ICharacter
    {
        public Character()
        {
            Factions = new List<Faction>();
        }
        public double Damage { get; set; } = 100;
        public double Healing { get; set; } = 100;
        public int MaxRange { get; set; }
        public List<Faction> Factions { get; set; }

        public Task<string> DealDamage(IBaseClass target)
        {
            //set variables
            double totalDamage = this.Damage;
            bool isAlly = false;
            bool isSelf = target.Id == this.Id;
            //setup Characters or Items
            var isCharacter = target.GetType().GetInterfaces().Contains(typeof(ICharacter));

            if (isCharacter)
            {
                var currentTarget = target as ICharacter;
                totalDamage = CalculateDamage(currentTarget.Level);
                isAlly = IsAlly(currentTarget.Factions).Result;
            }
            
            var validTarget = ((!isSelf && !isAlly )&& this.IsAlive);

            if (validTarget && IsInRange(target.XYLocation).Result)
            {
                target.Health -= totalDamage;
                if (isCharacter) { 

                    ICharacter currentTarget = target as ICharacter;
                    currentTarget.IsAlive = target.Health <= 0 ? false : true;
                }
                
                return Task.FromResult($"Dealt {totalDamage} to {nameof(target)}");
            }

            return Task.FromResult($"Unable to Damage {nameof(target)}");
        }

        public Task<string> HealDamage(ICharacter target)
        {
            bool validTarget = target.IsAlive && (target.Id == this.Id || target.IsAlly(target.Factions).Result);
            if (validTarget)
            {
                target.Health += Healing;
                return Task.FromResult($" {nameof(target)} healed {Healing}");
            }
            return Task.FromResult($"Unable to Heal {nameof(target)}");
        }

        #region Validators
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

        public Task<bool> IsAlly(List<Faction> factions)
        {
            if (!factions.Any())
            {
                return Task.FromResult(false );
            }
            foreach (var faction in factions)
            {
                if (faction == this.Factions.FirstOrDefault(q => q.Id == faction.Id))
                {
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }

        public Task JoinFaction(Faction faction)
        {
            if (faction == this.Factions.FirstOrDefault(q => q.Id == faction.Id))
            {
               return Task.CompletedTask;
            }
            else
            {
                Factions.Add(faction);
                return Task.CompletedTask;
            }

        }

        public Task LeaveFaction(Faction faction)
        {
            if (faction == this.Factions.FirstOrDefault(q => q.Id == faction.Id))
            {
                Factions.Remove(faction);
                return Task.CompletedTask;
            }
            else
            {
                return Task.CompletedTask;
            }
        }

        #endregion
    }
}
