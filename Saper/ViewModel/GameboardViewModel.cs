using Saper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Saper.ViewModel
{
    public class GameboardViewModel : INotifyPropertyChanged
    {
        private GameboardModel _gameboard;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<CellViewModel> Cells { get; }
        public int Heigth => _gameboard.Height;
        public int Width => _gameboard.Width;
        public int CellsLeft => _gameboard.CellsLeft;
        public int MinesLeft => _gameboard.MinesLeft;

        
        public GameboardViewModel(int x_size, int y_size, int mines)
        {

            if (mines >= x_size * y_size)
            {
                _gameboard = new GameboardModel(1, 1, 0);
                _gameboard.PropertyChanged += Gameboard_PropertyChanged;
                return;
            }
                

            // roboczo
            _gameboard = new GameboardModel(x_size, y_size, mines);
            _gameboard.PropertyChanged += Gameboard_PropertyChanged;
            Cells = new ObservableCollection<CellViewModel>();

            for (int i = 0; i < x_size; i++)
            {
                for (int j = 0; j < y_size; j++)
                {
                    CellViewModel cellViewModel = new CellViewModel(this, i, j, _gameboard.Board[i, j]);
                    Cells.Add(cellViewModel);
                }
            }
        }

        // im added to my child, on change, when child changes, it tells me, then i change...
        private void Gameboard_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_gameboard.MinesLeft))
            {
                OnPropertyChanged(nameof(MinesLeft));
            }
            else if (e.PropertyName == nameof(_gameboard.CellsLeft))
            {
                OnPropertyChanged(nameof(CellsLeft));
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
