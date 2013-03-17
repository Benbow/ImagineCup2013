using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    class GameMain
    {
        Map MyMap;

        bool statut_player;
        Jekyll LocalJekyll;
        Hide LocalHide;
        AlignementGUI alignement = new AlignementGUI(50, 30);
        MenuGUI menu = new MenuGUI();
        InventoryGUI inventory = new InventoryGUI();
        

        public static string Status;

        public GameMain()
        {
            this.LoadMap();
            
            //// Création Joueur + Carte
            MyMap = new Map(2000, 480);
            statut_player = false;
            LocalJekyll = new Jekyll(115, 100);
            LocalHide = new Hide(115, 100);
            Status = "on";

        }

        public void Update(KeyboardState keyboard, GamePadState pad, MouseState mouse, GameTime gameTime)
        {
            bool prec_statut = statut_player;

            if (!LocalHide.IsJumping)
                statut_player = LocalJekyll.Switch(pad);

            LocalJekyll.Statut = statut_player;
            LocalHide.Statut = statut_player;
            bool canopen = true;

            foreach (InteractZoneBlockWithPuzzle intblock in InteractZoneBlockWithPuzzle.InteractZoneBlockList)
            {
                if (intblock.IsActivate)
                    canopen = false;

            }
            if(canopen)
                menu.Update(pad, LocalJekyll, LocalHide);
            
           
            if (GameMain.Status != "menu")
            {
                if (!statut_player)
                {
                    if (GameMain.Status == "on")
                    {
                        if (prec_statut != statut_player)
                            LocalJekyll.InitChange(LocalHide.HitBox.X, LocalHide.HitBox.Y, LocalHide.DirectionPlayer);
                        LocalJekyll.Update(mouse, keyboard);
                    }
                    if(canopen)
                        inventory.Update(pad);
                    MyMap.Update(keyboard, pad, mouse, gameTime, LocalJekyll);

                }
                else if (statut_player)
                {

                    if (GameMain.Status == "on")
                    {
                        if (prec_statut != statut_player)
                            LocalHide.InitChange(LocalJekyll.HitBox.X, LocalJekyll.HitBox.Y, LocalJekyll.DirectionPlayer);
                        LocalHide.Update(mouse, keyboard);
                    }
                    MyMap.Update(keyboard, pad, mouse, gameTime, LocalHide);

                }

                alignement.Update(LocalJekyll.JekyllBias, LocalHide.HideBias);
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameMain.Status == "menu")
            {
                menu.Draw(spriteBatch);
            }
            else
            {
                if (!statut_player)
                {
                    MyMap.Draw(spriteBatch, LocalJekyll);
                    spriteBatch.DrawString(Ressources.cmpTitle, "Life :" + LocalJekyll.Health.ToString(), new Vector2(FirstGame.W-300, 30), Color.White);
                    LocalJekyll.Draw(spriteBatch);
                    if (GameMain.Status == "inventory")
                    {
                        inventory.Draw(spriteBatch);
                    }
                }
                else
                {
                    MyMap.Draw(spriteBatch, LocalHide);
                    spriteBatch.DrawString(Ressources.cmpTitle, "Life :" + LocalHide.Health.ToString(), new Vector2(FirstGame.W - 300, 30), Color.White);
                    LocalHide.Draw(spriteBatch);
                }

                alignement.Draw(spriteBatch);
                
            }
        }

        public void LoadMap()
        {
            List<string> lines = new List<string>();
            char[] delimiter = { ' ' };
            int count = 0;

            using (StreamReader reader = new StreamReader("Content/level1.txt"))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    lines.Add(line);
                    line = reader.ReadLine();
                }
            }

            foreach (string line in lines)
            {
                count++;
                string[] words = line.Split(delimiter);
                if (words[0] == "0") // Static Neutral Block
                {
                    if (words.Length == 4) // basic
                    {
                        int x = Convert.ToInt32(words[1]);
                        int y = Convert.ToInt32(words[2]);
                        int param = Convert.ToInt32(words[3]);
                        Texture2D text = Ressources.TextureList[param];
                        new StaticNeutralBlock(x, y, text);
                    }
                    else if (words.Length == 7) // complet
                    {
                        int x = Convert.ToInt32(words[1]);
                        int y = Convert.ToInt32(words[2]);
                        int param = Convert.ToInt32(words[3]);
                        Texture2D text = Ressources.TextureList[param];
                        bool breakable = Convert.ToBoolean(words[4]);
                        bool colidable = Convert.ToBoolean(words[5]);
                        int health = Convert.ToInt32(words[6]);
                        new StaticNeutralBlock(x, y, text, breakable, colidable, health);
                    }
                }
                else if (words[0] == "1") // Movable Neutral Block
                {
                    if (words.Length == 12) // basic
                    {
                        int x = Convert.ToInt32(words[1]);
                        int y = Convert.ToInt32(words[2]);
                        int param = Convert.ToInt32(words[3]);
                        Texture2D text = Ressources.TextureList[param];
                        int vector1 = Convert.ToInt32(words[4]);
                        int vector2 = Convert.ToInt32(words[5]);
                        Vector2 vec = new Vector2(vector1, vector2);
                        int speed = Convert.ToInt32(words[6]);
                        float animT = Convert.ToSingle(words[7]);
                        float waitT = Convert.ToSingle(words[8]);
                        bool anim = Convert.ToBoolean(words[9]);
                        bool reverse = Convert.ToBoolean(words[10]);
                        bool gravity = Convert.ToBoolean(words[11]);

                        new MovableNeutralBlock(x, y, text, vec, speed, animT, waitT, anim, reverse, gravity);
                    }
                    else if (words.Length == 15) // complet
                    {
                        int x = Convert.ToInt32(words[1]);
                        int y = Convert.ToInt32(words[2]);
                        int param = Convert.ToInt32(words[3]);
                        Texture2D text = Ressources.TextureList[param];
                        bool breakable = Convert.ToBoolean(words[4]);
                        bool colidable = Convert.ToBoolean(words[5]);
                        int health = Convert.ToInt32(words[6]);
                        int vector1 = Convert.ToInt32(words[7]);
                        int vector2 = Convert.ToInt32(words[8]);
                        Vector2 vec = new Vector2(vector1, vector2);
                        int speed = Convert.ToInt32(words[9]);
                        float animT = Convert.ToSingle(words[10]);
                        float waitT = Convert.ToSingle(words[11]);
                        bool anim = Convert.ToBoolean(words[12]);
                        bool reverse = Convert.ToBoolean(words[13]);
                        bool gravity = Convert.ToBoolean(words[14]);

                        new MovableNeutralBlock(x, y, text, breakable, colidable, health, vec, speed, animT, waitT, anim, reverse, gravity);
                    }

                }
                else if (words[0] == "2") // Static Ennemy Block
                {
                    if (words.Length == 9) // complet
                    {
                        int x = Convert.ToInt32(words[1]);
                        int y = Convert.ToInt32(words[2]);
                        int param = Convert.ToInt32(words[3]);
                        Texture2D text = Ressources.TextureList[param];
                        bool breakable = Convert.ToBoolean(words[4]);
                        bool colidable = Convert.ToBoolean(words[5]);
                        int health = Convert.ToInt32(words[6]);
                        bool haveSpotted = Convert.ToBoolean(words[7]);
                        int strength = Convert.ToInt32(words[8]);

                        new StaticEnnemyBlock(x, y, text, breakable, colidable, health, haveSpotted, strength);
                    }
                }
                else if (words[0] == "3") // Movable Ennemy Block
                {
                    if (words.Length == 18) // complet
                    {
                        int x = Convert.ToInt32(words[1]);
                        int y = Convert.ToInt32(words[2]);
                        int param = Convert.ToInt32(words[3]);
                        Texture2D text = Ressources.TextureList[param];
                        bool breakable = Convert.ToBoolean(words[4]);
                        bool colidable = Convert.ToBoolean(words[5]);
                        int health = Convert.ToInt32(words[6]);
                        int vector1 = Convert.ToInt32(words[7]);
                        int vector2 = Convert.ToInt32(words[8]);
                        Vector2 vec = new Vector2(vector1, vector2);
                        int speed = Convert.ToInt32(words[9]);
                        int strength = Convert.ToInt32(words[10]);
                        bool haveSpotted = Convert.ToBoolean(words[11]);
                        float animT = Convert.ToSingle(words[12]);
                        float waitT = Convert.ToSingle(words[13]);
                        bool anim = Convert.ToBoolean(words[14]);
                        bool reverse = Convert.ToBoolean(words[15]);
                        bool gravity = Convert.ToBoolean(words[16]);
                        float poids = Convert.ToSingle(words[17]);

                        new MovableEnnemyBlock(x, y, text, breakable, colidable, health, vec, speed, strength, haveSpotted, animT, waitT, anim, reverse, gravity, poids);
                    }
                }
                else if (words[0] == "4") // Ladder
                {
                    if (words.Length == 6) //basic
                    {
                        int x = Convert.ToInt32(words[1]);
                        int y = Convert.ToInt32(words[2]);
                        bool hv = Convert.ToBoolean(words[3]);
                        bool jv = Convert.ToBoolean(words[4]);
                        int param = Convert.ToInt32(words[5]);
                        Texture2D text = Ressources.TextureList[param];

                        new Ladder(x, y, hv, jv, text);
                    }
                }
                else if (words[0] == "5") // Interact Zone with Puzzle
                {
                    if (words.Length == 9)
                    {
                        int x = Convert.ToInt32(words[1]);
                        int y = Convert.ToInt32(words[2]);
                        int w = Convert.ToInt32(words[3]);
                        int h = Convert.ToInt32(words[4]);
                        int id = Convert.ToInt32(words[5]);
                        bool hv = Convert.ToBoolean(words[6]);
                        bool jv = Convert.ToBoolean(words[7]);
                        int param = Convert.ToInt32(words[8]);
                        Texture2D text = Ressources.TextureList[param];

                        new InteractZoneBlockWithPuzzle(x, y, w, h, id, hv, jv, text);
                    }
                }
                else if (words[0] == "6") // climbable block
                {
                    if (words.Length == 9) //basic
                    {
                        int x = Convert.ToInt32(words[1]);
                        int y = Convert.ToInt32(words[2]);
                        bool hv = Convert.ToBoolean(words[3]);
                        bool jv = Convert.ToBoolean(words[4]);
                        bool cl = Convert.ToBoolean(words[5]);
                        bool co = Convert.ToBoolean(words[6]);
                        int he = Convert.ToInt32(words[7]);
                        int param = Convert.ToInt32(words[8]);
                        Texture2D text = Ressources.TextureList[param];

                        new ClimbableBlock(x, y, hv, jv, cl, co, he, text);
                    }
                }
                else if (words[0] == "7") // Item
                {
                    
                    if (words.Length == 5) //basic
                    {
                        int x = Convert.ToInt32(words[1]);
                        int y = Convert.ToInt32(words[2]);
                        int id = Convert.ToInt32(words[3]);
                        int param = Convert.ToInt32(words[4]);
                        Texture2D text = Ressources.TextureList[param];
                        new ItemBlock(id, new Rectangle(x, y, text.Width, text.Height), text);
                        
                    }
                }
                else if (words[0] == "8") // Hiding block
                {
                    if (words.Length == 7) //basic
                    {
                        int x = Convert.ToInt32(words[1]);
                        int y = Convert.ToInt32(words[2]);
                        bool hv = Convert.ToBoolean(words[3]);
                        bool jv = Convert.ToBoolean(words[4]);
                        bool hi = Convert.ToBoolean(words[5]);
                        int param = Convert.ToInt32(words[6]);
                        Texture2D text = Ressources.TextureList[param];

                        new HidingBlock(x, y, hv, jv, hi, text);
                    }
                }
                else if (words[0] == "9") // Camera
                {
                    if (words.Length == 10) //basic
                    {
                        int x = Convert.ToInt32(words[1]);
                        int y = Convert.ToInt32(words[2]);
                        bool hv = Convert.ToBoolean(words[3]);
                        bool jv = Convert.ToBoolean(words[4]);
                        int speed = Convert.ToInt32(words[5]);
                        int waitTime = Convert.ToInt32(words[6]);
                        int animTime = Convert.ToInt32(words[7]);
                        int health = Convert.ToInt32(words[8]);
                        int param = Convert.ToInt32(words[9]);
                        Texture2D text = Ressources.TextureList[param];

                        new Camera(x, y, hv, jv, speed, waitTime, animTime, health, text);
                    }
                }
            }
        }
    }
}
