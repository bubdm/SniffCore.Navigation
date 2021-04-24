using System;

namespace SniffCore
{
    public sealed class DelegateCommand : IDelegateCommand
    {
        private readonly Func<bool> _canExecuteCallback;
        private readonly Action _executeCallback;

        public DelegateCommand(Action executeCallback)
            : this(() => true, executeCallback)
        {
        }

        public DelegateCommand(Func<bool> canExecuteCallback, Action executeCallback)
        {
            _canExecuteCallback = canExecuteCallback ?? throw new ArgumentNullException(nameof(canExecuteCallback));
            _executeCallback = executeCallback ?? throw new ArgumentNullException(nameof(executeCallback));
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteCallback();
        }

        public void Execute(object parameter)
        {
            _executeCallback();
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public sealed class DelegateCommand<T> : IDelegateCommand
    {
        private readonly Func<T, bool> _canExecuteCallback;
        private readonly Action<T> _executeCallback;

        public DelegateCommand(Action<T> executeCallback)
            : this(o => true, executeCallback)
        {
        }

        public DelegateCommand(Func<T, bool> canExecuteCallback, Action<T> executeCallback)
        {
            _canExecuteCallback = canExecuteCallback ?? throw new ArgumentNullException(nameof(canExecuteCallback));
            _executeCallback = executeCallback ?? throw new ArgumentNullException(nameof(executeCallback));
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteCallback((T) parameter);
        }

        public void Execute(object parameter)
        {
            _executeCallback((T) parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}