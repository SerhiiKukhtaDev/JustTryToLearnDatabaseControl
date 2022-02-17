using System.Threading;
using System.Threading.Tasks;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base
{
    public abstract class ParameterizedDialogViewModelBase<TResult, TParameter> : DialogViewModelBase<TResult>
        where TResult : DialogResultBase
    {
        public abstract void Activate(TParameter parameter);
    }

    public abstract class ParameterizedDialogViewModelBaseAsync<TResult, TParameter> : DialogViewModelBase<TResult>
        where TResult : DialogResultBase
    {
        public abstract Task ActivateAsync(TParameter parameter, CancellationToken cancellationToken = default);
    }

    public abstract class
        ParameterizedDialogViewModelBase<TParameter> : ParameterizedDialogViewModelBase<DialogResultBase, TParameter>
    {

    }
}
