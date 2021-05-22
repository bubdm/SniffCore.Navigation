//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Threading.Tasks;

namespace SniffCore.Navigation
{
    /// <summary>
    ///     Provides an async load method for a ViewModel which gets called directly after a window or user control is
    ///     displayed by the <see cref="NavigationService" />.
    /// </summary>
    public interface IAsyncLoader
    {
        /// <summary>
        ///     Loads the data in the ViewModel async as soon the window or user control is displayed.
        /// </summary>
        /// <returns>The task to await.</returns>
        Task LoadAsync();
    }
}