using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenMario.Core.Players.Actions
{
    public class KeyMapping
    {
        public enum KeyAction
        {
            LEFT,
            UP,
            RIGHT,
            DOWN,
            JUMP,
            SHOOT,
            RUN,
        }
        public enum KeyPressType
        {
            DOWN,
            UP
        }

        public Keys Key { get; set; }
        public KeyAction Action { get; set; }
        public KeyPressType PressType { get; set; }
    }
}
