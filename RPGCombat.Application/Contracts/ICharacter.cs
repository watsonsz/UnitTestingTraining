using RPGCombat.Application.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Application.Contracts
{
    public interface ICharacter: IBaseClass
    {
        public double Damage { get; set; }
        public double Healing { get; set; }
        public int MaxRange { get; set; }
        public int Level { get; set; }
        public bool IsAlive { get; set; }
        public List<Faction> Factions { get; set; }
        public Task<bool> IsInRange(int[] targetXYLocation);
        public Task<bool> IsAlly(List<Faction> factions);
        public Task<string> DealDamage(IBaseClass target);
        public Task<string> HealDamage(IBaseClass target);
        public double CalculateDamage(int targetLevel);
        public Task JoinFaction(Faction faction);
        public Task LeaveFaction(Faction faction);
    }
}
