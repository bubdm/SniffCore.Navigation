//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

namespace SniffCore.Navigation.PleaseWaits
{
    /// <summary>
    ///     Holds the loading canceled data.
    /// </summary>
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

        /// <summary>
        ///     The reason message why a loading is canceled.
        /// </summary>
        public string Reason { get; }

        /// <summary>
        ///     The reason data why loading is canceled.
        /// </summary>
        public object ReasonData { get; }
    }
}