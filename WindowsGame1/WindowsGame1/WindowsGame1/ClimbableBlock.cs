using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Overload
{
    class ClimbableBlock : Blocks
    {
        public static List<ClimbableBlock> ClimbableBlockList = new List<ClimbableBlock>();
        private bool _isClimbable = false;

        public ClimbableBlock(int x, int y, bool hv, bool jv, bool cl, bool co, int he, Texture2D text)
        {
            this._isBreakable = true;
            this._isCollidable = co;
            this._isActive = true;
            this._isClimbable = cl;
            this.IsHideVisible = hv;
            this.IsJekyllVisible = jv;
            this._health = he;
            this._texture = text;
            this._hitBox = new Rectangle(x, y, text.Width, text.Height);
            ClimbableBlockList.Add(this);
            BlockList.Add(this);
        }

        public bool IsClimbable
        {
            get { return this._isClimbable; }
            set { this._isClimbable = value; }
        }
    }
}
