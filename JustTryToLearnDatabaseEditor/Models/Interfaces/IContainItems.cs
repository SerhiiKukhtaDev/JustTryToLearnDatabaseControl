using System.Collections.ObjectModel;
using JustTryToLearnDatabaseEditor.Models.Base;

namespace JustTryToLearnDatabaseEditor.Models
{
    public interface IContainItems<T> where T : NotifiedModel, INamedModel
    {
        ObservableCollection<T> Items { get; set; }

        void AddItem(T item)
        {
            Items.Add(item);
        }

        void RemoveItem(T item)
        {
            Items.Remove(item);
        }
    }
}
