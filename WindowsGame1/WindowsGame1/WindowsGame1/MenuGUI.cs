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

        private double _hbias;
        private double _jbias;

        //home
        private Texture2D current_text;
        private Rectangle current_rec;
        private Rectangle bouton1;
        private Rectangle bouton2;
        private Rectangle bouton3;
        private Texture2D bouton1_text;
        private Texture2D bouton2_text;
        private Texture2D bouton3_text;

        //competences
        private Texture2D cmp_bg = Ressources.cmp_bg;
        private Rectangle cmp_rec = new Rectangle(0, 0, 796, 480);

        private Texture2D current2_text = Ressources.current2;
        private Rectangle current2_rec = new Rectangle(0, 61, 398, 100);

        private int side = 0;

        private Rectangle j_cmp1 = new Rectangle(0, 61, 398, 100);
        private Texture2D j_cmp1_text = Ressources.comp_bg;
        private Rectangle j_cmp1_img_rec = new Rectangle(10, 71, 91, 80);
        private Texture2D j_cmp1_img = Ressources.j_cmp1;
        private bool j_cmp1_status = false;
        private string j_cmp1_desc;
        private int j_cmp1_price = 10;

        private Rectangle j_cmp2 = new Rectangle(0, 161, 398, 100);
        private Texture2D j_cmp2_text = Ressources.comp_bg;
        private Rectangle j_cmp2_img_rec = new Rectangle(10, 171, 91, 80);
        private Texture2D j_cmp2_img = Ressources.j_cmp2;
        private bool j_cmp2_status = false;
        private string j_cmp2_desc;
        private int j_cmp2_price = 20;

        private Rectangle j_cmp3 = new Rectangle(0, 261, 398, 100);
        private Texture2D j_cmp3_text = Ressources.comp_bg;
        private Rectangle j_cmp3_img_rec = new Rectangle(10, 271, 91, 80);
        private Texture2D j_cmp3_img = Ressources.j_cmp3;
        private bool j_cmp3_status = false;
        private string j_cmp3_desc;
        private int j_cmp3_price = 10;

        private Rectangle j_cmp4 = new Rectangle(0, 361, 398, 100);
        private Texture2D j_cmp4_text = Ressources.comp_bg;
        private Rectangle j_cmp4_img_rec = new Rectangle(10, 371, 91, 80);
        private Texture2D j_cmp4_img = Ressources.j_cmp4;
        private bool j_cmp4_status = false;
        private string j_cmp4_desc;
        private int j_cmp4_price = 25;

        private Rectangle j_cmp5 = new Rectangle(0, 361, 398, 100);
        private Texture2D j_cmp5_text = Ressources.comp_bg;
        private Rectangle j_cmp5_img_rec = new Rectangle(10, 371, 91, 80);
        private Texture2D j_cmp5_img = Ressources.j_cmp5;
        private bool j_cmp5_status = false;
        private string j_cmp5_desc;
        private int j_cmp5_price = 50;


        private Rectangle h_cmp1 = new Rectangle(398, 61, 398, 100);
        private Texture2D h_cmp1_text = Ressources.comp_bg;
        private Rectangle h_cmp1_img_rec = new Rectangle(408, 71, 91, 80);
        private Texture2D h_cmp1_img = Ressources.h_cmp1;
        private bool h_cmp1_status = false;
        private string h_cmp1_desc;
        private int h_cmp1_price = 20;

        private Rectangle h_cmp2 = new Rectangle(398, 161, 398, 100);
        private Texture2D h_cmp2_text = Ressources.comp_bg;
        private Rectangle h_cmp2_img_rec = new Rectangle(408, 171, 91, 80);
        private Texture2D h_cmp2_img = Ressources.h_cmp2;
        private bool h_cmp2_status = false;
        private string h_cmp2_desc;
        private int h_cmp2_price = 40;

        private Rectangle h_cmp3 = new Rectangle(398, 261, 398, 100);
        private Texture2D h_cmp3_text = Ressources.comp_bg;
        private Rectangle h_cmp3_img_rec = new Rectangle(408, 271, 91, 80);
        private Texture2D h_cmp3_img = Ressources.h_cmp3;
        private bool h_cmp3_status = true;
        private string h_cmp3_desc;
        private int h_cmp3_price = 20;

        private Rectangle h_cmp4 = new Rectangle(398, 361, 398, 100);
        private Texture2D h_cmp4_text = Ressources.comp_bg;
        private Rectangle h_cmp4_img_rec = new Rectangle(408, 371, 91, 80);
        private Texture2D h_cmp4_img = Ressources.h_cmp4;
        private bool h_cmp4_status = false;
        private string h_cmp4_desc;
        private int h_cmp4_price = 50;

        private Rectangle h_cmp5 = new Rectangle(398, 361, 398, 100);
        private Texture2D h_cmp5_text = Ressources.comp_bg;
        private Rectangle h_cmp5_img_rec = new Rectangle(408, 371, 91, 80);
        private Texture2D h_cmp5_img = Ressources.h_cmp5;
        private bool h_cmp5_status = false;
        private string h_cmp5_desc;
        private int h_cmp5_price = 120;

        private string status;

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
            status = "home";
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._text, this._bg, Color.White);
            if (this.status == "home")
            {
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
            else if (this.status == "competences")
            {
                spriteBatch.Draw(this.cmp_bg, this.cmp_rec, Color.White);
                spriteBatch.DrawString(Ressources.font1, _jbias.ToString(), new Vector2(220, 13), Color.White);
                spriteBatch.DrawString(Ressources.font1, _hbias.ToString(), new Vector2(627, 13), Color.White);

                if (j_cmp1_status)
                {
                    spriteBatch.Draw(this.j_cmp1_text, this.j_cmp1, Color.Green);
                    spriteBatch.DrawString(Ressources.cmpContent, "Owned", new Vector2(180, 97), Color.White);
                }
                else
                {
                    spriteBatch.Draw(this.j_cmp1_text, this.j_cmp1, Color.Gray);
                    spriteBatch.DrawString(Ressources.cmpContent, "Not Owned", new Vector2(180, 97), Color.White);
                }
                spriteBatch.Draw(this.j_cmp1_img, this.j_cmp1_img_rec, Color.White);
                spriteBatch.DrawString(Ressources.cmpTitle, "Status :", new Vector2(120, 95), Color.White);
                spriteBatch.DrawString(Ressources.cmpTitle, "Price :", new Vector2(120, 75), Color.White);
                spriteBatch.DrawString(Ressources.cmpContent, j_cmp1_price.ToString(), new Vector2(170, 77), Color.White);

                if (j_cmp2_status)
                {
                    spriteBatch.Draw(this.j_cmp2_text, this.j_cmp2, Color.Green);
                    spriteBatch.DrawString(Ressources.cmpContent, "Owned", new Vector2(180, 197), Color.White);
                }
                else
                {
                    spriteBatch.Draw(this.j_cmp2_text, this.j_cmp2, Color.Gray);
                    spriteBatch.DrawString(Ressources.cmpContent, "Not Owned", new Vector2(180, 197), Color.White);
                }
                spriteBatch.Draw(this.j_cmp2_img, this.j_cmp2_img_rec, Color.White);
                spriteBatch.DrawString(Ressources.cmpTitle, "Status :", new Vector2(120, 195), Color.White);
                spriteBatch.DrawString(Ressources.cmpTitle, "Price :", new Vector2(120, 175), Color.White);
                spriteBatch.DrawString(Ressources.cmpContent, j_cmp2_price.ToString(), new Vector2(170, 177),
                                       Color.White);

                if (!j_cmp4_status)
                {
                    if (j_cmp3_status)
                    {
                        spriteBatch.Draw(this.j_cmp3_text, this.j_cmp3, Color.Green);
                        spriteBatch.DrawString(Ressources.cmpContent, "Owned", new Vector2(180, 297), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(this.j_cmp3_text, this.j_cmp3, Color.Gray);
                        spriteBatch.DrawString(Ressources.cmpContent, "Not Owned", new Vector2(180, 297), Color.White);
                    }
                    spriteBatch.Draw(this.j_cmp3_img, this.j_cmp3_img_rec, Color.White);
                    spriteBatch.DrawString(Ressources.cmpTitle, "Price :", new Vector2(120, 275), Color.White);
                    spriteBatch.DrawString(Ressources.cmpTitle, "Status :", new Vector2(120, 295), Color.White);
                    spriteBatch.DrawString(Ressources.cmpContent, j_cmp3_price.ToString(), new Vector2(170, 277),
                                           Color.White);
                }

                if (j_cmp3_status && !j_cmp4_status)
                {
                    if (j_cmp4_status)
                    {
                        spriteBatch.Draw(this.j_cmp4_text, this.j_cmp4, Color.Green);
                        spriteBatch.DrawString(Ressources.cmpContent, "Owned", new Vector2(180, 397), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(this.j_cmp4_text, this.j_cmp4, Color.Goldenrod);
                        spriteBatch.DrawString(Ressources.cmpContent, "Improvable, Owned Hiding I",
                                               new Vector2(180, 397), Color.White);
                    }
                    spriteBatch.Draw(this.j_cmp4_img, this.j_cmp4_img_rec, Color.White);
                    spriteBatch.DrawString(Ressources.cmpTitle, "Price :", new Vector2(120, 375), Color.White);
                    spriteBatch.DrawString(Ressources.cmpTitle, "Status :", new Vector2(120, 395), Color.White);
                    spriteBatch.DrawString(Ressources.cmpContent, j_cmp4_price.ToString(), new Vector2(170, 377),
                                           Color.White);
                }

                if (j_cmp4_status)
                {
                    j_cmp4 = new Rectangle(0, 261, 398, 100);
                    j_cmp4_img_rec = new Rectangle(10, 271, 91, 80);
                    spriteBatch.Draw(this.j_cmp4_text, this.j_cmp4, Color.Green);
                    spriteBatch.DrawString(Ressources.cmpContent, "Owned", new Vector2(180, 297), Color.White);
                    spriteBatch.Draw(this.j_cmp4_img, this.j_cmp4_img_rec, Color.White);
                    spriteBatch.DrawString(Ressources.cmpTitle, "Price :", new Vector2(120, 275), Color.White);
                    spriteBatch.DrawString(Ressources.cmpTitle, "Status :", new Vector2(120, 295), Color.White);
                    spriteBatch.DrawString(Ressources.cmpContent, j_cmp4_price.ToString(), new Vector2(170, 277),
                                           Color.White);

                    spriteBatch.Draw(this.j_cmp5_text, this.j_cmp5, Color.Goldenrod);
                    spriteBatch.DrawString(Ressources.cmpContent, "Improvable, Owned Hiding II", new Vector2(180, 397),
                                           Color.White);
                    spriteBatch.Draw(this.j_cmp5_img, this.j_cmp5_img_rec, Color.White);
                    spriteBatch.DrawString(Ressources.cmpTitle, "Price :", new Vector2(120, 375), Color.White);
                    spriteBatch.DrawString(Ressources.cmpTitle, "Status :", new Vector2(120, 395), Color.White);
                    spriteBatch.DrawString(Ressources.cmpContent, j_cmp5_price.ToString(), new Vector2(170, 377),
                                           Color.White);
                }

                if (h_cmp1_status)
                {
                    spriteBatch.Draw(this.h_cmp1_text, this.h_cmp1, Color.Green);
                    spriteBatch.DrawString(Ressources.cmpContent, "Owned", new Vector2(570, 97), Color.White);
                }
                else
                {
                    spriteBatch.Draw(this.h_cmp1_text, this.h_cmp1, Color.Gray);
                    spriteBatch.DrawString(Ressources.cmpContent, "Not Owned", new Vector2(570, 97), Color.White);
                }
                spriteBatch.Draw(this.h_cmp1_img, this.h_cmp1_img_rec, Color.White);
                spriteBatch.DrawString(Ressources.cmpTitle, "Status :", new Vector2(518, 95), Color.White);
                spriteBatch.DrawString(Ressources.cmpTitle, "Price :", new Vector2(518, 75), Color.White);
                spriteBatch.DrawString(Ressources.cmpContent, h_cmp1_price.ToString(), new Vector2(568, 77), Color.White);

                if (h_cmp2_status)
                {
                    spriteBatch.Draw(this.h_cmp2_text, this.h_cmp2, Color.Green);
                    spriteBatch.DrawString(Ressources.cmpContent, "Owned", new Vector2(570, 197), Color.White);
                }
                else
                {
                    spriteBatch.Draw(this.h_cmp2_text, this.h_cmp2, Color.Gray);
                    spriteBatch.DrawString(Ressources.cmpContent, "Not Owned", new Vector2(570, 197), Color.White);
                }
                spriteBatch.Draw(this.h_cmp2_img, this.h_cmp2_img_rec, Color.White);
                spriteBatch.DrawString(Ressources.cmpTitle, "Status :", new Vector2(518, 195), Color.White);
                spriteBatch.DrawString(Ressources.cmpTitle, "Price :", new Vector2(518, 175), Color.White);
                spriteBatch.DrawString(Ressources.cmpContent, h_cmp2_price.ToString(), new Vector2(568, 177),
                                       Color.White);

                if (!h_cmp4_status)
                {
                    if (h_cmp3_status)
                    {
                        spriteBatch.Draw(this.h_cmp3_text, this.h_cmp3, Color.Green);
                        spriteBatch.DrawString(Ressources.cmpContent, "Owned", new Vector2(570, 297), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(this.h_cmp3_text, this.h_cmp3, Color.Gray);
                        spriteBatch.DrawString(Ressources.cmpContent, "Not Owned", new Vector2(570, 297), Color.White);
                    }
                    spriteBatch.Draw(this.h_cmp3_img, this.h_cmp3_img_rec, Color.White);
                    spriteBatch.DrawString(Ressources.cmpTitle, "Status :", new Vector2(518, 295), Color.White);
                    spriteBatch.DrawString(Ressources.cmpTitle, "Price :", new Vector2(518, 275), Color.White);
                    spriteBatch.DrawString(Ressources.cmpContent, h_cmp3_price.ToString(), new Vector2(568, 277),
                                           Color.White);
                }

                if (h_cmp3_status && !h_cmp4_status)
                {
                    if (h_cmp4_status)
                    {
                        spriteBatch.Draw(this.h_cmp4_text, this.h_cmp4, Color.Green);
                        spriteBatch.DrawString(Ressources.cmpContent, "Owned", new Vector2(570, 397), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(this.h_cmp4_text, this.h_cmp4, Color.Goldenrod);
                        spriteBatch.DrawString(Ressources.cmpContent, "Improvable, Owned Health II",
                                               new Vector2(570, 397), Color.White);
                    }
                    spriteBatch.Draw(this.h_cmp4_img, this.h_cmp4_img_rec, Color.White);
                    spriteBatch.DrawString(Ressources.cmpTitle, "Status :", new Vector2(518, 395), Color.White);
                    spriteBatch.DrawString(Ressources.cmpTitle, "Price :", new Vector2(518, 375), Color.White);
                    spriteBatch.DrawString(Ressources.cmpContent, h_cmp4_price.ToString(), new Vector2(568, 377),
                                           Color.White);
                }

                if (h_cmp4_status)
                {
                    h_cmp4 = new Rectangle(398, 261, 398, 100);
                    h_cmp4_img_rec = new Rectangle(408, 271, 91, 80);
                    spriteBatch.Draw(this.h_cmp4_text, this.h_cmp4, Color.Green);
                    spriteBatch.DrawString(Ressources.cmpContent, "Owned", new Vector2(578, 297), Color.White);
                    spriteBatch.Draw(this.h_cmp4_img, this.h_cmp4_img_rec, Color.White);
                    spriteBatch.DrawString(Ressources.cmpTitle, "Price :", new Vector2(518, 275), Color.White);
                    spriteBatch.DrawString(Ressources.cmpTitle, "Status :", new Vector2(518, 295), Color.White);
                    spriteBatch.DrawString(Ressources.cmpContent, h_cmp4_price.ToString(), new Vector2(568, 277),Color.White);

                    spriteBatch.Draw(this.h_cmp5_text, this.h_cmp5, Color.Goldenrod);
                    spriteBatch.DrawString(Ressources.cmpContent, "Improvable, Owned Hiding II", new Vector2(578, 397),Color.White);
                    spriteBatch.Draw(this.h_cmp5_img, this.h_cmp5_img_rec, Color.White);
                    spriteBatch.DrawString(Ressources.cmpTitle, "Price :", new Vector2(518, 375), Color.White);
                    spriteBatch.DrawString(Ressources.cmpTitle, "Status :", new Vector2(518, 395), Color.White);
                    spriteBatch.DrawString(Ressources.cmpContent, h_cmp5_price.ToString(), new Vector2(568, 377), Color.White);
                }


                if (this.side == 0)
                {
                    switch (current)
                    {
                        case 1:
                            current2_rec = new Rectangle(0, 61, 398, 100);
                            spriteBatch.Draw(this.current2_text, this.current2_rec, Color.White);
                            break;
                        case 2:
                            current2_rec = new Rectangle(0, 161, 398, 100);
                            spriteBatch.Draw(this.current2_text, this.current2_rec, Color.White);
                            break;
                        case 3:
                            current2_rec = new Rectangle(0, 261, 398, 100);
                            spriteBatch.Draw(this.current2_text, this.current2_rec, Color.White);
                            break;
                        case 4:
                            if (j_cmp3_status)
                            {
                                current2_rec = new Rectangle(0, 361, 398, 100);
                                spriteBatch.Draw(this.current2_text, this.current2_rec, Color.White);
                            }
                            else
                            {
                                current = 3;
                            }
                            break;
                    }
                }
                else
                {
                    switch (current)
                    {
                        case 1:
                            current2_rec = new Rectangle(398, 61, 398, 100);
                            spriteBatch.Draw(this.current2_text, this.current2_rec, Color.White);
                            break;
                        case 2:
                            current2_rec = new Rectangle(398, 161, 398, 100);
                            spriteBatch.Draw(this.current2_text, this.current2_rec, Color.White);
                            break;
                        case 3:
                            current2_rec = new Rectangle(398, 261, 398, 100);
                            spriteBatch.Draw(this.current2_text, this.current2_rec, Color.White);
                            break;
                        case 4:
                            current2_rec = new Rectangle(398, 361, 398, 100);
                            spriteBatch.Draw(this.current2_text, this.current2_rec, Color.White);
                            break;
                    }
                }
            }
        }

        public void Update(GamePadState pad, Jekyll jekyll, Hide hide)
        {
            this._jbias = jekyll.JSkillPoints;
            this._hbias = hide.HSkillPoints;

            if (pad.IsButtonDown(Buttons.Start) && oldPad.IsButtonUp(Buttons.Start))
            {
                if (GameMain.Status == "on")
                {
                    current = 1;
                    this.status = "home";
                    GameMain.Status = "menu";
                }
                else
                    GameMain.Status = "on";
            }
            if (pad.IsButtonDown(Buttons.B) && oldPad.IsButtonUp(Buttons.B))
            {
                if (GameMain.Status == "menu")
                {
                    if (this.status == "home")
                    {
                        GameMain.Status = "on";
                    }
                    else if(this.status == "competences")
                    {
                        current = 1;
                        this.status = "home";
                    }
                }
            }
            if (GameMain.Status == "menu")
            {
                if (this.status == "home")
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
                    if (pad.IsButtonDown(Buttons.A) && oldPad.IsButtonUp(Buttons.A) && current == 1)
                    {
                        this.status = "competences";
                    }
                }
                else if (this.status == "competences")
                {
                    if (pad.IsButtonDown(Buttons.LeftThumbstickDown) && !oldPad.IsButtonDown(Buttons.LeftThumbstickDown))
                    {
                        if (current < 4)
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
                    if (pad.IsButtonDown(Buttons.LeftThumbstickRight) && !oldPad.IsButtonDown(Buttons.LeftThumbstickRight))
                    {
                        if (this.side == 0)
                        {
                            this.side = 1;
                        }
                    }
                    if (pad.IsButtonDown(Buttons.LeftThumbstickLeft) && !oldPad.IsButtonDown(Buttons.LeftThumbstickLeft))
                    {
                        if (this.side == 1)
                        {
                            this.side = 0;
                        }
                    }
                    if (pad.IsButtonDown(Buttons.A) && oldPad.IsButtonUp(Buttons.A))
                    {
                        if (side == 0)
                        {
                            switch (current)
                            {
                                case 1:
                                    if (!j_cmp1_status && (_jbias - j_cmp1_price >= 0))
                                    {
                                        jekyll.JSkillPoints -= j_cmp1_price;
                                        j_cmp1_status = true;
                                    }
                                    break;
                                case 2:
                                    if (!j_cmp2_status && (_jbias - j_cmp2_price >= 0))
                                    {
                                        jekyll.JSkillPoints -= j_cmp2_price;
                                        j_cmp2_status = true;
                                    }
                                    break;
                                case 3:
                                    if (!j_cmp3_status && (_jbias - j_cmp3_price >= 0))
                                    {
                                        jekyll.JSkillPoints -= j_cmp3_price;
                                        j_cmp3_status = true;
                                    }
                                    break;
                                case 4:
                                    if (!j_cmp4_status && (_jbias - j_cmp4_price >= 0))
                                    {
                                        jekyll.JSkillPoints -= j_cmp4_price;
                                        j_cmp4_status = true;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (current)
                            {
                                case 1:
                                    if (!h_cmp1_status && (_hbias - h_cmp1_price >= 0))
                                    {
                                        hide.HSkillPoints -= h_cmp1_price;
                                        h_cmp1_status = true;
                                    }
                                    break;
                                case 2:
                                    if (!h_cmp2_status && (_hbias - h_cmp2_price >= 0))
                                    {
                                        hide.HSkillPoints -= h_cmp2_price;
                                        h_cmp2_status = true;
                                    }
                                    break;
                                case 3:
                                    if (!h_cmp3_status && (_hbias - h_cmp3_price >= 0))
                                    {
                                        hide.HSkillPoints -= h_cmp3_price;
                                        h_cmp3_status = true;
                                    }
                                    break;
                                case 4:
                                    if (!h_cmp4_status && (_hbias - h_cmp4_price >= 0))
                                    {
                                        hide.HSkillPoints -= h_cmp4_price;
                                        h_cmp4_status = true;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            oldPad = pad;
        }
    }
}
