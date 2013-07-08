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

    public static class DrawingExtensions
    {
        public static Bitmap CropImage(this Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return bmpCrop;
        }
    }
}
