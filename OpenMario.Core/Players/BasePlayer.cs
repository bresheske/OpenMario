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
        protected List<KeyMapping> CurrentKeys;

        public abstract List<KeyMapping> GetKeyMappings();
        public event EventHandler<OpenMario.Core.Players.Actions.KeyEventArgs> OnKeyUp;
        public event EventHandler<OpenMario.Core.Players.Actions.KeyEventArgs> OnKeyDown;

        public BasePlayer() { CurrentKeys = new List<KeyMapping>(); }

        public bool IsActionPressed(Core.Players.Actions.KeyMapping action)
        {
            return CurrentKeys.Contains(action);
        }

        public void RegisterKeyMappings(Form form)
        {
            var mappings = GetKeyMappings();
            form.KeyDown += (o, e) =>
                {
                    var m = mappings.FirstOrDefault(x => x.Key == e.KeyCode && x.PressType == KeyMapping.KeyPressType.DOWN);
                    if (m != null && !CurrentKeys.Contains(m))
                    {
                        CurrentKeys.Add(m);
                        if (OnKeyDown != null)
                            OnKeyDown(this, new Actions.KeyEventArgs() { KeyMapping = m });
                    }
                };
            form.KeyUp += (o, e) =>
            {
                var m = mappings.FirstOrDefault(x => x.Key == e.KeyCode && x.PressType == KeyMapping.KeyPressType.UP);
                if (m != null && CurrentKeys.Contains(m))
                {
                    CurrentKeys.Remove(m);
                    if (OnKeyUp != null)
                        OnKeyUp(this, new Actions.KeyEventArgs() { KeyMapping = m });
                }
            };
        }
    }
}