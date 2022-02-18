using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Classes
{
    public class EditClassDialogViewModel : ParameterizedDialogViewModelBase<ItemResult<Class>, Class>
    {
        #region Fields

        private Class _selectedClass;

        private string _editedClassName;
        
        public string EditedClassName
        {
            get => _editedClassName;
            set => this.RaiseAndSetIfChanged(ref _editedClassName, value);
        }

        #endregion
        
        #region Commands
        
        public void OnEditCommandExecute(object parameter)
        {
            Close(new ItemResult<Class>(new Class() {Name = _editedClassName}));
        }

        public bool CanOnEditCommandExecute(object parameter)
        {
            string text = parameter as string;
            return !string.IsNullOrWhiteSpace(text) && text != _selectedClass.Name && text.Length < 256;
        }

        #endregion

        public EditClassDialogViewModel()
        {
            
        }

        public override void Activate(Class parameter)
        {
            _selectedClass = parameter;
            EditedClassName = _selectedClass.Name;
        }
    }
}