using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Archidamas.Extensions
{
    public static class RectangleExt
    {
        public static void Offset(this Rectangle rect, Vector2 vector)
        {
            rect.Offset((int)vector.X, (int)vector.Y);
        }
    }
}
