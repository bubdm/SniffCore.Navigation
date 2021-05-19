//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;

namespace SniffCore.PleaseWaits
{
    public sealed class ProgressDataEventArgs : EventArgs
    {
        public ProgressDataEventArgs(ProgressData data)
        {
            Data = data;
        }

        public ProgressData Data { get; }
    }
}