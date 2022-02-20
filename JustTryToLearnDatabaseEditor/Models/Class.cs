using System.Collections.ObjectModel;
using JustTryToLearnDatabaseEditor.Models.Base;

namespace JustTryToLearnDatabaseEditor.Models
{
    public sealed class Class : NotifiedModel, INamedModel, IContainItems<Theme>
    {
        private string? _name;

        public string? ItemName
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public Class()
        {
            Items = new ObservableCollection<Theme>();
        }

        public ObservableCollection<Theme> Items { get; set; }
    }
}
