using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class InventoryCase
    {
        private Rectangle _bg;
        private Texture2D _bg_text;
        private bool status;
        private bool is_empty;
        private string name;

        public static List<InventoryCase> InventoryCaseList= new List<InventoryCase>(); 

        public InventoryCase (Rectangle rec, bool stat, bool is_empty, string name)
        {
            this._bg = rec;
            this._bg_text = Ressources.inventory_case;
            this.status = stat;
            this.is_empty = is_empty;
            this.name = name;

            InventoryCaseList.Add(this);
        }

        public Rectangle Bg
        {
            get { return this._bg; }
            set { this._bg = value; }
        }

        public bool IsEmpty
        {
            get { return this.is_empty; }
            set { this.is_empty = value; }
        }

        public bool Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
    }
}
