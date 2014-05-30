using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ActionRPG
{
    enum EnumMapObjType { Empty, Terrain, Portal };

    interface IMapFeature
    {
        EnumMapObjType Type { get; }
        bool Passable { get; }
        bool Collidable { get; }
        Rectangle Area { get; }
        string Name { get; }
    }

    class CaveEntrance : IMapFeature
    {
        const string NAME = "Cave";
        const EnumMapObjType TYPE = EnumMapObjType.Portal;
        const bool PASS = true;
        const bool COLLIDE = true;
        const int WIDTH = 32;
        const int HEIGHT = 32;
        Rectangle area = Rectangle.Empty;

        //Constructor
        public CaveEntrance(Point location) : this(location.X, location.Y) {}
        public CaveEntrance(int x, int y) 
        {
            this.area = new Rectangle(x, y, WIDTH, HEIGHT);
        }

        //IMapObj implementation
        string IMapFeature.Name { get { return NAME; } }
        EnumMapObjType IMapFeature.Type { get { return TYPE; }}
        bool IMapFeature.Passable { get { return PASS; }}
        Rectangle IMapFeature.Area { get { return this.area; }}
        bool IMapFeature.Collidable { get { return COLLIDE; }}
    }

    class EntryPoint : IMapFeature
    {
        const string NAME = "EntryPoint";
        const EnumMapObjType TYPE = EnumMapObjType.Empty;
        const bool PASS = true;
        const bool COLLIDE = true;
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
        bool IMapFeature.Collidable { get { return COLLIDE; } }
    }

    #region Terrain
    class Mountain : IMapFeature
    {
        const string NAME = "Mountain";
        const EnumMapObjType TYPE = EnumMapObjType.Terrain;
        const bool PASS = false;
        const bool COLLIDE = true;
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
        bool IMapFeature.Collidable { get { return COLLIDE; } }
    }
    #endregion
}
