using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Model.Skills
{
    public class TestSkill : Skill
    {
        
        public TestSkill()
        {
            Description = "Test Test Test Test Test .";
            SkillName = "Test";
            SkillImage = "/Resource/mine.png";
        }

        protected override void ExecuteSkill(SkillContext context)
        {
            if (context.target == null)
                throw new Exception("Target is empty");

            if (context.x == null)
                throw new Exception("x is empty");

            if (context.x == null)
                throw new Exception("y is empty");

            if (context.target is GameboardModel gm &&
                context.x is int x &&
                context.y is int y)
            {
                gm.SaveFlipSingleCell(x, y);
            }
        }
    }
}
