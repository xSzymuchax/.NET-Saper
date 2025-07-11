using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Model.Skills
{
    public abstract class Skill
    {
        private bool _skillUsed = false;
        private string _description = "SkillDescription";
        private string _skillName = "SkillName";
        private string _skillImage = "SkillImage";

        public bool SkillUsed { get => _skillUsed; protected set => _skillUsed = value; }
        public string Description { get => _description; protected set => _description = value; }
        public string SkillName { get => _skillName; set => _skillName = value; }
        public string SkillImage { get => _skillImage; set => _skillImage = value; }

        public virtual void ActivateSkill(SkillContext context) 
        { 
            SkillUsed = true;
            ExecuteSkill(context);
        }

        protected abstract void ExecuteSkill(SkillContext context);
    }
}
