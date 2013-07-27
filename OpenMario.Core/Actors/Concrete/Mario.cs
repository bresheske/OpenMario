//-----------------------------------------------------------------------
// <copyright file="Mario.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Code containing all of the interactions with Mario. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Actors.Concrete
{
    using System.Collections.Generic;
    using System.Drawing;
    using OpenMario.Core.Actors.Sprites;
    using OpenMario.Core.Players;
    using OpenMario.Core.Players.Actions;
    using VectorClass;

    /// <summary>
    /// This class will handle Mario himself.
    /// </summary>
    public class Mario : GravityActor
    {
        /// <summary>
        /// Creates the player BasePlayer object.
        /// </summary>
        private readonly BasePlayer player;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mario"/> class.
        /// </summary>
        /// <param name="player">BasePlayer player</param>
        public Mario(BasePlayer player)
        {
            this.Width = 22;
            this.Height = 26;
            this.Position = new Vector2D_Dbl(100, 200);
            this.player = player;
            this.EnvironmentEffect = EnvironmentEffectType.ControlsViewportScroll;
            this.IsAlive = true;
            this.SpriteManager = new MarioSpriteManager(this, player);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the IsAlive state of the Mario object.
        /// TODO: Shouldn't this be available to all actors? Should this be moved to <see cref="BaseActor"/>?
        /// </summary>
        public bool IsAlive { get; set; }

        /// <summary>
        /// Override of <see cref="BaseActor"/> Update.
        /// Controls what Mario is doing on the screen at any given time.
        /// </summary>
        /// <param name="loadedactors">List of Base Actors to Update</param>
        public override void Update(List<BaseActor> loadedactors)
        {
            // Perform Gravity Updates.
            base.Update(loadedactors);

            // Perform Jumps
            if (this.player.IsActionPressed(new KeyMapping { Action = KeyMapping.KeyAction.JUMP }))
            {
                if (Physics.Physics.IsActorStandingOnAnother(this, loadedactors))
                {
                    this.Velocity += new Vector2D_Dbl(0, Physics.Physics.MaxJumpSpeed);
                }
            }

            // Perform Left/Right Velocity Updates.
            if (this.player.IsActionPressed(new KeyMapping { Action = KeyMapping.KeyAction.LEFT }))
            {
                this.Velocity += new Vector2D_Dbl(Physics.Physics.MovementDelta, 0);
            }
            else if (this.player.IsActionPressed(new KeyMapping { Action = KeyMapping.KeyAction.RIGHT }))
            {
                this.Velocity += new Vector2D_Dbl(-Physics.Physics.MovementDelta, 0);
            }
            else
            {
                Physics.Physics.ApplyGroundFriction(this, loadedactors);
            }
            
            // Check if we need to die.
            if (Position.Y > Environment.Height)
            {
                this.IsAlive = false;
            }

            // Normalize Velocities to only allow maximum speeds.
            Physics.Physics.NormalizeVelocity(this);

            // Block all Collisions with 'Block' set.
            Physics.Physics.BlockAllCollisions(this, loadedactors);

            // Update Sprite Manager
            SpriteManager.Update(loadedactors);
        }

        /// <summary>
        /// Loads the environment that the actor is moving around in.
        /// </summary>
        /// <param name="env"><see cref="Environment.Environment"/> that should be loaded.</param>
        public override void Load(Environment.Environment env)
        {
            this.Environment = env;
            SpriteManager.Load();
        }

        /// <summary>
        /// Draws the actors relative position on the screen.
        /// </summary>
        /// <param name="g"><see cref="Graphics"/> that should be drawn on.</param>
        public override void Draw(Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            g.DrawImage(SpriteManager.CurrentSprite, (int)pos.X, (int)pos.Y);

            // g.DrawRectangle(new Pen(Brushes.Blue), new Rectangle((int)pos.X, (int)pos.Y, Width, Height));
        }
    }
}