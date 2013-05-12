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

        private Bitmap _drawableright;
        private Bitmap _drawableleft;
        private Bitmap _currbitmap;

        public bool IsAlive { get; set; }

        public Mario(BasePlayer player)
        {
            Width = 30;
            Height = 40;
            Position = new Vector2D_Dbl(200, 0);
            _player = player;
            EnvironmentEffect = EnvironmentEffectType.CONTROLS_VIEWPORT_SCROLL;
            IsAlive = true;
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
                _currbitmap = _drawableleft;
                Velocity += new Vector2D_Dbl(Physics.Physics.MOVEMENT_DELTA, 0);
            }
            else if (_player.IsActionPressed(new KeyMapping() { Action = KeyMapping.KeyAction.RIGHT }))
            {
                _currbitmap = _drawableright;
                Velocity += new Vector2D_Dbl(-Physics.Physics.MOVEMENT_DELTA, 0);
            }
            else
            {
                Physics.Physics.ApplyGroundFriction(this, loadedactors);
            }
            
            //Check if we need to die.
            if (Position.Y > Environment.Height)
            {
                IsAlive = false;
            }

            //Normalize Velocities to only allow maximum speeds.
            Physics.Physics.NormalizeVelocity(this);

            //Block all Collisions with 'Block' set.
            Physics.Physics.BlockAllCollisions(this, loadedactors);
        }

        public override void Load(Environment.Environment env)
        {
            _drawableright = (Bitmap)Image.FromFile(@"Assets\marior.png");
            _drawableright = new Bitmap(_drawableright, new Size(Width, Height));
            _drawableleft = (Bitmap)Image.FromFile(@"Assets\mariol.png");
            _drawableleft = new Bitmap(_drawableleft, new Size(Width, Height));
            _currbitmap = _drawableright;
            Environment = env;
        }

        public override void Draw(Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            g.DrawImage(_currbitmap, (int)pos.X, (int)pos.Y);
        }
    }
}