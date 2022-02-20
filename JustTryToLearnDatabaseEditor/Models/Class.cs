using System.Collections.ObjectModel;
using JustTryToLearnDatabaseEditor.Models.Base;

namespace JustTryToLearnDatabaseEditor.Models
{
    public sealed class Class : NotifiedModel, INamedModel
    {
        private string? _name;
        
        public ObservableCollection<Theme> Themes { get; private set; }

        public string? ItemName
        {
            get => _name;
            set => Set(ref _name, value);
        }
        
        public void AddTheme(Theme themeToAdd)
        {
            Themes.Add(themeToAdd);
        }

        public void RemoveTheme(Theme theme)
        {
            Themes.Remove(theme);
        }

        public Class()
        {
            Themes = new ObservableCollection<Theme>();
        }
    }
}
