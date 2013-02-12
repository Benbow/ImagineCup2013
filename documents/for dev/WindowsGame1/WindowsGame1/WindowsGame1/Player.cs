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
    class Player : Sprite
    {
        private Rectangle _rect;
        private KeyboardState _keyboard;
        private double _offset;

        public Player()
        {

        }

        public void Initialize(int x, int y)
        {
            _pos = new Vector2(x, y);
            _rect = new Rectangle((int)_pos.X, (int)_pos.Y, (int)_text.Width, (int)_text.Height);
            _offset = 0;
        }

        public void Update(GameTime gameTime, Ground back)
        {
            _keyboard = Keyboard.GetState();

            if (_keyboard.IsKeyDown(Keys.Right))
            {
                if (_pos.X < 600)
                    this._pos = new Vector2((_pos.X += 2), _pos.Y);
                else
                    CheckBackground(back);
            }
            else if (_keyboard.IsKeyDown(Keys.Left))
            {
                if (_pos.X > 200)
                    this._pos = new Vector2((_pos.X -= 2), _pos.Y);
                else
                    CheckBackground(back);
            }
        }

        public void CheckBackground(Ground back)
        {
            if(_pos.X == 600)
            {
                _offset += 2;
                back.Position -= back.Position + new Vector2((float)(_offset), 0);
            }

            if (_pos.X == 200)
            {
                _offset -= 2;
                back.Position -= back.Position + new Vector2((float)(_offset), 0);
            }
            
        }
    }
}
