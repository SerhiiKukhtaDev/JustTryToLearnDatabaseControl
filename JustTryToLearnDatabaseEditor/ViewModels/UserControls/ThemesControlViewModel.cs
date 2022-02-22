using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Database;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Themes;
using JustTryToLearnDatabaseEditor.ViewModels.UserControls.Utils;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls
{
    public class ThemesControlViewModel : RelativeItemUserControlViewModel<Theme, Class>
    {
        private readonly IThemeService _themeService;
        
        private new const string AddItemViewModelName = nameof(AddThemeDialogViewModel);
        private new const string EditItemViewModelName = nameof(EditThemeDialogViewModel);

        private const string DeleteMessage =
            "Видалення цієї теми призведе до видалення всіх питань які залежать від неї. " +
            "Дійсно бажаєте видалити?";
        
        public ThemesControlViewModel(IDialogService dialogService, IThemeService themeService) : base(dialogService, DeleteMessage,
            AddItemViewModelName, EditItemViewModelName)
        {
            _themeService = themeService;
        }

        protected override void OnItemAdded(Theme item, Class parent)
        {
            item.SetParent(parent);
            
            ((IContainItems<Theme>)parent).AddItem(item);
            _themeService.InsertTheme(item);
        }

        protected override void OnItemEditRequested(Theme item, Theme newItem, Class parent)
        {
            item.Name = newItem.Name;
            _themeService.UpdateTheme(item);
        }


        protected override void OnItemRemoved(Theme item, Class parent)
        {
            _themeService.RemoveTheme(item);
            ((IContainItems<Theme>)parent).RemoveItem(item);
        }
    }
}
