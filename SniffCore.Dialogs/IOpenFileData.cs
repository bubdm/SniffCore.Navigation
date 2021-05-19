//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

namespace SniffCore.Dialogs
{
    public interface IOpenFileData
    {
        bool CheckFileExists { get; set; }
        bool CheckPathExists { get; set; }
        string DefaultExt { get; set; }
        string FileName { get; set; }
        string[] FileNames { get; set; }
        string SafeFileName { get; set; }
        string[] SafeFileNames { get; set; }
        string Filter { get; set; }
        int FilterIndex { get; set; }
        string InitialDirectory { get; set; }
        bool MultiSelect { get; set; }
        string Title { get; set; }
        bool ValidateNames { get; set; }
    }
}