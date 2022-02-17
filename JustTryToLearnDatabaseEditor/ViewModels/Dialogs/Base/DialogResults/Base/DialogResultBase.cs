namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base
{
    public class DialogResultBase
    {
        public bool Cancelled { get; }

        public DialogResultBase(bool cancelled)
        {
            Cancelled = cancelled;
        }
    }
}