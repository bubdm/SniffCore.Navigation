//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Threading.Tasks;
using SniffCore.Navigation.PleaseWaits;

namespace SniffCore.Navigation
{
    /// <summary>
    ///     Provides an async load method for a ViewModel which gets called before a window or user control is displayed by the
    ///     <see cref="NavigationService" />.
    /// </summary>
    public interface IDelayedAsyncLoader
    {
        /// <summary>
        ///     Loads the data in the ViewModel async and shows the window or user control after this finished.
        /// </summary>
        /// <param name="progress">The loading progress to report.</param>
        /// <returns>The task to await before the window or user control gets shown.</returns>
        Task LoadAsync(LoadingProgress progress);
    }
}