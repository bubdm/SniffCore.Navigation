//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Windows;

namespace SniffCore.Navigation.MessageBoxes
{
    /// <summary>
    ///     Provides additional data for the message box.
    /// </summary>
    public interface IMessageBoxOptions
    {
        /// <summary>
        ///     [Optional] The icon to show in the message box.
        /// </summary>
        MessageBoxImage? Icon { get; set; }

        /// <summary>
        ///     [Optional] The default result of the message box.
        /// </summary>
        MessageBoxResult? DefaultResult { get; set; }
    }
}