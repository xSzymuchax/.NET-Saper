using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Model.Skills
{
    public class RadarSkill : Skill
    {
        private int _radarRange = 1;
        public RadarSkill()
        {
            Description = $"This skill discovers the {_radarRange}x{_radarRange} zone and marks the mines.";
            SkillImage = "/Resource/SkillImages/radar.png";
            SkillName = "Radar";
        }
        protected override void ExecuteSkill(SkillContext context)
        {
            if (context.target is GameboardModel gm &&
                context.x is int x &&
                context.y is int y)
            {

                for (int i = x - _radarRange; i <= x + _radarRange; i++)
                    for (int j = y - _radarRange; j <= y + _radarRange; j++)
                        gm.FlipOrFlagCell(i, j);
            }
        }
    }
}
