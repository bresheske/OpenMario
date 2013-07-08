//-----------------------------------------------------------------------
// <copyright file="SpriteManager.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Declares the abstract class of SpriteManager </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Actors.Sprites
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Declares the abstract class of SpriteManager
    /// </summary>
    public abstract class SpriteManager
    {
        /// <summary>
        /// Initializes a readonly field of BaseActor.
        /// </summary>
        protected readonly BaseActor Actor;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteManager" /> class
        /// </summary>
        /// <param name="a"><c>BaseActor</c></param>
        public SpriteManager(BaseActor a)
        {
            this.Actor = a;
        }

        /// <summary>
        /// Gets or sets the CurrentSprite.
        /// </summary>
        public Bitmap CurrentSprite { get; protected set; }

        /// <summary>
        /// Gets or sets the SpriteWidth.
        /// </summary>
        public int SpriteWidth { get; protected set; }

        /// <summary>
        /// Gets or sets the SpriteHeight
        /// </summary>
        public int SpriteHeight { get; protected set; }

        /// <summary>
        /// Provides the abstract of the Load method.
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Provides the abstract for the Update method.
        /// </summary>
        /// <param name="loadedactors">List of <c>BaseActor</c></param>
        public abstract void Update(List<BaseActor> loadedactors);
    }
}
