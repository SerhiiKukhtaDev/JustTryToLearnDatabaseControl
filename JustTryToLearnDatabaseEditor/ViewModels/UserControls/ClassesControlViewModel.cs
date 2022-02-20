using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using DynamicData;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Classes;
using JustTryToLearnDatabaseEditor.ViewModels.UserControls.Utils;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls
{
    public class ClassesControlViewModel : SingleItemUserControlViewModel<,>
    {
        public event Action<Class> ClassDoubleTapped;
        public event Action ReturnRequested;

        private readonly IDialogService _dialogService;
        
        private Subject _currentSubject;

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
            
            AllItems.Clear();
            AllItems.AddRange(_currentSubject.Classes);
        }

        public ClassesControlViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            
            AddClassCommand = ReactiveCommand.CreateFromTask(OnAddClassCommandExecute);
        }
        
        public ReactiveCommand<Unit, Unit> AddClassCommand { get; }

        private async Task OnAddClassCommandExecute()
        {
            var result = await _dialogService.ShowDialogAsync<ItemResult<Class>,
                IEnumerable<Class>>(nameof(AddClassDialogViewModel), AllItems.Items);

            if (result != null)
            {
                AllItems.Add(result.Item);
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
                var index = Items.IndexOf(_selectedClass);
                _currentSubject.RemoveClass(_selectedClass);
                AllItems.Remove(_selectedClass);
            
                if (Items.Count > 0)
                    SelectedClass = Items[index == 0 ? index :  index - 1];
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
                _selectedClass.ItemName = result.Item.ItemName;
            }
        }

        bool CanEditClassCommand(/* CommandParameter */object parameter)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return parameter != null;
        }

        public void OnReturnRequestedExecute()
        {
            ReturnRequested?.Invoke();
        }

        public ClassesControlViewModel()
        {
            
        }
    }
}
