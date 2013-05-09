using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorClass;

namespace OpenMario.Core.Actors
{
    public abstract class BaseActor
    {
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public Vector2D_Int Position { get; protected set; }
        public Vector2D_Int Velocity { get; protected set; }

        public BaseActor()
        {
            Position = new Vector2D_Int(0, 0);
            Velocity = new Vector2D_Int(0, 0);
        }

        public abstract void Update(List<BaseActor> loadedactors);
        public abstract void Load();
        public abstract void Draw(Graphics g);
    }
}