using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ActionRPG
{
    interface IFlowCtrlService
    {
        bool AnimationLockout { get; }
        void AddAnimationLockout(TimeSpan length);
    }

    class FlowControlComponent : GameComponent, IFlowCtrlService
    {
        TimeSpan _animationLockout;

        public FlowControlComponent(Game game)
            : base(game)
        {
            game.Components.Add(this);
            game.Services.AddService(typeof(IFlowCtrlService), this);
        }

        public override void Initialize()
        {
            this._animationLockout = TimeSpan.Zero;
            base.Initialize();
        }


        //IFlowCtrlService explicit implementations
        bool IFlowCtrlService.AnimationLockout
        {
            get
            {
                return (!this._animationLockout.Equals(TimeSpan.Zero));
            }
        }
        void IFlowCtrlService.AddAnimationLockout(TimeSpan length)
        {
            if (length.CompareTo(length) < 0)
                this._animationLockout = length;
        }
    }
}
