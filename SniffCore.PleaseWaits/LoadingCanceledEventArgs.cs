using System;

namespace SniffCore.PleaseWaits
{
    public sealed class LoadingCanceledEventArgs : EventArgs
    {
        public LoadingCanceledEventArgs(LoadingCanceled data)
        {
            Data = data;
        }

        public LoadingCanceled Data { get; }
    }
}