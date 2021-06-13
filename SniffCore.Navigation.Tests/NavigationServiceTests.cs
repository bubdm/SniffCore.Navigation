using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Moq;
using NUnit.Framework;
using SniffCore.Navigation.Dialogs;
using SniffCore.Navigation.MessageBoxes;
using SniffCore.Navigation.PleaseWaits;
using SniffCore.Navigation.Windows;
using MessageBoxOptions = SniffCore.Navigation.MessageBoxes.MessageBoxOptions;

namespace SniffCore.Navigation.Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class NavigationServiceTests
    {
        [SetUp]
        public void Setup()
        {
            _windowProvider = new Mock<IWindowProvider>();
            _messageBoxProvider = new Mock<IMessageBoxProvider>();
            _pleaseWaitProvider = new Mock<IPleaseWaitProvider>();
            _dialogProvider = new Mock<IDialogProvider>();
            _target = new TestableNavigationService(_windowProvider.Object, _messageBoxProvider.Object, _pleaseWaitProvider.Object, _dialogProvider.Object);
        }

        [TearDown]
        public void Teardown()
        {
            NavigationService.GetRegisteredControls().Clear();
        }

        private Mock<IWindowProvider> _windowProvider;
        private Mock<IMessageBoxProvider> _messageBoxProvider;
        private Mock<IPleaseWaitProvider> _pleaseWaitProvider;
        private Mock<IDialogProvider> _dialogProvider;
        private TestableNavigationService _target;

        [Test]
        public async Task ShowWindowAsync_CalledWithoutOwner_ShowsNewCreatedWindow()
        {
            var window = new Window();
            var viewModel = new object();
            _windowProvider.Setup(x => x.GetNewWindow("windowKey")).Returns(window);
            var triggered = false;
            _target.OnWindowShow = e =>
            {
                triggered = true;
                Assert.That(e, Is.SameAs(window));
                Assert.That(e.DataContext, Is.SameAs(viewModel));
            };

            await _target.ShowWindowAsync("windowKey", viewModel);

            Assert.That(triggered, Is.True);
        }

        [Test]
        public void ShowWindowAsync_CalledWithOwner_ShowsNewCreatedWindowWithOwnerWindow()
        {
            var window = new Window();
            var owner = new Window();
            var viewModel = new object();
            _windowProvider.Setup(x => x.GetNewWindow("windowKey")).Returns(window);
            _windowProvider.Setup(x => x.GetOpenWindow("ownerKey")).Returns(owner);

            async Task AsyncTestDelegate()
            {
                await _target.ShowWindowAsync("ownerKey", "windowKey", viewModel);
            }

            // The InvalidOperationException proves the code wanted to set the Owner in an unit test environment.
            Assert.ThrowsAsync<InvalidOperationException>(AsyncTestDelegate);
        }

        [Test]
        public async Task ShowWindowAsync_ViewModelIsAnAsyncLoader_SetsContentAndLoadsIt()
        {
            var window = new Window();
            var showWindowTriggered = false;
            var loadingTriggered = false;
            var viewModel = new AsyncLoaderTestClass
            {
                WhileLoading = () =>
                {
                    loadingTriggered = true;
                    Assert.That(showWindowTriggered, Is.True);
                }
            };
            _target.OnWindowShow = e =>
            {
                showWindowTriggered = true;
                Assert.That(loadingTriggered, Is.False);
            };
            _windowProvider.Setup(x => x.GetNewWindow("windowKey")).Returns(window);

            await _target.ShowWindowAsync("windowKey", viewModel);

            Assert.That(showWindowTriggered, Is.True);
            Assert.That(loadingTriggered, Is.True);
        }

        [Test]
        public async Task ShowWindowAsync_ViewModelIsAnDelayedAsyncLoader_SetsContentAfterLoadingFinished()
        {
            var window = new Window();
            var showWindowTriggered = false;
            var loadingTriggered = false;
            var viewModel = new DelayedAsyncLoaderTestClass
            {
                WhileLoading = _ =>
                {
                    loadingTriggered = true;
                    Assert.That(showWindowTriggered, Is.False);
                }
            };
            _target.OnWindowShow = e =>
            {
                showWindowTriggered = true;
                Assert.That(loadingTriggered, Is.True);
            };
            _windowProvider.Setup(x => x.GetNewWindow("windowKey")).Returns(window);

            await _target.ShowWindowAsync("windowKey", viewModel);

            Assert.That(showWindowTriggered, Is.True);
            Assert.That(loadingTriggered, Is.True);
        }

        [Test]
        public async Task ShowWindowAsync_ViewModelIsAnDelayedAsyncLoaderAndReportsProgress_KeepsTheProgressState()
        {
            var window = new Window();
            var triggered = false;
            var viewModel = new DelayedAsyncLoaderTestClass
            {
                WhileLoading = e =>
                {
                    e.Report(50, "data");
                    triggered = true;
                }
            };
            _windowProvider.Setup(x => x.GetNewWindow("windowKey")).Returns(window);

            await _target.ShowWindowAsync("windowKey", viewModel);

            Assert.That(triggered, Is.True);
            _pleaseWaitProvider.Verify(x => x.Show(), Times.Once);
            _pleaseWaitProvider.Verify(x => x.HandleProgress(It.Is<ProgressData>(y => y.Progress == 50 && y.Message == "data")), Times.Once);
            _pleaseWaitProvider.Verify(x => x.Close(), Times.Once);
        }

        [Test]
        public async Task ShowWindowAsync_ViewModelIsAnDelayedAsyncLoaderButCanceled_ForwardsTheCancel()
        {
            var window = new Window();
            var triggered = false;
            var viewModel = new DelayedAsyncLoaderTestClass
            {
                WhileLoading = e =>
                {
                    e.Cancel("Reason");
                    triggered = true;
                }
            };
            _windowProvider.Setup(x => x.GetNewWindow("windowKey")).Returns(window);

            await _target.ShowWindowAsync("windowKey", viewModel);

            Assert.That(triggered, Is.True);
            _pleaseWaitProvider.Verify(x => x.Show(), Times.Once);
            _pleaseWaitProvider.Verify(x => x.HandleCanceled(It.Is<LoadingCanceled>(y => y.Reason == "Reason")), Times.Once);
            _pleaseWaitProvider.Verify(x => x.Close(), Times.Once);
        }

        [Test]
        public async Task ShowWindowAsync_ViewModelIsAnDelayedAsyncLoaderButCanceled_DoesNotShowTheWindow()
        {
            var window = new Window();
            var viewModel = new DelayedAsyncLoaderTestClass {WhileLoading = e => { e.Cancel("Reason"); }};
            _windowProvider.Setup(x => x.GetNewWindow("windowKey")).Returns(window);
            _target.OnWindowShow = _ => { Assert.Fail("Window.Show unexpected"); };

            await _target.ShowWindowAsync("windowKey", viewModel);
        }

        [Test]
        public async Task ShowModalWindowAsync_CalledWithoutOwner_ShowsNewCreatedWindow()
        {
            var window = new Window();
            var viewModel = new object();
            _windowProvider.Setup(x => x.GetNewWindow("windowKey")).Returns(window);
            var triggered = false;
            _target.OnWindowShowDialog = e =>
            {
                triggered = true;
                Assert.That(e, Is.SameAs(window));
                Assert.That(e.DataContext, Is.SameAs(viewModel));
                return true;
            };

            await _target.ShowModalWindowAsync("windowKey", viewModel);

            Assert.That(triggered, Is.True);
        }

        [Test]
        public void ShowModalWindowAsync_CalledWithOwner_ShowsNewCreatedWindowWithOwnerWindow()
        {
            var window = new Window();
            var owner = new Window();
            var viewModel = new object();
            _windowProvider.Setup(x => x.GetNewWindow("windowKey")).Returns(window);
            _windowProvider.Setup(x => x.GetOpenWindow("ownerKey")).Returns(owner);

            async Task AsyncTestDelegate()
            {
                await _target.ShowModalWindowAsync("ownerKey", "windowKey", viewModel);
            }

            // The InvalidOperationException proves the code wanted to set the Owner in an unit test environment.
            Assert.ThrowsAsync<InvalidOperationException>(AsyncTestDelegate);
        }

        [Test]
        public async Task ShowModalWindowAsync_ViewModelIsAnAsyncLoader_SetsContentAndLoadsIt()
        {
            var window = new Window();
            var showWindowTriggered = false;
            var loadingTriggered = false;
            var viewModel = new AsyncLoaderTestClass {WhileLoading = () => { loadingTriggered = true; }};
            _target.OnWindowShowDialog = e =>
            {
                showWindowTriggered = true;
                return true;
            };
            _windowProvider.Setup(x => x.GetNewWindow("windowKey")).Returns(window);

            await _target.ShowModalWindowAsync("windowKey", viewModel);

            Assert.That(showWindowTriggered, Is.True);
            Assert.That(loadingTriggered, Is.True);
        }

        [Test]
        public async Task ShowModalWindowAsync_ViewModelIsAnDelayedAsyncLoader_SetsContentAfterLoadingFinished()
        {
            var window = new Window();
            var showWindowTriggered = false;
            var loadingTriggered = false;
            var viewModel = new DelayedAsyncLoaderTestClass
            {
                WhileLoading = _ =>
                {
                    loadingTriggered = true;
                    Assert.That(showWindowTriggered, Is.False);
                }
            };
            _target.OnWindowShowDialog = e =>
            {
                showWindowTriggered = true;
                Assert.That(loadingTriggered, Is.True);
                return true;
            };
            _windowProvider.Setup(x => x.GetNewWindow("windowKey")).Returns(window);

            await _target.ShowModalWindowAsync("windowKey", viewModel);

            Assert.That(showWindowTriggered, Is.True);
            Assert.That(loadingTriggered, Is.True);
        }

        [Test]
        public async Task ShowModalWindowAsync_ViewModelIsAnDelayedAsyncLoaderAndReportsProgress_KeepsTheProgressState()
        {
            var window = new Window();
            var triggered = false;
            var viewModel = new DelayedAsyncLoaderTestClass
            {
                WhileLoading = e =>
                {
                    e.Report(50, "data");
                    triggered = true;
                }
            };
            _windowProvider.Setup(x => x.GetNewWindow("windowKey")).Returns(window);

            await _target.ShowModalWindowAsync("windowKey", viewModel);

            Assert.That(triggered, Is.True);
            _pleaseWaitProvider.Verify(x => x.Show(), Times.Once);
            _pleaseWaitProvider.Verify(x => x.HandleProgress(It.Is<ProgressData>(y => y.Progress == 50 && y.Message == "data")), Times.Once);
            _pleaseWaitProvider.Verify(x => x.Close(), Times.Once);
        }

        [Test]
        public async Task ShowModalWindowAsync_ViewModelIsAnDelayedAsyncLoaderButCanceled_ForwardsTheCancel()
        {
            var window = new Window();
            var triggered = false;
            var viewModel = new DelayedAsyncLoaderTestClass
            {
                WhileLoading = e =>
                {
                    e.Cancel("Reason");
                    triggered = true;
                }
            };
            _windowProvider.Setup(x => x.GetNewWindow("windowKey")).Returns(window);

            await _target.ShowModalWindowAsync("windowKey", viewModel);

            Assert.That(triggered, Is.True);
            _pleaseWaitProvider.Verify(x => x.Show(), Times.Once);
            _pleaseWaitProvider.Verify(x => x.HandleCanceled(It.Is<LoadingCanceled>(y => y.Reason == "Reason")), Times.Once);
            _pleaseWaitProvider.Verify(x => x.Close(), Times.Once);
        }

        [Test]
        public async Task ShowModalWindowAsync_ViewModelIsAnDelayedAsyncLoaderButCanceled_DoesNotShowTheWindow()
        {
            var window = new Window();
            var viewModel = new DelayedAsyncLoaderTestClass {WhileLoading = e => { e.Cancel("Reason"); }};
            _windowProvider.Setup(x => x.GetNewWindow("windowKey")).Returns(window);
            _target.OnWindowShowDialog = _ =>
            {
                Assert.Fail("Window.Show unexpected");
                return true;
            };

            await _target.ShowModalWindowAsync("windowKey", viewModel);
        }

        [Test]
        public void SetDialogResult_WindowProviderThrowsException_ForwardsTheException()
        {
            _windowProvider.Setup(x => x.GetOpenWindow("key")).Throws<ArgumentNullException>();

            void Action()
            {
                _target.SetDialogResult("key", true);
            }

            Assert.Throws<ArgumentNullException>(Action);
        }

        [Test]
        public void SetDialogResult_Called_SetsTheDialogResultOnTheWindow()
        {
            var window = new Window();
            _windowProvider.Setup(x => x.GetOpenWindow("key")).Returns(window);

            void Action()
            {
                _target.SetDialogResult("key", true);
            }

            // The InvalidOperationException proves the code wanted to set the DialogResult in an unit test environment.
            Assert.Throws<InvalidOperationException>(Action);
        }

        [Test]
        public void Close_WindowProviderThrowsException_ForwardsTheException()
        {
            _windowProvider.Setup(x => x.GetOpenWindow("key")).Throws<ArgumentNullException>();

            void Action()
            {
                _target.Close("key");
            }

            Assert.Throws<ArgumentNullException>(Action);
        }

        [Test]
        public void Close_Called_SetsTheDialogResultOnTheWindow()
        {
            var window = new Window();
            var triggered = false;

            void WindowOnClosing(object sender, CancelEventArgs e)
            {
                triggered = true;
            }

            window.Closing += WindowOnClosing;
            _windowProvider.Setup(x => x.GetOpenWindow("key")).Returns(window);

            _target.Close("key");

            window.Closing -= WindowOnClosing;
            Assert.That(triggered, Is.True);
        }

        [Test]
        public void ShowUserControlAsync_Called_RemovesDeadRegisters()
        {
            new Action(() =>
            {
                var presenter = new NavigationPresenter();
                NavigationService.RegisterPresenter("otherControlKey", presenter);
            })();
            Assert.That(NavigationService.GetRegisteredControls().Count, Is.EqualTo(1));
            GC.Collect();
            GC.WaitForPendingFinalizers();

            var act = new AsyncTestDelegate(async () => await _target.ShowControlAsync("TheHost", "controlKey", new object()));

            Assert.ThrowsAsync<InvalidOperationException>(act);
            Assert.That(NavigationService.GetRegisteredControls().Count, Is.EqualTo(0));
        }

        [Test]
        public async Task ShowUserControlAsync_CalledWithSimpleViewModel_ShowsControlInPresenter()
        {
            var host = new NavigationPresenter();
            var control = new UserControl();
            var viewModel = new object();
            NavigationService.RegisterPresenter("TheHost", host);
            _windowProvider.Setup(x => x.GetNewControl("TheControl")).Returns(control);

            await _target.ShowControlAsync("TheHost", "TheControl", viewModel);

            Assert.That(host.Content, Is.SameAs(control));
            Assert.That(control.DataContext, Is.SameAs(viewModel));
        }

        [Test]
        public async Task ShowUserControlAsync_ViewModelIsAnAsyncLoader_SetsContentAndLoadsIt()
        {
            var host = new NavigationPresenter();
            var control = new UserControl();
            var triggered = false;
            var viewModel = new AsyncLoaderTestClass
            {
                WhileLoading = () =>
                {
                    triggered = true;
                    Assert.That(host.Content, Is.EqualTo(control));
                }
            };
            NavigationService.RegisterPresenter("TheHost", host);
            _windowProvider.Setup(x => x.GetNewControl("TheControl")).Returns(control);

            await _target.ShowControlAsync("TheHost", "TheControl", viewModel);

            Assert.That(triggered, Is.True);
        }

        [Test]
        public async Task ShowUserControlAsync_ViewModelIsAnDelayedAsyncLoader_SetsContentAfterLoadingFinished()
        {
            var host = new NavigationPresenter();
            var control = new UserControl();
            var triggered = false;
            var viewModel = new DelayedAsyncLoaderTestClass
            {
                WhileLoading = _ =>
                {
                    triggered = true;
                    Assert.That(host.Content, Is.Null);
                }
            };
            NavigationService.RegisterPresenter("TheHost", host);
            _windowProvider.Setup(x => x.GetNewControl("TheControl")).Returns(control);

            await _target.ShowControlAsync("TheHost", "TheControl", viewModel);

            await Task.Delay(100); // The content will be set after WhileLoading finished.
            Assert.That(host.Content, Is.EqualTo(control));
            Assert.That(triggered, Is.True);
        }

        [Test]
        public async Task ShowUserControlAsync_ViewModelIsAnDelayedAsyncLoaderAndReportsProgress_KeepsTheProgressState()
        {
            var host = new NavigationPresenter();
            var control = new UserControl();
            var triggered = false;
            var viewModel = new DelayedAsyncLoaderTestClass
            {
                WhileLoading = e =>
                {
                    triggered = true;
                    Assert.That(host.PleaseWaitProgress, Is.SameAs(e));
                }
            };
            NavigationService.RegisterPresenter("TheHost", host);
            _windowProvider.Setup(x => x.GetNewControl("TheControl")).Returns(control);

            await _target.ShowControlAsync("TheHost", "TheControl", viewModel);

            await Task.Delay(100); // The content will be set after WhileLoading finished.
            Assert.That(host.PleaseWaitProgress, Is.Null);
            Assert.That(triggered, Is.True);
        }

        [Test]
        public void MessageBox_Called1_ForwardsTheCall()
        {
            _target.ShowMessageBox("Message");

            _messageBoxProvider.Verify(x => x.Show("Message"), Times.Once);
        }

        [Test]
        public void MessageBox_Called2_ForwardsTheCall()
        {
            var window = new Window();
            _windowProvider.Setup(x => x.GetOpenWindow("windowKey")).Returns(window);

            _target.ShowMessageBox((object) "windowKey", "Message");

            _messageBoxProvider.Verify(x => x.Show(window, "Message"), Times.Once);
        }

        [Test]
        public void MessageBox_Called3_ForwardsTheCall()
        {
            _target.ShowMessageBox("Message", "Caption");

            _messageBoxProvider.Verify(x => x.Show("Message", "Caption"), Times.Once);
        }

        [Test]
        public void MessageBox_Called4_ForwardsTheCall()
        {
            var window = new Window();
            _windowProvider.Setup(x => x.GetOpenWindow("windowKey")).Returns(window);

            _target.ShowMessageBox("windowKey", "Message", "Caption");

            _messageBoxProvider.Verify(x => x.Show(window, "Message", "Caption"), Times.Once);
        }

        [Test]
        public void MessageBox_Called5_ForwardsTheCall()
        {
            _target.ShowMessageBox("Message", "Caption", MessageBoxButton.YesNoCancel);

            _messageBoxProvider.Verify(x => x.Show("Message", "Caption", MessageBoxButton.YesNoCancel), Times.Once);
        }

        [Test]
        public void MessageBox_Called6_ForwardsTheCall()
        {
            var window = new Window();
            _windowProvider.Setup(x => x.GetOpenWindow("windowKey")).Returns(window);

            _target.ShowMessageBox("windowKey", "Message", "Caption", MessageBoxButton.YesNoCancel);

            _messageBoxProvider.Verify(x => x.Show(window, "Message", "Caption", MessageBoxButton.YesNoCancel), Times.Once);
        }

        [Test]
        public void MessageBox_Called7_ForwardsTheCall()
        {
            var options = new MessageBoxOptions();

            _target.ShowMessageBox("Message", "Caption", MessageBoxButton.YesNoCancel, options);

            _messageBoxProvider.Verify(x => x.Show("Message", "Caption", MessageBoxButton.YesNoCancel, options), Times.Once);
        }

        [Test]
        public void MessageBox_Called8_ForwardsTheCall()
        {
            var window = new Window();
            var options = new MessageBoxOptions();
            _windowProvider.Setup(x => x.GetOpenWindow("windowKey")).Returns(window);

            _target.ShowMessageBox("windowKey", "Message", "Caption", MessageBoxButton.YesNoCancel, options);

            _messageBoxProvider.Verify(x => x.Show(window, "Message", "Caption", MessageBoxButton.YesNoCancel, options), Times.Once);
        }

        [Test]
        public void ShowDialog_CalledWithOpenFileData_ForwardsToDialogProvider()
        {
            var data = new OpenFileData();
            _dialogProvider.Setup(x => x.Show(data)).Returns(true);

            var result = _target.ShowDialog(data);

            Assert.That(result, Is.True);
            _dialogProvider.Verify(x => x.Show(data), Times.Once);
        }

        [Test]
        public void ShowDialog_CalledWithSaveFileData_ForwardsToDialogProvider()
        {
            var data = new SaveFileData();
            _dialogProvider.Setup(x => x.Show(data)).Returns(true);

            var result = _target.ShowDialog(data);

            Assert.That(result, Is.True);
            _dialogProvider.Verify(x => x.Show(data), Times.Once);
        }

        [Test]
        public void ShowDialog_CalledWithBrowseFolderData_ForwardsToDialogProvider()
        {
            var data = new BrowseFolderData();
            _dialogProvider.Setup(x => x.Show(data)).Returns(true);

            var result = _target.ShowDialog(data);

            Assert.That(result, Is.True);
            _dialogProvider.Verify(x => x.Show(data), Times.Once);
        }

        [Test]
        public void UnregisterPresenter_CalledWithKnownPresenter_RemovesThePresenter()
        {
            var host = new NavigationPresenter();
            NavigationService.RegisterPresenter("TheHost", host);

            NavigationService.UnregisterPresenter("TheHost");

            var controls = NavigationService.GetRegisteredControls();
            Assert.That(controls, Is.Empty);
        }

        [Test]
        public void UnregisterPresenter_CalledWithUnknownHost_DoesNothing()
        {
            var host = new NavigationPresenter();
            NavigationService.RegisterPresenter("TheHost", host);

            NavigationService.UnregisterPresenter("Hello");

            var controls = NavigationService.GetRegisteredControls();
            Assert.That(controls.Count, Is.EqualTo(1));
            Assert.That(controls["TheHost"].Target, Is.SameAs(host));
        }

        [Test]
        public void RegisterPresenter_Called_KeepsThePresenterInternally()
        {
            var host = new NavigationPresenter();

            NavigationService.RegisterPresenter("TheHost", host);

            var controls = NavigationService.GetRegisteredControls();
            Assert.That(controls.Count, Is.EqualTo(1));
            Assert.That(controls["TheHost"].Target, Is.SameAs(host));
        }

        [Test]
        public void RegisterPresenter_CalledTwiceWithSameKey_OverridesThePresenter()
        {
            var oldHost = new NavigationPresenter();
            var newHost = new NavigationPresenter();

            NavigationService.RegisterPresenter("TheHost", oldHost);
            NavigationService.RegisterPresenter("TheHost", newHost);

            var controls = NavigationService.GetRegisteredControls();
            Assert.That(controls.Count, Is.EqualTo(1));
            Assert.That(controls["TheHost"].Target, Is.SameAs(newHost));
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

        private class TestableNavigationService : NavigationService
        {
            public TestableNavigationService(IWindowProvider windowProvider,
                IMessageBoxProvider messageBoxProvider,
                IPleaseWaitProvider pleaseWaitProvider,
                IDialogProvider dialogProvider)
                : base(windowProvider, messageBoxProvider, pleaseWaitProvider, dialogProvider)
            {
            }

            public Action<Window> OnWindowShow { get; set; }
            public Func<Window, bool?> OnWindowShowDialog { get; set; }

            internal override void WindowShow(Window window)
            {
                OnWindowShow?.Invoke(window);
            }

            internal override bool? WindowShowDialog(Window window)
            {
                return OnWindowShowDialog?.Invoke(window);
            }
        }
    }
}