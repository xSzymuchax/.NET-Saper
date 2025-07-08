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
        private TimerModel _timer;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<CellViewModel> Cells { get; }
        public int Heigth => Gameboard.Height;
        public int Width => Gameboard.Width;
        public int CellsLeft => Gameboard.CellsLeft;
        public int MinesLeft => Gameboard.MinesLeft;
        public TimerModel Timer { get; set; }
        public GameboardModel Gameboard { get => _gameboard; private set => _gameboard = value; }

        public GameboardViewModel(int x_size, int y_size, int mines)
        {

            if (mines >= x_size * y_size)
            {
                Gameboard = new GameboardModel(1, 1, 0);
                Gameboard.PropertyChanged += Gameboard_PropertyChanged;
                return;
            }
                

            // roboczo
            Gameboard = new GameboardModel(x_size, y_size, mines);
            Gameboard.PropertyChanged += Gameboard_PropertyChanged;
            Cells = new ObservableCollection<CellViewModel>();

            for (int i = 0; i < x_size; i++)
            {
                for (int j = 0; j < y_size; j++)
                {
                    CellViewModel cellViewModel = new CellViewModel(this, i, j, Gameboard.Board[i, j]);
                    Cells.Add(cellViewModel);
                }
            }
        }

        // im added to my child, on change, when child changes, it tells me, then i change...
        private void Gameboard_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Gameboard.MinesLeft))
            {
                OnPropertyChanged(nameof(MinesLeft));
            }
            else if (e.PropertyName == nameof(Gameboard.CellsLeft))
            {
                OnPropertyChanged(nameof(CellsLeft));
            }
            else if (e.PropertyName == nameof(Gameboard.Board))
            {
                OnPropertyChanged(nameof(Cells));
            }
        }

        public void FlipCell(int x, int y)
        {
            GameController.Instance.FlipCell(x, y);
            foreach (var cell in Cells)
            {
                cell.OnPropertyChanged(nameof(CellViewModel.IsFlipped));
            }
        }

        public void FlagCell(int x, int y)
        {
            GameController.Instance.FlagCell(x, y);
            var cell = Cells.FirstOrDefault(c => c.X == x && c.Y == y);
            if (cell != null)
                cell.OnPropertyChanged(nameof(CellViewModel.IsFlagged));
        }
    }
}
