using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieSurvivor.Application.Contracts;
using ZombieSurvivor.Application.Exceptions;
using ZombieSurvivor.Application.Messages;

namespace ZombieSurvivor.Application.Classes
{
    public class Game
    {
        public Game()
        {
            Id = Guid.NewGuid();
            Survivors = new List<Survivor>();
            GameLevel = ISurvivor.Levels.Blue;
            GameHistory.Add($"Began Game at {DateTime.Now}");
            WeakReferenceMessenger.Default.Register<SurvivorMessage>(this, (r, m) =>
            {
                if (Survivors.FirstOrDefault(q => q.Id == m.SenderId) != null)
                {
                    GameHistory.Add(m.Message);
                }
                
            });
            WeakReferenceMessenger.Default.Register<SkillTreeMessage>(this, (r, m) =>
            {
                if (Survivors.FirstOrDefault(q => q.Id == m.SenderId) != null)
                {
                    var finalMessage = m.Message;
                    foreach(var skill in m.AvailableSkills)
                    {
                        finalMessage += $"\n {skill.Name}: {skill.Description}";
                    }
                    GameHistory.Add(finalMessage);
                }
            });
        }
        public Guid Id { get; set; }
        public ISurvivor.Levels GameLevel { get; set; }
        public bool GameOver { get; set; } = false;
        public List<Survivor> Survivors { get; }
        public List<string> GameHistory { get; set; } = new List<string>();
        public void AddSurvivor(Survivor survivor)
        {
            var isUnique = CheckForUnique(survivor);
            if (isUnique)
            {
                Survivors.Add(survivor);
                SubscribeSurvivorEvent(survivor);
                GameHistory.Add($"Survivor {survivor.Name} has been added to the Game!");
            };
            
        }



        #region PrivateMethods
        private bool CheckForUnique(Survivor survivor)
        {
            if (Survivors.Contains(survivor))
            {
                throw new NonUniqueName();
            }
            return true;
        }
        private void SubscribeSurvivorEvent(Survivor survivor)
        {
            survivor.HasDied += OnSurvivorKilled;
            survivor.LevelledUp += OnSurvivorLevelledUp;
        }
        private void OnSurvivorLevelledUp(object sender, EventArgs e)
        {
            var survivor = (Survivor)sender;
            GameHistory.Add($"Survivor {survivor.Name} has Levelled Up to {survivor.Level.ToString()}");
            CheckGameLevel();
        }

        private void CheckGameLevel()
        {
            var previousGameLevel = (int)GameLevel;
            GameLevel = Survivors.Where(q=> q.isDead == false).Max(x => x.Level);
            if((int)GameLevel > (int)previousGameLevel)
            {
                GameHistory.Add($"Game Level has increased to {GameLevel.ToString()}");
            }
        }

        private void OnSurvivorKilled(object sender, EventArgs e)
        {
            var deadSurvivor = sender as Survivor;
            GameHistory.Add($"Survivor {deadSurvivor.Name} has died");
            foreach(var survivor in Survivors)
            {
                if (!survivor.isDead)
                {
                    CheckGameLevel();
                    return;
                }
            }
            GameOver = true;
            GameHistory.Add($"Game has ended at {DateTime.Now}");
            throw new GameOver(GameHistory);
        }
       
        #endregion
    }
}
