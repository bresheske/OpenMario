using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMario.Core.Actors.Concrete
{
    public class StaticBox : BaseActor
    {

        public StaticBox()
        {
            Position = new VectorClass.Vector2D_Dbl(0, 0);
            CollisionAction = CollisionType.BLOCK;
        }

        public override void Update(List<BaseActor> loadedactors)
        {
            Position += Velocity;
        }

        public override void Load(Environment.Environment env)
        {
            Environment = env;
        }

        public override void Draw(Graphics g)
        {
            var pos = Environment.CalculateRelativePosition(this);
            g.FillRectangle(Brushes.SeaGreen, new Rectangle((int)pos.X, (int)pos.Y, Width, Height));
        }
    }
}