using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Questions;
using JustTryToLearnDatabaseEditor.ViewModels.UserControls.Utils;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls
{
    public class QuestionControlViewModel : RelativeItemUserControlViewModel<Question, Theme>
    {
        private new const string AddItemViewModelName = nameof(AddQuestionDialogViewModel);
        private new const string EditItemViewModelName = nameof(EditQuestionDialogViewModel);

        private const string DeleteMessage = "Дійсно бажаєте видалити це питання?";
        
        public QuestionControlViewModel(IDialogService dialogService) : base(dialogService, DeleteMessage, 
            AddItemViewModelName, EditItemViewModelName)
        {
        }

        protected override void OnItemAdded(Question item, Theme parent)
        {
            ((IContainItems<Question>)parent).AddItem(item);
        }

        protected override void OnItemRemoved(Question item, Theme parent)
        {
            ((IContainItems<Question>)parent).RemoveItem(item);
        }

        protected override void OnItemEditRequested(Question item, Question newItem, Theme parent)
        {
            item.Answers = newItem.Answers;
            item.Difficulty = newItem.Difficulty;
            item.ItemName = newItem.ItemName;
            item.TimeToAnswer = newItem.TimeToAnswer;
        }
    }
}
