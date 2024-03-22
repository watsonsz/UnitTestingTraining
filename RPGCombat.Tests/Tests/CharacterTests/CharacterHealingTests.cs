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
    public class CharacterHealingTests
    {
        private readonly ICharacter _character;
        private readonly ICharacter _enemy;

        public CharacterHealingTests()
        {
            _character = new MeleeCharacter() { XYLocation = [2, 1] };
            _enemy = new RangeCharacter() { XYLocation = [2, 3] };
        }

        [Fact]
        public void CharacterHealsSelf_InvalidIfHealthNotIncreased()
        {
            //Act
            _enemy.DealDamage(_character);
            var characterCurrentHealth = _character.Health;
            //Assert
            _character.HealDamage(_character);

            _character.Health.ShouldBeGreaterThan(characterCurrentHealth);
            _character.IsAlive.ShouldBeTrue();
        }

        [Fact]
        public void CharacterHealsEnemy_InvalidIfEnemyHealthIncreased()
        {
            //Arrange
            var enemyStartingHealth = _enemy.Health;
            //Act
            _character.DealDamage(_enemy);
            _character.HealDamage(_enemy);
            //Assert
            _enemy.Health.ShouldBeLessThan(enemyStartingHealth);
            _enemy.IsAlive.ShouldBeTrue();
        }
    }
}
