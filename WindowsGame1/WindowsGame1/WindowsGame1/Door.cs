using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Overload
{
    class Door : Blocks
    {
        private bool isOpen = false;

        public static List<Door> DoorList = new List<Door>();

        public Door(int x, int y)
        {
            this._texture = Ressources.door_close;
            this._hitBox = new Rectangle(x, y, _texture.Width, _texture.Height);
            this._isCollidable = true;
            this._isHideVisible = false;
            this._isJekyllVisible = false;
            this.isOpen = false;
            DoorList.Add(this);
            BlockList.Add(this);
        }

        public bool IsOpen
        {
            get { return this.isOpen; }
            set { this.isOpen = value; }
        }
    }
}
