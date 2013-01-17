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
    class Barre : Sprite
    {
        private KeyboardState _keyboard;
        public Rectangle _recBarre;
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }
        private int _score;
        private int _H;
        private int _index { get; set; }

        public void Initialize(int index, int H)
        {
            if (index == 1)
            {
                _pos = new Vector2(10, 150);
                this._index = 1;
                this._score = 0;
            }
            else if (index == 2)
            {
                _pos = new Vector2(780, 150);
                this._index = 2;
                this._score = -1;
            }
            _H = H;
            _dir = Vector2.One;
            _speed = 0;
        }

        public void LoadContent(ContentManager content, string assetName)
        {
            _text = content.Load<Texture2D>(assetName);
            _recBarre = new Rectangle((int)this._pos.X, (int)this._pos.Y, (int)_text.Width, (int)_text.Height);
        }


        public void Update(GameTime gameTime)
        {
            _keyboard = Keyboard.GetState();

            if (this._index == 1)
            {
                if (_keyboard.IsKeyDown(Keys.Z) && this._pos.Y > 0)
                    this._pos = this._pos + new Vector2(0, -3);
                else if (_keyboard.IsKeyDown(Keys.S) && (this._pos.Y + this._text.Height) < _H)
                    this._pos = this._pos + new Vector2(0, 3);
            }
            else if (this._index == 2)
            {
                if (_keyboard.IsKeyDown(Keys.Up) && this._pos.Y > 0)
                    this._pos = this._pos + new Vector2(0, -3);
                else if (_keyboard.IsKeyDown(Keys.Down) && (this._pos.Y + this._text.Height) < _H)
                    this._pos = this._pos + new Vector2(0, 3);
            }

            _recBarre = new Rectangle((int)this._pos.X, (int)this._pos.Y, (int)_text.Width, (int)_text.Height);
        }
    }
}
