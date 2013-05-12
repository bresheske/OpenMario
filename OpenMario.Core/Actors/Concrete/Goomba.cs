using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMario.Core.Actors.Concrete
{
    public class Goomba : GravityActor
    {
        public Bitmap _drawableleft;
        public Bitmap _drawableright;
        public Bitmap _drawablecurrent;
        public Stopwatch _timer;
        public VectorClass.Vector2D_Dbl WalkingVelocity;

        public Goomba() 
        {
            WalkingVelocity = new VectorClass.Vector2D_Dbl(-1, 0);
            Width = 30;
            Height = 30;
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            g.DrawImage(_drawablecurrent, (int)pos.X, (int)pos.Y);
        }

        public override void Load(Environment.Environment env)
        {
            Environment = env;
            _drawableright = (Bitmap)Image.FromFile(@"Assets\goombar.png");
            _drawableright = new Bitmap(_drawableright, new Size(Width, Height));
            _drawableleft = (Bitmap)Image.FromFile(@"Assets\goombal.png");
            _drawableleft = new Bitmap(_drawableleft, new Size(Width, Height));
            _drawablecurrent = _drawableright;
            _timer = new Stopwatch();
            _timer.Start();
        }

        public override void Update(List<BaseActor> loadedactors)
        {
            //Gravity.
            base.Update(loadedactors);

            //Drawable
            if (_timer.ElapsedMilliseconds > 500)
            {
                //Swap drawables.
                if (_drawablecurrent == _drawableleft)
                    _drawablecurrent = _drawableright;
                else
                    _drawablecurrent = _drawableleft;
                _timer.Reset();
                _timer.Start();
            }

            //Walk
            Position += WalkingVelocity;

            //If bump into Left or Right, Turn around.
            if (Physics.Physics.IsActorPushingAnotherFromLeft(this, loadedactors)
                || Physics.Physics.IsActorPushingAnotherFromRight(this, loadedactors))
            {
                Velocity = new VectorClass.Vector2D_Dbl(WalkingVelocity.X * -1, Velocity.Y);
                WalkingVelocity = new VectorClass.Vector2D_Dbl(WalkingVelocity.X * -1, WalkingVelocity.Y);
            }

            //Normalize Velocities to only allow maximum speeds.
            Physics.Physics.NormalizeVelocity(this);

            Physics.Physics.BlockAllCollisions(this, loadedactors);
        }
    }
}
