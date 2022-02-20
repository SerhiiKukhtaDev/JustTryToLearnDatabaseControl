using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Questions;
using JustTryToLearnDatabaseEditor.ViewModels.UserControls.Utils;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls
{
    public class QuestionControlViewModel : RelativeItemUserControlViewModel<Question, Theme>
    {
        private const string AddItemViewModelName = nameof(AddQuestionDialogViewModel);
        private const string EditItemViewModelName = nameof(EditQuestionDialogViewModel);

        private const string DeleteMessage = "Дійсно бажаєте видалити це питання?";
        
        public QuestionControlViewModel(IDialogService dialogService) : base(dialogService, DeleteMessage, 
            AddItemViewModelName, EditItemViewModelName)
        {
        }

        protected override void OnItemAdded(Question item, Theme parent)
        {
            
        }

        protected override void OnItemRemoved(Question item, Theme parent)
        {
            
        }

        protected override void OnItemEditRequested(Question item, Question newItem, Theme parent)
        {
            
        }
    }
}
