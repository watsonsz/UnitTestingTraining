using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieSurvivor.Application.Classes;

namespace ZombieSurvivor.Application.Contracts
{
    public interface ISurvivor
    {
        #region Properties
            public string Name { get; set; }
            public bool isDead { get; set; }
            public abstract int Wounds { get; set; }
            public abstract int ActionsTaken { get; set; }
            public List<Equipment> ReserveEquipment { get; set; }
            public abstract Equipment LeftHandEquipped { get; set; }
            public abstract Equipment RightHandEquipped { get; set; }
        #endregion

        public enum ActionTypes
                {
                    Attack,
                    Hide,
                    Heal
                }
        public enum Hands
        {
            LeftHand,
            RightHand
        }
        #region Methods
            public Task TakeAction(ActionTypes type);
            public Task EquipItem(Hands Hand, Equipment equipment);
            public Task PickUpItem(Equipment equipment);
            public Task DiscardItem(int ListIndex);
        #endregion


    }
    
}
