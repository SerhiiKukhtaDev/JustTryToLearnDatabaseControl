using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JustTryToLearnDatabaseEditor.Views.UserControls
{
    public class QuestionsUserControl : UserControl
    {
        public QuestionsUserControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}