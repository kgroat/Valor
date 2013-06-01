using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using Llamanim.IO;
using Valor.Physics.Vector;

namespace Valor
{
    public class ValorEngine
    {
        public static Dictionary<string, LmaPackage> Packages { get; private set; }

        public static PrivateFontCollection FontCollection { get; private set; }

        public static Vector CenterOfViewInSpace { get; set; }

        public static float Scale { get; private set; }

        public static Font Font { get; private set; }

        public static double Vfps { get; private set; }

        public static double Cfps { get; private set; }

        public static int Width { get; set; }

        public static int Height { get; set; }

        private readonly object _lock;

        private DateTime? _previousStep;

        private GameMode _mode;

        static ValorEngine()
        {
            Init(Form.ActiveForm);
        }

        public static void Init(Form form)
        {
            if (Packages == null)
            {
                Packages = new Dictionary<string, LmaPackage>();

                var package = new LmaPackage(ConfigurationManager.AppSettings["mainPackage"]);
                ValorEngine.Packages.Add("main", package);
            }
            if (FontCollection == null)
            {
                FontCollection = new PrivateFontCollection();
            }

            Scale = (float)Double.Parse(ConfigurationManager.AppSettings["scale"]);
            Width = (int)(form.Width / Scale) + 1;
            Height = (int)(form.Height / Scale) + 1;
            Vfps = Double.Parse(ConfigurationManager.AppSettings["vfps"]);
            Cfps = Double.Parse(ConfigurationManager.AppSettings["cfps"]);
            var fontFileName = ConfigurationManager.AppSettings["fontFile"];
            if (File.Exists(fontFileName))
            {
                try
                {
                    FontCollection.AddFontFile(fontFileName);
                    Font = new Font(FontCollection.Families[0], 1f);
                }
                catch
                {
                    Font = new Font(ConfigurationManager.AppSettings["font"], 1);
                }
            }
            else
            {
                Font = new Font(ConfigurationManager.AppSettings["font"], 1);
            }
        }

        public ValorEngine()
        {
            this._lock = new object();
        }

        public GameMode Mode
        {
            get { return _mode; }
            set
            {
                lock (_lock)
                {
                    _mode = value;
                }
            }
        }

        public void Render(Graphics g, float width, float height)
        {
            lock (_lock)
            {
                Mode.Render(g, width, height);
            }
        }

        public void Step(DateTime time)
        {
            lock (_lock)
            {
                if (this._previousStep.HasValue && Mode != null)
                {
                    Mode.Step((float)(time - this._previousStep.Value).TotalSeconds);
                }
                this._previousStep = time;
            }
        }
    }
}
