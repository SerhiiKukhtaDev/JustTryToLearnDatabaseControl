using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Classes;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Themes;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls
{
    public class ThemesControlViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        public event Action<Theme> ThemeDoubleTapped;
        
        private Class _currentClass;
        
        private ObservableCollection<Theme> _themes;

        public ObservableCollection<Theme> Themes
        {
            get => _themes;
            set => this.RaiseAndSetIfChanged(ref _themes, value);
        }

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
            Themes = _currentClass.Themes;
        }

        public ThemesControlViewModel(IDialogService dialogService)
        {
            Themes = new ObservableCollection<Theme>();
            _dialogService = dialogService;
            
            AddThemeCommand = ReactiveCommand.CreateFromTask(OnAddThemeCommandExecute);
        }
        
        public ReactiveCommand<Unit, Unit> AddThemeCommand { get; }

        private async Task OnAddThemeCommandExecute()
        {
            var result = await _dialogService.ShowDialogAsync<ItemResult<Theme>,
                ObservableCollection<Theme>>(nameof(AddThemeDialogViewModel), Themes);

            if (result != null)
            {
                Themes.Add(result.Item);
            }
        }

        public async Task DeleteThemeCommand(object parameter)
        {
            string message = "Видалення цієї теми призведе до видалення всіх питань які з ним зв'язані. " +
                             "Дійсно бажаєте видалити?";
            
            var result = await _dialogService.ShowDialogAsync<DialogResultBase, string>
                (nameof(AcceptActionDialogViewModel), message);

            if (result != null)
            {
                var index = Themes.IndexOf(_selectedTheme);
                Themes.Remove(_selectedTheme);
            
                if (Themes.Count > 0)
                    SelectedTheme = Themes[index == 0 ? index :  index - 1];
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
                _selectedTheme.Name = result.Item.Name;
            }
        }

        bool CanEditThemeCommand(/* CommandParameter */object parameter)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return parameter != null;
        }
    }
}
