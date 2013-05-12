using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMario.Core.Actors.Concrete
{
    public class GreenStaticPipe : StaticBox
    {
        private Bitmap _drawable;

        public GreenStaticPipe() 
        {
            Width = 40;
            Height = 60;
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            g.DrawImage(_drawable, (int)pos.X, (int)pos.Y);
        }

        public override void Load(Environment.Environment env)
        {
            Environment = env;
            _drawable = (Bitmap)Image.FromFile(@"Assets\pipe.png");
            _drawable = new Bitmap(_drawable, new Size(Width, Height));
        }
    }
}
