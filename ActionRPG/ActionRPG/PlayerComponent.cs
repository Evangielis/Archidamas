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
    class PlayerComponent : GameComponent
    {
        IKeyService KeyService { get; set; }
        IObjectService ObjService { get; set; }
        IFlowCtrlService FlowService { get; set; }
        IActor Avatar { get { return this.ObjService.FetchAvatar(); } }

        public PlayerComponent(Game game)
            : base(game)
        {
            game.Components.Add(this);
        }

        public override void Initialize()
        {

            this.KeyService = (IKeyService)Game.Services.GetService(typeof(IKeyService));
            this.ObjService = (IObjectService)Game.Services.GetService(typeof(IObjectService));
            this.FlowService = (IFlowCtrlService)Game.Services.GetService(typeof(IFlowCtrlService));
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (!this.FlowService.AnimationLockout) this.CheckPlayerInputs();
            base.Update(gameTime);
        }

        void CheckPlayerInputs()
        {
            foreach (Keys k in this.KeyService.PressedKeys)
            {
                switch (k)
                {
                    case Keys.W: 
                        this.Avatar.Facing = EnumMapDirection.North;
                        this.Avatar.Action = EnumActorAction.Move;
                        break;
                    case Keys.A:
                        this.Avatar.Facing = EnumMapDirection.West;
                        this.Avatar.Action = EnumActorAction.Move;
                        break;
                    case Keys.S:
                        this.Avatar.Facing = EnumMapDirection.South;
                        this.Avatar.Action = EnumActorAction.Move;
                        break;
                    case Keys.D:
                        this.Avatar.Facing = EnumMapDirection.East;
                        this.Avatar.Action = EnumActorAction.Move;
                        break;
                    default: 
                        break;
                }
            }
        }
    }
}
