//-----------------------------------------------------------------------
// <copyright file="BaseActor.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Code for dealing with the various actors for Open Mario. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Actors
{
    using System.Collections.Generic;
    using System.Drawing;
    using OpenMario.Core.Actors.Sprites;
    using VectorClass;

    /// <summary>
    /// This class covers the base methods for interacting with actors that will exist in OpenMario.
    /// </summary>
    public abstract class BaseActor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseActor"/> class.
        /// </summary>
        protected BaseActor()
        {
            this.Position = new Vector2D_Dbl(0, 0);
            this.Velocity = new Vector2D_Dbl(0, 0);
            this.CollisionAction = CollisionType.NO_ACTION;
            this.EnvironmentEffect = EnvironmentEffectType.SCROLLS_WITH_VIEWPORT;
        }

        /// <summary>
        /// Enum covering the various types of collisions an actor can have.
        /// </summary>
        public enum CollisionType
        {
            /// <summary>
            /// TODO: Figure out what this is doing?
            /// </summary>
            BLOCK,

            /// <summary>
            /// Nothing happens when this object has a collision.
            /// </summary>
            NO_ACTION
        }

        /// <summary>
        /// Covers the various environment effect types that can occur in the game.
        /// </summary>
        public enum EnvironmentEffectType
        {
            /// <summary>
            /// No Effect with the Environment
            /// </summary>
            NO_EFFECT,

            /// <summary>
            /// Fixed position, does not move when the viewport moves.
            /// </summary>
            FIXED_POSITION,

            /// <summary>
            /// Moves when the viewport moves, generally when a player moves left or right.
            /// This is the default.
            /// </summary>
            SCROLLS_WITH_VIEWPORT,

            /// <summary>
            /// Actor actually controls the viewport. Probably only the 'mario' class.
            /// </summary>
            CONTROLS_VIEWPORT_SCROLL
        }

        /// <summary>
        /// Gets or sets the Width of the actor.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the Height of the actor.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the Position of the actor.
        /// </summary>
        public Vector2D_Dbl Position { get; set; }

        /// <summary>
        /// Gets or sets the Velocity of the actor.
        /// </summary>
        public Vector2D_Dbl Velocity { get; set; }

        /// <summary>
        /// Gets or sets the CollisionAction for the actor.
        /// </summary>
        public CollisionType CollisionAction { get; protected set; }

        /// <summary>
        /// Gets or sets the EnvironmentEffect for the actor.
        /// </summary>
        public EnvironmentEffectType EnvironmentEffect { get; set; }

        /// <summary>
        /// Gets or sets the Environment for the actor.
        /// </summary>
        public Environment.Environment Environment { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SpriteManager"/> class.
        /// </summary>
        public SpriteManager SpriteManager { get; set; }

        /// <summary>
        /// Defines the base method for Update.
        /// </summary>
        /// <param name="loadedactors"><c>List<c>BaseActor</c></c></param>
        public abstract void Update(List<BaseActor> loadedactors);

        /// <summary>
        /// Defines the base method for loading the environment.
        /// </summary>
        /// <param name="env">Environment</param>
        public abstract void Load(Environment.Environment env);

        /// <summary>
        /// Defines the base method of drawing the graphics.
        /// </summary>
        /// <param name="g"><see cref="Graphics"/></param>
        public abstract void Draw(Graphics g);
    }
}