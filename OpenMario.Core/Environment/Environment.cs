using OpenMario.Core.Actors;
using OpenMario.Core.Players;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorClass;

namespace OpenMario.Core.Environment
{
    public abstract class Environment
    {
        //TODO - StartPos not used yet.
        public Point StartingPosition { get; set; }
        public List<BasePlayer> Players { get; set; }
        public List<BaseActor> Actors { get; set; }
        public Vector2D_Dbl ViewportPosition { get; set; }
        public Vector2D_Dbl ViewportVelocity { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }

        public Environment()
        {
            Players = new List<BasePlayer>();
            Actors = new List<BaseActor>();
            Width = Engine.Engine.DEFAULT_WIDTH;
            Height = Engine.Engine.DEFAULT_HEIGHT;
            ViewportWidth = Engine.Engine.DEFAULT_WIDTH;
            ViewportHeight = Engine.Engine.DEFAULT_HEIGHT;
            ViewportPosition = new Vector2D_Dbl(0, 0);
            ViewportVelocity = new Vector2D_Dbl(0, 0);
        }

        public void RegisterAllKeys(Form f)
        {
            foreach (var p in Players)
                p.RegisterKeyMappings(f);
        }

        public void Update()
        {
            foreach (var a in Actors)
                a.Update(Actors);


            //The following is for updating the viewport.

            var scrollingactors = Actors.Where(x => x.EnvironmentEffect == BaseActor.EnvironmentEffectType.SCROLLS_WITH_VIEWPORT);

            foreach (var a in Actors.Where(x => x.EnvironmentEffect == BaseActor.EnvironmentEffectType.CONTROLS_VIEWPORT_SCROLL))
            {
                //Lets update the viewport if the actor is controlling our scroll.
                var leftthresh = (double)ViewportWidth * (1d / 3d);
                var rightthresh = (double)ViewportWidth * (2d / 3d);
                //if the actor is in the right-most 1/3 of our screen.
                if (CalculateRelativePosition(a).X <= leftthresh
                    && a.Velocity.X > 0
                    && ViewportPosition.X > 0)
                {
                    ViewportPosition -= new Vector2D_Dbl(leftthresh - CalculateRelativePosition(a).X, 0);
                }
                //if the actor is in the left-most 1/3 of our screen.
                if (CalculateRelativePosition(a).X >= rightthresh
                    && a.Velocity.X < 0
                    && ViewportPosition.X + ViewportWidth < Width)
                {
                    ViewportPosition += new Vector2D_Dbl(CalculateRelativePosition(a).X - rightthresh, 0);
                }
            }
        }

        public Vector2D_Dbl CalculateRelativePosition(BaseActor a)
        {
            return a.Position - ViewportPosition;
        }

        public void Render(Graphics g)
        {
            foreach (var a in Actors)
                a.Draw(g);
        }

        public void Load()
        {
            //TODO: Don't just load all, load in what is in the viewport.
            foreach (var a in Actors)
                a.Load(this);
        }
    }
}