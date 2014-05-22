using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace CardSim
{
    /// <summary>
    /// Represents a Card object which has the following attributes.
    /// - FTexture : The front face of the card
    /// - BTexture : The back face of the card 
    /// </summary>
    class Card
    {
        public Texture2D FTexture { get; private set; }
        public Texture2D BTexture { get; private set; }

        public Card(Texture2D front, Texture2D back)
        {
            this.FTexture = front;
            this.BTexture = back;
        }
    }
}
