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
        public void CharacterDealsDamageToSelf_InvalidIfCharacterHealthDecreased()
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
        public void CharacterDealsDamageToEnemy_InvalidIfEnemyHealthNotDecreased()
        {
            //Arrange
            var enemyStartingHealth = _enemy.Health;
            //Act
            _character.DealDamage(_enemy);
            //Assert
            _enemy.Health.ShouldBeLessThan(enemyStartingHealth);
            _enemy.IsAlive.ShouldBeTrue();
        }
        [Fact]
        public void CharacterDealsDamageToLowerLevelEnemy_InvalidIfDamageDealtNotIncreased()
        {
            _character.Level = 5 ;
            var damage = _character.CalculateDamage(1);

            damage.ShouldBeGreaterThan(_character.Damage);
        }
        [Fact]
        public void CharacterDealsDamageToHigherLevelEnemy_InvalidIfDamageDealtNotDecreased()
        {
            _character.Level = 1;
            var damage = _character.CalculateDamage(6);

            damage.ShouldBeLessThan(_character.Damage);
        }

        [Fact]
        public void CharacterAttemptsDamageOnOutOfRangeEnemey_InvalidIfEnemyDamaged()
        {
            _enemy.XYLocation = [20, 10];
            var enemyStartingHealth = _enemy.Health;
            _character.DealDamage(_enemy);
            _enemy.Health.ShouldBeEquivalentTo(enemyStartingHealth);
        }
    }
}
