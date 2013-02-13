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
    class PlayersOld
    {
        //FIELDS
        private Rectangle _player;
        private int _speed;
        private bool _isInJump = false;

        //CONSTRUCTOR
        public PlayersOld()
        {
            _player = new Rectangle(16, 15, 15, 15);
            _speed = 15;
        }

        //METHODS
        public void DecreaseCoordBlockX()
        {
            this._player.X -= this._speed;
        }
        public void IncreaseCoordBlockX()
        {
            this._player.X += this._speed;
        }
        public void IncreaseCoordBlockY()
        {
            this._player.Y += this._speed;
        }
        public void DecreaseCoordBlockY()
        {
            this._player.Y -= this._speed;
        }

        public void Jump()
        {
            for (int i = 0; i <= 60; i++)
            {
                if (i == 60)
                {
                    this._speed++;
                }
            }
        }


        //UPDATE & DRAW
        public void Update(KeyboardState keyboard, MouseState mouse)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Ressources.Player, _player, Color.White);
        }

        //GETTER SETTER
        public int Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        public Rectangle Player
        {
            get
            {
                return _player;
            }
            set
            {
                _player = value;
            }
        }

        public bool IsInJump
        {
            get
            {
                return _isInJump;
            }
            set
            {
                _isInJump = value;
            }
        }
    }
}
