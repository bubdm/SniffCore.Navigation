using System.Threading.Tasks;
using SniffCore.PleaseWaits;

namespace SniffCore.Navigation
{
    public interface IDelayedAsyncLoader
    {
        Task LoadAsync(LoadingProgress progress);
    }
}