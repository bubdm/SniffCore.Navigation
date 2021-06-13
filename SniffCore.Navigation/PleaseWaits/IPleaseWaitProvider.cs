//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

namespace SniffCore.Navigation.PleaseWaits
{
    /// <summary>
    ///     Provides possibility to show a global please wait.
    /// </summary>
    public interface IPleaseWaitProvider
    {
        /// <summary>
        ///     Shows the please wait.
        /// </summary>
        void Show();

        /// <summary>
        ///     Closes the please wait.
        /// </summary>
        void Close();

        /// <summary>
        ///     Updates the please wait with progress.
        /// </summary>
        /// <param name="progressData">The please wait progress.</param>
        void HandleProgress(ProgressData progressData);

        /// <summary>
        ///     Updates the please wait with canceled loading.
        /// </summary>
        /// <param name="canceledData">The cancel data.</param>
        void HandleCanceled(LoadingCanceled canceledData);
    }
}