using OpenMario.Core.Players;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Position = new VectorClass.Vector2D_Int(200, 0);

            _player = player;
            _player.OnKeyDown += KeyAction;
        }

        private void KeyAction(Object o, OpenMario.Core.Players.Actions.KeyEventArgs e)
        {
            if (e.KeyMapping.Action == Players.Actions.KeyMapping.KeyAction.JUMP)
            {
                Velocity += new VectorClass.Vector2D_Int(0, 15);
            }
            else if (e.KeyMapping.Action == Players.Actions.KeyMapping.KeyAction.LEFT)
            {
                Velocity += new VectorClass.Vector2D_Int(1, 0);
            }
            else if (e.KeyMapping.Action == Players.Actions.KeyMapping.KeyAction.RIGHT)
            {
                Velocity += new VectorClass.Vector2D_Int(-1, 0);
            }
        }

        public override void Update(List<BaseActor> loadedactors)
        {
            //Perform Gravity Updates.
            base.Update(loadedactors);
            //Check for collision
            Physics.Physics.BlockAllCollisions(this, loadedactors);
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