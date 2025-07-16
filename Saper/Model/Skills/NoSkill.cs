using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Model.Skills
{
    class NoSkill : Skill
    {
        public NoSkill()
        {
            Description = "This is no skill and has no effect on game.";
            SkillName = "No Skill";
            SkillImage = "/Resource/SkillImages/noskill.png";
            IsInstant = true;
        }

        protected override void ExecuteSkill(SkillContext? context)
        {
            return;
        }
    }
}
