//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Windows;

namespace SniffCore.MessageBoxes
{
    public sealed class MessageBoxProvider : IMessageBoxProvider
    {
        public MessageBoxResult Show(string messageBoxText)
        {
            return MessageBox.Show(messageBoxText);
        }

        public MessageBoxResult Show(Window owner, string messageBoxText)
        {
            return MessageBox.Show(owner, messageBoxText);
        }

        public MessageBoxResult Show(string messageBoxText, string caption)
        {
            return MessageBox.Show(messageBoxText, caption);
        }

        public MessageBoxResult Show(Window owner, string messageBoxText, string caption)
        {
            return MessageBox.Show(owner, messageBoxText, caption);
        }

        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            return MessageBox.Show(messageBoxText, caption, button);
        }

        public MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button)
        {
            return MessageBox.Show(owner, messageBoxText, caption, button);
        }

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