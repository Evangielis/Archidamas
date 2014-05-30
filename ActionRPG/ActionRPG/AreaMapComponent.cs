using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Archidamas;
using Archidamas.Extensions;
using Microsoft.Xna.Framework.Input;

namespace ActionRPG
{
    enum EnumMapObjType
    {
        Empty,
        Terrain,
    };

    interface IMapService
    {
        Vector2 PlayerLoc { get; }
        IMapFeature GetObjAt(int x, int y);
        IMapFeature[] GetBoundedSet(Rectangle bounds);
    }

    class AreaMapComponent : GameComponent, IMapService
    {
        const float MOVE_SPEED = 2.0F;

        List<IMapFeature> _mapObjs;
        Vector2 _playerLoc;
        Vector2 PlayerLoc 
        {
            get { return _playerLoc; }
        }
        IKeyService KeyService { get; set; }

        public AreaMapComponent(Game game) : base(game)
        {
            game.Components.Add(this);
            game.Services.AddService(typeof(IMapService),this);
        }

        public override void Initialize()
        {
            KeyService = (IKeyService)Game.Services.GetService(typeof(IKeyService));
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            this.CheckPlayerMovement();
            base.Update(gameTime);
        }

        public void CheckPlayerMovement()
        {
            Vector2 _proposedLoc = PlayerLoc.Clone();

            foreach (Keys k in this.KeyService.PressedKeys)
            {
                switch (k)
                {
                    case Keys.W: _proposedLoc.Y -= MOVE_SPEED; break;
                    case Keys.A: _proposedLoc.X -= MOVE_SPEED; break;
                    case Keys.S: _proposedLoc.Y += MOVE_SPEED; break;
                    case Keys.D: _proposedLoc.X += MOVE_SPEED; break;
                    default: break;
                }
            }

            Rectangle player = new Rectangle((int)_proposedLoc.X,(int)_proposedLoc.Y,32,32);
            _playerLoc = (this.CheckCollision(player)) ? _playerLoc : _proposedLoc;
        }

        public bool CheckCollision(Rectangle collider)
        {
            foreach (IMapFeature f in _mapObjs)
            {
                if (f.Area.Intersects(collider))
                    return true;
            }
            return false;
        }

        public void LoadMap(IMapFeature[] objs)
        {
            this._mapObjs = new List<IMapFeature>(objs);

            //Identify and process entry point
            IMapFeature p = this._mapObjs.Find(s => s.Name == "EntryPoint");
            this._playerLoc = p.Area.Location.ToVector2();
            this._mapObjs.Remove(p);
        }

        //SERVICE IMPLEMENTATION
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
        IMapFeature IMapService.GetObjAt(int x, int y)
        {
            return this.GetObjAt(x, y);
        }

        Vector2 IMapService.PlayerLoc
        {
            get { return this.PlayerLoc; }
        }

        IMapFeature[] IMapService.GetBoundedSet(Rectangle bounds)
        {
            return this.GetBoundedSet(bounds);
        }
    }
}
