using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ActionRPG
{
    interface IMapFeature
    {
        EnumMapObjType Type { get; }
        bool Passable { get; }
        Rectangle Area { get; }
        string Name { get; }
    }

    class EntryPoint : IMapFeature
    {
        const string NAME = "EntryPoint";
        const EnumMapObjType TYPE = EnumMapObjType.Empty;
        const bool PASS = true;
        const int WIDTH = 32;
        const int HEIGHT = 32;
        Rectangle area = Rectangle.Empty;

        //Constructor
        public EntryPoint(Point location) : this(location.X, location.Y) {}
        public EntryPoint(int x, int y) 
        {
            this.area = new Rectangle(x, y, WIDTH, HEIGHT);
        }

        //IMapObj implementation
        string IMapFeature.Name { get { return NAME; } }
        EnumMapObjType IMapFeature.Type { get { return TYPE; }}
        bool IMapFeature.Passable { get { return PASS; }}
        Rectangle IMapFeature.Area { get { return this.area; } }
    }

    #region Terrain
    class Mountain : IMapFeature
    {
        const string NAME = "Mountain";
        const EnumMapObjType TYPE = EnumMapObjType.Terrain;
        const bool PASS = false;
        const int WIDTH = 128;
        const int HEIGHT = 128;
        Rectangle area = Rectangle.Empty;

        //Constructor
        public Mountain(Point location) : this(location.X, location.Y) {}
        public Mountain(int x, int y) 
        {
            this.area = new Rectangle(x, y, WIDTH, HEIGHT);
        }

        //IMapObj implementation
        string IMapFeature.Name { get { return NAME; } }
        EnumMapObjType IMapFeature.Type { get { return TYPE; } }
        bool IMapFeature.Passable { get { return PASS; } }
        Rectangle IMapFeature.Area { get { return this.area; } }
    }
    #endregion
}
