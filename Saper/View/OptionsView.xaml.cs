﻿using Saper.Model;
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
using Saper.ViewModel;

namespace Saper.View
{
    /// <summary>
    /// Logika interakcji dla klasy OptionsView.xaml
    /// </summary>
    public partial class OptionsView : Window
    {
        public OptionsView()
        {
            InitializeComponent();
            DataContext = new OptionsViewModel();
        }
    }
}
