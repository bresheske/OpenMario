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

    /// <summary>
    /// The green static pipe.
    /// </summary>
    public class GreenStaticPipe : StaticBox
    {
        /// <summary>
        /// The <see cref="Bitmap"/> object for <see cref="GreenStaticPipe"/>
        /// </summary>
        private Bitmap drawable;

        /// <summary>
        /// Initializes a new instance of the <see cref="GreenStaticPipe"/> class.
        /// </summary>
        public GreenStaticPipe() 
        {
            this.Width = 40;
            this.Height = 60;
        }

        /// <summary>
        /// The drawing method for <see cref="GreenStaticPipe"/>
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> for <see cref="GreenStaticPipe"/></param>
        public override void Draw(Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            g.DrawImage(this.drawable, (int)pos.X, (int)pos.Y);
        }

        /// <summary>
        /// The loading method for <see cref="GreenStaticPipe"/>
        /// </summary><param name="env">The <see cref="Environment"/> to load for <see cref="GreenStaticPipe"/>.</param>
        public override void Load(Environment.Environment env)
        {
            this.Environment = env;
            this.drawable = (Bitmap)Image.FromFile(@"Assets\pipe.png");
            this.drawable = new Bitmap(this.drawable, new Size(Width, Height));
        }
    }
}
