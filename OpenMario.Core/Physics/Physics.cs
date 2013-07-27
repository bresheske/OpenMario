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
        public const double GroundFrictionDelta = 1.4d;

        /// <summary>
        /// Declares the Air Friction Delta field.
        /// </summary>
        public const double AirFrictionDelta = .9d;

        /// <summary>
        /// Declares the Max Movement Speed of any Actor.
        /// </summary>
        public const double MaxMovementSpeed = 10d;

        /// <summary>
        /// Declares the Max Jump Speed of any Actor.
        /// </summary>
        public const double MaxJumpSpeed = 12d;

        /// <summary>
        /// Declares the movement delta for any actor.
        /// </summary>
        public const double MovementDelta = .3d;

        /// <summary>
        /// Declares the gravity
        /// </summary>
        public static readonly Vector2D_Dbl Gravity = new Vector2D_Dbl(0d, -.6d);

        /// <summary>
        /// Declares the maximum gravity
        /// </summary>
        public static readonly Vector2D_Dbl MaxGravity = new Vector2D_Dbl(0d, -MaxMovementSpeed);

        /// <summary>
        /// Enumerates the different <c>CollisionType</c>
        /// </summary>
        public enum CollisionType
        {
            /// <summary>
            /// The actor is colliding from the top.
            /// </summary>
            Top,

            /// <summary>
            /// The actor is colliding from the left
            /// </summary>
            Left,

            /// <summary>
            /// The actor is colliding from the right
            /// </summary>
            Right,

            /// <summary>
            /// The actor is colliding from the bottom.
            /// </summary>
            Bottom,

            /// <summary>
            /// The actor is not colliding.
            /// </summary>
            None
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
                return CollisionType.Top;
            }

            if (IsActorPushingAnotherFromLeft(a, b))
            {
                return CollisionType.Left;
            }

            if (IsActorPushingAnotherFromRight(a, b))
            {
                return CollisionType.Right;
            }

            if (IsActorPushingAnotherFromBottom(a, b))
            {
                return CollisionType.Bottom;
            }

            return CollisionType.None;
        }

        /// <summary>
        /// The is within max threshold.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsWithinMaxThreshold(double value, double other)
        {
            return Math.Abs(value - other) <= MaxMovementSpeed;
        }

        /// <summary>
        /// The is actor standing on another.
        /// </summary>
        /// <param name="a">
        /// The <see cref="BaseActor"/> standing
        /// </param>
        /// <param name="loaded">
        /// The <see cref="BaseActor"/> that is being stood on.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsActorStandingOnAnother(BaseActor a, List<BaseActor> loaded)
        {
            return loaded.Any(b => IsActorStandingOnAnother(a, b));
        }

        /// <summary>
        /// The is actor pushing another from left.
        /// </summary>
        /// <param name="a">
        /// The <see cref="BaseActor"/> pushing.
        /// </param>
        /// <param name="loaded">
        /// The <see cref="BaseActor"/> being pushed.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsActorPushingAnotherFromLeft(BaseActor a, List<BaseActor> loaded)
        {
            return loaded.Any(b => IsActorPushingAnotherFromLeft(a, b));
        }

        /// <summary>
        /// The is actor pushing another from the right.
        /// </summary>
        /// <param name="a">
        /// The <see cref="BaseActor"/> pushing from the right.
        /// </param>
        /// <param name="loaded">
        /// The <see cref="BaseActor"/> being pushed.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsActorPushingAnotherFromRight(BaseActor a, List<BaseActor> loaded)
        {
            return loaded.Any(b => IsActorPushingAnotherFromRight(a, b));
        }

        /// <summary>
        /// The is actor pushing another from bottom.
        /// </summary>
        /// <param name="a">
        /// The <see cref="BaseActor"/> that is pushing.
        /// </param>
        /// <param name="loaded">
        /// The <see cref="BaseActor"/> that is being pushed.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsActorPushingAnotherFromBottom(BaseActor a, List<BaseActor> loaded)
        {
            return loaded.Any(b => IsActorPushingAnotherFromBottom(a, b));
        }

        /// <summary>
        /// The is actor standing on another.
        /// </summary>
        /// <param name="a">
        /// The <see cref="BaseActor"/> that is standing.
        /// </param>
        /// <param name="b">
        /// The <see cref="BaseActor"/> that is being stood on.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsActorStandingOnAnother(BaseActor a, BaseActor b)
        {
            return CollidedWith(a, b)
                   && IsWithinMaxThreshold(a.Position.Y + a.Height, b.Position.Y);
        }

        /// <summary>
        /// The is actor pushing another from left.
        /// </summary>
        /// <param name="a">
        /// The <see cref="BaseActor"/> that is pushing.
        /// </param>
        /// <param name="b">
        /// The <see cref="BaseActor"/> that is being pushed.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsActorPushingAnotherFromLeft(BaseActor a, BaseActor b)
        {
            return CollidedWith(a, b)
                   && IsWithinMaxThreshold(a.Position.X + a.Width, b.Position.X);
        }

        /// <summary>
        /// The is actor pushing another from right.
        /// </summary>
        /// <param name="a">
        /// The <see cref="BaseActor"/> that is pushing.
        /// </param>
        /// <param name="b">
        /// The <see cref="BaseActor"/> that is being pushed.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsActorPushingAnotherFromRight(BaseActor a, BaseActor b)
        {
            return CollidedWith(a, b)
                   && IsWithinMaxThreshold(a.Position.X, b.Position.X + b.Width);
        }

        /// <summary>
        /// The is actor pushing another from bottom.
        /// </summary>
        /// <param name="a">
        /// The <see cref="BaseActor"/> that is pushing.
        /// </param>
        /// <param name="b">
        /// The <see cref="BaseActor"/> that is being pushed.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsActorPushingAnotherFromBottom(BaseActor a, BaseActor b)
        {
            return CollidedWith(a, b)
                && IsWithinMaxThreshold(a.Position.Y, b.Position.Y + b.Height);
        }

        /// <summary>
        /// The block all collisions.
        /// </summary>
        /// <param name="a">
        /// The <see cref="BaseActor"/> colliding with an object.
        /// </param>
        /// <param name="loadedactors">
        /// The <see cref="BaseActor"/> that is being collided with.
        /// </param>
        public static void BlockAllCollisions(BaseActor a, List<BaseActor> loadedactors)
        {
            var collisions = GetAllCollisions(loadedactors, a);
            foreach (var c in collisions.Where(c => c.CollisionAction == BaseActor.CollisionType.Block))
            {
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

        /// <summary>
        /// The normalize velocity.
        /// </summary>
        /// <param name="a">
        /// The <see cref="BaseActor"/> to normalize.
        /// </param>
        public static void NormalizeVelocity(BaseActor a)
        {
            if (a.Velocity.X < -MaxMovementSpeed)
            {
                a.Velocity = new Vector2D_Dbl(-MaxMovementSpeed, a.Velocity.Y);
            }

            if (a.Velocity.X > MaxMovementSpeed)
            {
                a.Velocity = new Vector2D_Dbl(MaxMovementSpeed, a.Velocity.Y);
            }

            if (a.Velocity.Y < -MaxMovementSpeed)
            {
                a.Velocity = new Vector2D_Dbl(a.Velocity.X, -MaxMovementSpeed);
            }

            if (a.Velocity.Y > MaxMovementSpeed)
            {
                a.Velocity = new Vector2D_Dbl(a.Velocity.X, MaxMovementSpeed);
            }
        }

        /// <summary>
        /// The apply ground friction.
        /// </summary>
        /// <param name="a">
        /// The <see cref="BaseActor"/> to apply friction to.
        /// </param>
        /// <param name="loadedactors">
        /// The <see cref="BaseActor"/> to apply friction from (the ground).
        /// </param>
        public static void ApplyGroundFriction(BaseActor a, List<BaseActor> loadedactors)
        {
            if (!IsActorStandingOnAnother(a, loadedactors))
            {
                return;
            }

            // standing on ground, apply friction.
            if (Math.Abs(a.Velocity.X - GroundFrictionDelta) < GroundFrictionDelta)
            {
                a.Velocity = new Vector2D_Dbl(0, a.Velocity.Y);
            }
            else if (a.Velocity.X > 0)
            {
                a.Velocity += new Vector2D_Dbl(-GroundFrictionDelta, 0);
            }
            else if (a.Velocity.X < 0)
            {
                a.Velocity += new Vector2D_Dbl(GroundFrictionDelta, 0);
            }
        }

        /// <summary>
        /// The apply air friction.
        /// </summary>
        /// <param name="a">
        /// The <see cref="BaseActor"/> to apply air friction to.
        /// </param>
        /// <param name="loadedactors">
        /// The <see cref="BaseActor"/> to apply friction from.
        /// </param>
        public static void ApplyAirFriction(BaseActor a, List<BaseActor> loadedactors)
        {
            if (IsActorStandingOnAnother(a, loadedactors))
            {
                return;
            }

            // in air, apply friction.
            if (Math.Abs(a.Velocity.X - AirFrictionDelta) < AirFrictionDelta)
            {
                a.Velocity = new Vector2D_Dbl(0, a.Velocity.Y);
            }
            else if (a.Velocity.X > 0)
            {
                a.Velocity += new Vector2D_Dbl(-AirFrictionDelta, 0);
            }
            else if (a.Velocity.X < 0)
            {
                a.Velocity += new Vector2D_Dbl(AirFrictionDelta, 0);
            }
        }
    }
}