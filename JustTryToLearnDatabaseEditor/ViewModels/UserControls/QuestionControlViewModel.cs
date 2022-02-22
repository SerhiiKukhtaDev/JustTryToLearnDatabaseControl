using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Database;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Questions;
using JustTryToLearnDatabaseEditor.ViewModels.UserControls.Utils;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls
{
    public class QuestionControlViewModel : RelativeItemUserControlViewModel<Question, Theme>
    {
        private readonly IQuestionService _questionService;
        private new const string AddItemViewModelName = nameof(AddQuestionDialogViewModel);
        private new const string EditItemViewModelName = nameof(EditQuestionDialogViewModel);

        private const string DeleteMessage = "Дійсно бажаєте видалити це питання?";
        
        public QuestionControlViewModel(IDialogService dialogService, IQuestionService questionService) : base(dialogService, DeleteMessage, 
            AddItemViewModelName, EditItemViewModelName)
        {
            _questionService = questionService;
        }

        protected override void OnItemAdded(Question item, Theme parent)
        {
            item.SetParent(parent);
            
            ((IContainItems<Question>)parent).AddItem(item);
            _questionService.InsertQuestion(item);
        }

        protected override void OnItemRemoved(Question item, Theme parent)
        {
            _questionService.RemoveQuestion(item);
            ((IContainItems<Question>)parent).RemoveItem(item);
        }

        protected override void OnItemEditRequested(Question item, Question newItem, Theme parent)
        {
            item.Answers = newItem.Answers;
            item.Difficulty = newItem.Difficulty;
            item.Name = newItem.Name;
            item.TimeToAnswer = newItem.TimeToAnswer;

            _questionService.UpdateQuestion(item);
        }
    }
}
