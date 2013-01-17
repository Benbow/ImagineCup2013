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
    class Balle : Sprite
    {

        private int _H;
        public Rectangle _recBalle;

        public void Initialize(int H)
        {
            //Initialisation
            _pos = Vector2.Zero;
            _dir = Vector2.One;
            _speed = 0.2f;
            _H = H;

        }

        public void LoadContent(ContentManager content, string assetName)
        {
            _text = content.Load<Texture2D>(assetName);
            _recBalle = new Rectangle((int)this._pos.X, (int)this._pos.Y, (int)this._text.Width, (int)this._text.Height);
        }

        public void Update(GameTime gameTime, Barre B1, Barre B2)
        {
            if (this._pos.Y < 0 || (this._pos.Y + this._text.Height) > _H)
                this._dir = this._dir * new Vector2(1, -1);

            if (B1._recBarre.Intersects(this._recBalle) || B2._recBarre.Intersects(this._recBalle))
            {
                this._dir = this._dir * new Vector2(-1, 1);
                _speed += 0.05f;
            }

            

            _pos += _dir * _speed * gameTime.ElapsedGameTime.Milliseconds;

            _recBalle = new Rectangle((int)this._pos.X, (int)this._pos.Y, (int)this._text.Width, (int)this._text.Height);
        }

        public void Win(int index)
        {
            if (index == 1)
            {
                _pos = new Vector2(400, 10);
            }
            else if (index == 2)
            {
                _pos = new Vector2(400, 10);
                this._dir = this._dir * new Vector2(-1, 1);
            }
        }
    }
}
