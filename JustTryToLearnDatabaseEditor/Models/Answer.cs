using JustTryToLearnDatabaseEditor.Models.Base;

namespace JustTryToLearnDatabaseEditor.Models
{
    public class Answer : NotifiedModel
    {
        public Answer Element => this;
        
        private bool _isEnabled = true;

        public bool IsEnabled
        {
            get => _isEnabled;
            set => Set(ref _isEnabled, value);
        }

        public bool IsRightAnswer { get; set; }
        
        public string AnswerText { get; set; }

        public Answer()
        {
            IsRightAnswer = false;
        }
    }
}
