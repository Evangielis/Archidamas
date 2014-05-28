using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ActionRPG
{
    class WorldDraw
    {
        private Texture2D GrassTxr { get; set; }

        //Constructors
        public WorldDraw() { }
        public void Initialize() { }
        public void LoadContent(Texture2D backgroundTxr) 
        {
            this.GrassTxr = backgroundTxr;
        }

        public void DrawWorldToBatch(SpriteBatch batch)
        {
            this.GenerateBackground(batch);
        }

        private void GenerateBackground(SpriteBatch batch)
        {
            int width = this.GrassTxr.Width;
            int height = this.GrassTxr.Height;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    batch.Draw(this.GrassTxr, new Vector2(j*width, i*height), Color.White);
                }
            }
        }
    }
}
