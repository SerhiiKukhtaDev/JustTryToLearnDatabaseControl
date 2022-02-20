using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Themes;
using JustTryToLearnDatabaseEditor.ViewModels.UserControls.Utils;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls
{
    public class ThemesControlViewModel : RelativeItemUserControlViewModel<Theme, Class>
    {
        private const string AddItemViewModelName = nameof(AddThemeDialogViewModel);
        private const string EditItemViewModelName = nameof(EditThemeDialogViewModel);

        private const string DeleteMessage =
            "Видалення цієї теми призведе до видалення всіх питань які залежать від неї." +
            "Дійсно бажаєте видалити?";
        
        public ThemesControlViewModel(IDialogService dialogService) : base(dialogService, DeleteMessage,
            AddItemViewModelName, EditItemViewModelName)
        {
        }

        protected override void OnItemAdded(Theme item, Class parent)
        {
            
        }

        protected override void OnItemEditRequested(Theme item, Theme newItem, Class parent)
        {
            
        }


        protected override void OnItemRemoved(Theme item, Class parent)
        {
            
        }
    }
}
