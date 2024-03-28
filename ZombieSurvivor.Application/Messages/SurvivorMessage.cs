using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieSurvivor.Application.Messages
{
    public class SurvivorMessage : ValueChangedMessage<string>
    {
        public SurvivorMessage(Guid senderId, string value) : base(value)
        {
            SenderId = senderId;
            Message = value;
        }
        public Guid SenderId { get; set; }
        public string Message { get; set; }
    }
}
