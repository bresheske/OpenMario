//-----------------------------------------------------------------------
// <copyright file="MainForm.cs" company="brpeanut">
//     Copyright (c), brpeanut. All rights reserved.
// </copyright>
// <summary> The main form for interacting with OpenMario. </summary>
// <author> brpeanut/OpenMario - https://github.com/brpeanut/OpenMario </author>
//-----------------------------------------------------------------------

namespace OpenMario
{
    using OpenMario.Core.Actors.Concrete;
    using OpenMario.Core.Engine;
    using OpenMario.Core.Players;
    using OpenMario.Environments.OnePlayerEnvironments;
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using VectorClass;

    /// <summary>
    /// The mian window for interacting with OpenMario.
    /// </summary>
    public partial class MainForm : Form
    {
        Engine _engine;

        Core.Environment.Environment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            _environment = new LevelOne();
            _environment.Load();
            _environment.RegisterAllKeys(this);

            _engine = new Engine();
            _engine.Load(this, null);
            _engine.OnNewFrame += (o, e) => Tick(e);
            FormClosing += (o, e) => _engine.Dispose();
            _engine.Start();
        }

        public void Tick(FrameEventArgs e)
        {
            using (var g = Graphics.FromImage(e.Frame))
            {
                _environment.Update();
                _environment.Render(g);
                DrawDebug(g);
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

            if (_environment.Players[0].IsActionPressed(new Core.Players.Actions.KeyMapping() { Action = Core.Players.Actions.KeyMapping.KeyAction.JUMP }))
                g.FillEllipse(Brushes.Aquamarine, new Rectangle(50, 60, 8, 8));
            else
                g.FillEllipse(Brushes.Red, new Rectangle(50, 60, 8, 8));

            if (_environment.Players[0].IsActionPressed(new Core.Players.Actions.KeyMapping() { Action = Core.Players.Actions.KeyMapping.KeyAction.UP }))
                g.FillRectangle(Brushes.Aquamarine, new Rectangle(50, 80, 8, 15));
            else
                g.FillRectangle(Brushes.Red, new Rectangle(50, 80, 8, 15));

            if (_environment.Players[0].IsActionPressed(new Core.Players.Actions.KeyMapping() { Action = Core.Players.Actions.KeyMapping.KeyAction.DOWN }))
                g.FillRectangle(Brushes.Aquamarine, new Rectangle(50, 100, 8, 15));
            else
                g.FillRectangle(Brushes.Red, new Rectangle(50, 100, 8, 15));

            if (_environment.Players[0].IsActionPressed(new Core.Players.Actions.KeyMapping() { Action = Core.Players.Actions.KeyMapping.KeyAction.RIGHT }))
                g.FillRectangle(Brushes.Aquamarine, new Rectangle(61, 93, 15, 8));
            else
                g.FillRectangle(Brushes.Red, new Rectangle(61, 93, 15, 8));

            if (_environment.Players[0].IsActionPressed(new Core.Players.Actions.KeyMapping() { Action = Core.Players.Actions.KeyMapping.KeyAction.LEFT }))
                g.FillRectangle(Brushes.Aquamarine, new Rectangle(32, 93, 15, 8));
            else
                g.FillRectangle(Brushes.Red, new Rectangle(32, 93, 15, 8));

            g.Flush();
        }
    }
}