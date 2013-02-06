﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public enum Direction
    {
        Left, Right
    }

    abstract class Player
    {
        protected Rectangle _hitBox;
        protected Vector2 _pos;
        protected Vector2 _dir;

        protected Direction Direction;
        protected int FrameColumn;
        protected int FrameLine;
        protected SpriteEffects Effect;
        protected int Timer;
        protected int TimerMax;
        protected bool _isJumping = false;

        protected float _speed;
        protected float _poids;
        protected int _health;
        protected Texture2D _text;
        protected bool _isFalling;
        protected float _fallingSpeed = 0;
        protected float _impulsion;
        protected float _speedInAir;
        protected int _accelMode;
        protected bool _statut = false;

        protected KeyboardState oldKeyboard;

        

        public void Animate()
        {
            this.Timer++;
            if (this.Timer == this.TimerMax)
            {
                this.Timer = 0;
                this.FrameColumn++;
                if (this.FrameColumn > 11)
                {
                    this.FrameColumn = 0;
                }
            }
        }

        public bool Switch(KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.B) && oldKeyboard.IsKeyUp(Keys.B))
                _statut = !_statut;

            oldKeyboard = keyboard;

            return _statut;
        }

        public void MovePlayerLeft(bool player)
        {
            this.Direction = Direction.Left;
            if(player)
            {
                if (this._isJumping)
                {
                    this._hitBox.X -= (int)this._speedInAir;
                }
                else
                {
                    this._hitBox.X -= (int)this._speed;
                }
            }
            if (!this._isJumping)
            {
                this.Animate();
            }
        }

        public void MovePlayerRight(bool player)
        {
            this.Direction = Direction.Right;
            if (player)
            {
                if (this._isJumping)
                {
                    this._hitBox.X += (int)this._speedInAir;
                }
                else
                {
                    this._hitBox.X += (int)this._speed;
                }
                   
            }
            if (!this._isJumping)
                this.Animate();
        }

        public void JumpPlayer()
        {
            if(!this._isFalling)
            {
                this._isJumping = true;
                this.FrameColumn = 0;
                this.FrameLine = 1;
                this._fallingSpeed -= this._impulsion;
            }
        }

        public void BlockPLayer()
        {
            this.FrameColumn = 4;
            this.FrameLine = 0;
            this._dir = Vector2.Zero;
            this.Timer = 0;
        }

        public void CheckGravity()
        {
            bool colide = false;
            Rectangle futurPos = this._hitBox;
            futurPos.Y += 1 + (int)this._fallingSpeed;
            foreach (StaticNeutralBlock block in StaticNeutralBlock.StaticNeutralList)
            {
                if (futurPos.Intersects(block.HitBox))
                {
                    colide = true;
                    if (this._fallingSpeed > 0)
                    {
                        this._hitBox.Y = block.HitBox.Y - this._hitBox.Height;
                    }
                    break;
                }
            }

            if (!colide)
            {
                int i = 0;
                bool playerMove = false;
                foreach (Blocks block in Blocks.BlockList)
                {
                    i++;
                    if (i <= 3)
                    {
                        if ((i == 3 && this._hitBox.Y + (FirstGame.H / 2) + (this._hitBox.Height / 2) - (block.HitBox.Height * 2) > block.HitBox.Y))
                        {
                            playerMove = true;
                        }
                        else if ((i == 1 && this._hitBox.Y - (FirstGame.H / 2) + block.HitBox.Height < block.HitBox.Y))
                        {
                            playerMove = true;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (playerMove)
                {
                    if (this._fallingSpeed >= 0)
                        this._fallingSpeed += 0.15f * (this._poids / 4);
                    else
                        this._fallingSpeed += 0.10f * (this._poids / 4);
                    if (this._isJumping)
                    {
                        this.FrameColumn = 0;
                        this.FrameLine = 1;
                    }
                    this._isFalling = true;
                    int diff = this._hitBox.Y - futurPos.Y;
                    this._hitBox.Y -= diff;
                }
                else
                {
                    if (this._fallingSpeed >= 0)
                        this._fallingSpeed += 0.15f * (this._poids / 4);
                    else
                        this._fallingSpeed += 0.10f * (this._poids / 4);
                    if (this._isJumping)
                    {
                        this.FrameColumn = 0;
                        this.FrameLine = 1;
                    }
                    this._isFalling = true;
                    foreach (Blocks block in Blocks.BlockList)
                    {
                        block.DecreaseCoordBlockY(1 + (int)this._fallingSpeed);
                    }
                }
            }
            else
            {
                this._isFalling = false;

                if (this._isJumping && this._fallingSpeed >= 0)
                {
                    this.FrameColumn = 4;
                    this.FrameLine = 0;
                }
                if (this._fallingSpeed >= 0)
                    this._isJumping = false;

                this._fallingSpeed = 0;
            }
        }

        public void SetAccelSpeed()
        {
            switch (this._accelMode)
            {
                case 1:
                    this._speed = 2f;
                    this._speedInAir = 1f;
                    this._impulsion = 10.5f;
                    break;

                case 2:
                    this._speed = 4f;
                    this._speedInAir = 4f;
                    this._impulsion = 9f;
                    break;

                case 3:
                    this._speed = 8f;
                    this._speedInAir = 10f;
                    this._impulsion = 7.5f;
                    break;
            }
        }

        public void InitChange(int x, int y, Direction dir)
        {
            this._hitBox.X = x;
            this._hitBox.Y = y;
            this.Direction = dir;
        }

        /*public void IncreaseCoordY(int speed)
        {
            this._hitBox.Y += speed;
        }
        public void DecreaseCoordY(int speed)
        {
            this._hitBox.Y += speed;
        }*/

        //getter setter
        public bool IsJumping
        {
            get
            {
                return this._isJumping;
            }
            set
            {
                this._isJumping = value;
            }
        }

        public float Speed
        {
            get
            {
                return this._speed;
            }
            set
            {
                this._speed = value;
            }
        }

        public float FallingSpeed
        {
            get
            {
                return this._fallingSpeed;
            }
            set
            {
                this._fallingSpeed = value;
            }
        }

        public Rectangle HitBox
        {
            get
            {
                return this._hitBox;
            }
            set
            {
                this._hitBox = value;
            }
        }

        public float SpeedInAir
        {
            get
            {
                return this._speedInAir;
            }
            set
            {
                this._speedInAir = value;
            }
        }

        public int AccelMode
        {
            get
            {
                return this._accelMode;
            }
            set
            {
                this._accelMode = value;
            }
        }

        public Direction DirectionPlayer
        {
            get
            {
                return this.Direction;
            }
            set
            {
                this.Direction = value;
            }
        }
        
    }
}
