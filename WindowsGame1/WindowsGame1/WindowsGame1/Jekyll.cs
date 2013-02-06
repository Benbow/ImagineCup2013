using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    class Jekyll : Player
    {
        private int _adrenaline;

        public Jekyll(int x, int y)
        {
            this._hitBox = new Rectangle(x, y, 55, 87);
            this._text = Ressources.Jekyll;
            this._pos = new Vector2(this._hitBox.X, this._hitBox.Y);
            this._dir = Vector2.Zero;
            this._health = 100;
            this._poids = 9;
            this._accelMode = 1;
            this._adrenaline = 0;

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

            switch (this.Direction)
            {
                case Direction.Left: this.Effect = SpriteEffects.FlipHorizontally;
                    break;
                case Direction.Right: this.Effect = SpriteEffects.None;
                    break;
            }
            
        }

        //public void CheckGravity()
        //{
        //    bool colide = false;
        //    Rectangle futurPos = this._hitBox;
        //    futurPos.Y += 1 + (int)this._fallingSpeed;
        //    foreach (StaticNeutralBlock block in StaticNeutralBlock.StaticNeutralList)
        //    {
        //        if (futurPos.Intersects(block.HitBox))
        //        {
        //            colide = true;
        //            if (this._fallingSpeed > 0)
        //            {
        //                this._hitBox.Y = block.HitBox.Y - this._hitBox.Height;
        //            }
        //            break;
        //        }
        //    }

        //    if (!colide)
        //    {
        //        int i = 0;
        //        bool playerMove = false;
        //        foreach (Blocks block in Blocks.BlockList)
        //        {
        //            i++;
        //            if (i <= 3)
        //            {
        //                if ((i == 3 && this._hitBox.Y + (FirstGame.H / 2) + (this._hitBox.Height/2) - (block.HitBox.Height*2) > block.HitBox.Y))
        //                {
        //                    playerMove = true;
        //                }
        //                else if ((i == 1 && this._hitBox.Y - (FirstGame.H / 2) + block.HitBox.Height < block.HitBox.Y))
        //                {
        //                    playerMove = true;
        //                }
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }

        //        if (playerMove)
        //        {
        //            if (this._fallingSpeed >= 0)
        //                this._fallingSpeed += 0.15f * (this._poids / 4);
        //            else
        //                this._fallingSpeed += 0.10f * (this._poids / 4);
        //            if (this._isJumping)
        //            {
        //                this.FrameColumn = 0;
        //                this.FrameLine = 1;
        //            }
        //            this._isFalling = true;
        //            int diff = this._hitBox.Y - futurPos.Y;
        //            this._hitBox.Y -= diff;
        //        }
        //        else
        //        {
        //            if (this._fallingSpeed >= 0)
        //                this._fallingSpeed += 0.15f * (this._poids / 4);
        //            else
        //                this._fallingSpeed += 0.10f * (this._poids / 4);
        //            if (this._isJumping)
        //            {
        //                this.FrameColumn = 0;
        //                this.FrameLine = 1;
        //            }
        //            this._isFalling = true;
        //            foreach (Blocks block in Blocks.BlockList)
        //            {
        //                block.DecreaseCoordBlockY(1+(int)this._fallingSpeed);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        this._isFalling = false;

        //        if (this._isJumping && this._fallingSpeed >= 0)
        //        {
        //            this.FrameColumn = 4;
        //            this.FrameLine = 0;
        //        }
        //        if (this._fallingSpeed >= 0)
        //            this._isJumping = false;
                
        //        this._fallingSpeed = 0;
        //    }
        //}

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._text, this._hitBox, new Rectangle((this.FrameColumn * 55), (this.FrameLine * 87), 55, 87), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
        }
    }
}
