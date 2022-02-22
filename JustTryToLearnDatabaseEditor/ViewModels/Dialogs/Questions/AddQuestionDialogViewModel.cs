using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using JetBrains.Annotations;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Models.Statics;
using JustTryToLearnDatabaseEditor.Services.Utils;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Questions
{
    public class AddQuestionDialogViewModel : 
        ParameterizedDialogViewModelBase<ItemResult<Question>, IEnumerable<Question>>
    {
        private string _questionText;

        [NotNull]
        public string QuestionText
        {
            get => _questionText;
            set => this.RaiseAndSetIfChanged(ref _questionText, value);
        }

        private bool _isOnlyOneRightAnswer;

        public bool IsOnlyOneRightAnswer
        {
            get => _isOnlyOneRightAnswer;
            set => this.RaiseAndSetIfChanged(ref _isOnlyOneRightAnswer, value);
        }

        private int _sliderValue = 30;

        public int SliderValue
        {
            get => _sliderValue;
            set => this.RaiseAndSetIfChanged(ref _sliderValue, value);
        }

        private ObservableCollection<string> _difficultyTypes;

        public ObservableCollection<string> DifficultyTypes
        {
            get => _difficultyTypes;
            set => this.RaiseAndSetIfChanged(ref _difficultyTypes, value);
        }

        private string _selectedDifficulty = Difficulty.GetDefault();
        
        public string SelectedDifficulty
        {
            get => _selectedDifficulty;
            set => this.RaiseAndSetIfChanged(ref _selectedDifficulty, value);
        }

        private Answer _selectedAnswer;
        
        public Answer SelectedAnswer
        {
            get => _selectedAnswer;
            set => this.RaiseAndSetIfChanged(ref _selectedAnswer, value);
        }

        private ObservableCollection<Answer> _answers;

        public ObservableCollection<Answer> Answers
        {
            get => _answers;
            set => this.RaiseAndSetIfChanged(ref _answers, value);
        }

        public ReactiveCommand<Unit, Unit> AddNewQuestionCommand { get; set; }

        public void ChooseRightAnswer(Answer answer)
        {
            IsOnlyOneRightAnswer = Answers.Count(a => a.IsRightAnswer) == 1;
        }

        public void AddNewQuestion()
        {
            var answers = Answers.ToList();
            
            foreach (var answer in answers)
            {
                answer.AnswerText = answer.AnswerText.NormalizeString();
            }

            Question question = new Question()
            {
                Name = QuestionText.NormalizeString(),
                Answers = answers,
                Difficulty = SelectedDifficulty,
                TimeToAnswer = SliderValue
            };

            Close(new ItemResult<Question>(question));
        }

        private readonly IObservable<bool> _allAnswersNotEmpty;
        private readonly IObservable<bool> _questionTextAndRightAnswerNotEmpty;

        public AddQuestionDialogViewModel()
        {
            DifficultyTypes = new ObservableCollection<string>(Difficulty.GetAllTypes());
            Answers = new ObservableCollection<Answer>();

            _allAnswersNotEmpty = Answers
                .ToObservableChangeSet()
                .AutoRefresh(model => model.AnswerText)
                .ToCollection()                   
                .Select(x => x.All(y => !string.IsNullOrEmpty(y.AnswerText)));


            _questionTextAndRightAnswerNotEmpty = this.WhenAnyValue(
                x => x.IsOnlyOneRightAnswer,
                x => x.QuestionText,
                (answer, text) => answer && !string.IsNullOrEmpty(text));

            var canExecute = this.WhenAnyObservable(
                x => x._allAnswersNotEmpty,
                x => x._questionTextAndRightAnswerNotEmpty, (b, b1) => b && b1);

            AddNewQuestionCommand = ReactiveCommand.Create(AddNewQuestion, canExecute);
        }

        public override void Activate(IEnumerable<Question> parameter)
        {
            Answers.Add(new Answer());
            Answers.Add(new Answer());
        }

        public void AddNewAnswerExecute(object parameter)
        {
            if (Answers.FirstOrDefault(answer => answer.IsRightAnswer) != null)
            {
                Answers.Add(new Answer()); //{IsEnabled = false});
                return;
            }
            
            Answers.Add(new Answer());
        }

        public bool CanAddNewAnswerExecute(object parameter)
        {
            return (int)parameter < 5;
        }

        public void DeleteNewAnswerExecute(object parameter)
        {
            Answers.Remove(parameter as Answer);
        }

        public bool CanDeleteNewAnswerExecute(object parameter)
        {
            return parameter != null && Answers.Count > 2 && !(parameter as Answer).IsRightAnswer;
        }

        public void TextBoxTapped(Answer answer)
        {
            SelectedAnswer = answer;
        }
    }
}
