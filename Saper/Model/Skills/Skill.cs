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
        private bool _isInstant = false;

        public bool SkillUsed { get => _skillUsed; protected set => _skillUsed = value; }
        public string Description { get => _description; protected set => _description = value; }
        public string SkillName { get => _skillName; protected set => _skillName = value; }
        public string SkillImage { get => _skillImage; protected set => _skillImage = value; }
        public bool IsInstant { get => _isInstant; protected set => _isInstant = value; }

        public virtual void CheckContext(SkillContext context)
        {
            if (context.target == null)
                throw new Exception("Target is empty");

            if (context.x == null)
                throw new Exception("x is empty");

            if (context.x == null)
                throw new Exception("y is empty");
        }

        public virtual void ActivateSkill(SkillContext context) 
        { 
            SkillUsed = true;
            CheckContext(context);
            ExecuteSkill(context);
        }

        protected abstract void ExecuteSkill(SkillContext context);
    }
}
