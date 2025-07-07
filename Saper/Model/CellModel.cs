using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Model
{
    public class CellModel : INotifyPropertyChanged
    {
        private bool _isFlagged = false;
        private bool _isFlipped = false;
        private int _value = 0;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool IsFlagged {get { return _isFlagged; }  set { _isFlagged = value; }}
        public int Value 
        { 
            get => _value;
            set 
            { 
                _value = value;
                OnPropertyChanged(nameof(Value));
            } 
        }
        public bool IsFlipped { get { return _isFlipped; }  set { _isFlipped = value; } }

        public void FlipCell()
        {
            IsFlipped = true;
        }

        public void SetCellValue(int value)
        {
            Value = value;
        }

        public void FlagCell()
        {
            IsFlagged = !IsFlagged;
        }
    }
}
