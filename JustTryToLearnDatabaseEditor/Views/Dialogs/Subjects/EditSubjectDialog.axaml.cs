using Avalonia;
using Avalonia.Markup.Xaml;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using JustTryToLearnDatabaseEditor.Views.Dialogs.Base;

namespace JustTryToLearnDatabaseEditor.Views.Dialogs.Subjects
{
    public class EditSubjectDialog : DialogWindowBase<ItemResult<Subject>>
    {
        public EditSubjectDialog()
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