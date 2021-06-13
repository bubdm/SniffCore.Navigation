//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

namespace SniffCore.Navigation.Dialogs
{
    /// <summary>
    ///     Provides the data to be used for the save file dialog.
    /// </summary>
    public interface ISaveFileData
    {
        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a file name that
        ///     does not exist.
        /// </summary>
        bool CheckFileExists { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a path that does
        ///     not exist.
        /// </summary>
        bool CheckPathExists { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box prompts the user for permission to create a file if the user
        ///     specifies a file that does not exist.
        /// </summary>
        bool CreatePrompt { get; set; }

        /// <summary>
        ///     Gets or sets the default file extension.
        /// </summary>
        string DefaultExt { get; set; }

        /// <summary>
        ///     Gets or sets a string containing the file name selected in the file dialog box.
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        ///     Gets the file names of all selected files in the dialog box.
        /// </summary>
        string[] FileNames { get; set; }

        /// <summary>
        ///     Gets or sets the current file name filter string, which determines the choices that appear in the "Save as file
        ///     type" or "Files of type" box in the dialog box.
        /// </summary>
        string Filter { get; set; }

        /// <summary>
        ///     Gets or sets the index of the filter currently selected in the file dialog box.
        /// </summary>
        int FilterIndex { get; set; }

        /// <summary>
        ///     Gets or sets the initial directory displayed by the file dialog box.
        /// </summary>
        string InitialDirectory { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the Save As dialog box displays a warning if the user specifies a file name
        ///     that already exists.
        /// </summary>
        bool OverwritePrompt { get; set; }

        /// <summary>
        ///     Gets or sets the file dialog box title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box accepts only valid Win32 file names.
        /// </summary>
        bool ValidateNames { get; set; }
    }
}