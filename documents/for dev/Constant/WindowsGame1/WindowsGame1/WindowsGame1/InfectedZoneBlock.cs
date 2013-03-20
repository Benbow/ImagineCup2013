using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class InfectedZoneBlock : Blocks
    {
        public static List<InfectedZoneBlock> InfectedZoneBlockList = new List<InfectedZoneBlock>(); 

        public InfectedZoneBlock(int x, int y, Texture2D text)
        {
            this._texture = text;
            this._hitBox = new Rectangle(x, y, this._texture.Width, this._texture.Height);
            this._isCollidable = false;
            this._isBreakable = false;
            this._isHideVisible = false;
            this._isJekyllVisible = false;

            InfectedZoneBlockList.Add(this);
            BlockList.Add(this);
        }
    }
}
