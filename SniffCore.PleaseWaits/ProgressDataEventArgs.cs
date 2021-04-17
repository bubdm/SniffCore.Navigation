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