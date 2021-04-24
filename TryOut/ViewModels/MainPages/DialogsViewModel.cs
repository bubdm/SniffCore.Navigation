﻿using SniffCore;
using SniffCore.Dialogs;
using SniffCore.Navigation;

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
        }

        public IDelegateCommand SaveFileCommand { get; }
        public IDelegateCommand OpenFileCommand { get; }
        public IDelegateCommand BrowseFolderCommand { get; }

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
    }
}