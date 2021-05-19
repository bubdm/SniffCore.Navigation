//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Windows;

namespace SniffCore.MessageBoxes
{
    public sealed class MessageBoxOptions : IMessageBoxOptions
    {
        public MessageBoxOptions(MessageBoxImage? icon)
            : this(icon, null)
        {
        }

        public MessageBoxOptions(MessageBoxResult? defaultResult)
            : this(null, defaultResult)
        {
        }

        public MessageBoxOptions(MessageBoxImage? icon, MessageBoxResult? defaultResult)
        {
            Icon = icon;
            DefaultResult = defaultResult;
        }

        public MessageBoxImage? Icon { get; set; }
        public MessageBoxResult? DefaultResult { get; set; }
    }
}