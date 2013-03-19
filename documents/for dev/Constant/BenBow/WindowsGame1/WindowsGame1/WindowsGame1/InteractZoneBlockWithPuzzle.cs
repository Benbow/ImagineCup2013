using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class InteractZoneBlockWithPuzzle : Blocks
    {
        private bool _isActivate = false;
        private int _id;
        public static List<InteractZoneBlockWithPuzzle> InteractZoneBlockList = new List<InteractZoneBlockWithPuzzle>();

        public InteractZoneBlockWithPuzzle(int x, int y, int w, int h, int id, bool hidevision, bool jekyllvision,  Texture2D text)
        {
            this._isBreakable = false;
            this._isCollidable = false;
            this._hitBox = new Rectangle(x, y, w, h);
            this._texture = text;
            this._id = id;
            this._isHideVisible = hidevision;
            this._isJekyllVisible = jekyllvision;
            InteractZoneBlockList.Add(this);
            BlockList.Add(this);
        }

        public bool IsActivate
        {
            get { return this._isActivate; }
            set { this._isActivate = value; }
        }

        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }
    }
}
