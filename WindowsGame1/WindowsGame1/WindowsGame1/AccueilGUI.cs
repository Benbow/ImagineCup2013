using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    class AccueilGUI
    {
        private Rectangle _bg;
        private Texture2D _text;
        private int current;
        private Texture2D current_text;
        private Rectangle current_rec;
        private Rectangle bouton1;
        private Rectangle bouton2;
        private Rectangle bouton3;
        private Texture2D bouton1_text;
        private Texture2D bouton2_text;
        private Texture2D bouton3_text;

        GamePadState oldPad;

         public AccueilGUI()
        {
            _bg = new Rectangle(0, 0, FirstGame.W, FirstGame.H);
            _text = Ressources.accueil_bg;
            bouton1_text = Ressources.invisible;
            bouton2_text = Ressources.invisible;
            bouton3_text = Ressources.invisible;
            bouton1 = new Rectangle((FirstGame.W / 2) - (350 / 2) + 542, (FirstGame.H / 2) - (80 / 2) - 53, bouton1_text.Width, bouton1_text.Height);
            bouton2 = new Rectangle((FirstGame.W / 2) - (350 / 2) + 395, (FirstGame.H / 2) - (80 / 2) + 67, bouton2_text.Width, bouton2_text.Height);
            bouton3 = new Rectangle((FirstGame.W / 2) - (350 / 2) + 593, (FirstGame.H / 2) - (80 / 2) + 187, bouton3_text.Width, bouton3_text.Height);
            current = 1;
            current_text = Ressources.menu_current;
            current_rec = new Rectangle(bouton1.X, bouton1.Y, bouton1_text.Width, bouton1_text.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._text, this._bg, Color.White);
            spriteBatch.Draw(this.bouton1_text, this.bouton1, Color.White);
            spriteBatch.Draw(this.bouton2_text, this.bouton2, Color.White);
            spriteBatch.Draw(this.bouton3_text, this.bouton3, Color.White);
            switch (current)
            {
                case 1:
                    current_rec = new Rectangle(bouton1.X, bouton1.Y, bouton1_text.Width, bouton1_text.Height);
                    spriteBatch.Draw(this.current_text, this.current_rec, Color.White);
                    break;
                case 2:
                    current_rec = new Rectangle(bouton2.X, bouton2.Y, bouton2_text.Width, bouton2_text.Height);
                    spriteBatch.Draw(this.current_text, this.current_rec, Color.White);
                    break;
                case 3:
                    current_rec = new Rectangle(bouton3.X, bouton3.Y, bouton3_text.Width, bouton3_text.Height);
                    spriteBatch.Draw(this.current_text, this.current_rec, Color.White);
                    break;

            }
        }

        public void Update(GamePadState pad)
        {
            if (pad.IsButtonDown(Buttons.LeftThumbstickDown) && !oldPad.IsButtonDown(Buttons.LeftThumbstickDown))
            {
                if (current < 3)
                {
                    current++;
                }
            }
            if (pad.IsButtonDown(Buttons.LeftThumbstickUp) && !oldPad.IsButtonDown(Buttons.LeftThumbstickUp))
            {
                if (current > 1)
                {
                    current--;
                }
            }
            if (pad.IsButtonDown(Buttons.A) && oldPad.IsButtonUp(Buttons.A))
            {
                if (current == 1)
                {
                    FirstGame.start = true;
                }
                else if (current == 3)
                {
                    FirstGame.exit = true;
                }
            }

            oldPad = pad;
        }
    }
}
