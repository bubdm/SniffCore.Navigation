using System;
using System.Windows;
using SniffCore.PleaseWaits;
using TryOut.ViewModels;
using TryOut.Views;

namespace TryOut
{
    public class PleaseWaitProvider : IPleaseWaitProvider
    {
        private PleaseWaitViewModel _viewModel;
        private PleaseWaitView _window;

        public void Show()
        {
            _viewModel = new PleaseWaitViewModel();
            _viewModel.Cancel += UserCanceled;
            _window = new PleaseWaitView {DataContext = _viewModel};
            _window.Show();
        }

        public void Close()
        {
            _window.Close();
            _window = null;
            _viewModel.Cancel -= UserCanceled;
            _viewModel = null;
        }

        public void HandleProgress(ProgressData progressData)
        {
            _viewModel.Value = progressData.Progress;
        }

        public void HandleCanceled(LoadingCanceled canceledData)
        {
            MessageBox.Show(canceledData.Reason);
        }

        private void UserCanceled(object sender, EventArgs e)
        {
        }
    }
}