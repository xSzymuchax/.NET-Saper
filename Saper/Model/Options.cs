using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Saper.Model
{
    public class Options
    {
        private bool _sound;
        private bool _recording;
        private string _recordingPath;

        private static Options _instance;
        public static Options Instance => _instance;

        public bool Sound { get => _sound; set => _sound = value; }
        public bool Recording { get => _recording; set => _recording = value; }
        public string RecordingPath { get => _recordingPath; set => _recordingPath = value; }

        public Options()
        {
            _instance = this;

            if (FindOptions())
                LoadOptions();
            else
                DefaultOptions();

            ShowDebugOptions();
        }

        private bool FindOptions()
        {
            if (File.Exists("options.txt"))
                return true;
            return false;
        }

        private void DefaultOptions()
        {
            _recording = true;
            _recordingPath = AppDomain.CurrentDomain.BaseDirectory;
            _sound = true;
            SaveOptions();
        }

        private void LoadOptions() // sound, recording, recordingpath
        {
            foreach (string line in File.ReadLines("options.txt"))
            {
                string[] result = line.Split("=");
                bool parsed;

                switch (result[0])
                {
                    case "sound":
                        _sound = bool.TryParse(result[1], out parsed) ? parsed : false;
                        break;

                    case "recording":
                        _recording = bool.TryParse(result[1], out parsed) ? parsed : false;
                        break;

                    case "recording_path":
                        _recordingPath = result[1];
                        break;
                }
            }
        }

        public void SaveOptions()
        {
            List<string> linesToWrite = new List<string>() { $"sound={_sound}", $"recording={_recording}", $"recording_path={_recordingPath}" };
            File.WriteAllLines("options.txt", linesToWrite);
        }

        private void ShowDebugOptions()
        {
            string optionsString = $"Sound: {Sound}\n" +
                $"Recording: {Recording}\n" +
                $"RecordingPath: {RecordingPath}\n";

            Debug.WriteLine(optionsString);
        }
    }
}
