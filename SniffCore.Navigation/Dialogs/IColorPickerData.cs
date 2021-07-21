//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Drawing;

namespace SniffCore.Navigation.Dialogs
{
    /// <summary>
    ///     Represents a common dialog box that displays available colors along with controls that enable the user to define
    ///     custom colors. See <see cref="System.Windows.Forms.ColorDialog" />.
    /// </summary>
    public interface IColorPickerData
    {
        /// <summary>
        ///     Gets or sets a value indicating whether the user can use the dialog box to define custom colors.
        /// </summary>
        /// <returns>true if the user can define custom colors; otherwise, false. The default is true.</returns>
        bool AllowFullOpen { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box displays all available colors in the set of basic colors.
        /// </summary>
        /// <returns>
        ///     true if the dialog box displays all available colors in the set of basic colors; otherwise, false. The default
        ///     value is false.
        /// </returns>
        bool AnyColor { get; set; }

        /// <summary>
        ///     Gets or sets the color selected by the user.
        /// </summary>
        /// <returns>The color selected by the user. If a color is not selected, the default value is black.</returns>
        Color Color { get; set; }

        /// <summary>
        ///     Gets or sets the set of custom colors shown in the dialog box.
        /// </summary>
        /// <returns>A set of custom colors shown by the dialog box. The default value is null.</returns>
        int[] CustomColors { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the controls used to create custom colors are visible when the dialog box
        ///     is opened.
        /// </summary>
        /// <returns>
        ///     true if the custom color controls are available when the dialog box is opened; otherwise, false. The default
        ///     value is false.
        /// </returns>
        bool FullOpen { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box will restrict users to selecting solid colors only.
        /// </summary>
        /// <returns>true if users can select only solid colors; otherwise, false. The default value is false.</returns>
        bool SolidColorOnly { get; set; }
    }
}