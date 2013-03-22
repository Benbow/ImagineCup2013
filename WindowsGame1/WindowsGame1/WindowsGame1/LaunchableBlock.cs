using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overload
{
    class LaunchableBlock : Blocks
    {
        public static List<LaunchableBlock> LaunchableBlockList = new List<LaunchableBlock>();

        public LaunchableBlock(int x, int y, bool hv, bool jv, Texture2D text)
        {
            this._isBreakable = false;
            this._isCollidable = true;
            this._isActive = true;
            this.IsHideVisible = hv;
            this.IsJekyllVisible = jv;
            this._texture = text;
            this._hitBox = new Rectangle(x, y, text.Width, text.Height);
            LaunchableBlockList.Add(this);
            BlockList.Add(this);
        }
    }
}
