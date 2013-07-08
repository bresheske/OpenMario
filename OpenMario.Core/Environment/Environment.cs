//-----------------------------------------------------------------------
// <copyright file="Environment.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Where the base environment is created. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Environment
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using OpenMario.Core.Actors;
    using OpenMario.Core.Actors.Concrete;
    using OpenMario.Core.Players;
    using VectorClass;
    using WMPLib;

    public abstract class Environment : IDisposable
    {
        // TODO - StartPos not used yet.
        public Point StartingPosition { get; set; }
        public List<BasePlayer> Players { get; set; }
        public List<BaseActor> Actors { get; set; }
        public List<BaseActor> ActorsToRemove { get; set; }
        public Vector2D_Dbl ViewportPosition { get; set; }
        public Vector2D_Dbl ViewportVelocity { get; set; }

        public WindowsMediaPlayer MusicPlayer { get; set; }
        public string MusicAsset { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }
        public bool IsRunning { get; set; }

        public Environment()
        {
            Players = new List<BasePlayer>();
            Actors = new List<BaseActor>();
            ActorsToRemove = new List<BaseActor>();
            Width = Engine.Engine.DEFAULT_WIDTH;
            Height = Engine.Engine.DEFAULT_HEIGHT;
            ViewportWidth = Engine.Engine.DEFAULT_WIDTH;
            ViewportHeight = Engine.Engine.DEFAULT_HEIGHT;
            ViewportPosition = new Vector2D_Dbl(0, 0);
            ViewportVelocity = new Vector2D_Dbl(0, 0);
            IsRunning = true;
        }

        public void RegisterAllKeys(Form f)
        {
            foreach (var p in Players)
                p.RegisterKeyMappings(f);
        }

        public void Update()
        {
            if (!IsRunning)
                return;

            foreach (var a in Actors)
                a.Update(Actors);

            // The following is for updating the viewport.

            var scrollingactors = Actors.Where(x => x.EnvironmentEffect == BaseActor.EnvironmentEffectType.SCROLLS_WITH_VIEWPORT);

            foreach (var a in Actors.Where(x => x.EnvironmentEffect == BaseActor.EnvironmentEffectType.CONTROLS_VIEWPORT_SCROLL))
            {
                // Lets update the viewport if the actor is controlling our scroll.
                var leftthresh = (double)ViewportWidth * (1d / 3d);
                var rightthresh = (double)ViewportWidth * (1d / 2d);
                
                if (CalculateRelativePosition(a).X <= leftthresh
                    && a.Velocity.X > 0
                    && ViewportPosition.X > 0)
                {
                    ViewportPosition -= new Vector2D_Dbl(leftthresh - CalculateRelativePosition(a).X, 0);
                }
                
                if (CalculateRelativePosition(a).X >= rightthresh
                    && a.Velocity.X < 0
                    && ViewportPosition.X + ViewportWidth < Width)
                {
                    ViewportPosition += new Vector2D_Dbl(CalculateRelativePosition(a).X - rightthresh, 0);
                }
            }

            // Remove Unloaded Actors.
            foreach (var a in ActorsToRemove)
                Actors.Remove(a);

            // If all players are dead, move on.
            // TODO: Support more than just Mario class.
            if (!Actors.Any(x => x.GetType() == typeof(Mario) && ((Mario)x).IsAlive))
            {
                IsRunning = false;
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
            // TODO: Don't just load all, load in what is in the viewport.
            foreach (var a in Actors)
                a.Load(this);

            // Music
            if (!string.IsNullOrWhiteSpace(MusicAsset))
            {
                MusicPlayer = new WMPLib.WindowsMediaPlayer();
                MusicPlayer.URL = MusicAsset;
                MusicPlayer.controls.play();
            }
        }

        public void Dispose()
        {
        }
    }
}