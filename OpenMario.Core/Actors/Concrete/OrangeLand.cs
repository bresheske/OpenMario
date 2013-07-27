//-----------------------------------------------------------------------
// <copyright file="OrangeLand.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Defines the "dirt" that Mario is walking on. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Actors.Concrete
{
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;

    /// <summary>
    /// This is the ground the Actors are walking on.
    /// </summary>
    public class OrangeLand : StaticBox
    {
        /// <summary>
        /// The drawable 
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private Bitmap drawable;

        /// <summary>
        /// Override of <see cref="BaseActor"/> Update.
        /// The load method for <see cref="OrangeLand"/>
        /// Controls what the Actor is doing at any given time.
        /// Given this is a static box it doesn't actually /do/ anything.
        /// </summary>
        /// <param name="env">The environment to load.</param>
        public override void Load(Environment.Environment env)
        {
            this.Environment = env;
            this.drawable = (Bitmap)Image.FromFile("assets/land.png");
        }

        /// <summary>
        /// The drawable object for the actor.
        /// </summary>
        /// <param name="g">The graphics to draw</param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public override void Draw(Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            var curw = pos.X;
            while (curw < this.Width)
            {
                g.DrawImage(this.drawable, (int)curw, (int)pos.Y);
                curw += this.drawable.Width;
            }
        }
    }
}