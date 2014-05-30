using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Archidamas.Extensions
{
    public static class Vector2Ext
    {
        public static Vector2 Clone(this Vector2 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }
    }
}
