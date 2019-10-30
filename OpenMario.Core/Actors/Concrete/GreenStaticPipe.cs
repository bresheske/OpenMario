
namespace OpenMario.Core.Actors.Concrete
{
    using System.Drawing;

    /// <summary>
    /// The green static pipe.
    /// </summary>
    public class GreenStaticPipe : StaticBox
    {
        /// <summary>
        /// The <see cref="Bitmap"/> object for <see cref="GreenStaticPipe"/>
        /// </summary>
        private Bitmap drawable;

        /// <summary>
        /// Initializes a new instance of the <see cref="GreenStaticPipe"/> class.
        /// </summary>
        public GreenStaticPipe(int Width = 40, int Height = 60) 
        {
            this.Width = Width;
            this.Height = Height;
        }

        /// <summary>
        /// The drawing method for <see cref="GreenStaticPipe"/>
        /// </summary>
        /// <param name="g">The <see cref="Graphics"/> for <see cref="GreenStaticPipe"/></param>
        public override void Draw(Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            g.DrawImage(this.drawable, (int)pos.X, (int)pos.Y);
        }

        /// <summary>
        /// The loading method for <see cref="GreenStaticPipe"/>
        /// </summary><param name="env">The <see cref="Environment"/> to load for <see cref="GreenStaticPipe"/>.</param>
        public override void Load(Environment.Environment env)
        {
            this.Environment = env;

            Bitmap B = new Bitmap(this.Width, this.Height);
            for (int i = 0; i < B.Height; i++)
                for (int j = 0; j < B.Width; j++)
                    B.SetPixel(j, i, Color.White);

            for (int i = 10; i < 20; i++)
            {
                for (int y = 20; y < 40; y++)
                {
                    B.SetPixel(y, i, Color.Violet);
                }
            }
            this.drawable = B;
        }
    }
}
