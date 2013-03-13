using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    class InventoryGUI
    {
        private Rectangle _bg;
        private Texture2D _bg_text;

        private Rectangle _current_rec ;
        private Texture2D _current_text ;
        private int _x;
        private int _y;

        GamePadState oldPad;

        public InventoryGUI()
        {
            this._bg_text = Ressources.inventory_bg;
            this._bg = new Rectangle(0 + 75, 0 + 75, _bg_text.Width, _bg_text.Height);
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    count++;
                    new InventoryCase(new Rectangle(this._bg.X + 15 + 90*j + 15*j, this._bg.Y + 15 + 90*i + 15*i, 90, 90), false, true,"Item" + count.ToString(), new Rectangle(this._bg.X + 15 + 90*j + 15*j + 8, this._bg.Y + 15 + 90*i + 15*i +8, 75, 75), Ressources.inventory_masque);
                }
            }

            this._current_rec = new Rectangle(this._bg.X +15, this._bg.Y+15, 90, 90);
            this._current_text = Ressources.inventory_current;
            this._x = 0;
            this._y = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(_bg_text, _bg, Color.White);
            foreach (InventoryCase cas in InventoryCase.InventoryCaseList)
            {
                if(cas.IsEmpty)
                    spriteBatch.Draw(Ressources.inventory_case, cas.Bg, Color.LightSlateGray);
                else if (cas.Status)
                    spriteBatch.Draw(Ressources.inventory_case, cas.Bg, Color.Chartreuse);
                else
                    spriteBatch.Draw(Ressources.inventory_case, cas.Bg, Color.Silver);

                if(!cas.IsEmpty)
                    spriteBatch.Draw(cas.Img_text, cas.Img_rec, Color.White);
            }
            spriteBatch.Draw(this._current_text, this._current_rec, Color.White);
            
        }

        public void Update(GamePadState pad)
        {
            this._current_rec.X = this._bg.X + 15 * this._x + 90 * this._x + 15;
            this._current_rec.Y = this._bg.Y + 15 * this._y + 90 * this._y + 15;

            if (pad.IsButtonDown(Buttons.A) && oldPad.IsButtonUp(Buttons.A))
            {
                int nb = this._y*6 + this._x;
                if (!InventoryCase.InventoryCaseList[nb].IsEmpty)
                {
                    foreach (InventoryCase cas in InventoryCase.InventoryCaseList)
                    {
                        cas.Status = false;
                    }
                    InventoryCase.InventoryCaseList[nb].Status = true;
                }
            }

            if (pad.IsButtonDown(Buttons.DPadUp) && oldPad.IsButtonUp(Buttons.DPadUp))
            {
                if (GameMain.Status == "inventory")
                {
                    GameMain.Status = "on";
                }
                else
                {
                    GameMain.Status = "inventory";
                }
            }

            if (pad.IsButtonDown(Buttons.LeftThumbstickLeft) && oldPad.IsButtonUp(Buttons.LeftThumbstickLeft))
            {
                if (this._x > 0)
                {
                    this._x--;
                }
            }

            if (pad.IsButtonDown(Buttons.LeftThumbstickRight) && oldPad.IsButtonUp(Buttons.LeftThumbstickRight))
            {
                if (this._x +1 < 6)
                {
                    this._x++;
                }
            }

            if (pad.IsButtonDown(Buttons.LeftThumbstickUp) && oldPad.IsButtonUp(Buttons.LeftThumbstickUp))
            {
                if (this._y > 0)
                {
                    this._y--;
                }
            }

            if (pad.IsButtonDown(Buttons.LeftThumbstickDown) && oldPad.IsButtonUp(Buttons.LeftThumbstickDown))
            {
                if (this._y+1 < 3)
                {
                    this._y++;
                }
            }

            oldPad = pad;
        }
    }
}
