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

        private float _initAnimationTime;
        private float _initWaitTime;
        private float _waitTime;
        private float _onAnimationTime;
        private bool _reverse;
        private bool _isAnimate;
        private bool _isOnGravity;

        private bool _isFalling = false;
        private float _fallingSpeed = 0;
        private float _poids;
        private bool _isJumping = false;

        bool down = false;
        
        public static List<MovableEnnemyBlock> MovableEnnemyList = new List<MovableEnnemyBlock>();

        int time;

        //constructeur complet
        public MovableEnnemyBlock(int x, int y, Texture2D text, bool isBreakable, bool isCollidable, int health, Vector2 dir, int speed, int strength, bool haveSpotted, float animationTime, float waitTime, bool isAnimate, bool reverse, bool isOnGravity, float poids)
        {
            this._texture = text;
            this._hitBox = new Rectangle(x, y, text.Width, text.Height);
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

            MovableEnnemyList.Add(this);
            BlockList.Add(this);
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
                else
                {
                    this.AnimateNormal(gameTime, keyboard);
                }
            }
            else if (this._isJumping)
            {
                if (this._haveSpotted)
                {
                    this.AnimateAlert(gameTime, player);
                }
                else
                {
                    this.AnimateNormal(gameTime, keyboard);
                }
            }
        }

        public void AnimateNormal(GameTime gameTime, KeyboardState keyboard)
        {
            
        }

        public void AnimateAlert(GameTime gameTime, Player player)
        {

        }

        public void CheckGravity(GameTime gameTime)
        {   
            if (this.IsCollidable)
            {
                bool colide = false;
                Rectangle futurPos = this._hitBox;
                futurPos.Y += 1+(int)this._fallingSpeed;
                foreach (StaticNeutralBlock block in StaticNeutralBlock.StaticNeutralList)
                {
                    if (futurPos.Intersects(block.HitBox))
                    {
                        colide = true;
                        this._hitBox.Y = block.HitBox.Y-block.HitBox.Height;
                        break;
                    }
                }

                if (!colide)
                {
                    this._fallingSpeed += 0.10f * (this._poids / 4);
                    this._isFalling = true;
                    int diff = this._hitBox.Y - futurPos.Y;
                    this._hitBox.Y -= diff;
                }
                else
                {
                    this._isFalling = false;
                    this._isJumping = false;
                    this._fallingSpeed = 0;
                }
            }
        }
    }
}
