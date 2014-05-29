using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Archidamas.Extensions;

namespace ActionRPG
{
    class ActorDrawComponent : DrawableGameComponent
    {
        private Texture2D PCTxr { get; set; }
        public SpriteBatch Batch { get; set; }
        IMap Map { get; set; }

        public ActorDrawComponent(Game game) : base(game)
        { 
            game.Components.Add(this);
        }

        public override void Initialize()
        {
            this.Map = (IMap)Game.Services.GetService(typeof(IMap));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.PCTxr = Game.Content.Load<Texture2D>("knt1_fr1");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            DrawActorsToBatch(this.Batch);
            base.Draw(gameTime);
        }

        public void DrawActorsToBatch(SpriteBatch batch)
        {
            DrawPlayer(batch);
        }

        private void DrawPlayer(SpriteBatch batch)
        {
            batch.Draw(this.PCTxr, 
                this.Map.PlayerLoc.ToVector2(), 
                Color.White);
        }
    }
}
