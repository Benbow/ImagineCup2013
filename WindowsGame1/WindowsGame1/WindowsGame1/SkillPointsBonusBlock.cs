using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overload
{
    class SkillPointsBonusBlock : Blocks
    {
        private int value;
        private bool status;

        public static  List<SkillPointsBonusBlock> SkillPointsBonusList = new List<SkillPointsBonusBlock>();

        public SkillPointsBonusBlock (int x, int y, Texture2D text, int value, bool status)
        {
            this._texture = text;
            this._hitBox = new Rectangle(x, y, text.Width, text.Height);
            this._isBreakable = false;
            this._isCollidable = false;
            this._isHideVisible = true;
            this._isJekyllVisible = true;
            this.value = value;
            this.status = status;

            SkillPointsBonusList.Add(this);
            BlockList.Add(this);
        }

        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public bool Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        
    }
}
