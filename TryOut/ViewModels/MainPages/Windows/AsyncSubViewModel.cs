using System.Threading.Tasks;
using SniffCore;
using SniffCore.Navigation;

namespace TryOut.ViewModels.MainPages.Windows
{
    public class AsyncSubViewModel : ObservableObject, IAsyncLoader
    {
        public async Task LoadAsync()
        {
            await Task.Delay(3000);
        }
    }
}