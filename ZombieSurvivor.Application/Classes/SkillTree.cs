using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using ZombieSurvivor.Application.Contracts;

namespace ZombieSurvivor.Application.Classes
{
    public class SkillTree
    {
        public List<Skill> YellowSkills { get; set; } = new List<Skill>
        {
            new Skill()
            {
                Name = "+1 Action",
                Description = "You can take an additional action on your turn",
                Id = 1,
            }
        };

        public List<Skill> OrangeSkillList { get; set; } = new List<Skill>
        {
            new Skill()
            {
                Name = "1 Free Move Action",
                Description = "You can take one move action per turn without counting it as one of your standard actions",
                Id = 2,
            }
        };

        public List<Skill> RedSkillList { get; set; } = new List<Skill>()
        {
            new Skill()
            {
                Name = "Hoard",
                Description = "You can carry an additional item in your bag",
                Id = 3,
            }
        };

        public List<Skill> GetAvailableSkills(ISurvivor.Levels level)
        {

            switch (level)
            {
                case ISurvivor.Levels.Yellow: return YellowSkills;
                case ISurvivor.Levels.Orange: return OrangeSkillList;
                case ISurvivor.Levels.Red: return RedSkillList;
                default: return new List<Skill>();
            }
            
            
           
        }


    }
}
