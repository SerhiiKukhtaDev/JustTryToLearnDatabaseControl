using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults
{
    public class ItemResult<T> : DialogResultBase where T : new()
    {
        public bool Cencelled { get; }

        public T Item;

        public ItemResult(bool cancelled, T item) : base(cancelled)
        {
            Item = item;
        }
    }
}
