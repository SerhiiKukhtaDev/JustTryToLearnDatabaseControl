using System.Collections.ObjectModel;
using JustTryToLearnDatabaseEditor.Models.Base;

namespace JustTryToLearnDatabaseEditor.Models
{
    public class Question : NotifiedModel
    {
        private string? _name;

        public string? Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
    }
}
