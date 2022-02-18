﻿using System.Collections.ObjectModel;
using JustTryToLearnDatabaseEditor.Models.Base;

namespace JustTryToLearnDatabaseEditor.Models
{
    public sealed class Subject : NotifiedModel
    {
        private string? _name;
        
        public ObservableCollection<Class> Classes { get; private set; }

        public string? Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        
        public void AddClass(Class classToAdd)
        {
            
            Classes.Add(classToAdd);
        }

        public Subject()
        {
            Classes = new ObservableCollection<Class>();
        }
    }
}
