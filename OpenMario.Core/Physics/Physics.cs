using OpenMario.Core.Actors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using VectorClass;

namespace OpenMario.Core.Physics
{
    public static class Physics
    {
        public const double GROUND_FRICTION_DELTA = 1.5d;
        public const double AIR_FRICTION_DELTA = .3d;
        public const double MAX_MOVEMENT_SPEED = 15d;


        public static readonly Vector2D_Dbl GRAVITY = new Vector2D_Dbl(0d, -1d);
        public static readonly Vector2D_Dbl MAX_GRAVITY = new Vector2D_Dbl(0d, -MAX_MOVEMENT_SPEED);

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
            return new Rectangle((int)a.Position.X, (int)a.Position.Y, a.Width, a.Height)
                .IntersectsWith(new Rectangle((int)b.Position.X, (int)b.Position.Y, b.Width, b.Height));
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

        public static bool IsWithinMaxThreshold(double value, double other)
        {
            return Math.Abs(value - other) <= MAX_MOVEMENT_SPEED;
        }

        public static bool IsActorStandingOnAnother(BaseActor a, List<BaseActor> loaded )
        {
            return loaded.Any(b => IsActorStandingOnAnother(a, b));
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
                    a.Position = new Vector2D_Dbl(a.Position.X, c.Position.Y - a.Height);
                    a.Velocity = new Vector2D_Dbl(a.Velocity.X, 0);
                }
                else if (IsActorPushingAnotherFromLeft(a, c))
                {
                    //OnLeftSide
                    a.Position = new Vector2D_Dbl(c.Position.X - a.Width, a.Position.Y);
                    a.Velocity = new Vector2D_Dbl(0, a.Velocity.Y);
                }
                else if (IsActorPushingAnotherFromRight(a, c))
                {
                    //OnRightSide
                    a.Position = new Vector2D_Dbl(c.Position.X + c.Width, a.Position.Y);
                    a.Velocity = new Vector2D_Dbl(0, a.Velocity.Y);
                }
                else if (IsActorPushingAnotherFromBottom(a, c))
                {
                    //OnBottom
                    a.Position = new Vector2D_Dbl(a.Position.X, c.Position.Y + c.Height);
                    a.Velocity = new Vector2D_Dbl(a.Velocity.X, 0);
                }
            }
        }

        public static void NormalizeVelocity(BaseActor a)
        {
            if (a.Velocity.X < -MAX_MOVEMENT_SPEED)
                a.Velocity = new Vector2D_Dbl(-MAX_MOVEMENT_SPEED, a.Velocity.Y);
            if (a.Velocity.X > MAX_MOVEMENT_SPEED)
                a.Velocity = new Vector2D_Dbl(MAX_MOVEMENT_SPEED, a.Velocity.Y);
            if (a.Velocity.Y < -MAX_MOVEMENT_SPEED)
                a.Velocity = new Vector2D_Dbl(a.Velocity.X, -MAX_MOVEMENT_SPEED);
            if (a.Velocity.Y > MAX_MOVEMENT_SPEED)
                a.Velocity = new Vector2D_Dbl(a.Velocity.X, MAX_MOVEMENT_SPEED);
        }

        public static void ApplyGroundFriction(BaseActor a, List<BaseActor> loadedactors)
        {
            if (!IsActorStandingOnAnother(a, loadedactors)) return;

            //standing on ground, apply friction.
            if (Math.Abs(a.Velocity.X - GROUND_FRICTION_DELTA) < GROUND_FRICTION_DELTA)
                a.Velocity = new Vector2D_Dbl(0,0);
            else if (a.Velocity.X > 0)
                a.Velocity += new Vector2D_Dbl(-GROUND_FRICTION_DELTA, 0);
            else if (a.Velocity.X < 0)
                a.Velocity += new Vector2D_Dbl(GROUND_FRICTION_DELTA, 0);
        }

        public static void ApplyAirFriction(BaseActor a, List<BaseActor> loadedactors)
        {
            if (IsActorStandingOnAnother(a, loadedactors)) return;

            //in air, apply friction.
            if (Math.Abs(a.Velocity.X - AIR_FRICTION_DELTA) < AIR_FRICTION_DELTA)
                a.Velocity = new Vector2D_Dbl(0, 0);
            else if (a.Velocity.X > 0)
                a.Velocity += new Vector2D_Dbl(-AIR_FRICTION_DELTA, 0);
            else if (a.Velocity.X < 0)
                a.Velocity += new Vector2D_Dbl(AIR_FRICTION_DELTA, 0);
        }
    }
}