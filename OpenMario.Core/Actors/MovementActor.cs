//-----------------------------------------------------------------------
// <copyright file="MovementActor.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Handles the movements of the actors. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Actors
{
    using System.Collections.Generic;

    /// <summary>
    /// The movement actor.
    /// </summary>
    public abstract class MovementActor : BaseActor
    {
        /// <summary>
        /// Abstraction of the <see cref="BaseActor"/> Move method.
        /// </summary>
        public abstract void Move();

        /// <summary>
        /// Abstract override of the <see cref="BaseActor"/> Load method.
        /// </summary>
        /// <param name="env"><c>Environment.Environment</c></param>
        public abstract override void Load(Environment.Environment env);

        /// <summary>
        /// Abstract override of the <see cref="BaseActor"/> Draw method
        /// </summary>
        /// <param name="g"><c>System.Drawing.Graphics</c></param>
        public abstract override void Draw(System.Drawing.Graphics g);

        /// <summary>
        /// Override of the <see cref="BaseActor"/> Update method.
        /// </summary>
        /// <param name="loadedactors"><c>List(BaseActor)</c></param>
        public override void Update(List<BaseActor> loadedactors)
        {
            this.Move();
        }
    }
}