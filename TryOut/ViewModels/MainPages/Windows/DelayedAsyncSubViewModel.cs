using System.Threading.Tasks;
using SniffCore.Navigation;
using SniffCore.PleaseWaits;

namespace TryOut.ViewModels.MainPages.Windows
{
    public class DelayedAsyncSubViewModel : IDelayedAsyncLoader
    {
        public async Task LoadAsync(LoadingProgress progress)
        {
            for (var i = 1; i <= 100; i++)
            {
                await Task.Delay(10);
                progress.Report(i);
            }
        }
    }
}