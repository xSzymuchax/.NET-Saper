using Saper.Model.Skills;
using Saper.View;
using Saper.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Saper.Model
{
    public class GameController : INotifyPropertyChanged
    {
        private bool _gameRunning;
        private TimerModel _timer;
        private GameboardViewModel _gameboardViewModel;
        private GameboardModel _gameboard;
        private static GameController _instance;

        private Skill _skill1;
        private Skill _skill2;
        private bool _skill1Active;
        private bool _skill2Active;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GameController()
        {
            if (_instance != null)
                return;

            GameRunning = false;
            _timer = new TimerModel();

            Instance = this;
        }

        public bool GameRunning { get => _gameRunning; private set => _gameRunning = value; }
        public TimerModel Timer { get => _timer; set => _timer = value; }
        public static GameController Instance { get => _instance; private set => _instance = value; }
        public bool Skill1Active { get => _skill1Active; private set { _skill1Active = value; OnPropertyChanged(nameof(Skill1Active)); } }
        public bool Skill2Active { get => _skill2Active; private set { _skill2Active = value; OnPropertyChanged(nameof(Skill2Active)); } }

        public GameboardViewModel StartGame(GameMode gameMode, int height=0, int width=0, int mines=0)
        {
            _skill1 = new SaveClick();
            _skill2 = new SaveClick();

            GameboardViewModel gvm;
            switch (gameMode)
            {
                case GameMode.EASY:
                    gvm = new GameboardViewModel(10, 10, 10) { Timer = Timer };
                    break;
                case GameMode.NORMAL:
                    gvm = new GameboardViewModel(12, 12, 25) { Timer = Timer };
                    break;
                case GameMode.HARD:
                    gvm = new GameboardViewModel(15, 15, 60) { Timer = Timer };
                    break;
                case GameMode.EXPERT:
                    gvm = new GameboardViewModel(20, 20, 100) { Timer = Timer };
                    break;
                case GameMode.CUSTOM:
                    gvm = new GameboardViewModel(height, width, mines) { Timer = Timer };
                    break;
                default:
                    return new GameboardViewModel(2, 2, 1) { Timer = Timer };
            }
            
            _gameboardViewModel = gvm;
            _gameboard = gvm.Gameboard;
            GameRunning = true;
            Timer.StopTimer();
            Timer.ResetTimer();
            Skill1Active = false;
            Skill2Active = false;
            return gvm;
        }

        public bool ActivateSkill1()
        {
            if (_skill1 != null && _skill1.SkillUsed)
                return false;

            Debug.WriteLine("Skill1Active");
            Skill1Active = true;
            return true;
        }

        public bool ActivateSkill2()
        {
            if (_skill2 != null && _skill2.SkillUsed)
                return false;


            Skill2Active = true;
            return true;
        }

        public void EndGame() 
        {
            GameRunning = false;
            Timer.StopTimer();
            //MessageBox.Show("boom");
        }

        public void FlipCell(int x, int y)
        {
            if (!GameRunning)
                return;

            if (!Timer.IsRunning)
                Timer.StartTimer();

            bool bombClicked;
            if (Skill1Active)
            {
                SkillContext sc = new SkillContext()
                {
                    x = x,
                    y = y,
                    target=_gameboard
                };
                _skill1.ActivateSkill(sc);
                Skill1Active = false;
                bombClicked = false;
            }
            else if (Skill2Active)
            {
                SkillContext sc = new SkillContext()
                {
                    x = x,
                    y = y,
                    target = _gameboard
                };
                _skill2.ActivateSkill(sc);
                Skill2Active = false;
                bombClicked = false;
            }
            else
            {
                bombClicked = _gameboard.FlipCell(x, y);
            }

                
            if (bombClicked)
                EndGame();
        }

        public void FlagCell(int x, int y)
        {
            if (!GameRunning)
                return;

            _gameboard.FlagCell(x, y);
        }
    }
}
