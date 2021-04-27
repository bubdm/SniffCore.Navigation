using System.Threading.Tasks;
using SniffCore;
using SniffCore.Navigation;
using TryOut.ViewModels.MainPages.NavigationPresenter;

namespace TryOut.ViewModels.MainPages
{
    public class NavigationPresenterViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public NavigationPresenterViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowAsyncViewModelCommand = new AsyncDelegateCommand(ShowAsyncViewModelAsync);
            ShowDelayedAsyncViewModelCommand = new AsyncDelegateCommand(ShowDelayedAsyncViewModelAsync);
        }

        public IDelegateCommand ShowAsyncViewModelCommand { get; }
        public IDelegateCommand ShowDelayedAsyncViewModelCommand { get; }

        private async Task ShowAsyncViewModelAsync()
        {
            var vm = new NavigationAsyncViewModel();
            await _navigationService.ShowControlAsync("Spot", "NavigationAsyncView", vm);
        }

        private async Task ShowDelayedAsyncViewModelAsync()
        {
            var vm = new NavigationDelayedAsyncViewModel();
            await _navigationService.ShowControlAsync("Spot", "NavigationDelayedAsyncView", vm);
        }
    }
}