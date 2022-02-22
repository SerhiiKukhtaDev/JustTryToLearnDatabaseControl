using System;
using System.Threading;
using JetBrains.Annotations;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Database;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Base;
using JustTryToLearnDatabaseEditor.ViewModels.UserControls;
using MongoDB.Bson;
using MongoDB.Driver;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _greeting;

        [NotNull]
        public string Greeting
        {
            get => _greeting;
            set => this.RaiseAndSetIfChanged(ref _greeting, value);
        }

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
        
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<BsonDocument> _collection;
        private readonly CancellationToken _token;
        private readonly CancellationTokenSource _tokenSource;


        public MainWindowViewModel(IDialogService dialogService, 
            IDatabaseService databaseService)
        {
            _database = databaseService.GetDatabase();
            _collection = databaseService.GetCollection();

            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;

            School school = databaseService.DeserializeRoot<School>();
            
            SubjectsControlViewModel = 
                new SubjectsControlViewModel(dialogService, new SubjectService(_collection, _token));
            SubjectsControlViewModel.SetItemsBy(school);
            
            SubjectsControlViewModel.ItemDoubleTapped += OnItemDoubleTapped;

            ClassesControlViewModel = 
                new ClassesControlViewModel(dialogService, new ClassService(_collection, _token));
            ClassesControlViewModel.ItemDoubleTapped += OnClassDoubleTapped;
            ClassesControlViewModel.ReturnRequested += OnClassesReturnRequested;

            ThemesControlViewModel = 
                new ThemesControlViewModel(dialogService, new ThemeService(_collection, _token));
            ThemesControlViewModel.ItemDoubleTapped += OnThemeDoubleTapped;
            ThemesControlViewModel.ReturnRequested += OnThemesReturnRequested;
            
            QuestionControlViewModel = 
                new QuestionControlViewModel(dialogService, new QuestionService(_collection, _token));
            QuestionControlViewModel.ReturnRequested += OnQuestionReturnRequested;
        }

        public void OnQuit()
        {
            _tokenSource.Cancel();
            UnsubscribeEvents();   
        }

        private void UnsubscribeEvents()
        {
            SubjectsControlViewModel.ItemDoubleTapped -= OnItemDoubleTapped;
            SubjectsControlViewModel.Dispose();
            
            ClassesControlViewModel.ItemDoubleTapped -= OnClassDoubleTapped;
            ClassesControlViewModel.ReturnRequested -= OnClassesReturnRequested;
            ClassesControlViewModel.Dispose();
            
            ThemesControlViewModel.ItemDoubleTapped -= OnThemeDoubleTapped;
            ThemesControlViewModel.ReturnRequested -= OnThemesReturnRequested;
            ThemesControlViewModel.Dispose();
            
            QuestionControlViewModel.ReturnRequested -= OnQuestionReturnRequested;
            QuestionControlViewModel.Dispose();
        }

        private void OnQuestionReturnRequested()
        {
            SelectedIndex = 2;
        }

        private void OnThemesReturnRequested()
        {
            SelectedIndex = 1;
        }

        private void OnClassesReturnRequested()
        {
            SelectedIndex = 0;
        }

        private void OnThemeDoubleTapped(Theme obj)
        {
            SelectedIndex = 3;
            QuestionControlViewModel.SetItemsBy(obj);
        }

        private void OnClassDoubleTapped(Class obj)
        {
            SelectedIndex = 2;
            ThemesControlViewModel.SetItemsBy(obj);
        }

        private void OnItemDoubleTapped(Subject obj)
        {
            SelectedIndex = 1;
            ClassesControlViewModel.SetItemsBy(obj);
        }
    }
}
