using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Overload
{
    class AlignementGUI : GUI
    {
        private Rectangle _barre;
        private Texture2D text_barre;
        private Rectangle _jauge;
        private Texture2D text_jauge;
        public static double _value;

        public AlignementGUI(int x1, int y1)
        {
            text_barre = Ressources.alignement_barre;
            text_jauge = Ressources.alignement_value;
            _barre = new Rectangle(x1, y1, text_barre.Width, text_barre.Height);
            _jauge = new Rectangle(x1+200 - (text_jauge.Width/2), y1, text_jauge.Width, text_jauge.Height);
            _value = 0;
            GUIList.Add(this);
        }

        public void Update(double j, double h)
        {
            //double jekyll_pourcent = (j / (j + h)) * 100;
            double hide_pourcent = (h / (j + h)) * 100;
            _value = hide_pourcent * 4;
            this._jauge.X = (int) _value;
            this._jauge.X += 50 - (text_jauge.Width / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.text_barre, this._barre, Color.White);
            spriteBatch.Draw(this.text_jauge, this._jauge, Color.White);
        }

        public Rectangle Alignement_Jauge{
            get
            {
                return _barre;
            }
            set
            {
                _barre = value;
            }
        }

        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }
}
