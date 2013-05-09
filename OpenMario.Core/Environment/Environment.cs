using OpenMario.Core.Actors;
using OpenMario.Core.Players;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenMario.Core.Environment
{
    public class Environment
    {
        public Point StartingPosition { get; set; }
        public List<BasePlayer> Players { get; set; }
        public List<BaseActor> Actors { get; set; }

        public Environment()
        {
            Players = new List<BasePlayer>();
            Actors = new List<BaseActor>();
        }

        public void RegisterAllKeys(Form f)
        {
            foreach (var p in Players)
                p.RegisterKeyMappings(f);
        }

        public void Update()
        {
            foreach (var a in Actors)
                a.Update(Actors);
        }

        public void Render(Graphics g)
        {
            foreach (var a in Actors)
                a.Draw(g);
        }

        public void Load()
        {
            //TODO: Don't just load all.
            foreach (var a in Actors)
                a.Load();
        }
    }
}