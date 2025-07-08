using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Saper.Model
{
    public class GameboardModel : INotifyPropertyChanged
    {
        private int _minesLeft;
        private int _cellsLeft;
        private bool _firstFlipClick;
        private CellModel[,] _board;

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int MineCount { get; private set; }
        public CellModel[,] Board {
            get => _board;
            private set {
                _board = value;
                OnPropertyChanged(nameof(Board));
            }
        }

        // this one is funny - when i change, i notify...
        public int CellsLeft {
            get => _cellsLeft; 
            private set
            {
                _cellsLeft = value;
                OnPropertyChanged(nameof(CellsLeft));
            }
        }
        public int MinesLeft
        {
            get => _minesLeft;
            private set
            {
                _minesLeft = value;
                OnPropertyChanged(nameof(MinesLeft));
            }
        }
        public GameboardModel(int width, int height, int mineCount)
        {
            _firstFlipClick = true;
            Width = width;
            Height = height;
            MineCount = mineCount;
            Board = new CellModel[width, height];
            //SetUpDummyGameboard();
            SetUpGameboard();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetUpDummyGameboard()
        {
            InitializeBoard();
            CellsLeft = Width * Height - MineCount;
            MinesLeft = MineCount;
        }

        public void SetUpGameboard()
        {
            InitializeBoard();
            GenerateMines(MineCount);
            //CountSurroundingMines();
            CellsLeft = Width * Height - MineCount;
            MinesLeft = MineCount;
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
        private void DebugGameboard()
        {
            string log = "";
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                    log += Board[i, j].Value;
                log += "\n";
            }
            Debug.WriteLine(log);
        }

        public void GenerateMines(int mines_count)
        {
            Random random = new Random();
            int mines= mines_count;
            while (mines > 0)
            {
                int x = random.Next(0, Width);
                int y = random.Next(0, Height);

                if (Board[x,y].Value != -1 && Board[x,y].Value != -2)
                {
                    Board[x,y].SetCellValue(-1);
                    mines--;
                }
            }

            DebugGameboard();
        }

        public void CountSurroundingMines()
        {
            Debug.WriteLine("countingmines");
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
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
                            if (i2 < 0 || j2 < 0 || i2 >= Width || j2 >= Height)
                                continue;

                            if (Board[i2, j2].Value == -1)
                                foundMines++;
                        }
                    }

                    Board[i, j].SetCellValue(foundMines);
                }
            }

            DebugGameboard();
            //Application.Current.MainWindow.UpdateLayout();
        }

        public int CellValue(int x, int y)
        {
            return Board[x, y].Value;
        }

        // TODO - to ma kasowac 3x3 w miejscu kliku i przezucac bomby w inne miejsce
        private void FirstGameFlip(int x, int y)
        {
            int removedMines = 0;
            _firstFlipClick = false;
            int x2, y2;
            for (int i=-1; i<=1; i++)
                for (int j=-1; j<=1; j++)
                {
                    x2 = x + i;
                    y2 = y + j;
                    if (x2 < 0 || y2 < 0 || x2 >= Width || y2 >= Height)
                        continue;

                    // if bomb, remove and increment
                    // mark field as -2
                    if (Board[x2,y2].Value == -1)
                    {
                        removedMines++;
                    }
                    Board[x2, y2].Value = -2;
                }

            GenerateMines(removedMines);
            CountSurroundingMines();
        }

        public void FlipCell(int x, int y)
        {
            if (_firstFlipClick)
            {
                FirstGameFlip(x, y);
                FlipCell(x, y);
                return;
            }

            if (!Board[x, y].IsFlagged && !Board[x, y].IsFlipped)
            {
                Board[x, y].FlipCell();
                CellsLeft--;
            }
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
                    if (i < 0 || j < 0 || i >= Width || j >= Height)
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
                if (Board[x, y].IsFlagged)
                    MinesLeft--;
                else
                    MinesLeft++;
            }
        }

    }
}
