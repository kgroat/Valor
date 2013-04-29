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
        public static readonly float SCALE = (float)Double.Parse(ConfigurationManager.AppSettings["scale"]);
        public static readonly PrivateFontCollection FONT_COLLECTION = new PrivateFontCollection();
        public static readonly Font FONT;
        public static readonly double VFPS = Double.Parse(ConfigurationManager.AppSettings["vfps"]);
        public static readonly double CFPS = Double.Parse(ConfigurationManager.AppSettings["cfps"]);

        private object _lock;

        private DateTime? PreviousStep;

        static ValorEngine()
        {
            var fontFileName = String.Format("..\\..\\{0}", ConfigurationManager.AppSettings["fontFile"]);
            if (File.Exists(fontFileName))
            {
                try
                {
                    FONT_COLLECTION.AddFontFile(fontFileName);
                    FONT = new Font(FONT_COLLECTION.Families[0], 1f);
                }
                catch
                {
                    FONT = new Font(ConfigurationManager.AppSettings["font"], 1);
                }
            }
            else
            {
                FONT = new Font(ConfigurationManager.AppSettings["font"], 1);
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
