using OpenMario.Core.Actors.Sprites;
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

        public enum EnvironmentEffectType
        {
            //No Effect with the Environment
            NO_EFFECT,
            //Fixed position, does not move when the viewport moves.
            FIXED_POSITION,
            //Moves when the viewport moves, generally when a player moves left or right.
            //This is the default.
            SCROLLS_WITH_VIEWPORT,
            //Actor actually controls the viewport. Probably only the 'mario' class.
            CONTROLS_VIEWPORT_SCROLL
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public Vector2D_Dbl Position { get; set; }
        public Vector2D_Dbl Velocity { get; set; }
        public CollisionType CollisionAction { get; protected set; }
        public EnvironmentEffectType EnvironmentEffect { get; set; }
        public Environment.Environment Environment { get; set; }
        public SpriteManager SpriteManager { get; set; }

        protected BaseActor()
        {
            Position = new Vector2D_Dbl(0, 0);
            Velocity = new Vector2D_Dbl(0, 0);
            CollisionAction = CollisionType.NO_ACTION;
            EnvironmentEffect = EnvironmentEffectType.SCROLLS_WITH_VIEWPORT;
        }

        public abstract void Update(List<BaseActor> loadedactors);
        public abstract void Load(Environment.Environment env);
        public abstract void Draw(Graphics g);
    }
}