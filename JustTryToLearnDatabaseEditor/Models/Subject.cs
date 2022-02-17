using JustTryToLearnDatabaseEditor.Models.Base;

namespace JustTryToLearnDatabaseEditor.Models
{
    public sealed class Subject : NotifiedModel
    {
        private string? _name;

        public string? Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
    }
}
