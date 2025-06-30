using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.ViewModel
{
    public class MainViewModel
    {
        public GameboardViewModel GameboardVM { get; set; }

        public MainViewModel()
        {
            GameboardVM = new GameboardViewModel(10,15,10);
        }
    }
}
