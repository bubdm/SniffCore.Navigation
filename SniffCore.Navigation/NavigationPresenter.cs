//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;
using System.Windows;
using System.Windows.Controls;
using SniffCore.PleaseWaits;

// ReSharper disable MemberCanBePrivate.Global

namespace SniffCore.Navigation
{
    /// <summary>
    ///     Represents a host where a user control will be placed by the <see cref="INavigationService" />.
    /// </summary>
    /// <example>
    /// <code lang="XAML">
    /// <![CDATA[
    /// <Window xmlns:sniffcore="http://sniffcore.com">
    ///     <sniffcore:NavigationPresenter ID="MainSpot" />
    /// </Window>
    /// ]]>
    /// </code>
    ///
    /// <code lang="csharp">
    /// <![CDATA[
    /// public class ViewModel : ObservableObject
    /// {
    ///     private INavigationService _navigationService;
    ///
    ///     public ViewModel(INavigationService navigationService)
    ///     {
    ///         _navigationService = navigationService;
    ///     }
    ///
    ///     public async Task SwitchAsync()
    ///     {
    ///         var vm = new ControlViewModel();
    ///         // "Control" is the user control registered in the IWindowService for the INavigationService.
    ///         await _navigationService.ShowControlAsync("MainSpot", "Control", vm);
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public class NavigationPresenter : Control
    {
        /// <summary>
        ///     The DependencyProperty for the ID property.
        /// </summary>
        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(object), typeof(NavigationPresenter), new PropertyMetadata(OnIDChanged));

        internal static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(NavigationPresenter), new PropertyMetadata(OnContentChanged));

        /// <summary>
        ///     The DependencyProperty for the DisposeViewModel property.
        /// </summary>
        public static readonly DependencyProperty DisposeViewModelProperty =
            DependencyProperty.Register("DisposeViewModel", typeof(bool), typeof(NavigationPresenter), new PropertyMetadata(false));

        /// <summary>
        ///     The DependencyProperty for the PleaseWaitDataTemplate property.
        /// </summary>
        public static readonly DependencyProperty PleaseWaitDataTemplateProperty =
            DependencyProperty.Register("PleaseWaitDataTemplate", typeof(DataTemplate), typeof(NavigationPresenter), new PropertyMetadata(null));

        internal static readonly DependencyProperty PleaseWaitProgressProperty =
            DependencyProperty.Register("PleaseWaitProgress", typeof(LoadingProgress), typeof(NavigationPresenter), new PropertyMetadata(OnPleaseWaitProgressChanged));

        internal static readonly DependencyProperty ProgressDataProperty =
            DependencyProperty.Register("ProgressData", typeof(ProgressData), typeof(NavigationPresenter), new PropertyMetadata(null));

        static NavigationPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationPresenter), new FrameworkPropertyMetadata(typeof(NavigationPresenter)));
        }

        /// <summary>
        ///     Defines if the viewmodel shall be disposed if they implement the IDisposable and the ViewModel changed.
        /// </summary>
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

        /// <summary>
        ///     The ID of the <see cref="NavigationPresenter" /> how it registers in the <see cref="NavigationPresenter" />.
        /// </summary>
        public object ID
        {
            get => GetValue(IDProperty);
            set => SetValue(IDProperty, value);
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

        internal ProgressData ProgressData
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