using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.Services.Providers.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Classes;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Questions;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Themes;
using JustTryToLearnDatabaseEditor.ViewModels.UserControls;
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
            
            services.Register(() => new EditClassDialogViewModel());
            services.Register(() => new AddClassDialogViewModel());
            
            services.Register(() => new EditThemeDialogViewModel());
            services.Register(() => new AddThemeDialogViewModel());
            
            services.Register(() => new EditQuestionDialogViewModel());
            services.Register(() => new AddQuestionDialogViewModel());
            
            services.Register(() => new AcceptActionDialogViewModel());
        }

        private static void RegisterUserControlViewModels(IMutableDependencyResolver services,
            IReadonlyDependencyResolver resolver)
        {
            
        }
    }
}
