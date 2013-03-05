using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class StaticNeutralBlock : Blocks
    {
        public static List<StaticNeutralBlock> StaticNeutralList = new List<StaticNeutralBlock>();

        //constructeur structure basique (indestructible & colidable)
        public StaticNeutralBlock(int x, int y, Texture2D text)
        {
            this._texture = text;
            this._hitBox = new Rectangle(x, y, text.Width, text.Height);
            this._isBreakable = false;
            this._isCollidable = true;
            StaticNeutralList.Add(this);
            BlockList.Add(this);
        }
        //constructeur complet
        public StaticNeutralBlock(int x, int y, Texture2D text, bool isBreakable, bool isCollidable, int health)
        {
            this._texture = text;
            this._hitBox = new Rectangle(x, y, text.Width, text.Height);
            this._isBreakable = isBreakable;
            this._isCollidable = isCollidable;
            this._health = health;
            StaticNeutralList.Add(this);
            BlockList.Add(this);
        }
    }
}
