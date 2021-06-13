using System;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using NUnit.Framework;
using SniffCore.Navigation.PleaseWaits;

namespace SniffCore.Navigation.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class NavigationPresenterTests
    {
        [SetUp]
        public void Setup()
        {
            _target = new NavigationPresenter();
        }

        [TearDown]
        public void Teardown()
        {
            NavigationService.GetRegisteredControls().Clear();
        }

        private NavigationPresenter _target;

        [Test]
        public void ID_Set_RegistersTheControlInTheNavigationService()
        {
            _target.ID = "TheID";

            var controls = NavigationService.GetRegisteredControls();
            Assert.That(controls.Count, Is.EqualTo(1));
            var (key, value) = controls.First();
            Assert.That(key, Is.EqualTo("TheID"));
            Assert.That(value.Target, Is.SameAs(_target));
        }

        [Test]
        public void ID_Changed_RegistersTheControlInTheNavigationService()
        {
            _target.ID = "TheID";
            _target.ID = "TheOtherID";

            var controls = NavigationService.GetRegisteredControls();
            Assert.That(controls.Count, Is.EqualTo(1));
            var (key, value) = controls.First();
            Assert.That(key, Is.EqualTo("TheOtherID"));
            Assert.That(value.Target, Is.SameAs(_target));
        }

        [Test]
        public void Content_ChangedAndOldIsDisposable_DisposesIt()
        {
            var disposableTestClass = new DisposableTestClass();
            _target.Content = new TextBlock {DataContext = disposableTestClass};
            _target.DisposeViewModel = true;
            Assert.That(disposableTestClass.IsDisposed, Is.False);

            _target.Content = "Hans";

            Assert.That(disposableTestClass.IsDisposed, Is.True);
        }

        [Test]
        public void Content_DisposingIsDisable_DoesNotDisposeTheOldViewModel()
        {
            var disposableTestClass = new DisposableTestClass();
            _target.Content = new TextBlock {DataContext = disposableTestClass};
            _target.DisposeViewModel = false;
            Assert.That(disposableTestClass.IsDisposed, Is.False);

            _target.Content = "Hans";

            Assert.That(disposableTestClass.IsDisposed, Is.False);
        }

        [Test]
        public void PleaseWaitProgress_ViewModelReportsProgress_ForwardsTheProgressData()
        {
            var progress = new LoadingProgress();
            _target.PleaseWaitProgress = progress;

            progress.Report(33, "Loading");

            var data = _target.ProgressData;
            Assert.That(data.Progress, Is.EqualTo(33));
            Assert.That(data.Message, Is.EqualTo("Loading"));
        }

        [Test]
        public void PleaseWaitProgress_ViewModelReportsProgressAnotherTime_IgnoresTheOld()
        {
            var oldProgress = new LoadingProgress();
            var newProgress = new LoadingProgress();
            _target.PleaseWaitProgress = oldProgress;
            _target.PleaseWaitProgress = newProgress;

            newProgress.Report(66, "Still Loading");
            oldProgress.Report(33, "Loading");

            var data = _target.ProgressData;
            Assert.That(data.Progress, Is.EqualTo(66));
            Assert.That(data.Message, Is.EqualTo("Still Loading"));
        }

        [Test]
        public void PleaseWaitProgress_ProgressGotReset_IgnoresReports()
        {
            var progress = new LoadingProgress();
            _target.PleaseWaitProgress = progress;
            _target.PleaseWaitProgress = null;

            progress.Report(33, "Loading");

            var data = _target.ProgressData;
            Assert.That(data, Is.Null);
        }

        private class DisposableTestClass : IDisposable
        {
            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }
    }
}