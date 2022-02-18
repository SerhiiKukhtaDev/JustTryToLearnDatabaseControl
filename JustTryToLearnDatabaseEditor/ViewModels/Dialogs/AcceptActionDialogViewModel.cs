using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base;
using ReactiveUI;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs
{
    public class AcceptActionDialogViewModel : ParameterizedDialogViewModelBase<DialogResultBase, string>
    {
        private string _message;

        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }

        public void AcceptExecute()
        {
            Close(new AcceptResult(true));
        }

        public override void Activate(string parameter)
        {
            Message = parameter;
        }
    }
}