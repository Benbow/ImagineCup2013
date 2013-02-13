using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    class Map
    {
        private int _width;
        private int _height;

        bool blockMove = true;
        bool playerMove;
        Rectangle futurePos;
        int i;
        KeyboardState oldKeyboard;
        Keys jumpInitKey;

        int accelTimer;

        public Map(int x, int y)
        {
            _width = x;
            _height = y;
        }

        public void Update(KeyboardState keyboard, MouseState mouse, GameTime gameTime, Player player)
        {
            futurePos = player.HitBox;
            // Animation des blocs mouvants
            foreach (MovableNeutralBlock block in MovableNeutralBlock.MovableNeutralList)
            {
                block.Update(gameTime);
            }
            foreach (MovableEnnemyBlock block in MovableEnnemyBlock.MovableEnnemyList)
            {
                block.Update(gameTime, player, keyboard);
            }

            //Déplacements joueurs/cartes
            
            if (keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.Left) && !player.IsJumping)
            {
                player.AccelMode = 1;
                accelTimer = 0;
                player.BlockPLayer();
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                this.SetPlayerAccelMode(gameTime, player);
                this.Move(Keys.Left, player);
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                this.SetPlayerAccelMode(gameTime, player);
                this.Move(Keys.Right, player);
            }

            

            if (keyboard.IsKeyDown(Keys.Space) && oldKeyboard.IsKeyUp(Keys.Space) && !player.IsJumping)
            { 
                if(keyboard.IsKeyDown(Keys.Left))
                    jumpInitKey = Keys.Left;
                else if(keyboard.IsKeyDown(Keys.Right))
                    jumpInitKey = Keys.Right;
                else if (keyboard.IsKeyDown(Keys.Right) && keyboard.IsKeyDown(Keys.Left))
                    jumpInitKey = 0;
                else
                    jumpInitKey = 0;
                player.JumpPlayer();
            }

            oldKeyboard = keyboard;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Blocks block in Blocks.BlockList)
            {
                spriteBatch.Draw(block.Texture, block.HitBox, Color.White);
            }
        }

        public void SetPlayerAccelMode(GameTime gameTime, Player player)
        {
            if(!player.IsJumping && player.Speed <= 6)
                player.Speed += 0.01f;

            Console.WriteLine(player.Speed);

            if (accelTimer <= 2)
            {
                player.AccelMode = 1;
            }
            else if (accelTimer > 2 && accelTimer <= 4)
            {
                player.AccelMode = 2;
            }
            else if(accelTimer > 4)
            {
                player.AccelMode = 3;
            }
        }

        public void Move(Keys key, Player player)
        {
            player.SetAccelSpeed();
            if (player.IsJumping)
            {
                key = jumpInitKey;
                if (key == Keys.Left)
                {
                    futurePos.X -= (int)player.Speed;
                }
                else if (key == Keys.Right)
                {
                    futurePos.X += (int)player.Speed;
                }
            }
            else
            {
                if (key == Keys.Left)
                {
                    futurePos.X -= (int)player.Speed;
                }
                else if (key == Keys.Right)
                {
                    futurePos.X += (int)player.Speed;
                }
            }
            
            blockMove = true;
            foreach (StaticNeutralBlock block in StaticNeutralBlock.StaticNeutralList)
            {
                if(block.IsCollidable)
                {
                    if (block.HitBox.Intersects(futurePos))
                    {
                        if(player.FallingSpeed < 0 && player.AccelMode != 1)
                            player.FallingSpeed = 0;
                        blockMove = false;
                        player.AccelMode = 1;
                        accelTimer = 0;
                        
                        break;
                    }
                }
            }
            if (blockMove)
            {
                i = 0;
                playerMove = false;
                float sp;
                if (player.IsJumping)
                {
                    sp = player.SpeedInAir;
                }
                else {
                    sp = player.Speed;
                }
                foreach (StaticNeutralBlock block in StaticNeutralBlock.StaticNeutralList)
                {
                    i++;
                    if (i <= 4)
                    {
                        if(key == Keys.Left)
                        {
                            if ((i == 4 && player.HitBox.X - (FirstGame.W / 2) - sp < block.HitBox.X) || (i == 2 && (player.HitBox.X + (FirstGame.W / 2) - sp - block.HitBox.Width > block.HitBox.X)))
                            {
                               playerMove = true;
                            }
                        }
                        else if (key == Keys.Right)
                        {
                            if ((i == 2 && player.HitBox.X + ((FirstGame.W / 2)) - block.HitBox.Width > block.HitBox.X) || (i == 4 && player.HitBox.X - (FirstGame.W / 2) < block.HitBox.X))
                            {
                                playerMove = true;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (playerMove)
                {
                    if (key == Keys.Left)
                        player.MovePlayerLeft(true);
                    else if (key == Keys.Right)
                        player.MovePlayerRight(true);
                }     
                else
                {
                    if (key == Keys.Left)
                    {
                        player.MovePlayerLeft(false);
                        foreach (Blocks block in Blocks.BlockList)
                        {
                            if(player.IsJumping)
                                block.IncreaseCoordBlockX((int)player.SpeedInAir);
                            else
                                block.IncreaseCoordBlockX((int)player.Speed);
                        }
                    }
                    else if (key == Keys.Right)
                    {
                        player.MovePlayerRight(false);
                        foreach (Blocks block in Blocks.BlockList)
                        {
                            if (player.IsJumping)
                                block.DecreaseCoordBlockX((int)player.SpeedInAir);
                            else
                                block.DecreaseCoordBlockX((int)player.Speed);
                        }
                    }
                }
            }
        }
    }

}
