using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Themes;
using JustTryToLearnDatabaseEditor.ViewModels.UserControls.Utils;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls
{
    public class ThemesControlViewModel : RelativeItemUserControlViewModel<Theme, Class>
    {
        private new const string AddItemViewModelName = nameof(AddThemeDialogViewModel);
        private new const string EditItemViewModelName = nameof(EditThemeDialogViewModel);

        private const string DeleteMessage =
            "Видалення цієї теми призведе до видалення всіх питань які залежать від неї." +
            "Дійсно бажаєте видалити?";
        
        public ThemesControlViewModel(IDialogService dialogService) : base(dialogService, DeleteMessage,
            AddItemViewModelName, EditItemViewModelName)
        {
        }

        protected override void OnItemAdded(Theme item, Class parent)
        {
            ((IContainItems<Theme>)parent).AddItem(item);
        }

        protected override void OnItemEditRequested(Theme item, Theme newItem, Class parent)
        {
            item.ItemName = newItem.ItemName;
        }


        protected override void OnItemRemoved(Theme item, Class parent)
        {
            ((IContainItems<Theme>)parent).RemoveItem(item);
        }
    }
}
