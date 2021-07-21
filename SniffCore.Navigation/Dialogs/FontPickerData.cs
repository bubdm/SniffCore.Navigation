//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System;
using System.ComponentModel;
using System.Drawing;

namespace SniffCore.Navigation.Dialogs
{
    /// <summary>
    ///     Prompts the user to choose a font from among those installed on the local computer. See
    ///     <see cref="System.Windows.Forms.FontDialog" />.
    /// </summary>
    public class FontPickerData : IFontPickerData
    {
        /// <summary>
        ///     Creates a new instance of <see cref="FontPickerData" />.
        /// </summary>
        public FontPickerData()
        {
            AllowScriptChange = true;
            AllowSimulations = true;
            AllowVectorFonts = true;
            AllowVerticalFonts = true;
            Color = Color.Black;
            FixedPitchOnly = false;
            Font = null;
            FontMustExist = false;
            MaxSize = 0;
            MinSize = 0;
            ScriptsOnly = false;
            ShowApply = false;
            ShowColor = false;
            ShowEffects = true;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the user can change the character set specified in the Script combo box to
        ///     display a character set other than the one currently displayed.
        /// </summary>
        /// <returns>
        ///     true if the user can change the character set specified in the Script combo box; otherwise, false. The default
        ///     value is true.
        /// </returns>
        [DefaultValue(true)]
        public bool AllowScriptChange { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box allows graphics device interface (GDI) font simulations.
        /// </summary>
        /// <returns>true if font simulations are allowed; otherwise, false. The default value is true.</returns>
        [DefaultValue(true)]
        public bool AllowSimulations { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box allows vector font selections.
        /// </summary>
        /// <returns>true if vector fonts are allowed; otherwise, false. The default value is true.</returns>
        [DefaultValue(true)]
        public bool AllowVectorFonts { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box displays both vertical and horizontal fonts or only
        ///     horizontal fonts.
        /// </summary>
        /// <returns>true if both vertical and horizontal fonts are allowed; otherwise, false. The default value is true.</returns>
        [DefaultValue(true)]
        public bool AllowVerticalFonts { get; set; }

        /// <summary>
        ///     Gets or sets the selected font color.
        /// </summary>
        /// <returns>The color of the selected font. The default value is System.Drawing.Color.Black.</returns>
        [DefaultValue(typeof(Color), "Black")]
        public Color Color { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box allows only the selection of fixed-pitch fonts.
        /// </summary>
        /// <returns>true if only fixed-pitch fonts can be selected; otherwise, false. The default value is false.</returns>
        [DefaultValue(false)]
        public bool FixedPitchOnly { get; set; }

        /// <summary>
        ///     Gets or sets the selected font.
        /// </summary>
        /// <returns>The selected font.</returns>
        public Font Font { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box specifies an error condition if the user attempts to select
        ///     a font or style that does not exist.
        /// </summary>
        /// <returns>
        ///     true if the dialog box specifies an error condition when the user tries to select a font or style that does
        ///     not exist; otherwise, false. The default is false.
        /// </returns>
        [DefaultValue(false)]
        public bool FontMustExist { get; set; }

        /// <summary>
        ///     Gets or sets the maximum point size a user can select.
        /// </summary>
        /// <returns>The maximum point size a user can select. The default is 0.</returns>
        [DefaultValue(0)]
        public int MaxSize { get; set; }

        /// <summary>
        ///     Gets or sets the minimum point size a user can select.
        /// </summary>
        /// <returns>The minimum point size a user can select. The default is 0.</returns>
        [DefaultValue(0)]
        public int MinSize { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box allows selection of fonts for all non-OEM and Symbol
        ///     character sets, as well as the ANSI character set.
        /// </summary>
        /// <returns>
        ///     true if selection of fonts for all non-OEM and Symbol character sets, as well as the ANSI character set, is
        ///     allowed; otherwise, false. The default value is false.
        /// </returns>
        [DefaultValue(false)]
        public bool ScriptsOnly { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box contains an Apply button.
        /// </summary>
        /// <returns>true if the dialog box contains an Apply button; otherwise, false. The default value is false.</returns>
        [DefaultValue(false)]
        public bool ShowApply { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box displays the color choice.
        /// </summary>
        /// <returns>true if the dialog box displays the color choice; otherwise, false. The default value is false.</returns>
        [DefaultValue(false)]
        public bool ShowColor { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the dialog box contains controls that allow the user to specify
        ///     strikethrough, underline, and text color options.
        /// </summary>
        /// <returns>
        ///     true if the dialog box contains controls to set strikethrough, underline, and text color options; otherwise,
        ///     false. The default value is true.
        /// </returns>
        [DefaultValue(true)]
        public bool ShowEffects { get; set; }

        /// <summary>
        ///     Occurs when the user clicks the Apply button in the font dialog box.
        /// </summary>
        public event EventHandler Apply;

        /// <summary>
        ///     Raises the <see cref="Apply" /> event.
        /// </summary>
        /// <param name="sender">The sender dialog.</param>
        /// <param name="e">Empty.</param>
        public void OnApply(object sender, EventArgs e)
        {
            Apply?.Invoke(sender, e);
        }
    }
}