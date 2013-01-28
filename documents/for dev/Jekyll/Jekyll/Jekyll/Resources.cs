using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jekyll
{
    class Resources
    {
        public static Texture2D Jekyll;
        public static Texture2D Hide;

        public static void LoadContent(ContentManager Content)
        {
            Jekyll = Content.Load<Texture2D>("guys");
            Hide = Content.Load<Texture2D>("hide");
        }
    }
}
