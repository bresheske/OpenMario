using OpenMario.Core.Engine;
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

        public MainForm()
        {
            InitializeComponent();
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
                g.DrawString(
                    string.Format("FPS: {0}", _engine.CurrentFPS), 
                    new Font("Serif", 12, FontStyle.Bold), 
                    Brushes.Aqua, 
                    new PointF(5, 5));
                g.Flush();
            }
            
            this.Invoke((Action)(() =>
            {
                canvas.Image = e.Frame;
            }));
        }
    }
}