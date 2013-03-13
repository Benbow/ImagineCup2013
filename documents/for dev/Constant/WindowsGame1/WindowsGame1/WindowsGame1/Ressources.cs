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
    class Ressources
    {
        //FIELDS
        public static List<Texture2D> TextureList = new List<Texture2D>();

        public static Texture2D Player;
        public static Texture2D Wall;
        public static Texture2D Sol;
        public static Texture2D Int;
        public static Texture2D Ennemy;
        public static Texture2D Jekyll;
        public static Texture2D Hide;
        public static Texture2D Jekyll_Dissi;
        public static Texture2D Hide_Dissi;
        public static Texture2D invisible;
        public static Texture2D alignement_barre;
        public static Texture2D alignement_value;
        public static Texture2D interactZoneTest;
        public static Texture2D enigmes_fond;
        public static Texture2D enigmes_fond1;
        public static Texture2D LadderTest;
        public static Texture2D box;
        public static Texture2D boxH;
        public static Texture2D menu_bg;
        public static Texture2D menu_bouton1;
        public static Texture2D menu_bouton2;
        public static Texture2D menu_bouton3;
        public static Texture2D menu_current;

        public static Texture2D delimiterleftright;
        public static Texture2D delimiterupdown;

        //Load Content
        public static void LoadContent(ContentManager Content)
        {
            invisible = Content.Load<Texture2D>("invisible");
            alignement_barre = Content.Load<Texture2D>("alignement_barre");
            alignement_value = Content.Load<Texture2D>("alignement_value");
            
           
            enigmes_fond = Content.Load<Texture2D>("enigmes_fond");
            enigmes_fond1 = Content.Load<Texture2D>("enigmes_fond1");
            menu_bg = Content.Load<Texture2D>("menu_bg");
            menu_bouton1 = Content.Load<Texture2D>("bouton1");
            menu_bouton2 = Content.Load<Texture2D>("bouton2");
            menu_bouton3 = Content.Load<Texture2D>("bouton3");
            menu_current = Content.Load<Texture2D>("current");
            
            delimiterleftright = Content.Load<Texture2D>("leftright");
            delimiterupdown = Content.Load<Texture2D>("updown");

            Jekyll_Dissi = Content.Load<Texture2D>("jekyll_dissi");
            Hide_Dissi = Content.Load<Texture2D>("hide_dissi");

            Player = Content.Load<Texture2D>("tile");                   // 0
            Sol = Content.Load<Texture2D>("sol");                       // 1
            Wall = Content.Load<Texture2D>("wall");                     // 2
            Int = Content.Load<Texture2D>("int");                       // 3
            Ennemy = Content.Load<Texture2D>("ennemy");                 // 4
            Jekyll = Content.Load<Texture2D>("jekyll");                 // 5
            Hide = Content.Load<Texture2D>("hide");                     // 6
            LadderTest = Content.Load<Texture2D>("ladder");             // 7
            interactZoneTest = Content.Load<Texture2D>("InteractZone"); // 8
            box = Content.Load<Texture2D>("box");                       // 9
            boxH = Content.Load<Texture2D>("box_h");                    // 10

            TextureList.Add(Player);
            TextureList.Add(Sol);
            TextureList.Add(Wall);
            TextureList.Add(Int);
            TextureList.Add(Ennemy);
            TextureList.Add(Jekyll);
            TextureList.Add(Hide);
            TextureList.Add(LadderTest);
            TextureList.Add(interactZoneTest);
            TextureList.Add(box);
            TextureList.Add(boxH);
           
        }
    }
}
