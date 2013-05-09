using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMario.Core.Players
{
    public class PlayerOne : BasePlayer
    {
        
        public override List<Actions.KeyMapping> GetKeyMappings()
        {
            return new List<Actions.KeyMapping>()
            { 
                new Actions.KeyMapping()
                    { Action = Actions.KeyMapping.KeyAction.UP, 
                      Key = System.Windows.Forms.Keys.Up,
                      PressType = Actions.KeyMapping.KeyPressType.DOWN,},
                new Actions.KeyMapping()
                    { Action = Actions.KeyMapping.KeyAction.UP, 
                      Key = System.Windows.Forms.Keys.Up,
                      PressType = Actions.KeyMapping.KeyPressType.UP,},

                new Actions.KeyMapping()
                    { Action = Actions.KeyMapping.KeyAction.DOWN, 
                      Key = System.Windows.Forms.Keys.Down,
                      PressType = Actions.KeyMapping.KeyPressType.DOWN,},
                new Actions.KeyMapping()
                    { Action = Actions.KeyMapping.KeyAction.DOWN, 
                      Key = System.Windows.Forms.Keys.Down,
                      PressType = Actions.KeyMapping.KeyPressType.UP,},

                new Actions.KeyMapping()
                    { Action = Actions.KeyMapping.KeyAction.RIGHT, 
                      Key = System.Windows.Forms.Keys.Right,
                      PressType = Actions.KeyMapping.KeyPressType.DOWN,},
                new Actions.KeyMapping()
                    { Action = Actions.KeyMapping.KeyAction.RIGHT, 
                      Key = System.Windows.Forms.Keys.Right,
                      PressType = Actions.KeyMapping.KeyPressType.UP,},

                new Actions.KeyMapping()
                    { Action = Actions.KeyMapping.KeyAction.LEFT, 
                      Key = System.Windows.Forms.Keys.Left,
                      PressType = Actions.KeyMapping.KeyPressType.DOWN,},
                new Actions.KeyMapping()
                    { Action = Actions.KeyMapping.KeyAction.LEFT, 
                      Key = System.Windows.Forms.Keys.Left,
                      PressType = Actions.KeyMapping.KeyPressType.UP,},
                
                new Actions.KeyMapping()
                    { Action = Actions.KeyMapping.KeyAction.SHOOT, 
                      Key = System.Windows.Forms.Keys.Space,
                      PressType = Actions.KeyMapping.KeyPressType.DOWN,},
                new Actions.KeyMapping()
                    { Action = Actions.KeyMapping.KeyAction.SHOOT, 
                      Key = System.Windows.Forms.Keys.Space,
                      PressType = Actions.KeyMapping.KeyPressType.UP,},
            };
        }
    }
}