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

        private object _lock;

        private DateTime? PreviousStep;

        static ValorEngine()
        {
            init();
        }

        public static void init()
        {
            Scale = (float)Double.Parse(ConfigurationManager.AppSettings["scale"]);
            FontCollection = new PrivateFontCollection();
            Vfps = Double.Parse(ConfigurationManager.AppSettings["vfps"]);
            Cfps = Double.Parse(ConfigurationManager.AppSettings["cfps"]);
            var FontFileName = String.Format("..\\..\\{0}", ConfigurationManager.AppSettings["FontFile"]);
            if (File.Exists(FontFileName))
            {
                try
                {
                    FontCollection.AddFontFile(FontFileName);
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
                if (this.PreviousStep.HasValue)
                {
                    Mode.Step((time - this.PreviousStep.Value).TotalSeconds);
                }
                this.PreviousStep = time;
            }
        }
    }
}
