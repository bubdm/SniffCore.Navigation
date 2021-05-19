//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;

namespace SniffCore.Dialogs
{
    public interface IBrowseFolderData
    {
        string Description { get; set; }
        Environment.SpecialFolder? RootFolder { get; set; }
        string SelectedPath { get; set; }
        bool ShowNewFolderButton { get; set; }
        object Data { get; set; }
    }
}