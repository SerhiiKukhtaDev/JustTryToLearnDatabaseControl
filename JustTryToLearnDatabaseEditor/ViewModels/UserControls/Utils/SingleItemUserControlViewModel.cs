using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using DynamicData;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Models.Base;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls.Utils
{
    public abstract class SingleItemUserControlViewModel<TItem> : 
        ViewModelBase where TItem : NotifiedModel, INamedModel, new()
    {
        private readonly IDialogService _dialogService;
        
        private readonly string _addItemViewModelName;
        private readonly string _editItemViewModelName;
        
        public event Action<TItem> ItemDoubleTapped;

        private string _searchText;
        private readonly string _deleteMessage;
        
        public ReactiveCommand<Unit, Unit> AddItemCommand { get; }

        public string SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        private TItem _selectedItem;

        public TItem SelectedItem
        {
            get => _selectedItem;
            set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
        }

        private ReadOnlyObservableCollection<TItem> _items;

        public ReadOnlyObservableCollection<TItem> Items
        {
            get => _items;
            set => this.RaiseAndSetIfChanged(ref _items, value);
        }

        protected SourceList<TItem> AllItems;

        public void SubjectDoubleTappedExecute()
        {
            ItemDoubleTapped?.Invoke(_selectedItem);
        }
        
        protected virtual async Task OnAddSubjectCommandExecute()
        {
            var result = await _dialogService.ShowDialogAsync<ItemResult<TItem>,
                IEnumerable<TItem>>(_addItemViewModelName, AllItems.Items);

            if (result != null)
            {
                AllItems.Add(result.Item);
            }
        }
        
        public async Task DeleteItemCommand(object parameter)
        {
            var result = await _dialogService.ShowDialogAsync<DialogResultBase, string>
                (nameof(AcceptActionDialogViewModel), _deleteMessage);

            if (result != null)
            {
                var index = Items.IndexOf(_selectedItem);
                AllItems.Remove(_selectedItem);
            
                if (Items.Count > 0)
                    SelectedItem = Items[index == 0 ? index :  index - 1];
            }
        }

        bool CanDeleteItemCommand(/* CommandParameter */object parameter)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return parameter != null;
        }
        
        public virtual async Task EditItemCommand(object parameter)
        {
            var result = await _dialogService.ShowDialogAsync<ItemResult<TItem>, TItem>
                (_editItemViewModelName, SelectedItem);

            if (result != null)
            {
                _selectedItem.ItemName = result.Item.ItemName;
            }
        }
        
        bool CanEditSubjectCommand(/* CommandParameter */object parameter)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return parameter != null;
        }
        
         

        protected abstract Task OnItemEdited();
        protected abstract Task OnItemAdded();
        

        public SingleItemUserControlViewModel(IDialogService dialogService, string deleteMessage, 
            string addItemViewModelName, string editItemViewModelName)
        {
            _dialogService = dialogService;
            AllItems = new SourceList<TItem>();
            _deleteMessage = deleteMessage;
            _addItemViewModelName = addItemViewModelName;
            _editItemViewModelName = editItemViewModelName;

            IObservable<Func<TItem, bool>> filter = this.WhenAnyValue(vm => vm.SearchText)
                .Throttle(TimeSpan.FromMilliseconds(100))
                .Select(BuildFilter);

            AllItems.Connect()
                .Filter(filter)
                .ObserveOn(AvaloniaScheduler.Instance)
                .Bind(out _items)
                .Subscribe();
            
            AddItemCommand = ReactiveCommand.CreateFromTask(OnAddSubjectCommandExecute);
        }
        
        protected virtual Func<TItem, bool> BuildFilter(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return t => true;
            
            return t => t.ItemName.Contains(searchText);
        }
    }
}
