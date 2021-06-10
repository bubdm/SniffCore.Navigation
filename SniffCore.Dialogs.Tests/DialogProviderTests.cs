using System;
using NUnit.Framework;

namespace SniffCore.Dialogs.Tests
{
    [TestFixture]
    public class DialogProviderTests
    {
        [SetUp]
        public void Setup()
        {
            _target = new DialogProvider();
        }

        private DialogProvider _target;

        [Test]
        public void Show_CalledWithNullSaveFileData_ThrowsException()
        {
            SaveFileData data = null;

            var act = new TestDelegate(() => _target.Show(data));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show_CalledWithNullOpenFileData_ThrowsException()
        {
            OpenFileData data = null;

            var act = new TestDelegate(() => _target.Show(data));

            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Show_CalledWithNullBrowseFolderData_ThrowsException()
        {
            BrowseFolderData data = null;

            var act = new TestDelegate(() => _target.Show(data));

            Assert.Throws<ArgumentNullException>(act);
        }
    }
}