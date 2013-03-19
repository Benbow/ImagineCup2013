using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class LaunchableBlock : Blocks
    {
        public static List<LaunchableBlock> LaunchableBlockList = new List<LaunchableBlock>();
        private bool _isHide = false;

        public LaunchableBlock(int x, int y, bool hv, bool jv, bool hi, Texture2D text)
        {
            this._isBreakable = false;
            this._isCollidable = false;
            this._isActive = true;
            this._isHide = hi;
            this.IsHideVisible = hv;
            this.IsJekyllVisible = jv;
            this._texture = text;
            this._hitBox = new Rectangle(x, y, text.Width, text.Height);
            LaunchableBlockList.Add(this);
            BlockList.Add(this);
        }

        public bool IsHide
        {
            get { return this._isHide; }
            set { this._isHide = value; }
        }
    }
}
