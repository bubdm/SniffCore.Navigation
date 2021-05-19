//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;

namespace SniffCore.Dialogs
{
    public class BrowseFolderData : IBrowseFolderData
    {
        public string Description { get; set; }
        public Environment.SpecialFolder? RootFolder { get; set; }
        public string SelectedPath { get; set; }
        public bool ShowNewFolderButton { get; set; }
        public object Data { get; set; }
    }
}