using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Archidamas
{
    public interface IKeyService
    {
        Keys[] PressedKeys { get; }
    }

    public class ControlComponent : GameComponent, IKeyService
    {
        Keys[] PressedKeys { get; set; }

        public ControlComponent(Game game) : base(game)
        {
            game.Components.Add(this);
            game.Services.AddService(typeof(IKeyService), this);
        }

        public override void Initialize()
        {
            this.PressedKeys = new Keys[0];
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            this.PressedKeys = Keyboard.GetState().GetPressedKeys();
            base.Update(gameTime);
        }


        //Service interface implementations
        Keys[] IKeyService.PressedKeys
        {
            get { return this.PressedKeys; }
        }
    }
}
