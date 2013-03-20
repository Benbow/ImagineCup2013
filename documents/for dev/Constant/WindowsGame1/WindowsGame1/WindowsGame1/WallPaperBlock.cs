using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class WallPaperBlock: Blocks
    {
        public static List<WallPaperBlock> WallPaperBlockList = new List<WallPaperBlock>();

        public WallPaperBlock (int x, int y, int w, int h, Texture2D text)
        {
            this._texture = text;
            this.HitBox = new Rectangle(x,y,w,h);
            this._isCollidable = false;
            this._isBreakable = false;
            WallPaperBlockList.Add(this);
            BlockList.Add(this);
        }
    }
}
