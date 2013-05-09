using OpenMario.Core.Actors;
using OpenMario.Core.Actors.Concrete;
using OpenMario.Core.Engine;
using OpenMario.Core.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenMario
{
    public partial class MainForm : Form
    {
        Engine _engine;
        Core.Environment.Environment _environment;

        public MainForm()
        {
            InitializeComponent();
            _environment = LoadDefaultEnvironment();

            _engine = new Engine();
            _engine.Load(this, null);
            _engine.OnNewFrame += (o, e) => Tick(e);
            FormClosing += (o, e) => _engine.Dispose();
            _engine.Start();
        }

        public Core.Environment.Environment LoadDefaultEnvironment()
        {
            var e = new Core.Environment.Environment();
            e.Actors.Add(new FallingBox());
            e.Players.Add(new PlayerOne());
            e.RegisterAllKeys(this);
            return e;
        }

        public void Tick(FrameEventArgs e)
        {
            using (var g = Graphics.FromImage(e.Frame))
            {
                DrawDebug(g);

                _environment.Update();
                _environment.Render(g);
                g.Flush();
            }
            
            this.Invoke((Action)(() =>
            {
                canvas.Image = e.Frame;
            }));
        }

        public void DrawDebug(Graphics g)
        {
            g.DrawString(
                string.Format("FPS: {0}", _engine.CurrentFPS),
                new Font("Serif", 12, FontStyle.Bold),
                Brushes.Aqua,
                new PointF(5, 5));

            if (_environment.Players[0].IsActionPressed(new Core.Players.Actions.KeyMapping() { Action = Core.Players.Actions.KeyMapping.KeyAction.SHOOT }))
                g.FillEllipse(Brushes.Aquamarine, new Rectangle(50, 80, 8, 8));
            else
                g.FillEllipse(Brushes.Red, new Rectangle(50, 80, 8, 8));

            if (_environment.Players[0].IsActionPressed(new Core.Players.Actions.KeyMapping() { Action = Core.Players.Actions.KeyMapping.KeyAction.UP }))
                g.FillRectangle(Brushes.Aquamarine, new Rectangle(50, 100, 8, 15));
            else
                g.FillRectangle(Brushes.Red, new Rectangle(50, 100, 8, 15));

            if (_environment.Players[0].IsActionPressed(new Core.Players.Actions.KeyMapping() { Action = Core.Players.Actions.KeyMapping.KeyAction.DOWN }))
                g.FillRectangle(Brushes.Aquamarine, new Rectangle(50, 120, 8, 15));
            else
                g.FillRectangle(Brushes.Red, new Rectangle(50, 120, 8, 15));

            if (_environment.Players[0].IsActionPressed(new Core.Players.Actions.KeyMapping() { Action = Core.Players.Actions.KeyMapping.KeyAction.RIGHT }))
                g.FillRectangle(Brushes.Aquamarine, new Rectangle(61, 113, 15, 8));
            else
                g.FillRectangle(Brushes.Red, new Rectangle(61, 113, 15, 8));

            if (_environment.Players[0].IsActionPressed(new Core.Players.Actions.KeyMapping() { Action = Core.Players.Actions.KeyMapping.KeyAction.LEFT }))
                g.FillRectangle(Brushes.Aquamarine, new Rectangle(32, 113, 15, 8));
            else
                g.FillRectangle(Brushes.Red, new Rectangle(32, 113, 15, 8));

            g.Flush();
        }
    }
}