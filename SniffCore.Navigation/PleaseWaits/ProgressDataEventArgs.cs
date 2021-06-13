//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;

namespace SniffCore.Navigation.PleaseWaits
{
    /// <summary>
    ///     Holds the progress data.
    /// </summary>
    public sealed class ProgressDataEventArgs : EventArgs
    {
        internal ProgressDataEventArgs(ProgressData data)
        {
            Data = data;
        }

        /// <summary>
        ///     The progress data.
        /// </summary>
        public ProgressData Data { get; }
    }
}