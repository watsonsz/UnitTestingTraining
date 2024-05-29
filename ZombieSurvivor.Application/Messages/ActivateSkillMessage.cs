using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvivor.Application.Messages
{
    public class ActivateSkillMessage : ValueChangedMessage<int>
    {
        public ActivateSkillMessage(Guid survivorId,int value) : base(value)
        {
            this.SurvivorId = survivorId;
            SkillId = value;
        }

        public Guid SurvivorId { get; set; }
        public int SkillId { get; set; }
    }
}
