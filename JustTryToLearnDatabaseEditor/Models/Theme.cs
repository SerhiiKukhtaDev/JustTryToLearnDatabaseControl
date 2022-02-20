using System.Collections.ObjectModel;
using System.Linq;
using JustTryToLearnDatabaseEditor.Models.Base;

namespace JustTryToLearnDatabaseEditor.Models
{
    public sealed class Theme : NotifiedModel, INamedModel
    {
        private string? _name;
        
        public ObservableCollection<Question> Questions { get; private set; }

        public string? ItemName
        {
            get => _name;
            set => Set(ref _name, value);
        }
        
        public void AddQuestion(Question questionToAdd) 
        {
            Questions.Add(questionToAdd);
        }

        public void RemoveQuestion(Question question)
        {
            Questions.Remove(question);
        }

        public Theme()
        {
            Questions = new ObservableCollection<Question>();
        }
    }
}
