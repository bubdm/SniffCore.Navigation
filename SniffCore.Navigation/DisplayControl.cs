using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SniffCore.Navigation.External;
using SniffCore.PleaseWaits;

namespace SniffCore.Navigation
{
    public class DisplayControl : Control
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(object), typeof(DisplayControl), new PropertyMetadata(OnViewModelChanged));

        internal static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(DisplayControl), new PropertyMetadata(OnContentChanged));

        public static readonly DependencyProperty DisposeViewModelProperty =
            DependencyProperty.Register("DisposeViewModel", typeof(bool), typeof(DisplayControl), new PropertyMetadata(false));

        public static readonly DependencyProperty PleaseWaitDataTemplateProperty =
            DependencyProperty.Register("PleaseWaitDataTemplate", typeof(DataTemplate), typeof(DisplayControl), new PropertyMetadata(null));

        internal static readonly DependencyProperty PleaseWaitProgressProperty =
            DependencyProperty.Register("PleaseWaitProgress", typeof(LoadingProgress), typeof(DisplayControl), new PropertyMetadata(null));

        static DisplayControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DisplayControl), new FrameworkPropertyMetadata(typeof(DisplayControl)));
        }

        public object ViewModel
        {
            get => GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        internal object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public bool DisposeViewModel
        {
            get => (bool) GetValue(DisposeViewModelProperty);
            set => SetValue(DisposeViewModelProperty, value);
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

        private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (DisplayControl) d;
            if (e.NewValue is IAsyncLoader asyncLoader)
                control.DisplayAndLoadAsync(asyncLoader).FireAndForget();
            else if (e.NewValue is IDelayedAsyncLoader delayedAsyncLoader)
                control.LoadAndDisplayAsync(delayedAsyncLoader).FireAndForget();
            else
                control.Content = e.NewValue;
        }

        private async Task DisplayAndLoadAsync(IAsyncLoader asyncLoader)
        {
            await asyncLoader.LoadAsync();
            Content = asyncLoader;
        }

        private async Task LoadAndDisplayAsync(IDelayedAsyncLoader delayedAsyncLoader)
        {
            var progress = new LoadingProgress();
            PleaseWaitProgress = progress;
            Content = null;
            await delayedAsyncLoader.LoadAsync(progress);
            Content = delayedAsyncLoader;
            PleaseWaitProgress = null;
        }

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (DisplayControl) d;
            if (control.DisposeViewModel && e.OldValue is IDisposable disposable)
                disposable.Dispose();
        }
    }
}