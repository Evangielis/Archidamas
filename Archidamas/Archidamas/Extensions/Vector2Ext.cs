using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Archidamas.Extensions;

namespace Archidamas.Extensions
{
    public static class Vector2Ext
    {
        public static Vector2 Clone(this Vector2 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        public static Point ToPoint(this Vector2 vector, int multiplier = 1, int divisor = 1)
        {
            int x = ((int)vector.X * multiplier)/divisor;
            int y = ((int)vector.Y * multiplier)/divisor;
            return new Point(x, y);
        }
    }
}
