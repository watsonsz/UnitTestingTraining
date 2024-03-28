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
                    PickUp,
                    Equip
                }
        public enum Hands
        {
            LeftHand,
            RightHand
        }

        public enum Levels
        {
            Blue = 0,
            Yellow = 6,
            Orange = 18,
            Red = 42

        }
        #region Methods
            public Task EquipItem(Hands Hand, Equipment equipment);
            public Task PickUpItem(Equipment equipment);
            public Task DiscardItem(int ListIndex);
        #endregion


    }
    
}
