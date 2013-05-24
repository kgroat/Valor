namespace Valor
{
    using System;
    using System.Drawing;

    using Valor.Physics.Vector;

    public static class GraphicsHelper
    {
        private static Random _Rand = new Random();

        public static Random Rand
        {
            get
            {
                return _Rand;
            }
        }

        public static int Clamp(int min, int val, int max)
        {
            return Math.Max(min, Math.Min(val, max));
        }

        public static Color RandomColor(int high = 255, int low = 0, int alpha = 255)
        {
            int[] rgb = new int[3];
            int h, l;
            h = Rand.Next(3);
            l = Rand.Next(3);
            while (l == h) { l = Rand.Next(3); }
            rgb[h] = high;
            rgb[l] = low;
            rgb[3 - (h + l)] = Rand.Next(high - low) + low;
            return Color.FromArgb(alpha, rgb[0], rgb[1], rgb[2]);
        }

        public static Color Step(Color input, int steps = 1)
        {
            int min = Math.Min(input.R, Math.Min(input.G, input.B));
            int max = Math.Max(input.R, Math.Max(input.G, input.B));
            if (min == max)
            {
                return input;
            }

            int FULL = 6 * (max - min);

            if (steps < 0)
            {
                steps = (-steps) % FULL;
                steps = FULL - steps;
            }
            else
            {
                steps = steps % FULL;
            }

            int r = input.R, g = input.G, b = input.B;
            while (steps > 0)
            {
                if (r == max)
                {
                    if (b > min)
                    {
                        if (b - min > steps)
                        {
                            b -= steps;
                            steps = 0;
                        }
                        else
                        {
                            steps -= b;
                            b = min;
                        }
                    }
                    else if (g < max)
                    {
                        if (max - g > steps)
                        {
                            g += steps;
                            steps = 0;
                        }
                        else
                        {
                            steps -= max - g;
                            g = max;
                        }
                    }
                }
                if (g == max)
                {
                    if (r > min)
                    {
                        if (r - min > steps)
                        {
                            r -= steps;
                            steps = 0;
                        }
                        else
                        {
                            steps -= r;
                            r = min;
                        }
                    }
                    else if (b < max)
                    {
                        if (max - b > steps)
                        {
                            b += steps;
                            steps = 0;
                        }
                        else
                        {
                            steps -= max - b;
                            b = max;
                        }
                    }
                }
                if (b == max)
                {
                    if (g > min)
                    {
                        if (g - min > steps)
                        {
                            g -= steps;
                            steps = 0;
                        }
                        else
                        {
                            steps -= g;
                            g = min;
                        }
                    }
                    else if (r < max)
                    {
                        if (max - r > steps)
                        {
                            r += steps;
                            steps = 0;
                        }
                        else
                        {
                            steps -= max - r;
                            r = max;
                        }
                    }
                }
            }
            return Color.FromArgb(input.A, r, g, b);
        }

        public static Color CreateOpposite(Color input)
        {
            return Color.FromArgb(255 - input.R, 255 - input.G, 255 - input.B);
        }

        public static Color Rotate(Color input)
        {
            return Color.FromArgb(input.G, input.B, input.R);
        }

        public static void DrawLine(this Graphics g, Pen pen, Line line)
        {
            const float xMult = 1.5f;
            const float ratio = 2;
            g.DrawLine(pen, line.Start.X + line.Start.Z * xMult, line.Start.Y + line.Start.Z * xMult * ratio, line.End.X + line.End.Z * xMult, line.End.Y + line.End.Z * xMult * ratio);
        }

        public static void DrawRay(this Graphics g, Pen pen, Ray line)
        {
            g.DrawLine(pen, line.Start.X, line.Start.Y, line.Direction.X * g.ClipBounds.Width * 2, line.Direction.Y * g.ClipBounds.Height * 2);
        }
    }
}
