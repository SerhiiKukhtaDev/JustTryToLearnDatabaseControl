using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base;

namespace JustTryToLearnDatabaseEditor.Views.Dialogs.Base
{
    public class QuestionDialogWindowBase<TResult> : DialogWindowBase<TResult> where TResult : DialogResultBase
    {
        protected override void LockSize()
        {
            Width = MaxWidth = ParentWindow.ClientSize.Width / 1.2;
            Height = MaxHeight = ParentWindow.ClientSize.Height / 1.2;
            
            MinHeight = 200;
            MinWidth = 200;
        }
    }
}