using OpenMario.Core.Players;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Position = new VectorClass.Vector2D_Int(200, 0);

            _player = player;
            _player.OnKeyDown += KeyAction;
        }

        private void KeyAction(Object o, OpenMario.Core.Players.Actions.KeyEventArgs e)
        {
            if (e.KeyMapping.Action == Players.Actions.KeyMapping.KeyAction.JUMP)
            {
                //Do a little jumpyjumpy
                Velocity += new VectorClass.Vector2D_Int(0, 15);
            }
        }

        public override void Update(List<BaseActor> loadedactors)
        {
            //Perform Gravity Updates.
            base.Update(loadedactors);
            //Check for collision
            var collisions = Physics.Physics.GetAllCollisions(loadedactors, this);
            //TODO: Currently just assumes Top-Level collision.
            foreach (var c in collisions)
                if (c.CollisionAction == CollisionType.BLOCK)
                {
                    Position = new VectorClass.Vector2D_Int(Position.X, c.Position.Y - Height);
                    Velocity = new VectorClass.Vector2D_Int(0, 0);
                }
        }

        public override void Load()
        {
            _drawable = (Bitmap)Image.FromFile(@"Assets\mario.png");
            _drawable = new Bitmap(_drawable, new Size(Width, Height));
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            g.DrawImage(_drawable, Position.X, Position.Y);
        }
    }
}