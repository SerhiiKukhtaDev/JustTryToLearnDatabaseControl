using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Questions
{
    public class EditQuestionDialogViewModel : ParameterizedDialogViewModelBase<ItemResult<Question>, Question>
    {
        #region Fields

        private Question _selectedQuestion;

        private string _editedQuestionName;

        public string EditedQuestionName
        {
            get => _editedQuestionName;
            set => this.RaiseAndSetIfChanged(ref _editedQuestionName, value);
        }

        #endregion

        #region Commands

        public void OnEditCommandExecute(object parameter)
        {
            Close(new ItemResult<Question>(new Question() {Name = _editedQuestionName}));
        }

        public bool CanOnEditCommandExecute(object parameter)
        {
            string text = parameter as string;
            return !string.IsNullOrWhiteSpace(text) && text != _selectedQuestion.Name && text.Length < 256;
        }

        #endregion

        public override void Activate(Question parameter)
        {
            _selectedQuestion = parameter;
            EditedQuestionName = _selectedQuestion.Name;
        }
    }
}