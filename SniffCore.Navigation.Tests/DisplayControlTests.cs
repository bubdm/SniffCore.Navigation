using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using SniffCore.Navigation.PleaseWaits;

namespace SniffCore.Navigation.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class DisplayControlTests
    {
        [SetUp]
        public void Setup()
        {
            _target = new DisplayControl();
        }

        private DisplayControl _target;

        [Test]
        public void ViewModel_SetToAPlainViewModel_SetsItOnContent()
        {
            var vm = new DisposableTestClass();

            _target.ViewModel = vm;

            Assert.That(_target.Content, Is.SameAs(vm));
        }

        [Test]
        public void ViewModel_ChangedAndOldIsDisposable_DisposesIt()
        {
            var disposableTestClass = new DisposableTestClass();
            _target.Content = disposableTestClass;
            _target.DisposeViewModel = true;
            Assert.That(disposableTestClass.IsDisposed, Is.False);

            _target.Content = new DisposableTestClass();

            Assert.That(disposableTestClass.IsDisposed, Is.True);
        }

        [Test]
        public void ViewModel_ChangedAndOldIsDisposableButItsDisabled_DoesNotDisposesIt()
        {
            var disposableTestClass = new DisposableTestClass();
            _target.Content = disposableTestClass;
            _target.DisposeViewModel = false;
            Assert.That(disposableTestClass.IsDisposed, Is.False);

            _target.Content = new DisposableTestClass();

            Assert.That(disposableTestClass.IsDisposed, Is.False);
        }

        [Test]
        public void ViewModel_ViewModelIsAnAsyncLoader_SetsContentAndLoadsIt()
        {
            var testClass = new AsyncLoaderTestClass();
            var triggered = false;
            testClass.WhileLoading = () =>
            {
                triggered = true;
                Assert.That(_target.Content, Is.SameAs(testClass));
            };

            _target.ViewModel = testClass;

            Assert.That(triggered, Is.True);
        }

        [Test]
        public async Task ViewModel_ViewModelIsAnDelayedAsyncLoader_SetsContentAfterLoadingFinished()
        {
            var triggered = false;
            var testClass = new DelayedAsyncLoaderTestClass
            {
                WhileLoading = _ =>
                {
                    triggered = true;
                    Assert.That(_target.Content, Is.Null);
                }
            };

            _target.ViewModel = testClass;

            await Task.Delay(100); // The content will be set after WhileLoading finished.
            Assert.That(_target.Content, Is.SameAs(testClass));
            Assert.That(triggered, Is.True);
        }

        [Test]
        public async Task ViewModel_ViewModelIsAnDelayedAsyncLoaderAndReportsProgress_KeepsTheProgressState()
        {
            var triggered = false;
            var testClass = new DelayedAsyncLoaderTestClass
            {
                WhileLoading = e =>
                {
                    triggered = true;
                    Assert.That(_target.PleaseWaitProgress, Is.SameAs(e));
                }
            };

            _target.ViewModel = testClass;

            await Task.Delay(100); // The content will be set after WhileLoading finished.
            Assert.That(_target.PleaseWaitProgress, Is.Null);
            Assert.That(triggered, Is.True);
        }

        private class DisposableTestClass : IDisposable
        {
            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }

        private class AsyncLoaderTestClass : IAsyncLoader
        {
            public Action WhileLoading { get; set; }

            public Task LoadAsync()
            {
                WhileLoading();
                return Task.CompletedTask;
            }
        }

        private class DelayedAsyncLoaderTestClass : IDelayedAsyncLoader
        {
            public Action<LoadingProgress> WhileLoading { get; set; }

            public Task LoadAsync(LoadingProgress progress)
            {
                WhileLoading(progress);
                return Task.CompletedTask;
            }
        }
    }
}