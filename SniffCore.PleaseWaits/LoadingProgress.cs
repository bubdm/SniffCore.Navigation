using System;

namespace SniffCore.PleaseWaits
{
    public sealed class LoadingProgress
    {
        public void Report(double progress)
        {
            ProgressUpdated?.Invoke(this, new ProgressDataEventArgs(new ProgressData(progress)));
        }

        public void Report(string message)
        {
            ProgressUpdated?.Invoke(this, new ProgressDataEventArgs(new ProgressData(message)));
        }

        public void Report(double progress, string message)
        {
            ProgressUpdated?.Invoke(this, new ProgressDataEventArgs(new ProgressData(progress, message)));
        }

        public void Report(object messageData)
        {
            ProgressUpdated?.Invoke(this, new ProgressDataEventArgs(new ProgressData(messageData)));
        }

        public void Report(double progress, object messageData)
        {
            ProgressUpdated?.Invoke(this, new ProgressDataEventArgs(new ProgressData(progress, messageData)));
        }

        public void Cancel()
        {
            ProgressCanceled?.Invoke(this, new LoadingCanceledEventArgs(new LoadingCanceled()));
        }

        public void Cancel(string reason)
        {
            ProgressCanceled?.Invoke(this, new LoadingCanceledEventArgs(new LoadingCanceled(reason)));
        }

        public void Cancel(object reasonData)
        {
            ProgressCanceled?.Invoke(this, new LoadingCanceledEventArgs(new LoadingCanceled(reasonData)));
        }

        public event EventHandler<ProgressDataEventArgs> ProgressUpdated;
        public event EventHandler<LoadingCanceledEventArgs> ProgressCanceled;
    }
}