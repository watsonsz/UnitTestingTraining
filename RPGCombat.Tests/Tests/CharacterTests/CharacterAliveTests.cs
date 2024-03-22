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
    public class CharacterAliveTests
    {
        public ICharacter _character;
        public ICharacter _enemy;
        public CharacterAliveTests()
        {
            _character = new MeleeCharacter() {XYLocation = [2,1] };
            _enemy = new RangeCharacter() { XYLocation = [2, 3] };
            
        }
        [Fact]
        public void CharacterKillsEnemy_InvalidIfEnemyStillAlive()
        {
            //Arrange
            _character.Damage = 1000;
            var enemyStartingHealth = _enemy.Health;
            //Act
            _character.DealDamage(_enemy);
            //Assert
            _enemy.Health.ShouldBeLessThan(enemyStartingHealth);
            _enemy.IsAlive.ShouldBeFalse();
        }

        [Fact]
        public void CharacterTakesAnyActionWhileDead_InvalidIfCharacterHealedOrDealtDamaged()
        {
            //Arrange
            _character.Health = 0;
            _character.IsAlive = false;
            var enemyStartingHealth = _enemy.Health;
            //Act
            _character.DealDamage(_enemy);
            _character.HealDamage(_character);
            //Assert
            _enemy.Health.ShouldBeEquivalentTo(enemyStartingHealth);
            _character.Health.ShouldBeEquivalentTo(0.0);
            _character.IsAlive.ShouldBeFalse();
        }
    }
}
