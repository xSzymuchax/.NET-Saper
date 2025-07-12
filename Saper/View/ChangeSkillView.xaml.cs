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
    /// Logika interakcji dla klasy ChangeSkillView.xaml
    /// </summary>
    public partial class ChangeSkillView : Window
    {
        public ChangeSkillView()
        {
            InitializeComponent();
            DataContext = new ChangeSkillViewModel(() => { this.DialogResult = true; this.Close(); });
        }
    }
}
