//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Windows;

namespace SniffCore.Navigation.MessageBoxes
{
    /// <summary>
    ///     Provides possibilities to show message boxes.
    /// </summary>
    public interface IMessageBoxProvider
    {
        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        MessageBoxResult Show(string messageBoxText);

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="owner">The owner window of the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        MessageBoxResult Show(Window owner, string messageBoxText);

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <returns>The result of the message box after closing.</returns>
        MessageBoxResult Show(string messageBoxText, string caption);

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="owner">The owner window of the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <returns>The result of the message box after closing.</returns>
        MessageBoxResult Show(Window owner, string messageBoxText, string caption);

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button);

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="owner">The owner window of the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button);

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <param name="options">The additional options for the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options);

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="owner">The owner window of the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <param name="options">The additional options for the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options);
    }
}