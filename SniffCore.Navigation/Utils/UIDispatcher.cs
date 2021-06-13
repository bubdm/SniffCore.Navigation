//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;
using System.Windows;
using System.Windows.Threading;

namespace SniffCore.Navigation.Utils
{
    /// <summary>
    ///     Provides access to the UI dispatcher in the WPF application environment.
    /// </summary>
    /// <example>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// public void ViewModel : ObservableObject
    /// {
    ///     private string _name;
    /// 
    ///     public string Name
    ///     {
    ///         get => _name;
    ///         set => NotifyAndSetIfChanged(ref _name, value);
    ///     }
    /// 
    ///     public void Update(string name)
    ///     {
    ///         UIDispatcher.Current.Invoke(() => Name = name);
    ///     }
    /// }
    /// ]]>
    /// </code>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// [TestFixture]
    /// public class ViewModelTests
    /// {
    ///     private ViewModel _target;
    /// 
    ///     [SetUp]
    ///     public void Setup()
    ///     {
    ///         UIDispatcher.Override(Dispatcher.CurrentDispatcher);
    /// 
    ///         _target = new ViewModel();
    ///     }
    /// 
    ///     [Test]
    ///     public void Update_Called_SetsTheProperty()
    ///     {
    ///         _target.Update("Peter");
    /// 
    ///         Assert.That(_target.Name, Is.EqualTo("Peter"));
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public static class UIDispatcher
    {
        private static Dispatcher _dispatcherOverride;
        private static readonly Lazy<Dispatcher> _uiDispatcher;

        static UIDispatcher()
        {
            _uiDispatcher = new Lazy<Dispatcher>(() =>
            {
                var app = Application.Current;
                return app?.Dispatcher;
            });
        }

        /// <summary>
        ///     Gets the current UI dispatcher. Null if no Application has been created yet and its not yet overwritten by
        ///     <see cref="Override" />.
        /// </summary>
        public static Dispatcher Current => _dispatcherOverride ?? _uiDispatcher.Value;

        /// <summary>
        ///     Provides the possibility to override the dispatcher returned by <see cref="Current" />.
        /// </summary>
        /// <remarks>
        ///     This can be set to null to revert back to the original UI dispatcher.
        /// </remarks>
        /// <param name="dispatcher">
        ///     The dispatcher to use in <see cref="Current" />. Can be null to revert back to the original UI
        ///     dispatcher.
        /// </param>
        public static void Override(Dispatcher dispatcher)
        {
            _dispatcherOverride = dispatcher;
        }
    }
}