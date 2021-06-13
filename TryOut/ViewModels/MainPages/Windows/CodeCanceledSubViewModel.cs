using System.Threading.Tasks;
using SniffCore;
using SniffCore.Navigation;
using SniffCore.Navigation.PleaseWaits;

namespace TryOut.ViewModels.MainPages.Windows
{
    public class CodeCanceledSubViewModel : ObservableObject, IDelayedAsyncLoader
    {
        public async Task LoadAsync(LoadingProgress progress)
        {
            for (var i = 1; i <= 75; i++)
            {
                await Task.Delay(10);
                progress.Report(i);
            }

            progress.Cancel("Loading canceled, because reasons, that's why");
        }
    }
}