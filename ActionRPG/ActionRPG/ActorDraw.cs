using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Archidamas.Extensions;

namespace ActionRPG
{
    class ActorDraw
    {
        private Texture2D PCTxr { get; set; }

        public ActorDraw() { }

        public void Initialize()
        {
        }

        public void LoadContent(Texture2D PCtexture)
        {
            this.PCTxr = PCtexture;
        }

        public void DrawActorsToBatch(SpriteBatch batch)
        {
            DrawPlayer(batch);
        }

        private void DrawPlayer(SpriteBatch batch)
        {
            batch.Draw(this.PCTxr, 
                batch.GraphicsDevice.Viewport.Bounds.Center.ToVector2() - this.PCTxr.CenterVector(), 
                Color.White);
        }
    }
}
