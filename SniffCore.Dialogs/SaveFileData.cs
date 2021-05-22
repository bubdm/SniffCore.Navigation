//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.ComponentModel;

namespace SniffCore.Dialogs
{
    public class SaveFileData : ISaveFileData
    {
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

        public bool CheckFileExists { get; set; }
        public bool CheckPathExists { get; set; }
        public bool CreatePrompt { get; set; }
        public string DefaultExt { get; set; }
        public string FileName { get; set; }
        public string[] FileNames { get; set; }
        public string Filter { get; set; }
        public int FilterIndex { get; set; }
        public string InitialDirectory { get; set; }
        public bool OverwritePrompt { get; set; }
        public string Title { get; set; }
        public bool ValidateNames { get; set; }
    }
}