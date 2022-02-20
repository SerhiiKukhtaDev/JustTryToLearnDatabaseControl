using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using DynamicData;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Questions;
using JustTryToLearnDatabaseEditor.ViewModels.UserControls.Utils;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls
{
    public class QuestionControlViewModel : SingleItemUserControlViewModel<,>
    {
        public event Action ReturnRequested;
        
        private readonly IDialogService _dialogService;
        private Theme _currentTheme;

        private Question _selectedQuestion;
        
        public Question SelectedQuestion
        {
            get => _selectedQuestion;
            set => this.RaiseAndSetIfChanged(ref _selectedQuestion, value);
        }

        public void SetQuestionsBy(Theme selectedTheme)
        {
            _currentTheme = selectedTheme;
            
            AllItems.Clear();
            AllItems.AddRange(_currentTheme.Questions);
        }

        public QuestionControlViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            AddQuestionCommand = ReactiveCommand.CreateFromTask(OnAddQuestionCommandExecute);
        }
        
        public ReactiveCommand<Unit, Unit> AddQuestionCommand { get; }

        private async Task OnAddQuestionCommandExecute()
        {
            var result = await _dialogService.ShowDialogAsync<ItemResult<Question>,
                IEnumerable<Question>>(nameof(AddQuestionDialogViewModel), AllItems.Items);

            if (result != null)
            {
                _currentTheme.AddQuestion(result.Item);
                AllItems.Add(result.Item);
            }
        }

        public async Task DeleteQuestionCommand(object parameter)
        {
            string message = "Дійсно бажаєте видалити це питання?";
            
            var result = await _dialogService.ShowDialogAsync<DialogResultBase, string>
                (nameof(AcceptActionDialogViewModel), message);

            if (result != null)
            {
                var index = Items.IndexOf(SelectedQuestion);
                _currentTheme.RemoveQuestion(SelectedQuestion);
                AllItems.Remove(SelectedQuestion);

                if (Items.Count > 0)
                    SelectedQuestion = Items[index == 0 ? index :  index - 1];
            }
        }

        bool CanDeleteQuestionCommand(/* CommandParameter */object parameter)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return parameter != null;
        }

        public async Task EditQuestionCommand(object parameter)
        {
            var result = await _dialogService.ShowDialogAsync<ItemResult<Question>, Question>
                (nameof(EditQuestionDialogViewModel), SelectedQuestion);

            if (result != null)
            {
                var item = result.Item;
                
                SelectedQuestion.Answers = item.Answers;
                SelectedQuestion.Difficulty = item.Difficulty;
                SelectedQuestion.ItemName = item.ItemName;
                SelectedQuestion.TimeToAnswer = item.TimeToAnswer;
            }
        }

        bool CanEditQuestionCommand(/* CommandParameter */object parameter)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return parameter != null;
        }
        
        public void OnReturnRequestedExecute()
        {
            ReturnRequested?.Invoke();
        }
    }
}
