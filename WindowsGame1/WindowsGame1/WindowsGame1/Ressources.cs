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
        public static Texture2D invisible;
        public static Texture2D alignement_barre;
        public static Texture2D alignement_value;
        public static Texture2D interactZoneTest;
        public static Texture2D enigmes_fond;
        public static Texture2D enigmes_fond1;
        public static Texture2D LadderTest;

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
            Jekyll = Content.Load<Texture2D>("guys"); // 5
            TextureList.Add(Jekyll);
            Hide = Content.Load<Texture2D>("hide"); // 6
            TextureList.Add(Hide);
            LadderTest = Content.Load<Texture2D>("ladder"); // 7
            TextureList.Add(LadderTest);
            interactZoneTest = Content.Load<Texture2D>("InteractZone"); // 8
            TextureList.Add(interactZoneTest);
           
        }
    }
}
