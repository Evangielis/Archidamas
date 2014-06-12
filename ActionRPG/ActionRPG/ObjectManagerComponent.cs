using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ActionRPG
{
    interface IObjectService
    {
        IActor AllocAvatar();
        IActor FetchAvatar();

        //General Mob work
        IActor AllocActor(string name, int speed);
        IActor FetchActor(int id);
        IActor[] AllActors { get; }
    }

    class ObjectManagerComponent : GameComponent, IObjectService
    {
        int PlayerIndex { get; set; }
        List<IActor> _actors;

        public ObjectManagerComponent(Game game)
            : base(game)
        {
            game.Components.Add(this);
            game.Services.AddService(typeof(IObjectService), this);
        }

        public override void Initialize()
        {
            _actors = new List<IActor>();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public IActor AllocAvatar()
        {
            IActor p = this.AllocActor("PlayerAvatar", 50);
            this.PlayerIndex = p.MobID;
            return p;
        }

        public IActor FetchAvatar()
        {
            return _actors[this.PlayerIndex];
        }

        public IActor AllocActor(string name, int speed)
        {
            int i = _actors.Count;
            _actors.Add(new Actor(name, i, speed));
            return _actors[i];
        }

        public IActor FetchActor(int id)
        {
            return _actors[id];
        }
    
        IActor IObjectService.AllocAvatar()
        {
            return this.AllocAvatar();
        }

        IActor IObjectService.FetchAvatar()
        {
            return this.FetchAvatar();
        }

        IActor IObjectService.AllocActor(string name, int speed)
        {
            return this.AllocActor(name, speed);
        }

        IActor IObjectService.FetchActor(int id)
        {
            return this.FetchActor(id);
        }

        IActor[] IObjectService.AllActors
        {
            get { return this._actors.ToArray(); }
        }
    }
}
