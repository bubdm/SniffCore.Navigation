using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SniffCore.Windows
{
    public class WindowBehavior : DependencyObject
    {
        public static readonly DependencyProperty ClosingCommandProperty =
            DependencyProperty.RegisterAttached("ClosingCommand", typeof(ICommand), typeof(WindowBehavior), new UIPropertyMetadata(OnClosingCommandChanged));

        public static readonly DependencyProperty LoadedCommandProperty =
            DependencyProperty.RegisterAttached("LoadedCommand", typeof(ICommand), typeof(WindowBehavior), new UIPropertyMetadata(OnLoadedCommandChanged));

        public static ICommand GetClosingCommand(DependencyObject obj)
        {
            return (ICommand) obj.GetValue(ClosingCommandProperty);
        }

        public static void SetClosingCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ClosingCommandProperty, value);
        }

        private static void OnClosingCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Window window))
                throw new InvalidOperationException("'WindowBehavior.ClosingCommand' only can be attached to a 'Window' object");

            window.Closing += Window_Closing;
        }

        private static void Window_Closing(object sender, CancelEventArgs e)
        {
            var command = GetClosingCommand((DependencyObject) sender);
            if (command != null && command.CanExecute(null))
            {
                var args = new WindowClosingArgs();
                command.Execute(args);
                e.Cancel = args.Cancel;
            }
        }

        public static ICommand GetLoadedCommand(DependencyObject obj)
        {
            return (ICommand) obj.GetValue(LoadedCommandProperty);
        }

        public static void SetLoadedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(LoadedCommandProperty, value);
        }

        private static void OnLoadedCommandChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var window = sender as FrameworkElement;
            if (window == null)
                throw new InvalidOperationException("'WindowBehavior.LoadedCommand' only can be attached to a 'FrameworkElement' object");

            window.Loaded += Window_Loaded;
        }

        private static void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var command = GetLoadedCommand((DependencyObject) sender);
            if (command != null && command.CanExecute(null))
                command.Execute(null);
        }
    }
}