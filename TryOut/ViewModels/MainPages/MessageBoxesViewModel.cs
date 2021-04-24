using System.Windows;
using SniffCore;
using SniffCore.Navigation;

namespace TryOut.ViewModels.MainPages
{
    public class MessageBoxesViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public MessageBoxesViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowOKCommand = new DelegateCommand(ShowOK);
            ShowYesNoCommand = new DelegateCommand(ShowYesNo);
        }

        public IDelegateCommand ShowOKCommand { get; }
        public IDelegateCommand ShowYesNoCommand { get; }

        private void ShowOK()
        {
            _navigationService.ShowMessageBox("MainView", "Message", "Caption");
        }

        private void ShowYesNo()
        {
            _navigationService.ShowMessageBox("MainView", "Message", "Caption", MessageBoxButton.YesNo);
        }
    }
}