using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Archidamas.Extensions
{
    public static class PointExt
    {
        /// <summary>
        /// Translates the point to a 2d vector times a factor X.
        /// </summary>
        /// <param name="point">The point to translate.</param>
        /// <param name="factor">Optional factor to multiply ints by.</param>
        /// <returns>Vector2</returns>
        public static Vector2 ToVector2(this Point point, float factor = 1)
        {
            return new Vector2(factor * point.X, factor * point.Y);
        }

        public static Point Clone(this Point point)
        {
            return new Point(point.X, point.Y);
        }

        public static Point PointFromVector2(Vector2 vector, float factor = 1F)
        {
            return new Point((int)(vector.X * factor), (int)(vector.Y * factor));
        }
    }
}
