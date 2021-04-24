using System;
using System.Threading.Tasks;

namespace SniffCore
{
    public sealed class AsyncDelegateCommand : IDelegateCommand
    {
        private readonly Func<bool> _canExecuteCallback;
        private readonly Func<Task> _executeCallback;
        private bool _isBusy;

        public AsyncDelegateCommand(Func<Task> executeCallback)
            : this(() => true, executeCallback)
        {
        }

        public AsyncDelegateCommand(Func<bool> canExecuteCallback, Func<Task> executeCallback)
        {
            _canExecuteCallback = canExecuteCallback ?? throw new ArgumentNullException(nameof(canExecuteCallback));
            _executeCallback = executeCallback ?? throw new ArgumentNullException(nameof(executeCallback));
        }

        public bool CanExecute(object parameter)
        {
            return !_isBusy && _canExecuteCallback();
        }

        public void Execute(object parameter)
        {
            ExecuteAsync();
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private async void ExecuteAsync()
        {
            _isBusy = true;
            RaiseCanExecuteChanged();
            await _executeCallback();
            _isBusy = false;
            RaiseCanExecuteChanged();
        }
    }

    public sealed class AsyncDelegateCommand<T> : IDelegateCommand
    {
        private readonly Func<T, bool> _canExecuteCallback;
        private readonly Func<T, Task> _executeCallback;
        private bool _isBusy;

        public AsyncDelegateCommand(Func<T, Task> executeCallback)
            : this(o => true, executeCallback)
        {
        }

        public AsyncDelegateCommand(Func<T, bool> canExecuteCallback, Func<T, Task> executeCallback)
        {
            _canExecuteCallback = canExecuteCallback ?? throw new ArgumentNullException(nameof(canExecuteCallback));
            _executeCallback = executeCallback ?? throw new ArgumentNullException(nameof(executeCallback));
        }

        public bool CanExecute(object parameter)
        {
            return !_isBusy && _canExecuteCallback((T) parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteAsync(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private async void ExecuteAsync(object parameter)
        {
            _isBusy = true;
            RaiseCanExecuteChanged();
            await _executeCallback((T) parameter);
            _isBusy = false;
            RaiseCanExecuteChanged();
        }
    }
}