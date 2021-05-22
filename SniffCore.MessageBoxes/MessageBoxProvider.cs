//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Windows;

// ReSharper disable ClassNeverInstantiated.Global

namespace SniffCore.MessageBoxes
{
    /// <summary>
    ///     Provides possibilities to show message boxes.
    /// </summary>
    public sealed class MessageBoxProvider : IMessageBoxProvider
    {
        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        public MessageBoxResult Show(string messageBoxText)
        {
            return MessageBox.Show(messageBoxText);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="owner">The owner window of the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        public MessageBoxResult Show(Window owner, string messageBoxText)
        {
            return MessageBox.Show(owner, messageBoxText);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <returns>The result of the message box after closing.</returns>
        public MessageBoxResult Show(string messageBoxText, string caption)
        {
            return MessageBox.Show(messageBoxText, caption);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="owner">The owner window of the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <returns>The result of the message box after closing.</returns>
        public MessageBoxResult Show(Window owner, string messageBoxText, string caption)
        {
            return MessageBox.Show(owner, messageBoxText, caption);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            return MessageBox.Show(messageBoxText, caption, button);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="owner">The owner window of the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        public MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button)
        {
            return MessageBox.Show(owner, messageBoxText, caption, button);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <param name="options">The additional options for the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options)
        {
            if (options.Icon != null && options.DefaultResult != null)
                return MessageBox.Show(messageBoxText, caption, button, options.Icon.Value, options.DefaultResult.Value);
            if (options.Icon != null)
                return MessageBox.Show(messageBoxText, caption, button, options.Icon.Value);
            if (options.DefaultResult != null)
                return MessageBox.Show(messageBoxText, caption, button, MessageBoxImage.None, options.DefaultResult.Value);
            return MessageBox.Show(messageBoxText, caption, button);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="owner">The owner window of the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <param name="options">The additional options for the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        public MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options)
        {
            if (options.Icon != null && options.DefaultResult != null)
                return MessageBox.Show(owner, messageBoxText, caption, button, options.Icon.Value, options.DefaultResult.Value);
            if (options.Icon != null)
                return MessageBox.Show(owner, messageBoxText, caption, button, options.Icon.Value);
            if (options.DefaultResult != null)
                return MessageBox.Show(owner, messageBoxText, caption, button, MessageBoxImage.None, options.DefaultResult.Value);
            return MessageBox.Show(owner, messageBoxText, caption, button);
        }
    }
}