//-----------------------------------------------------------------------
// <copyright file="OrangeLand.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Defines the "dirt" that Mario is walking on. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Actors.Concrete
{
    using System.Drawing;

    public class OrangeLand : StaticBox
    {
        private Bitmap _drawable;

        public override void Load(Environment.Environment env)
        {
            Environment = env;
            _drawable = (Bitmap)Image.FromFile("assets/land.png");
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            var curw = pos.X;
            while (curw < Width)
            {
                g.DrawImage(_drawable, (int)curw, (int)pos.Y);
                curw += _drawable.Width;
            }
        }
    }
}