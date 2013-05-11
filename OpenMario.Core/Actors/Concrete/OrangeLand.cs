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

        public override void Load()
        {
            _drawable = (Bitmap)Image.FromFile("assets/land.png");
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            var curw = 0;
            while (curw < Width)
            {
                g.DrawImage(_drawable, (int)curw, (int)Position.Y);
                curw += _drawable.Width;
            }
        }
    }
}