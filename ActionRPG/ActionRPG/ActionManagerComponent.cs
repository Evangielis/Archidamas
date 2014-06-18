using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ActionRPG
{
    enum EnumActorAction { Idle, Wait, Move };

    //Not currently used
    interface IActionService
    {
        bool IsMobReady { get; }
        IActor NextMob { get; }
        void RecycleMob();
    }

    class ActionManagerComponent : GameComponent
    {
        //Services
        IObjectService ObjService { get; set; }
        IMapService MapService { get; set; }
        IFlowCtrlService FlowService { get; set; }

        TimeSpan UPDATE_CYCLE;
        TimeSpan SinceLastCycle { get; set; }
        
        Queue<IActor> _readyMobs;
        bool AnimationLockout { get; set; }

        public ActionManagerComponent(Game game)
            : base(game)
        {
            game.Components.Add(this);
        }

        public override void Initialize()
        {
            //Services
            this.ObjService = (IObjectService)Game.Services.GetService(typeof(IObjectService));
            this.MapService = (IMapService)Game.Services.GetService(typeof(IMapService));
            this.FlowService = (IFlowCtrlService)Game.Services.GetService(typeof(IFlowCtrlService));

            this.AnimationLockout = false;
            this.UPDATE_CYCLE = TimeSpan.FromTicks(1000);        
            this.SinceLastCycle = TimeSpan.Zero;
            this._readyMobs = new Queue<IActor>();
            base.Initialize();
        }

        #region UpdateLogic
        //Update logic
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.SinceLastCycle += gameTime.ElapsedGameTime;
            if (!this.AnimationLockout) this.UpdateCycleCheck();

        }
        void UpdateCycleCheck()
        {
            if (this.SinceLastCycle.CompareTo(UPDATE_CYCLE) >= 0)
            {
                this.SinceLastCycle -= UPDATE_CYCLE;
                UpdateCycle();
            }
        }
        void UpdateCycle()
        {
            foreach (IActor m in ObjService.AllActors)
            {
                if (m.IsReady)
                {
                    ResolveAction(m);
                }
                else
                    m.Ready();
            }
        }
        #endregion

        void ResolveAction(IActor actor)
        {
            switch (actor.Action)
            {
                case EnumActorAction.Idle: break;
                case EnumActorAction.Move: MoveAction(actor); break;
            }
            actor.Unready();
        }

        void MoveAction(IActor actor)
        {
        }
    }
}
