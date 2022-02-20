using System.Collections.ObjectModel;
using JustTryToLearnDatabaseEditor.Models.Base;

namespace JustTryToLearnDatabaseEditor.Models
{
    public sealed class Subject : NotifiedModel, INamedModel, IContainItems<Class>
    {
        private string? _name;

        public string? ItemName
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public Subject()
        {
            Items = new ObservableCollection<Class>();
        }

        public ObservableCollection<Class> Items { get; set; }
    }
}
