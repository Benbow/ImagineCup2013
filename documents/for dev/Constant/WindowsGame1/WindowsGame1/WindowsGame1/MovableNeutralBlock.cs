using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class MovableNeutralBlock : Blocks
    {
        public static int count ;

        private float _speed;
        private Vector2 _dir;
        private float _initAnimationTime;
        private float _initWaitTime;
        private float _waitTime;
        private float _onAnimationTime;
        private bool _reverse;
        private bool _isAnimate;
        private bool _activate;
       

        int time;

        public static List<MovableNeutralBlock> MovableNeutralList = new List<MovableNeutralBlock>();

        //constructeur basique (indestructible et colidable)
        public MovableNeutralBlock(int x, int y, Texture2D text, Vector2 dir, int speed, float animationTime, float waitTime, bool isAnimate, bool reverse, bool activate)
        {
            this._texture = text;
            this._hitBox = new Rectangle(x, y, text.Width, text.Height);
            this._isBreakable = false;
            this._isCollidable = true;
            this._dir = dir;
            this._speed = speed;
            this._initAnimationTime = animationTime;
            this._initWaitTime = waitTime;
            this._waitTime = waitTime;
            this._onAnimationTime = animationTime;
            this._isAnimate = isAnimate;
            this._reverse = reverse;
            this._activate = activate;

            count++;
            MovableNeutralList.Add(this);
            BlockList.Add(this);
        }

        //constructeur complet
        public MovableNeutralBlock(int x, int y, Texture2D text, bool isBreakable, bool isCollidable, int health, Vector2 dir, int speed, float animationTime, float waitTime, bool isAnimate, bool reverse, bool activate)
        {
            this._texture = text;
            this._hitBox = new Rectangle(x, y, text.Width, text.Height);
            this._isBreakable = isBreakable;
            this._isCollidable = isCollidable;
            this._health = health;
            this._dir = dir;
            this._speed = speed;
            this._initAnimationTime = animationTime;
            this._initWaitTime = waitTime;
            this._waitTime = waitTime;
            this._onAnimationTime = animationTime;
            this._isAnimate = isAnimate;
            this._reverse = reverse;
            this._activate = activate;

            count++;
            MovableNeutralList.Add(this);
            BlockList.Add(this);
        }

        public void Update(GameTime gametime)
        {
            if(this._activate)
                this.Animate(gametime);
        }

        public void Animate(GameTime gametime)
        {
            time += gametime.ElapsedGameTime.Milliseconds;
            if (time > 1000)
            {
                time = 0;
                if (this._isAnimate)
                {
                    this._onAnimationTime--;
                    if (this._onAnimationTime <= 0)
                    {
                        this._waitTime = _initWaitTime;
                        this._isAnimate = false;
                    }
                }
                else
                {
                    this._waitTime--;
                    if (this._waitTime <= 0)
                    {
                        this._onAnimationTime = _initAnimationTime;
                        this._isAnimate = true;
                        this._reverse = !this._reverse;
                    }
                }
            }


            if (this._isAnimate)
            {
                if (_reverse)
                {
                    this._hitBox.X -= (int)_dir.X * (int)this._speed;
                    this._hitBox.Y -= (int)_dir.Y * (int)this._speed;
                }
                else
                {
                    this._hitBox.X += (int)_dir.X * (int)this._speed;
                    this._hitBox.Y += (int)_dir.Y * (int)this._speed;
                }
            }
        }

        public bool Activate
        {
            get { return this._activate; }
            set { this._activate = value; }
        }

        public float Speed
        {
            get { return this._speed; }
            set { this._speed = value; }
        }

        public bool Reverse
        {
            get { return this._reverse; }
            set { this._reverse = value; }
        }

        public bool IsAnimate
        {
            get { return this._isAnimate; }
            set { this._isAnimate = value; }
        }
    }
}
