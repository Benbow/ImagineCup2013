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
    class BlockOld
    {
        private int _x;
        private int _y;
        private Texture2D _texture;
        private Rectangle _hitbox;
        private bool _collidable;

        private static List<BlockOld> _listBlock = new List<BlockOld>();

        

        public BlockOld(int x, int y, Texture2D text)
        {
            this._x = x;
            this._y = y;
            this._texture = text;
            this._hitbox = new Rectangle(x, y, text.Width, text.Height);
            this._collidable = true;

            _listBlock.Add(this);
        }

        public void DecreaseCoordBlockX(int speed)
        {
            this._hitbox.X -= speed;
        }
        public void IncreaseCoordBlockX(int speed)
        {
            this._hitbox.X += speed;
        }
        public void DecreaseCoordBlockY(int speed)
        {
            this._hitbox.Y -= speed;
        }
        public void IncreaseCoordBlockY(int speed)
        {
            this._hitbox.Y += speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }




        // GETTER SETTER
        public List<BlockOld> ListBlock
        {
            get
            {
                return _listBlock;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return _texture;
            }
        }

        public Rectangle Hitbox
        {
            get
            {
                return _hitbox;
            }
            set
            {
                _hitbox = value;
            }
        }

        public bool Collidable
        {
            get
            {
                return _collidable;
            }
            set
            {
                _collidable = value;
            }
        }

    }
}
