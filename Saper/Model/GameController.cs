using Saper.View;
using Saper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Saper.Model
{
    public class GameController
    {
        private bool _gameRunning;
        private TimerModel _timer;
        private GameboardViewModel _gameboardViewModel;
        private static GameController _instance;


        
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

        public GameboardViewModel StartGame(GameMode gameMode, int height=0, int width=0, int mines=0)
        {
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
            GameRunning = true;
            return gvm;
        }

        public void EndGame() 
        {
            GameRunning = false;
            Timer.StopTimer();
            MessageBox.Show("boom");
        }


    }
}
