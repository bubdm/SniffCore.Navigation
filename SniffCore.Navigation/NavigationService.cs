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
using SniffCore.Navigation.Dialogs;
using SniffCore.Navigation.External;
using SniffCore.Navigation.MessageBoxes;
using SniffCore.Navigation.PleaseWaits;
using SniffCore.Navigation.Windows;

// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable PossibleNullReferenceException

namespace SniffCore.Navigation
{
    /// <summary>
    ///     Provides ways to show windows, user controls, dialogs and more.
    /// </summary>
    /// <example>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// public void Bootstrapper
    /// {
    ///     IUnityContainer _unityContainer;
    /// 
    ///     public Bootstrapper()
    ///     {
    ///         _unityContainer = new UnityContainer();
    ///         _unityContainer.RegisterSingleton<IWindowProvider, WindowProvider>();
    ///         _unityContainer.RegisterType<IDialogProvider, DialogProvider>();
    ///         _unityContainer.RegisterType<IMessageBoxProvider, MessageBoxProvider>();
    ///         _unityContainer.RegisterType<IPleaseWaitProvider, PleaseWaitProvider>();
    ///         _unityContainer.RegisterType<INavigationService, NavigationService>();
    /// 
    ///         RegisterViews();
    ///     }
    /// 
    ///     public void RegisterViews()
    ///     {
    ///         var windowProvider = (WindowProvider) _unityContainer.Resolve<IWindowProvider>();
    ///         
    ///         windowProvider.RegisterWindow<MainView>("MainView");
    ///         windowProvider.RegisterWindow<SubView>("SubView");
    ///         
    ///         windowProvider.RegisterControl<DialogsView>("DialogsView");
    ///         windowProvider.RegisterControl<DisplayControlView>("DisplayControlView");
    ///     }
    /// }
    /// ]]>
    /// </code>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// public class WindowViewModel : ObservableObject, IAsyncLoader
    /// {
    ///     public async Task LoadAsync()
    ///     {
    ///         // Loads the data as soon the window got shown.
    ///         await Task.CompletedTask;
    ///     }
    /// }
    /// ]]>
    /// </code>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// public void ViewModel : ObservableObject
    /// {
    ///     private INavigationService _navigationService;
    /// 
    ///     public ViewModel(INavigationService navigationService)
    ///     {
    ///         _navigationService = navigationService;
    ///     }
    /// 
    ///     public async Task ShowWindow()
    ///     {
    ///         var vm = new WindowViewModel();
    ///         await _navigationService.ShowModalWindow("MainView", vm);
    ///     }
    /// }
    /// ]]>
    /// </code>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// [TestFixture]
    /// public class ViewModelTests
    /// {
    ///     private Mock<INavigationService> _navigationService;
    ///     private ViewModel _target;
    /// 
    ///     [SetUp]
    ///     public void Setup()
    ///     {
    ///         _navigationService = new Mock<INavigationService>();
    ///         _target = new ViewModel(_navigationService.Object);
    ///     }
    /// 
    ///     [Test]
    ///     public void ShowWindow_Called_ShowsTheWindow()
    ///     {
    ///         _target.ShowWindow();
    /// 
    ///         _navigationService.Verify(x => x.ShowModalWindow(Args.Any<string>(), Args.Any<object>()), Times.Once);
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public class NavigationService : INavigationService
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
        /// <exception cref="ArgumentNullException">windowProvider is null.</exception>
        /// <exception cref="ArgumentNullException">messageBoxProvider is null.</exception>
        /// <exception cref="ArgumentNullException">pleaseWaitProvider is null.</exception>
        /// <exception cref="ArgumentNullException">dialogProvider is null.</exception>
        public NavigationService(IWindowProvider windowProvider,
            IMessageBoxProvider messageBoxProvider,
            IPleaseWaitProvider pleaseWaitProvider,
            IDialogProvider dialogProvider)
        {
            _windowProvider = windowProvider ?? throw new ArgumentNullException(nameof(windowProvider));
            _messageBoxProvider = messageBoxProvider ?? throw new ArgumentNullException(nameof(messageBoxProvider));
            _pleaseWaitProvider = pleaseWaitProvider ?? throw new ArgumentNullException(nameof(pleaseWaitProvider));
            _dialogProvider = dialogProvider ?? throw new ArgumentNullException(nameof(dialogProvider));
        }

        /// <summary>
        ///     Shows a non modal window.
        /// </summary>
        /// <param name="windowKey">The key of the window to generate.</param>
        /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
        /// <returns>The task to await.</returns>
        /// <exception cref="ArgumentNullException">windowKey is null.</exception>
        /// <exception cref="ArgumentNullException">viewModel is null.</exception>
        public Task ShowWindowAsync(object windowKey, object viewModel)
        {
            if (windowKey == null)
                throw new ArgumentNullException(nameof(windowKey));
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            return ShowWindowImplAsync(null, windowKey, viewModel);
        }

        /// <summary>
        ///     Shows a non modal window.
        /// </summary>
        /// <param name="ownerWindowKey">The key of the open owner window.</param>
        /// <param name="windowKey">The key of the window to generate.</param>
        /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
        /// <returns>The task to await.</returns>
        /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
        /// <exception cref="ArgumentNullException">windowKey is null.</exception>
        /// <exception cref="ArgumentNullException">viewModel is null.</exception>
        public Task ShowWindowAsync(object ownerWindowKey, object windowKey, object viewModel)
        {
            if (ownerWindowKey == null)
                throw new ArgumentNullException(nameof(ownerWindowKey));
            if (windowKey == null)
                throw new ArgumentNullException(nameof(windowKey));
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            return ShowWindowImplAsync(ownerWindowKey, windowKey, viewModel);
        }

        /// <summary>
        ///     Shows a modal window.
        /// </summary>
        /// <param name="windowKey">The key of the window to generate.</param>
        /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
        /// <returns>The task to await with the DialogResult.</returns>
        /// <exception cref="ArgumentNullException">windowKey is null.</exception>
        /// <exception cref="ArgumentNullException">viewModel is null.</exception>
        public Task<bool?> ShowModalWindowAsync(object windowKey, object viewModel)
        {
            if (windowKey == null)
                throw new ArgumentNullException(nameof(windowKey));
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            return ShowModalWindowImplAsync(null, windowKey, viewModel);
        }

        /// <summary>
        ///     Shows a modal window.
        /// </summary>
        /// <param name="ownerWindowKey">The key of the open owner window.</param>
        /// <param name="windowKey">The key of the window to generate.</param>
        /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
        /// <returns>The task to await with the DialogResult.</returns>
        /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
        /// <exception cref="ArgumentNullException">windowKey is null.</exception>
        /// <exception cref="ArgumentNullException">viewModel is null.</exception>
        public Task<bool?> ShowModalWindowAsync(object ownerWindowKey, object windowKey, object viewModel)
        {
            if (ownerWindowKey == null)
                throw new ArgumentNullException(nameof(ownerWindowKey));
            if (windowKey == null)
                throw new ArgumentNullException(nameof(windowKey));
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            return ShowModalWindowImplAsync(ownerWindowKey, windowKey, viewModel);
        }

        /// <summary>
        ///     Sets the dialog result of the open modal window by its key.
        ///     That does not work for non modal windows.
        /// </summary>
        /// <param name="windowKey">The key of the window which dialog result to set.</param>
        /// <param name="dialogResult">The dialog result to set.</param>
        /// <exception cref="ArgumentNullException">windowKey is null.</exception>
        public void SetDialogResult(object windowKey, bool? dialogResult)
        {
            if (windowKey == null)
                throw new ArgumentNullException(nameof(windowKey));

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
        /// <exception cref="ArgumentNullException">windowKey is null.</exception>
        public void Close(object windowKey)
        {
            if (windowKey == null)
                throw new ArgumentNullException(nameof(windowKey));

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
        /// <exception cref="ArgumentNullException">hostId is null.</exception>
        /// <exception cref="ArgumentNullException">controlKey is null.</exception>
        /// <exception cref="ArgumentNullException">viewModel is null.</exception>
        public async Task ShowControlAsync(object hostId, object controlKey, object viewModel)
        {
            if (hostId == null)
                throw new ArgumentNullException(nameof(hostId));
            if (controlKey == null)
                throw new ArgumentNullException(nameof(controlKey));
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

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
        /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
        public MessageBoxResult ShowMessageBox(string messageBoxText)
        {
            if (messageBoxText == null)
                throw new ArgumentNullException(nameof(messageBoxText));

            return _messageBoxProvider.Show(messageBoxText);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="ownerWindowKey">The key of the owner window for the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
        /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
        public MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText)
        {
            if (ownerWindowKey == null)
                throw new ArgumentNullException(nameof(ownerWindowKey));
            if (messageBoxText == null)
                throw new ArgumentNullException(nameof(messageBoxText));

            return _messageBoxProvider.Show(_windowProvider.GetOpenWindow(ownerWindowKey), messageBoxText);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <returns>The result of the message box after closing.</returns>
        /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
        /// <exception cref="ArgumentNullException">caption is null.</exception>
        public MessageBoxResult ShowMessageBox(string messageBoxText, string caption)
        {
            if (messageBoxText == null)
                throw new ArgumentNullException(nameof(messageBoxText));
            if (caption == null)
                throw new ArgumentNullException(nameof(caption));

            return _messageBoxProvider.Show(messageBoxText, caption);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="ownerWindowKey">The key of the owner window for the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <returns>The result of the message box after closing.</returns>
        /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
        /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
        /// <exception cref="ArgumentNullException">caption is null.</exception>
        public MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption)
        {
            if (ownerWindowKey == null)
                throw new ArgumentNullException(nameof(ownerWindowKey));
            if (messageBoxText == null)
                throw new ArgumentNullException(nameof(messageBoxText));
            if (caption == null)
                throw new ArgumentNullException(nameof(caption));

            return _messageBoxProvider.Show(_windowProvider.GetOpenWindow(ownerWindowKey), messageBoxText, caption);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
        /// <exception cref="ArgumentNullException">caption is null.</exception>
        public MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button)
        {
            if (messageBoxText == null)
                throw new ArgumentNullException(nameof(messageBoxText));
            if (caption == null)
                throw new ArgumentNullException(nameof(caption));

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
        /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
        /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
        /// <exception cref="ArgumentNullException">caption is null.</exception>
        public MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption, MessageBoxButton button)
        {
            if (ownerWindowKey == null)
                throw new ArgumentNullException(nameof(ownerWindowKey));
            if (messageBoxText == null)
                throw new ArgumentNullException(nameof(messageBoxText));
            if (caption == null)
                throw new ArgumentNullException(nameof(caption));

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
        /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
        /// <exception cref="ArgumentNullException">caption is null.</exception>
        /// <exception cref="ArgumentNullException">options is null.</exception>
        public MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options)
        {
            if (messageBoxText == null)
                throw new ArgumentNullException(nameof(messageBoxText));
            if (caption == null)
                throw new ArgumentNullException(nameof(caption));
            if (options == null)
                throw new ArgumentNullException(nameof(options));

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
        /// <exception cref="ArgumentNullException">ownerWindowKey is null.</exception>
        /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
        /// <exception cref="ArgumentNullException">caption is null.</exception>
        /// <exception cref="ArgumentNullException">options is null.</exception>
        public MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options)
        {
            if (ownerWindowKey == null)
                throw new ArgumentNullException(nameof(ownerWindowKey));
            if (messageBoxText == null)
                throw new ArgumentNullException(nameof(messageBoxText));
            if (caption == null)
                throw new ArgumentNullException(nameof(caption));
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return _messageBoxProvider.Show(_windowProvider.GetOpenWindow(ownerWindowKey), messageBoxText, caption, button, options);
        }

        /// <summary>
        ///     Shows the open file dialog.
        /// </summary>
        /// <param name="openFileData">The open file dialog data.</param>
        /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">openFileData is null.</exception>
        public bool ShowDialog(IOpenFileData openFileData)
        {
            if (openFileData == null)
                throw new ArgumentNullException(nameof(openFileData));

            return _dialogProvider.Show(openFileData);
        }

        /// <summary>
        ///     Shows the save file dialog.
        /// </summary>
        /// <param name="saveFileData">The save file dialog data.</param>
        /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">saveFileData is null.</exception>
        public bool ShowDialog(ISaveFileData saveFileData)
        {
            if (saveFileData == null)
                throw new ArgumentNullException(nameof(saveFileData));

            return _dialogProvider.Show(saveFileData);
        }

        /// <summary>
        ///     Shows the browse folder dialog.
        /// </summary>
        /// <param name="browseFolderData">The browse folder dialog data.</param>
        /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">browseFolderData is null.</exception>
        public bool ShowDialog(IBrowseFolderData browseFolderData)
        {
            if (browseFolderData == null)
                throw new ArgumentNullException(nameof(browseFolderData));

            return _dialogProvider.Show(browseFolderData);
        }

        private async Task ShowWindowImplAsync(object ownerWindowKey, object windowKey, object viewModel)
        {
            var window = CreateWindow(ownerWindowKey, windowKey, viewModel);
            switch (viewModel)
            {
                case IAsyncLoader asyncLoader:
                {
                    WindowShow(window);
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
                        WindowShow(window);
                    break;
                }
                default:
                {
                    WindowShow(window);
                    break;
                }
            }
        }

        private async Task<bool?> ShowModalWindowImplAsync(object ownerWindowKey, object windowKey, object viewModel)
        {
            var window = CreateWindow(ownerWindowKey, windowKey, viewModel);
            switch (viewModel)
            {
                case IAsyncLoader asyncLoader:
                {
                    asyncLoader.LoadAsync().FireAndForget();
                    return WindowShowDialog(window);
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
                    return isCanceled ? null : WindowShowDialog(window);
                }
                default:
                {
                    return WindowShowDialog(window);
                }
            }
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

        // Unit tests only.
        internal static Dictionary<object, WeakReference> GetRegisteredControls()
        {
            return _navigationPresenter;
        }

        // For unit test since Window.Show cannot be done there
        internal virtual void WindowShow(Window window)
        {
            window.Show();
        }

        // For unit test since Window.ShowDialog cannot be done there
        internal virtual bool? WindowShowDialog(Window window)
        {
            return window.ShowDialog();
        }
    }
}