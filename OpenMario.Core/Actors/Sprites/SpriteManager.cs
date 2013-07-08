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
    using System.Drawing;

    public abstract class SpriteManager
    {
        protected readonly BaseActor _actor;

        public SpriteManager(BaseActor a)
        {
            _actor = a;
        }

        public Bitmap CurrentSprite { get; protected set; }
        public int SpriteWidth { get; protected set; }
        public int SpriteHeight { get; protected set; }

        public abstract void Load();
        public abstract void Update(List<BaseActor> loadedactors);
    }
}
