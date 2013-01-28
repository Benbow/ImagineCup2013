using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jekyll
{
    public enum Direction
    {
        Left, Right
    }

    class Player
    {
        protected Rectangle HitBox;
        protected Vector2 _pos;
        protected Vector2 _dir;

        protected Direction Direction;
        protected int FrameColumn;
        protected int FrameLine;
        protected SpriteEffects Effect;
        protected int Timer;
        protected int TimerMax;
        protected bool jump = true;

        protected KeyboardState oldKeyboard;

        public Player()
        {
            HitBox = new Rectangle(0, 200, 55, 87);
            _pos = new Vector2(this.HitBox.X, this.HitBox.Y);
            _dir = Vector2.Zero;
            this.FrameColumn = 4;
            this.FrameLine = 0;
            this.Direction = Direction.Right;
            this.Effect = SpriteEffects.None;
            this.Timer = 0;
            this.TimerMax = 5;
        }

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

        public void Update(MouseState mouse, KeyboardState keyboard)
        {
            _pos += _dir;

            if (keyboard.IsKeyDown(Keys.Left))
            {
                this.Direction = Direction.Left;
                if ((this._pos.X) > 0)
                {
                    this._pos.X -= 2f;
                    if (!this.jump)
                        this.Animate();
                }
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                this.Direction = Direction.Right;
                if ((this._pos.X + this.HitBox.Width) < 800)
                {
                    this._pos.X += 2f;
                    if (!this.jump)
                        this.Animate();
                }
            }

            if (keyboard.IsKeyDown(Keys.Space) && this.jump == false && oldKeyboard.IsKeyUp(Keys.Space))
            {
                this._pos.Y -= 10;
                this._dir.Y = -4f;
                this.jump = true;
                this.FrameColumn = 0;
                this.FrameLine = 1;
            }

            if (this.jump == true)
                this._dir.Y += 0.20f;

            if (this._pos.Y + this.HitBox.Height >= 450)
                this.jump = false;

            if (this.jump == false)
            {
                this._dir.Y = 0;
                this.FrameLine = 0;
            }

            switch (this.Direction)
            {
                case Direction.Left: this.Effect = SpriteEffects.FlipHorizontally;
                    break;
                case Direction.Right: this.Effect = SpriteEffects.None;
                    break;
            }

            if (keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.Left) && !this.jump)
            {
                this.FrameColumn = 4;
                this.FrameLine = 0;
                this._dir = Vector2.Zero;
                this.Timer = 0;
            }

            this.HitBox.X = (int)_pos.X;
            this.HitBox.Y = (int)_pos.Y;

            oldKeyboard = keyboard;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Resources.Jekyll, this.HitBox, new Rectangle((this.FrameColumn * 55), (this.FrameLine * 87), 55, 87), Color.White,
                0f, new Vector2(0, 0), this.Effect, 0f);
        }

    }
}
