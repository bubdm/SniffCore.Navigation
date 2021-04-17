﻿namespace SniffCore.PleaseWaits
{
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

        public string Message { get; }
        public object MessageData { get; }
        public double Progress { get; }
    }
}