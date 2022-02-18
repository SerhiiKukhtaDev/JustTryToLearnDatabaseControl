﻿using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults
{
    public class ItemResult<T> : DialogResultBase where T : new()
    {
        public T Item;

        public ItemResult(T item)
        {
            Item = item;
        }
    }
}
