using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Model.Skills
{
    public static class SkillGenerator
    {
        public static Skill ReturnSkill(string skillName = "Save Click")
        {
            switch (skillName)
            {
                case "Save Click":
                    return new SaveClick();
                case "No Skill":
                    return new NoSkill();
                case "Reveal Random Mines":
                    return new RevealRandomMinesSkill();
                case "Radar":
                    return new RadarSkill();
                default:
                    return new NoSkill();
            }
        }

        public static List<Skill> ReturnAllSkills()
        {
            List<Skill> result = new List<Skill>();

            result.Add(new NoSkill());
            result.Add(new SaveClick());
            result.Add(new RadarSkill());
            result.Add(new RevealRandomMinesSkill());

            return result;
        }
    }
}
