using Saper.Command;
using Saper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Saper.ViewModel
{
    public class CellViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private GameboardViewModel _gameboard;
        private int _x;
        private int _y;
        private CellModel _cell;

        private ICommand _flipCommand;
        private ICommand _flagCommand;

        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public int Value { get { return _cell.Value; } }

        public bool HasCounter { 
            get
            {
                if (Value == 0 || Value == -1)
                    return false;
                return true;
            } 
        }
        public ICommand FlipCommand { get => _flipCommand; set => _flipCommand = value; }
        public ICommand FlagCommand { get => _flagCommand; set => _flagCommand = value; }
        public Brush FontBrush { get => ColorsModel.Instance.SystemColor; }
        public Brush BorderBrush { get => ColorsModel.Instance.SystemColor; }
        public Brush UnflippedBrush { get => ColorsModel.Instance.UnflippedCellColor; }
        public Brush MouseOverUnflippedBrush { get => ColorsModel.Instance.MouseOverUnflippedBrush; }
        public bool IsFlagged 
        { 
            get => _cell.IsFlagged; 
        
            set
            {
                if (!_cell.IsFlipped)
                {
                    _cell.IsFlagged = !_cell.IsFlagged;
                    OnPropertyChanged();
                }
                    
            }
        }

        public bool IsFlipped
        {
            get => _cell.IsFlipped;

            set
            {
                if (!_cell.IsFlagged && !_cell.IsFlipped)
                {
                    _cell.IsFlipped = true;
                    OnPropertyChanged();
                }
            }
        }



        public CellViewModel(GameboardViewModel gameboardViewModel, int x, int y, CellModel cell)
        {
            _gameboard = gameboardViewModel;
            _x = x;
            _y = y;
            _cell = cell;

            _flipCommand = new RelayCommand(FlipCell, o => true);
            FlagCommand = new RelayCommand(FlagCell, o => true);
        }

        public void FlipCell(object obj)
        {
            Debug.WriteLine("FlipCommand");
            _gameboard.FlipCell(_x, _y);
        }

        public void FlagCell(object obj)
        {
            _gameboard.FlagCell(_x, _y);
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
