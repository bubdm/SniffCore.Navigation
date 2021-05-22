//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;

namespace SniffCore.PleaseWaits
{
    /// <summary>
    ///     Provides possibility to report progress.
    /// </summary>
    public sealed class LoadingProgress
    {
        /// <summary>
        ///     Reports loading progress.
        /// </summary>
        /// <param name="progress">The loading progress value.</param>
        public void Report(double progress)
        {
            ProgressUpdated?.Invoke(this, new ProgressDataEventArgs(new ProgressData(progress)));
        }

        /// <summary>
        ///     Reports loading progress message.
        /// </summary>
        /// <param name="message">The loading progress message.</param>
        public void Report(string message)
        {
            ProgressUpdated?.Invoke(this, new ProgressDataEventArgs(new ProgressData(message)));
        }

        /// <summary>
        ///     Reports loading progress message and value.
        /// </summary>
        /// <param name="progress">The loading progress value.</param>
        /// <param name="message">The loading progress message.</param>
        public void Report(double progress, string message)
        {
            ProgressUpdated?.Invoke(this, new ProgressDataEventArgs(new ProgressData(progress, message)));
        }

        /// <summary>
        ///     Reports loading progress data.
        /// </summary>
        /// <param name="messageData">The loading progress data.</param>
        public void Report(object messageData)
        {
            ProgressUpdated?.Invoke(this, new ProgressDataEventArgs(new ProgressData(messageData)));
        }

        /// <summary>
        ///     Reports loading progress data and value
        /// </summary>
        /// <param name="progress">The loading progress value.</param>
        /// <param name="messageData">The loading progress data.</param>
        public void Report(double progress, object messageData)
        {
            ProgressUpdated?.Invoke(this, new ProgressDataEventArgs(new ProgressData(progress, messageData)));
        }

        /// <summary>
        ///     Cancels the loading.
        /// </summary>
        public void Cancel()
        {
            ProgressCanceled?.Invoke(this, new LoadingCanceledEventArgs(new LoadingCanceled()));
        }

        /// <summary>
        ///     Cancels the loading with a reason message.
        /// </summary>
        /// <param name="reason">The loading cancel reason message.</param>
        public void Cancel(string reason)
        {
            ProgressCanceled?.Invoke(this, new LoadingCanceledEventArgs(new LoadingCanceled(reason)));
        }

        /// <summary>
        ///     Cancels the loading with a reason data.
        /// </summary>
        /// <param name="reasonData">The loading cancel reason data.</param>
        public void Cancel(object reasonData)
        {
            ProgressCanceled?.Invoke(this, new LoadingCanceledEventArgs(new LoadingCanceled(reasonData)));
        }

        /// <summary>
        ///     Raised if the progress changed.
        /// </summary>
        public event EventHandler<ProgressDataEventArgs> ProgressUpdated;

        /// <summary>
        ///     Raised if the loading got canceled.
        /// </summary>
        public event EventHandler<LoadingCanceledEventArgs> ProgressCanceled;
    }
}