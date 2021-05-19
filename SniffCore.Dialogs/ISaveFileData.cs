//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

namespace SniffCore.Dialogs
{
    public interface ISaveFileData
    {
        bool CheckFileExists { get; set; }
        bool CheckPathExists { get; set; }
        bool CreatePrompt { get; set; }
        string DefaultExt { get; set; }
        string FileName { get; set; }
        string[] FileNames { get; set; }
        string Filter { get; set; }
        int FilterIndex { get; set; }
        string InitialDirectory { get; set; }
        bool OverwritePrompt { get; set; }
        string Title { get; set; }
        bool ValidateNames { get; set; }
    }
}