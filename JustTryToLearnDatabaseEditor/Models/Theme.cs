using System.Collections.ObjectModel;
using JustTryToLearnDatabaseEditor.Models.Base;

namespace JustTryToLearnDatabaseEditor.Models
{
    public sealed class Theme : NotifiedModel
    {
        private string? _name;
        
        public ObservableCollection<Question> Questions { get; private set; }

        public string? Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        
        public void AddQuestion(Question questionToAdd) 
        {
            Questions.Add(questionToAdd);
        }

        public Theme()
        {
            Questions = new ObservableCollection<Question>();
        }
    }
}
