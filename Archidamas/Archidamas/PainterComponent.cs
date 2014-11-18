using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Archidamas
{
    /// <summary>
    /// Provides painting services to the game.
    /// </summary>
    public interface IPainterService
    {
        void registerPaintable(IPaintable obj);
    }

    public class PainterComponent
    {
    }

    /// <summary>
    /// Defines an object which is 'paintable'
    /// </summary>
    public interface IPaintable
    {
    }
}
