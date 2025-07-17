using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Model.Skills
{
    public class RevealRandomMinesSkill : Skill
    {
        private int _flagFields = 5;

        private class Point
        {
            public int x;
            public int y;
        }

        public RevealRandomMinesSkill()
        {
            Description = $"This skill marks {_flagFields} random mines on board.";
            SkillImage = "/Resource/SkillImages/revealrandommines.png";
            SkillName = "Reveal Random Mines";
            IsInstant = true;
        }
        protected override void ExecuteSkill(SkillContext context)
        {
            if (context.target is GameboardModel gm &&
                context.x is int x &&
                context.y is int y)
            {
                List<Point> cells = new List<Point>();

                for (int i=0; i < gm.Height; i++)
                {
                    for (int j=0; j < gm.Width; j++)
                    {
                        if (gm.Board[i,j].Value == -1 && gm.Board[i,j].IsFlagged == false && gm.Board[i,j].IsFlipped == false)
                        {
                            cells.Add(new Point() { x = i, y = j });
                            Debug.WriteLine($"{i} - {j}");
                        }
                    }
                }
                Debug.WriteLine($"{cells.Count}");
                // draw only as many times as there is uncovered mines
                int foundCells = cells.Count;
                int draws = _flagFields;
                if (draws >= foundCells)
                    draws = foundCells;

                Random rand = new Random(GameController.Instance.Seed);

                while ( draws > 0)
                {
                    int index = rand.Next(0, cells.Count);
                    gm.FlagCell(cells[index].x, cells[index].y);
                    cells.RemoveAt(index);
                    draws--;
                }

            }
        }
    }
}
