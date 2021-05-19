//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Windows;

namespace SniffCore.MessageBoxes
{
    public interface IMessageBoxProvider
    {
        MessageBoxResult Show(string messageBoxText);
        MessageBoxResult Show(Window owner, string messageBoxText);
        MessageBoxResult Show(string messageBoxText, string caption);
        MessageBoxResult Show(Window owner, string messageBoxText, string caption);
        MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button);
        MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button);
        MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options);
        MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options);
    }
}