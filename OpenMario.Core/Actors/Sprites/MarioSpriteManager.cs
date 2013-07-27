//-----------------------------------------------------------------------
// <copyright file="MarioSpriteManager.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Contains the logic for managing Mario's sprites. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Actors.Sprites
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using OpenMario.Core.Extensions;
    using OpenMario.Core.Players;
    using OpenMario.Core.Players.Actions;

    /// <summary>
    /// The mario sprite manager.
    /// </summary>
    public class MarioSpriteManager : SpriteManager
    {
        #region Constants
        /// <summary>
        /// The small mario width.
        /// </summary>
        public const int SmallMarioWidth = 15;

        /// <summary>
        /// The small mario height.
        /// </summary>
        public const int SmallMarioHeight = 20;

        /// <summary>
        /// The small mario standing left.
        /// </summary>
        private readonly Rectangle smallMarioStandingLeft = new Rectangle(168, 0, SmallMarioWidth, SmallMarioHeight);

        /// <summary>
        /// The small mario standing right.
        /// </summary>
        private readonly Rectangle smallMarioStandingRight = new Rectangle(209, 0, SmallMarioWidth, SmallMarioHeight);

        /// <summary>
        /// The player.
        /// </summary>
        private readonly BasePlayer player;
        #endregion

        /// <summary>
        /// The fullmap.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private Bitmap fullmap;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarioSpriteManager"/> class.
        /// </summary>
        /// <param name="a">The Base Actor.</param>
        /// <param name="p">The Base Player.</param>
        public MarioSpriteManager(BaseActor a, Players.BasePlayer p) : base(a)
        {
            this.player = p;
        }

        /// <summary>
        /// The load method for Mario Sprite Manager.
        /// </summary>
        public override void Load()
        {
            this.fullmap = (Bitmap)Image.FromFile(@"Assets\mariosheet.png");
        }

        /// <summary>
        /// The update method for Mario Sprite Manager
        /// </summary>
        /// <param name="loadedactors"> The loadedactors.  </param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public override void Update(List<BaseActor> loadedactors)
        {
            if (this.CurrentSprite == null)
            {
                this.CurrentSprite = new Bitmap(this.fullmap.CropImage(this.smallMarioStandingLeft), this.Actor.Width, this.Actor.Height);   
            }

            if (this.player.IsActionPressed(new KeyMapping() { Action = KeyMapping.KeyAction.LEFT }))
            {
                this.CurrentSprite = this.GetCrop(this.smallMarioStandingLeft);
            }
            else if (this.player.IsActionPressed(new KeyMapping() { Action = KeyMapping.KeyAction.RIGHT }))
            {
                this.CurrentSprite = this.GetCrop(this.smallMarioStandingRight);
            }
        }

        /// <summary>
        /// The get crop method used to crop the image to around the actor.
        /// </summary>
        /// <param name="crop">The <see cref="Rectangle"/> to be cropped. </param>
        /// <returns> The <see cref="Bitmap"/> cropped. </returns>
        private Bitmap GetCrop(Rectangle crop)
        {
            this.SpriteHeight = crop.Height;
            this.SpriteWidth = crop.Width;
            return new Bitmap(this.fullmap.CropImage(crop), this.Actor.Width, this.Actor.Height);
        }
    }
}