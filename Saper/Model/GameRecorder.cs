using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Model
{
    public static class GameRecorder
    {
        private static int _seed;
        private static string _recordingPath;
        private static List<string> _steps;

        public static void StartRecord(int seed)
        {
            _seed = seed;
            _steps = new List<string>();
        }

        public static void SaveRecord()
        {
            _recordingPath = Options.Instance.RecordingPath;
            string filename = $"GAME_{_seed}.txt";
            string fullPath = _recordingPath + "/" + filename;

            File.AppendAllText(fullPath, _seed.ToString() + ";\n");
            foreach (var line in _steps)
                File.AppendAllText(fullPath, line);
        }

        public static void AddClick(int x, int y, string time)
        {
            AddStep($"{time};CLICK;{x};{y};\n");
        }

        public static void AddFlag(int x, int y, string time)
        {
            AddStep($"{time};FLAG;{x};{y};\n");
        }

        public static void AddSkillUsed(string skillName, string time)
        {
            AddStep($"{time};SKILL;{skillName};\n");
        }

        private static void AddStep(string data)
        {
            _steps.Add(data);
        }
    }
}
