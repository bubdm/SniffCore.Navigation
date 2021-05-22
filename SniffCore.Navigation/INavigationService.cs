//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Threading.Tasks;
using System.Windows;
using SniffCore.Dialogs;
using SniffCore.MessageBoxes;

namespace SniffCore.Navigation
{
    /// <summary>
    ///     Provides ways to show windows, user controls, dialogs and more.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        ///     Shows a non modal window.
        /// </summary>
        /// <param name="windowKey">The key of the window to generate.</param>
        /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
        /// <returns>The task to await.</returns>
        Task ShowWindowAsync(object windowKey, object viewModel);

        /// <summary>
        ///     Shows a non modal window.
        /// </summary>
        /// <param name="ownerWindowKey">The key of the open owner window.</param>
        /// <param name="windowKey">The key of the window to generate.</param>
        /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
        /// <returns>The task to await.</returns>
        Task ShowWindowAsync(object ownerWindowKey, object windowKey, object viewModel);

        /// <summary>
        ///     Shows a modal window.
        /// </summary>
        /// <param name="windowKey">The key of the window to generate.</param>
        /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
        /// <returns>The task to await with the DialogResult.</returns>
        Task<bool?> ShowModalWindowAsync(object windowKey, object viewModel);

        /// <summary>
        ///     Shows a modal window.
        /// </summary>
        /// <param name="ownerWindowKey">The key of the open owner window.</param>
        /// <param name="windowKey">The key of the window to generate.</param>
        /// <param name="viewModel">The ViewModel to set into the DataContext of the newly created window.</param>
        /// <returns>The task to await with the DialogResult.</returns>
        Task<bool?> ShowModalWindowAsync(object ownerWindowKey, object windowKey, object viewModel);

        /// <summary>
        ///     Sets the dialog result of the open modal window by its key.
        ///     That does not work for non modal windows.
        /// </summary>
        /// <param name="windowKey">The key of the window which dialog result to set.</param>
        /// <param name="dialogResult">The dialog result to set.</param>
        void SetDialogResult(object windowKey, bool? dialogResult);

        /// <summary>
        ///     Closes the open window known by its key.
        ///     If the window was modal, the DialogResult will be null.
        /// </summary>
        /// <param name="windowKey">The key of the window to close.</param>
        void Close(object windowKey);

        /// <summary>
        ///     Shows a new user control by its control key into the <see cref="NavigationPresenter" /> known by its id.
        /// </summary>
        /// <param name="hostId">The ID of the <see cref="NavigationPresenter" /> where to display the user control.</param>
        /// <param name="controlKey">The ID of the user control to create.</param>
        /// <param name="viewModel">The ViewModel which will be set into the DataContext of newly created user control.</param>
        /// <returns>The task to await.</returns>
        Task ShowControlAsync(object hostId, object controlKey, object viewModel);

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        MessageBoxResult ShowMessageBox(string messageBoxText);

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="ownerWindowKey">The key of the owner window for the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText);

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <returns>The result of the message box after closing.</returns>
        MessageBoxResult ShowMessageBox(string messageBoxText, string caption);

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="ownerWindowKey">The key of the owner window for the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <returns>The result of the message box after closing.</returns>
        MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption);

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button);

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="ownerWindowKey">The key of the owner window for the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption, MessageBoxButton button);

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <param name="options">The additional options for the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options);

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="ownerWindowKey">The key of the owner window for the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <param name="options">The additional options for the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options);

        /// <summary>
        ///     Shows the open file dialog.
        /// </summary>
        /// <param name="openFileData">The open file dialog data.</param>
        /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
        bool ShowDialog(IOpenFileData openFileData);

        /// <summary>
        ///     Shows the save file dialog.
        /// </summary>
        /// <param name="saveFileData">The save file dialog data.</param>
        /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
        bool ShowDialog(ISaveFileData saveFileData);

        /// <summary>
        ///     Shows the browse folder dialog.
        /// </summary>
        /// <param name="browseFolderData">The browse folder dialog data.</param>
        /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
        bool ShowDialog(IBrowseFolderData browseFolderData);
    }
}