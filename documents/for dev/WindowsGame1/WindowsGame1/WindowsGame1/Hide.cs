using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    class Hide : Player
    {
        public Hide()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressources.Hide, this._hitBox, new Rectangle((this.FrameColumn * 55), (this.FrameLine * 87), 55, 87), Color.White,
                0f, new Vector2(0, 0), this.Effect, 0f);
        }
    }
}
