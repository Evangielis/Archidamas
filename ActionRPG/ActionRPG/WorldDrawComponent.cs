using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Archidamas.Extensions;

namespace ActionRPG
{
    enum EnumAnimation { None, Walk };

    delegate void Animation(IActor actor, Vector2 origin, TimeSpan length, SpriteBatch batch, Texture2D spriteSheet);

    interface IAnimationService
    {
        void SetAnimation(IActor actor, Animation animDelegate);
    }

    class WorldDrawComponent : DrawableGameComponent
    {
        //Draw layers from 0 to 1.0
        public readonly float[] DRAW_LAYER = { 0F, 0.1F, 0.2F, 0.3F, 0.4F, 0.5F, 0.6F, 0.7F, 0.8F, 0.9F, 1.0F };
        Dictionary<string, Texture2D> _textureBank;
        Dictionary<IActor, Animation> _activeAnimations;

        Vector2 Center { get; set; }
        public SpriteBatch Batch { get; set; }
        IMapService MapService { get; set; }
        IObjectService ObjService { get; set; }

        //Constructors
        public WorldDrawComponent(Game game) : base(game) 
        {
            game.Components.Add(this);
            this._textureBank = new Dictionary<string, Texture2D>();
        }

        public override void Initialize()
        {
            this.MapService = (IMapService)Game.Services.GetService(typeof(IMapService));
            this.ObjService = (IObjectService)Game.Services.GetService(typeof(IObjectService));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this._textureBank.Add("Grass", Game.Content.Load<Texture2D>("Grass"));
            this._textureBank.Add("MountainWall", Game.Content.Load<Texture2D>("MountainWall"));
            this._textureBank.Add("CaveEntrance", Game.Content.Load<Texture2D>("CaveEntrance"));
            this._textureBank.Add("PlayerAvatar", Game.Content.Load<Texture2D>("Avatar"));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            this.Center = this.ObjService.FetchAvatar().Loc.ToVector2();
            base.Update(gameTime);
        }
        void UpdateAnimations(GameTime gameTime)
        {
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
                    int x = (int)this.Center.X - i;
                    int y = (int)this.Center.Y - j;
                    MapDrawTile(batch, this.MapService.GetSurfaceAt(x,y));
                    MapDrawTile(batch, this.MapService.GetFeatureAt(x,y));
                    if (this.MapService.IsActorAt(x,y))
                        MapDrawActor(batch, this.MapService.GetActorAt(x,y));
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
                    batch.Draw(this._textureBank["CaveEntrance"],
                        feature.Loc.ToVector2(this.MapService.GridSize),
                        null,Color.White,0F,Vector2.Zero,1F,SpriteEffects.None,DRAW_LAYER[9]);
                    break;

                case "Mountain Wall":
                    batch.Draw(this._textureBank["MountainWall"],
                        feature.Loc.ToVector2(this.MapService.GridSize),
                        null, Color.White, 0F, Vector2.Zero, 1F, SpriteEffects.None, DRAW_LAYER[9]);
                    break;

                case "Grass":
                    batch.Draw(this._textureBank["Grass"],
                        feature.Loc.ToVector2(this.MapService.GridSize),
                        null, Color.White, 0F, Vector2.Zero, 1F, SpriteEffects.None, DRAW_LAYER[10]);
                    break;
            }
        }

        private void MapDrawActor(SpriteBatch batch, IActor actor)
        {
            switch (actor.Name)
            {
                case "PlayerAvatar":
                    Rectangle source = new Rectangle(0,0,32,32);
                    source.Offset((int)actor.Facing * 32, 0);

                    batch.Draw(this._textureBank["PlayerAvatar"],
                        actor.Loc.ToVector2(this.MapService.GridSize),
                        source, Color.White, 0F, Vector2.Zero, 1F, SpriteEffects.None, DRAW_LAYER[8]);
                    break;
            }
        }

        private void MapDrawAnimation(SpriteBatch batch, IActor actor)
        {
        }
    }
}

/*private void GenerateBackground(SpriteBatch batch)
{
    int width = this._textureBank["Grass"].Width;
    int height = this._textureBank["Grass"].Height;

    for (int i = -1; i < 5; i++)
    {
        for (int j = -1; j < 8; j++)
        {
            batch.Draw(this._textureBank["Grass"],
                new Vector2((j + this.Center.X) * width - (Game.GraphicsDevice.Viewport.Width / 2),
                    (i + this.Center.Y) * height - (Game.GraphicsDevice.Viewport.Height / 2)), 
                Color.White);
        }
    }
}*/
