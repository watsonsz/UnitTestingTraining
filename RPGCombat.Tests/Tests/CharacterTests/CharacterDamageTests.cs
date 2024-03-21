using RPGCombat.Application.Classes;
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
        [Fact]
        public void CharacterDealsDamageToSelf_InvalidIfDamage()
        {
            //Arrange
            var character = new Character();
            var characterStartingHealth = character.Health;
            //Act
            character.DealDamage(character);
            //Assert
            character.Health.ShouldBe(characterStartingHealth);
            character.IsAlive.ShouldBeTrue();
        }
        [Fact]
        public void CharacterDealsDamageToEnemy_InvalidIfNoDamage()
        {
            //Arrange
            var character = new Character();
            var enemy = new Character();
            var enemyStartingHealth = enemy.Health;
            //Act
            character.DealDamage(enemy);
            //Assert
            enemy.Health.ShouldBeLessThan(enemyStartingHealth);
            enemy.IsAlive.ShouldBeTrue();
        }
        [Fact]
        public void CharacterDealsDamageToLowerLevelEnemy_InvalidIfDamageNotIncreased()
        {
            var character = new Character() { Level = 5 };
            var damage = character.CalculateDamage(1);

            damage.ShouldBeGreaterThan(character.DamageDealt);
        }
        [Fact]
        public void CharacterDealsDamageToHigherLevelEnemy_InvalidIfDamageNotDecreased()
        {
            var character = new Character() { Level = 1 };
            var damage = character.CalculateDamage(6);

            damage.ShouldBeLessThan(character.DamageDealt);
        }
    }
}
