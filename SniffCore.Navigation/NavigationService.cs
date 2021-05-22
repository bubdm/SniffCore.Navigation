//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

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

// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable PossibleNullReferenceException

namespace SniffCore.Navigation
{
    /// <summary>
    ///     Provides ways to show windows, user controls, dialogs and more.
    /// </summary>
    public sealed class NavigationService : INavigationService
    {
        private static readonly Dictionary<object, WeakReference> _navigationPresenter = new Dictionary<object, WeakReference>();
        private readonly IDialogProvider _dialogProvider;
        private readonly IMessageBoxProvider _messageBoxProvider;
        private readonly IPleaseWaitProvider _pleaseWaitProvider;
        private readonly IWindowProvider _windowProvider;

        /// <summary>
        ///     Creates a new instance of <see cref="NavigationPresenter" />.
        /// </summary>
        /// <param name="windowProvider">The provider of new windows or user controls.</param>
        /// <param name="messageBoxProvider">The provider to display message boxes.</param>
        /// <param name="pleaseWaitProvider">The provider to display and update a global please wait.</param>
        /// <param name="dialogProvider">The provider to display system dialogs.</param>
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

        /// <summary>
        ///     Shows a non modal window.
        /// </summary>
        /// <param name="windowKey">The key of the window to generate.</param>
        /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
        /// <returns>The task to await.</returns>
        public Task ShowWindowAsync(object windowKey, object viewModel)
        {
            return ShowWindowAsync(null, windowKey, viewModel);
        }

        /// <summary>
        ///     Shows a non modal window.
        /// </summary>
        /// <param name="ownerWindowKey">The key of the open owner window.</param>
        /// <param name="windowKey">The key of the window to generate.</param>
        /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
        /// <returns>The task to await.</returns>
        public async Task ShowWindowAsync(object ownerWindowKey, object windowKey, object viewModel)
        {
            var window = CreateWindow(ownerWindowKey, windowKey, viewModel);
            switch (viewModel)
            {
                case IAsyncLoader asyncLoader:
                {
                    window.Show();
                    await asyncLoader.LoadAsync();
                    break;
                }
                case IDelayedAsyncLoader delayedAsyncLoader:
                {
                    var loadingProgress = new LoadingProgress();
                    var isCanceled = false;

                    void LoadingProgressOnProgressUpdated(object sender, ProgressDataEventArgs e)
                    {
                        _pleaseWaitProvider.HandleProgress(e.Data);
                    }

                    void LoadingProgressOnProgressCanceled(object sender, LoadingCanceledEventArgs e)
                    {
                        isCanceled = true;
                        _pleaseWaitProvider.HandleCanceled(e.Data);
                    }

                    loadingProgress.ProgressUpdated += LoadingProgressOnProgressUpdated;
                    loadingProgress.ProgressCanceled += LoadingProgressOnProgressCanceled;
                    _pleaseWaitProvider.Show();
                    await delayedAsyncLoader.LoadAsync(loadingProgress);
                    loadingProgress.ProgressUpdated -= LoadingProgressOnProgressUpdated;
                    loadingProgress.ProgressCanceled -= LoadingProgressOnProgressCanceled;
                    _pleaseWaitProvider.Close();
                    if (!isCanceled)
                        window.Show();
                    break;
                }
                default:
                {
                    window.Show();
                    break;
                }
            }
        }

        /// <summary>
        ///     Shows a modal window.
        /// </summary>
        /// <param name="windowKey">The key of the window to generate.</param>
        /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
        /// <returns>The task to await with the DialogResult.</returns>
        public Task<bool?> ShowModalWindowAsync(object windowKey, object viewModel)
        {
            return ShowModalWindowAsync(null, windowKey, viewModel);
        }

        /// <summary>
        ///     Shows a modal window.
        /// </summary>
        /// <param name="ownerWindowKey">The key of the open owner window.</param>
        /// <param name="windowKey">The key of the window to generate.</param>
        /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
        /// <returns>The task to await with the DialogResult.</returns>
        public async Task<bool?> ShowModalWindowAsync(object ownerWindowKey, object windowKey, object viewModel)
        {
            var window = CreateWindow(ownerWindowKey, windowKey, viewModel);
            switch (viewModel)
            {
                case IAsyncLoader asyncLoader:
                {
                    asyncLoader.LoadAsync().FireAndForget();
                    return window.ShowDialog();
                }
                case IDelayedAsyncLoader delayedAsyncLoader:
                {
                    var loadingProgress = new LoadingProgress();
                    var isCanceled = false;

                    void LoadingProgressOnProgressUpdated(object sender, ProgressDataEventArgs e)
                    {
                        _pleaseWaitProvider.HandleProgress(e.Data);
                    }

                    void LoadingProgressOnProgressCanceled(object sender, LoadingCanceledEventArgs e)
                    {
                        isCanceled = true;
                        _pleaseWaitProvider.HandleCanceled(e.Data);
                    }

                    loadingProgress.ProgressUpdated += LoadingProgressOnProgressUpdated;
                    loadingProgress.ProgressCanceled += LoadingProgressOnProgressCanceled;
                    _pleaseWaitProvider.Show();
                    await delayedAsyncLoader.LoadAsync(loadingProgress);
                    loadingProgress.ProgressUpdated -= LoadingProgressOnProgressUpdated;
                    loadingProgress.ProgressCanceled -= LoadingProgressOnProgressCanceled;
                    _pleaseWaitProvider.Close();
                    return isCanceled ? null : window.ShowDialog();
                }
                default:
                {
                    return window.ShowDialog();
                }
            }
        }

        /// <summary>
        ///     Sets the dialog result of the open modal window by its key.
        ///     That does not work for non modal windows.
        /// </summary>
        /// <param name="windowKey">The key of the window which dialog result to set.</param>
        /// <param name="dialogResult">The dialog result to set.</param>
        public void SetDialogResult(object windowKey, bool? dialogResult)
        {
            var window = _windowProvider.GetOpenWindow(windowKey);
            if (window == null)
                throw new InvalidOperationException($"The window with the key '{windowKey}' cannot be closed. It not open anymore or got not opened by INavigationService.Show[Modal]WindowAsync");
            window.DialogResult = dialogResult;
        }

        /// <summary>
        ///     Closes the open window known by its key.
        ///     If the window was modal, the DialogResult will be null.
        /// </summary>
        /// <param name="windowKey">The key of the window to close.</param>
        public void Close(object windowKey)
        {
            var window = _windowProvider.GetOpenWindow(windowKey);
            if (window == null)
                throw new InvalidOperationException($"The window with the key '{windowKey}' cannot be closed. It not open anymore or got not opened by INavigationService.Show[Modal]WindowAsync");
            window.Close();
        }

        /// <summary>
        ///     Shows a new user control by its control key into the <see cref="NavigationPresenter" /> known by its id.
        /// </summary>
        /// <param name="hostId">The ID of the <see cref="NavigationPresenter" /> where to display the user control.</param>
        /// <param name="controlKey">The ID of the user control to create.</param>
        /// <param name="viewModel">The ViewModel which will be set into the DataContext of newly created user control.</param>
        /// <returns>The task to await.</returns>
        public async Task ShowControlAsync(object hostId, object controlKey, object viewModel)
        {
            RemoveDeadNavigationPresenter();
            if (!_navigationPresenter.TryGetValue(hostId, out var reference))
                throw new InvalidOperationException($"For the ID '{hostId}' no NavigationPresenter is registered");

            var control = _windowProvider.GetNewControl(controlKey);
            control.DataContext = viewModel;
            var host = (NavigationPresenter) reference.Target;
            switch (viewModel)
            {
                case IAsyncLoader asyncLoader:
                {
                    host.Content = control;
                    await asyncLoader.LoadAsync();
                    break;
                }
                case IDelayedAsyncLoader delayedAsyncLoader:
                {
                    var loadingProgress = new LoadingProgress();
                    host.Content = null;
                    host.PleaseWaitProgress = loadingProgress;
                    await delayedAsyncLoader.LoadAsync(loadingProgress);
                    host.PleaseWaitProgress = null;
                    host.Content = control;
                    break;
                }
                default:
                {
                    host.Content = control;
                    break;
                }
            }
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        public MessageBoxResult ShowMessageBox(string messageBoxText)
        {
            return _messageBoxProvider.Show(messageBoxText);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="ownerWindowKey">The key of the owner window for the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        public MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText)
        {
            return _messageBoxProvider.Show(_windowProvider.GetOpenWindow(ownerWindowKey), messageBoxText);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <returns>The result of the message box after closing.</returns>
        public MessageBoxResult ShowMessageBox(string messageBoxText, string caption)
        {
            return _messageBoxProvider.Show(messageBoxText, caption);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="ownerWindowKey">The key of the owner window for the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <returns>The result of the message box after closing.</returns>
        public MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption)
        {
            return _messageBoxProvider.Show(_windowProvider.GetOpenWindow(ownerWindowKey), messageBoxText, caption);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        public MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button)
        {
            return _messageBoxProvider.Show(messageBoxText, caption, button);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="ownerWindowKey">The key of the owner window for the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        public MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption, MessageBoxButton button)
        {
            return _messageBoxProvider.Show(_windowProvider.GetOpenWindow(ownerWindowKey), messageBoxText, caption, button);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <param name="options">The additional options for the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        public MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options)
        {
            return _messageBoxProvider.Show(messageBoxText, caption, button, options);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="ownerWindowKey">The key of the owner window for the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <param name="options">The additional options for the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        public MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options)
        {
            return _messageBoxProvider.Show(_windowProvider.GetOpenWindow(ownerWindowKey), messageBoxText, caption, button, options);
        }

        /// <summary>
        ///     Shows the open file dialog.
        /// </summary>
        /// <param name="openFileData">The open file dialog data.</param>
        /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
        public bool ShowDialog(IOpenFileData openFileData)
        {
            return _dialogProvider.Show(openFileData);
        }

        /// <summary>
        ///     Shows the save file dialog.
        /// </summary>
        /// <param name="saveFileData">The save file dialog data.</param>
        /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
        public bool ShowDialog(ISaveFileData saveFileData)
        {
            return _dialogProvider.Show(saveFileData);
        }

        /// <summary>
        ///     Shows the browse folder dialog.
        /// </summary>
        /// <param name="browseFolderData">The browse folder dialog data.</param>
        /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
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

        private static void HandleWindowClosing(object sender, CancelEventArgs e)
        {
            var window = (Window) sender;
            window.Closing -= HandleWindowClosing;
            (window.DataContext as IDisposable)?.Dispose();
        }

        internal static void UnregisterPresenter(object controlKey)
        {
            RemoveDeadNavigationPresenter();
            _navigationPresenter.Remove(controlKey);
        }

        private static void RemoveDeadNavigationPresenter()
        {
            var dead = _navigationPresenter.Where(x => !x.Value.IsAlive).ToList();
            foreach (var pair in dead)
                _navigationPresenter.Remove(pair.Key);
        }

        internal static void RegisterPresenter(object controlKey, NavigationPresenter control)
        {
            RemoveDeadNavigationPresenter();
            _navigationPresenter[controlKey] = new WeakReference(control);
        }
    }
}