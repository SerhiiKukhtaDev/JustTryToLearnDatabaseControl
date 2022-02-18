using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JustTryToLearnDatabaseEditor.Views.UserControls
{
    public class ThemesControl : UserControl
    {
        public ThemesControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}