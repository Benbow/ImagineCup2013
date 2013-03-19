using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    class MovableEnnemyBlock : Blocks
    {
        private float _speed;
        private Vector2 _dir;
        private int _strength;
        private bool _haveSpotted;

        private bool _isOnAlert;
        private int distance = 0;
        private int init_distance = 0;
        private bool _isReturn = false;
        private bool side;

        private float _initAnimationTime;
        private float _initWaitTime;
        private float _waitTime;
        private float _onAnimationTime;
        private bool _reverse;
        private bool _isAnimate;
        private bool _isOnGravity;

        private Rectangle spotted_zone;
        private Texture2D spotted_text;


        private bool _isFalling = false;
        private float _fallingSpeed = 0;
        private float _poids;
        private bool _isJumping = false;

        private SpriteEffects Effects;
        private int FrameColumn;
        private int SpriteWidth;
        private int SpriteHeight;

        bool down = false;

        public static List<MovableEnnemyBlock> MovableEnnemyList = new List<MovableEnnemyBlock>();

        int time;
        int time2;
        int time3;
        int time4;
        int waitAlert;

        //constructeur complet
        public MovableEnnemyBlock(int x, int y, Texture2D text, bool isBreakable, bool isCollidable, int health, Vector2 dir, int speed, int strength, bool haveSpotted, float animationTime, float waitTime, bool isAnimate, bool reverse, bool isOnGravity, float poids)
        {
            this._texture = text;
            this.SpriteWidth = 50;
            this.SpriteWidth = 64;
            this._hitBox = new Rectangle(x, y, this.SpriteWidth, this.SpriteWidth);
            this._isBreakable = isBreakable;
            this._isCollidable = isCollidable;
            this._health = health;
            this._dir = dir;
            this._speed = speed;
            this._strength = strength;
            this._haveSpotted = haveSpotted;
            this._initAnimationTime = animationTime;
            this._initWaitTime = waitTime;
            this._waitTime = waitTime;
            this._onAnimationTime = animationTime;
            this._isAnimate = isAnimate;
            this._reverse = reverse;
            this._isOnGravity = isOnGravity;
            this._poids = poids;
            this.FrameColumn = 1;


            this.spotted_zone = new Rectangle(this._hitBox.X + this.SpriteWidth, this.HitBox.Y, 200, this._hitBox.Height);
            this.spotted_text = Ressources.spot_zone;

            MovableEnnemyList.Add(this);
            //BlockList.Add(this);
        }

        public void Update(GameTime gameTime, Player player, KeyboardState keyboard)
        {

            if (this._isOnGravity)
            {
                this.CheckGravity(gameTime);
            }

            if (!this._isFalling)
            {

                if (this._haveSpotted)
                {
                    this.AnimateAlert(gameTime, player);
                }
                else if (this.IsOnAlert)
                {
                    this.AnimateOnAlert(gameTime);
                }
                else
                {
                    this.AnimateNormal(gameTime);
                }

            }
            else if (this._isJumping)
            {

                if (this._haveSpotted)
                {
                    this.AnimateAlert(gameTime, player);
                }
                else if (this.IsOnAlert)
                {
                    this.AnimateOnAlert(gameTime);
                }
                else
                {
                    this.AnimateNormal(gameTime);
                }

            }
        }

        public void AnimateNormal(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.Milliseconds;
            time2 += gameTime.ElapsedGameTime.Milliseconds;
            if (time2 > 500 / this._speed)
            {
                time2 = 0;
                if (_isAnimate)
                {
                    this.FrameColumn++;
                    if (this.FrameColumn > 3)
                        this.FrameColumn = 0;
                }
                else
                {
                    this.FrameColumn = 0;
                }
            }
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
                    this.spotted_zone.X = this._hitBox.X - this.spotted_zone.Width;
                    this.spotted_zone.Y -= (int)_dir.Y * (int)this._speed;
                    this.Effects = SpriteEffects.FlipHorizontally;

                }
                else
                {
                    this._hitBox.X += (int)_dir.X * (int)this._speed;
                    this._hitBox.Y += (int)_dir.Y * (int)this._speed;
                    this.spotted_zone.X = this._hitBox.X + this._hitBox.Width;
                    this.spotted_zone.Y += (int)_dir.Y * (int)this._speed;
                    this.Effects = SpriteEffects.None;
                }
            }
            else
            {
                if (_reverse)
                {
                    this.spotted_zone.X = this._hitBox.X - this.spotted_zone.Width;
                    this.spotted_zone.Y -= (int)_dir.Y * (int)this._speed;
                    this.Effects = SpriteEffects.FlipHorizontally;

                }
                else
                {
                    this.spotted_zone.X = this._hitBox.X + this._hitBox.Width;
                    this.spotted_zone.Y += (int)_dir.Y * (int)this._speed;
                    this.Effects = SpriteEffects.None;
                }
            }
        }

        public void AnimateAlert(GameTime gameTime, Player player)
        {
            if (_reverse)
            {
                if (IsOnAlert)
                {
                    if (Side)
                    {
                        this.spotted_zone.X = this._hitBox.X + this._hitBox.Width;
                        this.spotted_zone.Y += (int)_dir.Y * (int)this._speed;
                        this.Effects = SpriteEffects.None;
                    }
                    else
                    {
                        this.spotted_zone.X = this._hitBox.X - this.spotted_zone.Width;
                        this.spotted_zone.Y -= (int)_dir.Y * (int)this._speed;
                        this.Effects = SpriteEffects.FlipHorizontally;
                    }
                }
                else
                {
                    this.spotted_zone.X = this._hitBox.X - this.spotted_zone.Width;
                    this.spotted_zone.Y -= (int) _dir.Y*(int) this._speed;
                    this.Effects = SpriteEffects.FlipHorizontally;
                }

            }
            else
            {
                if (IsOnAlert)
                {
                    if (Side)
                    {
                        this.spotted_zone.X = this._hitBox.X + this._hitBox.Width;
                        this.spotted_zone.Y += (int)_dir.Y * (int)this._speed;
                        this.Effects = SpriteEffects.None;
                    }
                    else
                    {
                        this.spotted_zone.X = this._hitBox.X - this.spotted_zone.Width;
                        this.spotted_zone.Y -= (int)_dir.Y * (int)this._speed;
                        this.Effects = SpriteEffects.FlipHorizontally;
                    }
                }
                else
                {
                    this.spotted_zone.X = this._hitBox.X + this._hitBox.Width;
                    this.spotted_zone.Y += (int)_dir.Y * (int)this._speed;
                    this.Effects = SpriteEffects.None;
                }
                
            }

            if (!player.HitBox.Intersects(this.spotted_zone))
            {
                time3 += gameTime.ElapsedGameTime.Milliseconds;
                if (time3 > 2000)
                {
                    this._haveSpotted = false;
                    time3 = 0;
                }
            }

            time4 += gameTime.ElapsedGameTime.Milliseconds;
            if (time4 > 500)
            {
                time4 = 0;
                if (this._reverse)
                {
                    if(this._isOnAlert && this.side)
                        new BulletBlock(this._hitBox.X + this._hitBox.Width, this.HitBox.Y + (this._hitBox.Height) / 3, new Vector2(1, 0), this._strength / 5);
                    else
                        new BulletBlock(this._hitBox.X, this.HitBox.Y + (this._hitBox.Height) / 3, new Vector2(-1, 0), this._strength / 5);
                }
                else
                {
                    if (this._isOnAlert && !this.side)
                        new BulletBlock(this._hitBox.X, this.HitBox.Y + (this._hitBox.Height) / 3, new Vector2(-1, 0), this._strength / 5);
                    else
                        new BulletBlock(this._hitBox.X + this._hitBox.Width, this.HitBox.Y + (this._hitBox.Height) / 3, new Vector2(1, 0), this._strength / 5);
                }
            }
        }

        public void AnimateOnAlert(GameTime gameTime)
        {
            if (this._isOnAlert && !this._isReturn)
            {
                if (Side)
                {
                    this._hitBox.X -= (int)this._speed;
                    this.spotted_zone.X = this._hitBox.X - this.spotted_zone.Width;
                    this.Effects = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    this._hitBox.X += (int)this._speed;
                    this.spotted_zone.X = this._hitBox.X + this._hitBox.Width;
                    this.Effects = SpriteEffects.None;
                }
                this.distance -= (int)this._speed;
                if (this.distance <= 0)
                {
                    this._isReturn = true;
                    this.distance = initDistance;
                    this.waitAlert = 100;
                }
            }
            else if (this._isOnAlert && this._isReturn)
            {
                if (this.waitAlert > 0)
                {
                    this.waitAlert--;
                    if (Side)
                        this.spotted_zone.X = this._hitBox.X - this.spotted_zone.Width;
                    else
                        this.spotted_zone.X = this._hitBox.X + this._hitBox.Width;
                }
                if (this.waitAlert == 0)
                {
                    if (Side)
                    {
                        this._hitBox.X += (int) this._speed;
                        this.spotted_zone.X = this._hitBox.X + this._hitBox.Width;
                        this.Effects = SpriteEffects.None;
                    }
                    else
                    {
                        this._hitBox.X -= (int) this._speed;
                        this.spotted_zone.X = this._hitBox.X - this.spotted_zone.Width;
                        this.Effects = SpriteEffects.FlipHorizontally;
                    }
                    this.distance -= (int) this._speed;
                    if (this.distance <= 0)
                    {
                        this._isReturn = false;
                        this.IsOnAlert = false;
                        this.initDistance = 0;
                        this.distance = 0;
                    }
                }
            }
        }

        public void CheckGravity(GameTime gameTime)
        {
            if (this.IsCollidable)
            {
                bool colide = false;
                Rectangle futurPos = this._hitBox;
                futurPos.Y += 1 + (int)this._fallingSpeed;
                foreach (StaticNeutralBlock block in StaticNeutralBlock.StaticNeutralList)
                {
                    if (futurPos.Intersects(block.HitBox))
                    {
                        colide = true;
                        this._hitBox.Y = block.HitBox.Y - this._hitBox.Height;
                        this.spotted_zone.Y = block.HitBox.Y - this._hitBox.Height;
                        break;
                    }
                }

                if (!colide)
                {
                    this._fallingSpeed += 0.10f * (this._poids / 4);
                    this._isFalling = true;
                    int diff = this._hitBox.Y - futurPos.Y;
                    this._hitBox.Y -= diff;
                    this.spotted_zone.Y -= diff;
                }
                else
                {
                    this._isFalling = false;
                    this._isJumping = false;
                    this._fallingSpeed = 0;
                }
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (!_haveSpotted)
            {
                this._texture = Ressources.ennemySprite;
                this.SpriteWidth = 50;
                this.SpriteHeight = 64;
                this._hitBox = new Rectangle(this._hitBox.X, this._hitBox.Y, this.SpriteWidth, this.SpriteHeight);
                spritebatch.Draw(this._texture, this._hitBox, new Rectangle((this.FrameColumn * this.SpriteWidth), (0 * this.SpriteHeight), this.SpriteWidth, this.SpriteHeight), Color.White, 0f, new Vector2(0, 0), this.Effects, 0f);
            }
            else
            {
                this._texture = Ressources.ennemySimple;
                this.SpriteWidth = 64;
                this.SpriteHeight = 64;
                this._hitBox = new Rectangle(this._hitBox.X, this._hitBox.Y, this.SpriteWidth, this.SpriteHeight);
                spritebatch.Draw(this._texture, this._hitBox, null, Color.White, 0f, new Vector2(0, 0), this.Effects, 0f);
            }

            spritebatch.Draw(this.spotted_text, this.spotted_zone, Color.White);
        }

        public Rectangle SpotZone
        {
            get { return this.spotted_zone; }
            set { this.spotted_zone = value; }
        }

        public bool HaveSpotted
        {
            get { return this._haveSpotted; }
            set { this._haveSpotted = value; }
        }

        public int Strength
        {
            get { return this._strength; }
        }

        public int Distance
        {
            get { return this.distance; }
            set { this.distance = value; }
        }

        public int initDistance
        {
            get { return this.init_distance; }
            set { this.init_distance = value; }
        }

        public bool IsOnAlert
        {
            get { return this._isOnAlert; }
            set { this._isOnAlert = value; }
        }

        public bool Side
        {
            get { return this.side; }
            set { this.side = value; }
        }
    }
}
