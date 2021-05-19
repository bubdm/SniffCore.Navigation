//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SniffCore.Windows
{
    public sealed class WindowProvider : IWindowProvider
    {
        private static readonly DependencyProperty InstanceProperty =
            DependencyProperty.RegisterAttached("Instance", typeof(Guid), typeof(WindowProvider), new PropertyMetadata(default(Guid)));

        private readonly Dictionary<object, Type> _controls;
        private readonly Dictionary<Guid, Tuple<object, Window>> _openWindows;
        private readonly Dictionary<object, Type> _windows;

        public WindowProvider()
        {
            _windows = new Dictionary<object, Type>();
            _controls = new Dictionary<object, Type>();
            _openWindows = new Dictionary<Guid, Tuple<object, Window>>();
        }

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

        public Window GetOpenWindow(object windowKey)
        {
            if (windowKey == null)
                throw new ArgumentNullException(nameof(windowKey));

            var first = _openWindows.FirstOrDefault(x => Equals(x.Value.Item1, windowKey));
            if (first.Value == null)
                throw new InvalidOperationException($@"There is no open window with the window key '{windowKey}'");

            return first.Value.Item2;
        }

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

        public void RegisterWindow<TWindow>(object windowKey) where TWindow : Window
        {
            _windows[windowKey] = typeof(TWindow);
        }

        public void RegisterControl<TControl>(object controlKey) where TControl : UserControl
        {
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