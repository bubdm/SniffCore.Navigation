using System.Threading.Tasks;
using SniffCore.Navigation;

namespace TryOut.ViewModels.MainPages.Windows
{
    public class AsyncSubViewModel : IAsyncLoader
    {
        public async Task LoadAsync()
        {
            await Task.Delay(3000);
        }
    }
}