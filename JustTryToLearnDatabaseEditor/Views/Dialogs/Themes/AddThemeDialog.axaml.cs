using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using JustTryToLearnDatabaseEditor.Views.Dialogs.Base;

namespace JustTryToLearnDatabaseEditor.Views.Dialogs.Themes
{
    public class AddThemeDialog : DialogWindowBase<ItemResult<Theme>>
    {
        public AddThemeDialog()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}