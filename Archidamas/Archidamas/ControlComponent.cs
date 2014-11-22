using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Archidamas
{
    /// <summary>
    /// Provides more advanced control interface services which allow
    /// the programmer to define control channels which it is interested
    /// in.  Is overall more modular and trades processor time for stable 
    /// heap space.
    /// </summary>
    public interface IControlService
    {
        Keys[] PressedKeys { get; }
        bool isKeyDown(Keys key);
        bool isButtonDown(Buttons button);
    }

    /// <summary>
    /// Defines a Control Channel which the control service
    /// can publish events to.  Each channel takes a delegate
    /// which is used to check its status.  If the channel is Active
    /// then its conditions have been met this update.  If the channel 
    /// is Live then it is being checked by the control service.
    /// </summary>
    public class ControlChannel
    {
        /// <summary>
        /// Delegate used to set the active status of the control channel.
        /// </summary>
        /// <param name="control">The checking control service</param>
        /// <returns>True if event is active, false if not</returns>
        delegate bool ControlEvent(IControlService control);

        string Name { get; set; }
        bool Active { get; set; }
        bool Live { get; set; }
        ControlEvent ActivityCheck { get; set; }        

        public ControlChannel(string name, ControlEvent del)
        {
            this.Name = name;
            this.Active = this.Live = false;
            this.ActivityCheck = del;
        }
    }

    /// <summary>
    /// This component serves all countrol based interfaces
    /// </summary>
    public class ControlComponent : GameComponent, IControlService
    {
        //Channels
        List<ControlChannel> _controlChannels;

        //Mouse
        MouseState _currentMouse;
        MouseState _prevMouse;
        //Kb
        KeyboardState _currentKb;
        KeyboardState _prevKb;

        public ControlComponent(Game game) : base(game)
        {
            game.Components.Add(this);
            game.Services.AddService(typeof(IControlService), this);

            this._controlChannels = new List<ControlChannel>();
        }

        public override void Initialize()
        {
            this._currentMouse = this._prevMouse = Mouse.GetState();
            this._currentKb = this._prevKb = Keyboard.GetState();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //Kb updates
            this._prevKb = this._currentKb;
            this._currentKb = Keyboard.GetState();

            //Mouse updates
            this._prevMouse = this._currentMouse;
            this._currentMouse = Mouse.GetState();

            base.Update(gameTime);
        }

        //Service interface implementations
    }
}
