using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ActionRPG
{
    class CameraComponent : GameComponent
    {
        float ZoomLevel { get; set; }
        
        public Matrix TranslationMatrix 
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-1 * this.ObjService.FetchAvatar().Loc.X, -1 * this.ObjService.FetchAvatar().Loc.Y, 0))
                    * Matrix.CreateTranslation(new Vector3(-16, -16, 0))
                    * Matrix.CreateRotationZ(0F)
                    * Matrix.CreateScale(new Vector3(this.ZoomLevel, this.ZoomLevel, 1F))
                    * Matrix.CreateTranslation(new Vector3(Game.GraphicsDevice.Viewport.Width * 0.5F, Game.GraphicsDevice.Viewport.Height * 0.5F, 0));
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
            this.ObjService = (IObjectService)Game.Services.GetService(typeof(IObjectService));
            this.MapService = (IMapService)Game.Services.GetService(typeof(IMapService));
            this.ZoomLevel = 1.5F;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
