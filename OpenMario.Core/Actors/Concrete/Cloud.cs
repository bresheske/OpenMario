//-----------------------------------------------------------------------
// <copyright file="Cloud.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Code for the cloud actor.  Inherits from the StaticTransparentBox class. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Actors.Concrete
{
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Drawing.Imaging;

    /// <summary>
    /// Code for the Actor 'Cloud.' 
    /// </summary>
    public class Cloud : StaticTransparentBox
    {
        /// <summary>
        /// Initializes the drawable variable to the System.Drawing.Bitmap allowing us to draw on it later in the class.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private Bitmap drawable;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cloud"/> class.
        /// </summary>
        public Cloud()
        {
            this.Position = new VectorClass.Vector2D_Dbl(0, 0);
        }

        /// <summary>
        /// Loads the cloud into the environment and defines the size and image for the actor.
        /// </summary>
        /// <param name="env">Base Environment <see cref="Environment"/></param>
        public override void Load(Environment.Environment env)
        {
            this.Width = env.Width;
            this.Height = env.Height;
            this.Environment = env;
            var row = 500;
            var columns = 280;
            Bitmap B = new Bitmap(row, columns);
            for (int i = 0; i < row; i++)
                for (int j = 0; j < columns; j++)
                    B.SetPixel(i, j, Color.CornflowerBlue);
            this.drawable = B;
            //Bitmap B = new Bitmap(row, columns);
            //Graphics G = Graphics.FromImage(B);
            //G.FillRectangle(Brushes.Blue, 0, 0, row, columns);
            //this.drawable = new Bitmap(row, columns, G);
        }

        /// <summary>
        /// Draws the cloud into the environment based on the relative position.
        /// </summary>
        /// <param name="g">System Graphics <see cref="Graphics"/></param>
        public override void Draw(Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);

            for (var curw = pos.X; curw < this.Width; curw += this.drawable.Width)
            {
                for (var curh = pos.Y; curh < this.Height; curh += this.drawable.Height)
                {
                    g.DrawImage(this.drawable, (int)curw, (int)curh);
                }
            }
        }
    }
}
