using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class ClimbableBlock : Blocks
    {
        public static List<ClimbableBlock> ClimbableBlockList = new List<ClimbableBlock>();

        public ClimbableBlock(int x, int y, bool hv, bool jv, Texture2D text)
        {
            this._isBreakable = false;
            this._isCollidable = true;
            this.IsHideVisible = hv;
            this.IsJekyllVisible = jv;
            this._texture = text;
            this._hitBox = new Rectangle(x, y, text.Width, text.Height);
            ClimbableBlockList.Add(this);
            BlockList.Add(this);
        }
    }
}
