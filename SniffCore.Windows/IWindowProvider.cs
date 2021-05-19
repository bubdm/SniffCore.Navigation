//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Windows;
using System.Windows.Controls;

namespace SniffCore.Windows
{
    public interface IWindowProvider
    {
        Window GetNewWindow(object windowKey);
        Window GetOpenWindow(object windowKey);

        UserControl GetNewControl(object controlKey);
    }
}