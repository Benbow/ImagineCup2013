using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class EndZoneBlock : Blocks
    {
        public static List<EndZoneBlock> EndZoneBlockList = new List<EndZoneBlock>();

        public EndZoneBlock(int x, int y, Texture2D text)
        {
            this._texture = text;
            this._hitBox = new Rectangle(x, y, text.Width, text.Height);
            this._isBreakable = false;
            this._isCollidable = false;
            this._isHideVisible = false;
            this._isJekyllVisible = false;

            EndZoneBlockList.Add(this);
            BlockList.Add(this);
        }
    }
}
