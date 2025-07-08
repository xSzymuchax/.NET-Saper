using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Saper.Model
{
    public class TimerModel : INotifyPropertyChanged
    {
        private DispatcherTimer _timer;
        private DateTime _clickTime;
        private DateTime _currentTime;
        private string _gameTime;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string GameTime
        { 
            get => _gameTime;
            private set {
                _gameTime = value;
                OnPropertyChanged(nameof(GameTime));
            }
        }

        private void UpdateTimer()
        {
            _currentTime = DateTime.Now;
            TimeSpan elapsed = _currentTime - _clickTime;
            GameTime = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", (int)elapsed.TotalHours, elapsed.Minutes, elapsed.Seconds, elapsed.Milliseconds);
            Debug.WriteLine(GameTime);
        }

        public void StartTimer()
        {
            _clickTime = DateTime.Now;
            _timer.Start();
        }

        public TimerModel()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(50);
            _timer.Tick += (s, e) => UpdateTimer();
            _gameTime = "00:00:00.000";
        }
    }
}
