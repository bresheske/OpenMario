//-----------------------------------------------------------------------
// <copyright file="DrawingExtensions.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Methods for manging drawing of the sprites. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Extensions
{
    using System.Drawing;

    /// <summary>
    /// The drawing extensions.
    /// </summary>
    public static class DrawingExtensions
    {
        /// <summary>
        /// The crop image method.
        /// </summary>
        /// <param name="img">
        /// The <see cref="Image"/> to crop.
        /// </param>
        /// <param name="cropArea">
        /// The <see cref="Rectangle "/> crop area.
        /// </param>
        /// <returns>
        /// The <see cref="Bitmap"/> to crop.
        /// </returns>
        public static Bitmap CropImage(this Image img, Rectangle cropArea)
        {
            var bmpImage = new Bitmap(img);
            var bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return bmpCrop;
        }
    }
}
