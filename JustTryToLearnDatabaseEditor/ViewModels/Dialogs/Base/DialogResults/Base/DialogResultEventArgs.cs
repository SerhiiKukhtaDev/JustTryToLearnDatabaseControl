using System;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base
{
    public class DialogResultEventArgs<TResult> : EventArgs
    {
        public TResult Result { get; }

        public DialogResultEventArgs(TResult result)
        {
            Result = result;
        }
    }

}

