using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valor
{
    using Microsoft.Xna.Framework.Input;

    public static class InputHelper
    {
        public static KeyboardState Keyboard { get; set; }
        public static MouseState Mouse { get; set; }
        public static GamePadState GamePad { get; set; }
    }
}
