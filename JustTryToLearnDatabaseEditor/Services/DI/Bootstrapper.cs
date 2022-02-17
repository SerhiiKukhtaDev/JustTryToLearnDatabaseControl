using Splat;

namespace JustTryToLearnDatabaseEditor.Services.DI
{
    public static class Bootstrapper
    {
        public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            AvaloniaServicesBootstrapper.RegisterAvaloniaServices(services);
            ServicesBootstrapper.RegisterServices(services, resolver);
            ViewModelsBootstrapper.RegisterViewModels(services, resolver);
        }
    }
}
