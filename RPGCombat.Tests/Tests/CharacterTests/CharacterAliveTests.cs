using RPGCombat.Application.Classes;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Tests.Tests.CharacterTests
{
    public class CharacterAliveTests
    {

        [Fact]
        public void CharacterKillsEnemy_InvalidIfAlive()
        {
            //Arrange
            var character = new Character() { DamageDealt = 1000 };
            var enemy = new Character();
            var enemyStartingHealth = enemy.Health;
            //Act
            character.DealDamage(enemy);
            //Assert
            enemy.Health.ShouldBeLessThan(enemyStartingHealth);
            enemy.IsAlive.ShouldBeFalse();
        }

        [Fact]
        public void CharacterTakesAnyActionWhileDead_InvalidIfHealedOrDamaged()
        {
            //Arrange
            var character = new Character() { IsAlive = false, Health = 0.0 };
            var enemy = new Character();
            var enemyStartingHealth = enemy.Health;
            //Act
            character.DealDamage(enemy);
            character.HealDamage(character);
            //Assert
            enemy.Health.ShouldBeEquivalentTo(enemyStartingHealth);
            character.Health.ShouldBeEquivalentTo(0.0);
            character.IsAlive.ShouldBeFalse();
        }
    }
}
