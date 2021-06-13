//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;
using System.ComponentModel;

namespace SniffCore.Navigation.Dialogs
{
    /// <summary>
    ///     Provides the data to be used for the folder browser dialog.
    /// </summary>
    public class BrowseFolderData : IBrowseFolderData
    {
        /// <summary>
        ///     Creates a new instance of <see cref="BrowseFolderData" />.
        /// </summary>
        public BrowseFolderData()
        {
            Description = string.Empty;
            RootFolder = Environment.SpecialFolder.Desktop;
            SelectedPath = string.Empty;
            ShowNewFolderButton = true;
            Data = null;
        }

        /// <summary>
        ///     Gets or sets a description to show above the folders. Here you can provide instructions for selecting a folder.
        /// </summary>
        /// <remarks>Default: ""</remarks>
        [DefaultValue("")]
        public string Description { get; set; }

        /// <summary>
        ///     Gets/sets the root node of the directory tree.
        /// </summary>
        /// <remarks>Default: Environment.SpecialFolder.Desktop</remarks>
        [DefaultValue(Environment.SpecialFolder.Desktop)]
        public Environment.SpecialFolder? RootFolder { get; set; }

        /// <summary>
        ///     Gets the directory path of the folder the user picked.
        ///     Sets the directory path of the initial folder shown in the dialog box.
        /// </summary>
        /// <remarks>Default: ""</remarks>
        [DefaultValue("")]
        public string SelectedPath { get; set; }

        /// <summary>
        ///     Determines if the 'New Folder' button should be exposed.
        ///     This property has no effect if the Vista style dialog is used; in that case, the New Folder button is always shown.
        /// </summary>
        /// <remarks>Default: true</remarks>
        [DefaultValue(true)]
        public bool ShowNewFolderButton { get; set; }

        /// <summary>
        ///     Gets or sets additional dialog data to be use for customization.
        /// </summary>
        /// <remarks>Default: null</remarks>
        [DefaultValue(null)]
        public object Data { get; set; }
    }
}