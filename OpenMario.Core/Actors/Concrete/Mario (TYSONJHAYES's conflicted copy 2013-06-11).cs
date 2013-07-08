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

    public class Mario : GravityActor
    {
        private BasePlayer _player;

        public bool IsAlive { get; set; }

        public Mario(BasePlayer player)
        {
            Width = 22;
            Height = 26;
            Position = new Vector2D_Dbl(100, 200);
            _player = player;
            EnvironmentEffect = EnvironmentEffectType.CONTROLS_VIEWPORT_SCROLL;
            IsAlive = true;
            SpriteManager = new MarioSpriteManager(this, player);
        }

        public override void Update(List<BaseActor> loadedactors)
        {
            // Perform Gravity Updates.
            base.Update(loadedactors);

            // Perform Jumps
            if (_player.IsActionPressed(new KeyMapping() { Action = KeyMapping.KeyAction.JUMP }))
            {
                if (Physics.Physics.IsActorStandingOnAnother(this, loadedactors))
                    Velocity += new Vector2D_Dbl(0, Physics.Physics.MAX_JUMP_SPEED);
            }

            // Perform Left/Right Velocity Updates.
            if (_player.IsActionPressed(new KeyMapping() { Action = KeyMapping.KeyAction.LEFT }))
            {
                Velocity += new Vector2D_Dbl(Physics.Physics.MOVEMENT_DELTA, 0);
            }
            else if (_player.IsActionPressed(new KeyMapping() { Action = KeyMapping.KeyAction.RIGHT }))
            {
                Velocity += new Vector2D_Dbl(-Physics.Physics.MOVEMENT_DELTA, 0);
            }
            else
            {
                Physics.Physics.ApplyGroundFriction(this, loadedactors);
            }
            
            // Check if we need to die.
            if (Position.Y > Environment.Height)
            {
                IsAlive = false;
            }

            // Normalize Velocities to only allow maximum speeds.
            Physics.Physics.NormalizeVelocity(this);

            // Block all Collisions with 'Block' set.
            Physics.Physics.BlockAllCollisions(this, loadedactors);

            // Update Sprite Manager
            SpriteManager.Update(loadedactors);
        }

        public override void Load(Environment.Environment env)
        {
            Environment = env;
            SpriteManager.Load();
        }

        public override void Draw(Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            g.DrawImage(SpriteManager.CurrentSprite, (int)pos.X, (int)pos.Y);
            // g.DrawRectangle(new Pen(Brushes.Blue), new Rectangle((int)pos.X, (int)pos.Y, Width, Height));
        }
    }
}