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
        }
        public const int MAX_ACTIONS_PER_TURN = 3;
        public const int MAX_WOUNDS = 2;
        public int Reserve_Equipment_Slots { get; set; } = 3;
        
        public string Name { get; set; }
        public bool isDead { get; set; } = false;
        private int _wounds;
        public int Wounds { get => _wounds; 
            set 
            { 
                if(_wounds < value)
                {
                    ReduceEquipmentSlots();
                }
                _wounds = value;

                if(_wounds >= MAX_WOUNDS)
                {
                    isDead = true;
                }
            }
        }

       

        public int ActionsTaken { get; set; } = 0;
        public List<Equipment>? ReserveEquipment { get; set; }
        public Equipment? LeftHandEquipped { get; set; }
        public Equipment? RightHandEquipped { get; set; }

        #region Methods
        public Task TakeAction(ISurvivor.ActionTypes type)
        {
            if(ActionsTaken < MAX_ACTIONS_PER_TURN && !isDead)
            {
                //perform actions
                ActionsTaken++;
            }
            else
            {
                throw new MaximumNumberofActions();
            }

            return Task.CompletedTask;
        }

        public Task EquipItem(ISurvivor.Hands Hand, Equipment equipment)
        {
            if(Hand == ISurvivor.Hands.LeftHand)
            {
                bool isHoldingEquipment = LeftHandEquipped != null;
                if(isHoldingEquipment)
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
        }

        public Task PickUpItem(Equipment equipment)
        {
            if(ReserveEquipment.Count >= Reserve_Equipment_Slots)
            {
                throw new FullBag();
            }
            ReserveEquipment.Add(equipment);
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
        #endregion
    }
}
