//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

// ReSharper disable PossibleNullReferenceException

namespace SniffCore.Windows
{
    /// <summary>
    ///     Provides possibilities to create and display windows and user controls.
    /// </summary>
    public sealed class WindowProvider : IWindowProvider
    {
        private static readonly DependencyProperty InstanceProperty =
            DependencyProperty.RegisterAttached("Instance", typeof(Guid), typeof(WindowProvider), new PropertyMetadata(default(Guid)));

        private readonly Dictionary<object, Type> _controls;
        private readonly Dictionary<Guid, Tuple<object, Window>> _openWindows;
        private readonly Dictionary<object, Type> _windows;

        /// <summary>
        ///     Creates a new instance of <see cref="WindowProvider" />.
        /// </summary>
        public WindowProvider()
        {
            _windows = new Dictionary<object, Type>();
            _controls = new Dictionary<object, Type>();
            _openWindows = new Dictionary<Guid, Tuple<object, Window>>();
        }

        /// <summary>
        ///     Creates a new window by the key.
        /// </summary>
        /// <param name="windowKey">The key of the window to create.</param>
        /// <returns>The newly created window.</returns>
        /// <exception cref="ArgumentNullException">windowKey is null.</exception>
        /// <exception cref="InvalidOperationException">For the windowKey no window is registered.</exception>
        public Window GetNewWindow(object windowKey)
        {
            if (windowKey == null)
                throw new ArgumentNullException(nameof(windowKey));

            if (!_windows.TryGetValue(windowKey, out var windowType))
                throw new InvalidOperationException($"For the windowKey '{windowKey}' no window is registered");

            var window = (Window) Activator.CreateInstance(windowType);
            var instance = Guid.NewGuid();
            SetInstance(window, instance);
            _openWindows[instance] = Tuple.Create(windowKey, window);
            window.Closed += HandleWindowClosed;
            return window;
        }

        /// <summary>
        ///     Returns the open window by the key.
        /// </summary>
        /// <param name="windowKey">The key of the window to return.</param>
        /// <returns>The open window know by the key.</returns>
        /// <exception cref="ArgumentNullException">windowKey is null.</exception>
        public Window GetOpenWindow(object windowKey)
        {
            if (windowKey == null)
                throw new ArgumentNullException(nameof(windowKey));

            var (_, value) = _openWindows.FirstOrDefault(x => Equals(x.Value.Item1, windowKey));
            if (value == null)
                throw new InvalidOperationException($@"There is no open window with the window key '{windowKey}'");

            return value.Item2;
        }

        /// <summary>
        ///     Creates a new user control by the key.
        /// </summary>
        /// <param name="controlKey">The key of the user control to create.</param>
        /// <returns>The newly created user control.</returns>
        /// <exception cref="ArgumentNullException">controlKey is null.</exception>
        /// <exception cref="InvalidOperationException">For the control key no user control is registered</exception>
        public UserControl GetNewControl(object controlKey)
        {
            if (controlKey == null)
                throw new ArgumentNullException(nameof(controlKey));

            if (!_controls.TryGetValue(controlKey, out var controlType))
                throw new InvalidOperationException($"For the control key '{controlKey}' no user control is registered");

            return (UserControl) Activator.CreateInstance(controlType);
        }

        private void HandleWindowClosed(object sender, EventArgs e)
        {
            var window = (Window) sender;
            window.Closed -= HandleWindowClosed;
            var instance = GetInstance(window);
            _openWindows.Remove(instance);
        }

        /// <summary>
        ///     Registers a window type for a window key.
        /// </summary>
        /// <typeparam name="TWindow">The type of the window to register.</typeparam>
        /// <param name="windowKey">The window key.</param>
        /// <exception cref="ArgumentNullException">windowKey is null.</exception>
        public void RegisterWindow<TWindow>(object windowKey) where TWindow : Window
        {
            if (windowKey == null)
                throw new ArgumentNullException(nameof(windowKey));

            _windows[windowKey] = typeof(TWindow);
        }

        /// <summary>
        ///     Registers a user control type of a key.
        /// </summary>
        /// <typeparam name="TControl">The type of the user control to register.</typeparam>
        /// <param name="controlKey">The user control key.</param>
        /// <exception cref="ArgumentNullException">controlKey is null.</exception>
        public void RegisterControl<TControl>(object controlKey) where TControl : UserControl
        {
            if (controlKey == null)
                throw new ArgumentNullException(nameof(controlKey));

            _controls[controlKey] = typeof(TControl);
        }

        private static Guid GetInstance(DependencyObject obj)
        {
            return (Guid) obj.GetValue(InstanceProperty);
        }

        private static void SetInstance(DependencyObject obj, Guid value)
        {
            obj.SetValue(InstanceProperty, value);
        }
    }
}