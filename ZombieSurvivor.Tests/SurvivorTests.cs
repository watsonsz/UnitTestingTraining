using Castle.DynamicProxy.Generators;
using Shouldly;
using ZombieSurvivor.Application.Classes;
using ZombieSurvivor.Application.Contracts;
using ZombieSurvivor.Application.Exceptions;

namespace ZombieSurvivor.Tests
{
    public class SurvivorTests
    {
        
        [Fact] 
        public void ActionsTaken_GivenFourActions_ActionsTakenShouldRemainAtThree()
        {
            var survivor = new Survivor();
            survivor.ActionsTaken.ShouldBeEquivalentTo(0);
            survivor.PickUpItem(new Equipment() { Name = "Shovel"});
            survivor.PickUpItem(new Equipment() { Name = "Rope" });
            survivor.PickUpItem(new Equipment() { Name = "Pickaxe" });
            Should.Throw<MaximumNumberofActions>(()=> survivor.PickUpItem(new Equipment() { Name = "Dinner Plate" }));
            
           
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

        [Fact]
        public void EquipItem_GivenItem_HandEquipedHasItem()
        {
            Survivor survivor = new Survivor();
            Equipment equipment = new Equipment() { Name = "Shovel" };
            survivor.ReserveEquipment.Add(equipment);

            survivor.LeftHandEquipped.ShouldBeNull();
            survivor.EquipItem(ISurvivor.Hands.LeftHand, equipment);
            
            survivor.LeftHandEquipped.ShouldNotBeNull();
        }
        [Fact]
        public void PickUpItem_GivenItem_ReserveEquipmentContainsItem()
        {
            Survivor survivor = new Survivor();
            Equipment equipment = new Equipment() { Name = "Shovel" };
            survivor.PickUpItem(equipment);
            survivor.ReserveEquipment.ShouldContain(equipment);
        }
        [Fact]
        public void PickUpItem_GivenItemAndFullBag_ShouldThrowFullBagException()
        {
            Survivor survivor = new Survivor()
            {
                ReserveEquipment = new List<Equipment>
                {
                    new Equipment()
                    {
                        Name = "Pickaxe"
                    },
                    new Equipment()
                    {
                        Name = "Sandwich"
                    },
                    new Equipment()
                    {
                        Name = "Rope"
                    }
                }
            };
            Equipment equipment = new Equipment() { Name = "Shovel" };
            Should.Throw<FullBag>(() => survivor.PickUpItem(equipment));
        }
        [Fact]
        public void DiscardItem_GivenIndexofItemtoDiscard_ItemRemovedFromReserveEquipment()
        {
            Survivor survivor = new Survivor()
            {
                ReserveEquipment = new List<Equipment>
                {
                    new Equipment()
                    {
                        Name = "Pickaxe"
                    },
                    new Equipment()
                    {
                        Name = "Sandwich"
                    },
                    new Equipment()
                    {
                        Name = "Rope"
                    }
                }
            };

            var equipment = survivor.ReserveEquipment[1];

            survivor.DiscardItem(1);
            survivor.ReserveEquipment.ShouldNotContain(equipment);
        }

        [Fact]
        public void LevelUp_GivenExperience_SurvivorLevelIncreased()
        {
            var survivor = new Survivor();
            survivor.Level.ShouldBe(ISurvivor.Levels.Blue);

            survivor.Experience = 9;
            survivor.Level.ShouldBe(ISurvivor.Levels.Yellow);

            survivor.Experience = 18;
            survivor.Level.ShouldBe(ISurvivor.Levels.Orange);

            survivor.Experience = 42;
            survivor.Level.ShouldBe(ISurvivor.Levels.Red);

        }
    }
}