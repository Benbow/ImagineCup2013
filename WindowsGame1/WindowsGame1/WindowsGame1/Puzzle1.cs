using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Overload
{
    class Puzzle1 : Puzzle
    {
        public Puzzle1()
        {
            this._text = Ressources.enigmes_fond1;
            this._x = FirstGame.W / 2 - this._text.Width / 2;
            this._y = FirstGame.H / 2 - this._text.Height / 2;
            this._hitBox = new Rectangle(_x, _y, _text.Width, _text.Height);
            PuzzleList.Add(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._text, this._hitBox, Color.White);
        }

        public void Update(GamePadState pad, GameTime time)
        {

        }
    }
}
