using System.Windows;

namespace SniffCore.MessageBoxes
{
    public interface IMessageBoxOptions
    {
        MessageBoxImage? Icon { get; set; }
        MessageBoxResult? DefaultResult { get; set; }
    }
}