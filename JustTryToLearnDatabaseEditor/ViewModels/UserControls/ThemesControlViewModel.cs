using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using DynamicData;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Classes;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Themes;
using JustTryToLearnDatabaseEditor.ViewModels.UserControls.Utils;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls
{
    public class ThemesControlViewModel : SingleItemUserControlViewModel<,>
    {
        private readonly IDialogService _dialogService;
        public event Action<Theme> ThemeDoubleTapped;
        public event Action ReturnRequested;
        
        private Class _currentClass;

        private Theme _selectedTheme;
        
        public Theme SelectedTheme
        {
            get => _selectedTheme;
            set => this.RaiseAndSetIfChanged(ref _selectedTheme, value);
        }

        public void OnThemeDoubleTappedExecute()
        {
            ThemeDoubleTapped?.Invoke(_selectedTheme);
        }

        public void SetThemesBy(Class selectedClass)
        {
            _currentClass = selectedClass;
            
            AllItems.Clear();
            AllItems.AddRange(_currentClass.Themes);
        }

        public ThemesControlViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            
            AddThemeCommand = ReactiveCommand.CreateFromTask(OnAddThemeCommandExecute);
        }
        
        public ReactiveCommand<Unit, Unit> AddThemeCommand { get; }

        private async Task OnAddThemeCommandExecute()
        {
            var result = await _dialogService.ShowDialogAsync<ItemResult<Theme>,
                IEnumerable<Theme>>(nameof(AddThemeDialogViewModel), AllItems.Items);

            if (result != null)
            {
                AllItems.Add(result.Item);
            }
        }

        public async Task DeleteThemeCommand(object parameter)
        {
            string message = "Видалення цієї теми призведе до видалення всіх питань які з нею зв'язані. " +
                             "Дійсно бажаєте видалити?";
            
            var result = await _dialogService.ShowDialogAsync<DialogResultBase, string>
                (nameof(AcceptActionDialogViewModel), message);

            if (result != null)
            {
                var index = Items.IndexOf(_selectedTheme);
                _currentClass.RemoveTheme(_selectedTheme);
                AllItems.Remove(_selectedTheme);
            
                if (Items.Count > 0)
                    SelectedTheme = Items[index == 0 ? index :  index - 1];
            }
        }

        bool CanDeleteThemeCommand(/* CommandParameter */object parameter)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return parameter != null;
        }

        public async Task EditThemeCommand(object parameter)
        {
            var result = await _dialogService.ShowDialogAsync<ItemResult<Theme>, Theme>
                (nameof(EditThemeDialogViewModel), SelectedTheme);

            if (result != null)
            {
                _selectedTheme.ItemName = result.Item.ItemName;
            }
        }

        bool CanEditThemeCommand(/* CommandParameter */object parameter)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return parameter != null;
        }

        public void OnReturnRequestedExecute()
        {
            ReturnRequested?.Invoke();
        }
    }
}
