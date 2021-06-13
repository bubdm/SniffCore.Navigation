using System.Threading.Tasks;
using SniffCore;
using SniffCore.Navigation;
using SniffCore.Navigation.PleaseWaits;

namespace TryOut.ViewModels.MainPages.Windows
{
    public class DelayedAsyncSubViewModel : ObservableObject, IDelayedAsyncLoader
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