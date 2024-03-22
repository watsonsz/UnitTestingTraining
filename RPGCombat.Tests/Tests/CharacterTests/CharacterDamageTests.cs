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
    public class CharacterDamageTests
    {
        private readonly ICharacter _character;
        private readonly ICharacter _enemy;

        public CharacterDamageTests()
        {
            this._character = new MeleeCharacter() { XYLocation = [2, 1] };
            _enemy = new RangeCharacter() { XYLocation = [2, 3] };
        }
        [Fact]
        public void DealDamage_GivenSelf_HealthUnaffected()
        {
            //Arrange
            var characterStartingHealth = _character.Health;
            //Act
            _character.DealDamage(_character);
            //Assert
            _character.Health.ShouldBe(characterStartingHealth);
            _character.IsAlive.ShouldBeTrue();
        }
        [Fact]
        public void DealDamage_GivenEnemy_LowersEnemyHealth()
        {
            //Arrange
            var enemyStartingHealth = _enemy.Health;
            
            _enemy.Factions.Add(new Faction() { Id= Guid.NewGuid(), Name = "Riders of Mordor" });
            _character.Factions.Add(new Faction() { Name = "Rohirrim", Id = Guid.NewGuid()});
            //Act
            _character.DealDamage(_enemy);
            //Assert
            _enemy.Health.ShouldBeLessThan(enemyStartingHealth);
            _enemy.IsAlive.ShouldBeTrue();
        }
        [Fact]
        public void DealDamage_GivenLowerLevelEnemy_DamageIncreased()
        {
            _character.Level = 5 ;
            var damage = _character.CalculateDamage(1);

            damage.ShouldBeGreaterThan(_character.Damage);
        }
        [Fact]
        public void DealDamage_GivenHigherLevelEnemy_DamageDecreased()
        {
            _character.Level = 1;
            var damage = _character.CalculateDamage(6);

            damage.ShouldBeLessThan(_character.Damage);
        }

        [Fact]
        public void DealDamage_GivenEnemyOutOfRange_EnemyHealthUnaffected()
        {
            _enemy.XYLocation = [20, 10];
            var enemyStartingHealth = _enemy.Health;
            _character.DealDamage(_enemy);
            _enemy.Health.ShouldBeEquivalentTo(enemyStartingHealth);
        }

        [Fact]
        public void DealDamage_GivenAllyInRange_AllyHealthUnaffected()
        {
            _character.Factions.Add(new Faction() { Id = Guid.NewGuid(), Name = "Rohirrim" });
            var ally = new MeleeCharacter() { XYLocation = [2, 3] };
            ally.Factions.Add(_character.Factions[0]);
            ally.Factions.Add(new Faction { Id = Guid.NewGuid(), Name = "Red Hand" });
            var allyStartingHealth = ally.Health;
            _character.DealDamage(ally);

            ally.Health.ShouldBeEquivalentTo(allyStartingHealth);
        }

    }
}
