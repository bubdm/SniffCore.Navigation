namespace SniffCore.Dialogs
{
    public interface IDialogProvider
    {
        bool Show(IOpenFileData openFileData);
        bool Show(ISaveFileData saveFileData);
        bool Show(IBrowseFolderData browseFolderData);
    }
}