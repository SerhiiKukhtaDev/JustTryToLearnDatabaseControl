using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using DynamicData.Binding;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls
{
    public class SubjectsControlViewModel : ViewModelBase
    {
        public IObservableCollection<Subject> Subjects { get; }

        public event Action<Subject> SubjectDoubleTapped;

        private readonly IDialogService _dialogService;

        private Subject _selectedSubject;
        private TabControl _tabControl;

        public Subject SelectedSubject
        {
            get => _selectedSubject;
            set => this.RaiseAndSetIfChanged(ref _selectedSubject, value);
        }

        public void SubjectDoubleTappedExecute()
        {
            SubjectDoubleTapped?.Invoke(_selectedSubject);
        }

        public ReactiveCommand<Unit, Unit> AddSubjectCommand { get; }

        private async Task OnAddSubjectCommandExecute()
        {
            var result = await _dialogService.ShowDialogAsync<ItemResult<Subject>,
                IObservableCollection<Subject>>(nameof(AddSubjectDialogViewModel), Subjects);

            if (result != null)
            {
                Subjects.Add(result.Item);
            }
        }

        public async Task DeleteSubjectCommand(object parameter)
        {
            string message = "Видалення цього предмету призведе до видалення всіх питань які з ним зв'язані. " +
                             "Дійсно бажаєте видалити?";
            
            var result = await _dialogService.ShowDialogAsync<DialogResultBase, string>
                (nameof(AcceptActionDialogViewModel), message);

            if (result != null)
            {
                var index = Subjects.IndexOf(_selectedSubject);
                Subjects.Remove(_selectedSubject);
            
                if (Subjects.Count > 0)
                    SelectedSubject = Subjects[index == 0 ? index :  index - 1];
            }
        }

        bool CanDeleteSubjectCommand(/* CommandParameter */object parameter)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return parameter != null;
        }

        public async Task EditSubjectCommand(object parameter)
        {
            var result = await _dialogService.ShowDialogAsync<ItemResult<Subject>, Subject>
                (nameof(EditSubjectDialogViewModel), SelectedSubject);

            if (result != null)
            {
                _selectedSubject.Name = result.Item.Name;
            }
        }

        bool CanEditSubjectCommand(/* CommandParameter */object parameter)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return parameter != null;
        }

        public SubjectsControlViewModel(IDialogService dialogService)
        {
            Subjects = new ObservableCollectionExtended<Subject>
            {
                new() {Name = "Математика"},
                new() {Name = "Біологія"}
            };
            
            Subjects[0].AddClass(new Class(){Name = "Atata"});
            Subjects[0].AddClass(new Class(){Name = "Atata"});
            Subjects[0].AddClass(new Class(){Name = "Atata"});
            Subjects[0].AddClass(new Class(){Name = "Atata"});
            Subjects[0].AddClass(new Class(){Name = "Atata"});
            
            Subjects[0].Classes[0].AddTheme(new Theme(){Name = "Theme 1"});
            Subjects[0].Classes[0].AddTheme(new Theme(){Name = "Theme 2"});
            
            Subjects[0].Classes[0].Themes[0].AddQuestion(new Question() {Name = "Question 1"});

            _dialogService = dialogService;

            AddSubjectCommand = ReactiveCommand.CreateFromTask(OnAddSubjectCommandExecute);
        }
    }
}
