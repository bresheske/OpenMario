using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenMario.Core.Engine
{
    public class Engine : IDisposable
    {
        public const int DEFAULT_HEIGHT = 480;
        public const int DEFAULT_WIDTH = 640;

        public Engine()
        {
            TargetFPS = 60;
        }

        #region Props
        public int TargetFPS { get; set; }
        public float CurrentFPS { get; private set; }
        public Bitmap CurrentFrame { get; private set; }
        public bool IsRunning { get; private set; }

        protected Thread RunningThread { get; private set; }
        #endregion

        #region Events
        public event EventHandler<FrameEventArgs> OnNewFrame;
        #endregion


        /// <summary>
        /// Threadrun -> Tick
        /// </summary>
        protected void ThreadRun()
        {
            var framecounter = new Stopwatch();
            var tickcounter = new Stopwatch();
            var count = 0;
            while (IsRunning)
            {
                /* Just run the tick. */
                framecounter.Start();
                tickcounter.Start();
                Tick();
                tickcounter.Stop();
                count++;

                /* Handle FPS & FPS-Limiting. */
                var targettime = 1f / (float)TargetFPS * 1000f;
                var actualtime = tickcounter.ElapsedMilliseconds;
                var sleep = targettime - actualtime;
                if (sleep > 0)
                    Thread.Sleep((int)sleep);

                tickcounter.Reset();
                framecounter.Stop();

                /* Calculate current FPS. */
                if (framecounter.ElapsedMilliseconds >= 1000)
                {
                    CurrentFPS = (float)count / (framecounter.ElapsedMilliseconds / 1000f);
                    framecounter.Reset();
                    count = 0;
                }
            }
        }

        protected void Tick()
        {
            var curframe = new Bitmap(DEFAULT_WIDTH, DEFAULT_HEIGHT);

            /* Draw on curframe */

            /* Dispose current frame, set new, and fire event. */
            //if (CurrentFrame != null)
                //CurrentFrame.Dispose();
            CurrentFrame = curframe;
            if (OnNewFrame != null)
                OnNewFrame(this, new FrameEventArgs() { Frame = CurrentFrame });
        }

        public void Load()
        {

        }

        public void Start()
        {
            if (IsRunning)
                throw new Exception("Engine already running, cannot start.");
            IsRunning = true;
            RunningThread = new Thread(new ThreadStart(() => ThreadRun()));
            RunningThread.Start();
        }

        public void Stop()
        {
            if (!IsRunning)
                throw new Exception("Engine not running, cannot stop.");
            IsRunning = false;
        }

        public void Dispose()
        {
            if (IsRunning)
                Stop();
        }
    }
}