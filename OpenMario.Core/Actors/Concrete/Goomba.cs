//-----------------------------------------------------------------------
// <copyright file="Goomba.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Contains all of the logic for interacting and managing the Goomba actor. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Actors.Concrete
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;

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
            this._drawableright = (Bitmap)Image.FromFile(@"Assets\goombar.png");
            this._drawableright = new Bitmap(this._drawableright, new Size(Width, Height));
            this._drawableleft = (Bitmap)Image.FromFile(@"Assets\goombal.png");
            this._drawableleft = new Bitmap(this._drawableleft, new Size(Width, Height));
            this._drawablecurrent = this._drawableright;
            this._timer = new Stopwatch();
            this._timer.Start();
        }

        public override void Update(List<BaseActor> loadedactors)
        {
            // Gravity.
            base.Update(loadedactors);

            // Drawable
            if (_timer.ElapsedMilliseconds > 500)
            {
                // Swap drawables.
                _drawablecurrent = _drawablecurrent == _drawableleft ? _drawableright : _drawableleft;

                _timer.Reset();
                _timer.Start();
            }

            // Walk
            this.Position += WalkingVelocity;

            // If bump into Left or Right, Turn around.
            if (Physics.Physics.IsActorPushingAnotherFromLeft(this, loadedactors)
                || Physics.Physics.IsActorPushingAnotherFromRight(this, loadedactors))
            {
                this.Velocity = new VectorClass.Vector2D_Dbl(WalkingVelocity.X * -1, Velocity.Y);
                this.WalkingVelocity = new VectorClass.Vector2D_Dbl(WalkingVelocity.X * -1, WalkingVelocity.Y);
            }

            // If we got stomped on, we'll need to die.
            if (Physics.Physics.IsActorPushingAnotherFromBottom(this, loadedactors))
            {
                // Queue the removal. 
                Environment.ActorsToRemove.Add(this);
            }

            // Normalize Velocities to only allow maximum speeds.
            Physics.Physics.NormalizeVelocity(this);

            Physics.Physics.BlockAllCollisions(this, loadedactors);
        }
    }
}