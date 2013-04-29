using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Valor;

namespace ValorVisuals
{
    static class Program
    {
        static ValorForm f;
        static TestMode tm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            f = new ValorForm();
            f.Engine = new ValorEngine();
            f.Engine.Mode = tm = new TestMode();
            var ticker = new GraphicsBindingContext(f);
            var c1 = GraphicsHelper.RandomColor();
            var cmin = Int32.Parse(ConfigurationManager.AppSettings["cycleMin"]);
            var cmax = Int32.Parse(ConfigurationManager.AppSettings["cycleMax"]);
            var c2 = Color.FromArgb(cmin, cmin, cmax);
            var pwd = Directory.GetCurrentDirectory();
            tm.Image = new ColoredImage(new Bitmap(String.Format("..\\..\\{0}", ConfigurationManager.AppSettings["filename"])), GraphicsHelper.Rotate(c1), c1, c2);
            ticker.Start();
            Application.Run(f);
        }
    }
}
