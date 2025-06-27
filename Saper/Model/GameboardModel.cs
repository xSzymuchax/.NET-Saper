using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Model
{
    public class GameboardModel
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int MineCount { get; private set; }
        public CellModel[,] Board { get; private set; }

        public GameboardModel(int width, int height, int mineCount)
        {
            Width = width;
            Height = height;
            MineCount = mineCount;
            Board = new CellModel[width, height];
            SetUpGameboard();

        }

        public void SetUpGameboard()
        {
            InitializeBoard();
            GenerateMines();
            CountSurroundingMines();
        }
        
        public void InitializeBoard()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Board[j, i] = new CellModel();
                    Board[j, i].SetCellValue(0);
                }
            }
        }

        public void GenerateMines()
        {
            Random random = new Random();
            int mines= MineCount;
            while (mines > 0)
            {
                int x = random.Next(0, Width);
                int y = random.Next(0, Height);

                if (Board[x,y].Value != -1)
                {
                    Board[x,y].SetCellValue(-1);
                    mines--;
                }
            }
        }

        public void CountSurroundingMines()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    // if on mine cell
                    if (Board[i, j].Value == -1)
                        continue;
                    
                    int foundMines = 0;

                    for (int i2 = i-1; i2 <= i+1; i2++)
                    {
                        for (int j2=j-1; j2 <=j+1; j2++)
                        {
                            // center
                            if (i == i2 && j == j2)
                                continue;

                            // non-existing cell
                            if (i2 < 0 || j2 < 0 || i2 >= Height || j2 >= Width)
                                continue;

                            if (Board[i2, j2].Value == -1)
                                foundMines++;
                        }
                    }

                    Board[i, j].SetCellValue(foundMines);
                }
            }
        }

        public int CellValue(int x, int y)
        {
            return Board[x, y].Value;
        }

        public void FlipCell(int x, int y)
        {
            if (!Board[x, y].IsFlagged && !Board[x, y].IsFlipped)
                Board[x, y].FlipCell();
            else
                return;

            if (Board[x, y].Value != 0)
                return;

            for (int i = x-1; i <= x+1; i++)
            {
                for (int j = y-1; j <= y+1; j++)
                {
                    // center
                    if (i == x && j == y)
                        continue;

                    // non-existing cell
                    if (i < 0 || j < 0 || i >= Height || j >= Width)
                        continue;

                    FlipCell(i, j);
                }
            }   
        }

        public void FlagCell(int x, int y)
        {
            if (!Board[x, y].IsFlipped)
            {
                Board[x, y].FlagCell();
            }
        }

    }
}
