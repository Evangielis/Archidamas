using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ActionRPG
{
    class CameraComponent : GameComponent
    {
        Matrix _baseTranslationMatrix;
        public Matrix TranslationMatrix 
        {
            get
            {
                return _baseTranslationMatrix
                    * Matrix.CreateTranslation((-1 * MapService.PlayerLoc.X) - 16, (-1 * MapService.PlayerLoc.Y) - 16, 0);
            }
        }
        IMapService MapService { get; set; }

        public CameraComponent(Game game)
            : base(game)
        {
            game.Components.Add(this);
        }

        public override void Initialize()
        {
            this._baseTranslationMatrix = Matrix.CreateTranslation(
                (Game.GraphicsDevice.Viewport.Width/2),
                (Game.GraphicsDevice.Viewport.Height/2),
                0);
            this.MapService = (IMapService)Game.Services.GetService(typeof(IMapService));
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
