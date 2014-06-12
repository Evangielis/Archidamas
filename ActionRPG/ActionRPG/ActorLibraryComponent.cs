using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ActionRPG
{
    interface IActor
    {
        int MobID { get; }
        string Name { get; }
        EnumMapDirection Facing { get; set; }
        EnumActorAction Action { get; set; }
        Point Loc { get; set; }
        
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
        Point Loc { get; set; }

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
            this.Loc = Point.Zero;
            this.Facing = EnumMapDirection.South;
            this.Action = EnumActorAction.Idle;
            this.Unready();
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
        void IActor.Ready()
        {
            this.Ready();
        }
        void IActor.Unready()
        {
            this.Unready();
        }
        bool IActor.IsReady
        {
            get { return this.IsReady; }
        }
        int IActor.MobID
        {
            get { return this.MobID; }
        }

        string IActor.Name
        {
            get { return this.Name; }
        }

        Point IActor.Loc
        {
            get
            {
                return this.Loc;
            }
            set
            {
                this.Loc = value;
            }
        }
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
