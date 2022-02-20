using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Classes;
using JustTryToLearnDatabaseEditor.ViewModels.UserControls.Utils;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls
{
    public class ClassesControlViewModel : RelativeItemUserControlViewModel<Class, Subject>
    {
        private new const string AddItemViewModelName = nameof(AddClassDialogViewModel);
        private new const string EditItemViewModelName = nameof(EditClassDialogViewModel);

        private const string DeleteMessage =
            "Видалення цього класу призведе до видалення всіх питань які залежать від нього." +
            "Дійсно бажаєте видалити?";
        
        public ClassesControlViewModel(IDialogService dialogService) 
            : base(dialogService, DeleteMessage, AddItemViewModelName, EditItemViewModelName)
        {
        }

        protected override void OnItemAdded(Class item, Subject parent)
        {
            (parent as IContainItems<Class>).AddItem(item);
        }

        protected override void OnItemEditRequested(Class item, Class newItem, Subject parent)
        {
            
        }

        protected override void OnItemRemoved(Class item, Subject parent)
        {
            
        }
    }
}
