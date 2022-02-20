using System.Collections.ObjectModel;
using JustTryToLearnDatabaseEditor.Models.Base;

namespace JustTryToLearnDatabaseEditor.Models
{
    public sealed class Subject : NotifiedModel, INamedModel
    {
        private string? _name;
        
        public ObservableCollection<Class> Classes { get; private set; }

        public string? ItemName
        {
            get => _name;
            set => Set(ref _name, value);
        }
        
        public void AddClass(Class classToAdd)
        {
            Classes.Add(classToAdd);
        }

        public void RemoveClass(Class @class)
        {
            Classes.Remove(@class);
        }

        public Subject()
        {
            Classes = new ObservableCollection<Class>();
        }
    }
}
