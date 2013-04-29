using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Valor
{
    public class GraphicsBindingContext
    {
        private Timer _GraphicsTimer, _PhysicsTimer;

        public IntPtr? HWnd { get; private set; }

        protected ValorForm Form { get; private set; }

        protected int Width
        {
            get
            {
                return this.Form.Width;
            }
        }

        protected int Height
        {
            get
            {
                return this.Form.Height;
            }
        }

        public double RenderInterval
        {
            get
            {
                return this._GraphicsTimer.Interval;
            }
            set
            {
                this._GraphicsTimer.Interval = value;
            }
        }

        public ValorEngine RenderContext
        {
            get
            {
                return Form.Engine;
            }
        }

        public double PhysicsInterval
        {
            get
            {
                return this._PhysicsTimer.Interval;
            }
            set
            {
                this._PhysicsTimer.Interval = value;
            }
        }

        public GraphicsBindingContext(ValorForm form, double RenderInterval = 0, double PhysicsInterval = 0)
        {
            if (form == null)
            {
                throw new ArgumentNullException("The Form supplied was null.  BindingContext must reference either an Image or a Form.");
            }
            if (RenderInterval <= 0)
            {
                RenderInterval = 1000 / ValorEngine.VFPS;
            }
            if (PhysicsInterval <= 0)
            {
                PhysicsInterval = 1000 / ValorEngine.CFPS;
            }
            this.Form = form;
            this.HWnd = this.Form.Handle;
            this._GraphicsTimer = new Timer();
            this._GraphicsTimer.Interval = RenderInterval;
            this._GraphicsTimer.Elapsed += _GraphicsTimer_Tick;
            this._PhysicsTimer = new Timer();
            this.PhysicsInterval = PhysicsInterval;
            this._PhysicsTimer.Elapsed += _PhysicsTimer_Tick;
        }

        public System.Drawing.Graphics CreateGraphics()
        {
            Graphics output = null;
            output = System.Drawing.Graphics.FromHwnd(this.HWnd.Value);
            output.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            return output;
        }

        public Rectangle Viewport()
        {
            return new Rectangle(0, 0, this.Form.Width, this.Form.Height);
        }

        public bool Start()
        {
            var ret = true;
            if (this._GraphicsTimer.Enabled || this._PhysicsTimer.Enabled)
            {
                ret = false;
            }
            this._GraphicsTimer.Enabled = true;
            this._PhysicsTimer.Enabled = true;
            return ret;
        }

        public bool Stop()
        {
            var ret = false;
            if (this._GraphicsTimer.Enabled || this._PhysicsTimer.Enabled)
            {
                ret = true;
            }
            this._GraphicsTimer.Enabled = false;
            this._PhysicsTimer.Enabled = false;
            return ret;
        }

        public void Render()
        {
            if (this.RenderContext != null)
            {
                try
                {
                    using (var g = this.CreateGraphics())
                    {
                        this.Form.Invalidate();
                    }
                }
                catch { }
            }
        }

        private void _GraphicsTimer_Tick(object sender, ElapsedEventArgs e)
        {
            this.Render();
        }

        private void _PhysicsTimer_Tick(object sender, ElapsedEventArgs e)
        {
            this.RenderContext.Step(e.SignalTime);
        }
    }
}
