using RPGCombat.Application.Classes;
using RPGCombat.Application.Contracts;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Tests.Tests.CharacterTests
{
    public class CharacterAllyTests
    {
        public ICharacter _character;
        public ICharacter _enemy;
        public CharacterAllyTests()
        {
            _character = new MeleeCharacter() { XYLocation = [2, 1] };
            _enemy = new RangeCharacter() { XYLocation = [2, 3] };
        }

        [Fact]
        public void IsAlly_GivenMatchingFactions_IsAllyReturnsTrue()
        {
            _character.Factions.Add(new Faction() { Id = Guid.NewGuid(), Name = "Rohirrim" });
            var ally = new MeleeCharacter();
            ally.Factions.Add(_character.Factions[0]);
            ally.Factions.Add(new Faction { Id = Guid.NewGuid(), Name = "Red Hand" });
            bool isAlly = _character.IsAlly(ally.Factions).Result;

            isAlly.ShouldBeTrue();
        }
        [Fact]
        public void IsAlly_GivenNonMatchingFactions_IsAllyReturnsFalse()
        {
            _character.Factions.Add(new Faction() { Id = Guid.NewGuid(), Name = "Rohirrim" });
            var ally = new MeleeCharacter();
            ally.Factions.Add(new Faction { Id = Guid.NewGuid(), Name = "Red Hand" });
            bool isAlly = _character.IsAlly(ally.Factions).Result;

            isAlly.ShouldBeFalse();
        }

        [Fact]
        public void JoinFaction_GivenFactionNotMemberOf_FactionAddedToList()
        {
            var faction = new Faction() {Id = Guid.NewGuid(), Name = "The Mad Legion" };

            var otherFaction = new Faction() { Name = "Winged Hussars", Id = Guid.NewGuid() };

            _character.Factions.ShouldNotContain(faction);
            _character.JoinFaction(otherFaction);
            _character.JoinFaction(faction);
            _character.Factions.ShouldContain(faction);
        }

        [Fact]
        public void LeaveFaction_GivenFaction_FactionRemovedFromList()
        {
            var faction = new Faction() { Id = Guid.NewGuid(), Name = "The Mad Legion" };
            _character.JoinFaction(faction);

            _character.Factions.ShouldContain(faction);

            _character.LeaveFaction(faction);
            _character.Factions.ShouldNotContain(faction);
        }
    }


}
