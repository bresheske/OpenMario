//-----------------------------------------------------------------------
// <copyright file="QuestionBox.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Contains all of the logic for drawing and interacting with the Question Box </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Actors.Concrete
{
    using System.Drawing;

    public class QuestionBox : StaticBox
    {
        private Bitmap _drawable;

        public QuestionBox()
        {
            Width = 30;
            Height = 30;
        }

        public override void Load(Environment.Environment env)
        {
            Environment = env;
            _drawable = (Bitmap)Image.FromFile("Assets/questionblock.png");
            _drawable = new Bitmap(_drawable, new Size(Width, Height));
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            g.DrawImage(_drawable, (int)pos.X, (int)pos.Y);
        }
    }
}
