using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Archidamas
{
    /// <summary>
    /// This interface provides keyboard interface services.
    /// </summary>
    public interface IKeyService
    {
        Keys[] PressedKeys { get; }
        Dictionary<Keys,bool> KeyStatus { get; }
    }

    /// <summary>
    /// This component serves all countrol based interfaces
    /// </summary>
    public class ControlComponent : GameComponent, IKeyService
    {
        Keys[] PressedKeys { get; set; }
        Dictionary<Keys, bool> _keyStatus;
        Dictionary<Keys, TimeSpan> _keyPresses;

        public ControlComponent(Game game) : base(game)
        {
            game.Components.Add(this);
            game.Services.AddService(typeof(IKeyService), this);
        }

        public override void Initialize()
        {
            this.PressedKeys = new Keys[0];
            this._keyStatus = new Dictionary<Keys, bool>();
            this._keyPresses = new Dictionary<Keys, TimeSpan>();

            //Initialize _keyPresses
            foreach (Keys k in Enum.GetValues(typeof(Keys)).Cast<Keys>())
            {
                this._keyStatus[k] = false;
                this._keyPresses[k] = TimeSpan.Zero;
            }

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //Operate on all all previously set keys
            foreach (Keys k in this.PressedKeys)
            {
                this._keyStatus[k] = false;
            }

            this.PressedKeys = Keyboard.GetState().GetPressedKeys();

            //Operate on each currently pressed key
            foreach (Keys k in this.PressedKeys)
            {
                //if (this._keyPresses[k].Equals(TimeSpan.Zero))
                 //   this._keyPresses[k] = gameTime.TotalGameTime;
                this._keyStatus[k] = true;
            }

            base.Update(gameTime);
        }


        //Service interface implementations
        Keys[] IKeyService.PressedKeys
        {
            get { return this.PressedKeys; }
        }
        Dictionary<Keys, bool> IKeyService.KeyStatus
        {
            get { return this._keyStatus; }
        }
    }
}
