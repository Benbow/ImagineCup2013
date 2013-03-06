using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    class ClimbableBlock : Blocks
    {
        public static List<ClimbableBlock> ClimbableBlockList = new List<ClimbableBlock>();

        public ClimbableBlock(int x, int y, int w, int h)
        {
            this._isBreakable = false;
            this._isCollidable = true;
            this._texture = Ressources.box;
            this._hitBox = new Rectangle(x, y, w, h);
            ClimbableBlockList.Add(this);
            BlockList.Add(this);
        }
    }
}
