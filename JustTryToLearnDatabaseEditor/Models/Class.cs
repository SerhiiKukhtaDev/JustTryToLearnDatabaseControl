using System;
using System.Collections.ObjectModel;
using JustTryToLearnDatabaseEditor.Models.Base;
using MongoDB.Bson;

namespace JustTryToLearnDatabaseEditor.Models
{
    public sealed class Class : Model<Subject>, INamedModel, IContainItems<Theme>
    {
        public ObjectId Id { get; set; }

        [NonSerialized] private Subject _subject;
        
        public Subject Subject => _subject;

        private string? _name;
        

        public string? Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public Class() : this(null)
        {
            
        }

        public Class(Subject subject)
        {
            Items = new ObservableCollection<Theme>();
            
            Id = ObjectId.GenerateNewId();
            _subject = subject;
        }

        public ObservableCollection<Theme> Items { get; set; }
        
        public override void SetParent(Subject parent)
        {
            _subject = parent;
        }
    }
}
