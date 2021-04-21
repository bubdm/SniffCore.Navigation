using System.Windows;
using System.Windows.Controls;

namespace SniffCore.Windows
{
    public interface IWindowProvider
    {
        Window GetNewWindow(object windowKey);
        Window GetOpenWindow(object windowKey);

        UserControl GetNewControl(object controlKey);
    }
}