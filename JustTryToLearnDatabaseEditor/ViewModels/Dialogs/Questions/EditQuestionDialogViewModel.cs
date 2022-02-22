using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Models.Statics;
using JustTryToLearnDatabaseEditor.Services.Utils;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Questions
{
    public class EditQuestionDialogViewModel : ParameterizedDialogViewModelBase<ItemResult<Question>, Question>
    {
        private bool _isOnlyOneRightAnswer = true;

        public bool IsOnlyOneRightAnswer
        {
            get => _isOnlyOneRightAnswer;
            set => this.RaiseAndSetIfChanged(ref _isOnlyOneRightAnswer, value);
        }
        
        private int _sliderValue;

        public int SliderValue
        {
            get => _sliderValue;
            set => this.RaiseAndSetIfChanged(ref _sliderValue, value);
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

        public List<string> Difficulties => Difficulty.GetAllTypes();

        private string _difficultyTextText;
        
        public string DifficultyText
        {
            get => _difficultyTextText;
            set => this.RaiseAndSetIfChanged(ref _difficultyTextText, value);
        }
        
        private string _questionText;
        
        public string QuestionText
        {
            get => _questionText;
            set => this.RaiseAndSetIfChanged(ref _questionText, value);
        }

        private Question _currentQuestion;
        
        private readonly IObservable<bool> _allAnswersNotEmpty;
        private readonly IObservable<bool> _questionTextAndRightAnswerNotEmpty;

        public Question CurrentQuestion => _currentQuestion;

        public override void Activate(Question parameter)
        {
            if(Answers != null)
                Answers.Clear();

            _currentQuestion = parameter;

            SliderValue = _currentQuestion.TimeToAnswer;
            DifficultyText = _currentQuestion.Difficulty;
            Answers.AddRange(parameter.Answers);
            QuestionText = _currentQuestion.Name;
        }
        
        public void AddNewAnswerExecute(object parameter)
        {
            if (Answers.FirstOrDefault(answer => answer.IsRightAnswer) != null)
            {
                Answers.Add(new Answer());
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
        
        public void ToggleRightAnswer(Answer answer)
        {
            IsOnlyOneRightAnswer = Answers.Count(a => a.IsRightAnswer) == 1;
        }
        
        public ReactiveCommand<Unit, Unit> EditQuestionCommand { get; set; }

        public void EditQuestion()
        {
            var answers = Answers.ToList();
            
            foreach (var answer in answers)
            {
                answer.AnswerText = answer.AnswerText.NormalizeString();
            }

            Question question = new()
            {
                Answers = answers,
                Difficulty = DifficultyText,
                Name = QuestionText.NormalizeString(),
                TimeToAnswer = Clamp(_sliderValue, 0, 180)
            };
            
            Close(new ItemResult<Question>(question));
        }

        public EditQuestionDialogViewModel()
        {
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
            
            EditQuestionCommand = ReactiveCommand.Create(EditQuestion, canExecute);
        }

        private int Clamp(int value, int minValue, int maxValue)
        {
            if (value < minValue)
                return minValue;

            if (value > maxValue)
                return maxValue;

            return value;
        }
    }
}
