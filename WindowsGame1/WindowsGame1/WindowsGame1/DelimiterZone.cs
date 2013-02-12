using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class DelimiterZone : Blocks
    {
        public static List<DelimiterZone> DelimiterZoneList = new List<DelimiterZone>();

        //constructeur structure basique (indestructible & colidable)
        public DelimiterZone(int x, int y, int w, int h)
        {
            this._texture = Ressources.invisible;
            this._hitBox = new Rectangle(x, y, w, h);
            this._isBreakable = false;
            this._isCollidable = false;
            DelimiterZoneList.Add(this);
            BlockList.Add(this);
        }
    }
}
