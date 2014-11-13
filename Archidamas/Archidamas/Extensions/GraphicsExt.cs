using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archidamas.Extensions
{
    public enum DrawLayer { Background, Floor, Feature, Actor, Effect, HUD, Menu, Foreground };

    public static class GraphicsExt
    {
        public static float GetDrawLayer(DrawLayer l, bool backToFront = true)
        {
            if (backToFront)
                switch (l)
                {
                    case DrawLayer.Background: return 1.0F;
                    case DrawLayer.Floor: return 0.9F;
                    case DrawLayer.Feature: return 0.8F;
                    case DrawLayer.Actor: return 0.7F;
                    case DrawLayer.Effect: return 0.6F;
                    case DrawLayer.HUD: return 0.5F;
                    case DrawLayer.Menu: return 0.4F;
                    case DrawLayer.Foreground: return 0.0F;
                    default: return 1.0F;
                }
            else
                switch (l)
                {
                    case DrawLayer.Background: return 0.0F;
                    case DrawLayer.Floor: return 0.1F;
                    case DrawLayer.Feature: return 0.2F;
                    case DrawLayer.Actor: return 0.3F;
                    case DrawLayer.Effect: return 0.4F;
                    case DrawLayer.HUD: return 0.5F;
                    case DrawLayer.Menu: return 0.6F;
                    case DrawLayer.Foreground: return 1.0F;
                    default: return 0.0F;
                }
        }
    }
}
