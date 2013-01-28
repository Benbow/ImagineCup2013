using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jekyll
{
    class GameMain
    {
        int statut_player;
        Jekyll LocalJekyll;
        Hide LocalHide;

        public GameMain()
        {
            statut_player = 0;
            LocalJekyll = new Jekyll();
            LocalHide = new Hide();
        }

        public void Update(MouseState mouse, KeyboardState keyboard)
        {
            if (statut_player == 0)
            {
                LocalJekyll.Update(mouse, keyboard);
                statut_player = LocalJekyll.Switch(keyboard, statut_player);
            }
            else if (statut_player == 1)
            {
                LocalHide.Update(mouse, keyboard);
                statut_player = LocalHide.Switch(keyboard, statut_player);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (statut_player == 0)
                LocalJekyll.Draw(spriteBatch);
            else if (statut_player == 1)
                LocalHide.Draw(spriteBatch);
        }
    }
}
