//-----------------------------------------------------------------------
// <copyright file="StaticTransparentBox.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Sets the properties on a StaticTransparentBox. Which is to say the box does nothing. :) </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Actors.Concrete
{
    /// <summary>
    /// Sets the properties on a StaticTransparentBox. Which is to say the box does nothing. :)
    /// </summary>
    public class StaticTransparentBox : StaticBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticTransparentBox"/> class. 
        /// This sets the StaticTransparentBox to <c>collisiontype.noaction</c>;
        /// </summary>
        public StaticTransparentBox()
        {
            // Not collidable.
            this.CollisionAction = CollisionType.NoAction;
        }
    }
}