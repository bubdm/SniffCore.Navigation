//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.ComponentModel;

namespace SniffCore.Dialogs
{
    /// <summary>
    ///     Provides the data to be used for the save file dialog.
    /// </summary>
    public class SaveFileData : ISaveFileData
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveFileData" />.
        /// </summary>
        public SaveFileData()
        {
            CheckFileExists = false;
            CheckPathExists = true;
            CreatePrompt = false;
            DefaultExt = string.Empty;
            FileName = string.Empty;
            Filter = string.Empty;
            FilterIndex = 1;
            InitialDirectory = string.Empty;
            OverwritePrompt = true;
            Title = string.Empty;
            ValidateNames = true;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a file name that
        ///     does not exist.
        /// </summary>
        /// <remarks>Default: false</remarks>
        [DefaultValue(false)]
        public bool CheckFileExists { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a path that does
        ///     not exist.
        /// </summary>
        /// <remarks>Default: true</remarks>
        [DefaultValue(true)]
        public bool CheckPathExists { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box prompts the user for permission to create a file if the user
        ///     specifies a file that does not exist.
        /// </summary>
        /// <remarks>Default: false</remarks>
        [DefaultValue(false)]
        public bool CreatePrompt { get; set; }

        /// <summary>
        ///     Gets or sets the default file extension.
        /// </summary>
        /// <remarks>Default: ""</remarks>
        [DefaultValue("")]
        public string DefaultExt { get; set; }

        /// <summary>
        ///     Gets or sets a string containing the file name selected in the file dialog box.
        /// </summary>
        /// <remarks>Default: ""</remarks>
        [DefaultValue("")]
        public string FileName { get; set; }

        /// <summary>
        ///     Gets the file names of all selected files in the dialog box.
        /// </summary>
        public string[] FileNames { get; set; }

        /// <summary>
        ///     Gets or sets the current file name filter string, which determines the choices that appear in the "Save as file
        ///     type" or "Files of type" box in the dialog box.
        /// </summary>
        /// <remarks>Default: ""</remarks>
        [DefaultValue("")]
        public string Filter { get; set; }

        /// <summary>
        ///     Gets or sets the index of the filter currently selected in the file dialog box.
        /// </summary>
        /// <remarks>Default: 1</remarks>
        [DefaultValue(1)]
        public int FilterIndex { get; set; }

        /// <summary>
        ///     Gets or sets the initial directory displayed by the file dialog box.
        /// </summary>
        /// <remarks>Default: ""</remarks>
        [DefaultValue("")]
        public string InitialDirectory { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the Save As dialog box displays a warning if the user specifies a file name
        ///     that already exists.
        /// </summary>
        /// <remarks>Default: true</remarks>
        [DefaultValue(true)]
        public bool OverwritePrompt { get; set; }

        /// <summary>
        ///     Gets or sets the file dialog box title.
        /// </summary>
        /// <remarks>Default: ""</remarks>
        [DefaultValue("")]
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box accepts only valid Win32 file names.
        /// </summary>
        /// <remarks>Default: true</remarks>
        [DefaultValue(true)]
        public bool ValidateNames { get; set; }
    }
}