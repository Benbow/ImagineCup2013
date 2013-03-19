using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class ItemBlock : Blocks
    {
        public static List<ItemBlock> ItemBlockList = new List<ItemBlock>();
        private int id;

        public ItemBlock(int id, Rectangle rec, Texture2D text)
        {
            this.id = id;
            this._isBreakable = false;
            this._isCollidable = false;
            this._isActive = true;
            this._isHideVisible = false;
            this._isJekyllVisible = false;
            this._hitBox = rec;
            this._texture = text;
            BlockList.Add(this);
            ItemBlockList.Add(this);
        }

        public int Id
        {
            get { return this.id; }
        }
    }
}
