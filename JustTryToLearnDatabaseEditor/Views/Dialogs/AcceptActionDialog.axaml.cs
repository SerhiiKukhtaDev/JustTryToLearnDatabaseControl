using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using JustTryToLearnDatabaseEditor.Views.Dialogs.Base;

namespace JustTryToLearnDatabaseEditor.Views.Dialogs
{
    public class AcceptActionDialog : DialogWindowBase
    {
        public AcceptActionDialog()
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