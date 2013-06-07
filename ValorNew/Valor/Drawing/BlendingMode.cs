using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valor.Drawing
{
    public abstract class BlendingMode
    {
        public static BlendingMode AlphaComposite { get; private set; }

        public static BlendingMode Multiply { get; private set; }

        public static BlendingMode Screen { get; private set; }

        public static BlendingMode Overlay { get; private set; } // Unfinished

        public static BlendingMode ColorDodge { get; private set; }

        public static BlendingMode LinearDodge { get; private set; }

        public static BlendingMode ColorBurn { get; private set; }

        public static BlendingMode LinearBurn { get; private set; }

        public static BlendingMode DarkenOnly { get; private set; }

        public static BlendingMode LightenOnly { get; private set; }

        static BlendingMode()
        {
            AlphaComposite = new GenericBlendingMode((src, dst) =>
            {
                if (src.A == 255) return src;

                int outA, outR, outG, outB;
                outA = src.A + dst.A * (255 - src.A) / 255;
                outR = (src.R * src.A + dst.R * dst.A * (255 - src.A) / 255) / outA / 65025;
                outG = (src.G * src.A + dst.G * dst.A * (255 - src.A) / 255) / outA / 65025;
                outB = (src.B * src.A + dst.B * dst.A * (255 - src.A) / 255) / outA / 65025;
                return new Color(outR, outG, outB, outA);
            });

            Multiply = new GenericBlendingMode((src, dst) =>
            {
                if(src.A == 255) return new Color(src.R * dst.R / 255, src.G * dst.G / 255, src.B * dst.B / 255);

                // TODO: figure out how to make Multiply work with alpha
                int outA, outR, outG, outB;
                outA = src.A + dst.A * (255 - src.A) / 255;
                outR = src.R * dst.R / 255;
                outG = src.G * dst.G / 255;
                outB = src.B * dst.B / 255;
                return new Color(outR, outG, outB, outA);
            });

            Screen = new GenericBlendingMode((src, dst) =>
            {
                if (src.A == 255) return new Color(255 - (255 - src.R) * (255 - dst.R) / 255, 255 - (255 - src.G) * (255 - dst.G) / 255, 255 - (255 - src.B) * (255 - dst.B) / 255);

                // TODO: figure out how to make Screen work with alpha
                int outA, outR, outG, outB;
                outA = src.A + dst.A * (255 - src.A) / 255;
                outR = 255 - (255 - src.R) * (255 - dst.R) / 255;
                outG = 255 - (255 - src.G) * (255 - dst.G) / 255;
                outB = 255 - (255 - src.B) * (255 - dst.B) / 255;
                return new Color(outR, outG, outB, outA);
            });

            Screen = new GenericBlendingMode((src, dst) =>
            {
                throw new NotImplementedException("Screen blending mode incomplete");
                int outA, outR, outG, outB;
                outA = src.A + dst.A * (255 - src.A) / 255;
                outR = 255 - (255 - src.R) * (255 - dst.R) / 255;
                return new Color(outR, outG, outB, outA);
            });

            ColorDodge = new GenericBlendingMode((src, dst) =>
            {
                if (src.A == 255) return new Color(dst.R * 255 / (255 - src.R), dst.G * 255 / (255 - src.G), dst.B * 255 / (255 - src.B));

                // TODO: figure out how to make Color Dodge work with alpha
                int outA, outR, outG, outB;
                outA = src.A + dst.A * (255 - src.A) / 255;
                outR = dst.R * 255 / (255 - src.R);
                outG = dst.G * 255 / (255 - src.G);
                outB = dst.B * 255 / (255 - src.B);
                return new Color(outR, outG, outB, outA);
            });

            LinearDodge = new GenericBlendingMode((src, dst) =>
            {
                if (src.A == 255) return new Color(Math.Min(src.R + dst.R, 255), Math.Min(src.G + dst.G, 255), Math.Min(src.B + dst.B, 255));

                // TODO: figure out how to make Linear Dodge work with alpha
                int outA, outR, outG, outB;
                outA = src.A + dst.A * (255 - src.A) / 255;
                outR = Math.Min(src.R + dst.R, 255);
                outG = Math.Min(src.G + dst.G, 255);
                outB = Math.Min(src.B + dst.B, 255);
                return new Color(outR, outG, outB, outA);
            });

            ColorBurn = new GenericBlendingMode((src, dst) =>
            {
                if (src.A == 255) return new Color(255 - ((255 - dst.R) * 255 / src.R), 255 - ((255 - dst.G) * 255 / src.G), 255 - ((255 - dst.B) * 255 / src.B));

                // TODO: figure out how to make Color Burn work with alpha
                int outA, outR, outG, outB;
                outA = src.A + dst.A * (255 - src.A) / 255;
                outR = 255 - ((255 - dst.R) * 255 / src.R);
                outG = 255 - ((255 - dst.G) * 255 / src.G);
                outB = 255 - ((255 - dst.B) * 255 / src.B);
                return new Color(outR, outG, outB, outA);
            });

            LinearBurn = new GenericBlendingMode((src, dst) =>
            {
                if (src.A == 255) return new Color(Math.Max(src.R + dst.R - 255, 0), Math.Max(src.G + dst.G - 255, 0), Math.Max(src.B + dst.B - 255, 0));

                // TODO: figure out how to make Linear Burn work with alpha
                int outA, outR, outG, outB;
                outA = src.A + dst.A * (255 - src.A) / 255;
                outR = Math.Max(src.R + dst.R - 255, 0);
                outG = Math.Max(src.G + dst.G - 255, 0);
                outB = Math.Max(src.B + dst.B - 255, 0);
                return new Color(outR, outG, outB, outA);
            });

            LightenOnly = new GenericBlendingMode((src, dst) =>
            {
                if (src.A == 255) return new Color(Math.Max(src.R, dst.R), Math.Max(src.G, dst.G), Math.Max(src.B, dst.B));

                // TODO: figure out how to make Lighten Only work with alpha
                int outA, outR, outG, outB;
                outA = src.A + dst.A * (255 - src.A) / 255;
                outR = Math.Max(src.R, dst.R);
                outG = Math.Max(src.G, dst.G);
                outB = Math.Max(src.B, dst.B);
                return new Color(outR, outG, outB, outA);
            });

            DarkenOnly = new GenericBlendingMode((src, dst) =>
            {
                if (src.A == 255) return new Color(Math.Min(src.R, dst.R), Math.Min(src.G, dst.G), Math.Min(src.B, dst.B));

                // TODO: figure out how to make Linear Burn work with alpha
                int outA, outR, outG, outB;
                outA = src.A + dst.A * (255 - src.A) / 255;
                outR = Math.Min(src.R, dst.R);
                outG = Math.Min(src.G, dst.G);
                outB = Math.Min(src.B, dst.B);
                return new Color(outR, outG, outB, outA);
            });
        }

        public abstract Color Blend(Color src, Color dst);

        private class GenericBlendingMode : BlendingMode
        {
            private Func<Color, Color, Color> blend;

            public GenericBlendingMode(Func<Color, Color, Color> blend)
            {
                this.blend = blend;
            }

            public override Color Blend(Color src, Color dst)
            {
                return this.blend(src, dst);
            }
        }
    }
}
