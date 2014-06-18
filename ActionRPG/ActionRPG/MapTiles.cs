using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Archidamas.Extensions;

namespace ActionRPG
{
    enum EnumMapObjType { Empty, Generic, Terrain, Meta, Surface };
    [Flags] enum FlagsMapFeature
    {
        Impassable = 1,
        Triggered = 2,
    };

    interface IMapTile
    {
        string Name { get; }
        EnumMapObjType Type { get; }
        
        Vector2 Loc { get; }
        Rectangle Area { get; }
        void SetLoc(Vector2 loc);

        bool Impassable { get; }
        bool Collidable { get; }
        bool Triggered { get; }
    }

    class MapFeatureComponent : GameComponent
    {
        public MapFeatureComponent(Game game) : base(game)
        {
            game.Components.Add(this);
        }
    }

    abstract class MapTile : IMapTile
    {
        protected string Name { get; set; }
        protected EnumMapObjType Type { get; set; }
        private Vector2 Loc { get; set; }
        private Rectangle Area { get; set; }
        protected FlagsMapFeature _featureFlags;

        //Constructors
        public MapTile()
        {
            this.Name = String.Empty;
            this.Type = EnumMapObjType.Empty;
            this.Loc = Vector2.Zero;
            this.Area = Rectangle.Empty;
            this._featureFlags = 0;
        }
        public MapTile(float x, float y) : this()
        {
            this.Loc = new Vector2(x,y);
            this.Area = RectangleExt.FromVector2(this.Loc, 32, 32);
        }

        protected virtual void SetLoc(Vector2 loc)
        {
            this.Loc = loc;
            this.Area.SetLocFromVector2(loc);
        }

        protected bool FlagTest(FlagsMapFeature f)
        {
            return this._featureFlags == (this._featureFlags | f);
        }
        protected void AddFlag(FlagsMapFeature f)
        {
            this._featureFlags = (this._featureFlags | f);
        }

        //IMapFeature implementation
        EnumMapObjType IMapTile.Type { get { return this.Type; } }
        Vector2 IMapTile.Loc { get { return this.Loc; } }
        Rectangle IMapTile.Area { get { return this.Area; } }
        string IMapTile.Name { get { return this.Name; } }
        bool IMapTile.Impassable { get { return this.FlagTest(FlagsMapFeature.Impassable); } }
        bool IMapTile.Collidable 
        { 
            get { return this.FlagTest(FlagsMapFeature.Impassable | FlagsMapFeature.Triggered); }
        }
        bool IMapTile.Triggered { get { return this.FlagTest(FlagsMapFeature.Triggered); } }

        void IMapTile.SetLoc(Vector2 loc)
        {
            this.SetLoc(loc);
        }
    }

    //TILES
    #region Surface Tiles
    class SurfaceTile : MapTile
    {
        //Constructor
        public SurfaceTile(Point location) : this(location.X, location.Y) {}
        public SurfaceTile(int x, int y) : base(x,y)
        {
            this.Name = "Generic Portal";
            this.Type = EnumMapObjType.Generic;
        }
    }
    class GrassTile : SurfaceTile
    {
        //Constructor
        public GrassTile(Point location) : this(location.X, location.Y) {}
        public GrassTile(int x, int y) : base(x,y)
        {
            this.Name = "Grass";
            this.Type = EnumMapObjType.Surface;
        }
    }
    #endregion

    #region Portals
    class PortalTile : MapTile
    {
        //Constructor
        public PortalTile(Point location) : this(location.X, location.Y) {}
        public PortalTile(int x, int y) : base(x,y)
        {
            this.Name = "Generic Portal";
            this.Type = EnumMapObjType.Generic;
            this.AddFlag(FlagsMapFeature.Triggered);
        }
    }
    class CaveEntranceTile : PortalTile
    {
        //Constructor
        public CaveEntranceTile(Point location) : this(location.X, location.Y) {}
        public CaveEntranceTile(int x, int y) : base(x,y) 
        { 
            this.Name = "Cave Entrance";
            this.Type = EnumMapObjType.Terrain;
        }
    }
    #endregion

    #region MetaTiles
    class NullTile : MapTile
    {
        //Constructor
        public NullTile(Point location) : this(location.X, location.Y) {}
        public NullTile(int x, int y) : base(x,y)
        {
            this.Name = "Null Tile";
            this.Type = EnumMapObjType.Meta;
        }
    }
    class EntryPointTile : MapTile
    {
        //Constructor
        public EntryPointTile(Point location) : this(location.X, location.Y) {}
        public EntryPointTile(int x, int y) : base(x,y)
        {
            this.Name = "Entry Point";
            this.Type = EnumMapObjType.Meta;
        }
    }
    #endregion

    #region Barriers
    class BarrierTile : MapTile
    {
        //Constructor
        public BarrierTile(Point location) : this(location.X, location.Y) {}
        public BarrierTile(int x, int y) : base(x,y)
        {
            this.Name = "Generic Barrier";
            this.Type = EnumMapObjType.Generic;
            this.AddFlag(FlagsMapFeature.Impassable);
        }
    }
    class MountainWallTile : BarrierTile
    {
        //Constructor
        public MountainWallTile(Point location) : this(location.X, location.Y) {}
        public MountainWallTile(int x, int y) : base(x,y)
        {
            this.Name = "Mountain Wall";
            this.Type = EnumMapObjType.Terrain;
        }
    }
    #endregion
}
