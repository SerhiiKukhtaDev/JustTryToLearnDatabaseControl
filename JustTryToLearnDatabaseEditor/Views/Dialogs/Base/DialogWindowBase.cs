using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using JustTryToLearnDatabaseEditor.ViewModels;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base;

namespace JustTryToLearnDatabaseEditor.Views.Dialogs.Base
{
    public class DialogWindowBase<TResult> : Window where TResult : DialogResultBase
        
    {
        protected Window ParentWindow => (Window) Owner;

        protected DialogViewModelBase<TResult> ViewModel => (DialogViewModelBase<TResult>) DataContext;

        protected DialogWindowBase()
        {
            SubscribeToViewEvents();
        }

        protected virtual void OnOpened()
        {

        }

        private void OnOpened(object sender, EventArgs e)
        {
            LockSize();
            CenterDialog();
            HideTopPanel();
            HideInTaskbar();
            
            OnOpened();
        }

        private void CenterDialog()
        {
            var x = ParentWindow.Position.X + (ParentWindow.Bounds.Width - Width) / 2;
            var y = ParentWindow.Position.Y + (ParentWindow.Bounds.Height - Height) / 2;

            Position = new PixelPoint((int) x, (int) y);
        }

        protected virtual void LockSize()
        {
            Width = MaxWidth = ParentWindow.ClientSize.Width / 2;
            Height = MaxHeight = ParentWindow.ClientSize.Height / 2;
            
            MinHeight = 200;
            MinWidth = 200;
        }

        private void HideTopPanel()
        {
            ExtendClientAreaToDecorationsHint = true;
            ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.NoChrome;
            ExtendClientAreaTitleBarHeightHint = -1;
        }

        private void HideInTaskbar()
        {
            ShowInTaskbar = false;
        }

        private void SubscribeToViewModelEvents() => ViewModel.CloseRequested += ViewModelOnCloseRequested;

        private void UnsubscribeFromViewModelEvents() => ViewModel.CloseRequested -= ViewModelOnCloseRequested;

        private void SubscribeToViewEvents()
        {
            DataContextChanged += OnDataContextChanged;
            Opened += OnOpened;
        }

        private void UnsubscribeFromViewEvents()
        {
            DataContextChanged -= OnDataContextChanged;
            Opened -= OnOpened;
        }

        private void OnDataContextChanged(object sender, EventArgs e) => SubscribeToViewModelEvents();

        private void ViewModelOnCloseRequested(object sender, DialogResultEventArgs<TResult> args)
        {
            UnsubscribeFromViewModelEvents();
            UnsubscribeFromViewEvents();

            Close(args.Result);
        }
    }

    public class DialogWindowBase : DialogWindowBase<DialogResultBase>
    {

    }
}