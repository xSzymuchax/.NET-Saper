using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Saper.Model
{
    public class ColorsModel : INotifyPropertyChanged
    {
        private Brush _systemColor;
        private Brush _ligthColorTheme;
        private Brush _darkColorTheme;
        private Brush _darkColorUnflipped;
        private Brush _ligthColorUnflipped;
        private Brush _darkmouseOverUnflipped;
        private Brush _ligthmouseOverUnflipped;

        private bool _systemMode;
        private static ColorsModel _instance;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Brush SystemColor { 
            get => _systemColor; 
            private set
            {
                _systemColor = value;
                OnPropertyChanged(nameof(_systemColor));
            }

        }
        public bool SystemMode
        {
            get => _systemMode;
            private set
            {
                _systemMode = value;
                OnPropertyChanged(nameof(_systemMode));
            }
        }

        public static ColorsModel Instance {
            get {
                return _instance;
            } }
        public Brush LigthColorTheme { get => _ligthColorTheme; private set => _ligthColorTheme = value; }
        public Brush DarkColorTheme { get => _darkColorTheme; private set => _darkColorTheme = value; }

        public Brush SystemTheme { 
            get {
                if (_systemMode)
                    return DarkColorTheme;
                else
                    return LigthColorTheme;
            }
        }

        public Brush UnflippedCellColor
        {
            get
            {
                if (_systemMode)
                    return _darkColorUnflipped;
                else
                    return _ligthColorUnflipped;
            }
        }

        public Brush MouseOverUnflippedBrush
        {
            get
            {
                if (_systemMode)
                    return _darkmouseOverUnflipped;
                else
                    return _ligthmouseOverUnflipped;
            }
        }
        public ColorsModel() {
            FindSystemColor();
            IsDarkMode();
            _ligthColorTheme = new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            _darkColorTheme = new SolidColorBrush(Color.FromArgb(255, 26, 26, 26));
            _ligthColorUnflipped = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
            _darkColorUnflipped = new SolidColorBrush(Color.FromArgb(255, 40, 40, 40));
            _darkmouseOverUnflipped = new SolidColorBrush(Color.FromArgb(255, 32, 32, 32));
            _ligthmouseOverUnflipped = new SolidColorBrush(Color.FromArgb(255, 220, 220, 220));

            _instance = this;
            Debug.WriteLine("Utworzono model koloru");
        }

        private void FindSystemColor()
        {
            SystemColor = new SolidColorBrush(SystemParameters.WindowGlassColor);
            Debug.WriteLine("system color: " + _systemColor);
        }

        private void IsDarkMode()
        {
            try
            {
                RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                
                if (key != null)
                {
                    object? value = key.GetValue("AppsUseLightTheme");

                    if (value != null)
                    {
                        int result = (int)value;
                        if (result > 0)
                        {
                            SystemMode = false;
                        }
                        else
                        {
                            SystemMode = true;
                        }   
                    }
                }
                Debug.WriteLine($"Is dark: {_systemMode}");
            }
            catch
            {
                Debug.WriteLine("darkmodereading_error");
            }
        }
    }
}
