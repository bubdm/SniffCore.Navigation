//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SniffCore.Navigation.External;
using SniffCore.PleaseWaits;

// ReSharper disable MemberCanBePrivate.Global

namespace SniffCore.Navigation
{
    /// <summary>
    ///     Provides a way to display a ViewModel where the View is found by the Resources but allow loading of IAsyncLoader
    ///     and Please Wait and more.
    /// </summary>
    public class DisplayControl : Control
    {
        /// <summary>
        ///     The DependencyProperty for the ViewModel property.
        /// </summary>
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(object), typeof(DisplayControl), new PropertyMetadata(OnViewModelChanged));

        internal static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(DisplayControl), new PropertyMetadata(OnContentChanged));

        /// <summary>
        ///     The DependencyProperty for the DisposeViewModel property.
        /// </summary>
        public static readonly DependencyProperty DisposeViewModelProperty =
            DependencyProperty.Register("DisposeViewModel", typeof(bool), typeof(DisplayControl), new PropertyMetadata(false));

        /// <summary>
        ///     The DependencyProperty for the PleaseWaitDataTemplate property.
        /// </summary>
        public static readonly DependencyProperty PleaseWaitDataTemplateProperty =
            DependencyProperty.Register("PleaseWaitDataTemplate", typeof(DataTemplate), typeof(DisplayControl), new PropertyMetadata(null));

        internal static readonly DependencyProperty PleaseWaitProgressProperty =
            DependencyProperty.Register("PleaseWaitProgress", typeof(LoadingProgress), typeof(DisplayControl), new PropertyMetadata(null));

        static DisplayControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DisplayControl), new FrameworkPropertyMetadata(typeof(DisplayControl)));
        }

        /// <summary>
        ///     The ViewModel to display which view will be determined by the resources.
        /// </summary>
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

        /// <summary>
        ///     Defines if the viewmodel shall be disposed if they implement the IDisposable and the ViewModel changed.
        /// </summary>
        public bool DisposeViewModel
        {
            get => (bool) GetValue(DisposeViewModelProperty);
            set => SetValue(DisposeViewModelProperty, value);
        }

        /// <summary>
        ///     Defines the please wait data template which gets the <see cref="ProgressData" /> as a DataContext.
        /// </summary>
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
            Content = asyncLoader;
            await asyncLoader.LoadAsync();
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