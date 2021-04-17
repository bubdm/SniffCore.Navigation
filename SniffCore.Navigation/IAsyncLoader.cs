using System.Threading.Tasks;

namespace SniffCore.Navigation
{
    public interface IAsyncLoader
    {
        Task LoadAsync();
    }
}