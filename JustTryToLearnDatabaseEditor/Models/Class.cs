using System.Collections.ObjectModel;
using JustTryToLearnDatabaseEditor.Models.Base;

namespace JustTryToLearnDatabaseEditor.Models
{
    public sealed class Class : NotifiedModel
    {
        private string? _name;
        
        public ObservableCollection<Theme> Themes { get; private set; }

        public string? Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        
        public void AddTheme(Theme themeToAdd)
        {
            Themes.Add(themeToAdd);
        }

        public Class()
        {
            Themes = new ObservableCollection<Theme>();
        }
    }
}
