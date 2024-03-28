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
        public SurvivorMessage(string value) : base(value)
        {
        }
    }
}
