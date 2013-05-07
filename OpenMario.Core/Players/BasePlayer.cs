using OpenMario.Core.Players.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenMario.Core.Players
{
    public abstract class BasePlayer
    {
        public abstract List<KeyMapping> GetKeyMappings();
        public event EventHandler<OpenMario.Core.Players.Actions.KeyEventArgs> OnKeyUp;
        public event EventHandler<OpenMario.Core.Players.Actions.KeyEventArgs> OnKeyDown;

        public void RegisterKeyMappings(Form form)
        {
            var mappings = GetKeyMappings();
            form.KeyDown += (o, e) =>
                {
                    var m = mappings.FirstOrDefault(x => x.Key == e.KeyCode && x.PressType == KeyMapping.KeyPressType.DOWN);
                    if (m != null && OnKeyDown != null)
                        OnKeyDown(this, new Actions.KeyEventArgs() { KeyMapping = m });
                };
            form.KeyUp += (o, e) =>
            {
                var m = mappings.FirstOrDefault(x => x.Key == e.KeyCode && x.PressType == KeyMapping.KeyPressType.UP);
                if (m != null && OnKeyUp != null)
                    OnKeyUp(this, new Actions.KeyEventArgs() { KeyMapping = m });
            };
        }
    }
}