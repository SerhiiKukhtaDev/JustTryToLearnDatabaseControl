﻿using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Subjects;
using JustTryToLearnDatabaseEditor.ViewModels.UserControls.Utils;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls
{
    public class SubjectsControlViewModel : SingleItemUserControlViewModel<Subject>
    {
        private const string AddItemViewModelName = nameof(AddSubjectDialogViewModel);
        private const string EditItemViewModelName = nameof(EditSubjectDialogViewModel);

        private const string DeleteMessage =
            "Видалення цього придмета призведе до видалення всіх питань які залежать від нього." +
            "Дійсно бажаєте видалити?";
        
        

        public SubjectsControlViewModel(IDialogService dialogService) : 
            base(dialogService, DeleteMessage, AddItemViewModelName, EditItemViewModelName)
        {
            
        }
    }
}
