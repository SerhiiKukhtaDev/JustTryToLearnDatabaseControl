using System.Collections.ObjectModel;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Questions
{
    public class AddQuestionDialogViewModel : 
        ParameterizedDialogViewModelBase<ItemResult<Question>, ObservableCollection<Question>>
    {
        private ObservableCollection<Question> _questions;

        public void AddNewQuestion(object parameter)
        {
            string name = parameter as string;
            
            Close(new ItemResult<Question>(new Question {Name = name}));
        }

        public bool CanAddNewQuestion(object parameter)
        {
            return true;
        }

        public override void Activate(ObservableCollection<Question> parameter)
        {
            _questions = parameter;
        }
    }
}
