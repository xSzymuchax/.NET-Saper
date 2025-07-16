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

        private Skill _skill;
        private bool _skillActive;
        private string _skillName;

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

            SetUpSkill("No Skill");
            Instance = this;
        }

        public bool GameRunning { get => _gameRunning; private set { _gameRunning = value; OnPropertyChanged(nameof(GameRunning)); } }
        public TimerModel Timer { get => _timer; set => _timer = value; }
        public static GameController Instance { get => _instance; private set => _instance = value; }
        public bool SkillActive { get => _skillActive; private set { _skillActive = value; OnPropertyChanged(nameof(SkillActive)); } }

        public Skill Skill { get => _skill; set { _skill = value; OnPropertyChanged(nameof(Skill)); } }

        public GameboardViewModel StartGame(GameMode gameMode, int height=0, int width=0, int mines=0)
        {
            GameboardViewModel gvm;
            switch (gameMode)
            {
                case GameMode.EASY:
                    gvm = new GameboardViewModel(10, 10, 18) { Timer = Timer };
                    break;
                case GameMode.NORMAL:
                    gvm = new GameboardViewModel(12, 12, 25) { Timer = Timer };
                    break;
                case GameMode.HARD:
                    gvm = new GameboardViewModel(15, 15, 38) { Timer = Timer };
                    break;
                case GameMode.EXPERT:
                    gvm = new GameboardViewModel(20, 20, 70) { Timer = Timer };
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
            SetUpSkill(_skillName);
            return gvm;
        }

        public void SetUpSkill(string skillName)
        {
            _skillName = skillName;
            Skill = SkillGenerator.ReturnSkill(skillName);
            SkillActive = false;
        }

        public bool ActivateSkill1()
        {
            if (Skill != null && Skill.SkillUsed)
                return false;

            Debug.WriteLine($"Skill1Active - hash: {_gameboard.GetHashCode()}");
            SkillActive = true;

            // if instant skill
            if (Skill.IsInstant)
            {
                SkillActive = false;
                Skill.ActivateSkill(new SkillContext()
                {
                    target = this._gameboard,
                    x = -1,
                    y = -1
                });
                return false;
            }

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
            if (SkillActive)
            {
                SkillContext sc = new SkillContext()
                {
                    x = x,
                    y = y,
                    target=_gameboard
                };
                Skill.ActivateSkill(sc);
                SkillActive = false;
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
