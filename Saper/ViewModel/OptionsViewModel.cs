using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Saper.Command;
using Saper.Model;

namespace Saper.ViewModel
{
    public class OptionsViewModel : INotifyPropertyChanged
    {
        public Options Options => Options.Instance;
        public ColorsModel ColorsModel => ColorsModel.Instance;
        public bool Sound 
        { 
            get
            {
                return Options.Sound;
            }
            set
            {
                Options.Sound = value;
                OnPropertyChanged(nameof(Options.Sound));
            }
        }

        public bool Recording
        {
            get
            {
                return Options.Recording;
            }
            set
            {
                Options.Recording = value;
                OnPropertyChanged(nameof(Options.Recording));
            }
        }

        public string RecordingPath
        {
            get
            {
                return Options.RecordingPath;
            }
            set
            {
                Options.RecordingPath = value;
                OnPropertyChanged(nameof(Options.RecordingPath));
            }
        }

        private ICommand _saveAndExitCommand;
        private ICommand _changeRecordDestinationCommand;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SaveAndExitCommand { get => _saveAndExitCommand; private set => _saveAndExitCommand = value; }
        public ICommand ChangeRecordDestinationCommand { get => _changeRecordDestinationCommand; private set => _changeRecordDestinationCommand = value; }

        public OptionsViewModel() 
        {
            SaveAndExitCommand = new RelayCommand(SaveAndExit, o => true);
            ChangeRecordDestinationCommand = new RelayCommand(ChangeRecordDestination, o => true);
        }

        // tutaj trzba przekazac z formularza
        private void SaveAndExit(object param)
        {
            Options.SaveOptions();
            if (param is Window window)
                window.Close();
        }
        
        private void ChangeRecordDestination(object param)
        {
            //TODO okienko z wyborem miejsca
            Options.SaveOptions();
        }

    }
}
