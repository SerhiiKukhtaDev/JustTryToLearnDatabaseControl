using System;
using System.Collections;
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

        private IEnumerable<Question> _questions;

        public void AddNewQuestion(object parameter)
        {
            Question question = new Question()
            {
                ItemName = QuestionText,
                Answers = Answers.ToList(),
                Difficulty = SelectedDifficulty,
                TimeToAnswer = SliderValue
            };
            
            Close(new ItemResult<Question>(question));
        }

        public bool CanAddNewQuestion(object parameter)
        {
            return true;
        }

        public void ChooseRightAnswer(Answer answer)
        {
            if (answer.IsRightAnswer)
            {
                foreach (var a in Answers.Where(an => !an.Equals(answer)))
                {
                    a.IsEnabled = true;
                }

                answer.IsRightAnswer = false;
                return;
            }

            answer.IsRightAnswer = true;

            foreach (var a in Answers.Where(an => !an.Equals(answer)))
            {
                a.IsEnabled = false;
            }
        }

        public AddQuestionDialogViewModel()
        {
            DifficultyTypes = new ObservableCollection<string>(Difficulty.GetAllTypes());
        }

        public override void Activate(IEnumerable<Question> parameter)
        {
            _questions = parameter;

            Answers = new ObservableCollection<Answer>();
            
            Answers.Add(new Answer());
            Answers.Add(new Answer());
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
    }
}
