//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Windows;
using System.Windows.Controls;

namespace SniffCore.Navigation.Windows
{
    /// <summary>
    ///     Provides possibilities to create and display windows and user controls.
    /// </summary>
    public interface IWindowProvider
    {
        /// <summary>
        ///     Creates a new window by the key.
        /// </summary>
        /// <param name="windowKey">The key of the window to create.</param>
        /// <returns>The newly created window.</returns>
        Window GetNewWindow(object windowKey);

        /// <summary>
        ///     Returns the open window by the key.
        /// </summary>
        /// <param name="windowKey">The key of the window to return.</param>
        /// <returns>The open window know by the key.</returns>
        Window GetOpenWindow(object windowKey);

        /// <summary>
        ///     Creates a new user control by the key.
        /// </summary>
        /// <param name="controlKey">The key of the user control to create.</param>
        /// <returns>The newly created user control.</returns>
        UserControl GetNewControl(object controlKey);
    }
}