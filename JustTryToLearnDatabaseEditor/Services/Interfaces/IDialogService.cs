using System.Threading.Tasks;
using JustTryToLearnDatabaseEditor.ViewModels;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base;

namespace JustTryToLearnDatabaseEditor.Services.Interfaces
{
    public interface IDialogService
    {
        Task<TResult> ShowDialogAsync<TResult>(string viewModelName)
            where TResult : DialogResultBase;

        Task ShowDialogAsync(string viewModelName);

        Task ShowDialogAsync<TParameter>(string viewModelName, TParameter parameter);

        Task<TResult> ShowDialogAsync<TResult, TParameter>(string viewModelName, TParameter parameter)
            where TResult : DialogResultBase;
    }
}

