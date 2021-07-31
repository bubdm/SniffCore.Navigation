//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;
using System.Windows;

namespace SniffCore.Navigation.MessageBoxes
{
    /// <summary>
    ///     Provides possibilities to show message boxes.
    /// </summary>
    /// <example>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// public class ViewModel : ObservableObject
    /// {
    ///     private IMessageBoxProvider _messageBoxProvider;
    /// 
    ///     public ViewModel(IMessageBoxProvider messageBoxProvider)
    ///     {
    ///         _messageBoxProvider = messageBoxProvider;
    ///     }
    /// 
    ///     public bool ShallDeleteFile()
    ///     {
    ///         var result = _messageBoxProvider.Show("Do you want to delete the file?", "Question", MessageBoxButton.YesNo);
    ///         return result == MessageBoxResult.Yes;
    ///     }
    /// }
    /// ]]>
    /// </code>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// [TestFixture]
    /// public class ViewModelTests
    /// {
    ///     private Mock<IMessageBoxProvider> _messageBoxProvider;
    ///     private ViewModel _target;
    /// 
    ///     [SetUp]
    ///     public void Setup()
    ///     {
    ///         _messageBoxProvider = new Mock<IMessageBoxProvider>();
    ///         _target = new ViewModel(_messageBoxProvider.Object);
    ///     }
    /// 
    ///     [Test]
    ///     public void ShallDeleteFile_UserConfirmed_ReturnsTrue()
    ///     {
    ///         _messageBoxProvider.Setup(x => x.Show(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<MessageBoxButton>())
    ///             .Returns(MessageBoxResult.Yes);
    /// 
    ///         var result = _target.ShallDeleteFile();
    /// 
    ///         Assert.That(result, Is.True);
    ///     }
    /// 
    ///     [Test]
    ///     public void ShallDeleteFile_UserRejected_ReturnsTrue()
    ///     {
    ///         _messageBoxProvider.Setup(x => x.Show(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<MessageBoxButton>())
    ///             .Returns(MessageBoxResult.No);
    /// 
    ///         var result = _target.ShallDeleteFile();
    /// 
    ///         Assert.That(result, Is.False);
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public sealed class MessageBoxProvider : IMessageBoxProvider
    {
        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
        public MessageBoxResult Show(string messageBoxText)
        {
            if (messageBoxText == null)
                throw new ArgumentNullException(nameof(messageBoxText));

            return MessageBox.Show(messageBoxText);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="owner">The owner window of the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        /// <exception cref="ArgumentNullException">owner is null.</exception>
        /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
        public MessageBoxResult Show(Window owner, string messageBoxText)
        {
            if (owner == null)
                throw new ArgumentNullException(nameof(owner));
            if (messageBoxText == null)
                throw new ArgumentNullException(nameof(messageBoxText));

            return MessageBox.Show(owner, messageBoxText);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <returns>The result of the message box after closing.</returns>
        /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
        /// <exception cref="ArgumentNullException">caption is null.</exception>
        public MessageBoxResult Show(string messageBoxText, string caption)
        {
            if (messageBoxText == null)
                throw new ArgumentNullException(nameof(messageBoxText));
            if (caption == null)
                throw new ArgumentNullException(nameof(caption));

            return MessageBox.Show(messageBoxText, caption);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="owner">The owner window of the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <returns>The result of the message box after closing.</returns>
        /// <exception cref="ArgumentNullException">owner is null.</exception>
        /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
        /// <exception cref="ArgumentNullException">caption is null.</exception>
        public MessageBoxResult Show(Window owner, string messageBoxText, string caption)
        {
            if (owner == null)
                throw new ArgumentNullException(nameof(owner));
            if (messageBoxText == null)
                throw new ArgumentNullException(nameof(messageBoxText));
            if (caption == null)
                throw new ArgumentNullException(nameof(caption));

            return MessageBox.Show(owner, messageBoxText, caption);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
        /// <exception cref="ArgumentNullException">caption is null.</exception>
        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            if (messageBoxText == null)
                throw new ArgumentNullException(nameof(messageBoxText));
            if (caption == null)
                throw new ArgumentNullException(nameof(caption));

            return MessageBox.Show(messageBoxText, caption, button);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="owner">The owner window of the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        /// <exception cref="ArgumentNullException">owner is null.</exception>
        /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
        /// <exception cref="ArgumentNullException">caption is null.</exception>
        public MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button)
        {
            if (owner == null)
                throw new ArgumentNullException(nameof(owner));
            if (messageBoxText == null)
                throw new ArgumentNullException(nameof(messageBoxText));
            if (caption == null)
                throw new ArgumentNullException(nameof(caption));

            return MessageBox.Show(owner, messageBoxText, caption, button);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <param name="options">The additional options for the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
        /// <exception cref="ArgumentNullException">caption is null.</exception>
        /// <exception cref="ArgumentNullException">options is null.</exception>
        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options)
        {
            if (messageBoxText == null)
                throw new ArgumentNullException(nameof(messageBoxText));
            if (caption == null)
                throw new ArgumentNullException(nameof(caption));
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (options.Icon != null && options.DefaultResult != null)
                return MessageBox.Show(messageBoxText, caption, button, options.Icon.Value, options.DefaultResult.Value);
            if (options.Icon != null)
                return MessageBox.Show(messageBoxText, caption, button, options.Icon.Value);
            if (options.DefaultResult != null)
                return MessageBox.Show(messageBoxText, caption, button, MessageBoxImage.None, options.DefaultResult.Value);
            return MessageBox.Show(messageBoxText, caption, button);
        }

        /// <summary>
        ///     Shows the message box.
        /// </summary>
        /// <param name="owner">The owner window of the message box.</param>
        /// <param name="messageBoxText">The text show in the message box.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="button">The buttons show in the message box.</param>
        /// <param name="options">The additional options for the message box.</param>
        /// <returns>The result of the message box after closing.</returns>
        /// <exception cref="ArgumentNullException">owner is null.</exception>
        /// <exception cref="ArgumentNullException">messageBoxText is null.</exception>
        /// <exception cref="ArgumentNullException">caption is null.</exception>
        /// <exception cref="ArgumentNullException">options is null.</exception>
        public MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, IMessageBoxOptions options)
        {
            if (owner == null)
                throw new ArgumentNullException(nameof(owner));
            if (messageBoxText == null)
                throw new ArgumentNullException(nameof(messageBoxText));
            if (caption == null)
                throw new ArgumentNullException(nameof(caption));
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (options.Icon != null && options.DefaultResult != null)
                return MessageBox.Show(owner, messageBoxText, caption, button, options.Icon.Value, options.DefaultResult.Value);
            if (options.Icon != null)
                return MessageBox.Show(owner, messageBoxText, caption, button, options.Icon.Value);
            if (options.DefaultResult != null)
                return MessageBox.Show(owner, messageBoxText, caption, button, MessageBoxImage.None, options.DefaultResult.Value);
            return MessageBox.Show(owner, messageBoxText, caption, button);
        }
    }
}