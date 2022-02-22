using System;
using System.Collections.Generic;
using JustTryToLearnDatabaseEditor.Models.Base;
using MongoDB.Bson;

namespace JustTryToLearnDatabaseEditor.Models
{
    public class Question : Model<Theme>, INamedModel
    {
        public ObjectId Id { get; set; }

        [NonSerialized] private Theme _theme;
        
        public Theme Theme => _theme;

        private string? _question;
        

        public string? Name
        {
            get => _question;
            set => Set(ref _question, value);
        }

        public Question() : this(null)
        {
            
        }

        public Question(Theme theme)
        {
            _theme = theme;
            Id = ObjectId.GenerateNewId();
        }
        
        public List<Answer> Answers { get; set; }

        public string Difficulty { get; set; }
        
        public int TimeToAnswer { get; set; }
        
        public override void SetParent(Theme parent)
        {
            _theme = parent;
        }
    }
}
