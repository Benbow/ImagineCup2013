using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overload
{
    class CheckpointBlock : Blocks
    {
        public static List<CheckpointBlock> CheckpointBlockList = new List<CheckpointBlock>();

        public CheckpointBlock(int x, int y, Texture2D text)
        {
            this._texture = text;
            this._hitBox = new Rectangle(x, y, text.Width, text.Height);
            this._isCollidable = false;
            this._isBreakable = false;
            this._isHideVisible = true;
            this._isJekyllVisible = true;

            CheckpointBlockList.Add(this);
            BlockList.Add(this);
        }
    }
}
