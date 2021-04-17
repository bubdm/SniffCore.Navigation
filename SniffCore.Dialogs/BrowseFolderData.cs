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