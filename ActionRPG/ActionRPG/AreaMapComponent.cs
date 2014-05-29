using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ActionRPG
{
    enum EnumMapObjType
    {
        Empty,
        Terrain,
    };

    interface IMapFeature
    {
        EnumMapObjType Type { get; }
        bool Passable { get; }
        Rectangle Area { get; }
        string Name { get; }
    }

    class AreaMapComponent : GameComponent, IMap
    {
        List<IMapFeature> _mapObjs;
        Point PlayerLoc { get; set; }

        public AreaMapComponent(Game game) : base(game)
        {
            game.Components.Add(this);
            game.Services.AddService(typeof(IMap),this);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void LoadMap(IMapFeature[] objs)
        {
            this._mapObjs = new List<IMapFeature>(objs);

            //Identify and process entry point
            IMapFeature p = this._mapObjs.Find(s => s.Name == "EntryPoint");
            this.PlayerLoc = p.Area.Location;
            this._mapObjs.Remove(p);
        }
        //private void RegisterMapObj(IMapObj o) { _mapBlocks[o.Loc.X, o.Loc.Y] = o; }

        IMapFeature GetObjAt(int x, int y) 
        {
            return this._mapObjs.Find(s => s.Area.Contains(x,y));
        }

        IMapFeature[] GetBoundedSet(Rectangle bounds)
        {
            if (bounds != Rectangle.Empty)
                return this._mapObjs.FindAll(s => s.Area.Intersects(bounds)).ToArray();
            else
                return this._mapObjs.ToArray();
        }


        //IMap implementation
        IMapFeature IMap.GetObjAt(int x, int y)
        {
            return this.GetObjAt(x, y);
        }

        Point IMap.PlayerLoc
        {
            get { return this.PlayerLoc; }
        }

        IMapFeature[] IMap.GetBoundedSet(Rectangle bounds)
        {
            return this.GetBoundedSet(bounds);
        }
    }
}
