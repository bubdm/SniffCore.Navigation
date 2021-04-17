using System.Windows;

namespace SniffCore.Windows
{
    public interface IWindowProvider
    {
        Window GetNewWindow(object windowKey);
        Window GetOpenWindow(object windowKey);
    }
}