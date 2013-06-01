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

        public ValorForm Form { get; private set; }

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
                RenderInterval = 1000 / ValorEngine.Vfps;
            }
            if (PhysicsInterval <= 0)
            {
                PhysicsInterval = 1000 / ValorEngine.Cfps;
            }
            this.Form = form;
            this._GraphicsTimer = new Timer { Interval = RenderInterval };
            this._GraphicsTimer.Elapsed += _GraphicsTimer_Tick;
            this._PhysicsTimer = new Timer() { Interval = PhysicsInterval };
            this._PhysicsTimer.Elapsed += _PhysicsTimer_Tick;
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
            if (!this.Form.IsDisposed && this.RenderContext != null)
            {
                this.Form.Invalidate();
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
