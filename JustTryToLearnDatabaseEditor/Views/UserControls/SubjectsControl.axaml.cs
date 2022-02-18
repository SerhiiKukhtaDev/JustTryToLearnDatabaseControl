using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JustTryToLearnDatabaseEditor.Views.UserControls
{
    public class SubjectsControl : UserControl
    {
        public SubjectsControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }

}
