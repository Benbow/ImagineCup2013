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
        private Texture2D _invisible_spot_rec;
        private Rectangle _spot_rec;

        private int coeff;

        private bool _animate;
        private int _waitTime;
        private int _animationTime;
        private int _speed;
        private int _time;
        private int _initAnimationTime;
        private int _initWaitTime;
        private bool _reverse;
        private bool change;
        private bool changeSide;

        private Vector2 rotationOrigin;
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
            this.coeff = 0;

            this.change = false;
            this.changeSide = false;
            this._spot_text = Ressources.spot;
            this._spot_zone = new Rectangle();

            this._spot_zone = new Rectangle(x + text.Width / 2, y + text.Height, this._spot_text.Width, this._spot_text.Height);
            this.rotationOrigin = new Vector2(this._spot_text.Width / 2, 0);
            
            this._spot_rec = new Rectangle(this._spot_zone.X - this._spot_text.Width/2 + 75/2 - 30, this._spot_zone.Y, 125, 10);
            this._invisible_spot_rec = Ressources.Player;

            this.rotation = 0;
            this._speed = sp;

            CamerasBlockList.Add(this);
            BlockList.Add(this);
        }

        public void Update(GameTime time)
        {
            if (!this._animate && !change)
            {
                this._initAnimationTime = this._initAnimationTime*2;
                change = true;
            }
            this.Animate(time);
        }

        public void Draw(SpriteBatch spritebatch)
        {
           
            spritebatch.Draw(this._spot_text, new Vector2(this._spot_zone.X, this._spot_zone.Y), null, Color.PaleTurquoise, this.rotation, this.rotationOrigin, 1f, SpriteEffects.None, 0);
            spritebatch.Draw(this._invisible_spot_rec, this._spot_rec, Color.Red);
        }

        public void Animate(GameTime gametime)
        {
            this.gravity();
            this._time += gametime.ElapsedGameTime.Milliseconds;
            if (this._time >= 10)
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
                //int tmp = Convert.ToInt32(Math.Abs(this.coeff)*Math.Abs(this.rotation)*this._speed) / 4;
                
                //this._spot_rec.Width += coeff/10;
                int verify;
                if (change)
                    verify = this._initAnimationTime/2;
                else
                    verify = this._initAnimationTime;

                if (_reverse)
                {
                    this.coeff -= 1;
                    this.rotation -= 0.01f * this._speed;

                    if (coeff == 0 || coeff == -verify - 1)
                    {
                        changeSide = !changeSide;
                        if (coeff == -verify - 1)
                        {
                            this._spot_rec.X -= this._speed * 2;
                            this._spot_rec.X += this._speed * 3;
                        }

                    }
                    if (changeSide)
                    {
                        this._spot_rec.Width--;
                        this._spot_rec.X += this._speed*3;
                    }
                    else
                    {
                        this._spot_rec.Width++;
                        this._spot_rec.X += this._speed*2;
                    }
                }
                else
                {
                    this.coeff += 1;
                    this.rotation += 0.01f * this._speed;
                    if (coeff == 0 || coeff == verify - 1)
                    {
                        changeSide = !changeSide;
                        if (coeff == verify - 1)
                        {
                            this._spot_rec.X -= this._speed * 2;
                            this._spot_rec.X += this._speed * 3;
                        }
                    }
                    if (changeSide)
                    {
                        this._spot_rec.Width--;
                        this._spot_rec.X -= this._speed * 3;
                    }
                    else
                    {
                        this._spot_rec.Width++;
                        this._spot_rec.X -= this._speed * 2;
                    }
                }
            }
        }

        public void gravity()
        {
            Rectangle tempRec = this._spot_rec;
            tempRec.Y+=3;
            bool colide = false;
            foreach (StaticNeutralBlock block in StaticNeutralBlock.StaticNeutralList)
            {
                if (block.HitBox.Intersects(tempRec))
                {
                    colide = true;
                    break;
                }
            }
            if (!colide)
            {
                this._spot_rec.Y+=3;
            }
        }

        public Rectangle Spot_zone
        {
            get { return this._spot_zone; }
            set { this._spot_zone = value;  }
        }

        public Rectangle Spot_rec
        {
            get { return this._spot_rec; }
            set { this._spot_rec = value; }
        }

        public void DecreaseSpotCoordBlockX(int speed)
        {
            this._spot_zone.X -= speed;
            this._spot_rec.X -= speed;
        }
        public void IncreaseSpotCoordBlockX(int speed)
        {
            this._spot_zone.X += speed;
            this._spot_rec.X += speed;
        }
        public void DecreaseSpotCoordBlockY(int speed)
        {
            this._spot_zone.Y -= speed;
            this._spot_rec.Y -= speed;
        }
        public void IncreaseSpotCoordBlockY(int speed)
        {
            this._spot_zone.Y += speed;
            this._spot_rec.Y += speed;
        }
    }
}
