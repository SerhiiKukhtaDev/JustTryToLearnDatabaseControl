using System;
using System.Collections.ObjectModel;
using System.Linq;
using JustTryToLearnDatabaseEditor.Models.Base;
using MongoDB.Bson;

namespace JustTryToLearnDatabaseEditor.Models
{
    public sealed class Theme : Model<Class>, INamedModel, IContainItems<Question>
    {
        public ObjectId Id { get; set; }

        [NonSerialized] private Class _class;
        
        public Class Class => _class;

        private string? _name;
        

        public string? Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        
        public Theme() : this(null)
        {
            Items = new ObservableCollection<Question>();
        }

        public Theme(Class @class)
        {
            Items = new ObservableCollection<Question>();
            
            _class = @class;
            Id = ObjectId.GenerateNewId();
        }

        public ObservableCollection<Question> Items { get; set; }
        public override void SetParent(Class parent)
        {
            _class = parent;
        }
    }
}
