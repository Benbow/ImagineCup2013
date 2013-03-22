using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Overload
{
    class Puzzle
    {
        protected int _x;
        protected int _y;
        protected Texture2D _text;
        protected Rectangle _hitBox;

        public static List<Puzzle> PuzzleList = new List<Puzzle>();
    }
}
