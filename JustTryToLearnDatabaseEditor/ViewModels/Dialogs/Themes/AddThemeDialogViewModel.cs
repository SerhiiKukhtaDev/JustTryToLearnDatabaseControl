using System.Collections.ObjectModel;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Themes
{
    public class AddThemeDialogViewModel : 
        ParameterizedDialogViewModelBase<ItemResult<Theme>, ObservableCollection<Theme>>
    {
        private ObservableCollection<Theme> _themes;

        public void AddNewTheme(object parameter)
        {
            string name = parameter as string;
            
            Close(new ItemResult<Theme>(new Theme {Name = name}));
        }

        public bool CanAddNewSubject(object parameter)
        {
            return true;
        }
        
        public override void Activate(ObservableCollection<Theme> parameter)
        {
            _themes = parameter;
        }
    }
}
