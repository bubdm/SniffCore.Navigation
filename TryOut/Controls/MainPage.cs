using System.Windows;
using System.Windows.Controls;

namespace TryOut.Controls
{
    public class MainPage : ContentControl
    {
        static MainPage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MainPage), new FrameworkPropertyMetadata(typeof(MainPage)));
        }
    }
}