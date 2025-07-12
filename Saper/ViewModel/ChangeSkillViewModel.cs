using Saper.Command;
using Saper.Model.Skills;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Saper.ViewModel
{
    public class ChangeSkillViewModel
    {
        private ObservableCollection<Skill> _skillsList;

        private string _selectedSkillName;
        private ICommand _selectCommand;
        private Action _closeView;

        public string SelectedSkillName { get => _selectedSkillName; private set => _selectedSkillName = value; }
        public ICommand SelectCommand { get => _selectCommand; set => _selectCommand = value; }
        public ObservableCollection<Skill> SkillsList { get => _skillsList; private set => _skillsList = value; }
    
        public ChangeSkillViewModel(Action closeAction)
        {
            _closeView = closeAction;
            
            List<Skill> skills = SkillGenerator.ReturnAllSkills();
            _skillsList = new ObservableCollection<Skill>();

            foreach (var s in skills)
                SkillsList.Add(s);

            SelectCommand = new RelayCommand(Select, o => true);
        }

        public void Select(object param)
        {
            if (param is string s)
            {
                SelectedSkillName = s;    
            }
            _closeView?.Invoke();
        }
    }
}
