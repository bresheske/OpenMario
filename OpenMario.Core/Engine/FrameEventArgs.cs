using System;
using System.Drawing;

namespace OpenMario.Core.Engine
{
    public class FrameEventArgs : EventArgs
    {
        public Bitmap Frame { get; set; }
    }
}
