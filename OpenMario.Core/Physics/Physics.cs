//-----------------------------------------------------------------------
// <copyright file="Physics.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Sets up all of the physics for the game. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Physics
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using OpenMario.Core.Actors;
    using VectorClass;

    /// <summary>
    /// Declaring the awesome power of OpenMario physics.
    /// </summary>
    public static class Physics
    {
        /// <summary>
        /// Declares the Ground Friction Delta field.
        /// </summary>
        public const double GROUND_FRICTION_DELTA = 1.4d;

        /// <summary>
        /// Declares the Air Friction Delta field.
        /// </summary>
        public const double AIR_FRICTION_DELTA = .9d;

        /// <summary>
        /// Declares the Max Movement Speed of any Actor.
        /// </summary>
        public const double MAX_MOVEMENT_SPEED = 10d;

        /// <summary>
        /// Declares the Max Jump Speed of any Actor.
        /// </summary>
        public const double MAX_JUMP_SPEED = 12d;

        /// <summary>
        /// Declares the movement delta for any actor.
        /// </summary>
        public const double MOVEMENT_DELTA = .3d;

        /// <summary>
        /// Declares the gravity
        /// </summary>
        public static readonly Vector2D_Dbl GRAVITY = new Vector2D_Dbl(0d, -.6d);

        /// <summary>
        /// Declares the maximum gravity
        /// </summary>
        public static readonly Vector2D_Dbl MAX_GRAVITY = new Vector2D_Dbl(0d, -MAX_MOVEMENT_SPEED);

        /// <summary>
        /// Enumerates the different <c>CollisionType</c>
        /// </summary>
        public enum CollisionType
        {
            /// <summary>
            /// The actor is colliding from the top.
            /// </summary>
            TOP,

            /// <summary>
            /// The actor is colliding from the left
            /// </summary>
            LEFT,

            /// <summary>
            /// The actor is colliding from the right
            /// </summary>
            RIGHT,

            /// <summary>
            /// The actor is colliding from the bottom.
            /// </summary>
            BOTTOM,

            /// <summary>
            /// The actor is not colliding.
            /// </summary>
            NONE
        }

        /// <summary>
        /// Method for defining what the actor is colliding with.
        /// Figures out with a given two actors where they are intersecting.
        /// </summary>
        /// <param name="a">Actor 1 - <c>BaseActor</c></param>
        /// <param name="b">Actor 2 - <c>BaseActor</c></param>
        /// <returns>true if intersecting</returns>
        public static bool CollidedWith(BaseActor a, BaseActor b)
        {
            return new Rectangle((int)a.Position.X, (int)a.Position.Y, a.Width, a.Height)
                .IntersectsWith(new Rectangle((int)b.Position.X, (int)b.Position.Y, b.Width, b.Height));
        }

        /// <summary>
        /// Method for defining what happens when a given list of actors collide with another actor.
        /// </summary>
        /// <param name="allactors">List of <c>BaseActor</c> that will be colliding with</param>
        /// <param name="collidedwith"><c>BaseActor</c> object being collided with.</param>
        /// <returns>List of <c>BaseActor</c> and their collision states</returns>
        public static List<BaseActor> GetAllCollisions(List<BaseActor> allactors, BaseActor collidedwith)
        {
            return allactors
                .Where(x => x != collidedwith)
                .Where(x => CollidedWith(x, collidedwith))
                .ToList();
        }

        /// <summary>
        /// Method for figuring out the type of collision that is happening.
        /// </summary>
        /// <param name="a">Actor 1 - <c>BaseActor</c></param>
        /// <param name="b">Actor 2 - <c>BaseActor</c></param>
        /// <returns><c>CollisionType</c></returns>s
        public static CollisionType GetCollisionType(BaseActor a, BaseActor b)
        {
            if (IsActorStandingOnAnother(a, b))
            {
                return CollisionType.TOP;
            }
            else if (IsActorPushingAnotherFromLeft(a, b))
            {
                return CollisionType.LEFT;
            }
            else if (IsActorPushingAnotherFromRight(a, b))
            {
                return CollisionType.RIGHT;
            }
            else if (IsActorPushingAnotherFromBottom(a, b))
            {
                return CollisionType.BOTTOM;
            }
            else
            {
                return CollisionType.NONE;
            }
        }

        public static bool IsWithinMaxThreshold(double value, double other)
        {
            return Math.Abs(value - other) <= MAX_MOVEMENT_SPEED;
        }

        public static bool IsActorStandingOnAnother(BaseActor a, List<BaseActor> loaded)
        {
            return loaded.Any(b => IsActorStandingOnAnother(a, b));
        }

        public static bool IsActorPushingAnotherFromLeft(BaseActor a, List<BaseActor> loaded)
        {
            return loaded.Any(b => IsActorPushingAnotherFromLeft(a, b));
        }

        public static bool IsActorPushingAnotherFromRight(BaseActor a, List<BaseActor> loaded)
        {
            return loaded.Any(b => IsActorPushingAnotherFromRight(a, b));
        }

        public static bool IsActorPushingAnotherFromBottom(BaseActor a, List<BaseActor> loaded)
        {
            return loaded.Any(b => IsActorPushingAnotherFromBottom(a, b));
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
                if (c.CollisionAction != BaseActor.CollisionType.BLOCK)
                {
                    continue;
                }

                if (IsActorStandingOnAnother(a, c))
                {
                    // Standing
                    a.Position = new Vector2D_Dbl(a.Position.X, c.Position.Y - a.Height);
                    a.Velocity = new Vector2D_Dbl(a.Velocity.X, a.Velocity.Y < 0 ? 0 : a.Velocity.Y);
                }
                else if (IsActorPushingAnotherFromLeft(a, c))
                {
                    // OnLeftSide
                    a.Position = new Vector2D_Dbl(c.Position.X - a.Width, a.Position.Y);
                    a.Velocity = new Vector2D_Dbl(0, a.Velocity.Y);
                }
                else if (IsActorPushingAnotherFromRight(a, c))
                {
                    // OnRightSide
                    a.Position = new Vector2D_Dbl(c.Position.X + c.Width, a.Position.Y);
                    a.Velocity = new Vector2D_Dbl(0, a.Velocity.Y);
                }
                else if (IsActorPushingAnotherFromBottom(a, c))
                {
                    // OnBottom
                    a.Position = new Vector2D_Dbl(a.Position.X, c.Position.Y + c.Height);
                    a.Velocity = new Vector2D_Dbl(a.Velocity.X, 0);
                }
            }
        }

        public static void NormalizeVelocity(BaseActor a)
        {
            if (a.Velocity.X < -MAX_MOVEMENT_SPEED)
            {
                a.Velocity = new Vector2D_Dbl(-MAX_MOVEMENT_SPEED, a.Velocity.Y);
            }

            if (a.Velocity.X > MAX_MOVEMENT_SPEED)
            {
                a.Velocity = new Vector2D_Dbl(MAX_MOVEMENT_SPEED, a.Velocity.Y);
            }

            if (a.Velocity.Y < -MAX_MOVEMENT_SPEED)
            {
                a.Velocity = new Vector2D_Dbl(a.Velocity.X, -MAX_MOVEMENT_SPEED);
            }

            if (a.Velocity.Y > MAX_MOVEMENT_SPEED)
            {
                a.Velocity = new Vector2D_Dbl(a.Velocity.X, MAX_MOVEMENT_SPEED);
            }
        }

        public static void ApplyGroundFriction(BaseActor a, List<BaseActor> loadedactors)
        {
            if (!IsActorStandingOnAnother(a, loadedactors))
            {
                return;
            }

            // standing on ground, apply friction.
            if (Math.Abs(a.Velocity.X - GROUND_FRICTION_DELTA) < GROUND_FRICTION_DELTA)
            {
                a.Velocity = new Vector2D_Dbl(0, a.Velocity.Y);
            }
            else if (a.Velocity.X > 0)
            {
                a.Velocity += new Vector2D_Dbl(-GROUND_FRICTION_DELTA, 0);
            }
            else if (a.Velocity.X < 0)
            {
                a.Velocity += new Vector2D_Dbl(GROUND_FRICTION_DELTA, 0);
            }
        }

        public static void ApplyAirFriction(BaseActor a, List<BaseActor> loadedactors)
        {
            if (IsActorStandingOnAnother(a, loadedactors))
            {
                return;
            }

            // in air, apply friction.
            if (Math.Abs(a.Velocity.X - AIR_FRICTION_DELTA) < AIR_FRICTION_DELTA)
            {
                a.Velocity = new Vector2D_Dbl(0, a.Velocity.Y);
            }
            else if (a.Velocity.X > 0)
            {
                a.Velocity += new Vector2D_Dbl(-AIR_FRICTION_DELTA, 0);
            }
            else if (a.Velocity.X < 0)
            {
                a.Velocity += new Vector2D_Dbl(AIR_FRICTION_DELTA, 0);
            }
        }
    }
}