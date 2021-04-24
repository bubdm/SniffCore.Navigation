using System.Windows.Input;

namespace SniffCore
{
    public interface IDelegateCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}