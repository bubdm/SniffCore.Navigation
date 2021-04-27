using System.Threading.Tasks;
using SniffCore;
using SniffCore.Navigation;
using TryOut.ViewModels.MainPages;
using Unity;

namespace TryOut.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IUnityContainer _unityContainer;

        public MainViewModel(INavigationService navigationService, IUnityContainer unityContainer)
        {
            _navigationService = navigationService;
            _unityContainer = unityContainer;

            ShowControlCommand = new AsyncDelegateCommand<string>(ShowControlAsync);
        }

        public IDelegateCommand ShowControlCommand { get; }

        private async Task ShowControlAsync(string controlKey)
        {
            ObservableObject vm = null;
            switch (controlKey)
            {
                case "DialogsView":
                    vm = _unityContainer.Resolve<DialogsViewModel>();
                    break;
                case "DisplayControlView":
                    vm = _unityContainer.Resolve<DisplayControlViewModel>();
                    break;
                case "InputValidationsView":
                    vm = _unityContainer.Resolve<InputValidationsViewModel>();
                    break;
                case "LockedView":
                    vm = _unityContainer.Resolve<LockedViewModel>();
                    break;
                case "MessageBoxesView":
                    vm = _unityContainer.Resolve<MessageBoxesViewModel>();
                    break;
                case "NavigationPresenterView":
                    vm = _unityContainer.Resolve<NavigationPresenterViewModel>();
                    break;
                case "PendingChangesView":
                    vm = _unityContainer.Resolve<PendingChangesViewModel>();
                    break;
                case "WindowsView":
                    vm = _unityContainer.Resolve<WindowsViewModel>();
                    break;
            }

            await _navigationService.ShowControlAsync("MainSpot", controlKey, vm);
        }
    }
}