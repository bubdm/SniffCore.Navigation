//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;

namespace SniffCore.Dialogs
{
    /// <summary>
    ///     Provides the data to be used for the folder browser dialog.
    /// </summary>
    public interface IBrowseFolderData
    {
        /// <summary>
        ///     Gets or sets a description to show above the folders. Here you can provide instructions for selecting a folder.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        ///     Gets/sets the root node of the directory tree.
        /// </summary>
        Environment.SpecialFolder? RootFolder { get; set; }

        /// <summary>
        ///     Gets the directory path of the folder the user picked.
        ///     Sets the directory path of the initial folder shown in the dialog box.
        /// </summary>
        string SelectedPath { get; set; }

        /// <summary>
        ///     Determines if the 'New Folder' button should be exposed.
        ///     This property has no effect if the Vista style dialog is used; in that case, the New Folder button is always shown.
        /// </summary>
        bool ShowNewFolderButton { get; set; }

        /// <summary>
        ///     Gets or sets additional dialog data to be use for customization.
        /// </summary>
        object Data { get; set; }
    }
}