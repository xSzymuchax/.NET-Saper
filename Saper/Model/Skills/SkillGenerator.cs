using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Model.Skills
{
    public static class SkillGenerator
    {
        public static Skill ReturnSkill(string skillName = "SaveClick")
        {
            switch (skillName)
            {
                case "SaveClick":
                    return new SaveClick();

            }

            // default skill
            return new SaveClick();
        }

        public static List<Skill> ReturnAllSkills()
        {
            List<Skill> result = new List<Skill>();
            
            result.Add(new SaveClick());

            return result;
        }
    }
}
