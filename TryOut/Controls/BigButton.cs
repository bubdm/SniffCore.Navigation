using System.Windows;
using System.Windows.Controls;

namespace TryOut.Controls
{
    public class BigButton : Button
    {
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(BigButton), new PropertyMetadata(string.Empty));

        static BigButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BigButton), new FrameworkPropertyMetadata(typeof(BigButton)));
        }

        public string Label
        {
            get => (string) GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }
    }
}