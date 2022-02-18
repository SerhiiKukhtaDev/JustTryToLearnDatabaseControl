using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Base;
using JustTryToLearnDatabaseEditor.ViewModels.UserControls;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public SubjectsControlViewModel SubjectsControlViewModel { get; }
        public ClassesControlViewModel ClassesControlViewModel { get; }
        public ThemesControlViewModel ThemesControlViewModel { get; }
        public QuestionControlViewModel QuestionControlViewModel { get; }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedIndex, value);
        }

        private int _selectedIndex;


        public MainWindowViewModel(IDialogService dialogService)
        {
            SubjectsControlViewModel = new SubjectsControlViewModel(dialogService);
            SubjectsControlViewModel.SubjectDoubleTapped += OnSubjectDoubleTapped;

            ClassesControlViewModel = new ClassesControlViewModel(dialogService);
            ClassesControlViewModel.ClassDoubleTapped += OnClassDoubleTapped;

            ThemesControlViewModel = new ThemesControlViewModel(dialogService);
            ThemesControlViewModel.ThemeDoubleTapped += OnThemeDoubleTapped;

            QuestionControlViewModel = new QuestionControlViewModel(dialogService);
        }

        private void OnThemeDoubleTapped(Theme obj)
        {
            SelectedIndex = 3;
            QuestionControlViewModel.SetQuestionsBy(obj);
        }

        private void OnClassDoubleTapped(Class obj)
        {
            SelectedIndex = 2;
            ThemesControlViewModel.SetThemesBy(obj);
        }

        private void OnSubjectDoubleTapped(Subject obj)
        {
            SelectedIndex = 1;
            ClassesControlViewModel.SetClassesBy(obj);
        }
    }
}
