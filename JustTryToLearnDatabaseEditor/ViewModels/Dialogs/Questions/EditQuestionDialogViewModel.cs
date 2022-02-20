using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Models.Statics;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Questions
{
    public class EditQuestionDialogViewModel : ParameterizedDialogViewModelBase<ItemResult<Question>, Question>
    {
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
        
        public Question CurrentQuestion => _currentQuestion;

        public override void Activate(Question parameter)
        {
            _currentQuestion = parameter;

            SliderValue = _currentQuestion.TimeToAnswer;
            DifficultyText = _currentQuestion.Difficulty;
            Answers = new ObservableCollection<Answer>(parameter.Answers);
            QuestionText = _currentQuestion.ItemName;
        }
        
        public void AddNewAnswerExecute(object parameter)
        {
            if (Answers.FirstOrDefault(answer => answer.IsRightAnswer) != null)
            {
                Answers.Add(new Answer() {IsEnabled = false});
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
            if (!answer.IsRightAnswer)
            {
                foreach (var a in Answers)
                {
                    a.IsEnabled = true;
                }
                
                return;
            }

            foreach (var a in Answers.Where(an => !an.Equals(answer)))
            {
                a.IsEnabled = false;
            }
        }

        public void EditQuestion()
        {
            Question question = new Question()
            {
                Answers = Answers.ToList(),
                Difficulty = DifficultyText,
                ItemName = QuestionText,
                TimeToAnswer = Clamp(_sliderValue, 0, 180)
            };
            
            Close(new ItemResult<Question>(question));
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
