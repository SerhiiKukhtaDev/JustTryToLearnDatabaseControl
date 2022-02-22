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
        protected readonly IDialogService DialogService;
        
        protected readonly string AddItemViewModelName;
        protected readonly string EditItemViewModelName;
        
        public event Action<TItem> ItemDoubleTapped;

        private string _searchText;
        protected readonly string _deleteMessage;
        
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
        
        protected virtual async Task OnAddItemCommandExecute()
        {
            var result = await DialogService.ShowDialogAsync<ItemResult<TItem>,
                IEnumerable<TItem>>(AddItemViewModelName, AllItems.Items);

            if (result != null)
            {
                AllItems.Add(result.Item);
                ItemAdded?.Invoke(result.Item);
            }
        }
        
        public virtual async Task DeleteItemCommand(object parameter)
        {
            var result = await DialogService.ShowDialogAsync<DialogResultBase, string>
                (nameof(AcceptActionDialogViewModel), _deleteMessage);

            if (result != null)
            {
                var index = Items.IndexOf(_selectedItem);
                ItemDeleted?.Invoke(_selectedItem);
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
            var result = await DialogService.ShowDialogAsync<ItemResult<TItem>, TItem>
                (EditItemViewModelName, SelectedItem);

            if (result != null)
            {
                _selectedItem.Name = result.Item.Name;
                ItemEdited?.Invoke(_selectedItem);
            }
        }
        
        bool CanEditItemCommand(/* CommandParameter */object parameter)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return parameter != null;
        }

        private event Action<TItem> ItemEdited;
        private event Action<TItem> ItemDeleted;
        private event Action<TItem> ItemAdded;

        protected abstract void OnItemEdited(TItem item);
        protected abstract void OnItemAdded(TItem item);
        protected abstract void OnItemDeleted(TItem item);
        

        public SingleItemUserControlViewModel(IDialogService dialogService, string deleteMessage, 
            string addItemViewModelName, string editItemViewModelName)
        {
            DialogService = dialogService;
            AllItems = new SourceList<TItem>();
            _deleteMessage = deleteMessage;
            AddItemViewModelName = addItemViewModelName;
            EditItemViewModelName = editItemViewModelName;

            IObservable<Func<TItem, bool>> filter = this.WhenAnyValue(vm => vm.SearchText)
                .Throttle(TimeSpan.FromMilliseconds(100))
                .Select(BuildFilter);

            AllItems.Connect()
                .Filter(filter)
                .ObserveOn(AvaloniaScheduler.Instance)
                .Bind(out _items)
                .Subscribe();
            
            AddItemCommand = ReactiveCommand.CreateFromTask(OnAddItemCommandExecute);

            ItemEdited += OnItemEdited;
            ItemAdded += OnItemAdded;
            ItemDeleted += OnItemDeleted;
        }
        
        protected virtual Func<TItem, bool> BuildFilter(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return t => true;
            
            return t => t.Name.Contains(searchText);
        }
    }
}
