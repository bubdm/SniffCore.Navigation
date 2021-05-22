//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

namespace SniffCore.PleaseWaits
{
    /// <summary>
    ///     Provides a default IPleaseWaitProvider to register which does nothing.
    /// </summary>
    public sealed class InvisiblePleaseWaitProvider : IPleaseWaitProvider
    {
        /// <summary>
        ///     Does nothing.
        /// </summary>
        public void Show()
        {
        }

        /// <summary>
        ///     Does nothing.
        /// </summary>
        public void Close()
        {
        }

        /// <summary>
        ///     Does nothing.
        /// </summary>
        /// <param name="progressData">unused</param>
        public void HandleProgress(ProgressData progressData)
        {
        }

        /// <summary>
        ///     Does nothing.
        /// </summary>
        /// <param name="canceledData">unused</param>
        public void HandleCanceled(LoadingCanceled canceledData)
        {
        }
    }
}