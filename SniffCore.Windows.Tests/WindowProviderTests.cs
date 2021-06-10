using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;

namespace SniffCore.Windows.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class WindowProviderTests
    {
        [SetUp]
        public void Setup()
        {
            _target = new WindowProvider();
        }

        private WindowProvider _target;

        [Test]
        public void GetNewWindow_CalledWithNull_ThrowsException()
        {
            string windowKey = null;

            var act = new TestDelegate(() => _ = _target.GetNewWindow(windowKey));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void GetNewWindow_CalledWithUnknownKey_ThrowsException()
        {
            var windowKey = "windowKey";

            var act = new TestDelegate(() => _ = _target.GetNewWindow(windowKey));

            Assert.Throws<InvalidOperationException>(act);
        }

        [Test]
        public void GetNewWindow_CalledWithKnownKey_GeneratesWindow()
        {
            var windowKey = "windowKey";
            _target.RegisterWindow<MyWindow>(windowKey);

            var result = _target.GetNewWindow(windowKey);

            Assert.That(result, Is.InstanceOf<MyWindow>());
        }

        [Test]
        public void GetNewWindow_CalledWithKnownKey_KeepsTheOpenWindow()
        {
            var windowKey = "windowKey";
            _target.RegisterWindow<MyWindow>(windowKey);

            var window = _target.GetNewWindow(windowKey);

            var openWindows = _target.OpenWindows();
            Assert.That(openWindows.Count, Is.EqualTo(1));
            var first = openWindows.First();
            Assert.That(first.Value.Item1, Is.EqualTo(windowKey));
            Assert.That(first.Value.Item2, Is.SameAs(window));
        }

        [Test]
        public void GetNewWindow_WindowClosedAfter_RemovesFromKnownWindows()
        {
            var windowKey = "windowKey";
            _target.RegisterWindow<MyWindow>(windowKey);

            var window = _target.GetNewWindow(windowKey);
            window.Close();

            var openWindows = _target.OpenWindows();
            Assert.That(openWindows, Is.Empty);
        }

        [Test]
        public void GetNewWindow_TwoWindowsTheSameInstanceOpenedOneClosed_RemovesTheRightFromKnownWindows()
        {
            var windowKey = "windowKey";
            _target.RegisterWindow<MyWindow>(windowKey);

            var window1 = _target.GetNewWindow(windowKey);
            var window2 = _target.GetNewWindow(windowKey);
            window2.Close();

            var openWindows = _target.OpenWindows();
            Assert.That(openWindows.Count, Is.EqualTo(1));
            var first = openWindows.First();
            Assert.That(first.Value.Item1, Is.EqualTo(windowKey));
            Assert.That(first.Value.Item2, Is.SameAs(window1));
        }

        [Test]
        public void GetOpenWindow_CalledWithNull_ThrowsException()
        {
            string windowKey = null;

            var act = new TestDelegate(() => _ = _target.GetOpenWindow(windowKey));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void GetOpenWindow_CalledWithNotOpenWindowKey_ThrowsException()
        {
            var windowKey = "windowKey";

            var act = new TestDelegate(() => _ = _target.GetOpenWindow(windowKey));

            Assert.Throws<InvalidOperationException>(act);
        }

        [Test]
        public void GetOpenWindow_CalledWithNotOpenWindowKey_ReturnsTheFirstMatching()
        {
            var windowKey = "windowKey";
            var openWindows = _target.OpenWindows();
            openWindows[Guid.NewGuid()] = new Tuple<object, Window>(windowKey, new MyWindow());
            openWindows[Guid.NewGuid()] = new Tuple<object, Window>(windowKey, new MyWindow());

            var window = _target.GetOpenWindow(windowKey);

            Assert.That(window, Is.SameAs(openWindows.First().Value.Item2));
        }

        [Test]
        public void GetNewControl_CalledWithNull_ThrowsException()
        {
            string controlKey = null;

            var act = new TestDelegate(() => _ = _target.GetNewControl(controlKey));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void GetNewControl_CalledWithUnknownKey_ThrowsException()
        {
            var controlKey = "controlKey";

            var act = new TestDelegate(() => _ = _target.GetNewControl(controlKey));

            Assert.Throws<InvalidOperationException>(act);
        }

        [Test]
        public void GetNewControl_CalledWithKnownKey_CreatesTheUserControl()
        {
            var controlKey = "controlKey";
            _target.RegisterControl<MyUserControl>(controlKey);

            var actual = _target.GetNewControl(controlKey);

            Assert.That(actual, Is.InstanceOf<MyUserControl>());
        }

        [Test]
        public void RegisterWindow_CalledWithNull_ThrowsException()
        {
            string windowKey = null;

            var act = new TestDelegate(() => _target.RegisterWindow<Window>(windowKey));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void RegisterWindow_Called_RegistersTheWindowPair()
        {
            var windowKey = "windowKey";

            _target.RegisterWindow<MyWindow>(windowKey);

            var registered = _target.RegisteredWindows();
            Assert.That(registered.Count, Is.EqualTo(1));
            var pair = registered.First();
            Assert.That(pair.Value, Is.EqualTo(typeof(MyWindow)));
        }

        [Test]
        public void RegisterControl_CalledWithNull_ThrowsException()
        {
            string controlKey = null;

            var act = new TestDelegate(() => _target.RegisterControl<MyUserControl>(controlKey));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void RegisterControl_Called_RegistersTheUserControlPair()
        {
            var controlKey = "controlKey";

            _target.RegisterControl<MyUserControl>(controlKey);

            var registered = _target.RegisteredControls();
            Assert.That(registered.Count, Is.EqualTo(1));
            var pair = registered.First();
            Assert.That(pair.Value, Is.EqualTo(typeof(MyUserControl)));
        }

        private class MyUserControl : UserControl
        {
        }

        private class MyWindow : Window
        {
        }
    }
}