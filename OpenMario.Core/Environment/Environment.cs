using OpenMario.Core.Players;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMario.Core.Environment
{
    public class Environment
    {
        public Point StartingPosition { get; set; }
        public List<BasePlayer> Players { get; set; }
    }
}