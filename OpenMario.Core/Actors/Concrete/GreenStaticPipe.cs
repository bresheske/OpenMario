//-----------------------------------------------------------------------
// <copyright file="GreenStaticPipe.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Contains all of the logic for drawing and interacting with the Green Pipe. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Actors.Concrete
{
    using System.Drawing;

    public class GreenStaticPipe : StaticBox
    {
        private Bitmap _drawable;

        public GreenStaticPipe() 
        {
            Width = 40;
            Height = 60;
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            g.DrawImage(_drawable, (int)pos.X, (int)pos.Y);
        }

        public override void Load(Environment.Environment env)
        {
            Environment = env;
            _drawable = (Bitmap)Image.FromFile(@"Assets\pipe.png");
            _drawable = new Bitmap(_drawable, new Size(Width, Height));
        }
    }
}
