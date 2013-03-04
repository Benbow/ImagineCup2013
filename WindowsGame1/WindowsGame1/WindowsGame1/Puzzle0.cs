using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class Puzzle0 : Puzzle
    {
       public Puzzle0()
       {
           this._x = 100;
           this._y = 50;
           this._text = Ressources.enigmes_fond;
           this._hitBox = new Rectangle(_x, _y, _text.Width, _text.Height);
           PuzzleList.Add(this);
       }

       public void Draw(SpriteBatch spriteBatch)
       {
            spriteBatch.Draw(this._text, this._hitBox, Color.White);
       }
    }
}
