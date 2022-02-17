using System.Reactive;
using System.Threading.Tasks;
using DynamicData.Binding;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        public IObservableCollection<Subject> Subjects { get; }

        private readonly IDialogService _dialogService;

        private Subject _selectedSubject;

        public Subject SelectedSubject
        {
            get => _selectedSubject;
            set => this.RaiseAndSetIfChanged(ref _selectedSubject, value);
        }

        #endregion

        #region Commands

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

        public void DeleteSubjectCommand(object parameter)
        {
            var index = Subjects.IndexOf(_selectedSubject);
            Subjects.Remove(_selectedSubject);
            
            if (Subjects.Count > 0)
                SelectedSubject = Subjects[index == 0 ? index :  index - 1];
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

        #endregion


        public MainWindowViewModel(IDialogService dialogService)
        {
            Subjects = new ObservableCollectionExtended<Subject>
            {
                new() {Name = "Математика"},
                new() {Name = "Біологія"},
            };

            _dialogService = dialogService;
            
            AddSubjectCommand = ReactiveCommand.CreateFromTask(OnAddSubjectCommandExecute);
        }
    }
}
