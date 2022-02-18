using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults
{
    public class AcceptResult : DialogResultBase
    {
        public bool Accepted { get; }

        public AcceptResult(bool accepted)
        {
            Accepted = accepted;
        }
    }
}