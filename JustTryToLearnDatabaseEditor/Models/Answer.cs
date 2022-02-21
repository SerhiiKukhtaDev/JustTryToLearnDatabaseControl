using JustTryToLearnDatabaseEditor.Models.Base;

namespace JustTryToLearnDatabaseEditor.Models
{
    public class Answer : NotifiedModel
    {
        public Answer Element => this;
        
        private bool _isEnabled = true;
        private bool _isRightAnswer;
        private string _answerText;

        public bool IsEnabled
        {
            get => _isEnabled;
            set => Set(ref _isEnabled, value);
        }

        public bool IsRightAnswer
        {
            get => _isRightAnswer;
            set => Set(ref _isRightAnswer, value);
        }

        public string AnswerText
        {
            get => _answerText;
            set => Set(ref _answerText, value);
        }

        public Answer()
        {
            IsRightAnswer = false;
        }
    }
}
