using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Subjects;
using JustTryToLearnDatabaseEditor.ViewModels.UserControls.Utils;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls
{
    public class SubjectsControlViewModel : SingleItemUserControlViewModel<Subject>
    {
        private new const string AddItemViewModelName = nameof(AddSubjectDialogViewModel);
        private new const string EditItemViewModelName = nameof(EditSubjectDialogViewModel);

        private const string DeleteMessage =
            "Видалення цього придмета призведе до видалення всіх питань які залежать від нього." +
            "Дійсно бажаєте видалити?";


        public SubjectsControlViewModel(IDialogService dialogService) : 
            base(dialogService, DeleteMessage, AddItemViewModelName, EditItemViewModelName)
        {
            
        }

        protected override void OnItemEdited(Subject item)
        {
            
        }

        protected override void OnItemAdded(Subject item)
        {
            
        }

        protected override void OnItemDeleted(Subject item)
        {
            
        }
    }
}
