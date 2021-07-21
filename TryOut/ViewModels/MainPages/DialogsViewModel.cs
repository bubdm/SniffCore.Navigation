using SniffCore;
using SniffCore.Navigation;
using SniffCore.Navigation.Dialogs;

namespace TryOut.ViewModels.MainPages
{
    public class DialogsViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public DialogsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            SaveFileCommand = new DelegateCommand(SaveFile);
            OpenFileCommand = new DelegateCommand(OpenFile);
            BrowseFolderCommand = new DelegateCommand(BrowseFolder);
            ColorPickerCommand = new DelegateCommand(ColorPicker);
            FontPickerCommand = new DelegateCommand(FontPicker);
        }

        public IDelegateCommand SaveFileCommand { get; }
        public IDelegateCommand OpenFileCommand { get; }
        public IDelegateCommand BrowseFolderCommand { get; }
        public IDelegateCommand ColorPickerCommand { get; }
        public IDelegateCommand FontPickerCommand { get; }

        private void SaveFile()
        {
            var data = new SaveFileData();
            _navigationService.ShowDialog(data);
        }

        private void OpenFile()
        {
            var data = new OpenFileData();
            _navigationService.ShowDialog(data);
        }

        private void BrowseFolder()
        {
            var data = new BrowseFolderData();
            _navigationService.ShowDialog(data);
        }

        private void ColorPicker()
        {
            var data = new ColorPickerData();
            _navigationService.ShowDialog(data);
        }

        private void FontPicker()
        {
            var data = new FontPickerData();
            _navigationService.ShowDialog(data);
        }
    }
}