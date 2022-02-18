using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using JetBrains.Annotations;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Classes;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls
{
    public class ClassesControlViewModel : ViewModelBase
    {
        public event Action<Class> ClassDoubleTapped;

        private readonly IDialogService _dialogService;
        
        private Subject _currentSubject;
        
        private ObservableCollection<Class> _classes;

        public ObservableCollection<Class> Classes
        {
            get => _classes;
            set => this.RaiseAndSetIfChanged(ref _classes, value);
        }

        private Class _selectedClass;
        
        public Class SelectedClass
        {
            get => _selectedClass;
            set => this.RaiseAndSetIfChanged(ref _selectedClass, value);
        }

        public void OnClassDoubleTappedExecute()
        {
            ClassDoubleTapped?.Invoke(_selectedClass);
        }

        public void SetClassesBy(Subject selectedSubject)
        {
            _currentSubject = selectedSubject;
            Classes = _currentSubject.Classes;
        }

        public ClassesControlViewModel(IDialogService dialogService)
        {
            Classes = new ObservableCollection<Class>();
            _dialogService = dialogService;
            
            AddClassCommand = ReactiveCommand.CreateFromTask(OnAddClassCommandExecute);
        }
        
        public ReactiveCommand<Unit, Unit> AddClassCommand { get; }

        private async Task OnAddClassCommandExecute()
        {
            var result = await _dialogService.ShowDialogAsync<ItemResult<Class>,
                ObservableCollection<Class>>(nameof(AddClassDialogViewModel), Classes);

            if (result != null)
            {
                Classes.Add(result.Item);
            }
        }

        public async Task DeleteClassCommand(object parameter)
        {
            string message = "Видалення цього класу призведе до видалення всіх питань які з ним зв'язані. " +
                             "Дійсно бажаєте видалити?";
            
            var result = await _dialogService.ShowDialogAsync<DialogResultBase, string>
                (nameof(AcceptActionDialogViewModel), message);

            if (result != null)
            {
                var index = Classes.IndexOf(_selectedClass);
                Classes.Remove(_selectedClass);
            
                if (Classes.Count > 0)
                    SelectedClass = Classes[index == 0 ? index :  index - 1];
            }
        }

        bool CanDeleteClassCommand(/* CommandParameter */object parameter)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return parameter != null;
        }

        public async Task EditClassCommand(object parameter)
        {
            var result = await _dialogService.ShowDialogAsync<ItemResult<Class>, Class>
                (nameof(EditClassDialogViewModel), SelectedClass);

            if (result != null)
            {
                _selectedClass.Name = result.Item.Name;
            }
        }

        bool CanEditClassCommand(/* CommandParameter */object parameter)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return parameter != null;
        }
    }
}
