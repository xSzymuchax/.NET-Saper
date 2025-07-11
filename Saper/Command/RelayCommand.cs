﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Saper.Command
{
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private Action<object> _Execute { get; set; }
        private Predicate<object> _CanExecute { get; set; }


        public RelayCommand(Action<object> Method, Predicate<object> CanExecute)
        {
            _Execute = Method ?? throw new ArgumentNullException(nameof(Method));
            _CanExecute = CanExecute ?? throw new ArgumentNullException(nameof(CanExecute));
        }

        public bool CanExecute(object? parameter)
        {
            return _CanExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object? parameter)
        {
            _Execute(parameter);
        }
    }
}
