﻿using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Models.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base
{
    public class EditSingleSubjectDialogViewModel<T> : ParameterizedDialogViewModelBase<ItemResult<T>, T>
    where T : NotifiedModel, INamedModel, new()
    {
        private T _selectedItem;

        private string _editedItemName;
        
        public string EditedItemName
        {
            get => _editedItemName;
            set => this.RaiseAndSetIfChanged(ref _editedItemName, value);
        }

        public void OnEditCommandExecute(object parameter)
        {
            Close(new ItemResult<T>(new T() {ItemName = _editedItemName}));
        }

        public bool CanOnEditCommandExecute(object parameter)
        {
            string text = parameter as string;
            return !string.IsNullOrWhiteSpace(text) && text != _selectedItem.ItemName && text.Length < 256;
        }

        public override void Activate(T parameter)
        {
            _selectedItem = parameter;
            EditedItemName = _selectedItem.ItemName;
        }
    }
}