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

    public class StaticBox : BaseActor
    {
        /// <summary>
        /// Intializes a new instance of the <see cref="StaticBox"/> class.
        /// </summary>
        public StaticBox()
        {
            Position = new VectorClass.Vector2D_Dbl(0, 0);
            CollisionAction = CollisionType.BLOCK;
        }

        /// <summary>
        /// Overrides the base method of Update in <see cref="BaseActor"/>
        /// </summary>
        /// <param name="loadedactors">List of <c>BaseActor</c></param>
        public override void Update(List<BaseActor> loadedactors)
        {
            Position += Velocity;
        }

        /// <summary>
        /// Overrides the base method of Load in <see cref="BaseActor"/>
        /// </summary>
        /// <param name="env"><c>Environment.Environment</c></param>
        public override void Load(Environment.Environment env)
        {
            Environment = env;
        }

        /// <summary>
        /// Overrides the base method of Draw in <see cref="BaseActor"/>
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            g.FillRectangle(Brushes.SeaGreen, new Rectangle((int)pos.X, (int)pos.Y, Width, Height));
        }
    }
}