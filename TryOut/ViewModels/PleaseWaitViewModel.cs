using System;
using SniffCore;

namespace TryOut.ViewModels
{
    public class PleaseWaitViewModel : ObservableObject
    {
        private double _value;

        public PleaseWaitViewModel()
        {
            CancelCommand = new DelegateCommand(OnCancel);
        }

        public IDelegateCommand CancelCommand { get; }

        public double Value
        {
            get => _value;
            set => NotifyAndSetIfChanged(ref _value, value);
        }

        private void OnCancel()
        {
            Cancel?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Cancel;
    }
}