using OpenMario.Core.Players;
using System.Collections.Generic;
using System.Drawing;
using OpenMario.Core.Players.Actions;
using VectorClass;

namespace OpenMario.Core.Actors.Concrete
{
    public class Mario : GravityActor
    {
        private BasePlayer _player;
        private Bitmap _drawable;

        public Mario(BasePlayer player)
        {
            Width = 30;
            Height = 40;
            Position = new Vector2D_Dbl(200, 0);
            _player = player;
        }

        public override void Update(List<BaseActor> loadedactors)
        {
            //Perform Gravity Updates.
            base.Update(loadedactors);

            //Perform Jumps
            if (_player.IsActionPressed(new KeyMapping(){ Action = KeyMapping.KeyAction.JUMP }))
            {
                if (Physics.Physics.IsActorStandingOnAnother(this, loadedactors))
                    Velocity += new Vector2D_Dbl(0, Physics.Physics.MAX_JUMP_SPEED);
            }

            //Perform Left/Right Velocity Updates.
            if (_player.IsActionPressed(new KeyMapping(){ Action = KeyMapping.KeyAction.LEFT }))
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
            
            //Normalize Velocities to only allow maximum speeds.
            Physics.Physics.NormalizeVelocity(this);

            //Block all Collisions with 'Block' set.
            Physics.Physics.BlockAllCollisions(this, loadedactors);
        }

        public override void Load()
        {
            _drawable = (Bitmap)Image.FromFile(@"Assets\mario.png");
            _drawable = new Bitmap(_drawable, new Size(Width, Height));
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(_drawable, (int)Position.X, (int)Position.Y);
        }
    }
}