using System;
using System.Collections.ObjectModel;
using JustTryToLearnDatabaseEditor.Models.Base;
using MongoDB.Bson;

namespace JustTryToLearnDatabaseEditor.Models
{
    public sealed class Subject : Model<School>, INamedModel, IContainItems<Class>
    {
        public ObjectId Id { get; set; }

        [NonSerialized] 
        private School _school;

        public School School => _school;
        
        private string? _name;
        
        public string? Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public Subject() : this(null)
        {
            
        }

        public Subject(School school)
        {
            Items = new ObservableCollection<Class>();
            
            Id = ObjectId.GenerateNewId();
            _school = school;
        }

        public ObservableCollection<Class> Items { get; set; }

        public override void SetParent(School parent)
        {
            _school = parent;
        }
    }
}
