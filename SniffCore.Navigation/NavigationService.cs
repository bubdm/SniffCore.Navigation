using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SniffCore.Dialogs;
using SniffCore.MessageBoxes;
using SniffCore.Navigation.External;
using SniffCore.PleaseWaits;
using SniffCore.Windows;

namespace SniffCore.Navigation
{
    public sealed class NavigationService : INavigationService
    {
        private static readonly Dictionary<object, WeakReference> _displayControls = new Dictionary<object, WeakReference>();
        private readonly IDialogProvider _dialogProvider;
        private readonly IMessageBoxProvider _messageBoxProvider;
        private readonly IPleaseWaitProvider _pleaseWaitProvider;
        private readonly IWindowProvider _windowProvider;

        public NavigationService(IWindowProvider windowProvider,
            IMessageBoxProvider messageBoxProvider,
            IPleaseWaitProvider pleaseWaitProvider,
            IDialogProvider dialogProvider)
        {
            _windowProvider = windowProvider;
            _messageBoxProvider = messageBoxProvider;
            _pleaseWaitProvider = pleaseWaitProvider;
            _dialogProvider = dialogProvider;
        }

        public Task ShowWindowAsync(object windowKey, object viewModel)
        {
            return ShowWindowAsync(null, windowKey, viewModel);
        }

        public async Task ShowWindowAsync(object ownerWindowKey, object windowKey, object viewModel)
        {
            var window = CreateWindow(ownerWindowKey, windowKey, viewModel);
            if (viewModel is IAsyncLoader asyncLoader)
            {
                window.Show();
                await asyncLoader.LoadAsync();
            }
            else if (viewModel is IDelayedAsyncLoader delayedAsyncLoader)
            {
                await ShowWindow(delayedAsyncLoader, () => window.Show());
            }

            window.Show();
        }

        public Task<bool?> ShowModalWindowAsync(object windowKey, object viewModel)
        {
            return ShowModalWindowAsync(null, windowKey, viewModel);
        }

        public async Task<bool?> ShowModalWindowAsync(object ownerWindowKey, object windowKey, object viewModel)
        {
            var window = CreateWindow(ownerWindowKey, windowKey, viewModel);
            if (viewModel is IAsyncLoader asyncLoader)
            {
                asyncLoader.LoadAsync().FireAndForget();
                return window.ShowDialog();
            }

            if (viewModel is IDelayedAsyncLoader delayedAsyncLoader)
            {
                await ShowWindow(delayedAsyncLoader, () => { });
                return window.ShowDialog();
            }

            return window.ShowDialog();
        }

        public void SetDialogResult(object windowKey, bool? dialogResult)
        {
            var window = _windowProvider.GetOpenWindow(windowKey);
            if (window == null)
                throw new InvalidOperationException($"The window with the key '{windowKey}' cannot be closed. It not open anymore or got not opened by INavigationService.Show[Modal]WindowAsync");
            window.DialogResult = dialogResult;
        }

        public void Close(object windowKey)
        {
            var window = _windowProvider.GetOpenWindow(windowKey);
            if (window == null)
                throw new InvalidOperationException($"The window with the key '{windowKey}' cannot be closed. It not open anymore or got not opened by INavigationService.Show[Modal]WindowAsync");
            window.Close();
        }

        public MessageBoxResult ShowMessageBox(string messageBoxText)
        {
            return _messageBoxProvider.Show(messageBoxText);
        }

        public MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText)
        {
            return _messageBoxProvider.Show(_windowProvider.GetOpenWindow(ownerWindowKey), messageBoxText);
        }

        public MessageBoxResult ShowMessageBox(string messageBoxText, string caption)
        {
            return _messageBoxProvider.Show(messageBoxText, caption);
        }

        public MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption)
        {
            return _messageBoxProvider.Show(_windowProvider.GetOpenWindow(ownerWindowKey), messageBoxText, caption);
        }

        public MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button)
        {
            return _messageBoxProvider.Show(messageBoxText, caption, button);
        }

        public MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption, MessageBoxButton button)
        {
            return _messageBoxProvider.Show(_windowProvider.GetOpenWindow(ownerWindowKey), messageBoxText, caption, button);
        }

        public MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options)
        {
            return _messageBoxProvider.Show(messageBoxText, caption, button, options);
        }

        public MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options)
        {
            return _messageBoxProvider.Show(_windowProvider.GetOpenWindow(ownerWindowKey), messageBoxText, caption, button, options);
        }

        public bool ShowDialog(IOpenFileData openFileData)
        {
            return _dialogProvider.Show(openFileData);
        }

        public bool ShowDialog(ISaveFileData saveFileData)
        {
            return _dialogProvider.Show(saveFileData);
        }

        public bool ShowDialog(IBrowseFolderData browseFolderData)
        {
            return _dialogProvider.Show(browseFolderData);
        }

        private Window CreateWindow(object ownerWindowKey, object windowKey, object viewModel)
        {
            var window = _windowProvider.GetNewWindow(windowKey);
            window.DataContext = viewModel;
            if (ownerWindowKey != null)
                window.Owner = _windowProvider.GetOpenWindow(ownerWindowKey);
            window.Closing += HandleWindowClosing;
            return window;
        }

        private async Task ShowWindow(IDelayedAsyncLoader delayedAsyncLoader, Action callback)
        {
            var loadingProgress = new LoadingProgress();

            void LoadingProgressOnProgressUpdated(object sender, ProgressDataEventArgs e)
            {
                _pleaseWaitProvider.HandleProgress(e.Data);
            }

            void LoadingProgressOnProgressCanceled(object sender, LoadingCanceledEventArgs e)
            {
                _pleaseWaitProvider.HandleCanceled(e.Data);
            }

            loadingProgress.ProgressUpdated += LoadingProgressOnProgressUpdated;
            loadingProgress.ProgressCanceled += LoadingProgressOnProgressCanceled;
            _pleaseWaitProvider.Show();

            await delayedAsyncLoader.LoadAsync(loadingProgress);

            loadingProgress.ProgressUpdated -= LoadingProgressOnProgressUpdated;
            loadingProgress.ProgressCanceled -= LoadingProgressOnProgressCanceled;
            _pleaseWaitProvider.Close();

            callback?.Invoke();
        }

        private void HandleWindowClosing(object sender, CancelEventArgs e)
        {
            var window = (Window) sender;
            window.Closing -= HandleWindowClosing;
            (window.DataContext as IDisposable)?.Dispose();
        }

        internal static void UnregisterDisplayControl(object controlKey)
        {
            _displayControls.Remove(controlKey);
            RemoveDeadDisplayControls();
        }

        private static void RemoveDeadDisplayControls()
        {
            var dead = _displayControls.Where(x => !x.Value.IsAlive).ToList();
            foreach (var pair in dead)
                _displayControls.Remove(pair.Key);
        }

        internal static void RegisterDisplayControl(object controlKey, DisplayControl control)
        {
            RemoveDeadDisplayControls();
            _displayControls[controlKey] = new WeakReference(control);
        }
    }
}