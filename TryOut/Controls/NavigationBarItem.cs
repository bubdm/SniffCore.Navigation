using System.Windows;
using System.Windows.Controls;

namespace TryOut.Controls
{
    public class NavigationBarItem : RadioButton
    {
        static NavigationBarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationBarItem), new FrameworkPropertyMetadata(typeof(NavigationBarItem)));
        }
    }
}