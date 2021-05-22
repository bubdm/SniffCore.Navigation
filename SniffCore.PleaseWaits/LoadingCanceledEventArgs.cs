//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;

namespace SniffCore.PleaseWaits
{
    public sealed class LoadingCanceledEventArgs : EventArgs
    {
        internal LoadingCanceledEventArgs(LoadingCanceled data)
        {
            Data = data;
        }

        public LoadingCanceled Data { get; }
    }
}