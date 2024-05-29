using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieSurvivor.Application.Classes;

namespace ZombieSurvivor.Application.Messages
{
    internal class SkillTreeMessage : SurvivorMessage
    {
        public SkillTreeMessage(Guid senderId,List<Skill> skills, string value) : base(senderId, value)
        {
            AvailableSkills = skills;
        }
        public List<Skill> AvailableSkills { get; set; }

    }
}
