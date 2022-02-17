using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using JustTryToLearnDatabaseEditor.Services.Providers.Interfaces;

namespace JustTryToLearnDatabaseEditor.Services.Providers
{
    public class MainWindowProvider : IMainWindowProvider
    {
        public Window GetMainWindow()
        {
            var lifetime = (IClassicDesktopStyleApplicationLifetime) Application.Current.ApplicationLifetime;

            return lifetime.MainWindow;
        }
    }
}