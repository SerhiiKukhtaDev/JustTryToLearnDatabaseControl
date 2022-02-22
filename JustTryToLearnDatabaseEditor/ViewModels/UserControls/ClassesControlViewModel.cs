using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Database;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Classes;
using JustTryToLearnDatabaseEditor.ViewModels.UserControls.Utils;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls
{
    public class ClassesControlViewModel : RelativeItemUserControlViewModel<Class, Subject>
    {
        private readonly IClassService _classService;
        
        private new const string AddItemViewModelName = nameof(AddClassDialogViewModel);
        private new const string EditItemViewModelName = nameof(EditClassDialogViewModel);

        private const string DeleteMessage =
            "Видалення цього класу призведе до видалення всіх питань які залежать від нього. " +
            "Дійсно бажаєте видалити?";
        
        public ClassesControlViewModel(IDialogService dialogService, IClassService classService) 
            : base(dialogService, DeleteMessage, AddItemViewModelName, EditItemViewModelName)
        {
            _classService = classService;
        }

        protected override void OnItemAdded(Class item, Subject parent)
        {
            item.SetParent(parent);

            (parent as IContainItems<Class>).AddItem(item);
            _classService.InsertClass(item);
        }

        protected override void OnItemEditRequested(Class item, Class newItem, Subject parent)
        {
            item.Name = newItem.Name;
            _classService.UpdateClass(item);
        }

        protected override void OnItemRemoved(Class item, Subject parent)
        {
            _classService.RemoveClass(item);
            (parent as IContainItems<Class>).RemoveItem(item);
        }
    }
}
