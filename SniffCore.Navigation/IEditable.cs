//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Threading.Tasks;

namespace SniffCore.Navigation
{
    /// <summary>
    ///     Represents a ViewModel which can block the navigation with pending changes, input validations and other.
    /// </summary>
    public interface IEditable
    {
        /// <summary>
        ///     The callback called if the user wants to navigate to another user control.
        /// </summary>
        /// <returns>True if the page is save to leave; otherwise False.</returns>
        Task<bool> TryLeave();
    }
}