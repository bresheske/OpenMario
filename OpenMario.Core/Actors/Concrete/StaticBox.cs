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
            Position = new VectorClass.Vector2D_Dbl(20, 200);
            Height = 10;
            Width = 300;
            CollisionAction = CollisionType.BLOCK;
        }

        public override void Update(List<BaseActor> loadedactors)
        {
            
        }

        public override void Load()
        {
            
        }

        public override void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.SeaGreen, new Rectangle((int)Position.X, (int)Position.Y, Width, Height));
        }
    }
}