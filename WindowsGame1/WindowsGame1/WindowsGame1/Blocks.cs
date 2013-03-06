using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class  Blocks
    {
        protected bool _isBreakable = false;
        protected bool _isCollidable = true;
        protected bool _isHideVisible = false;
        protected bool _isJekyllVisible = false;
        protected int _health;
        protected Rectangle _hitBox;
        protected Texture2D _texture;

        public static List<Blocks> BlockList = new List<Blocks>();

        public void DecreaseCoordBlockX(int speed)
        {
            this._hitBox.X -= speed;
        }
        public void IncreaseCoordBlockX(int speed)
        {
            this._hitBox.X += speed;
        }
        public void DecreaseCoordBlockY(int speed)
        {
            this._hitBox.Y -= speed;
        }
        public void IncreaseCoordBlockY(int speed)
        {
            this._hitBox.Y += speed;
        }



        //GETTER SETTERS
        public bool IsBreakable
        {
            get
            {
                return _isBreakable;
            }
        }

        public bool IsCollidable
        {
            get
            {
                return _isCollidable;
            }
        }

        public int Health
        {
            get
            {
                return _health;
            }

            set
            {
                _health = value;
            }
        }

        public int X
        {
            get
            {
                return _hitBox.X;
            }

            set
            {
                _hitBox.X = value;
            }
        }

        public int Y
        {
            get
            {
                return _hitBox.Y;
            }
            set
            {
                _hitBox.Y = value;
            }
        }

        public Rectangle HitBox
        {
            get
            {
                return _hitBox;
            }

            set
            {
                _hitBox = value;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return _texture;
            }
            set
            {
                _texture = value;
            }
        }
        public bool IsHideVisible
        {
            get { return this._isHideVisible; }
            set { this._isHideVisible = value; }
        }
        public bool IsJekyllVisible
        {
            get { return this._isJekyllVisible; }
            set { this._isJekyllVisible = value; }
        }
    }
}
