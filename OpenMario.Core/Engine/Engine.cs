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
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>
    /// The engine. Vrroom. Vroom.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class Engine : IDisposable
    {
        /// <summary>
        /// Defines the height of the window 
        /// </summary>
        public const int DefaultHeight = 480;

        /// <summary>
        /// Defines the width of the window.
        /// </summary>
        public const int DefaultWidth = 640;
        
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

        /// <summary>
        /// Gets the current fps.
        /// </summary>
        public float CurrentFPS { get; private set; }

        /// <summary>
        /// Gets the current frame.
        /// </summary>
        public Bitmap CurrentFrame { get; private set; }

        /// <summary>
        /// Gets a value indicating whether engine is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Gets or sets the main form.
        /// </summary>
        public Form MainForm { get; set; }

        /// <summary>
        /// Gets the running thread.
        /// </summary>
        protected Thread RunningThread { get; private set; }
        #endregion

        /// <summary>
        /// The load method for <see cref="Engine"/>
        /// </summary>
        /// <param name="form">The <see cref="Form"/> for <see cref="Engine"/>.</param>
        /// <param name="e">The <see cref="Environment"/> for the <see cref="Engine"/>.</param>
        public void Load(Form form, Core.Environment.Environment e)
        {
            // left blank
        }

        /// <summary>
        /// The start of the <see cref="Engine"/>
        /// </summary>
        /// <exception cref="Exception">Engine is already running.</exception>
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

        /// <summary>
        /// The stopping of the engine.
        /// </summary>
        /// <exception cref="Exception">
        /// Engine not running cannot stop.
        /// </exception>
        public void Stop()
        {
            if (!this.IsRunning)
            {
                throw new Exception("Engine not running, cannot stop.");
            }

            this.IsRunning = false;
        }

        /// <summary>
        /// The disposing of the engine.
        /// </summary>
        public void Dispose()
        {
            if (this.IsRunning)
            {
                this.Stop();
            }
        }

        /// <summary>
        /// The tick.
        /// </summary>
        protected void Tick()
        {
            var curframe = new Bitmap(DefaultWidth, DefaultHeight);

            /* Draw on curframe */

            /* Dispose current frame, set new, and fire event. */
            /* if (CurrentFrame != null)
                 CurrentFrame.Dispose(); */
            this.CurrentFrame = curframe;
            if (this.OnNewFrame != null)
            {
                this.OnNewFrame(this, new FrameEventArgs { Frame = this.CurrentFrame });
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
                var targettime = 1f / this.TargetFPS * 1000f;
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

                this.CurrentFPS = count / (framecounter.ElapsedMilliseconds / 1000f);
                framecounter.Reset();
                count = 0;
            }
        }
    }
}