//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

namespace SniffCore.Navigation.PleaseWaits
{
    /// <summary>
    ///     Provides a default IPleaseWaitProvider to register which does nothing.
    /// </summary>
    /// <example>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// public class ViewModel : ObservableObject
    /// {
    ///     private IPleaseWaitProvider _pleaseWaitProvider;
    /// 
    ///     public ViewModel(IPleaseWaitProvider pleaseWaitProvider)
    ///     {
    ///         _pleaseWaitProvider = pleaseWaitProvider;
    ///     }
    /// 
    ///     public void DoSomething()
    ///     {
    ///         _pleaseWaitProvider.Show();
    ///         FindFile();
    ///         _pleaseWaitProvider.Close();
    ///     }
    /// }
    /// ]]>
    /// </code>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// [TestFixture]
    /// public class ViewModelTests
    /// {
    ///     private Mock<IPleaseWaitProvider> _pleaseWaitProvider;
    ///     private ViewModel _target;
    /// 
    ///     [SetUp]
    ///     public void Setup()
    ///     {
    ///         _pleaseWaitProvider = new Mock<IPleaseWaitProvider>();
    ///         _target = new ViewModel(_pleaseWaitProvider.Object);
    ///     }
    /// 
    ///     [Test]
    ///     public void DoSomething_Called_ShowPleaseWait()
    ///     {
    ///         var showTriggered = false;
    ///         var closeTriggered = false;
    ///         _messageBoxProvider.Setup(x => x.Show().Callback(() =>
    ///         {
    ///             Assert.That(closeTriggered, Is.False);
    ///             showTriggered = true;
    ///         });
    ///         _messageBoxProvider.Setup(x => x.Close().Callback(() =>
    ///         {
    ///             Assert.That(showTriggered, Is.True);
    ///             closeTriggered = true;
    ///         });
    /// 
    ///         var result = _target.ShallDeleteFile();
    /// 
    ///         Assert.That(showTriggered, Is.True);
    ///         Assert.That(closeTriggered, Is.True);
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public sealed class InvisiblePleaseWaitProvider : IPleaseWaitProvider
    {
        /// <summary>
        ///     Does nothing.
        /// </summary>
        public void Show()
        {
        }

        /// <summary>
        ///     Does nothing.
        /// </summary>
        public void Close()
        {
        }

        /// <summary>
        ///     Does nothing.
        /// </summary>
        /// <param name="progressData">unused</param>
        public void HandleProgress(ProgressData progressData)
        {
        }

        /// <summary>
        ///     Does nothing.
        /// </summary>
        /// <param name="canceledData">unused</param>
        public void HandleCanceled(LoadingCanceled canceledData)
        {
        }
    }
}