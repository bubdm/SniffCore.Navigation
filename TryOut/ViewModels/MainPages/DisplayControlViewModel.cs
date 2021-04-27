using SniffCore;
using TryOut.ViewModels.MainPages.DisplayControl;

namespace TryOut.ViewModels.MainPages
{
    public class DisplayControlViewModel : ObservableObject
    {
        private ObservableObject _currentViewModel;

        public DisplayControlViewModel()
        {
            ShowAsyncViewModelCommand = new DelegateCommand(ShowAsyncViewModel);
            ShowDelayedAsyncViewModelCommand = new DelegateCommand(ShowDelayedAsyncViewModel);
        }

        public IDelegateCommand ShowAsyncViewModelCommand { get; }
        public IDelegateCommand ShowDelayedAsyncViewModelCommand { get; }

        public ObservableObject CurrentViewModel
        {
            get => _currentViewModel;
            set => NotifyAndSetIfChanged(ref _currentViewModel, value);
        }

        private void ShowAsyncViewModel()
        {
            CurrentViewModel = new DisplayAsyncViewModel();
        }

        private void ShowDelayedAsyncViewModel()
        {
            CurrentViewModel = new DisplayDelayedAsyncViewModel();
        }
    }
}