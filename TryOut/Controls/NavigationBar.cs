using System.Windows;
using System.Windows.Controls;

namespace TryOut.Controls
{
    public class NavigationBar : ItemsControl
    {
        static NavigationBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationBar), new FrameworkPropertyMetadata(typeof(NavigationBar)));
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is NavigationBarItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NavigationBarItem();
        }
    }
}