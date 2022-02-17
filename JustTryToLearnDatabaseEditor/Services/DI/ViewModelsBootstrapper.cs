using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs;
using Splat;

namespace JustTryToLearnDatabaseEditor.Services.DI
{
    public static class ViewModelsBootstrapper
    {
        public static void RegisterViewModels(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            RegisterServices(services, resolver);
            RegisterCommonViewModels(services, resolver);
        }

        private static void RegisterServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.RegisterLazySingleton(() => new MainWindowViewModel(
                resolver.GetService<IDialogService>()
            ));
        }
        
        private static void RegisterCommonViewModels(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.Register(() => new EditSubjectDialogViewModel());
            services.Register(() => new AddSubjectDialogViewModel());
        }
    }
}
