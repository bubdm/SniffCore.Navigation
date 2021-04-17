using System.Windows;
using System.Windows.Controls;

namespace SniffCore.Navigation
{
    public class DisplayControl : Control
    {
        internal static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(object), typeof(DisplayControl), new PropertyMetadata(null));

        internal static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(DisplayControl), new PropertyMetadata(null));

        public static readonly DependencyProperty IsCachedProperty =
            DependencyProperty.Register("IsCached", typeof(bool), typeof(DisplayControl), new PropertyMetadata(false));

        public static readonly DependencyProperty DisposeViewModelsProperty =
            DependencyProperty.Register("DisposeViewModels", typeof(bool), typeof(DisplayControl), new PropertyMetadata(false));

        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(object), typeof(DisplayControl), new PropertyMetadata(OnIDChanged));

        static DisplayControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DisplayControl), new FrameworkPropertyMetadata(typeof(DisplayControl)));
        }

        internal object ViewModel
        {
            get => GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        internal object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public bool IsCached
        {
            get => (bool) GetValue(IsCachedProperty);
            set => SetValue(IsCachedProperty, value);
        }

        // Dispose if not cached and replaced
        public bool DisposeViewModels
        {
            get => (bool) GetValue(DisposeViewModelsProperty);
            set => SetValue(DisposeViewModelsProperty, value);
        }

        public object ID
        {
            get => GetValue(IDProperty);
            set => SetValue(IDProperty, value);
        }

        private static void OnIDChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (DisplayControl) d;
            if (e.OldValue != null)
                NavigationService.UnregisterDisplayControl(e.OldValue);

            if (e.NewValue != null)
                NavigationService.RegisterDisplayControl(e.NewValue, control);
        }

        // Please Wait
        // Switch Animation
    }
}