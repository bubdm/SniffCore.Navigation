//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Windows.Forms;

namespace SniffCore.Dialogs
{
    public class DialogProvider : IDialogProvider
    {
        public bool Show(IOpenFileData openFileData)
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = openFileData.CheckFileExists,
                CheckPathExists = openFileData.CheckPathExists,
                DefaultExt = openFileData.DefaultExt,
                FileName = openFileData.FileName,
                Filter = openFileData.Filter,
                FilterIndex = openFileData.FilterIndex,
                InitialDirectory = openFileData.InitialDirectory,
                Multiselect = openFileData.MultiSelect,
                Title = openFileData.Title,
                ValidateNames = openFileData.ValidateNames
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                openFileData.FileName = dialog.FileName;
                openFileData.FileNames = dialog.FileNames;
                openFileData.SafeFileName = dialog.SafeFileName;
                openFileData.SafeFileNames = dialog.SafeFileNames;
                return true;
            }

            return false;
        }

        public bool Show(ISaveFileData saveFileData)
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = saveFileData.CheckFileExists,
                CheckPathExists = saveFileData.CheckPathExists,
                CreatePrompt = saveFileData.CreatePrompt,
                DefaultExt = saveFileData.DefaultExt,
                FileName = saveFileData.FileName,
                Filter = saveFileData.Filter,
                FilterIndex = saveFileData.FilterIndex,
                InitialDirectory = saveFileData.InitialDirectory,
                OverwritePrompt = saveFileData.OverwritePrompt,
                Title = saveFileData.Title,
                ValidateNames = saveFileData.ValidateNames
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                saveFileData.FileName = dialog.FileName;
                saveFileData.FileNames = dialog.FileNames;
                return true;
            }

            return false;
        }

        public bool Show(IBrowseFolderData browseFolderData)
        {
            var dialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = browseFolderData.ShowNewFolderButton
            };
            if (browseFolderData.RootFolder != null)
                dialog.RootFolder = browseFolderData.RootFolder.Value;
            if (!string.IsNullOrWhiteSpace(browseFolderData.Description))
                dialog.Description = browseFolderData.Description;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                browseFolderData.SelectedPath = dialog.SelectedPath;
                return true;
            }

            return false;
        }
    }
}