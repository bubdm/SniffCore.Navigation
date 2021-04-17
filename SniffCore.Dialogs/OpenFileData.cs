namespace SniffCore.Dialogs
{
    public class OpenFileData : IOpenFileData
    {
        public bool CheckFileExists { get; set; }
        public bool CheckPathExists { get; set; }
        public string DefaultExt { get; set; }
        public string FileName { get; set; }
        public string[] FileNames { get; set; }
        public string SafeFileName { get; set; }
        public string[] SafeFileNames { get; set; }
        public string Filter { get; set; }
        public int FilterIndex { get; set; }
        public string InitialDirectory { get; set; }
        public bool MultiSelect { get; set; }
        public string Title { get; set; }
        public bool ValidateNames { get; set; }
    }
}