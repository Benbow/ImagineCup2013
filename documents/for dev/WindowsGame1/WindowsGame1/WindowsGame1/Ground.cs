using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    class Ground : Sprite
    {
        private Rectangle _rect;
        private double _init;

        public Ground(int x, int y)
        {
            this._pos = new Vector2(x, y);
            _speed = 0;
            _init = 0;
        }

        public virtual void LoadContent(ContentManager content, string assetName)
        {
            _text = content.Load<Texture2D>(assetName);
            _rect = new Rectangle((int) _pos.X, (int) _pos.Y, (int) _text.Width, (int) _text.Height);
        }

        public void Update(GameTime gameTime)
        {
            _pos += _dir * _speed * gameTime.ElapsedGameTime.Milliseconds;
        }

    }
}
