using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieSurvivor.Application.Classes;
using ZombieSurvivor.Application.Contracts;
using ZombieSurvivor.Application.Exceptions;

namespace ZombieSurvivor.Tests
{
    public class GameTests
    {
        [Fact]
        public void AddSurvivor_GivenUniqueName_SurvivorAddedToList()
        {
            var game = new Game();
            var survivor = new Survivor() { Name = "Zack" };
            game.AddSurvivor(survivor);
            game.Survivors.ShouldContain(survivor);
        }

        [Fact]
        public void AddSurvivor_GivenNonUniqueName_ThrowsException()
        {
            var game = new Game();
            var survivor = new Survivor() { Name = "Zack" };
            game.AddSurvivor(survivor);
            Should.Throw<NonUniqueName>(() => game.AddSurvivor(survivor));

        }

        [Fact]
        public void OnSurvivorKilled_NotAllSurvivorsDead_GameIsNotOver()
        {
            var game = new Game();
            game.AddSurvivor(new Survivor() { Name = "Zack" });
            game.AddSurvivor(new Survivor() { Name = "Nani" });
            game.Survivors[0].Wounds = 2;

            game.Survivors[0].isDead.ShouldBeTrue();
            game.GameOver.ShouldBeFalse();

        }

        [Fact]
        public void OnSurvivorKilled_AllSurvivorsDead_GameIsOver()
        {
            var game = new Game();
            game.AddSurvivor(new Survivor() { Name = "Zack" });
            game.AddSurvivor(new Survivor() { Name = "Nani" });
            game.Survivors[0].Wounds = 2;
            game.Survivors[0].isDead.ShouldBeTrue();
            Should.Throw<GameOver>(() => game.Survivors[1].Wounds = 2);

        }

        [Fact]
        public void OnSurvivorLeveledUp_GivenSurvivorLevel_GameLevelMatchesSurvivor()
        {
            var game = new Game();
            var survivor = new Survivor();
            game.AddSurvivor(survivor);
            game.Survivors[0].Experience = 6;
            survivor.Level.ShouldBe(ISurvivor.Levels.Yellow);
            game.GameLevel.ShouldBe(ISurvivor.Levels.Yellow);
            
        }
        [Fact]
        public void OnSurvivorKilled_GivenLowerLevelSurvivors_GameLevelDecreases()
        {
            var game = new Game();
            var survivorOne = new Survivor();
            game.AddSurvivor(survivorOne);
            game.Survivors[0].Experience = 6;
            survivorOne.Level.ShouldBe(ISurvivor.Levels.Yellow);

            game.Survivors.Add(new Survivor());
            game.Survivors[1].Level.ShouldBe(ISurvivor.Levels.Blue);
            
            game.GameLevel.ShouldBe(ISurvivor.Levels.Yellow);
            game.Survivors[0].Wounds = 2;
            game.Survivors[0].isDead.ShouldBeTrue();
            game.GameLevel.ShouldBe(ISurvivor.Levels.Blue);
        }

        [Fact]
        public void GameHistory_GivenFiveEvents_GameHistoryCountIsFive()
        {
            try
            {
                var game = new Game();
                game.AddSurvivor(new Survivor() { Name = "Zack" });
                game.Survivors[0].PickUpItem(new Equipment() { Name = "Shovel" });
                game.Survivors[0].Experience = 6;
                game.Survivors[0].EquipItem(ISurvivor.Hands.LeftHand, game.Survivors[0].ReserveEquipment[0]);
                game.Survivors[0].Wounds = 1;
                game.Survivors[0].Wounds = 2;
            }
            catch(GameOver ex)
            {
                ex.History.Count.ShouldBe(9);
            }
            
        }
    }
}
