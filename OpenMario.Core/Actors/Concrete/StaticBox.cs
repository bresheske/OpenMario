//-----------------------------------------------------------------------
// <copyright file="StaticBox.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Defines the base class for static boxes. This will likely be used for ? boxes and bricks. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Actors.Concrete
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// The static box.
    /// </summary>
    public class StaticBox : BaseActor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticBox"/> class.
        /// </summary>
        public StaticBox()
        {
            this.Position = new VectorClass.Vector2D_Dbl(0, 0);
            this.CollisionAction = CollisionType.Block;
        }

        /// <summary>
        /// Overrides the base method of Update in <see cref="BaseActor"/>
        /// </summary>
        /// <param name="loadedactors">List of <c>BaseActor</c></param>
        public override void Update(List<BaseActor> loadedactors)
        {
            this.Position += this.Velocity;
        }

        /// <summary>
        /// Overrides the base method of Load in <see cref="BaseActor"/>
        /// </summary>
        /// <param name="env"><c>Environment.Environment</c></param>
        public override void Load(Environment.Environment env)
        {
            this.Environment = env;
        }

        /// <summary>
        /// The draw for the <see cref="StaticBox"/>
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> for <see cref="StaticBox"/></param>
        public override void Draw(Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            g.FillRectangle(Brushes.SeaGreen, new Rectangle((int)pos.X, (int)pos.Y, Width, Height));
        }
    }
}