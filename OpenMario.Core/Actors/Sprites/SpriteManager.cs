//-----------------------------------------------------------------------
// <copyright file="SpriteManager.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Contains the base methods for the Sprite Manager class. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Actors.Sprites
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;

    /// <summary>
    /// The sprite manager.
    /// </summary>
    public abstract class SpriteManager
    {
        /// <summary>
        /// The actor in which this sprite is currently dealing with.
        /// </summary>
        protected readonly BaseActor Actor;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteManager"/> class.
        /// </summary>
        /// <param name="a">
        /// The Base Actor
        /// </param>
        protected SpriteManager(BaseActor a)
        {
            this.Actor = a;
        }

        /// <summary>
        /// Gets or sets the current sprite.
        /// </summary>
        public Bitmap CurrentSprite { get; protected set; }

        /// <summary>
        /// Gets or sets the sprite width.
        /// </summary>
        public int SpriteWidth { get; protected set; }

        /// <summary>
        /// Gets or sets the sprite height.
        /// </summary>
        public int SpriteHeight { get; protected set; }

        /// <summary>
        /// The load method for the Sprite Manager.
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// The update method for the Sprite Manager.
        /// </summary>
        /// <param name="loadedactors"> The loadedactors. </param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public abstract void Update(List<BaseActor> loadedactors);
    }
}
