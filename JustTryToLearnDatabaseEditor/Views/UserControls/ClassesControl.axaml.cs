using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JustTryToLearnDatabaseEditor.Views.UserControls
{
    public class ClassesControl : UserControl
    {
        public ClassesControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}