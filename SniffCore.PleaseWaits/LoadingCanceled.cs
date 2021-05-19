//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

namespace SniffCore.PleaseWaits
{
    public sealed class LoadingCanceled
    {
        internal LoadingCanceled()
        {
        }

        internal LoadingCanceled(string reason)
        {
            Reason = reason;
        }

        internal LoadingCanceled(object reasonData)
        {
            ReasonData = reasonData;
        }

        public string Reason { get; }
        public object ReasonData { get; }
    }
}