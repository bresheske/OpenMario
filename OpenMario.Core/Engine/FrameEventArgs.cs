//-----------------------------------------------------------------------
// <copyright file="FrameEventArgs.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Arguments for dealing with drawing frames. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Engine
{
    using System;
    using System.Drawing;

    /// <summary>
    /// The frame event args.
    /// </summary>
    public class FrameEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the frame.
        /// </summary>
        public Bitmap Frame { get; set; }
    }
}
