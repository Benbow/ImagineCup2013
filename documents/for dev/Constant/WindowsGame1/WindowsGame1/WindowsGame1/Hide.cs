﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    class Hide : Player
    {
        private int _adrenaline;
        private double _hideBias;

        public Hide(int x, int y)
        {
            this._hitBox = new Rectangle(x, y, 55, 87);
            this._text = Ressources.Hide;
            this._pos = new Vector2(this._hitBox.X, this._hitBox.Y);
            this._dir = Vector2.Zero;
            this._health = 100;
            this._poids = 9;
            this._accelMode = 1;
            this._adrenaline = 0;
            this._hideBias = 0;

            this.FrameColumn = 4;
            this.FrameLine = 0;
            this.Direction = Direction.Right;
            this.Effect = SpriteEffects.None;
            this.Timer = 0;
            this.TimerMax = 5;

        }

        public void Update(MouseState mouse, KeyboardState keyboard)
        {
            this.CheckGravity();
            this.UpdateBias();
            switch (this.Direction)
            {
                case Direction.Left: this.Effect = SpriteEffects.FlipHorizontally;
                    break;
                case Direction.Right: this.Effect = SpriteEffects.None;
                    break;
            }

        }

        public void UpdateBias()
        {
            this._hideBias++;
        }
        

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._text, this._hitBox, new Rectangle((this.FrameColumn * 55), (this.FrameLine * 87), 55, 87), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
        }

        public double HideBias
        {
            get
            {
                return this._hideBias;
            }
            set
            {
                this._hideBias = value;
            }
        }
    }
}