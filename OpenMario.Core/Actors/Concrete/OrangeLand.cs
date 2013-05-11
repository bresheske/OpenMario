using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMario.Core.Actors.Concrete
{
    public class OrangeLand : StaticBox
    {
        private Bitmap _drawable;

        public override void Load(Environment.Environment env)
        {
            Environment = env;
            _drawable = (Bitmap)Image.FromFile("assets/land.png");
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            var curw = pos.X;
            while (curw < Width)
            {
                g.DrawImage(_drawable, (int)curw, (int)pos.Y);
                curw += _drawable.Width;
            }
        }
    }
}