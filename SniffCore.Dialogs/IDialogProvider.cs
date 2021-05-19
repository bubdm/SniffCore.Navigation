//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

namespace SniffCore.Dialogs
{
    public interface IDialogProvider
    {
        bool Show(IOpenFileData openFileData);
        bool Show(ISaveFileData saveFileData);
        bool Show(IBrowseFolderData browseFolderData);
    }
}