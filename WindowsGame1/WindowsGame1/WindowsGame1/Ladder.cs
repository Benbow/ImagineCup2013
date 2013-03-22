using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overload
{
    class Ladder : Blocks
    {
        public static List<Ladder> LadderList = new List<Ladder>();

        public Ladder(int x, int y, bool hidevision, bool jekyllvision, Texture2D text)
        {
            this._isBreakable = false;
            this._isCollidable = false;
            this._texture = text;
            this._isHideVisible = hidevision;
            this._isJekyllVisible = jekyllvision;
            this._hitBox = new Rectangle(x, y, _texture.Width, _texture.Height);
            LadderList.Add(this);
            BlockList.Add(this);
        }
    }
}
