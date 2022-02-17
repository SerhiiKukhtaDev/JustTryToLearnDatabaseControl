using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using JustTryToLearnDatabaseEditor.Services.DI;
using JustTryToLearnDatabaseEditor.ViewModels;
using JustTryToLearnDatabaseEditor.Views;
using Splat;

namespace JustTryToLearnDatabaseEditor
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                RegisterDependencies();
                
                DataContext = GetRequiredService<MainWindowViewModel>();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = DataContext
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
        
        private static void RegisterDependencies() =>
            Bootstrapper.Register(Locator.CurrentMutable, Locator.Current);
        
        private static T GetRequiredService<T>() => Locator.Current.GetService<T>();
    }
}
