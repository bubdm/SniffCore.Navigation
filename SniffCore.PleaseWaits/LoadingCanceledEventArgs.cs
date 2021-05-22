//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;

namespace SniffCore.PleaseWaits
{
    /// <summary>
    ///     Holds the loading data.
    /// </summary>
    public sealed class LoadingCanceledEventArgs : EventArgs
    {
        internal LoadingCanceledEventArgs(LoadingCanceled data)
        {
            Data = data;
        }

        /// <summary>
        ///     The loading data.
        /// </summary>
        public LoadingCanceled Data { get; }
    }
}