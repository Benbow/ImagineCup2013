using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace WindowsGame1
{
    class InteractZoneBlock : Blocks
    {
        private bool _isActivate = false;
        private int _id;
        public static List<InteractZoneBlock> InteractZoneBlockList = new List<InteractZoneBlock>();

        public InteractZoneBlock(int x, int y, int w, int h, int id)
        {
            this._isBreakable = false;
            this._isCollidable = false;
            this._hitBox = new Rectangle(x, y, w, h);
            this._texture = Ressources.interactZone;
            this._id = id;
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
