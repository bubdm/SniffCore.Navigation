//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;
using System.Windows.Forms;

// ReSharper disable ClassNeverInstantiated.Global

namespace SniffCore.Navigation.Dialogs
{
    /// <summary>
    ///     Provides way how to show system dialogs for files and folders.
    /// </summary>
    /// <example>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// public class ViewModel : ObservableObject
    /// {
    ///     private IDialogProvider _dialogProvider;
    /// 
    ///     public ViewModel(IDialogProvider dialogProvider)
    ///     {
    ///         _dialogProvider = dialogProvider;
    ///     }
    /// 
    ///     public string GetFile()
    ///     {
    ///         var data = new OpenFileData
    ///         {
    ///             CheckFileExists = true,
    ///             MultiSelect = false
    ///         };
    /// 
    ///         if (_dialogProvider.Show(data))
    ///             return data.FileName;
    /// 
    ///         return null;
    ///     }
    /// }
    /// ]]>
    /// </code>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// [TestFixture]
    /// public class ViewModelTests
    /// {
    ///     private Mock<IDialogProvider> _dialogProvider;
    ///     private ViewModel _target;
    /// 
    ///     [SetUp]
    ///     public void Setup()
    ///     {
    ///         _dialogProvider = new Mock<IDialogProvider>();
    ///         _target = new ViewModel(_dialogProvider.Object);
    ///     }
    /// 
    ///     [Test]
    ///     public void GetFile_Called_ReturnsTheUserSelectedFile()
    ///     {
    ///         _dialogProvider.Setup(x => x.Show(Arg.Any<IOpenFileData>())
    ///             .Callback(e => e.FileName = "filename")
    ///             .Returns(true);
    /// 
    ///         var result = _target.GetFile();
    /// 
    ///         Assert.That(result, Is.EqualTo("filename"));
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public sealed class DialogProvider : IDialogProvider
    {
        /// <summary>
        ///     Shows the open file dialog.
        /// </summary>
        /// <param name="openFileData">The open file dialog data.</param>
        /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">openFileData is null.</exception>
        public bool Show(IOpenFileData openFileData)
        {
            if (openFileData == null)
                throw new ArgumentNullException(nameof(openFileData));

            var dialog = new OpenFileDialog
            {
                CheckFileExists = openFileData.CheckFileExists,
                CheckPathExists = openFileData.CheckPathExists,
                DefaultExt = openFileData.DefaultExt,
                FileName = openFileData.FileName,
                Filter = openFileData.Filter,
                FilterIndex = openFileData.FilterIndex,
                InitialDirectory = openFileData.InitialDirectory,
                Multiselect = openFileData.MultiSelect,
                Title = openFileData.Title,
                ValidateNames = openFileData.ValidateNames
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return false;

            openFileData.FileName = dialog.FileName;
            openFileData.FileNames = dialog.FileNames;
            openFileData.SafeFileName = dialog.SafeFileName;
            openFileData.SafeFileNames = dialog.SafeFileNames;
            return true;
        }

        /// <summary>
        ///     Shows the save file dialog.
        /// </summary>
        /// <param name="saveFileData">The save file dialog data.</param>
        /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">saveFileData is null.</exception>
        public bool Show(ISaveFileData saveFileData)
        {
            if (saveFileData == null)
                throw new ArgumentNullException(nameof(saveFileData));

            var dialog = new SaveFileDialog
            {
                CheckFileExists = saveFileData.CheckFileExists,
                CheckPathExists = saveFileData.CheckPathExists,
                CreatePrompt = saveFileData.CreatePrompt,
                DefaultExt = saveFileData.DefaultExt,
                FileName = saveFileData.FileName,
                Filter = saveFileData.Filter,
                FilterIndex = saveFileData.FilterIndex,
                InitialDirectory = saveFileData.InitialDirectory,
                OverwritePrompt = saveFileData.OverwritePrompt,
                Title = saveFileData.Title,
                ValidateNames = saveFileData.ValidateNames
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return false;

            saveFileData.FileName = dialog.FileName;
            saveFileData.FileNames = dialog.FileNames;
            return true;
        }

        /// <summary>
        ///     Shows the browse folder dialog.
        /// </summary>
        /// <param name="browseFolderData">The browse folder dialog data.</param>
        /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">browseFolderData is null.</exception>
        public bool Show(IBrowseFolderData browseFolderData)
        {
            if (browseFolderData == null)
                throw new ArgumentNullException(nameof(browseFolderData));

            var dialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = browseFolderData.ShowNewFolderButton
            };
            if (browseFolderData.RootFolder != null)
                dialog.RootFolder = browseFolderData.RootFolder.Value;
            if (!string.IsNullOrWhiteSpace(browseFolderData.Description))
                dialog.Description = browseFolderData.Description;
            if (!string.IsNullOrWhiteSpace(browseFolderData.SelectedPath))
                dialog.SelectedPath = browseFolderData.Description;

            if (dialog.ShowDialog() != DialogResult.OK)
                return false;

            browseFolderData.SelectedPath = dialog.SelectedPath;
            return true;
        }

        /// <summary>
        ///     Shows the color picker dialog.
        /// </summary>
        /// <param name="colorPickerData">The color picker dialog data.</param>
        /// <returns>True of the dialog was closed with OK; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">colorPickerData is null.</exception>
        public bool Show(IColorPickerData colorPickerData)
        {
            if (colorPickerData == null)
                throw new ArgumentNullException(nameof(colorPickerData));

            var dialog = new ColorDialog
            {
                AllowFullOpen = colorPickerData.AllowFullOpen,
                AnyColor = colorPickerData.AnyColor,
                Color = colorPickerData.Color,
                CustomColors = colorPickerData.CustomColors,
                FullOpen = colorPickerData.FullOpen,
                SolidColorOnly = colorPickerData.SolidColorOnly
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return false;

            colorPickerData.Color = dialog.Color;

            return true;
        }
    }
}