using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs
{
    public class EditSubjectDialogViewModel : ParameterizedDialogViewModelBase<ItemResult<Subject>, Subject>
    {
        #region Fields

        private Subject _selectedSubject;

        private string _editedSubjectName;
        
        public string EditedSubjectName
        {
            get => _editedSubjectName;
            set => this.RaiseAndSetIfChanged(ref _editedSubjectName, value);
        }

        #endregion
        
        #region Commands
        
        public void OnEditCommandExecute(object parameter)
        {
            Close(new ItemResult<Subject>(new Subject() {Name = _editedSubjectName}));
        }

        public bool CanOnEditCommandExecute(object parameter)
        {
            string text = parameter as string;
            return !string.IsNullOrWhiteSpace(text) && text != _selectedSubject.Name && text.Length < 256;
        }

        #endregion

        public EditSubjectDialogViewModel()
        {
            
        }

        public override void Activate(Subject parameter)
        {
            _selectedSubject = parameter;
            EditedSubjectName = _selectedSubject.Name;
        }
    }
}
