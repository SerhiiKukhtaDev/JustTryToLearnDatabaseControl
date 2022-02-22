using System.Collections.ObjectModel;
using MongoDB.Bson;

namespace JustTryToLearnDatabaseEditor.Models
{
    public class School : IContainItems<Subject>
    {
        public ObjectId Id { get; set; }
        
        public string Name { get; set; }
        
        public ObservableCollection<Subject> Items { get; set; }

        public School()
        {
            Id = ObjectId.GenerateNewId();
            Items = new ObservableCollection<Subject>();
        }
    }
}
