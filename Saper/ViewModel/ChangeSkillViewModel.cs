using Saper.Command;
using Saper.Model;
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
    public class SkillWithColor
    {
        private Skill _skill;
        private ColorsModel _colorsModel;

        public SkillWithColor(Skill skill, ColorsModel colorsModel)
        {
            Skill = skill;
            ColorsModel = colorsModel;
        }

        public ColorsModel ColorsModel { get => _colorsModel; set => _colorsModel = value; }
        public Skill Skill { get => _skill; set => _skill = value; }
    }

    public class ChangeSkillViewModel
    {
        private ObservableCollection<SkillWithColor> _skillsList;

        private string _selectedSkillName;
        private ICommand _selectCommand;
        private Action _closeView;
        public ColorsModel ColorsModel {get => ColorsModel.Instance; }

        public string SelectedSkillName { get => _selectedSkillName; private set => _selectedSkillName = value; }
        public ICommand SelectCommand { get => _selectCommand; set => _selectCommand = value; }
        public ObservableCollection<SkillWithColor> SkillsList { get => _skillsList; private set => _skillsList = value; }
    
        public ChangeSkillViewModel(Action closeAction)
        {
            _closeView = closeAction;
            
            List<Skill> skills = SkillGenerator.ReturnAllSkills();
            _skillsList = new ObservableCollection<SkillWithColor>();

            foreach (var s in skills)
                SkillsList.Add(new(s, ColorsModel));

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
