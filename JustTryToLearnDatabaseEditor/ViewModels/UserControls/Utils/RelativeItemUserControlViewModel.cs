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
    public abstract class RelativeItemUserControlViewModel<TItem, TParent> : ViewModelBase
        where TItem : NotifiedModel, INamedModel, new() where TParent : IContainItems<TItem>
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
        
        private TParent _parent;
        
        public void SetItemsBy(TParent parent)
        {
            _parent = parent;
            
            AllItems.Clear();
            AllItems.AddRange(_parent.Items);
        }

        public void ItemDoubleTappedExecute()
        {
            ItemDoubleTapped?.Invoke(_selectedItem);
        }

        public event Action ReturnRequested;

        public void ReturnToParentExecute()
        {
            ReturnRequested?.Invoke();
        }
        
        protected async Task OnAddItemCommandExecute()
        {
            var result = await DialogService.ShowDialogAsync<ItemResult<TItem>,
                IEnumerable<TItem>>(AddItemViewModelName, AllItems.Items);

            if (result != null)
            {
                AllItems.Add(result.Item);
                ItemAdded?.Invoke(result.Item, _parent);
            }
        }
        
        public async Task DeleteItemCommand(object parameter)
        {
            var result = await DialogService.ShowDialogAsync<DialogResultBase, string>
                (nameof(AcceptActionDialogViewModel), _deleteMessage);

            if (result != null)
            {
                var index = Items.IndexOf(SelectedItem);
                ItemRemoved?.Invoke(SelectedItem, _parent);
                AllItems.Remove(SelectedItem);

                if (Items.Count > 0)
                    SelectedItem = Items[index == 0 ? index :  index - 1];
            }
        }

        bool CanDeleteItemCommand(/* CommandParameter */object parameter)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return parameter != null;
        }
        
        public async Task EditItemCommand(object parameter)
        {
            var result = await DialogService.ShowDialogAsync<ItemResult<TItem>, TItem>
                (EditItemViewModelName, SelectedItem);

            if (result != null)
            {
                ItemEditRequested?.Invoke(SelectedItem, result.Item, _parent);
            }
        }
        
        bool CanEditItemCommand(/* CommandParameter */object parameter)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return parameter != null;
        }

        private event Action<TItem, TParent> ItemAdded;
        private event Action<TItem, TItem, TParent> ItemEditRequested;
        private event Action<TItem, TParent> ItemRemoved;

        protected new abstract void OnItemAdded(TItem item, TParent parent);
        protected new abstract void OnItemEditRequested(TItem item, TItem newItem, TParent parent);
        protected abstract void OnItemRemoved(TItem item, TParent parent);
        

        public RelativeItemUserControlViewModel(IDialogService dialogService, string deleteMessage, 
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

            ItemAdded += OnItemAdded;
            ItemEditRequested += OnItemEditRequested;
            ItemRemoved += OnItemRemoved;
        }
        
        protected virtual Func<TItem, bool> BuildFilter(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return t => true;
            
            return t => t.ItemName.Contains(searchText);
        }
    }
}
