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
        public static SpriteFont puzzle0Lose;

        public static Texture2D Player;
        public static Texture2D Wall;
        public static Texture2D Sol;
        public static Texture2D Int;
        public static Texture2D Ennemy;
        public static Texture2D Jekyll;
        public static Texture2D Hide;
        public static Texture2D Jekyll_Dissi;
        public static Texture2D Hide_Dissi;
        public static Texture2D Hide_Punch;
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
        public static Texture2D endzone;

        public static Texture2D wallMaison1;
        public static Texture2D wallMaison2;
        public static Texture2D wall2Maison1;
        public static Texture2D elevator;
        public static Texture2D plafondMaison1;
        public static Texture2D plafondMaison2;
        public static Texture2D platform;
        public static Texture2D wallMaison3;
        public static Texture2D infected;
        public static Texture2D elevator2;
        public static Texture2D bonus;

        public static Texture2D accueil_bg;
        public static Texture2D a_bouton1;
        public static Texture2D a_bouton2;
        public static Texture2D a_bouton3;

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
        public static Texture2D cible;
        public static Texture2D bottle;
        public static Texture2D bottle_crash;
        
        //enigmes 1
        public static Texture2D imgA;
        public static Texture2D imgB;
        public static Texture2D imgX;
        public static Texture2D imgY;
        public static Texture2D imgUp;
        public static Texture2D imgRight;
        public static Texture2D imgDown;
        public static Texture2D imgLeft;
        public static Texture2D ready;
        public static Texture2D go;
        public static Texture2D reload;
        public static Texture2D win;

        //Inventory Jekyll
        public static Texture2D inventory_bg;
        public static Texture2D inventory_case;
        public static Texture2D inventory_current;
        public static Texture2D item_masque;
        public static Texture2D inventory_masque;

        // Camera
        public static Texture2D spot;
        public static Texture2D camera;

        public static Texture2D delimiterleftright;
        public static Texture2D delimiterupdown;

        //ennmy
        public static Texture2D ennemySimple;
        public static Texture2D ennemySprite;
        public static Texture2D bullet;

        //Load Content
        public static void LoadContent(ContentManager Content)
        {
            accueil_bg = Content.Load<Texture2D>("accueil");
            a_bouton1 = Content.Load<Texture2D>("Play");
            a_bouton2 = Content.Load<Texture2D>("Credits");
            a_bouton3 = Content.Load<Texture2D>("Exit");
            

            font1 = Content.Load<SpriteFont>("SpriteFont1");
            cmpTitle = Content.Load<SpriteFont>("cmpTitle");
            cmpContent = Content.Load<SpriteFont>("cmpContent");
            puzzle0Lose = Content.Load<SpriteFont>("puzzle0");

            invisible = Content.Load<Texture2D>("invisible");
            alignement_barre = Content.Load<Texture2D>("alignement_barre");
            alignement_value = Content.Load<Texture2D>("alignement_value");

            Jekyll_Dissi = Content.Load<Texture2D>("jekyll_dissi");
            Hide_Dissi = Content.Load<Texture2D>("hide_dissi");
            Hide_Punch = Content.Load<Texture2D>("hide_punch");
            
           
            enigmes_fond = Content.Load<Texture2D>("enigmes_fond");
            enigmes_fond1 = Content.Load<Texture2D>("enigmes_fond1");
            imgA = Content.Load<Texture2D>("imgA");
            imgB = Content.Load<Texture2D>("imgB");
            imgY = Content.Load<Texture2D>("imgY");
            imgX = Content.Load<Texture2D>("imgX");
            imgLeft = Content.Load<Texture2D>("imgLEFT");
            imgRight = Content.Load<Texture2D>("imgRIGHT");
            imgUp = Content.Load<Texture2D>("imgUP");
            imgDown = Content.Load<Texture2D>("imgDOWN");
            ready = Content.Load<Texture2D>("ready");
            go = Content.Load<Texture2D>("go");
            reload = Content.Load<Texture2D>("reload");
            win = Content.Load<Texture2D>("win");


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
            cible = Content.Load<Texture2D>("cible");
            bottle = Content.Load<Texture2D>("bottle");
            bottle_crash = Content.Load<Texture2D>("bottle_crash");

            inventory_bg = Content.Load<Texture2D>("inventory_bg");
            inventory_case = Content.Load<Texture2D>("inventory_case");
            inventory_current = Content.Load<Texture2D>("inventory_current");
            inventory_masque = Content.Load<Texture2D>("Gas_Mask");

            spot = Content.Load<Texture2D>("spot");


            
            delimiterleftright = Content.Load<Texture2D>("leftright");
            delimiterupdown = Content.Load<Texture2D>("updown");

            bullet = Content.Load<Texture2D>("bullet");

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
            item_masque = Content.Load<Texture2D>("masque"); //11
            TextureList.Add(item_masque);
            camera = Content.Load<Texture2D>("camera"); // 12
            TextureList.Add(camera);
            ennemySimple = Content.Load<Texture2D>("enemy attack"); //13
            TextureList.Add(ennemySimple);
            ennemySprite = Content.Load<Texture2D>("Ennemy Walk"); //14
            TextureList.Add(ennemySprite);
            wallMaison1 = Content.Load<Texture2D>("wallMaison1"); //15
            TextureList.Add(wallMaison1);
            wallMaison2 = Content.Load<Texture2D>("wallMaison2"); //16
            TextureList.Add(wallMaison2);
            elevator = Content.Load<Texture2D>("elevators"); //17
            TextureList.Add(elevator);
            plafondMaison1 = Content.Load<Texture2D>("plafondMaison1"); //18
            TextureList.Add(plafondMaison1);
            plafondMaison2 = Content.Load<Texture2D>("plafondMaison2"); //19
            TextureList.Add(plafondMaison2);
            wall2Maison1 = Content.Load<Texture2D>("wall2Maison1"); //20
            TextureList.Add(wallMaison2);
            platform = Content.Load<Texture2D>("platform"); //21
            TextureList.Add(platform);
            wallMaison3 = Content.Load<Texture2D>("wallMaison3"); //22
            TextureList.Add(wallMaison3);
            infected = Content.Load<Texture2D>("infected"); //23
            TextureList.Add(infected);
            elevator2 = Content.Load<Texture2D>("elevators2"); //24
            TextureList.Add(elevator2);
            endzone = Content.Load<Texture2D>("endzone"); //25
            TextureList.Add(endzone);
            bonus = Content.Load<Texture2D>("bonus"); //26
            TextureList.Add(bonus);
        }
    }
}
