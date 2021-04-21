using System;
using System.Windows;
using System.Windows.Controls;
using SniffCore.PleaseWaits;

namespace SniffCore.Navigation
{
    public class NavigationPresenter : Control
    {
        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(object), typeof(NavigationPresenter), new PropertyMetadata(OnIDChanged));

        internal static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(NavigationPresenter), new PropertyMetadata(OnContentChanged));

        public static readonly DependencyProperty DisposeViewModelProperty =
            DependencyProperty.Register("DisposeViewModel", typeof(bool), typeof(NavigationPresenter), new PropertyMetadata(false));

        public static readonly DependencyProperty PleaseWaitDataTemplateProperty =
            DependencyProperty.Register("PleaseWaitDataTemplate", typeof(DataTemplate), typeof(NavigationPresenter), new PropertyMetadata(null));

        internal static readonly DependencyProperty PleaseWaitProgressProperty =
            DependencyProperty.Register("PleaseWaitProgress", typeof(LoadingProgress), typeof(NavigationPresenter), new PropertyMetadata(OnPleaseWaitProgressChanged));

        public static readonly DependencyProperty ProgressDataProperty =
            DependencyProperty.Register("ProgressData", typeof(ProgressData), typeof(NavigationPresenter), new PropertyMetadata(null));

        static NavigationPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationPresenter), new FrameworkPropertyMetadata(typeof(NavigationPresenter)));
        }

        public bool DisposeViewModel
        {
            get => (bool) GetValue(DisposeViewModelProperty);
            set => SetValue(DisposeViewModelProperty, value);
        }

        internal object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public object ID
        {
            get => GetValue(IDProperty);
            set => SetValue(IDProperty, value);
        }

        public DataTemplate PleaseWaitDataTemplate
        {
            get => (DataTemplate) GetValue(PleaseWaitDataTemplateProperty);
            set => SetValue(PleaseWaitDataTemplateProperty, value);
        }

        internal LoadingProgress PleaseWaitProgress
        {
            get => (LoadingProgress) GetValue(PleaseWaitProgressProperty);
            set => SetValue(PleaseWaitProgressProperty, value);
        }

        public ProgressData ProgressData
        {
            get => (ProgressData) GetValue(ProgressDataProperty);
            set => SetValue(ProgressDataProperty, value);
        }

        private static void OnPleaseWaitProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (NavigationPresenter) d;
            if (e.OldValue != null)
                ((LoadingProgress) e.OldValue).ProgressUpdated -= control.ProgressUpdated;
            if (e.NewValue != null)
                ((LoadingProgress) e.NewValue).ProgressUpdated += control.ProgressUpdated;
        }

        private void ProgressUpdated(object sender, ProgressDataEventArgs e)
        {
            ProgressData = e.Data;
        }

        private static void OnIDChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (NavigationPresenter) d;
            if (e.OldValue != null)
                NavigationService.UnregisterPresenter(e.OldValue);
            if (e.NewValue != null)
                NavigationService.RegisterPresenter(e.NewValue, control);
        }

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (NavigationPresenter) d;
            if (control.DisposeViewModel && e.OldValue is FrameworkElement {DataContext: IDisposable disposable})
                disposable.Dispose();
        }
    }
}