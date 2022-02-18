using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Themes
{
    public class EditThemeDialogViewModel : ParameterizedDialogViewModelBase<ItemResult<Theme>, Theme>
    {
        #region Fields

        private Theme _selectedTheme;

        private string _editedThemeName;
        
        public string EditedThemeName
        {
            get => _editedThemeName;
            set => this.RaiseAndSetIfChanged(ref _editedThemeName, value);
        }

        #endregion
        
        #region Commands
        
        public void OnEditCommandExecute(object parameter)
        {
            Close(new ItemResult<Theme>(new Theme() {Name = _editedThemeName}));
        }

        public bool CanOnEditCommandExecute(object parameter)
        {
            string text = parameter as string;
            return !string.IsNullOrWhiteSpace(text) && text != _selectedTheme.Name && text.Length < 256;
        }

        #endregion
        
        public override void Activate(Theme parameter)
        {
            _selectedTheme = parameter;
            EditedThemeName = _selectedTheme.Name;
        }
    }
}
