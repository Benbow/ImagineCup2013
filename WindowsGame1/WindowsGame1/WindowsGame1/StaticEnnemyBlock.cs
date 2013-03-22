using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overload
{
    class StaticEnnemyBlock : Blocks
    {
        private bool _haveSpotted;
        private int _strength;


        //constructeur complet
        public StaticEnnemyBlock(int x, int y, Texture2D text, bool isBreakable, bool isCollidable, int health, bool haveSpotted, int strength)
        {
            this._texture = text;
            this._hitBox = new Rectangle(x, y, text.Width, text.Height);
            this._isBreakable = isBreakable;
            this._isCollidable = isCollidable;
            this._health = health;
            this._haveSpotted = haveSpotted;
            this._strength = strength;
            BlockList.Add(this);
        }
    }
}
