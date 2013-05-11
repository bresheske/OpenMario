using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMario.Core.Actors.Concrete
{
    public class Cloud : StaticTransparentBox
    {
        private Bitmap _drawable;

        public Cloud()
        {
            Position = new VectorClass.Vector2D_Dbl(0, 0);
        }

        public override void Load(Environment.Environment env)
        {
            Width = env.Width;
            Height = env.Height;
            Environment = env;
            _drawable = (Bitmap)Image.FromFile(@"Assets\cloudbg.jpg");
            _drawable = new Bitmap(_drawable, new Size(500, 280));
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);

            for (var curw = pos.X; curw < Width; curw += _drawable.Width)
                for (var curh = pos.Y; curh < Height; curh += _drawable.Height)
                    g.DrawImage(_drawable, (int)curw, (int)curh);
        }
    }
}
