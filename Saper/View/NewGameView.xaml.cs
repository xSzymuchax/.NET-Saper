using Saper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Saper.View
{
    /// <summary>
    /// Logika interakcji dla klasy NewGameView.xaml
    /// </summary>
    public partial class NewGameView : Window
    {
        private GameMode _difficulty;
        private int _width;
        private int _heigth;
        private int _mines;

        public GameMode Difficulty { get => _difficulty; private set => _difficulty = value; }
        public int Width1 { get => _width; private set => _width = value; }
        public int Heigth { get => _heigth; private set => _heigth = value; }
        public int Mines { get => _mines; private set => _mines = value; }

        public NewGameView()
        {
            InitializeComponent();
        }

        private void EasyGameStartButton_Click(object sender, RoutedEventArgs e)
        {
            Difficulty = GameMode.EASY;
            DialogResult = true;
        }

        private void MediumGameStartButton_Click(object sender, RoutedEventArgs e)
        {
            Difficulty = GameMode.NORMAL;
            DialogResult = true;
        }

        private void HardGameStartButton_Click(object sender, RoutedEventArgs e)
        {
            Difficulty = GameMode.HARD;
            DialogResult = true;
        }

        private void ExpertGameStartButton_Click(object sender, RoutedEventArgs e)
        {
            Difficulty = GameMode.EXPERT;
            DialogResult = true;
        }

        private void CustomGameStartButton_Click(object sender, RoutedEventArgs e)
        {
            Difficulty = GameMode.CUSTOM;

            if (Int32.TryParse(WidthTextbox.Text, out _width) == false)
            {
                MessageBox.Show("Bad width!", "Error!");
            }

            if (Int32.TryParse(HeigthTextbox.Text, out _heigth) == false)
            {
                MessageBox.Show("Bad width!", "Error!");
            }

            if (Int32.TryParse(MinesTextbox.Text, out _mines) == false)
            {
                MessageBox.Show("Bad width!", "Error!");
            }
            DialogResult = true;
        }
    }
}
