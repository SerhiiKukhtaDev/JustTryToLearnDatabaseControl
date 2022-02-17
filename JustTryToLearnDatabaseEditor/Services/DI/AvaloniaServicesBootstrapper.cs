using JustTryToLearnDatabaseEditor.Services.Providers;
using JustTryToLearnDatabaseEditor.Services.Providers.Interfaces;
using Splat;

namespace JustTryToLearnDatabaseEditor.Services.DI
{
    public static class AvaloniaServicesBootstrapper
    {
        public static void RegisterAvaloniaServices(IMutableDependencyResolver services)
        {
            services.RegisterLazySingleton<IMainWindowProvider>(() => new MainWindowProvider());
        }
    }
}
