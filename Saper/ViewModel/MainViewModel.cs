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
using System.Windows.Threading;

namespace Saper.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // game controller
        private GameController _gameController;
        private bool _skill1Visible;
        private bool _skill2Visible;

        // timer
        private TimerModel _timer;
        public TimerModel Timer { get => _timer; private set { _timer = value; }}

        // skills visulas
        // 1 -> GameController.Skill1Active

        //commands
        private ICommand _startNewGameCommand;
        private ICommand _showOptionDialogCommand;
        private ICommand _activateSkill1;
        private ICommand _activateSkill2;
        public ICommand StartNewGameCommand { get => _startNewGameCommand; set => _startNewGameCommand = value; }
        public ICommand ShowOptionDialogCommand { get => _showOptionDialogCommand; set => _showOptionDialogCommand = value; }

        // property changed event
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

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

        private GameboardViewModel _gameboardVM;
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

        public ColorsModel ColorsModel { get => ColorsModel.Instance; }
        public ICommand ActivateSkill1Command { get => _activateSkill1; set => _activateSkill1 = value; }
        public ICommand ActivateSkill2Command { get => _activateSkill2; set => _activateSkill2 = value; }
        public bool Skill2Visible { get => _skill2Visible; set { _skill2Visible = value; OnPropertyChanged(nameof(Skill2Visible)); } }
        public bool Skill1Visible { get => _skill1Visible; set { _skill1Visible = value; OnPropertyChanged(nameof(Skill1Visible)); } }



        public MainViewModel()
        {
            new ColorsModel();
            new Options();
            _gameController = new GameController();
            Timer = _gameController.Timer;
            
            StartNewGameCommand = new RelayCommand(StartNewGame, o => true);
            ShowOptionDialogCommand = new RelayCommand(ChangeOptions, o => true);
            ActivateSkill1Command = new RelayCommand(ActivateSkill1, o => true);
            ActivateSkill2Command = new RelayCommand(ActivateSkill2, o => true);
        }
        
        public void ChangeOptions(object param)
        {
            OptionsView optionsView = new OptionsView();
            optionsView.ShowDialog();
        }

        public void StartNewGame(object param)
        {
            NewGameView newGameView = new NewGameView();
            newGameView.DataContext = this;
            newGameView.ShowDialog();

            if (newGameView.DialogResult == true)
                GameboardVM = _gameController.StartGame(newGameView.Difficulty, newGameView.Heigth, newGameView.Width1, newGameView.Mines);
            else return;
        }

        public void ActivateSkill1(object param)
        {
            Skill1Visible = _gameController.ActivateSkill1();
        }

        public void ActivateSkill2(object param)
        {
            Skill2Visible = _gameController.ActivateSkill2();
        }
    }
}
