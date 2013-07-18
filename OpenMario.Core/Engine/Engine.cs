//-----------------------------------------------------------------------
// <copyright file="Engine.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> Contains all of the base logic for the "OpenMario" engine. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario.Core.Engine
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class Engine : IDisposable
    {
        /// <summary>
        /// Defines the height of the window 
        /// </summary>
        public const int DEFAULT_HEIGHT = 480;

        /// <summary>
        /// Defines the width of the window.
        /// </summary>
        public const int DEFAULT_WIDTH = 640;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Engine" /> class.
        /// </summary>
        public Engine()
        {
            this.TargetFPS = 60;
        }

        #region Events
        /// <summary>
        /// Sets up the an event handler for handling a new frame.
        /// </summary>
        public event EventHandler<FrameEventArgs> OnNewFrame;
        #endregion

        #region Props
        /// <summary>
        /// Gets or sets the TargetFPS for the game.
        /// </summary>
        public int TargetFPS { get; set; }

        public float CurrentFPS { get; private set; }

        public Bitmap CurrentFrame { get; private set; }

        public bool IsRunning { get; private set; }

        public Form MainForm { get; set; }

        protected Thread RunningThread { get; private set; }
        #endregion

        public void Load(Form form, Core.Environment.Environment e)
        {
            // left blank
        }

        public void Start()
        {
            if (this.IsRunning)
            {
                throw new Exception("Engine already running, cannot start.");
            }

            this.IsRunning = true;
            this.RunningThread = new Thread(this.ThreadRun);
            this.RunningThread.Start();
        }

        public void Stop()
        {
            if (!this.IsRunning)
            {
                throw new Exception("Engine not running, cannot stop.");
            }

            this.IsRunning = false;
        }

        public void Dispose()
        {
            if (this.IsRunning)
            {
                this.Stop();
            }
        }

        protected void Tick()
        {
            var curframe = new Bitmap(DEFAULT_WIDTH, DEFAULT_HEIGHT);

            /* Draw on curframe */

            /* Dispose current frame, set new, and fire event. */
            /* if (CurrentFrame != null)
                 CurrentFrame.Dispose(); */
            this.CurrentFrame = curframe;
            if (this.OnNewFrame != null)
            {
                this.OnNewFrame(this, new FrameEventArgs() { Frame = this.CurrentFrame });
            }
        }

        /// <summary>
        /// Threadrun -> Tick
        /// </summary>
        protected void ThreadRun()
        {
            var framecounter = new Stopwatch();
            var tickcounter = new Stopwatch();
            var count = 0;
            while (this.IsRunning)
            {
                /* Just run the tick. */
                framecounter.Start();
                tickcounter.Start();
                this.Tick();
                tickcounter.Stop();
                count++;

                /* Handle FPS & FPS-Limiting. */
                var targettime = 1f / (float)this.TargetFPS * 1000f;
                var actualtime = tickcounter.ElapsedMilliseconds;
                var sleep = targettime - actualtime;
                if (sleep > 0)
                {
                    Thread.Sleep((int)sleep);
                }

                tickcounter.Reset();
                framecounter.Stop();

                /* Calculate current FPS. */
                if (framecounter.ElapsedMilliseconds < 1000)
                {
                    continue;
                }
                this.CurrentFPS = (float)count / (framecounter.ElapsedMilliseconds / 1000f);
                framecounter.Reset();
                count = 0;
            }
        }
    }
}