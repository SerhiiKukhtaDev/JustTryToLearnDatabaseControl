using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Questions;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Themes;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls
{
    public class QuestionControlViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private Theme _currentTheme;
        
        private ObservableCollection<Question> _questions;

        public ObservableCollection<Question> Questions
        {
            get => _questions;
            set => this.RaiseAndSetIfChanged(ref _questions, value);
        }

        private Question _selectedQuestion;
        
        public Question SelectedQuestion
        {
            get => _selectedQuestion;
            set => this.RaiseAndSetIfChanged(ref _selectedQuestion, value);
        }

        public void SetQuestionsBy(Theme selectedTheme)
        {
            _currentTheme = selectedTheme;
            Questions = _currentTheme.Questions;
        }

        public QuestionControlViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            Questions = new ObservableCollection<Question>();
            
            AddQuestionCommand = ReactiveCommand.CreateFromTask(OnAddQuestionCommandExecute);
        }
        
        public ReactiveCommand<Unit, Unit> AddQuestionCommand { get; }

        private async Task OnAddQuestionCommandExecute()
        {
            var result = await _dialogService.ShowDialogAsync<ItemResult<Question>,
                ObservableCollection<Question>>(nameof(AddQuestionDialogViewModel), Questions);

            if (result != null)
            {
                Questions.Add(result.Item);
            }
        }

        public async Task DeleteQuestionCommand(object parameter)
        {
            string message = "Дійсно бажаєте видалити це питання?";
            
            var result = await _dialogService.ShowDialogAsync<DialogResultBase, string>
                (nameof(AcceptActionDialogViewModel), message);

            if (result != null)
            {
                var index = Questions.IndexOf(_selectedQuestion);
                Questions.Remove(_selectedQuestion);
            
                if (Questions.Count > 0)
                    SelectedQuestion = Questions[index == 0 ? index :  index - 1];
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
                _selectedQuestion.Name = result.Item.Name;
            }
        }

        bool CanEditQuestionCommand(/* CommandParameter */object parameter)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return parameter != null;
        }
    }
}