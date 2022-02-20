using System.Collections.ObjectModel;
using System.Linq;
using JustTryToLearnDatabaseEditor.Models.Base;

namespace JustTryToLearnDatabaseEditor.Models
{
    public sealed class Theme : NotifiedModel, INamedModel, IContainItems<Question>
    {
        private string? _name;

        public string? ItemName
        {
            get => _name;
            set => Set(ref _name, value);
        }
        
        public Theme()
        {
            Items = new ObservableCollection<Question>();
        }

        public ObservableCollection<Question> Items { get; set; }
    }
}
