//-----------------------------------------------------------------------
// <copyright file="Goomba.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Contains all of the logic for interacting and managing the Goomba actor. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Actors.Concrete
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;

    /// <summary>
    /// Class for the <see cref="Goomba"/> actor.
    /// </summary>
    public class Goomba : GravityActor
    {
        /// <summary>
        /// Initializes the _drawable variable to the System.Drawing.Bitmap allowing us to draw on it later in the class.
        /// This instance is to draw the Goomba moving left.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private Bitmap drawableLeft;

        /// <summary>
        /// Initializes the _drawable variable to the System.Drawing.Bitmap allowing us to draw on it later in the class.
        /// Bitmap for drawing the Goomba moving right.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private Bitmap drawableRight;

        /// <summary>
        /// /// Initializes the _drawable variable to the System.Drawing.Bitmap allowing us to draw on it later in the class.
        /// Bitmap for drawing the current movement of the Goomba.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private Bitmap drawableCurrent;

        /// <summary>
        /// Initializes the _drawable variable to the System.Drawing.Bitmap allowing us to draw on it later in the class.
        /// Used to swap the drawables for the Goomba.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private Stopwatch timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Goomba"/> class.
        /// </summary>
        public Goomba() 
        {
            this.WalkingVelocity = new VectorClass.Vector2D_Dbl(-1, 0);
            this.Width = 30;
            this.Height = 30;
        }

        /// <summary>
        /// Gets or sets the walking velocity.
        /// </summary>
        public VectorClass.Vector2D_Dbl WalkingVelocity { get; set; }

        /// <summary>
        /// Draws the cloud into the environment based on the relative position.
        /// </summary>
        /// <param name="g">System Graphics <see cref="Graphics"/></param>
        public override void Draw(Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            g.DrawImage(this.drawableCurrent, (int)pos.X, (int)pos.Y);
        }

        /// <summary>
        /// Loads the cloud into the environment and defines the size and image for the actor.
        /// </summary>
        /// <param name="env">Base Environment <see cref="Environment"/></param>
        public override void Load(Environment.Environment env)
        {
            this.Environment = env;
            this.drawableRight = (Bitmap)Image.FromFile(@"Assets\goombar.png");
            this.drawableRight = new Bitmap(this.drawableRight, new Size(Width, Height));
            this.drawableLeft = (Bitmap)Image.FromFile(@"Assets\goombal.png");
            this.drawableLeft = new Bitmap(this.drawableLeft, new Size(Width, Height));
            this.drawableCurrent = this.drawableRight;
            this.timer = new Stopwatch();
            this.timer.Start();
        }

        /// <summary>
        /// Override of <see cref="BaseActor"/> Update.
        /// Controls what the Goomba is doing on the screen at any given time.
        /// </summary>
        /// <param name="loadedactors">List of Base Actors to Update</param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public override void Update(List<BaseActor> loadedactors)
        {
            // Gravity.
            base.Update(loadedactors);

            // Drawable
            if (this.timer.ElapsedMilliseconds > 500)
            {
                // Swap drawables.
                this.drawableCurrent = this.drawableCurrent == this.drawableLeft ? this.drawableRight : this.drawableLeft;

                this.timer.Reset();
                this.timer.Start();
            }

            // Walk
            this.Position += this.WalkingVelocity;

            // If bump into Left or Right, Turn around.
            if (Physics.Physics.IsActorPushingAnotherFromLeft(this, loadedactors)
                || Physics.Physics.IsActorPushingAnotherFromRight(this, loadedactors))
            {
                this.Velocity = new VectorClass.Vector2D_Dbl(this.WalkingVelocity.X * -1, Velocity.Y);
                this.WalkingVelocity = new VectorClass.Vector2D_Dbl(this.WalkingVelocity.X * -1, this.WalkingVelocity.Y);
            }

            // If we got stomped on, we'll need to die.
            if (Physics.Physics.IsActorPushingAnotherFromBottom(this, loadedactors))
            {
                // Queue the removal. 
                Environment.ActorsToRemove.Add(this);
            }

            // Normalize Velocities to only allow maximum speeds.
            Physics.Physics.NormalizeVelocity(this);

            Physics.Physics.BlockAllCollisions(this, loadedactors);
        }
    }
}