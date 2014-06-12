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
                    * Matrix.CreateTranslation((-1 * this.ObjService.FetchAvatar().Loc.X * this.MapService.GridSize) - 16,
                    (-1 * this.ObjService.FetchAvatar().Loc.Y * this.MapService.GridSize) - 16, 0);
            }
        }
        IObjectService ObjService { get; set; }
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
            this.ObjService = (IObjectService)Game.Services.GetService(typeof(IObjectService));
            this.MapService = (IMapService)Game.Services.GetService(typeof(IMapService));
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
