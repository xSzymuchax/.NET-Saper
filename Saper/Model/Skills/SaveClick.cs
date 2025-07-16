using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Model.Skills
{
    public class SaveClick : Skill
    {
        
        public SaveClick()
        {
            Description = "Makes your next flip save, disarms the bomb.";
            SkillName = "Save Click";
            SkillImage = "/Resource/SkillImages/safeclick.png";
        }

        protected override void ExecuteSkill(SkillContext context)
        {
            if (context.target is GameboardModel gm &&
                context.x is int x &&
                context.y is int y)
            {
                gm.SaveFlipSingleCell(x, y);
            }
        }
    }
}
