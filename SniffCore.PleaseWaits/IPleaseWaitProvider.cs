namespace SniffCore.PleaseWaits
{
    public interface IPleaseWaitProvider
    {
        void Show();
        void Close();
        void HandleProgress(ProgressData progressData);
        void HandleCanceled(LoadingCanceled canceledData);
    }
}