using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SniffCore.Windows
{
    public sealed class WindowProvider : IWindowProvider
    {
        private readonly Dictionary<object, Type> _controls;
        private readonly Dictionary<object, Window> _openWindows;
        private readonly Dictionary<object, Type> _windows;

        public WindowProvider()
        {
            _windows = new Dictionary<object, Type>();
            _controls = new Dictionary<object, Type>();
            _openWindows = new Dictionary<object, Window>();
        }

        public Window GetNewWindow(object windowKey)
        {
            if (windowKey == null)
                throw new ArgumentNullException(nameof(windowKey));

            if (!_windows.TryGetValue(windowKey, out var windowType))
                throw new InvalidOperationException($"For the windowKey '{windowKey}' no window is registered");

            var window = (Window) Activator.CreateInstance(windowType);
            _openWindows[windowKey] = window;
            window.Closed += HandleWindowClosed;
            return window;
        }

        public Window GetOpenWindow(object windowKey)
        {
            if (windowKey == null)
                throw new ArgumentNullException(nameof(windowKey));
            return _openWindows[windowKey];
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
            var knownPair = _openWindows.FirstOrDefault(p => Equals(p.Value, window));
            _openWindows.Remove(knownPair.Key);
        }

        public void RegisterWindow<TWindow>(object windowKey) where TWindow : Window
        {
            _windows[windowKey] = typeof(TWindow);
        }

        public void RegisterControl<TControl>(object controlKey) where TControl : UserControl
        {
            _controls[controlKey] = typeof(TControl);
        }
    }
}