using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Archidamas.Extensions;

namespace ActionRPG
{
    interface IMap
    {
        Point PlayerLoc { get; }
        IMapFeature GetObjAt(int x, int y);
        IMapFeature[] GetBoundedSet(Rectangle bounds);
    }

    class WorldDrawComponent : DrawableGameComponent
    {
        Dictionary<string, Texture2D> _textureBank;

        Point Center { get; set; }
        public SpriteBatch Batch { get; set; }
        IMap Map { get; set; }

        //Constructors
        public WorldDrawComponent(Game game) : base(game) 
        {
            game.Components.Add(this);
            this._textureBank = new Dictionary<string, Texture2D>();
        }

        public override void Initialize()
        {
            this.Map = (IMap)Game.Services.GetService(typeof(IMap));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this._textureBank.Add("Grass", Game.Content.Load<Texture2D>("Grass"));
            this._textureBank.Add("Mountain", Game.Content.Load<Texture2D>("Mountain"));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            this.Center = this.Map.PlayerLoc;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.DrawWorldToBatch(this.Batch);
            base.Draw(gameTime);
        }

        public void DrawWorldToBatch(SpriteBatch batch)
        {
            this.GenerateBackground(batch);
            this.GenerateMap(batch);
        }

        private void GenerateMap(SpriteBatch batch)
        {
            IMapFeature[] m = this.Map.GetBoundedSet(Rectangle.Empty);
            foreach (IMapFeature f in m)
            {
                MapDrawFeature(batch, f);
            }
        }
        private void MapDrawFeature(SpriteBatch batch, IMapFeature feature)
        {
            switch (feature.Type)
            {
                case EnumMapObjType.Empty:
                    break;

                case EnumMapObjType.Terrain:
                    batch.Draw(this._textureBank[feature.Name],feature.Area,Color.White);
                    break;
            }
        }

        private void GenerateBackground(SpriteBatch batch)
        {
            int width = this._textureBank["Grass"].Width;
            int height = this._textureBank["Grass"].Height;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    batch.Draw(this._textureBank["Grass"],
                        new Vector2(j * width - Game.GraphicsDevice.Viewport.Width / 2,
                            i * height - Game.GraphicsDevice.Viewport.Height / 2), 
                        Color.White);
                }
            }
        }
    }
}
