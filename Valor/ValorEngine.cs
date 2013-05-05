using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Valor
{
    public class ValorEngine
    {
        public static float Scale { get; private set; }

        public static PrivateFontCollection FontCollection { get; private set; }

        public static Font Font { get; private set; }

        public static double Vfps { get; private set; }

        public static double Cfps { get; private set; }

        private readonly object _lock;

        private DateTime? previousStep;

        static ValorEngine()
        {
            Init();
        }

        public static void Init()
        {
            Scale = (float)Double.Parse(ConfigurationManager.AppSettings["scale"]);
            FontCollection = new PrivateFontCollection();
            Vfps = Double.Parse(ConfigurationManager.AppSettings["vfps"]);
            Cfps = Double.Parse(ConfigurationManager.AppSettings["cfps"]);
            var fontFileName = ConfigurationManager.AppSettings["FontFile"];
            if (File.Exists(fontFileName))
            {
                try
                {
                    FontCollection.AddFontFile(fontFileName);
                    Font = new Font(FontCollection.Families[0], 1f);
                }
                catch
                {
                    Font = new Font(ConfigurationManager.AppSettings["Font"], 1);
                }
            }
            else
            {
                Font = new Font(ConfigurationManager.AppSettings["Font"], 1);
            }
        }

        public ValorEngine()
        {
            this._lock = new object();
        }

        public GameMode Mode { get; set; }

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
                if (this.previousStep.HasValue)
                {
                    Mode.Step((time - this.previousStep.Value).TotalSeconds);
                }
                this.previousStep = time;
            }
        }
    }
}
