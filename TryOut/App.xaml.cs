using System.Windows;
using SniffCore.Navigation;
using SniffCore.Navigation.Dialogs;
using SniffCore.Navigation.MessageBoxes;
using SniffCore.Navigation.PleaseWaits;
using SniffCore.Navigation.Windows;
using TryOut.ViewModels;
using TryOut.Views;
using TryOut.Views.MainPages;
using TryOut.Views.MainPages.NavigationPresenter;
using TryOut.Views.MainPages.Windows;
using Unity;

namespace TryOut
{
    public partial class App
    {
        private IUnityContainer _unityContainer;

        protected override void OnStartup(StartupEventArgs e)
        {
            _unityContainer = new UnityContainer();

            _unityContainer.RegisterSingleton<IWindowProvider, WindowProvider>();
            _unityContainer.RegisterType<IDialogProvider, DialogProvider>();
            _unityContainer.RegisterType<IMessageBoxProvider, MessageBoxProvider>();
            _unityContainer.RegisterType<IPleaseWaitProvider, PleaseWaitProvider>();
            _unityContainer.RegisterType<INavigationService, NavigationService>();

            var windowProvider = (WindowProvider) _unityContainer.Resolve<IWindowProvider>();
            var navigationService = _unityContainer.Resolve<INavigationService>();

            windowProvider.RegisterWindow<MainView>("MainView");
            windowProvider.RegisterWindow<SubView>("SubView");

            windowProvider.RegisterControl<DialogsView>("DialogsView");
            windowProvider.RegisterControl<DisplayControlView>("DisplayControlView");
            windowProvider.RegisterControl<InputValidationsView>("InputValidationsView");
            windowProvider.RegisterControl<LockedView>("LockedView");
            windowProvider.RegisterControl<MessageBoxesView>("MessageBoxesView");
            windowProvider.RegisterControl<NavigationPresenterView>("NavigationPresenterView");
            windowProvider.RegisterControl<PendingChangesView>("PendingChangesView");
            windowProvider.RegisterControl<WindowsView>("WindowsView");
            windowProvider.RegisterControl<NavigationAsyncView>("NavigationAsyncView");
            windowProvider.RegisterControl<NavigationDelayedAsyncView>("NavigationDelayedAsyncView");

            base.OnStartup(e);

            var vm = _unityContainer.Resolve<MainViewModel>();
            navigationService.ShowWindowAsync("MainView", vm);
        }
    }
}