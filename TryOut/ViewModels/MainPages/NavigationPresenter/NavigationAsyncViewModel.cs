using System.Threading.Tasks;
using SniffCore;
using SniffCore.Navigation;

namespace TryOut.ViewModels.MainPages.NavigationPresenter
{
    public class NavigationAsyncViewModel : ObservableObject, IAsyncLoader
    {
        private string _status;

        public string Status
        {
            get => _status;
            private set => NotifyAndSetIfChanged(ref _status, value);
        }

        public async Task LoadAsync()
        {
            Status = "Loading ...";
            await Task.Delay(3000);
            Status = "Done";
        }
    }
}