//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace SniffCore.Navigation.PleaseWaits
{
    /// <summary>
    ///     Holds the loading progress data.
    /// </summary>
    public sealed class ProgressData
    {
        internal ProgressData(double progress)
        {
            Progress = progress;
        }

        internal ProgressData(string message)
        {
            Message = message;
        }

        internal ProgressData(double progress, string message)
        {
            Message = message;
            Progress = progress;
        }

        internal ProgressData(object messageData)
        {
            MessageData = messageData;
        }

        internal ProgressData(double progress, object messageData)
        {
            MessageData = messageData;
            Progress = progress;
        }

        /// <summary>
        ///     The loading progress message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        ///     The loading progress data.
        /// </summary>
        public object MessageData { get; }

        /// <summary>
        ///     The loading progress value.
        /// </summary>
        public double Progress { get; }
    }
}