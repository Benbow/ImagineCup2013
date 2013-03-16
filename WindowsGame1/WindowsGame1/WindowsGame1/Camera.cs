using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class Camera : Blocks
    {
        public static List<Camera> CamerasBlockList = new List<Camera>();
        private Texture2D _spot_text;
        private Rectangle _spot_zone;
        private bool _animate;
        private int _waitTime;
        private int _animationTime;
        private int _speed;
        private int _time;
        private int _initAnimationTime;
        private int _initWaitTime;
        private bool _reverse;

        private Vector2 rotationOrigin;
        private Vector2 _position;
        private float rotation;
            
        public Camera(int x, int y, bool hv, bool jv, int sp, int waitTime, int animTime, int he, Texture2D text)
        {
            this._isActive = true;
            this.IsHideVisible = hv;
            this.IsJekyllVisible = jv;
            this._health = he;
            this._texture = text;
            this._hitBox = new Rectangle(x, y, text.Width, text.Height);

            this._animate = true;
            this._reverse = false;
            this._waitTime = waitTime;
            this._initWaitTime = waitTime;
            this._animationTime = animTime;
            this._initAnimationTime = animTime;
            
            this._spot_text = Ressources.spot;
            this._spot_zone = new Rectangle();

            this._spot_zone = new Rectangle(x + text.Width / 2, y + text.Height, this._spot_text.Width, this._spot_text.Height);
            this.rotationOrigin = new Vector2(this._spot_text.Width / 2, 0);
            
            this.rotation = 0;
            this._speed = sp;

            CamerasBlockList.Add(this);
            BlockList.Add(this);
        }

        public void Update(GameTime time)
        {
            Console.WriteLine(this._spot_zone.X);
            if (this._animate)
            {
                this.Animate(time);
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(this._spot_text, new Vector2(this._spot_zone.X, this._spot_zone.Y), null, Color.PaleTurquoise, this.rotation, this.rotationOrigin, 1f, SpriteEffects.None, 0);
        }

        public void Animate(GameTime gametime)
        {
            this._time += gametime.ElapsedGameTime.Milliseconds;
            if (this._time > 1000)
            {
                this._time = 0;
                if (this._animate)
                {
                    this._animationTime--;
                    if (this._animationTime <= 0)
                    {
                        this._waitTime = _initWaitTime;
                        this._animate = false;
                    }
                }
                else
                {
                    this._waitTime--;
                    if (this._waitTime <= 0)
                    {
                        this._animationTime = _initAnimationTime;
                        this._animate = true;
                        this._reverse = !this._reverse;
                    }
                }
            }


            if (this._animate)
            {
                if (_reverse)
                {
                    this.rotation -= 0.01f*this._speed;
                }
                else
                {
                    this.rotation += 0.01f * this._speed;
                }
            }
        }

        public Rectangle Spot
        {
            get { return this._spot_zone; }
            set { this._spot_zone = value;  }
        }

        public void DecreaseSpotCoordBlockX(int speed)
        {
            this._spot_zone.X -= speed;
        }
        public void IncreaseSpotCoordBlockX(int speed)
        {
            this._spot_zone.X += speed;
        }
        public void DecreaseSpotCoordBlockY(int speed)
        {
            this._spot_zone.Y -= speed;
        }
        public void IncreaseSpotCoordBlockY(int speed)
        {
            this._spot_zone.Y += speed;
        }
    }
}
