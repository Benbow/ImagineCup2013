using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Overload
{
    class EndGUI
    {
        private Rectangle hitbox;
        private Texture2D text;

        private GamePadState oldPad;

        public EndGUI ()
        {
            this.hitbox = new Rectangle(0, 0, FirstGame.W, FirstGame.H);
            this.text = Ressources.end_bg;
        }

        public void Update(GamePadState pad)
        {
            if (pad.IsButtonDown(Buttons.A) && oldPad.IsButtonUp(Buttons.A))
            {
                FirstGame.end = false;
                FirstGame.reload = true;
            }
            oldPad = pad;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.text, this.hitbox, Color.White);
            spriteBatch.DrawString(Ressources.puzzle0Lose, "Level Complete !", new Vector2(FirstGame.W/2 - 300, FirstGame.H/2 - 200), Color.Black);
            spriteBatch.DrawString(Ressources.font2, "You gain 1500 skill points !", new Vector2(FirstGame.W / 2 - 240, FirstGame.H / 2 - 120), Color.Black);
            spriteBatch.DrawString(Ressources.font2, "Scientist Skill Points : " + Math.Ceiling((FirstGame.Jp / 100) * 1500), new Vector2(FirstGame.W / 2 - 240, FirstGame.H / 2), Color.Black);
            spriteBatch.DrawString(Ressources.font2, "Monster Skill Points : " + Math.Ceiling((FirstGame.Hp / 100) * 1500), new Vector2(FirstGame.W / 2 - 240, FirstGame.H / 2 + 100), Color.Black);
        }

    }
}
