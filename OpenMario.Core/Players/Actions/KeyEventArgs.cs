using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMario.Core.Players.Actions
{
    public class KeyEventArgs : EventArgs
    {
        public KeyMapping KeyMapping { get; set; }
    }
}
