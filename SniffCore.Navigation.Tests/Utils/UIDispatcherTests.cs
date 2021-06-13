using System.Windows.Threading;
using NUnit.Framework;
using SniffCore.Navigation.Utils;

namespace SniffCore.Navigation.Tests.Utils
{
    [TestFixture]
    public class UIDispatcherTests
    {
        [Test]
        public void Override_Called_UsesTheRightDispatcher()
        {
            UIDispatcher.Override(Dispatcher.CurrentDispatcher);

            var triggered = false;
            UIDispatcher.Current.Invoke(() => triggered = true);

            Assert.That(triggered, Is.True);
        }
    }
}