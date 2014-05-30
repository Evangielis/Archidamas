using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Archidamas.Extensions;

namespace ActionRPG
{
    class WorldDrawComponent : DrawableGameComponent
    {
        Dictionary<string, Texture2D> _textureBank;

        Vector2 Center { get; set; }
        public SpriteBatch Batch { get; set; }
        IMapService MapService { get; set; }

        //Constructors
        public WorldDrawComponent(Game game) : base(game) 
        {
            game.Components.Add(this);
            this._textureBank = new Dictionary<string, Texture2D>();
        }

        public override void Initialize()
        {
            this.MapService = (IMapService)Game.Services.GetService(typeof(IMapService));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this._textureBank.Add("Grass", Game.Content.Load<Texture2D>("Grass"));
            this._textureBank.Add("MountainWall", Game.Content.Load<Texture2D>("MountainWall"));
            this._textureBank.Add("CaveEntrance", Game.Content.Load<Texture2D>("CaveEntrance"));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            this.Center = this.MapService.PlayerLoc;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.DrawWorldToBatch(this.Batch);
            base.Draw(gameTime);
        }

        public void DrawWorldToBatch(SpriteBatch batch)
        {
            //this.GenerateBackground(batch);
            this.GenerateMap(batch);
        }

        private void GenerateMap(SpriteBatch batch)
        {
            for (int i = -15; i < 16; i++)
            {
                for (int j = -10; j < 11; j++)
                {
                    MapDrawTile(batch, this.MapService.GetSurfaceAt((int)this.Center.X/32 - i, (int)this.Center.Y/32 - j));
                    MapDrawTile(batch, this.MapService.GetFeatureAt((int)this.Center.X/32 - i,(int)this.Center.Y/32 - j));
                }
            }
        }
        private void MapDrawTile(SpriteBatch batch, IMapTile feature)
        {
            switch (feature.Name)
            {
                case "":
                    break;

                case "Cave Entrance":
                    batch.Draw(this._textureBank["CaveEntrance"],feature.Area,Color.White);
                    break;

                case "Mountain Wall":
                    batch.Draw(this._textureBank["MountainWall"], feature.Area, Color.White);
                    break;

                case "Grass":
                    batch.Draw(this._textureBank["Grass"], feature.Area, Color.White);
                    break;
            }
        }

        private void GenerateBackground(SpriteBatch batch)
        {
            int width = this._textureBank["Grass"].Width;
            int height = this._textureBank["Grass"].Height;

            for (int i = -1; i < 5; i++)
            {
                for (int j = -1; j < 8; j++)
                {
                    batch.Draw(this._textureBank["Grass"],
                        new Vector2((j + ((int)MapService.PlayerLoc.X/width)) * width - (Game.GraphicsDevice.Viewport.Width / 2),
                            (i + ((int)MapService.PlayerLoc.Y/height)) * height - (Game.GraphicsDevice.Viewport.Height / 2)), 
                        Color.White);
                }
            }
        }
    }
}
