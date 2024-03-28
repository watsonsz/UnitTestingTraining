using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieSurvivor.Application.Contracts;
using ZombieSurvivor.Application.Exceptions;

namespace ZombieSurvivor.Application.Classes
{
    public class Survivor : ISurvivor
    {
        public Survivor()
        {
            ReserveEquipment = new List<Equipment>();
            _experience = 0;
            Level = ISurvivor.Levels.Blue;
        }
        
        public const int MAX_ACTIONS_PER_TURN = 3;
        public const int MAX_WOUNDS = 2;
        public int Reserve_Equipment_Slots { get; set; } = 3;
        
        public string Name { get; set; }
        public bool isDead { get; set; } = false;
        public int Experience { get => _experience; set
            {
                _experience = value;
                CheckForLevelUp(_experience);
            } }

        

        public int Wounds { get => _wounds; 
            set 
            { 
                if(!isDead)
                {
                    if (_wounds < _wounds + value)
                    {
                        ReduceEquipmentSlots();
                    }
                    _wounds = value;

                    if (_wounds >= MAX_WOUNDS)
                    {
                        isDead = true;
                        OnHasDied();
                    }
                }
            }
        }
        public ISurvivor.Levels Level { get; set; }

        #region PrivateMembers
        private int _wounds;
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
        public int ActionsTaken { get; set; } = 0;
        public List<Equipment>? ReserveEquipment { get; set; }
        public Equipment? LeftHandEquipped { get; set; }
        public Equipment? RightHandEquipped { get; set; }

        #region Methods
        

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
                ReserveEquipment.Remove(equipment);
                return Task.CompletedTask;
                ActionsTaken++;
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

            if(experience >= _experienceThreshold)
            {
                //Level Up
                this.Level = (ISurvivor.Levels)Enum.ToObject(typeof(ISurvivor.Levels), _experienceThreshold);
                //Set New Threshold
                _experienceThreshold = SetNewThreshold(((int)this.Level));
                OnLevelledUp();
            }
            
        }

        private int SetNewThreshold(int level)
        {
            var values = Enum.GetValues(typeof(ISurvivor.Levels));
            foreach (var value in values)
            {
                if (level < (int)value)
                {
                    return (int)value;
                }
            }
            return 0;
        }
        #endregion
    }
}
