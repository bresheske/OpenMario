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
        public enum CollisionType
        {
            BLOCK,
            NO_ACTION
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2D_Dbl Position { get; set; }
        public Vector2D_Dbl Velocity { get; set; }
        public CollisionType CollisionAction { get; protected set; }

        protected BaseActor()
        {
            Position = new Vector2D_Dbl(0, 0);
            Velocity = new Vector2D_Dbl(0, 0);
            CollisionAction = CollisionType.NO_ACTION;
        }

        public abstract void Update(List<BaseActor> loadedactors);
        public abstract void Load();
        public abstract void Draw(Graphics g);
    }
}