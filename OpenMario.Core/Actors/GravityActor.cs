//-----------------------------------------------------------------------
// <copyright file="GravityActor.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Base class for the gravity physics for Actors. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Actors
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    ///     Base class for gravity in regards to the actors.
    /// </summary>
    public abstract class GravityActor : MovementActor
    {
        /// <summary>
        ///     Override of the of <see cref="BaseActor" /> Load method.
        /// </summary>
        /// <param name="env">
        ///     <see cref="Environment" />
        /// </param>
        public abstract override void Load(Environment.Environment env);

        /// <summary>
        ///     Override of the <see cref="BaseActor" /> Draw method.
        /// </summary>
        /// <param name="g">System.Drawing.Graphics</param>
        public abstract override void Draw(Graphics g);

        /// <summary>
        ///     Override of the <see cref="BaseActor" /> Move method.
        ///     Method is manipulated by controlling the gravity on the Actor's Y plane.
        /// </summary>
        public override void Move()
        {
            if (Velocity.Y < Physics.Physics.MAX_GRAVITY.Y)
            {
                this.Velocity -= Physics.Physics.GRAVITY;
            }
            else if (Velocity.Y > Physics.Physics.MAX_GRAVITY.Y)
            {
                this.Velocity += Physics.Physics.GRAVITY;
            }

            this.Position -= this.Velocity;
        }

        /// <summary>
        ///     Override of the <see cref="BaseActor" /> Update method
        /// </summary>
        /// <param name="loadedactors">
        ///     List of <c>BaseActor</c>
        /// </param>
        public override void Update(List<BaseActor> loadedactors)
        {
            this.Move();
        }
    }
}