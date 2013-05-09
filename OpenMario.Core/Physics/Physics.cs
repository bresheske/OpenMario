using OpenMario.Core.Actors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenMario.Core.Actors.Concrete;
using VectorClass;

namespace OpenMario.Core.Physics
{
    public static class Physics
    {
        public static readonly int MAX_MOVEMENT_SPEED = 7;

        public static readonly VectorClass.Vector2D_Int GRAVITY = new VectorClass.Vector2D_Int(0, -1);
        public static readonly VectorClass.Vector2D_Int MAX_GRAVITY = new VectorClass.Vector2D_Int(0, -MAX_MOVEMENT_SPEED);

        public enum CollisionType
        {
            TOP,
            LEFT,
            RIGHT,
            BOTTOM,
            NONE
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
            if (IsActorStandingOnAnother(a, b))
                return CollisionType.TOP;
            else if(IsActorPushingAnotherFromLeft(a, b))
                return CollisionType.LEFT;
            else if (IsActorPushingAnotherFromRight(a, b))
                return CollisionType.RIGHT;
            else if (IsActorPushingAnotherFromBottom(a, b))
                return CollisionType.BOTTOM;
            else
                return CollisionType.NONE;
        }

        public static bool IsWithinMaxThreshold(int value, int other)
        {
            return Math.Abs(value - other) <= MAX_MOVEMENT_SPEED;
        }

        public static bool IsActorStandingOnAnother(BaseActor a, BaseActor b)
        {
            return CollidedWith(a, b)
                   && IsWithinMaxThreshold(a.Position.Y + a.Height, b.Position.Y);
        }

        public static bool IsActorPushingAnotherFromLeft(BaseActor a, BaseActor b)
        {
            return CollidedWith(a, b)
                   && IsWithinMaxThreshold(a.Position.X + a.Width, b.Position.X);
        }

        public static bool IsActorPushingAnotherFromRight(BaseActor a, BaseActor b)
        {
            return CollidedWith(a, b)
                   && IsWithinMaxThreshold(a.Position.X, b.Position.X + b.Width);
        }

        public static bool IsActorPushingAnotherFromBottom(BaseActor a, BaseActor b)
        {
            return CollidedWith(a, b)
                   && IsWithinMaxThreshold(a.Position.Y, b.Position.Y + b.Height);
        }

        public static void BlockAllCollisions(BaseActor a, List<BaseActor> loadedactors)
        {
            var collisions = GetAllCollisions(loadedactors, a);
            foreach (var c in collisions)
            {
                if (c.CollisionAction != BaseActor.CollisionType.BLOCK) continue;

                if (IsActorStandingOnAnother(a, c))
                {
                    //Standing
                    a.Position = new Vector2D_Int(a.Position.X, c.Position.Y - a.Height);
                    a.Velocity = new Vector2D_Int(a.Velocity.X, 0);
                }
                else if (IsActorPushingAnotherFromLeft(a, c))
                {
                    //OnLeftSide
                    a.Position = new Vector2D_Int(c.Position.X - a.Width, a.Position.Y);
                    a.Velocity = new Vector2D_Int(0, a.Velocity.Y);
                }
                else if (IsActorPushingAnotherFromRight(a, c))
                {
                    //OnRightSide
                    a.Position = new Vector2D_Int(c.Position.X + c.Width, a.Position.Y);
                    a.Velocity = new Vector2D_Int(0, a.Velocity.Y);
                }
                else if (IsActorPushingAnotherFromBottom(a, c))
                {
                    //OnBottom
                    a.Position = new Vector2D_Int(a.Position.X, c.Position.Y + c.Height);
                    a.Velocity = new Vector2D_Int(a.Velocity.X, -a.Velocity.Y);
                }
            }
        }
    }
}