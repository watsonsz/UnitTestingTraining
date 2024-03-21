using RPGCombat.Application.Classes;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Tests.Tests.CharacterTests
{
    public class CharacterHealingTests
    {
        [Fact]
        public void CharacterHealsSelf_InvalidIfNotHealed()
        {
            //Arrange
            var character = new Character();
            var enemy = new Character();
            //Act
            enemy.DealDamage(character);
            var characterCurrentHealth = character.Health;
            //Assert
            character.HealDamage(character);
            character.Health.ShouldBeGreaterThan(characterCurrentHealth);
            character.IsAlive.ShouldBeTrue();
        }

        [Fact]
        public void CharacterHealsEnemy_InvalidIfHealed()
        {
            //Arrange
            var character = new Character();
            var enemy = new Character();
            var enemyStartingHealth = enemy.Health;
            //Act
            character.DealDamage(enemy);
            character.HealDamage(enemy);
            //Assert
            enemy.Health.ShouldBeLessThan(enemyStartingHealth);
            enemy.IsAlive.ShouldBeTrue();
        }
    }
}
