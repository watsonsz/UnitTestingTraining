using Shouldly;
using ZombieSurvivor.Application.Classes;

namespace ZombieSurvivor.Tests
{
    public class SurvivorTests
    {
        [Fact]
        public void ActionsTaken_GivenAction_ActionsTakenIncreases()
        {
            var survivor = new Survivor();
            survivor.ActionsTaken.ShouldBeEquivalentTo(0);
            survivor.TakeAction(Application.Contracts.ISurvivor.ActionTypes.Attack);
            survivor.ActionsTaken.ShouldBeGreaterThan(0);
        }
        [Fact] 
        public void ActionsTaken_GivenFourActions_ActionsTakenShouldRemainAtThree()
        {
            var survivor = new Survivor();
            survivor.ActionsTaken.ShouldBeEquivalentTo(0);
            survivor.TakeAction(Application.Contracts.ISurvivor.ActionTypes.Attack);
            survivor.TakeAction(Application.Contracts.ISurvivor.ActionTypes.Hide);
            survivor.TakeAction(Application.Contracts.ISurvivor.ActionTypes.Heal);
            survivor.TakeAction(Application.Contracts.ISurvivor.ActionTypes.Attack);
            survivor.ActionsTaken.ShouldBeEquivalentTo(3);
           
        }
        [Fact]
        public void IsDead_GivenTwoWounds_IsDeadSetToTrue()
        {
            var survivor = new Survivor();
            survivor.Wounds.ShouldBeEquivalentTo(0);
            survivor.isDead.ShouldBeFalse();
            survivor.Wounds = 2;
            survivor.isDead.ShouldBeTrue();
        }
    }
}