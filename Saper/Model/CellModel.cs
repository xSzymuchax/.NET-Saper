using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Model
{
    public class CellModel
    {
        private bool _isFlagged = false;
        private bool _isFlipped = false;
        private int _value = 0;
        public bool IsFlagged {get { return _isFlagged; } private set { _isFlagged = value; }}
        public int Value { get { return _value; } private set { _value = value; } }
        public bool IsFlipped { get { return _isFlipped; } private set { _isFlipped = value; } }

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
