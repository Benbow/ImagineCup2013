using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    class MenuGUI
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

        public MenuGUI()
        {
            _bg = new Rectangle(0, 0, FirstGame.W, FirstGame.H);
            _text = Ressources.menu_bg;
            bouton1 = new Rectangle(223, 50, 350, 80);
            bouton2 = new Rectangle(223, 200, 350, 80);
            bouton3 = new Rectangle(223, 350, 350, 80);
            bouton1_text = Ressources.menu_bouton1;
            bouton2_text = Ressources.menu_bouton2;
            bouton3_text = Ressources.menu_bouton3;
            current = 1;
            current_text = Ressources.menu_current;
            current_rec = new Rectangle(bouton1.X - 10, bouton1.Y - 10, 370, 100);
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
                    current_rec = new Rectangle(bouton1.X - 10, bouton1.Y - 10, 370, 100);
                    spriteBatch.Draw(this.current_text, this.current_rec, Color.White);
                    break;
                case 2:
                    current_rec = new Rectangle(bouton2.X - 10, bouton2.Y - 10, 370, 100);
                    spriteBatch.Draw(this.current_text, this.current_rec, Color.White);
                    break;
                case 3:
                    current_rec = new Rectangle(bouton3.X - 10, bouton3.Y - 10, 370, 100);
                    spriteBatch.Draw(this.current_text, this.current_rec, Color.White);
                    break;
            }
        }

        public void Update(GamePadState pad)
        {
            Console.WriteLine(current);
            if (pad.IsButtonDown(Buttons.Start) && oldPad.IsButtonUp(Buttons.Start))
            {
                if (GameMain.Status == "on")
                    GameMain.Status = "menu";
                else
                    GameMain.Status = "on";
            }
            if (GameMain.Status == "menu")
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
            }
            oldPad = pad;
        }
    }
}
