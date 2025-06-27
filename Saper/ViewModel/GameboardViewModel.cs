using Saper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.ViewModel
{
    public class GameboardViewModel
    {
        private GameboardModel _gameboard;

        public ObservableCollection<CellViewModel> Cells { get; }

        public int Heigth => _gameboard.Height;

        public int Width => _gameboard.Width;

        public GameboardViewModel()
        {
            // roboczo
            _gameboard = new GameboardModel(10, 10, 10);
            Cells = new ObservableCollection<CellViewModel>();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    CellViewModel cellViewModel = new CellViewModel(this, i, j, _gameboard.Board[i, j]);
                    Cells.Add(cellViewModel);
                }
            }
        }

        public void FlipCell(int x, int y)
        {
            _gameboard.FlipCell(x, y);

            foreach (var cell in Cells)
            {
                cell.OnPropertyChanged(nameof(CellViewModel.IsFlipped));
            }
        }

        public void FlagCell(int x, int y)
        {
            _gameboard.FlagCell(x,y);

            var cell = Cells.FirstOrDefault(c => c.X == x && c.Y == y);
            if (cell != null)
                cell.OnPropertyChanged(nameof(CellViewModel.IsFlagged));
        }
    }
}
