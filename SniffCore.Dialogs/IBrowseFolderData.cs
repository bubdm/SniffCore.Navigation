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