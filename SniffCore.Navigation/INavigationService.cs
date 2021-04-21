using System.Threading.Tasks;
using System.Windows;
using SniffCore.Dialogs;
using SniffCore.MessageBoxes;

namespace SniffCore.Navigation
{
    public interface INavigationService
    {
        Task ShowWindowAsync(object windowKey, object viewModel);
        Task ShowWindowAsync(object ownerWindowKey, object windowKey, object viewModel);
        Task<bool?> ShowModalWindowAsync(object windowKey, object viewModel);
        Task<bool?> ShowModalWindowAsync(object ownerWindowKey, object windowKey, object viewModel);

        void SetDialogResult(object windowKey, bool? dialogResult);
        void Close(object windowKey);

        Task ShowControlAsync(object hostId, object controlKey, object viewModel);

        MessageBoxResult ShowMessageBox(string messageBoxText);
        MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText);
        MessageBoxResult ShowMessageBox(string messageBoxText, string caption);
        MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption);
        MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button);
        MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption, MessageBoxButton button);
        MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options);
        MessageBoxResult ShowMessageBox(object ownerWindowKey, string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options);

        bool ShowDialog(IOpenFileData openFileData);
        bool ShowDialog(ISaveFileData saveFileData);
        bool ShowDialog(IBrowseFolderData browseFolderData);
    }
}