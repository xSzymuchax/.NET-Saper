using Saper.Command;
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


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public GameboardViewModel GameboardVM { get => _gameboardVM;
            set
            {
                _gameboardVM = value;
                Debug.WriteLine("zmieniono planszę");
                OnPropertyChanged(nameof(GameboardVM));
            }
        }
        public ICommand StartNewGameCommand { get => _startNewGameCommand; set => _startNewGameCommand = value; }
        public ICommand ShowOptionDialogCommand { get => _showOptionDialogCommand; set => _showOptionDialogCommand = value; }

        public ColorsModel ColorsModel { get => ColorsModel.Instance; }

        public MainViewModel()
        {
            new ColorsModel();
            Debug.WriteLine("Instancja:" + ColorsModel);
            _gameboardVM = new GameboardViewModel(1,2,1);
            

            _startNewGameCommand = new RelayCommand(StartNewGame, o => true);
            _showOptionDialogCommand = new RelayCommand(ChangeOptions, o => true);
        }

        public void StartNewGame(object param)
        {
            NewGameView newGameView = new NewGameView();
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
                        GameboardVM = new GameboardViewModel(20, 20, 120);
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
