using OpenMario.Core.Actors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMario.Core.Physics
{
    public static class Physics
    {
        public static readonly VectorClass.Vector2D_Int GRAVITY = new VectorClass.Vector2D_Int(0, -1);
        public static readonly VectorClass.Vector2D_Int MAX_GRAVITY = new VectorClass.Vector2D_Int(0, -9);

        public enum CollisionType
        {
            TOP,
            LEFT,
            RIGHT,
            BOTTOM
        }

        public static bool CollidedWith(BaseActor a, BaseActor b)
        {
            return new Rectangle(a.Position.X, a.Position.Y, a.Width, a.Height)
                .IntersectsWith(new Rectangle(b.Position.X, b.Position.Y, b.Width, b.Height));
        }

        public static List<BaseActor> GetAllCollisions(List<BaseActor> allactors, BaseActor collidedwith)
        {
            return allactors
                .Where(x => x != collidedwith)
                .Where(x => CollidedWith(x, collidedwith))
                .ToList();
        }

        public static CollisionType GetCollisionType(BaseActor a, BaseActor b)
        {
            return CollisionType.TOP;
        }
    }
}