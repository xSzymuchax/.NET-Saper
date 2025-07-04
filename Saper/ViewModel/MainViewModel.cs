﻿using Saper.Command;
using Saper.Model;
using Saper.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization.DataContracts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Saper.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private GameboardViewModel _gameboardVM;
        private ICommand _startNewGameCommand;
        private ICommand _showOptionDialogCommand;

        public string MinesLeft
        {
            get
            {
                if (_gameboardVM != null)
                    return $"Mines: {_gameboardVM.MinesLeft}";
                else
                    return "Mines: 0";
            }
        }

        public string CellsLeft
        {
            get
            {
                if (_gameboardVM != null)
                    return $"Cells: {_gameboardVM.CellsLeft}";
                else
                    return "Cells: 0";
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public GameboardViewModel GameboardVM
        {
            get => _gameboardVM;
            private set
            {
                if (_gameboardVM != value)
                {
                    if (_gameboardVM != null)
                        _gameboardVM.PropertyChanged -= GameboardVM_PropertyChanged;

                    _gameboardVM = value;

                    if (_gameboardVM != null)
                        _gameboardVM.PropertyChanged += GameboardVM_PropertyChanged;

                    OnPropertyChanged(nameof(GameboardVM));
                    OnPropertyChanged(nameof(MinesLeft));
                    OnPropertyChanged(nameof(CellsLeft));
                }
            }
        }

        // and im added to my child too... if it changes then i change, and then ui changes
        private void GameboardVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GameboardVM.MinesLeft))
            {
                OnPropertyChanged(nameof(MinesLeft));
            }
            else if (e.PropertyName == nameof(GameboardVM.CellsLeft))
            {
                OnPropertyChanged(nameof(CellsLeft));
            }
        }
        public ICommand StartNewGameCommand { get => _startNewGameCommand; set => _startNewGameCommand = value; }
        public ICommand ShowOptionDialogCommand { get => _showOptionDialogCommand; set => _showOptionDialogCommand = value; }

        public ColorsModel ColorsModel { get => ColorsModel.Instance; }

        public MainViewModel()
        {
            new ColorsModel();
            new Options();
            Debug.WriteLine("Instancja:" + ColorsModel);
            //_gameboardVM = new GameboardViewModel(1,2,1);
            

            _startNewGameCommand = new RelayCommand(StartNewGame, o => true);
            _showOptionDialogCommand = new RelayCommand(ChangeOptions, o => true);
        }

        public void StartNewGame(object param)
        {
            NewGameView newGameView = new NewGameView();
            newGameView.DataContext = this;
            newGameView.ShowDialog();

            if (newGameView.DialogResult == true)
            {
                switch (newGameView.Difficulty)
                {
                    case "easy":
                        GameboardVM = new GameboardViewModel(10, 10, 10);
                        break;
                    case "medium":
                        GameboardVM = new GameboardViewModel(12, 12, 25);
                        break;
                    case "hard":
                        GameboardVM = new GameboardViewModel(15, 15, 60);
                        break;
                    case "expert":
                        GameboardVM = new GameboardViewModel(20, 20, 100);
                        break;
                    case "custom":
                        GameboardVM = new GameboardViewModel(newGameView.Heigth, newGameView.Width1, newGameView.Mines);
                        break;
                    default:
                        break;
                }
            }
            else return;
        }
        
        public void ChangeOptions(object param)
        {
            OptionsView optionsView = new OptionsView();
            optionsView.ShowDialog();
        }
    }
}
