using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMario.Core.Actors.Concrete
{
    public class QuestionBox : StaticBox
    {
        private Bitmap _drawable;

        public QuestionBox()
        {
            Width = 30;
            Height = 30;
        }

        public override void Load(Environment.Environment env)
        {
            Environment = env;
            _drawable = (Bitmap)Image.FromFile("Assets/questionblock.png");
            _drawable = new Bitmap(_drawable, new Size(Width, Height));
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            g.DrawImage(_drawable, (int)pos.X, (int)pos.Y);
        }
    }
}
