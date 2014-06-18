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

        public static void SetLocFromVector2(this Rectangle rect, Vector2 loc)
        {
            rect.Location = loc.ToPoint();
        }

        public static Rectangle FromVector2(Vector2 topleft, Vector2 diagonal)
        {
            return new Rectangle((int)topleft.X, (int)topleft.Y, (int)diagonal.X, (int)diagonal.Y);
        }

        public static Rectangle FromVector2(Vector2 topleft, int h, int w)
        {
            return new Rectangle((int)topleft.X, (int)topleft.Y, h, w);
        }
    }
}
