using System;
using System.Threading;
using System.Windows;
using Moq;
using NUnit.Framework;
using SniffCore.Navigation.MessageBoxes;

namespace SniffCore.Navigation.Tests.MessageBoxes
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class MessageBoxProviderTests
    {
        [SetUp]
        public void Setup()
        {
            _target = new MessageBoxProvider();
        }

        private MessageBoxProvider _target;

        [Test]
        public void Show1_CalledWithNullMessage_ThrowsException()
        {
            string message = null;

            var act = new TestDelegate(() => _target.Show(message));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show2_CalledWithNullOwner_ThrowsException()
        {
            Window owner = null;
            var message = "message";

            var act = new TestDelegate(() => _target.Show(owner, message));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show2_CalledWithNullMessage_ThrowsException()
        {
            var owner = new Window();
            string message = null;

            var act = new TestDelegate(() => _target.Show(owner, message));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show3_CalledWithNullMessage_ThrowsException()
        {
            string message = null;
            var caption = "caption";

            var act = new TestDelegate(() => _target.Show(message, caption));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show3_CalledWithNullCaption_ThrowsException()
        {
            var message = "message";
            string caption = null;

            var act = new TestDelegate(() => _target.Show(message, caption));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show4_CalledWithNullOwner_ThrowsException()
        {
            Window owner = null;
            var message = "message";
            var caption = "caption";

            var act = new TestDelegate(() => _target.Show(owner, message, caption));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show4_CalledWithNullMessage_ThrowsException()
        {
            var owner = new Window();
            string message = null;
            var caption = "caption";

            var act = new TestDelegate(() => _target.Show(owner, message, caption));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show4_CalledWithNullCaption_ThrowsException()
        {
            var owner = new Window();
            var message = "message";
            string caption = null;

            var act = new TestDelegate(() => _target.Show(owner, message, caption));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show5_CalledWithNullMessage_ThrowsException()
        {
            string message = null;
            var caption = "caption";
            var button = MessageBoxButton.OKCancel;

            var act = new TestDelegate(() => _target.Show(message, caption, button));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show5_CalledWithNullCaption_ThrowsException()
        {
            var message = "message";
            string caption = null;
            var button = MessageBoxButton.OKCancel;

            var act = new TestDelegate(() => _target.Show(message, caption, button));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show6_CalledWithNullOwner_ThrowsException()
        {
            Window owner = null;
            var message = "message";
            var caption = "caption";
            var button = MessageBoxButton.OKCancel;

            var act = new TestDelegate(() => _target.Show(owner, message, caption, button));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show6_CalledWithNullMessage_ThrowsException()
        {
            var owner = new Window();
            string message = null;
            var caption = "caption";
            var button = MessageBoxButton.OKCancel;

            var act = new TestDelegate(() => _target.Show(owner, message, caption, button));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show6_CalledWithNullCaption_ThrowsException()
        {
            var owner = new Window();
            var message = "message";
            string caption = null;
            var button = MessageBoxButton.OKCancel;

            var act = new TestDelegate(() => _target.Show(owner, message, caption, button));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show7_CalledWithNullMessage_ThrowsException()
        {
            string message = null;
            var caption = "caption";
            var button = MessageBoxButton.OKCancel;
            var options = new Mock<IMessageBoxOptions>().Object;

            var act = new TestDelegate(() => _target.Show(message, caption, button, options));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show7_CalledWithNullCaption_ThrowsException()
        {
            var message = "message";
            string caption = null;
            var button = MessageBoxButton.OKCancel;
            var options = new Mock<IMessageBoxOptions>().Object;

            var act = new TestDelegate(() => _target.Show(message, caption, button, options));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show7_CalledWithNullOptions_ThrowsException()
        {
            var message = "message";
            var caption = "caption";
            var button = MessageBoxButton.OKCancel;
            IMessageBoxOptions options = null;

            var act = new TestDelegate(() => _target.Show(message, caption, button, options));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show8_CalledWithNullOwner_ThrowsException()
        {
            Window owner = null;
            var message = "message";
            var caption = "caption";
            var button = MessageBoxButton.OKCancel;
            var options = new Mock<IMessageBoxOptions>().Object;

            var act = new TestDelegate(() => _target.Show(owner, message, caption, button, options));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show8_CalledWithNullMessage_ThrowsException()
        {
            var owner = new Window();
            string message = null;
            var caption = "caption";
            var button = MessageBoxButton.OKCancel;
            var options = new Mock<IMessageBoxOptions>().Object;

            var act = new TestDelegate(() => _target.Show(owner, message, caption, button, options));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show8_CalledWithNullCaption_ThrowsException()
        {
            var owner = new Window();
            var message = "message";
            string caption = null;
            var button = MessageBoxButton.OKCancel;
            var options = new Mock<IMessageBoxOptions>().Object;

            var act = new TestDelegate(() => _target.Show(owner, message, caption, button, options));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show8_CalledWithNullOptions_ThrowsException()
        {
            var owner = new Window();
            var message = "message";
            var caption = "caption";
            var button = MessageBoxButton.OKCancel;
            IMessageBoxOptions options = null;

            var act = new TestDelegate(() => _target.Show(owner, message, caption, button, options));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}