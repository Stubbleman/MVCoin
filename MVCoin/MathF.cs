using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace MVCoin
{
    class MathF
    {
        public const float PI = (float)Math.PI;

        public static float Abs(float a)
        {
            float b = (float)Math.Abs(a);
            return b;
        }

        public static float Cos(float a)
        {
            float b = (float)Math.Cos(a);
            return b;
        }

        public static float Sin(float a)
        {
            float b = (float)Math.Sin(a);
            return b;
        }

        public static float Pow(float a, float b)
        {
            float c = (float)Math.Pow((double)a, (double)b);
            return c;
        }

        public static float Sqrt(float a)
        {
            float b = (float)Math.Sqrt((double)a);
            return b;
        }

        public static float Floor(float a)
        {
            float b = (float)Math.Floor(a);
            return b;
        }

        public static float Ceiling(float a)
        {
            float b = (float)Math.Ceiling(a);
            return b;
        }

        public static float Round(float a)
        {
            float b = (float)Math.Round(a);
            return b;
        }

        public static float RSS(float x, float y) // root-square-sum
        {
            return (float)Math.Sqrt(Math.Pow(x - y, 2) + Math.Pow(x - y, 2));
        }

        public static Point Rotate(Point oldPoint, Point centerPoint, float angleA)
        {
            float angleR = angleA / 180 * PI;
            oldPoint.Offset(-centerPoint.X, -centerPoint.Y); // Set center to zero point

            // Calculate rotation
            Point newPoint = new Point((int)(oldPoint.X * Cos(angleR) + oldPoint.Y * Sin(angleR))
                , (int)(oldPoint.Y * Cos(angleR) - oldPoint.X * Sin(angleR)));
            newPoint.Offset(centerPoint.X, centerPoint.Y); // Offset back by original center
            return newPoint;
        }
    }
}
