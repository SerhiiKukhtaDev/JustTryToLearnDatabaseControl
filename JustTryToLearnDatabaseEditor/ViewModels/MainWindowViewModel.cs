using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private int _count = 0;

        public int Count
        {
            get => _count;
            set => this.RaiseAndSetIfChanged(ref _count, value);
        }

        public ObservableCollection<TextBlock> TextBlocks { get; }

        public ReactiveCommand<Unit, Unit> OnLoaded { get; }


        private void OnButtonClickExecute()
        {
            Count++;
        }


        public MainWindowViewModel()
        {
            OnLoaded = ReactiveCommand.Create(OnButtonClickExecute);
            
            TextBlocks = new ObservableCollection<TextBlock>()
            {
                new TextBlock() {Text = "Atata"},
                new TextBlock() {Text = "Bebebe"}
            };
        }
    }
}
