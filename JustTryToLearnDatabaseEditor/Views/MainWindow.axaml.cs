using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JustTryToLearnDatabaseEditor.Views
{
    public partial class MainWindow : Window
    {
        private Grid OverlayGrid => this.FindControl<Grid>("OverlayGrid");
        
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        
        public void ShowOverlay() => OverlayGrid.ZIndex = 0;

        public void HideOverlay() => OverlayGrid.ZIndex = 0;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
