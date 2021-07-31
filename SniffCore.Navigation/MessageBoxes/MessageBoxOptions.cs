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
    public class MessageBoxOptions : IMessageBoxOptions
    {
        /// <summary>
        ///     Creates a new instance of <see cref="MessageBoxOptions" />.
        /// </summary>
        public MessageBoxOptions()
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="MessageBoxOptions" />.
        /// </summary>
        /// <param name="icon">The icon to show in the message box.</param>
        public MessageBoxOptions(MessageBoxImage icon)
        {
            Icon = icon;
        }

        /// <summary>
        ///     Creates a new instance of <see cref="MessageBoxOptions" />.
        /// </summary>
        /// <param name="defaultResult">The default result of the message box.</param>
        public MessageBoxOptions(MessageBoxResult defaultResult)
        {
            DefaultResult = defaultResult;
        }

        /// <summary>
        ///     Creates a new instance of <see cref="MessageBoxOptions" />.
        /// </summary>
        /// <param name="icon">The icon to show in the message box.</param>
        /// <param name="defaultResult">The default result of the message box.</param>
        public MessageBoxOptions(MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            Icon = icon;
            DefaultResult = defaultResult;
        }

        /// <summary>
        ///     [Optional] The icon to show in the message box.
        /// </summary>
        public MessageBoxImage? Icon { get; set; }

        /// <summary>
        ///     [Optional] The default result of the message box.
        /// </summary>
        public MessageBoxResult? DefaultResult { get; set; }
    }
}