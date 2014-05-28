using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Archidamas.Extensions
{
    public static class Texture2DExt
    {
        public static Vector2 CenterVector(this Texture2D texture)
        {
            return new Vector2(texture.Width/2, texture.Height/2);
        }
    }
}
