using System.Threading.Tasks;
using SniffCore;
using SniffCore.Navigation;
using TryOut.ViewModels.MainPages.Windows;

namespace TryOut.ViewModels.MainPages
{
    public class WindowsViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public WindowsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            AsyncLoaderCommand = new AsyncDelegateCommand(AsyncLoaderAsync);
            DelayedAsyncLoaderCommand = new AsyncDelegateCommand(DelayedAsyncLoaderAsync);
            CodeCanceledFolderCommand = new AsyncDelegateCommand(CodeCanceledFolderAsync);
            ModalAsyncLoaderCommand = new AsyncDelegateCommand(ModalAsyncLoaderAsync);
            ModalDelayedAsyncLoaderCommand = new AsyncDelegateCommand(ModalDelayedAsyncLoaderAsync);
            ModalCodeCanceledFolderCommand = new AsyncDelegateCommand(ModalCodeCanceledFolderAsync);
        }

        public IDelegateCommand AsyncLoaderCommand { get; }
        public IDelegateCommand DelayedAsyncLoaderCommand { get; }
        public IDelegateCommand CodeCanceledFolderCommand { get; }
        public IDelegateCommand ModalAsyncLoaderCommand { get; }
        public IDelegateCommand ModalDelayedAsyncLoaderCommand { get; }
        public IDelegateCommand ModalCodeCanceledFolderCommand { get; }

        private async Task AsyncLoaderAsync()
        {
            var vm = new AsyncSubViewModel();
            await _navigationService.ShowWindowAsync("MainView", "SubView", vm);
        }

        private async Task DelayedAsyncLoaderAsync()
        {
            var vm = new DelayedAsyncSubViewModel();
            await _navigationService.ShowWindowAsync("MainView", "SubView", vm);
        }

        private async Task CodeCanceledFolderAsync()
        {
            var vm = new CodeCanceledSubViewModel();
            await _navigationService.ShowWindowAsync("MainView", "SubView", vm);
        }

        private async Task ModalAsyncLoaderAsync()
        {
            var vm = new AsyncSubViewModel();
            await _navigationService.ShowModalWindowAsync("MainView", "SubView", vm);
        }

        private async Task ModalDelayedAsyncLoaderAsync()
        {
            var vm = new DelayedAsyncSubViewModel();
            await _navigationService.ShowModalWindowAsync("MainView", "SubView", vm);
        }

        private async Task ModalCodeCanceledFolderAsync()
        {
            var vm = new CodeCanceledSubViewModel();
            await _navigationService.ShowModalWindowAsync("MainView", "SubView", vm);
        }
    }
}