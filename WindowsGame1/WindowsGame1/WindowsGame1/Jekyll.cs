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
        private double _jekyllBias;
        private double _jskillsPoints = 70;

        public Jekyll(int x, int y)
        {
            this.WidthSprite = 32;
            this.HeightSprite = 64;
            this._hitBox = new Rectangle(x, y, this.WidthSprite, this.HeightSprite);
            this._text = Ressources.Jekyll;
            this._pos = new Vector2(this._hitBox.X, this._hitBox.Y);
            this._dir = Vector2.Zero;
            this._health = 1;
            this._poids = 9;
            this._accelMode = 1;
            this._adrenaline = 0;
            this._jekyllBias = 0;

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
            this._jekyllBias++;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._text, this._hitBox, new Rectangle((this.FrameColumn * WidthSprite), (this.FrameLine * HeightSprite), WidthSprite, HeightSprite), Color.White, 0f, new Vector2(0, 0), this.Effect, 0f);
        }

        public double JekyllBias
        {
            get
            {
                return this._jekyllBias;
            }
            set
            {
                this._jekyllBias = value;
            }
        }

        public double JSkillPoints
        {
            get
            {
                return this._jskillsPoints;
            }
            set
            {
                this._jskillsPoints = value;
            }
        }
    }
}
