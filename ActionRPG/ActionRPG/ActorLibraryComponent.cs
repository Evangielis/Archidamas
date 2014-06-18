using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Archidamas.Extensions;

namespace ActionRPG
{
    interface IActor
    {
        int MobID { get; }
        string Name { get; }
        EnumMapDirection Facing { get; set; }
        EnumActorAction Action { get; set; }
        Vector2 Loc { get; }
        Rectangle Area { get; }
        void SetLoc(Vector2 loc);        
        bool IsReady { get; }
        void Ready();
        void Unready();

        //bool IsAnimated { get; }
        //EnumAnimation Animation { get; }
        //void Animate(EnumAnimation anim);
        //void Deanimate();
    }

    class Actor : IActor
    {
        int MobID { get; set; }
        string Name { get; set; }
        EnumMapDirection Facing { get; set; }
        EnumActorAction Action { get; set; }
        Vector2 Loc { get; set; }
        Rectangle _area;
        int _readyCounter;
        int Speed { get; set; }
        bool IsReady { get { return (this._readyCounter <= 0); } }

        //bool IsAnimated { get { return false; } }
        //EnumAnimation Animation { get; set; }


        public Actor(string name, int id, int speed)
        {
            this.Name = name;
            this.MobID = id;
            this.Speed = speed;
            this.Loc = Vector2.Zero;
            this.SetArea();
            this.Facing = EnumMapDirection.South;
            this.Action = EnumActorAction.Idle;
            this.Unready();
        }

        void SetLoc(Vector2 loc)
        {
            Vector2 diff = this.Loc - loc;
            this.Loc = loc;
            this._area.Offset(diff);
        }

        /// <summary>
        /// This should be overwritten for any actor that doesn't fit the standard collision size.
        /// </summary>
        protected virtual void SetArea()
        {
            this._area = RectangleExt.FromVector2(this.Loc, 32, 32);
            this._area.Inflate(-2, -2);
        }

        //Active turn stuff
        void Ready()
        {
            _readyCounter -= this.Speed;
        }
        void Unready()
        {
            _readyCounter = 100;
            this.Action = EnumActorAction.Idle;
        }

        //Animations


        //Explicit implementation of IActor
        EnumMapDirection IActor.Facing
        {
            get { return this.Facing; }
            set { this.Facing = value; }
        }
        EnumActorAction IActor.Action
        {
            get { return this.Action; }
            set { this.Action = value; }
        }
        void IActor.Ready() { this.Ready(); }
        void IActor.Unready() { this.Unready(); }
        bool IActor.IsReady { get { return this.IsReady; } }
        int IActor.MobID { get { return this.MobID; } }
        string IActor.Name { get { return this.Name; } }
        Vector2 IActor.Loc { get { return this.Loc; } }
        Rectangle IActor.Area { get { return this._area; } }
        void IActor.SetLoc(Vector2 loc) { this.SetLoc(loc); }
    }

    class PlayerAvatar : Actor
    {
        public PlayerAvatar(int id)
            : base("PlayerAvatar", id, 50)
        {

        }
    }

    class ActorLibraryComponent : GameComponent
    {
        public ActorLibraryComponent(Game game)
            : base(game)
        {
            game.Components.Add(this);
        }
    }
}
