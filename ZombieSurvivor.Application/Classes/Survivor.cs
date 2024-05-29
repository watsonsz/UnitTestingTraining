﻿using CommunityToolkit.Mvvm.Messaging;
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
    public class Survivor
    {
        public Survivor()
        {
            ReserveEquipment = new List<Equipment>();
            _experience = 0;
            SkillTree = new SkillTree();
            Level = ISurvivor.Levels.Blue;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public const int MAX_ACTIONS_PER_TURN = 3;
        public const int MAX_WOUNDS = 2;
        public int Reserve_Equipment_Slots { get; set; } = 3;
        public int MoveActions { get; set; } = 0;
        
        public string Name { get; set; }
        public bool isDead { get; set; } = false;
        public int Experience { get => _experience; set
            {
                _experience += value;
                CheckForLevelUp(_experience);
            } }
        public int ActionsTaken { get; set; } = 0;
        public List<Equipment>? ReserveEquipment { get; set; }
        public Equipment? LeftHandEquipped { get; set; }
        public Equipment? RightHandEquipped { get; set; }
        public int Wounds { get => _wounds; 
            set 
            { 
                if(!isDead)
                {
                    if (_wounds < value)
                    {
                        ReduceEquipmentSlots();
                        
                    }
                    _wounds = value;
                    
                    if (_wounds >= MAX_WOUNDS)
                    {
                        isDead = true;
                        OnHasDied();
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(new SurvivorMessage(Id,$"{Name} was wounded: Wounds:{_wounds}"));
                    }
                }
            }
        }
        public ISurvivor.Levels Level { get; set; }
        public SkillTree SkillTree { get; set; }

        #region PrivateMembers
        private int _wounds = 0;
        private int _experience;
        private int _experienceThreshold = 6;
        #endregion

        #region Events
        public event EventHandler HasDied;
        public event EventHandler LevelledUp;
        protected virtual void OnHasDied()
        {
            HasDied?.Invoke(this, new EventArgs());
        }

        protected virtual void OnLevelledUp()
        {
            LevelledUp?.Invoke(this, new EventArgs());
        }
        #endregion
        

        #region Methods
        public void Move()
        {
            if(MoveActions != 0)
            {
                MoveActions--;
            }
            else
            {
                ActionsTaken++;
            }
        }

        public Task EquipItem(ISurvivor.Hands Hand, Equipment equipment)
        {

            if (ActionsTaken < MAX_ACTIONS_PER_TURN && !isDead)
            {
                if (Hand == ISurvivor.Hands.LeftHand)
                {
                    bool isHoldingEquipment = LeftHandEquipped != null;
                    if (isHoldingEquipment)
                    {
                        PickUpItem(LeftHandEquipped);
                        LeftHandEquipped = null;
                        LeftHandEquipped = equipment;
                        
                    }
                    else
                    {
                        LeftHandEquipped = equipment;
                    }

                }
                else
                {
                    bool isHoldingEquipment = RightHandEquipped != null;
                    if (isHoldingEquipment)
                    {
                        PickUpItem(RightHandEquipped);
                        RightHandEquipped = null;
                        RightHandEquipped = equipment;
                    }
                    else
                    {
                        RightHandEquipped = equipment;
                    }
                    
                }
                WeakReferenceMessenger.Default.Send(new SurvivorMessage(Id,$"{Name} Equipped {equipment.Name} to their {Hand}"));
                ReserveEquipment.Remove(equipment);
                ActionsTaken++;
                return Task.CompletedTask;
               
            }
            else
            {
                throw new MaximumNumberofActions();
            }
            return Task.CompletedTask;
            
        }

        public Task PickUpItem(Equipment equipment)
        {
            if (ActionsTaken < MAX_ACTIONS_PER_TURN && !isDead)
            {
                if (ReserveEquipment.Count >= Reserve_Equipment_Slots)
                {
                    throw new FullBag();
                }
                WeakReferenceMessenger.Default.Send(new SurvivorMessage(Id, $"{Name} Picked up {equipment.Name}"));
                ReserveEquipment.Add(equipment);
                ActionsTaken++;
            }
            else
            {
                throw new MaximumNumberofActions();
            }
            return Task.CompletedTask;
        }
        private void ReduceEquipmentSlots()
        {
            Reserve_Equipment_Slots -= 1;
            if (ReserveEquipment.Count > Reserve_Equipment_Slots)
            {
                ReserveEquipment.Remove(ReserveEquipment.First());
            }
        }

        public Task DiscardItem(int ListIndex)
        {
            ReserveEquipment.Remove(ReserveEquipment[ListIndex]);
            return Task.CompletedTask;
        }
        private void CheckForLevelUp(int experience)
        {

            if(experience >= _experienceThreshold && _experienceThreshold != 0)
            {
                while(experience > _experienceThreshold)
                {
                    _experienceThreshold = SetNewThreshold(_experienceThreshold);
                }
                if (this.Level != ISurvivor.Levels.Red && _experienceThreshold <= ((int)ISurvivor.Levels.Red))
                {
                    //Level Up
                    this.Level = (ISurvivor.Levels)Enum.ToObject(typeof(ISurvivor.Levels), _experienceThreshold);

                    OnLevelledUp();
                }
                else if (this.Level != ISurvivor.Levels.Red)
                {
                    this.Level = ISurvivor.Levels.Red;
                    OnLevelledUp();
                }
                UpdateSkillTree(_experienceThreshold);
                //Set New Threshold
                
                
            }
            
        }

        private void UpdateSkillTree(int experienceThreshold)
        {
            foreach (var pair in ISurvivor.levelValues)
            {
                if (pair.Key == experienceThreshold)
                {
                    if(pair.Value == ISurvivor.Levels.Orange || pair.Value == ISurvivor.Levels.Red)
                    {
                        var skills = SkillTree.GetAvailableSkills(pair.Value);
                        if(skills.Count > 0)
                        {
                            WeakReferenceMessenger.Default.Send(new SkillTreeMessage(this.Id, skills, $"{Name} has gained access to the following skills: "));
                            break;
                        }
                        else
                        {
                            WeakReferenceMessenger.Default.Send(new SurvivorMessage(this.Id, $"{Name} has no more available skills at the {pair.Value} tier"));
                            break;
                        }
                        
                    }
                    if(pair.Value == ISurvivor.Levels.Yellow && SkillTree.YellowSkill.Activated == false)
                    {
                        WeakReferenceMessenger.Default.Send(new SurvivorMessage(this.Id, $"{Name} has gained access to skill: ${SkillTree.YellowSkill.Name}: {SkillTree.YellowSkill.Description}"));
                    }
                    
                }
            }
        }

        public void ChooseSkill(int Id, ISurvivor.Levels level)
        {
            string skillName = string.Empty;
            if(level == ISurvivor.Levels.Yellow)
            {
                SkillTree.YellowSkill.Activated = true;
            }
            else if(level == ISurvivor.Levels.Orange)
            {
                SkillTree.OrangeSkillList.FirstOrDefault(p=> p.Id == Id).Activated = true;
                skillName = SkillTree.OrangeSkillList.FirstOrDefault(p => p.Id == Id).Name;
            }
            else if (level == ISurvivor.Levels.Red)
            {
                SkillTree.RedSkillList.FirstOrDefault(p => p.Id == Id).Activated = true;
                skillName = SkillTree.RedSkillList.FirstOrDefault(p => p.Id == Id).Name;
            }

            if(skillName != string.Empty)
            {
                WeakReferenceMessenger.Default.Send(new SurvivorMessage(this.Id, $"{Name} has activated {skillName}"));
            }
        }

        private int SetNewThreshold(int experienceThreshold)
        {
            foreach(var pair in ISurvivor.levelValues)
            {
                if(pair.Key > experienceThreshold)
                {
                    return pair.Key;
                }
            }
            return 0;
        }
        #endregion
    }
}
