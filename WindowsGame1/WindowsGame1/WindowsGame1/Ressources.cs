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

        public static SpriteFont font1;
        public static SpriteFont cmpTitle;
        public static SpriteFont cmpContent;

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
        //home
        public static Texture2D menu_bouton1;
        public static Texture2D menu_bouton2;
        public static Texture2D menu_bouton3;
        public static Texture2D menu_current;
        //competences
        public static Texture2D cmp_bg;
        public static Texture2D comp_bg;
        public static Texture2D j_cmp1;
        public static Texture2D j_cmp2;
        public static Texture2D j_cmp3;
        public static Texture2D j_cmp4;
        public static Texture2D j_cmp5;
        public static Texture2D h_cmp1;
        public static Texture2D h_cmp2;
        public static Texture2D h_cmp3;
        public static Texture2D h_cmp4;
        public static Texture2D h_cmp5;
        public static Texture2D current2;


        public static Texture2D delimiterleftright;
        public static Texture2D delimiterupdown;

        //Load Content
        public static void LoadContent(ContentManager Content)
        {
            font1 = Content.Load<SpriteFont>("SpriteFont1");
            cmpTitle = Content.Load<SpriteFont>("cmpTitle");
            cmpContent = Content.Load<SpriteFont>("cmpContent");

            invisible = Content.Load<Texture2D>("invisible");
            alignement_barre = Content.Load<Texture2D>("alignement_barre");
            alignement_value = Content.Load<Texture2D>("alignement_value");

            Jekyll_Dissi = Content.Load<Texture2D>("jekyll_dissi");
            Hide_Dissi = Content.Load<Texture2D>("hide_dissi");
            
           
            enigmes_fond = Content.Load<Texture2D>("enigmes_fond");
            enigmes_fond1 = Content.Load<Texture2D>("enigmes_fond1");
            menu_bg = Content.Load<Texture2D>("menu_bg");
            menu_bouton1 = Content.Load<Texture2D>("bouton1");
            menu_bouton2 = Content.Load<Texture2D>("bouton2");
            menu_bouton3 = Content.Load<Texture2D>("bouton3");
            menu_current = Content.Load<Texture2D>("current");
            cmp_bg = Content.Load<Texture2D>("comp_bg");
            comp_bg = Content.Load<Texture2D>("cmp_bg");
            j_cmp1 = Content.Load<Texture2D>("j_cmp1");
            j_cmp2 = Content.Load<Texture2D>("j_cmp2");
            j_cmp3 = Content.Load<Texture2D>("j_cmp3");
            j_cmp4 = Content.Load<Texture2D>("j_cmp4");
            j_cmp5 = Content.Load<Texture2D>("j_cmp5");
            h_cmp1 = Content.Load<Texture2D>("h_cmp1");
            h_cmp2 = Content.Load<Texture2D>("h_cmp2");
            h_cmp3 = Content.Load<Texture2D>("h_cmp3");
            h_cmp4 = Content.Load<Texture2D>("h_cmp4");
            h_cmp5 = Content.Load<Texture2D>("h_cmp5");
            current2 = Content.Load<Texture2D>("current2");

            
            delimiterleftright = Content.Load<Texture2D>("leftright");
            delimiterupdown = Content.Load<Texture2D>("updown");

            Player = Content.Load<Texture2D>("tile"); // 0
            TextureList.Add(Player);
            Sol = Content.Load<Texture2D>("sol"); // 1
            TextureList.Add(Sol);
            Wall = Content.Load<Texture2D>("wall"); // 2
            TextureList.Add(Wall);
            Int = Content.Load<Texture2D>("int"); // 3
            TextureList.Add(Int);
            Ennemy = Content.Load<Texture2D>("ennemy"); // 4
            TextureList.Add(Ennemy);
            Jekyll = Content.Load<Texture2D>("jekyll"); // 5
            TextureList.Add(Jekyll);
            Hide = Content.Load<Texture2D>("hide"); // 6
            TextureList.Add(Hide);
            LadderTest = Content.Load<Texture2D>("ladder"); // 7
            TextureList.Add(LadderTest);
            interactZoneTest = Content.Load<Texture2D>("InteractZone"); // 8
            TextureList.Add(interactZoneTest);
            box = Content.Load<Texture2D>("box"); //9
            TextureList.Add(box);
            boxH = Content.Load<Texture2D>("box_h"); //10
            TextureList.Add(boxH);
           
        }
    }
}
