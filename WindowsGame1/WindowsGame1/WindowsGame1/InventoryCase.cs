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

        private Rectangle img_rec;
        private Texture2D img_text;

        public static List<InventoryCase> InventoryCaseList= new List<InventoryCase>(); 

        public InventoryCase (Rectangle rec, bool stat, bool is_empty, string name, Rectangle img_rec, Texture2D img_text)
        {
            this._bg = rec;
            this._bg_text = Ressources.inventory_case;
            this.status = stat;
            this.is_empty = is_empty;
            this.name = name;
            this.img_rec = img_rec;
            this.img_text = img_text;

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

        public Rectangle Img_rec
        {
            get { return this.img_rec; }
            set { this.img_rec = value; }
        }

        public Texture2D Img_text
        {
            get { return this.img_text; }
            set { this.img_text = value; }
        }
    }
}
