//-----------------------------------------------------------------------
// <copyright file="KeyEventArgs.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Sets the event args for KeyMappings. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Players.Actions
{
    using System;

    /// <summary>
    /// Allows us to set the even args for the KeyMappings variable.
    /// </summary>
    public class KeyEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the KeyMapping field.
        /// </summary>
        public KeyMapping KeyMapping { get; set; }
    }
}
