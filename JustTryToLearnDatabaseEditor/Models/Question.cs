using System.Collections.Generic;
using JustTryToLearnDatabaseEditor.Models.Base;

namespace JustTryToLearnDatabaseEditor.Models
{
    public class Question : NotifiedModel, INamedModel
    {
        private string? _question;

        public string? ItemName
        {
            get => _question;
            set => Set(ref _question, value);
        }
        
        public List<Answer> Answers { get; set; }

        public string Difficulty { get; set; }
        
        public int TimeToAnswer { get; set; }
    }
}
