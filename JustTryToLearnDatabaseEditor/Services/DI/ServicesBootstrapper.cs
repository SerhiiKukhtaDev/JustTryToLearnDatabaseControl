using JustTryToLearnDatabaseEditor.Services.Database;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.Services.Providers.Interfaces;
using Splat;

namespace JustTryToLearnDatabaseEditor.Services.DI
{
    public static class ServicesBootstrapper
    {
        public static void RegisterServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            RegisterCommonServices(services, resolver);
        }

        private static void RegisterCommonServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.RegisterLazySingleton<IDialogService>(() => new DialogService(
                resolver.GetService<IMainWindowProvider>()
            ));

            services.RegisterLazySingleton<IDatabaseService>(() => new DatabaseService());
        }
    }
}
