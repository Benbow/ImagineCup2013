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
    class Sprite
    {
        public Texture2D Texture
        {
            get { return _text; }
            set { _text = value; }
        }
        protected Texture2D _text;

        public Vector2 Position
        {
            get { return _pos; }
            set { _pos = value; }
        }
        protected Vector2 _pos;

        public Vector2 Direction
        {
            get { return _dir; }
            set { _dir = Vector2.Normalize(value); }
        }
        protected Vector2 _dir;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        protected float _speed;

        public virtual void Initialize()
        {
            _pos = Vector2.Zero;
            _dir = Vector2.One;
            _speed = 0;
        }

        public virtual void LoadContent(ContentManager content, string assetName)
        {
            _text = content.Load<Texture2D>(assetName);
        }

        public virtual void Update(GameTime gameTime)
        {
            _pos += _dir * _speed * gameTime.ElapsedGameTime.Milliseconds;
        }

        public virtual void Handleinput(KeyboardState keyboard, MouseState mouse)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_text, _pos, Color.White);
        }
    }
}
