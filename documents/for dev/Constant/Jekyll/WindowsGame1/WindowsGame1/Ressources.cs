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

        //Load Content
        public static void LoadContent(ContentManager Content)
        {
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
        }
           
    }
}
