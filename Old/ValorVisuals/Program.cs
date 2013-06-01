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
        static LmaPackage package;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            f = new ValorForm {Engine = new ValorEngine()};
            var ticker = new GraphicsBindingContext(f);
            ticker.Start();
            Application.Run(f);
        }
    }
}
