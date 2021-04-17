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