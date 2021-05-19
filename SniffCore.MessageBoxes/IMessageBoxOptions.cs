﻿//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Windows;

namespace SniffCore.MessageBoxes
{
    public interface IMessageBoxOptions
    {
        MessageBoxImage? Icon { get; set; }
        MessageBoxResult? DefaultResult { get; set; }
    }
}