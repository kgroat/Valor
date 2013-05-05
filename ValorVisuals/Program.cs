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
    using Llamanim.Anim;
    using Llamanim.IO;

    static class Program
    {
        static ValorForm f;
        static TestMode tm;
        static LmaPackage package;

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
            var c2 = GraphicsHelper.RandomColor();
            var c1 = GraphicsHelper.Rotate(c2);
            var cmin = Int32.Parse(ConfigurationManager.AppSettings["cycleMin"]);
            var cmax = Int32.Parse(ConfigurationManager.AppSettings["cycleMax"]);
            var c3 = Color.FromArgb(cmin, cmin, cmax);
            var pwd = Directory.GetCurrentDirectory();
            var c4 = GraphicsHelper.CreateOpposite(c3);
            var c4Set = false;

            if (ConfigurationManager.AppSettings["hardCodedColors"] == "True")
            {
                c1 = ParseColor(ConfigurationManager.AppSettings["redReplacementColor"]);
                c2 = ParseColor(ConfigurationManager.AppSettings["greenReplacementColor"]);
                c3 = ParseColor(ConfigurationManager.AppSettings["lightingColor"]);
                c4 = ParseColor(ConfigurationManager.AppSettings["darknessColor"]);
                c4Set = true;
            }

            package = new LmaPackage(@"D:\awesome");
            tm.Image = package.GetAllScripts()[0];
            ticker.Start();
            Application.Run(f);
        }

        static Color ParseColor(string input)
        {
            var parts = input.Split(',').Select(each => Int32.Parse(each.Trim())).ToArray();
            return Color.FromArgb(parts[0], parts[1], parts[2]);
        }
    }
}
